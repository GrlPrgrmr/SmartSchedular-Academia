using CsvHelper;
using ProjectCSULB.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectCSULB.ViewModel
{
    public class StudentViewModel
    {
        private List<Student> studentData;

        public List<Student> StudedentData
        {
            get { return studentData; }
            set { studentData = value; }
        }


        public static List<Student> getStudentData()
        {
            using (CsvReader csv = new CsvReader(File.OpenText(@"E:\New Job\StudentsData.csv")))
            {
                csv.Configuration.RegisterClassMap<StudentMap>();
                List<Student> dataReport = csv.GetRecords<Student>().ToList();
                return dataReport;
            }

        }


    }
}
