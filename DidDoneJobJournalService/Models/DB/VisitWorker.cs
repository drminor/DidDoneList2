using System;
using System.Collections.Generic;

namespace DidDoneJobJournalService.Models.DB
{
    public partial class VisitWorker
    {
        public int VisitId { get; set; }
        public int WorkerId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public decimal Rate { get; set; }

        public Visits Visit { get; set; }
        public Workers Worker { get; set; }
    }
}
