using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace UserManagement.Models
{
    public class ForgotViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class LoginViewModel
    {
        [Required]
        [Display(Name = "Email")]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }

    public class RegisterViewModel
    {
        [Required]
        [RegularExpression("^[a-zA-Z0-9_.+-]+@(?:(?:[a-zA-Z0-9-]+\\.)?[a-zA-Z]+\\.)?lnu\\.edu\\.ua", ErrorMessage = "Invalid email. Should be ...@lnu.edu.ua")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "FirstName")]
        public string FirstName { get; set; }
        
        [Required]
        [Display(Name = "LastName")]
        public string LastName { get; set; }

        [Required]
        [Display(Name = "FathersName")]
        public string FathersName { get; set; }

        [Required]
        [Display(Name = "BirthDate")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime BirthDate { get; set; }

        [Required]
        [Display(Name = "GraduationDate")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy}")]
        public DateTime GraduationDate { get; set; }

        [Required]
        [Display(Name = "AwardingDate")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy}")]
        public DateTime AwardingDate { get; set; }

        [Required]
        [Display(Name = "DefenseYear")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy}")]
        public DateTime DefenseYear { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        [Required]
        [Display(Name = "Cathedra")]
        public string Cathedra { get; set; }
        [Required]
        [Display(Name = "AcademicStatus")]
        public string AcademicStatus { get; set; }
        [Required]
        [Display(Name = "ScienceDegree")]
        public string ScienceDegree { get; set; }
        [Required]
        [Display(Name = "Position")]
        public string Position { get; set; }
    }

    public class ResetPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }

    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    //public class ApplicationUserViewModel
    //{
    //    [Required]
    //    [EmailAddress]
    //    [RegularExpression("^[a-zA-Z0-9_.+-]+@(?:(?:[a-zA-Z0-9-]+\\.)?[a-zA-Z]+\\.)?lnu\\.edu\\.ua",ErrorMessage ="Invalid email. Should be ...@lnu.edu.ua")]
    //    [Display(Name = "Email")]
    //    public string Email { get; set; }

    //    [Required]
    //    [Display(Name = "FirstName")]
    //    public string FirstName { get; set; }

    //    [Required]
    //    [Display(Name = "LastName")]
    //    public string LastName { get; set; }

    //    [Required]
    //    [Display(Name = "FathersName")]
    //    public string FathersName { get; set; }
        
    //    [Required]
    //    [Display(Name = "IsActive")]
    //    public bool IsActive { get; set; }

    //    [Required]
    //    [Display(Name = "Roles")]
    //    public string[] Roles { get; set; }

    //    [Required]
    //    [Display(Name = "Role")]
    //    public string RoleToAdd { get; set; }
    //}
}
