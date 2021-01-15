using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebSitesiZiyaretciRaporu.Models
{
    public class Data
    {
        public string PageName { get; set; }
        public string Browser { get; set; }
        public string Country { get; set; }
        public DateTime VisitDate { get; set; }
    }
}