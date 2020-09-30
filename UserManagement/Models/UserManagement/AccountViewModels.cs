using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using UserManagement.Models.db;

namespace UserManagement.Models
{
    public class ForgotViewModel
    {
        [Required]
        [Display(Name = "Електронна пошта")]
        public string Email { get; set; }
    }

    public class LoginViewModel
    {
        [Required]
        [Display(Name = "Електронна пошта")]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }

        [Display(Name = "Запам’ятати?")]
        public bool RememberMe { get; set; }
    }

    public class RegisterViewModel
    {
        [Required]
        [RegularExpression("^[a-zA-Z0-9_.+-]+@(?:(?:[a-zA-Z0-9-]+\\.)?[a-zA-Z]+\\.)?lnu\\.edu\\.ua", ErrorMessage = "Некоректна електронна пошта. Повинен бути: ...@lnu.edu.ua")]
        [EmailAddress(ErrorMessage = "Некоректна електронна пошта")]
        [Display(Name = "Електронна пошта")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "{0} повинен мати мінімум {2} символи.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Підтвердження паролю")]
        [Compare("Password", ErrorMessage = "Паролі не співпадають.")]
        public string ConfirmPassword { get; set; }

        [Required]
        [Display(Name = "Кафедра")]
        public string Cathedra { get; set; }

        [Required]
        [Display(Name = "Факультет")]
        public string Faculty { get; set; }
    }


    public class UpdateViewModel
    {
        [Required]
        [RegularExpression("^[a-zA-Z0-9_.+-]+@(?:(?:[a-zA-Z0-9-]+\\.)?[a-zA-Z]+\\.)?lnu\\.edu\\.ua", ErrorMessage = "Invalid email. Should be ...@lnu.edu.ua")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        [Display(Name = "Електронна пошта")]
        public string Email { get; set; }

        [Required]
        public ICollection<I18nUserInitials> I18nUserInitials { get; set; }

        [Required]
        [Display(Name = "Кількість публікацій до реєстрації")]
        public int PublicationsBeforeRegister { get; set; }

        [Required]
        [Display(Name = "Дата народження")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime BirthDate { get; set; }

        [Required]
        [Display(Name = "Рік закінчення ВНЗ")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy}")]
        public DateTime GraduationDate { get; set; }

        [Required]
        [Display(Name = "Рік присвоєння")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy}")]
        public DateTime AwardingDate { get; set; }

        [Required]
        [Display(Name = "Рік захисту")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy}")]
        public DateTime DefenseYear { get; set; }

        [Display(Name = "Рік початку перебування в аспірантурі")]
        public int? AspirantStartYear { get; set; }

        [Display(Name = "Рік закінчення перебування в аспірантурі")]
        public int? AspirantFinishYear { get; set; }

        [Display(Name = "Рік початку перебування в докторатурі")]
        public int? DoctorStartYear { get; set; }

        [Display(Name = "Рік закінчення перебування в докторантурі")]
        public int? DoctorFinishYear { get; set; }

        [Required]
        [Display(Name = "Науковий ступінь")]
        public string AcademicStatus { get; set; }
        [Required]
        [Display(Name = "Вчене звання")]
        public string ScienceDegree { get; set; }
        [Required]
        [Display(Name = "Посада")]
        public string Position { get; set; }
    }

    public class ResetPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Електронна пошта")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "{0} повинен мати мінімум {2} символи.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Підтвердження паролю")]
        [Compare("Password", ErrorMessage = "Паролі не співпадають.")]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }

    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Електронна пошта")]
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
