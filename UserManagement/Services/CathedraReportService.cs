 using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UserManagement.Models;
using UserManagement.Models.db;
using UserManagement.Models.PublicationDB;
using UserManagement.Models.Reports;

namespace UserManagement.Services
{
    public class CathedraReportService
    {
        private ApplicationDbContext db;
        private PublicationService publicationService = new PublicationService();

        private static String REPORT_HEADER = "{REPORT_HEADER}";
        private static String PUNKT_1 = "{PUNKT_1}";
        private static String PUNKT_2 = "{PUNKT_2}";
        private static String PUNKT_2_DESCRIPTION = "{PUNKT_2_DESCRIPTION}";
        private static String PUNKT_2_CV = "{PUNKT_2_CV}";
        private static String PUNKT_2_DEFENSES = "{PUNKT_2_DEFENSES}";
        private static String PUNKT_2_PUBLICATIONS = "{PUNKT_2_PUBLICATIONS}";
        private static String PUNKT_2_PATENTS = "{PUNKT_2_PATENTS}";
        private static String PUNKT_2_OTHER = "{PUNKT_2_OTHER}";

        private static String PUNKT_3 = "{PUNKT_3}";
        private static String PUNKT_3_DESCRIPTION = "{PUNKT_3_DESCRIPTION}";
        private static String PUNKT_3_CV = "{PUNKT_3_CV}";
        private static String PUNKT_3_DEFENSES = "{PUNKT_3_DEFENSES}";
        private static String PUNKT_3_PUBLICATIONS = "{PUNKT_3_PUBLICATIONS}";
        private static String PUNKT_3_PATENTS = "{PUNKT_3_PATENTS}";
        private static String PUNKT_3_OTHER = "{PUNKT_3_OTHER}";

        private static String PUNKT_4 = "{PUNKT_4}";
        private static String PUNKT_4_DESCRIPTION = "{PUNKT_4_DESCRIPTION}";
        private static String PUNKT_4_CV = "{PUNKT_4_CV}";
        private static String PUNKT_4_DEFENSES = "{PUNKT_4_DEFENSES}";
        private static String PUNKT_4_PUBLICATIONS = "{PUNKT_4_PUBLICATIONS}";
        private static String PUNKT_4_PATENTS = "{PUNKT_4_PATENTS}";
        private static String PUNKT_4_OTHER = "{PUNKT_4_OTHER}";

        private static String PUNKT_5 = "{PUNKT_5}";
        private static String PUNKT_6 = "{PUNKT_6}";
        private static String PUNKT_6_1 = "{PUNKT_6_1}";
        private static String PUNKT_6_2 = "{PUNKT_6_2}";
        private static String PUNKT_8 = "{PUNKT_8}";
        private static String PUNKT_9 = "{PUNKT_9}";
        private static String PUNKT_9_MONOGRAPHY = "{PUNKT_9_MONOGRAPHY}";
        private static String PUNKT_9_MONOGRAPHY_TABLE = "{PUNKT_9_MONOGRAPHY_TABLE}";
        private static String PUNKT_9_BOOK = "{PUNKT_9_BOOK}";
        private static String PUNKT_9_BOOK_TABLE = "{PUNKT_9_BOOK_TABLE}";
        private static String PUNKT_9_TRAINING_BOOK = "{PUNKT_9_TRAINING_BOOK}";
        private static String PUNKT_9_TRAINING_BOOK_TABLE = "{PUNKT_9_TRAINING_BOOK_TABLE}";
        private static String PUNKT_9_OTHER = "{PUNKT_9_OTHER}";
        private static String PUNKT_9_OTHER_TABLE = "{PUNKT_9_OTHER_TABLE}";
        private static String PUNKT_9_ARTICLES_HEADER = "{PUNKT_9_ARTICLES_HEADER}";
        private static String PUNKT_9_ARTICLES_INTERNATIONAL = "{PUNKT_9_ARTICLES_INTERNATIONAL}";
        private static String PUNKT_9_ARTICLES_NATIONAL_FAH = "{PUNKT_9_ARTICLES_NATIONAL_FAH}";
        private static String PUNKT_9_ARTICLES_NATIONAL = "{PUNKT_9_ARTICLES_NATIONAL}";
        private static String PUNKT_9_CONFERENCES_HEADER = "{PUNKT_9_CONFERENCES_HEADER}";
        private static String PUNKT_9_CONFERENCES_INTERNATIONAL = "{PUNKT_9_CONFERENCES_INTERNATIONAL}";
        private static String PUNKT_9_CONFERENCES_NATIONAL = "{PUNKT_9_CONFERENCES_NATIONAL}";
        private static String PUNKT_10 = "{PUNKT_10}";
        private static String PUNKT_11 = "{PUNKT_11}";
        private static String PUNKT_11_1 = "{PUNKT_11_1}";
        private static String PUNKT_11_2 = "{PUNKT_11_2}";
        private static String PUNKT_12 = "{PUNKT_12}";
        private static String PUNKT_13 = "{PUNKT_13}";
        private static String FOOTER = "{FOOTER}";


        private static String PUBLICATION_ROW = "{PUBLICATION_ROW}";
        private static String PUBLICATION_ROW_NAME = "{PUBLICATION_ROW_NAME}";
        private static String PUBLICATION_ROW_PAGES = "{PUBLICATION_ROW_PAGES}";


        private static String YEAR_CONST = "{YEAR}";
        private static String FACULTY_CONST = "{FACULTY_CONST}";
        private static String CATHEDRA_CONST = "{CATHEDRA}";
        private static String PROTOCOL_CONST = "{PROTOCOL_CONST}";
        private static String DATE_CONST = "{DATE_CONST}";
        private static String GENERIC_TEXT_CONST = "{GENERIC_TEXT}";

        public CathedraReportService(ApplicationDbContext db)
        {
            this.db = db;
        }
        public String GenerateHTMLReport(int reportId)
        {
            return GenerateHTMLReport(db.CathedraReport.Find(reportId));
        }

        public String GenerateHTMLReport(CathedraReport report)
        {
            var template = GenerateHtmlTemplateWithoutBody();
            var body = GenerateBodyForTemplate(report);
            return template.Replace("{BODY}", body);
        }

        private String GenerateBodyForTemplate(CathedraReport report)
        {
            String body = "<body><div class=\"body\">"
                + REPORT_HEADER
                + PUNKT_1
                + PUNKT_2
                + PUNKT_2_DESCRIPTION
                + PUNKT_2_CV
                + PUNKT_2_DEFENSES
                + PUNKT_2_PUBLICATIONS
                + PUNKT_2_PATENTS
                + PUNKT_2_OTHER
                + PUNKT_3
                + PUNKT_3_DESCRIPTION
                + PUNKT_3_CV
                + PUNKT_3_DEFENSES
                + PUNKT_3_PUBLICATIONS
                + PUNKT_3_PATENTS
                + PUNKT_3_OTHER
                + PUNKT_4
                + PUNKT_4_DESCRIPTION
                + PUNKT_4_CV
                + PUNKT_4_DEFENSES
                + PUNKT_4_PUBLICATIONS
                + PUNKT_4_PATENTS
                + PUNKT_4_OTHER
                + PUNKT_5
                + PUNKT_6
                + PUNKT_6_1
                + PUNKT_6_2
                + PUNKT_8
                + PUNKT_9
                + PUNKT_9_MONOGRAPHY
                + PUNKT_9_MONOGRAPHY_TABLE
                + PUNKT_9_BOOK
                + PUNKT_9_BOOK_TABLE
                + PUNKT_9_TRAINING_BOOK
                + PUNKT_9_TRAINING_BOOK_TABLE
                + PUNKT_9_OTHER
                + PUNKT_9_OTHER_TABLE
                + PUNKT_9_ARTICLES_HEADER
                + PUNKT_9_ARTICLES_INTERNATIONAL
                + PUNKT_9_ARTICLES_NATIONAL_FAH
                + PUNKT_9_ARTICLES_NATIONAL
                + PUNKT_9_CONFERENCES_HEADER
                + PUNKT_9_CONFERENCES_INTERNATIONAL
                + PUNKT_9_CONFERENCES_NATIONAL
                + PUNKT_10
                + PUNKT_11
                + PUNKT_11_1
                + PUNKT_11_2
                + PUNKT_12
                + PUNKT_13
                + FOOTER
                + "</div></body>";
            var mainHeader = GenerateTemplateForHeadOfReport();
            var punktOne = GetPunktOne(report);
            var punktTwo = GetPunktThemeGeneric(report.BudgetTheme, GetTitleForPunktTwo());
            var punktTwoDescr = GetPunktThemeGenericField(report.AllDescriptionBudgetTheme, report.BudgetTheme, GetTitleForPunktTwoDescription());
            var punktTwoCV = GetPunktThemeGenericField(report.CVBudgetTheme, report.BudgetTheme, GetTitleForPunktBudgetCV());
            var punktTwoDefenses = GetPunktThemeGenericField(report.DefensesOfCoworkersBudgetTheme, report.BudgetTheme, GetTitleForPunktBudgetDefenses());
            var punktTwoPublications = GetPunktThemeGenericPublications(report.BudgetTheme, report.PrintedPublicationBudgetTheme, GetTitleForPunktBudgetPublications());
            var punktTwoPatents = GetPunktThemeGenericField(report.ApplicationAndPatentsOnInventionBudgetTheme, report.BudgetTheme, GetTitleForPunktBudgetPatents());
            var punktTwoOther = GetPunktThemeGenericField(report.OtherBudgetTheme, report.BudgetTheme, GetTitleForPunktBudgetOther());

            var punktTree = GetPunktThemeGeneric(report.ThemeInWorkTime, GetTitleForPunktTree());
            var punktTreeDescr = GetPunktThemeGenericField(report.AllDescriptionThemeInWorkTime, report.ThemeInWorkTime, GetTitleForPunktTreeDescription());
            var punktTreeCV = GetPunktThemeGenericField(report.CVThemeInWorkTime, report.ThemeInWorkTime, GetTitleForPunktInWorkCV());
            var punktTreeDefenses = GetPunktThemeGenericField(report.DefensesOfCoworkersThemeInWorkTime, report.ThemeInWorkTime, GetTitleForPunktInWorkDefenses());
            var punktTreePublications = GetPunktThemeGenericPublications(report.ThemeInWorkTime, report.PrintedPublicationThemeInWorkTime, GetTitleForPunktInWorkPublications());
            var punktTreePatents = GetPunktThemeGenericField(report.ApplicationAndPatentsOnInventionThemeInWorkTime, report.ThemeInWorkTime, GetTitleForPunktInWorkPatents());
            var punktTreeOther = GetPunktThemeGenericField(report.OtherThemeInWorkTime, report.ThemeInWorkTime, GetTitleForPunktInWorkOther());

            var punktFour = GetPunktThemeGeneric(report.HospDohovirTheme, GetTitleForPunktFour());
            var punktFourDescr = GetPunktThemeGenericField(report.AllDescriptionHospDohovirTheme, report.HospDohovirTheme, GetTitleForPunktFourDescription());
            var punktFourCV = GetPunktThemeGenericField(report.CVHospDohovirTheme, report.HospDohovirTheme, GetTitleForPunktHospDohovirCV());
            var punktFourDefenses = GetPunktThemeGenericField(report.DefensesOfCoworkersHospDohovirTheme, report.HospDohovirTheme, GetTitleForPunktHospDohovirDefenses());
            var punktFourPublications = GetPunktThemeGenericPublications(report.HospDohovirTheme, report.PrintedPublicationHospDohovirTheme, GetTitleForPunktHospDohovirPublications());
            var punktFourPatents = GetPunktThemeGenericField(report.ApplicationAndPatentsOnInventionHospDohovirTheme, report.HospDohovirTheme, GetTitleForPunktHospDohovirPatents());
            var punktFourOther = GetPunktThemeGenericField(report.OtherHospDohovirTheme, report.HospDohovirTheme, GetTitleForPunktHospDohovirOther());

            var punktFive = GetPunktFive(report);
            var punktSix = GetPunktSix(report);
            var punktSixOne = GetPunktSixOne(report);
            var punktSixTwo = GetPunktSixTwo(report);
            var punktEight = GetPunktEight(report);
            var punktNine = GetPunktNine(report);
            var distinctPublications = report.PrintedPublicationBudgetTheme
                .Union(report.PrintedPublicationHospDohovirTheme)
                .Union(report.PrintedPublicationThemeInWorkTime)
                .GroupBy(x => x.ID)
                .Select(x => x.First())
                .ToList();

            var punktNineMonography = GetPublicationGenericPunktNine("Монографії", PublicationType.Монографія, distinctPublications);
            var punktNineMonographyTable = GetPublicationTableGeneric(PublicationType.Монографія, distinctPublications);
            var punktNineBook = GetPublicationGenericPunktNine("Підручники", PublicationType.Підручник, distinctPublications);
            var punktNineBookTable = GetPublicationTableGeneric(PublicationType.Підручник, distinctPublications);
            var punktNineTrainingBook = GetPublicationGenericPunktNine("Навчальні посібники", PublicationType.Навчальний_Посібник, distinctPublications);
            var punktNineTrainingBookTable = GetPublicationTableGeneric(PublicationType.Навчальний_Посібник, distinctPublications);
            var punktNineOther = GetPublicationGenericPunktNine("Інші наукові видання", PublicationType.Інше_Наукове_Видання, distinctPublications);
            var punktNineOtherTable = GetPublicationTableGeneric(PublicationType.Інше_Наукове_Видання, distinctPublications);

            var punktNineArticlesHeader = GetArticlesHeader(distinctPublications);
            var punktNineArticlesInternational = GetPublicationArticlesOrConferencesTemplate(GetTitleForArticlesInternational(), 
                PublicationType.Стаття_В_Закордонних_Виданнях, distinctPublications);
            var punktNineArticlesNational = GetPublicationArticlesOrConferencesTemplate(GetTitleForArticlesNational(),
                PublicationType.Стаття_В_Інших_Виданнях_України, distinctPublications);
            var punktNineArticlesNationalFah = GetPublicationArticlesOrConferencesTemplate(GetTitleForArticlesNationalFah(),
                PublicationType.Стаття_В_Фахових_Виданнях_України, distinctPublications);
            var punktNineConferencesHeader = GetConferencesHeader(distinctPublications);
            var punktNineConferencesInternational = GetPublicationArticlesOrConferencesTemplate(GetTitleForConferencesInternational(),
                PublicationType.Тези_Доповіді_На_Міжнародній_Конференції, distinctPublications);
            var punktNineConferencesNational= GetPublicationArticlesOrConferencesTemplate(GetTitleForConferencesNational(),
                PublicationType.Тези_Доповіді_На_Вітчизняній_Конференції, distinctPublications);

            var punktTen = GetPunktTen(report);
            var punktEleven = GetPunktEleven(report);
            var punktElevenOne = GetPunktElevenOne(report);
            var punktElevenTwo = GetPunktElevenTwo(report);
            var punktTwelve = GetPunktTwelve(report);
            var punktThirteen = GetPunktThirteen(report);
            var footer = GetFooter(report);


            var formetedBody = ReplaceStringWithParameters(body, new Dictionary<string, string>()
            {
                [REPORT_HEADER] = GetHeaderOfReport(report),
                [PUNKT_1] = punktOne,
                [PUNKT_2] = punktTwo,
                [PUNKT_2_DESCRIPTION] = punktTwoDescr,
                [PUNKT_2_CV] = punktTwoCV,
                [PUNKT_2_DEFENSES] = punktTwoDefenses,
                [PUNKT_2_PUBLICATIONS] = punktTwoPublications,
                [PUNKT_2_PATENTS] = punktTwoPatents,
                [PUNKT_2_OTHER] = punktTwoOther,
                [PUNKT_3] = punktTree,
                [PUNKT_3_DESCRIPTION] = punktTreeDescr,
                [PUNKT_3_CV] = punktTreeCV,
                [PUNKT_3_DEFENSES] = punktTreeDefenses,
                [PUNKT_3_PUBLICATIONS] = punktTreePublications,
                [PUNKT_3_PATENTS] = punktTreePatents,
                [PUNKT_3_OTHER] = punktTreeOther,
                [PUNKT_4] = punktFour,
                [PUNKT_4_DESCRIPTION] = punktFourDescr,
                [PUNKT_4_CV] = punktFourCV,
                [PUNKT_4_DEFENSES] = punktFourDefenses,
                [PUNKT_4_PUBLICATIONS] = punktFourPublications,
                [PUNKT_4_PATENTS] = punktFourPatents,
                [PUNKT_4_OTHER] = punktFourOther,
                [PUNKT_5] = punktFive,
                [PUNKT_6] = punktSix,
                [PUNKT_6_1] = punktSixOne,
                [PUNKT_6_2] = punktSixTwo,
                [PUNKT_8] = punktEight,
                [PUNKT_9] = punktNine,
                [PUNKT_9_MONOGRAPHY] = punktNineMonography,
                [PUNKT_9_MONOGRAPHY_TABLE] = punktNineMonographyTable,
                [PUNKT_9_BOOK] = punktNineBook,
                [PUNKT_9_BOOK_TABLE] = punktNineBookTable,
                [PUNKT_9_TRAINING_BOOK] = punktNineTrainingBook,
                [PUNKT_9_TRAINING_BOOK_TABLE] = punktNineTrainingBookTable,
                [PUNKT_9_OTHER] = punktNineOther,
                [PUNKT_9_OTHER_TABLE] = punktNineOtherTable,
                [PUNKT_9_ARTICLES_HEADER] = punktNineArticlesHeader,
                [PUNKT_9_ARTICLES_INTERNATIONAL] = punktNineArticlesInternational,
                [PUNKT_9_ARTICLES_NATIONAL_FAH] = punktNineArticlesNationalFah,
                [PUNKT_9_ARTICLES_NATIONAL] = punktNineArticlesNational,
                [PUNKT_9_CONFERENCES_HEADER] = punktNineConferencesHeader,
                [PUNKT_9_CONFERENCES_INTERNATIONAL] = punktNineConferencesInternational, 
                [PUNKT_9_CONFERENCES_NATIONAL] = punktNineConferencesNational,
                [PUNKT_10] = punktTen,
                [PUNKT_11] = punktEleven,
                [PUNKT_11_1] = punktElevenOne,
                [PUNKT_11_2] = punktElevenTwo,
                [PUNKT_12] = punktTwelve,
                [PUNKT_13] = punktThirteen,
                [FOOTER] = footer,
            });
            return formetedBody;
        }

        private String GenerateTemplateForHeadOfReport()
        {
            return "<div class=\"header\"><h2>Звіт про наукову роботу кафедри "
                + CATHEDRA_CONST
                + " у "
                + YEAR_CONST
                + "</h2></div>";
        }

        private String GenerateHtmlTemplateWithoutBody()
        {
            return "<!DOCTYPE html><html><head><meta charset = \"utf-8\"/><title>Звіт</title><style>p, h2 {margin: 0;}.body {line-height: 23px;padding: 5mm 10mm;margin: auto;}.header {text-align: center;}.block {margin-top: 25px;}.input-text {margin-left: 34px;}table, th, td {border: 1px solid black;border-collapse: collapse;}th, td {padding: 7px;}.table-report {margin: auto;margin-top:10px;}.footer-text{margin-top:10px;}</style></head> {BODY}</html>";
        }

        private string GenerateTemplateForGenericPunkt(String title)
        {
            return "<div class=\"block\"><p>&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp"
                + title
                + "</p><p class=\"input-text\">"
                + GENERIC_TEXT_CONST
                + "</p></div>";
        }

        private string GetHeaderOfReport(CathedraReport report)
        {
            return ReplaceStringWithParameters(GenerateTemplateForHeadOfReport(), new Dictionary<string, string>()
            {
                [YEAR_CONST] = report.Date.Value.Year.ToString(),
                [CATHEDRA_CONST] = report.User.Cathedra.Name.Replace("Кафедра ", ""),
            });
        }

        private string GetPunktOne(CathedraReport report)
        {
            if ((report.AchivementSchool == null || report.AchivementSchool == ""))
            {
                return "";
            }
            return ReplaceStringWithParameters(GenerateTemplateForGenericPunkt(GetTitleForPunktOne()), new Dictionary<string, string>()
            {
                [GENERIC_TEXT_CONST] = report.AchivementSchool
            });
        }

        private string GetPunktTen(CathedraReport report)
        {
            if ((report.ConferencesInUniversity == null || report.ConferencesInUniversity == ""))
            {
                return "";
            }
            return ReplaceStringWithParameters(GenerateTemplateForGenericPunkt(GetTitleForPunktTen()), new Dictionary<string, string>()
            {
                [GENERIC_TEXT_CONST] = report.ConferencesInUniversity
            });
        }

        private string GetPunktThirteen(CathedraReport report)
        {
            if ((report.PropositionForNewForms == null || report.PropositionForNewForms == ""))
            {
                return "";
            }
            return ReplaceStringWithParameters(GenerateTemplateForGenericPunkt(GetTitleForPunktThirteen()), new Dictionary<string, string>()
            {
                [GENERIC_TEXT_CONST] = report.PropositionForNewForms
            });
        }

        private string GetPunktTwelve(CathedraReport report)
        {
            if ((report.Materials == null || report.Materials == ""))
            {
                return "";
            }
            return ReplaceStringWithParameters(GenerateTemplateForGenericPunkt(GetTitleForPunktTwelve()), new Dictionary<string, string>()
            {
                [GENERIC_TEXT_CONST] = report.Materials
            });
        }

        private string GetPunktElevenTwo(CathedraReport report)
        {
            if ((report.Patents == null || report.Patents == ""))
            {
                return "";
            }
            return ReplaceStringWithParameters(GenerateTemplateForGenericPunkt(GetTitleForPunktElevenTwo()), new Dictionary<string, string>()
            {
                [GENERIC_TEXT_CONST] = report.Patents
            });
        }

        private string GetPunktElevenOne(CathedraReport report)
        {
            if ((report.ApplicationOnInvention == null || report.ApplicationOnInvention == ""))
            {
                return "";
            }
            return ReplaceStringWithParameters(GenerateTemplateForGenericPunkt(GetTitleForPunktElevenOne()), new Dictionary<string, string>()
            {
                [GENERIC_TEXT_CONST] = report.ApplicationOnInvention
            });
        }
        private string GetPunktEleven(CathedraReport report)
        {
            if ((report.ApplicationOnInvention == null || report.ApplicationOnInvention == "") && 
                (report.Patents == null || report.Patents == ""))
            {
                return "";
            }
            return GenerateTemplateForGenericPunktHeader(GetTitleForPunktEleven());
        }
        private string GetArticlesHeader(List<Publication> publications)
        {
            if (publications == null || !publications.Any(x => x.PublicationType == PublicationType.Стаття_В_Інших_Виданнях_України ||
                                                                x.PublicationType == PublicationType.Стаття_В_Закордонних_Виданнях ||
                                                                x.PublicationType == PublicationType.Стаття_В_Фахових_Виданнях_України))
            {
                return "";
            }

            return GetTemplateHeaderBlock("Статті");
        }
        private string GetConferencesHeader(List<Publication> publications)
        {
            if (publications == null || !publications.Any(x => x.PublicationType == PublicationType.Тези_Доповіді_На_Вітчизняній_Конференції ||
                                                                x.PublicationType == PublicationType.Тези_Доповіді_На_Міжнародній_Конференції ))
            {
                return "";
            }

            return GetTemplateHeaderBlock("Тези доповідей на конференціях");
        }

        private string GetPunktThemeGeneric(ThemeOfScientificWork theme, String title)
        {
            if ((theme == null))
            {
                return "";
            }

            return ReplaceStringWithParameters(GenerateTemplateForGenericPunkt(title), new Dictionary<string, string>()
            {
                [GENERIC_TEXT_CONST] = theme.Value + ". "
                + theme.ThemeNumber + "; "
                + theme.ScientificHead + " "
                + theme.PeriodFrom.Year.ToString()
                + " - " + theme.PeriodTo.Year.ToString()
            });
        }
        private string GetPunktThemeGenericField(String field, ThemeOfScientificWork theme, String title)
        {
            if ((theme == null || field == null || field == ""))
            {
                return "";
            }

            return ReplaceStringWithParameters(GenerateTemplateForGenericPunkt(title), new Dictionary<string, string>()
            {
                [GENERIC_TEXT_CONST] = field,
            });
        }
        private string GetPunktThemeGenericPublications(ThemeOfScientificWork theme, List<Publication> publications, String title)
        {
            if ((theme == null
                || publications == null || publications.Count == 0))
            {
                return "";
            }
            return ReplaceStringWithParameters(GenerateTemplateForGenericPunkt(title), new Dictionary<string, string>()
            {
                [GENERIC_TEXT_CONST] = GetPunktBudgetPublicationsTemplate(publications),
            });
        }
        private string GetPublicationGenericPunktNine(String title, PublicationType type, List<Publication> publications)
        {
            if (publications == null || publications.Where(x => x.PublicationType == type).Count() == 0)
            {
                return "";
            }
            var list = publications.Where(x => x.PublicationType == type).ToList();
            var size = list.Sum(x => x.SizeOfPages);
            return GetTemplatePublicationByType(title, list.Count.ToString(), size.ToString(), title.ToLower());
        }

        private string GetPublicationArticlesOrConferencesTemplate(String title, PublicationType type, List<Publication> publications)
        {
            if (publications == null || publications.Where(x => x.PublicationType == type).Count() == 0)
            {
                return "";
            }
            var toReturn = "";
            var list = publications.Where(x => x.PublicationType == type).ToList();
            int index = 1;
            foreach (var item in publications)
            {
                if (item.PublicationType == type)
                {
                    toReturn += index + ". " + publicationService.GenerateNameOfPublication(item) + "\n\r";
                    index++;
                }
            }
            return ReplaceStringWithParameters(GenerateTemplateForGenericPunkt(title), new Dictionary<string, string>()
            {
                [GENERIC_TEXT_CONST] = toReturn,
            });
        }

        private string GetPublicationTableGeneric(PublicationType type, List<Publication> publications)
        {
            if (publications == null || publications.Where(x => x.PublicationType == type).Count() == 0)
            {
                return "";
            }
            var publicationRows = "";
            foreach (var item in publications)
            {
                if (item.PublicationType == type)
                {
                    publicationRows += ReplaceStringWithParameters(GetTemplatePublicationRow(), new Dictionary<string, string>()
                    {
                        [PUBLICATION_ROW_NAME] = publicationService.GenerateNameOfPublication(item),
                        [PUBLICATION_ROW_PAGES] = item.SizeOfPages.ToString(),
                    });
                }
            }
            return ReplaceStringWithParameters(GetTemplatePublicationTable(), new Dictionary<string, string>()
            {
                [PUBLICATION_ROW] = publicationRows,
            });
        }

        private string GetTemplateHeaderBlock(String title)
        {
            return "<div class=\"block\"><p class=\"header\"><b>"
                + title
                + "</b></p></div>";
        }

        private string GetTemplatePublicationByType(String title, String countPublications, String size, String type)
        {
            return "<div class=\"block\"><p class=\"header\"><b>"
                + title
                + "</b></p><p>&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbspВидано "
                + countPublications
                + " " + type
                + " загальним обсягом "
                + size
                + ".</p></div>";
        }

        private string GetTemplatePublicationTable()
        {
            return "<table class=\"table-report\"><tr><th>Бібліографічний опис</th><th>Обсяг, друк. арк.</th></tr>"
                + PUBLICATION_ROW
                + "</table>";
        }

        private string GetTemplatePublicationRow()
        {
            return "<tr><td>"
                    + PUBLICATION_ROW_NAME
                    + "</td><td>"
                    + PUBLICATION_ROW_PAGES
                    + "</td></tr>";
        }


        private string GetPunktEight(CathedraReport report)
        {
            if ((report.StudentsWorks == null
                || report.StudentsWorks == ""))
            {
                return "";
            }
            return ReplaceStringWithParameters(GenerateTemplateForGenericPunkt(GetTitleForPunktEight()), new Dictionary<string, string>()
            {
                [GENERIC_TEXT_CONST] = report.StudentsWorks,
            });
        }

        private string GetPunktNine(CathedraReport report)
        {
            if ((report.PrintedPublicationBudgetTheme == null
                || report.PrintedPublicationBudgetTheme.Count == 0) &&
                (report.PrintedPublicationHospDohovirTheme == null
                || report.PrintedPublicationHospDohovirTheme.Count == 0) &&
                (report.PrintedPublicationThemeInWorkTime == null
                || report.PrintedPublicationThemeInWorkTime.Count == 0))
            {
                return "";
            }
            return GenerateTemplateForGenericPunktHeader(GetTitleForPunktNine());
        }

        private string GetPunktSix(CathedraReport report)
        {
            if ((report.CooperationWithAcadamyOfScience == null
                || report.CooperationWithAcadamyOfScience == "") &&
                (report.CooperationWithForeignScientificInstitution == null
                || report.CooperationWithForeignScientificInstitution == ""))
            {
                return "";
            }
            return GenerateTemplateForGenericPunktHeader(GetTitleForPunktSix());
        }

        private string GetPunktSixOne(CathedraReport report)
        {
            if ((report.CooperationWithAcadamyOfScience == null
                || report.CooperationWithAcadamyOfScience == ""))
            {
                return "";
            }
            return ReplaceStringWithParameters(GenerateTemplateForGenericPunkt(GetTitleForPunktSixOne()), new Dictionary<string, string>()
            {
                [GENERIC_TEXT_CONST] = report.CooperationWithAcadamyOfScience,
            });
        }

        private string GetPunktSixTwo(CathedraReport report)
        {
            if ((report.CooperationWithForeignScientificInstitution == null
                || report.CooperationWithForeignScientificInstitution == ""))
            {
                return "";
            }
            return ReplaceStringWithParameters(GenerateTemplateForGenericPunkt(GetTitleForPunktSixTwo()), new Dictionary<string, string>()
            {
                [GENERIC_TEXT_CONST] = report.CooperationWithForeignScientificInstitution,
            });
        }

        private string GetPunktFive(CathedraReport report)
        {
            if ((report.OtherFormsOfScientificWork == null
                || report.OtherFormsOfScientificWork == ""))
            {
                return "";
            }
            return ReplaceStringWithParameters(GenerateTemplateForGenericPunkt(GetTitleForPunktFive()), new Dictionary<string, string>()
            {
                [GENERIC_TEXT_CONST] = report.OtherFormsOfScientificWork,
            });
        }

        private string GetPunktBudgetPublicationsTemplate(List<Publication> report)
        {
            String publicationText = "Всього по темі опубліковано ";
            var publications = report.GroupBy(x => x.PublicationType).ToDictionary(k => k.Key, x => x.Count());
            for (var i = 0; i < publications.Count; i++)
            {
                publicationText += publications.ElementAt(i).Value + " " + publications.ElementAt(i).Key.ToString().Replace("_", " ").ToLower();
                if (i == publications.Count - 1)
                {
                    publicationText += ". ";
                }
                else
                {
                    publicationText += ", ";
                }
            }
            return publicationText;
        }

        private string GetFooter(CathedraReport report)
        {
            return ReplaceStringWithParameters(GetFooterTemplate(), new Dictionary<string, string>()
            {
                [PROTOCOL_CONST] = report.Protocol != null ? report.Protocol : "",
                [DATE_CONST] = report.Date != null ? report.Date.Value.ToString("dd.MM.yyyy") : "",
                [FACULTY_CONST] = report.User.Cathedra.Faculty.Name.Replace("Факультет ", "").ToLower(),
            });
        }

        private string GenerateTemplateForGenericPunktHeader(String title)
        {
            return "<div class=\"block\"><p>&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp"
                + title
                + "</p></div>";
        }
        private string GetTitleForPunktNine()
        {
            return @"9. Публікації: (бібліографічний опис згідно з державним стандартом).";
        }
        private string GetTitleForPunktThirteen()
        {
            return @"13. Пропозиції щодо нових форм організації наукової роботи в ринкових умовах:";
        }

        private string GetTitleForPunktTwelve()
        {
            return @"12. Матеріальна база підрозділу (обладнання, придбане за звітний період чи введене в дію на кінець звітного року):";
        }
        private string GetTitleForPunktTen()
        {
            return @"10. Конференції: стислий звіт про проведення конференцій на базі університету (0,5 с. про кожну конференцію):";
        }

        private string GetTitleForPunktEleven()
        {
            return @"11. Патентно-ліцензійна діяльність:";
        }
        private string GetTitleForPunktElevenOne()
        {
            return @"11.1 Заявки на винахід (корисну модель) (на видачу патенту на винахід (корисну модель)) −
                автори, назва, № заявки, дата подачі, заявник(и):";
        }

        private string GetTitleForPunktElevenTwo()
        {
            return @"11.2 Патенти на винахід (корисну модель) − автори, назва, № патенту, дата видачі, заявник(и):";
        }
        private string GetTitleForPunktEight()
        {
            return @"8. Студентська наукова робота: кількість наукових гуртків і кількість студентів, 
                що беруть участь у їхній роботі; участь (кількість студентів) у 
                виконанні держбюджетної чи іншої наукової тематики; проведені студентські
                наукові конференції на базі університету; виступи на конференціях
                (кількість доповідей за участю студентів, назви конференцій); індивідуальні 
                та спільні зі співробітниками університету публікації; отримані нагороди
                у II етапі Всеукраїнських студентських Олімпіад, міжнародних Олімпіадах,
                Всеукраїнських конкурсах студентських наукових робіт, турнірах, чемпіонатах тощо:";
        }
        
        private string GetTitleForArticlesInternational()
        {
            return @"Статті в інших закордонних виданнях:";
        }

        private string GetTitleForArticlesNational()
        {
            return @"Статті в інших виданнях України:";
        }

        private string GetTitleForConferencesInternational()
        {
            return @"Тези доповідей на міжнародних конференціях:";
        }

        private string GetTitleForConferencesNational()
        {
            return @"Тези доповідей на вітчизняних конференціях:";
        }

        private string GetTitleForArticlesNationalFah()
        {
            return @"Статті у фахових виданнях України:";
        }
        private string GetTitleForPunktSixOne()
        {
            return @"6.1. Співпраця з науковими установами НАН України 
                та галузевих академій наук України(наукові стажування, кількість спільних публікацій, спільні наукові заходи):";
        }
        private string GetTitleForPunktSixTwo()
        {
            return @"6.2. Співпраця із закордонними науковими установами та фірмами (наукові стажування, 
                ґранти(додаток 3), контракти, кількість спільних публікацій, спільні наукові заходи, запрошення закордонних науковців):";
        }

        private string GetTitleForPunktSix()
        {
            return "6. Зовнішні зв’язки:";
        }
        private string GetTitleForPunktFive()
        {
            return "5. Інші форми наукової діяльності (робота спеціалізованих вчених, експертних рад, рецензування та опонування дисертацій тощо):";
        }
        private string GetTitleForPunktOne()
        {
            return "1. Досягнення провідних наукових шкіл за звітний рік (до 1 с.):";
        }
        private string GetTitleForPunktTwo()
        {
            return @"2. Держбюджетні теми: шифр, назва, науковий керівник (науковий ступінь, вчене звання), 
                № держреєстрації, термін виконання, кількість штатних
                виконавців і сумісників із зазначенням їхніх посад, наукових ступенів, вчених звань:";
        }
        private string GetTitleForPunktTwoDescription()
        {
            return @"Узагальнені результати виконання теми (за весь час дії теми (для завершених) та за звітний рік зокрема):";
        }
        private string GetTitleForPunktBudgetCV()
        {
            return @"2.1. Резюме (0,3 с.):";
        }
        private string GetTitleForPunktBudgetDefenses()
        {
            return @"2.2. Захищені дисертації співробітниками, докторантами та аспірантами (назва, ПІБ):";
        }
        private string GetTitleForPunktBudgetPublications()
        {
            return @"2.3 Опубліковані монографії, підручники,
                навчальні посібники, словники, переклади наукових праць, кількість статей, тез доповідей на конференціях:";
        }
        private string GetTitleForPunktBudgetPatents()
        {
            return "2.4 Подані заявки, отримані патенти на винахід (корисну модель):";
        }
        private string GetTitleForPunktBudgetOther()
        {
            return "2.5 Інше (макети приладів, нові методики, технології, експериментальні зразки матеріалів, рекламна діяльність тощо):";
        }

        private string GetTitleForPunktTree()
        {
            return @"3. Теми, які виконуються в межах робочого часу викладачів: назва, 
                науковий керівник (науковий ступінь, вчене звання), № держреєстрації, термін виконання:";
        }
        private string GetTitleForPunktTreeDescription()
        {
            return @"Узагальнені результати виконання теми (за весь час дії теми (для завершених) та за звітний рік зокрема):";
        }
        private string GetTitleForPunktInWorkCV()
        {
            return @"3.1. Резюме (0,3 с.):";
        }
        private string GetTitleForPunktInWorkDefenses()
        {
            return @"3.2. Захищені дисертації співробітниками, докторантами та аспірантами (назва, ПІБ):";
        }
        private string GetTitleForPunktInWorkPublications()
        {
            return @"3.3 Опубліковані монографії, підручники,
                навчальні посібники, словники, переклади наукових праць, кількість статей, тез доповідей на конференціях:";
        }
        private string GetTitleForPunktInWorkPatents()
        {
            return "3.4 Подані заявки, отримані патенти на винахід (корисну модель):";
        }
        private string GetTitleForPunktInWorkOther()
        {
            return "3.5 Інше (макети приладів, нові методики, технології, експериментальні зразки матеріалів, рекламна діяльність тощо):";
        }

        private string GetTitleForPunktFour()
        {
            return @"4. Госпдоговірна тематика (шифр, назва, науковий керівник, термін виконання, 
                кількість штатних виконавців і сумісників із зазначенням їхніх посад, наукового ступеня, вченого звання):";
        }
        private string GetTitleForPunktFourDescription()
        {
            return @"Узагальнені результати виконання теми (за весь час дії теми (для завершених) та за звітний рік зокрема):";
        }
        private string GetTitleForPunktHospDohovirCV()
        {
            return @"4.1. Резюме (0,3 с.):";
        }
        private string GetTitleForPunktHospDohovirDefenses()
        {
            return @"4.2. Захищені дисертації співробітниками, докторантами та аспірантами (назва, ПІБ):";
        }
        private string GetTitleForPunktHospDohovirPublications()
        {
            return @"4.3 Опубліковані монографії, підручники,
                навчальні посібники, словники, переклади наукових праць, кількість статей, тез доповідей на конференціях:";
        }
        private string GetTitleForPunktHospDohovirPatents()
        {
            return "4.4 Подані заявки, отримані патенти на винахід (корисну модель):";
        }
        private string GetTitleForPunktHospDohovirOther()
        {
            return "4.5 Інше (макети приладів, нові методики, технології, експериментальні зразки матеріалів, рекламна діяльність тощо):";
        }


        private string GetSignture()
        {
            return "<div class=\"block input-text\"><p>Підпис_______________</p></div>";
        }

        private string GetFooterTemplate()
        {
            return "<div class=\"block input-text\"><p>Звіт заслухано і затверджено на Вченій (науково-технічній) раді факультету</p><p class=\"footer-text\">"
                + FACULTY_CONST
                + ", протокол №: "
                + PROTOCOL_CONST
                + " від: "
                + DATE_CONST
                + "</p><p class=\"footer-text\">Декан факультету "
                + FACULTY_CONST
                + "__________________</p></div>";
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