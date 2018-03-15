using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectCSULB.Model
{
    public class Student
    {
        private string studentId;

        public string StudentId
        {
            get { return studentId; }
            set { studentId = value; }
        }

        
        private string studentName;

        public string StudentName
        {
            get { return studentName; }
            set { studentName = value; }
        }


        private string major;

        public string Major
        {
            get { return major; }
            set { major = value; }
        }

        private string freshmanYear;

        public string FreshmanYear
        {
            get { return freshmanYear; }
            set { freshmanYear = value; }
        }


        private int subInConflict;

        public int SubInConflict
        {
            get { return subInConflict; }
            set { subInConflict = value; }
        }


        private List<ScheduleReportItem> subjectList;

        public List<ScheduleReportItem> SubjectList
        {
            get { return subjectList; }
            set { subjectList = value; }
        }


    }
}
