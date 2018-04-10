using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectCSULB.Model
{
    public class DataReport
    {
        private string runId;

        public string RunId
        {
            get { return runId; }
            set { runId = value; }
        }


        private string major;

        public string Major
        {
            get { return major; }
            set { major = value; }
        }

        private string year;

        public string Year
        {
            get { return year; }
            set { year = value; }
        }

        private string batchSize;

        public string BatchSize
        {
            get { return batchSize; }
            set { batchSize = value; }
        }

        private int studentsAffected;

        public int StudentsAffected
        {
            get { return studentsAffected; }
            set { studentsAffected = value; }
        }


        private string noConflictStudentCount;

        public string NoConflictStudentCount
        {
            get { return noConflictStudentCount; }
            set { noConflictStudentCount = value; }
        }

        public DataReport()
        {

        }
        public DataReport(int batchSize, Course currentCourse, string year, int studentAffected, string studentHeadCount)
        {
            BatchSize = batchSize.ToString();
            Major = currentCourse.Name;
            RunId = new Random(1).ToString();
            Year = year;
            StudentsAffected = studentAffected;
            if (StudentsAffected > 0)
            {
                NoConflictStudentCount = (Convert.ToInt32(studentHeadCount) - studentAffected).ToString();
            }
            else
            {
                NoConflictStudentCount = "0";
            }


        }

    }
}
