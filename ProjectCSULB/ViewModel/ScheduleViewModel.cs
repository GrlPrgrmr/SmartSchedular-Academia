using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using ProjectCSULB.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectCSULB.ViewModel
{
    public class ScheduleViewModel : ViewModelBase
    {

        private Schedule currentschedule;

        public Schedule CurrentSchedule
        {
            get { return currentschedule; }
            set { currentschedule = value; RaisePropertyChanged(() => CurrentSchedule); }
        }

        [PreferredConstructor]
        public ScheduleViewModel()
        {

        }

        public ScheduleViewModel(Schedule schedule)
        {
            CurrentSchedule = new Schedule();
            CurrentSchedule = schedule;

        }


        
    }
}
