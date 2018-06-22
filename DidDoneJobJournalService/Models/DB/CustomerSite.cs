using System;
using System.Collections.Generic;

namespace DidDoneJobJournalService.Models.DB
{
    public partial class CustomerSite
    {
        public CustomerSite()
        {
            Visits = new HashSet<Visits>();
        }

        public int CustomerSiteId { get; set; }
        public int CustomerId { get; set; }
        public string SiteName { get; set; }
        public string SiteAddress { get; set; }
        public string City { get; set; }
        public int StateId { get; set; }
        public string EmailAddress { get; set; }
        public decimal? Long { get; set; }
        public decimal? Lat { get; set; }

        public Customers Customer { get; set; }
        public StateCodes State { get; set; }
        public ICollection<Visits> Visits { get; set; }
    }
}
