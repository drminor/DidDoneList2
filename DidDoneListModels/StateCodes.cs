using System;
using System.Collections.Generic;

namespace DidDoneListModels
{
    public partial class StateCodes
    {
        public StateCodes()
        {
            CustomerSite = new HashSet<CustomerSite>();
            Customers = new HashSet<Customers>();
        }

        public int StateId { get; set; }
        public string StateCode { get; set; }
        public string StateName { get; set; }
        public string DomainName { get; set; }

        public ICollection<CustomerSite> CustomerSite { get; set; }
        public ICollection<Customers> Customers { get; set; }
    }
}
