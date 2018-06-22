using System;
using System.Collections.Generic;

namespace DidDoneJobJournalService.Models.DB
{
    public partial class Visits
    {
        public Visits()
        {
            VisitWorker = new HashSet<VisitWorker>();
        }

        public int VisitId { get; set; }
        public int CustomerSiteId { get; set; }
        public string DescriptionOfWork { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime? EndTime { get; set; }

        public CustomerSite CustomerSite { get; set; }
        public ICollection<VisitWorker> VisitWorker { get; set; }
    }
}
