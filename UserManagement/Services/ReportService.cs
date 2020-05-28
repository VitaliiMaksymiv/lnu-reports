using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UserManagement.Models;
using UserManagement.Models.db;
using UserManagement.Models.PublicationDB;

namespace UserManagement.Services
{
    public class ReportService
    {
        private ApplicationDbContext db;
        private PublicationService publicationService = new PublicationService();

        private static String REPORT_HEADER = "{REPORT_HEADER}";
        private static String PUNKT_1 = "{PUNKT_1}";
        private static String PUNKT_2 = "{PUNKT_2}";
        private static String PUNKT_3 = "{PUNKT_3}";
        private static String PUNKT_4 = "{PUNKT_4}";
        private static String PUNKT_5 = "{PUNKT_5}";

        private static String PUNKT_6_TABLE = "{PUNKT_6_TABLE}";
        private static String PUNKT_6_1 = "{PUNKT_6_1}";
        private static String PUNKT_6_1_MONO = "{PUNKT_6_1_MONO}";
        private static String PUNKT_6_1_BOOK = "{PUNKT_6_1_BOOK}";
        private static String PUNKT_6_1_TRAINING = "{PUNKT_6_1_TRAINING}";
        private static String PUNKT_6_1_OTHER = "{PUNKT_6_1_OTHER}";
        private static String PUNKT_6_1_ARTICLES = "{PUNKT_6_1_ARTICLES}";
        private static String PUNKT_6_1_ARTICLES_FACTOR = "{PUNKT_6_1_ARTICLES_FACTOR}";
        //private static String PUNKT_6_1_ARTICLES_INTERNATIONAL = "{PUNKT_6_1_ARTICLES_INTERNATIONAL}";
        private static String PUNKT_6_1_ARTICLES_OTHER_INTERNATIONAL = "{PUNKT_6_1_ARTICLES_OTHER_INTERNATIONAL}";
        private static String PUNKT_6_1_ARTICLES_NATIONAL = "{PUNKT_6_1_ARTICLES_NATIONAL}";
        private static String PUNKT_6_1_ARTICLES_NATIONAL_FAH = "{PUNKT_6_1_ARTICLES_NATIONAL_FAH}";
        private static String PUNKT_6_1_ARTICLES_INTERNATIONAL_METRICALS = "{PUNKT_6_1_ARTICLES_INTERNATIONAL_METRICALS}";
        private static String PUNKT_6_1_CONFERENCES = "{PUNKT_6_1_CONFERENCES}";
        private static String PUNKT_6_1_CONFERENCES_INTERNATIONAL = "{PUNKT_6_1_CONFERENCES_INTERNATIONAL}";
        private static String PUNKT_6_1_CONFERENCES_NATIONAL = "{PUNKT_6_1_CONFERENCES_NATIONAL}";
        private static String PUNKT_6_2 = "{PUNKT_6_2}";
        private static String PUNKT_6_2_MONO = "{PUNKT_6_2_MONO}";
        private static String PUNKT_6_2_BOOK = "{PUNKT_6_2_BOOK}";
        private static String PUNKT_6_2_TRAINING = "{PUNKT_6_2_TRAINING}";
        private static String PUNKT_6_2_OTHER = "{PUNKT_6_2_OTHER}";
        private static String PUNKT_6_2_ARTICLES = "{PUNKT_6_2_ARTICLES}";

        private static String PUNKT_7 = "{PUNKT_7}";
        private static String PUNKT_7_1 = "{PUNKT_7_1}";
        private static String PUNKT_7_2 = "{PUNKT_7_2}";
        private static String PUNKT_8 = "{PUNKT_8}";
        private static String PUNKT_9 = "{PUNKT_9}";
        private static String PUNKT_10 = "{PUNKT_10}";
        private static String SIGNATURE = "{SIGNATURE}";
        private static String FOOTER = "{FOOTER}";

        private static String YEAR_CONST = "{YEAR}";
        private static String POSITION_CONST = "{POSITION}";
        private static String CATHEDRA_CONST = "{CATHEDRA}";
        private static String CATHEDRA_LEAD = "{CATHEDRA_LEAD}";
        private static String USER_NAME_CONST = "{USER_NAME}";
        private static String BIRTHDAY_CONST = "{BIRTHDAY}";
        private static String GRADUATION_YEAR_CONST = "{GRADUATION_YEAR}";
        private static String ACADEMIC_STATUS_YEAR_CONST = "{ACADEMIC_STATUS_YEAR}";
        private static String SCIENCE_DEGREE_YEAR_CONST = "{SCIENCE_DEGREE_YEAR}";
        private static String THEME_SCIENTIFIC_WORK_CONST = "{THEME_SCIENTIFIC_WORK}";
        private static String DESCR_SCIENTIFIC_WORK_CONST = "{DESCR_SCIENTIFIC_WORK_CONST}";
        private static String THEME_NUMBER_SCIENTIFIC_WORK_CONST = "{THEME_NUMBER_SCIENTIFIC_WORK_CONST}";
        private static String PERIOD_SCIENTIFIC_WORK_CONST = "{PERIOD_SCIENTIFIC_WORK_CONST}";
        private static String HEAD_SCIENTIFIC_WORK_CONST = "{HEAD_SCIENTIFIC_WORK_CONST}";
        private static String FINANCIAL = "{FINANCIAL}";
        private static String PROTOCOL_CONST = "{PROTOCOL_CONST}";
        private static String DATE_CONST = "{DATE_CONST}";
        private static String GENERIC_TEXT_CONST = "{GENERIC_TEXT}";

        //Punkt 6
        private static String MONOGRAPH_ALL_CONST = "{MONOGRAPH_ALL_CONST}";
        private static String MONOGRAPH_PERIOD_CONST = "{MONOGRAPH_PERIOD_CONST}";
        private static String BOOK_ALL_CONST = "{BOOK_ALL_CONST}";
        private static String BOOK_PERIOD_CONST = "{BOOK_PERIOD_CONST}";
        private static String TRAINING_BOOK_ALL_CONST = "{TRAINING_BOOK_ALL_CONST}";
        private static String TRAINING_BOOK_PERIOD_CONST = "{TRAINING_BOOK_PERIOD_CONST}";
        private static String ARTICLES_ALL_CONST = "{ARTICLES_ALL_CONST}";
        private static String ARTICLES_PERIOD_CONST = "{ARTICLES_PERIOD_CONST}";
        private static String OTHER_WRITINGS_ALL_CONST = "{OTHER_WRITINGS_ALL_CONST}";
        private static String OTHER_WRITINGS_PERIOD_CONST = "{OTHER_WRITINGS_PERIOD_CONST}";
        private static String CONFERENCES_ALL_CONST = "{CONFERENCES_ALL_CONST}";
        private static String CONFERENCES_PERIOD_CONST = "{CONFERENCES_PERIOD_CONST}";
        private static String PATENTS_ALL_CONST = "{PATENTS_ALL_CONST}";
        private static String PATENTS_PERIOD_CONST = "{PATENTS_PERIOD_CONST}";

        public ReportService(ApplicationDbContext db)
        {
            this.db = db;
        }
        public String GenerateHTMLReport(int reportId)
        {
            return GenerateHTMLReport(db.Reports.Find(reportId));
        }

        public String GenerateHTMLReport(Report report)
        {
            var template = GenerateHtmlTemplateWithoutBody();
            var body = GenerateBodyForTemplate(report);
            return template.Replace("{BODY}", body);
        }

        private String GenerateBodyForTemplate(Report report)
        {
            String body = "<body><div class=\"body\">"
                + REPORT_HEADER
                + PUNKT_1
                + PUNKT_2
                + PUNKT_3
                + PUNKT_4
                + PUNKT_5
                + PUNKT_6_TABLE
                + PUNKT_6_1
                + PUNKT_6_1_MONO
                + PUNKT_6_1_BOOK
                + PUNKT_6_1_TRAINING
                + PUNKT_6_1_OTHER
                + PUNKT_6_1_ARTICLES
                + PUNKT_6_1_ARTICLES_FACTOR
                //+ PUNKT_6_1_ARTICLES_INTERNATIONAL
                + PUNKT_6_1_ARTICLES_OTHER_INTERNATIONAL
                + PUNKT_6_1_ARTICLES_NATIONAL_FAH
                + PUNKT_6_1_ARTICLES_NATIONAL
                + PUNKT_6_1_CONFERENCES
                + PUNKT_6_1_CONFERENCES_INTERNATIONAL
                + PUNKT_6_1_CONFERENCES_NATIONAL
                + PUNKT_6_2
                + PUNKT_6_2_MONO
                + PUNKT_6_2_BOOK
                + PUNKT_6_2_TRAINING
                + PUNKT_6_2_OTHER
                + PUNKT_6_2_ARTICLES
                + PUNKT_7
                + PUNKT_7_1
                + PUNKT_7_2
                + PUNKT_8
                + PUNKT_9
                + PUNKT_10
                + SIGNATURE
                + FOOTER
                + "</div></body>";
            var readyReportHeader = GetHeaderOfReport(report);
            var readyPunktOne = GetPunktOne(report);
            var readyPunktTwo = GetPunktTwo(report);
            var readyPunktTree = GetPunktTree(report);
            var readyPunktFour = GetPunktFour(report);
            var readyPunktFive = GetPunktFive(report);
            var readyPunktSixTable = GetPunktSixTable(report);
            var readyPunktSixOne = publicationService.GetPunktSixOne(report);
            var readyPunktSixOneMono = publicationService.GetPunktSixOneMono(report);
            var readyPunktSixOneBook = publicationService.GetPunktSixOneBook(report);
            var readyPunktSixOneTrainingBook = publicationService.GetPunktSixOneTrainingBook(report);
            var readyPunktSixOneOther = publicationService.GetPunktSixOneOther(report);
            var readyPunktSixOneArticles = publicationService.GetPunktSixOneArticles(report);
            var readyPunktSixOneArticlesFactor = publicationService.GetPunktSixOneArticlesFactor(report);
            var readyPunktSixOneArticlesInternationals = publicationService.GetPunktSixOneArticlesOtherInterantional(report);
            var readyPunktSixOneArticlesInternationalsMetricals = publicationService.GetPunktSixOneArticlesInterantionalMetricals(report);
            var readyPunktSixOneArticlesNationalFah = publicationService.GetPunktSixOneArticlesNationalFah(report);
            var readyPunktSixOneArticlesNational = publicationService.GetPunktSixOneArticlesNational(report);
            var readyPunktSixOneConferences = publicationService.GetPunktSixOneConferences(report);
            var readyPunktSixOneConferencesInternational = publicationService.GetPunktSixOneInternationalConferences(report);
            var readyPunktSixOneConferencesNational = publicationService.GetPunktSixOneNationalConferences(report);
            var readyPunktSixTwo = publicationService.GetPunktSixTwo(report);
            var readyPunktSixTwoMono = publicationService.GetPunktSixTwoMono(report);
            var readyPunktSixTwoBook = publicationService.GetPunktSixTwoBook(report);
            var readyPunktSixTwoTrainingBook = publicationService.GetPunktSixTwoTrainingBook(report);
            var readyPunktSixTwoOther = publicationService.GetPunktSixTwoOther(report);
            var readyPunktSixTwoArticles = publicationService.GetPunktSixTwoFive(report);
            var readyPunktSeven = GetPunktSeven(report);
            var readyPunktSevenOne = GetPunktSevenOne(report);
            var readyPunktSevenTwo = GetPunktSevenTwo(report);
            var readyPunktEight = GetPunktEight(report);
            var readyPunktNine = GetPunktNine(report);
            var readyPunktTen = GetPunktTen(report);
            var readySignature = GetSignture();
            var readyFooter = GetFooter(report);

            var formetedBody = ReplaceStringWithParameters(body, new Dictionary<string, string>()
            {
                [REPORT_HEADER] = readyReportHeader,
                [PUNKT_1] = readyPunktOne,
                [PUNKT_2] = readyPunktTwo,
                [PUNKT_3] = readyPunktTree,
                [PUNKT_4] = readyPunktFour,
                [PUNKT_5] = readyPunktFive,
                [PUNKT_6_TABLE] = readyPunktSixTable,
                [PUNKT_6_1] = readyPunktSixOne,
                [PUNKT_6_1_MONO] = readyPunktSixOneMono,
                [PUNKT_6_1_BOOK] = readyPunktSixOneBook,
                [PUNKT_6_1_TRAINING] = readyPunktSixOneTrainingBook,
                [PUNKT_6_1_OTHER] = readyPunktSixOneOther,
                [PUNKT_6_1_ARTICLES] = readyPunktSixOneArticles,
                [PUNKT_6_1_ARTICLES_INTERNATIONAL_METRICALS] = readyPunktSixOneArticlesInternationalsMetricals,
                [PUNKT_6_1_ARTICLES_FACTOR] = readyPunktSixOneArticlesFactor,
                [PUNKT_6_1_ARTICLES_OTHER_INTERNATIONAL] = readyPunktSixOneArticlesInternationals,              
                [PUNKT_6_1_ARTICLES_NATIONAL_FAH] = readyPunktSixOneArticlesNationalFah,
                [PUNKT_6_1_ARTICLES_NATIONAL] = readyPunktSixOneArticlesNational,
                [PUNKT_6_1_CONFERENCES] = readyPunktSixOneConferences,
                [PUNKT_6_1_CONFERENCES_INTERNATIONAL] = readyPunktSixOneConferencesInternational,
                [PUNKT_6_1_CONFERENCES_NATIONAL] = readyPunktSixOneConferencesNational,
                [PUNKT_6_2] = readyPunktSixTwo,
                [PUNKT_6_2_MONO] = readyPunktSixTwoMono,
                [PUNKT_6_2_BOOK] = readyPunktSixTwoBook,
                [PUNKT_6_2_TRAINING] = readyPunktSixTwoTrainingBook,
                [PUNKT_6_2_OTHER] = readyPunktSixTwoOther,
                [PUNKT_6_2_ARTICLES] = readyPunktSixTwoArticles,
                [PUNKT_7] = readyPunktSeven,
                [PUNKT_7_1] = readyPunktSevenOne,
                [PUNKT_7_2] = readyPunktSevenTwo,
                [PUNKT_8] = readyPunktEight,
                [PUNKT_9] = readyPunktNine,
                [PUNKT_10] = readyPunktTen,
                [SIGNATURE] = readySignature,
                [FOOTER] = readyFooter
            });
            return formetedBody;
        }

        private String GenerateTemplateForHeadOfReport()
        {
            return "<div class=\"header\"><h2> Індивідуальний звіт про наукову роботу в "
                + YEAR_CONST
                + " році</h2><p><i>"
                + POSITION_CONST
                + " кафедри "
                + CATHEDRA_CONST + " "
                + USER_NAME_CONST
                + "</i></p></div><div class=\"input-text\"><p><b>Відомості про вченого:</b></p><p>Рік народження: "
                  + BIRTHDAY_CONST
                + "</p><p> Рік закінчення ВНЗ: "
                + GRADUATION_YEAR_CONST
                + "</p><p> Науковий ступінь, рік захисту: "
                + ACADEMIC_STATUS_YEAR_CONST
                + "</p><p> Вчене звання, рік присвоєння: "
                + SCIENCE_DEGREE_YEAR_CONST
                + "</p></div> ";
        }

        private String GenerateHtmlTemplateWithoutBody()
        {
            return "<!DOCTYPE html><html><head><meta charset = \"utf-8\"/><title>Звіт</title><style>p, h2 {margin: 0;}.body {line-height: 23px;padding: 5mm 10mm;margin: auto;}.header {text-align: center;}.block {margin-top: 25px;}.input-text {margin-left: 34px;}table, th, td {border: 1px solid black;border-collapse: collapse;}th, td {padding: 7px;}.table-report {margin: auto;}.footer-text{margin-top:10px;}</style></head> {BODY}</html>";
        }

        private string GenerateTemplateForPunktOne()
        {
            return "<div class=\"block\"><p>&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp1.Участь у науково-дослідній тематиці підрозділу − шифр теми, категорія (держбюджетна,госпдоговірна, в межах робочого часу), назва, стисло зміст виконаної роботи(до семи рядків).</p><p class=\"input-text\"><i>"
                +  FINANCIAL + " " + THEME_SCIENTIFIC_WORK_CONST + ". " //
                + THEME_NUMBER_SCIENTIFIC_WORK_CONST 
                + "; " + HEAD_SCIENTIFIC_WORK_CONST + " "
                + PERIOD_SCIENTIFIC_WORK_CONST
                + "</i></p><p class=\"input-text block\">"
                + DESCR_SCIENTIFIC_WORK_CONST
                + "</p></div>";
        }
        private string GenerateTemplateForGenericPunkt(String title)
        {
            return "<div class=\"block\"><p>&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp"
                + title +
                "</p><p class=\"input-text\">"
                + GENERIC_TEXT_CONST
                + "</p></div>";
        }

        private string GenerateTemplateForPunktSixTable()
        {
            return "<div class=\"block\"><p>&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp6.</p><table class=\"table-report\"><tr><th>Загальна кількість наукових публікацій</th><th>Разом</th><th>За звітний період</th></tr><tr><td>Монографії</td><td>"
                + MONOGRAPH_ALL_CONST
                + "</td><td>"
                + MONOGRAPH_PERIOD_CONST
                + "</td></tr><tr><td>Підручників</td><td>"
                + BOOK_ALL_CONST
                + "</td><td>"
                + BOOK_PERIOD_CONST
                + "</td></tr><tr><td>Навчальні посібники</td><td>"
                + TRAINING_BOOK_ALL_CONST
                + "</td><td>"
                + TRAINING_BOOK_PERIOD_CONST
                + "</td></tr><tr><td>Статті</td><td>"
                + ARTICLES_ALL_CONST
                + "</td><td>"
                + ARTICLES_PERIOD_CONST
                + "</td></tr><tr><td>Інші наукові видання</td><td>"
                + OTHER_WRITINGS_ALL_CONST
                + "</td><td>"
                + OTHER_WRITINGS_PERIOD_CONST
                + "</td></tr><tr><td>Тези доповідей на конференціях</td><td>"
                + CONFERENCES_ALL_CONST
                + "</td><td>"
                + CONFERENCES_PERIOD_CONST
                + "</td></tr><tr><td>Патенти</td><td>"
                + PATENTS_ALL_CONST
                + "</td><td>"
                + PATENTS_PERIOD_CONST
                + "</table></div>";
        }

        private string GetHeaderOfReport(Report report)
        {
            var initials = report.User.I18nUserInitials.Where(x => x.Language == Language.UA).First();
            return ReplaceStringWithParameters(GenerateTemplateForHeadOfReport(), new Dictionary<string, string>()
            {
                [YEAR_CONST] = report.Date == null ? "" : report.Date.Value.Year.ToString(),
                [POSITION_CONST] = report.User.Position == null ? "" : report.User.Position.Value.Replace("кафедри",string.Empty),
                [CATHEDRA_CONST] = report.User.Cathedra == null ? "" : report.User.Cathedra.Name.Replace("Кафедра ", ""),
                [USER_NAME_CONST] = initials.LastName + " " + initials.FirstName + " " + initials.FathersName,
                [BIRTHDAY_CONST] = report.User.BirthDate.Year.ToString(),
                [GRADUATION_YEAR_CONST] = report.User.GraduationDate.Year.ToString(),
                [ACADEMIC_STATUS_YEAR_CONST] = report.User.AcademicStatus == null ? "" : report.User.AcademicStatus.Value == "Без ступеня" ? report.User.AcademicStatus.Value : report.User.AcademicStatus.Value + ", " + report.User.DefenseYear.Year.ToString(),
                [SCIENCE_DEGREE_YEAR_CONST] = report.User.ScienceDegree == null ? "" : report.User.ScienceDegree.Value == "Без звання" ? report.User.ScienceDegree.Value : report.User.ScienceDegree.Value + ", " + report.User.AwardingDate.Year.ToString(),
            });
        }
        private string GetPunktOne(Report report)
        {
            if (report.ThemeOfScientificWork == null)
            {
                return "";
            }
            return ReplaceStringWithParameters(GenerateTemplateForPunktOne(), new Dictionary<string, string>()
            {
                [THEME_SCIENTIFIC_WORK_CONST] = report.ThemeOfScientificWork.Value,
                [THEME_NUMBER_SCIENTIFIC_WORK_CONST] = report.ThemeOfScientificWork.ThemeNumber,
                [PERIOD_SCIENTIFIC_WORK_CONST] = report.ThemeOfScientificWork.PeriodFrom.Year.ToString() + " - " + report.ThemeOfScientificWork.PeriodTo.Year.ToString(),
                [DESCR_SCIENTIFIC_WORK_CONST] = report.ThemeOfScientificWorkDescription,
                [HEAD_SCIENTIFIC_WORK_CONST] = report.ThemeOfScientificWork.ScientificHead,
                [FINANCIAL] = report.ThemeOfScientificWork.Financial.ToString().ToLower(),
            });
        }
        private string GetPunktTwo(Report report)
        {
            if (report.ParticipationInGrands == null || report.ParticipationInGrands == "")
            {
                return "";
            }
            return ReplaceStringWithParameters(GenerateTemplateForGenericPunkt(GetTitleForPunktTwo()), new Dictionary<string, string>()
            {
                [GENERIC_TEXT_CONST] = report.ParticipationInGrands,
            });
        }
        private string GetPunktTree(Report report)
        {
            if (report.ScientificTrainings == null || report.ParticipationInGrands == "")
            {
                return "";
            }
            return ReplaceStringWithParameters(GenerateTemplateForGenericPunkt(GetTitleForPunktTree()), new Dictionary<string, string>()
            {
                [GENERIC_TEXT_CONST] = report.ScientificTrainings,
            });
        }
        private string GetPunktFour(Report report)
        {
            if (report.ScientificControlDoctorsWork == null || report.ScientificControlDoctorsWork == "")
            {
                return "";
            }
            return ReplaceStringWithParameters(GenerateTemplateForGenericPunkt(GetTitleForPunktFour()), new Dictionary<string, string>()
            {
                [GENERIC_TEXT_CONST] = report.ScientificControlDoctorsWork,
            });
        }
        private string GetPunktFive(Report report)
        {
            if (report.ScientificControlStudentsWork == null || report.ScientificControlStudentsWork == "")
            {
                return "";
            }
            return ReplaceStringWithParameters(GenerateTemplateForGenericPunkt(GetTitleForPunktFive()), new Dictionary<string, string>()
            {
                [GENERIC_TEXT_CONST] = report.ScientificControlStudentsWork,
            });
        }
        private string GetPunktSixTable(Report report)
        {
            var allPulications = db.Publication.Where(x => x.User.Any(y => y.Id == report.User.Id)).ToList();
            var dictionary = allPulications.GroupBy(x => x.PublicationType).ToDictionary(x => x.Key, x => x.Count());
            var allPublicationInReport = report.PrintedPublication.Union(report.RecomendedPublication).Union(report.AcceptedToPrintPublication);
            var dictionaryInReport = allPublicationInReport.GroupBy(x => x.PublicationType).ToDictionary(x => x.Key, x => x.Count());
            return ReplaceStringWithParameters(GenerateTemplateForPunktSixTable(), new Dictionary<string, string>()
            {
                [MONOGRAPH_ALL_CONST] = dictionary.ContainsKey(PublicationType.Монографія) ? dictionary[PublicationType.Монографія].ToString() : "0",
                [MONOGRAPH_PERIOD_CONST] = dictionaryInReport.ContainsKey(PublicationType.Монографія) ? dictionaryInReport[PublicationType.Монографія].ToString() : "0",
                [BOOK_ALL_CONST] = dictionary.ContainsKey(PublicationType.Підручник) ? dictionary[PublicationType.Підручник].ToString() : "0",
                [BOOK_PERIOD_CONST] = dictionaryInReport.ContainsKey(PublicationType.Підручник) ? dictionaryInReport[PublicationType.Підручник].ToString() : "0",
                [TRAINING_BOOK_ALL_CONST] = dictionary.ContainsKey(PublicationType.Навчальний_Посібник) ? dictionary[PublicationType.Навчальний_Посібник].ToString() : "0",
                [TRAINING_BOOK_PERIOD_CONST] = dictionaryInReport.ContainsKey(PublicationType.Навчальний_Посібник) ? dictionaryInReport[PublicationType.Навчальний_Посібник].ToString() : "0",
                [ARTICLES_ALL_CONST] = ((dictionary.ContainsKey(PublicationType.Стаття) ? dictionary[PublicationType.Стаття] : 0)
                                        + (dictionary.ContainsKey(PublicationType.Стаття_В_Інших_Виданнях_України) ? dictionary[PublicationType.Стаття_В_Інших_Виданнях_України] : 0)
                                        + (dictionary.ContainsKey(PublicationType.Стаття_В_Інших_Закордонних_Виданнях) ? dictionary[PublicationType.Стаття_В_Інших_Закордонних_Виданнях] : 0)
                                        + (dictionary.ContainsKey(PublicationType.Стаття_В_Виданнях_які_мають_імпакт_фактор) ? dictionary[PublicationType.Стаття_В_Виданнях_які_мають_імпакт_фактор] : 0)
                                        + (dictionary.ContainsKey(PublicationType.Стаття_В_Фахових_Виданнях_України) ? dictionary[PublicationType.Стаття_В_Фахових_Виданнях_України] : 0)).ToString(),
                [ARTICLES_PERIOD_CONST] = ((dictionaryInReport.ContainsKey(PublicationType.Стаття) ? dictionaryInReport[PublicationType.Стаття] : 0)
                                        + (dictionaryInReport.ContainsKey(PublicationType.Стаття_В_Інших_Виданнях_України) ? dictionaryInReport[PublicationType.Стаття_В_Інших_Виданнях_України] : 0)
                                        + (dictionaryInReport.ContainsKey(PublicationType.Стаття_В_Інших_Закордонних_Виданнях) ? dictionaryInReport[PublicationType.Стаття_В_Інших_Закордонних_Виданнях] : 0)
                                        + (dictionaryInReport.ContainsKey(PublicationType.Стаття_В_Виданнях_які_мають_імпакт_фактор) ? dictionaryInReport[PublicationType.Стаття_В_Виданнях_які_мають_імпакт_фактор] : 0)
                                        + (dictionaryInReport.ContainsKey(PublicationType.Стаття_В_Фахових_Виданнях_України) ? dictionaryInReport[PublicationType.Стаття_В_Фахових_Виданнях_України] : 0)).ToString(),
                [OTHER_WRITINGS_ALL_CONST] = dictionary.ContainsKey(PublicationType.Інше_Наукове_Видання) ? dictionary[PublicationType.Інше_Наукове_Видання].ToString() : "0",
                [OTHER_WRITINGS_PERIOD_CONST] = dictionaryInReport.ContainsKey(PublicationType.Інше_Наукове_Видання) ? dictionaryInReport[PublicationType.Інше_Наукове_Видання].ToString() : "0",
                [CONFERENCES_ALL_CONST] = ((dictionary.ContainsKey(PublicationType.Тези_Доповіді_На_Вітчизняній_Конференції) ? dictionary[PublicationType.Тези_Доповіді_На_Вітчизняній_Конференції] : 0)
                                        + (dictionary.ContainsKey(PublicationType.Тези_Доповіді_На_Міжнародній_Конференції) ? dictionary[PublicationType.Тези_Доповіді_На_Міжнародній_Конференції] : 0)).ToString(),
                [CONFERENCES_PERIOD_CONST] = ((dictionaryInReport.ContainsKey(PublicationType.Тези_Доповіді_На_Вітчизняній_Конференції) ? dictionaryInReport[PublicationType.Тези_Доповіді_На_Вітчизняній_Конференції] : 0)
                                        + (dictionaryInReport.ContainsKey(PublicationType.Тези_Доповіді_На_Міжнародній_Конференції) ? dictionaryInReport[PublicationType.Тези_Доповіді_На_Міжнародній_Конференції] : 0)).ToString(),
                [PATENTS_ALL_CONST] = dictionary.ContainsKey(PublicationType.Патент) ? dictionary[PublicationType.Патент].ToString() : "0",
                [PATENTS_PERIOD_CONST] = dictionaryInReport.ContainsKey(PublicationType.Патент) ? dictionaryInReport[PublicationType.Патент].ToString() : "0",
            });
        }

        private string GetPunktSeven(Report report)
        {
            if ((report.PatentForInevention == null || report.ParticipationInGrands == "")
                && (report.ApplicationForInevention == null || report.ApplicationForInevention == ""))
            {
                return "";
            }
            return GenerateTemplateForGenericPunktHeader(GetTitleForPunktSeven());
        }
        private string GetPunktSevenOne(Report report)
        {
            if ((report.PatentForInevention == null || report.ParticipationInGrands == ""))
            {
                return "";
            }
            return ReplaceStringWithParameters(GenerateTemplateForGenericPunkt(GetTitleForPunktSevenOne()), new Dictionary<string, string>()
            {
                [GENERIC_TEXT_CONST] = report.PatentForInevention,
            });
        }
        private string GetPunktSevenTwo(Report report)
        {
            if ((report.ApplicationForInevention == null || report.ApplicationForInevention == ""))
            {
                return "";
            }
            return ReplaceStringWithParameters(GenerateTemplateForGenericPunkt(GetTitleForPunktSevenTwo()), new Dictionary<string, string>()
            {
                [GENERIC_TEXT_CONST] = report.ApplicationForInevention,
            });
        }
        private string GetPunktEight(Report report)
        {
            if ((report.ReviewForTheses == null || report.ReviewForTheses == ""))
            {
                return "";
            }
            return ReplaceStringWithParameters(GenerateTemplateForGenericPunkt(GetTitleForPunktEight()), new Dictionary<string, string>()
            {
                [GENERIC_TEXT_CONST] = report.ReviewForTheses,
            });
        }
        private string GetPunktNine(Report report)
        {
            if ((report.MembershipInCouncils == null || report.MembershipInCouncils == ""))
            {
                return "";
            }
            return ReplaceStringWithParameters(GenerateTemplateForGenericPunkt(GetTitleForPunktNine()), new Dictionary<string, string>()
            {
                [GENERIC_TEXT_CONST] = report.MembershipInCouncils,
            });
        }
        private string GetPunktTen(Report report)
        {
            if ((report.Other == null || report.Other == ""))
            {
                return "";
            }
            return ReplaceStringWithParameters(GenerateTemplateForGenericPunkt(GetTitleForPunktTen()), new Dictionary<string, string>()
            {
                [GENERIC_TEXT_CONST] = report.Other,
            });
        }

        private string GetFooter(Report report)
        {
            var cathedraLeadInitials = db.Users.FirstOrDefault(x => x.Position.ID == 2 && x.Cathedra.ID == report.User.Cathedra.ID)?.I18nUserInitials.FirstOrDefault();
            var initials = string.Empty;
            if (cathedraLeadInitials != null)
                initials = cathedraLeadInitials.FirstName?.Substring(0, 1).ToUpper()
                    + ". " + cathedraLeadInitials.FathersName?.Substring(0, 1).ToUpper()
                    + ". " + cathedraLeadInitials.LastName;
            return ReplaceStringWithParameters(GetFooterTemplate(), new Dictionary<string, string>()
            {
                [PROTOCOL_CONST] = report.Protocol,
                [DATE_CONST] = report.Date == null ? "" : report.Date.Value.ToString("dd.MM.yyyy"),
                [CATHEDRA_CONST] = report.User.Cathedra == null ? "" : report.User.Cathedra.Name.Replace("Кафедра ", ""),
                [CATHEDRA_LEAD] = initials
            });
        }

        private string GenerateTemplateForGenericPunktHeader(String title)
        {
            return "<div class=\"block\"><p>&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp"
                + title
                + "</p></div>";
        }

        private string GetTitleForPunktTwo()
        {
            return "2. Участь у виконанні індивідуальних або колективних ґрантів (окрім ґрантів на поїздки) − згідно з додатком 3.";
        }
        private string GetTitleForPunktTree()
        {
            return "3. Наукові стажування";
        }
        private string GetTitleForPunktFour()
        {
            return @"4. Наукове керівництво аспірантами, здобувачами, наукове консультування при написанні
                докторських дисертацій. Захист дисертацій аспірантами, докторантами(прізвище, назва дисертації,
                спеціальність, дата захисту, рік закінчення аспірантури, докторантури).";
        }
        private string GetTitleForPunktFive()
        {
            return @"5. Керівництво студентською науковою роботою, спільні публікації, керівництво студентськими
                гуртками, підготовка студентів для участі у Всеукраїнських конкурсах студентських наукових робіт
                тощо.";
        }
        private string GetTitleForPunktSeven()
        {
            return "7. Патентно-ліцензійна діяльність:";
        }
        private string GetTitleForPunktSevenOne()
        {
            return @"7.1 Заявки на винахід (корисну модель) (на видачу патенту на винахід (корисну модель)) −
                автори, назва, № заявки, дата подачі, заявник(и):";
        }
        private string GetTitleForPunktSevenTwo()
        {
            return "7.2 Патенти на винахід (корисну модель) − автори, назва, № патенту, дата видачі, заявник(и):";
        }
        private string GetTitleForPunktEight()
        {
            return "8. Рецензування та опонування дисертацій, відгуки на автореферати, експертні висновки:";
        }
        private string GetTitleForPunktNine()
        {
            return "9. Членство у спеціалізованих вчених, експертних радах, редколегіях наукових журналів тощо:";
        }
        private string GetTitleForPunktTen()
        {
            return "10. Інше:";
        }
        private string GetSignture()
        {
            return "<div class=\"block input-text\"><p>Підпис_______________</p></div>";
        }

        private string GetFooterTemplate()
        {
            return "<div class=\"block input-text\"><p>Звіт заслухано і затверджено на засіданні кафедри(підрозділу)</p><p class=\"footer-text\">" 
                + CATHEDRA_CONST 
                + ", протокол: "
                + PROTOCOL_CONST 
                + ", дата: "
                + DATE_CONST 
                + "</p><p class=\"footer-text\">Завідувач кафедри"
                + CATHEDRA_CONST 
                + "__________________"
                + CATHEDRA_LEAD
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