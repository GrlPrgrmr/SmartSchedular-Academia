using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectCSULB.Model
{
    public class MessageScheduleToStudent:Messenger
    {
        private ObservableCollection<ScheduleReportItem> sched;

        public ObservableCollection<ScheduleReportItem> Sched
        {
            get { return sched; }
            set { sched = value; }
        }

        private int batchSize;

        public int BatchSize
        {
            get { return batchSize; }
            set { batchSize = value; }
        }

        private Course course;

        public Course Course_
        {
            get { return course; }
            set { course = value; }
        }

        private string year;

        public string Year
        {
            get { return year; }
            set { year = value; }
        }

        private int count;

        public int Count
        {
            get { return count; }
            set { count = value; }
        }


    }
}
