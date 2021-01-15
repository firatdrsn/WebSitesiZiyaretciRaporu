using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebSitesiZiyaretciRaporu.Models;

namespace WebSitesiZiyaretciRaporu.ViewModel
{
    public class DataViewModel
    {
        public List<Data> ListData { get; set; }
        public int TotalVisitCount { get; set; }
        public string MostVisitCountry { get; set; }
        public List<ListPageModel> ListPage { get; set; }
        public List<ListVisitDateModel> ListDate { get; set; }
    }
    public class ListPageModel
    {
        public string PageName { get; set; }
        public int PageCount { get; set; }
    }
    public class ListVisitDateModel
    {
        public DateTime VisitDate { get; set; }
        public int VisitCount { get; set; }
    }
    public class ListVisitCountry
    {
        public string CountryName { get; set; }
        public int VisitCountryCount { get; set; }
    }
}