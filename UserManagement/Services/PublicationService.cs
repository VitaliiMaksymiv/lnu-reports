using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UserManagement.Models.db;
using UserManagement.Models.PublicationDB;

namespace UserManagement.Services
{
    public class PublicationService
    {
        private static String GENERIC_TEXT_CONST = "{GENERIC_TEXT}";

        public String GenerateNameOfPublication(Publication publication)
        {
            string toReturn = "";

            toReturn += publication.MainAuthor;
            toReturn += publication.Name + " / ";
            for (var i = 0; i < publication.User.Count; i++)
            {
                var initials = publication.User.ElementAt(i).I18nUserInitials.Where(x => x.Language == publication.Language).First();
                toReturn = toReturn + initials.FirstName.Substring(0, 1).ToUpper()
                    + ". " + initials.FathersName.Substring(0, 1).ToUpper()
                    + ". " + initials.LastName;
                if (i != publication.User.Count - 1)
                    toReturn = toReturn + ", ";
            }

            toReturn = toReturn + ", " +
                ((publication.OtherAuthors != null || publication.OtherAuthors != "") ? publication.OtherAuthors : "") +
                " // " +
                (publication.Magazine != null ? publication.Magazine + ", " : "") +
                (publication.Edition != null ? publication.Edition + ", " : "") +
                (publication.Place != null ? publication.Place : "") +
                ". – " +
                publication.Date.Year + ".";

            toReturn += AddEndOfPublication(publication);

            return toReturn;
        }
        private String AddEndOfPublication(Publication publication)
        {
            string toReturn = "";
            toReturn = toReturn + " – " + (publication.Tome == null ? "" : (publication.Tome + ", "));
            if(publication.PublicationType == PublicationType.Монографія
                        || publication.PublicationType == PublicationType.Підручник
                        || publication.PublicationType == PublicationType.Навчальний_Посібник)
            {
                toReturn = toReturn + publication.Pages;
                if (publication.Language == Language.UA)
                    toReturn = toReturn + " c";
                if (publication.Language == Language.EN)
                    toReturn = toReturn + " p";
            }
            else
            {
                if (publication.Language == Language.UA)
                    toReturn = toReturn + "C. ";
                if (publication.Language == Language.EN)
                    toReturn = toReturn + "P. ";
                toReturn = toReturn + publication.Pages;
            }
            toReturn = toReturn + ".";
            return toReturn;
        }

        public string GenerateNameOfPublications(List<Publication> publications)
        {
            string toReturn = "";
            for (var i = 0; i < publications.Count; i++)
            {
                toReturn = toReturn + (i + 1) + ". " + GenerateNameOfPublication(publications[i]);
                if (i != publications.Count - 1)
                {
                    toReturn = toReturn + "<br/>";
                }
            }
            return toReturn;
        }

        public string GetPunktSixOne(Report report)
        {
            var count = report.PrintedPublication.Count();
            return count == 0 ? "" : GenerateTemplateForGenericPunktHeader(GetTitleForPunktSixOne());
        }
        public string GetPunktSixOneMono(Report report)
        {
            var collection = report.PrintedPublication.Where(x => x.PublicationType == PublicationType.Монографія).ToList();
            return PopulatePunkt(collection, GenerateTemplateForGenericPunkt(GetTitleForPunktSixOneMono()));
        }
        public string GetPunktSixOneBook(Report report)
        {
            var collection = report.PrintedPublication.Where(x => x.PublicationType == PublicationType.Підручник).ToList();
            return PopulatePunkt(collection, GenerateTemplateForGenericPunkt(GetTitleForPunktSixOneBook()));
        }
        public string GetPunktSixOneTrainingBook(Report report)
        {
            var collection = report.PrintedPublication.Where(x => x.PublicationType == PublicationType.Навчальний_Посібник).ToList();
            return PopulatePunkt(collection, GenerateTemplateForGenericPunkt(GetTitleForPunktSixOneTrainingBook()));
        }
        public string GetPunktSixOneArticlesFactor(Report report)
        {
            var collection = report.PrintedPublication.Where(x => x.PublicationType == PublicationType.Стаття_В_Виданнях_які_мають_імпакт_фактор).ToList();
            return PopulatePunkt(collection, GenerateTemplateForGenericPunkt(GetTitleForPunktSixOneArticlesFactor()));
        }
        public string GetPunktSixOneOther(Report report)
        {
            var collection = report.PrintedPublication.Where(x => x.PublicationType == PublicationType.Інше_Наукове_Видання).ToList();
            return PopulatePunkt(collection, GenerateTemplateForGenericPunkt(GetTitleForPunktSixOneOther()));
        }
        public string GetPunktSixOneArticlesOtherInterantional(Report report)
        {
            var collection = report.PrintedPublication.Where(x => x.PublicationType == PublicationType.Стаття_В_Інших_Закордонних_Виданнях).ToList();
            return PopulatePunkt(collection, GenerateTemplateForGenericPunkt(GetTitleForPunktSixOneArticlesInternationalOther()));
        }
        public string GetPunktSixOneArticlesInterantionalMetricals(Report report)
        {
            var collection = report.PrintedPublication.Where(x => x.PublicationType == PublicationType.Стаття_В_Інших_Виданнях_які_включені_до_міжнародних_наукометричних_баз_даних).ToList();
            return PopulatePunkt(collection, GenerateTemplateForGenericPunkt(GetTitleForPunktSixOneArticlesInternational()));
        }
        public string GetPunktSixOneArticlesNationalFah(Report report)
        {
            var collection = report.PrintedPublication.Where(x => x.PublicationType == PublicationType.Стаття_В_Фахових_Виданнях_України).ToList();
            return PopulatePunkt(collection, GenerateTemplateForGenericPunkt(GetTitleForPunktSixOneArticlesNationalFah()));
        }
        public string GetPunktSixOneArticlesNational(Report report)
        {
            var collection = report.PrintedPublication.Where(x => x.PublicationType == PublicationType.Стаття_В_Інших_Виданнях_України).ToList();
            return PopulatePunkt(collection, GenerateTemplateForGenericPunkt(GetTitleForPunktSixOneArticlesNational()));
        }
        public string GetPunktSixOneConferences(Report report)
        {
            var count = report.PrintedPublication
                .Where(x => x.PublicationType == PublicationType.Тези_Доповіді_На_Вітчизняній_Конференції
                         || x.PublicationType == PublicationType.Тези_Доповіді_На_Міжнародній_Конференції).Count();
            return count == 0 ? "" : GenerateTemplateForGenericPunktHeader(GetTitleForPunktSixOneConferences());
        }
        public string GetPunktSixOneInternationalConferences(Report report)
        {
            var collection = report.PrintedPublication.Where(x => x.PublicationType == PublicationType.Тези_Доповіді_На_Міжнародній_Конференції).ToList();
            return PopulatePunkt(collection, GenerateTemplateForGenericPunkt(GetTitleForPunktSixOneConferencesInternational()));
        }
        public string GetPunktSixOneNationalConferences(Report report)
        {
            var collection = report.PrintedPublication.Where(x => x.PublicationType == PublicationType.Тези_Доповіді_На_Вітчизняній_Конференції).ToList();
            return PopulatePunkt(collection, GenerateTemplateForGenericPunkt(GetTitleForPunktSixOneConferencesNational()));
        }
        public string GetPunktSixTwo(Report report)
        {
            var count = report.RecomendedPublication.Count();
            return count == 0 ? "" : GenerateTemplateForGenericPunktHeader(GetTitleForPunktSixTwo());
        }
        public string GetPunktSixTwoMono(Report report)
        {
            var collection = report.RecomendedPublication.Where(x => x.PublicationType == PublicationType.Монографія).ToList();
            return PopulatePunkt(collection, GenerateTemplateForGenericPunkt(GetTitleForPunktSixTwoMono()));
        }
        public string GetPunktSixTwoBook(Report report)
        {
            var collection = report.RecomendedPublication.Where(x => x.PublicationType == PublicationType.Підручник).ToList();
            return PopulatePunkt(collection, GenerateTemplateForGenericPunkt(GetTitleForPunktSixTwoBook()));
        }
        public string GetPunktSixTwoTrainingBook(Report report)
        {
            var collection = report.RecomendedPublication.Where(x => x.PublicationType == PublicationType.Навчальний_Посібник).ToList();
            return PopulatePunkt(collection, GenerateTemplateForGenericPunkt(GetTitleForPunktSixTwoTrainingBook()));
        }
        public string GetPunktSixTwoOther(Report report)
        {
            var collection = report.RecomendedPublication.Where(x => x.PublicationType == PublicationType.Інше_Наукове_Видання).ToList();
            return PopulatePunkt(collection, GenerateTemplateForGenericPunkt(GetTitleForPunktSixTwoOther()));
        }
        public string GetPunktSixTwoFive(Report report)
        {
            var collection = report.AcceptedToPrintPublication.ToList();
            return PopulatePunkt(collection, GenerateTemplateForGenericPunkt(GetTitleForPunktSixTwoFive()));
        }
        private string PopulatePunkt(List<Publication> publications, string title)
        {
            return publications.Count == 0 ? "" : ReplaceStringWithParameters(title, new Dictionary<string, string>()
            {
                [GENERIC_TEXT_CONST] = GenerateNameOfPublications(publications),
            });
        }

        public string GetPunktSixOneArticles(Report report)
        {
            var count = report.PrintedPublication.Where(x => x.PublicationType == PublicationType.Стаття
            || x.PublicationType == PublicationType.Стаття_В_Інших_Виданнях_України
            || x.PublicationType == PublicationType.Стаття_В_Закордонних_Виданнях
            || x.PublicationType == PublicationType.Стаття_В_Фахових_Виданнях_України
            || x.PublicationType == PublicationType.Стаття_В_Інших_Закордонних_Виданнях
            || x.PublicationType == PublicationType.Стаття_В_Виданнях_які_мають_імпакт_фактор).Count();
            return count == 0 ? "" : GenerateTemplateForGenericPunktHeader(GetTitleForPunktSixOneArticles());
        }

        private string GetTitleForPunktSixOne()
        {
            return "6.1 Праці, що вийшли з друку (бібліографічний опис згідно з державним стандартом):";
        }
        private string GetTitleForPunktSixTwo()
        {
            return @"6.2.Праці, рекомендовані Вченою радою університету до друку(автори, назва праці, обсяг, дата
               подачі):";
        }
        private string GetTitleForPunktSixOneMono()
        {
            return "6.1.1 Монографії:";
        }
        private string GetTitleForPunktSixOneBook()
        {
            return "6.1.2 Підручники:";
        }
        private string GetTitleForPunktSixOneTrainingBook()
        {
            return "6.1.3 Навчальні посібники:";
        }
        private string GetTitleForPunktSixOneOther()
        {
            return @"6.1.4  Інші наукові видання (словники, переклади наукових праць, науковий коментар,
                бібліографічний покажчик тощо):";
        }
        private string GetTitleForPunktSixTwoMono()
        {
            return "6.2.1 Монографії:";
        }
        private string GetTitleForPunktSixTwoBook()
        {
            return "6.2.2 Підручники:";
        }
        private string GetTitleForPunktSixTwoTrainingBook()
        {
            return "6.2.3 Навчальні посібники:";
        }
        private string GetTitleForPunktSixTwoOther()
        {
            return @"6.2.4  Інші наукові видання (словники, переклади наукових праць, науковий коментар,
                бібліографічний покажчик тощо):";
        }
        private string GetTitleForPunktSixTwoFive()
        {
            return "6.2.5 Статті, прийняті до друку:";
        }
        private string GetTitleForPunktSixOneArticles()
        {
            return "6.1.5 Статті:";
        }
        private string GetTitleForPunktSixOneArticlesFactor()
        {
            return "6.1.5.1  Статті у виданнях, які мають імпакт-фактор:";
        }
        private string GetTitleForPunktSixOneArticlesInternational()
        {
            return @"6.1.5.2  Статті в інших виданнях, які включені до міжнародних наукометричних баз даних Web of
                Science, Scopus та інших:";
        }
        private string GetTitleForPunktSixOneArticlesInternationalOther()
        {
            return "6.1.5.3 Статті в інших закордонних виданнях:";
        }
        private string GetTitleForPunktSixOneArticlesNationalFah()
        {
            return "6.1.5.4 Статті у фахових виданнях України:";
        }
        private string GetTitleForPunktSixOneArticlesNational()
        {
            return "6.1.5.5 Статті в інших виданнях України:";
        }
        private string GetTitleForPunktSixOneConferences()
        {
            return "6.1.6 Тези доповідей:";
        }
        private string GetTitleForPunktSixOneConferencesInternational()
        {
            return "6.1.6.1 Тези доповідей на міжнародних конференціях:";
        }
        private string GetTitleForPunktSixOneConferencesNational()
        {
            return "6.1.6.2 Тези доповідей на вітчизняних конференціях:";
        }

        private string GenerateTemplateForGenericPunkt(String title)
        {
            return " <div class=\"block\"><p>&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp"
                + title
                + "</p><p class=\"input-text\">"
                + GENERIC_TEXT_CONST
                + "</p></div>";
        }

        private string GenerateTemplateForGenericPunktHeader(String title)
        {
            return "<div class=\"block\"><p>&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp"
                + title
                + "</p></div>";
        }

        private String ReplaceStringWithParameters(String str, Dictionary<String, String> parameters)
        {
            string toReturn = str;
            foreach (var i in parameters)
            {
                toReturn = toReturn.Replace(i.Key, i.Value);
            }
            return toReturn;
        }
    }
}