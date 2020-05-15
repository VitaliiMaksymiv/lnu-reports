using System;
using System.Configuration;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using MimeKit;
using MailKit.Net.Smtp;
using UserManagement.Models;
using UserManagement.Models.db;
using UserManagement.Utilities;

namespace UserManagement.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        public ApplicationDbContext db = new ApplicationDbContext();

        public AccountController()
        {
        }

        public AccountController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        //
        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            ViewBag.Success = TempData["Success"];
           
            return View();
        }

        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            ApplicationUser user = UserManager.FindByEmail(model.Email);
            if (user != null && !user.IsActive)
            {
                ModelState.AddModelError("", "This user is not active.");
                return View(model);
            }

            HttpCookie cookie = new HttpCookie("UserName", model.Email);
            HttpCookie cookie1 = HttpContext.Request.Cookies.Get("UserName");
            if (model.RememberMe)
            {
                cookie.Expires = DateTime.Now.AddDays(10);
            }
            Response.Cookies.Add(cookie);

            


            var result = await SignInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, shouldLockout: false);

            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToLocal(returnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.RequiresVerification:
                    return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = model.RememberMe });
                case SignInStatus.Failure:
                default:
                    ModelState.AddModelError("", "Invalid login attempt.");
                    return View(model);
            }
          
        }

        //
        // GET: /Account/Register
        [AllowAnonymous]
        public ActionResult Register()
        {
            ViewBag.AllCathedras = db.Cathedra.ToList().Select(x => x.Name);
            ViewBag.AllFaculties = db.Faculty.ToList().Select(x => x.Name);
            
            return View();
        }

        //
        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    UserName = model.Email,
                    Email = model.Email,
                    BirthDate = DateTime.Now,
                    GraduationDate = DateTime.Now,
                    AwardingDate = DateTime.Now,
                    DefenseYear = DateTime.Now,
                    PublicationCounterBeforeRegistration = model.PublicationsBeforeRegister,
                };
                var result = await UserManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    UserManager.AddToRole(UserManager.FindByName(model.Email).Id, "Викладач");
                    //await SignInManager.SignInAsync(user, isPersistent:false, rememberBrowser:false);
                    var u = db.Users.Where(x => x.UserName == model.Email).First();
                    u.Cathedra = db.Cathedra.Where(x => x.Name.Equals(model.Cathedra)).FirstOrDefault();
                    foreach (var i in Enum.GetNames(typeof(Language)))
                    {
                        u.I18nUserInitials.Add(new I18nUserInitials()
                        {
                            Language = (Language)Enum.Parse(typeof(Language), i),
                            FirstName = "",
                            LastName = "",
                            FathersName = "",
                            User = u,
                        });
                    }
                    db.SaveChanges();
                    TempData["Success"] = true;
                    return  RedirectToAction("Login", "Account");
                }
                AddErrors(result);
            }
            ViewBag.AllCathedras = db.Cathedra.ToList().Select(x => x.Name);
            ViewBag.AllFaculties = db.Faculty.ToList().Select(x => x.Name);
            return View(model);
        }

        //
        // GET: /Account/ForgotPassword
        [AllowAnonymous]
        public ActionResult ForgotPassword()
        {
            return View();
        }

        //
        // POST: /Account/ForgotPassword
        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await UserManager.FindByNameAsync(model.Email);
                if (user == null)
                {
                    return View(model);                    
                }

                string smtpHost = ConfigurationManager.AppSettings["smtpHost"];
                int smtpPort = Convert.ToInt32(ConfigurationManager.AppSettings["smtpPort"]);
                bool smtpUseSSL = Convert.ToBoolean(ConfigurationManager.AppSettings["smtpUseSSL"]);
                string smtpUserName = ConfigurationManager.AppSettings["smtpUserName"];
                string smtpPassword = ConfigurationManager.AppSettings["smtpPassword"];

                // get change password page

                MimeMessage message = new MimeMessage();

                string address = "no-reply@lnu.edu.ua";

                MailboxAddress from = new MailboxAddress("LnuReports", address);
                message.From.Add(from);

                MailboxAddress to = new MailboxAddress("User", user.Email);
                message.To.Add(to);

                message.Subject = "Restore password";

                BodyBuilder bodyBuilder = new BodyBuilder();


                //var provider = new DpapiDataProtectionProvider("SampleAppName");

                //UserManager.UserTokenProvider = new DataProtectorTokenProvider<ApplicationUser>(
                //    provider.Create("SampleTokenName"));

                string hashedGuId = await UserManager.GeneratePasswordResetTokenAsync(user.Id);

                hashedGuId = hashedGuId.Crypt();

                string recoveryLink = Request.Url.ToString()
                    .Replace("ForgotPassword", $"ResetPassword?code=" + hashedGuId );

                // generate body
                //bodyBuilder.HtmlBody = body;
                bodyBuilder.HtmlBody = $"<a href='{recoveryLink}'>Click here to reset Your password</a>";

                message.Body = bodyBuilder.ToMessageBody();

                SmtpClient client = new SmtpClient();

                client.Connect(smtpHost, smtpPort, smtpUseSSL);

                client.Authenticate(smtpUserName, smtpPassword);

                client.Send(message);
                client.Disconnect(true);
                client.Dispose();
            }
            return View("ForgotPasswordConfirmation");
        }

        //
        // GET: /Account/ForgotPasswordConfirmation
        [AllowAnonymous]
        public ActionResult ForgotPasswordConfirmation()
        {
            return View();
        }

        //
        // GET: /Account/ResetPassword
        [AllowAnonymous]
        public ActionResult ResetPassword(string code)
        {
            return code == null ? View("Error") : View();
        }

        //
        // POST: /Account/ResetPassword
        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = await UserManager.FindByNameAsync(model.Email);
            if (user == null)
            {
                return RedirectToAction("ResetPasswordConfirmation", "Account");
            }            

            var result = await UserManager.ResetPasswordAsync(user.Id, model.Code.Decrypt(), model.Password);


            if (result.Succeeded)
            {
                return RedirectToAction("ResetPasswordConfirmation", "Account");
            }
            AddErrors(result);
            return View();
        }

        //
        // GET: /Account/ResetPasswordConfirmation
        [AllowAnonymous]
        public ActionResult ResetPasswordConfirmation()
        {
            return View();
        }

        //
        // POST: /Account/ExternalLogin
        [HttpPost]
        [AllowAnonymous]
        public ActionResult ExternalLogin(string provider, string returnUrl)
        {
            // Request a redirect to the external login provider
            return new ChallengeResult(provider, Url.Action("ExternalLoginCallback", "Account", new { ReturnUrl = returnUrl }));
        }

        //
        // POST: /Account/LogOff
        [HttpPost]
        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("Index", "Home");
        }
        #region Helpers
        // Used for XSRF protection when adding external logins
        private const string XsrfKey = "XsrfId";

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }

        internal class ChallengeResult : HttpUnauthorizedResult
        {
            public ChallengeResult(string provider, string redirectUri)
                : this(provider, redirectUri, null)
            {
            }

            public ChallengeResult(string provider, string redirectUri, string userId)
            {
                LoginProvider = provider;
                RedirectUri = redirectUri;
                UserId = userId;
            }

            public string LoginProvider { get; set; }
            public string RedirectUri { get; set; }
            public string UserId { get; set; }

            public override void ExecuteResult(ControllerContext context)
            {
                var properties = new AuthenticationProperties { RedirectUri = RedirectUri };
                if (UserId != null)
                {
                    properties.Dictionary[XsrfKey] = UserId;
                }
                context.HttpContext.GetOwinContext().Authentication.Challenge(properties, LoginProvider);
            }
        }
        #endregion

        protected override void Dispose(bool disposing)
        {
            if (disposing && _userManager != null)
            {
                _userManager.Dispose();
                _userManager = null;
                db.Dispose();
            }

            base.Dispose(disposing);
        }
    }
}