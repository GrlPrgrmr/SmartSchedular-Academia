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

        private string expectedGradYear;

        public string ExpectedGradYear
        {
            get { return expectedGradYear; }
            set { expectedGradYear = value; }
        }


    }
}
