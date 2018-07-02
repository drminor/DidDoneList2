using System;
using System.Collections.Generic;
using System.Text;

namespace DidDoneListModels
{
    public partial class Customers
    {
        public Customers()
        {
            CustomerSite = new HashSet<CustomerSite>();
        }

        public int CustomerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string StreetAddress { get; set; }
        public string City { get; set; }
        public int StateId { get; set; }
        public string Zip { get; set; }
        public string EmailAddress { get; set; }
        public bool Active { get; set; }

        public StateCodes State { get; set; }
        public ICollection<CustomerSite> CustomerSite { get; set; }
    }
}
