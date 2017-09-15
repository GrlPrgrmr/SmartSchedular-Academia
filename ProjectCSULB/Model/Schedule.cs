using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectCSULB.Model
{
    public class Schedule
    {
        private int semId;

        public int SemID
        {
            get { return semId; }
            set { semId = value; }
        }

        private List<ScheduleReportItem> semScheduleReport;

        public List<ScheduleReportItem> SemScheduleReport
        {
            get { return semScheduleReport; }
            set { semScheduleReport = value; }
        }


    }
}
