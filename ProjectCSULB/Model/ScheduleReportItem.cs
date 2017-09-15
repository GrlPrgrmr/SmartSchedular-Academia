using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectCSULB
{
    public class ScheduleReportItem
    {
        //private string college;

        //public string College
        //{
        //    get { return college; }
        //    set { college = value; }
        //}

        //private string department;

        //public string Department
        //{
        //    get { return department; }
        //    set { department = value; }
        //}

        private string subject;

        public string Subject
        {
            get { return subject; }
            set { subject = value; }
        }

        private string  enrollment_Cap;

        public string Enrollment_Cap
        {
            get { return enrollment_Cap; }
            set { enrollment_Cap = value; }
        }

        private string enrollment_Act;

        public string Enrollment_Act
        {
            get { return enrollment_Act; }
            set { enrollment_Act = value; }
        }

        private string waitlist_Total;

        public string WaitList_Total
        {
            get { return waitlist_Total; }
            set { waitlist_Total = value; }
        }


        //private string catalog_nbr;

        //public string Catalog_Nbr
        //{
        //    get { return catalog_nbr; }
        //    set { catalog_nbr = value; }
        //}

        private string section;

        public string Section
        {
            get { return section; }
            set { section = value; }
        }

        private string sessiom;

        public string Session
        {
            get { return sessiom; }
            set { sessiom = value; }
        }

        private string class_nbr;

        public string Class_Nbr
        {
            get { return class_nbr; }
            set { class_nbr = value; }
        }

        //private string enr_mngment_Flag;

        //public string Enr_Mngment_Flag
        //{
        //    get { return enr_mngment_Flag; }
        //    set { enr_mngment_Flag = value; }
        //}

        //private string instruction_mode;

        //public string Instruction_Mode
        //{
        //    get { return instruction_mode; }
        //    set { instruction_mode = value; }
        //}

        //private string instruction_mode_descr;

        //public string Instruction_Mode_Descr
        //{
        //    get { return instruction_mode_descr; }
        //    set { instruction_mode_descr = value; }
        //}

        //private string apdb_learning_mode;

        //public string APDB_Learning_Mode
        //{
        //    get { return apdb_learning_mode; }
        //    set { apdb_learning_mode = value; }
        //}


        //private string apdb_learning_mode_descr;

        //public string APDB_Learning_Mode_Descr
        //{
        //    get { return apdb_learning_mode_descr; }
        //    set { apdb_learning_mode_descr = value; }
        //}

        //private string multiple_Meetings;

        //public string Multiple_Meetings
        //{
        //    get { return multiple_Meetings; }
        //    set { multiple_Meetings = value; }
        //}


        private string title;

        public string Title
        {
            get { return title; }
            set { title = value; }
        }

        private string units;

        public string Units
        {
            get { return units; }
            set { units = value; }
        }

        private string color;

        public string Color
        {
            get { return color; }
            set { color = value; }
        }


        private string components;

        public string Components
        {
            get { return components; }
            set { components = value; }
        }

        private string days;

        public string Days
        {
            get { return days; }
            set { days = value; }
        }

        private string[] daysArray;

        public string[] DaysArray
        {
            get { return daysArray; }
            set { daysArray = value; }
        }


        private string begin_time;

        public string Begin_Time
        {
            get { return begin_time; }
            set { begin_time = value; }
        }

        private string end_time;

        public string End_Time
        {
            get { return end_time; }
            set { end_time = value; }
        }


        private TimeSpan b_Time;

        public TimeSpan B_Time
        {
            get { return b_Time; }
            set { b_Time = value; }
        }

        private TimeSpan e_Time;

        public TimeSpan E_Time
        {
            get { return e_Time; }
            set { e_Time = value; }
        }





    }
}
