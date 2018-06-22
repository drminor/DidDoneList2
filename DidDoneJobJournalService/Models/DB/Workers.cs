using System;
using System.Collections.Generic;

namespace DidDoneJobJournalService.Models.DB
{
    public partial class Workers
    {
        public Workers()
        {
            VisitWorker = new HashSet<VisitWorker>();
        }

        public int WorkerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool Ten99 { get; set; }
        public DateTime StartDate { get; set; }
        public bool Active { get; set; }
        public bool CrewBoss { get; set; }
        public decimal Rate { get; set; }

        public ICollection<VisitWorker> VisitWorker { get; set; }
    }
}
