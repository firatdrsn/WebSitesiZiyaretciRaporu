using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using WebSitesiZiyaretciRaporu.Models;
using WebSitesiZiyaretciRaporu.ViewModel;

namespace WebSitesiZiyaretciRaporu.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index(DateTime? startDate, DateTime? endDate, bool isFilterFlag = false)
        {
            StreamReader reader = new StreamReader(Server.MapPath(@"~\Data\Data.txt"), System.Text.Encoding.Default);
            List<Data> visitDatas = new List<Data>();
            string[] splintContent = new string[] { };
            string line;

            while ((line = reader.ReadLine()) != null)
            {
                splintContent = line.Split('\t');
                visitDatas.Add(new Data { PageName = splintContent[0], Browser = splintContent[1], Country = splintContent[2], VisitDate = Convert.ToDateTime(splintContent[3]) });
            }

            List<ListVisitDateModel> ListByVisitDate = GetVisitDate(visitDatas);
            List<ListVisitCountry> visitDataCountryCount = GetVisitCountry(visitDatas);
            List<ListPageModel> ListByVisitPage = GetVisitPage(visitDatas);

            if (isFilterFlag)
            {
                if (startDate != null)
                    ListByVisitDate = ListByVisitDate.Where(x => x.VisitDate >= startDate.Value).ToList();
                if (endDate != null)
                    ListByVisitDate = ListByVisitDate.Where(x => x.VisitDate <= endDate.Value).ToList();
            }
            DataViewModel dataViewModel = new DataViewModel
            {
                ListData = visitDatas,
                MostVisitCountry = visitDataCountryCount.Max(x => x.CountryName),
                TotalVisitCount = visitDatas.Count,
                ListPage = ListByVisitPage,
                ListDate = ListByVisitDate
            };

            return View(dataViewModel);
        }
        public List<ListVisitDateModel> GetVisitDate(List<Data> visitDatas)
        {
            List<ListVisitDateModel> ListByVisitDate = visitDatas.GroupBy(l => Convert.ToDateTime(l.VisitDate.ToShortDateString()))
                .Select(lg => new ListVisitDateModel
                {
                    VisitDate = lg.Key,
                    VisitCount = lg.Count()
                }).ToList();
            return ListByVisitDate;
        }
        public List<ListVisitCountry> GetVisitCountry(List<Data> visitDatas)
        {
            List<ListVisitCountry> visitDataCountryCount = visitDatas.GroupBy(l => l.Country)
                .Select(lg => new ListVisitCountry
                {
                    CountryName = lg.Key,
                    VisitCountryCount = lg.Count()
                }).OrderBy(x => x.VisitCountryCount).ToList();
            return visitDataCountryCount;
        }
        public List<ListPageModel> GetVisitPage(List<Data> visitDatas)
        {
            List<ListPageModel> ListByVisitPage = visitDatas.GroupBy(l => l.PageName)
                .Select(lg => new ListPageModel
                {
                    PageName = lg.Key,
                    PageCount = lg.Count()
                }).OrderBy(x=>x.PageCount).ToList();
            return ListByVisitPage;
        }
    }
}