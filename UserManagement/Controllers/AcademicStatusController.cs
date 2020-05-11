using System.Data.Entity;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using UserManagement.Models;
using UserManagement.Models.db;

namespace UserManagement.Controllers
{
    [Authorize(Roles = "Superadmin")]
    public class AcademicStatusController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public async Task<ActionResult> Index()
        {
            return View(await db.AcademicStatus.ToListAsync());
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ID,Value")] AcademicStatus academicStatus)
        {
            if (ModelState.IsValid)
            {
                db.AcademicStatus.Add(academicStatus);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(academicStatus);
        }

        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AcademicStatus academicStatus = await db.AcademicStatus.FindAsync(id);
            if (academicStatus == null)
            {
                return HttpNotFound();
            }
            return View(academicStatus);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID,Value")] AcademicStatus academicStatus)
        {
            if (ModelState.IsValid)
            {
                db.Entry(academicStatus).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(academicStatus);
        }

        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AcademicStatus academicStatus = await db.AcademicStatus.FindAsync(id);
            if (academicStatus == null)
            {
                return HttpNotFound();
            }
            return View(academicStatus);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            AcademicStatus academicStatus = await db.AcademicStatus.FindAsync(id);
            db.AcademicStatus.Remove(academicStatus);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
