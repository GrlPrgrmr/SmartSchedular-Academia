using CsvHelper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectCSULB.Model
{
    public class GECourses
    {
        private string category;

        public string Category
        {
            get { return category; }
            set { category = value; }
        }

        private string subject;

        public string Subject
        {
            get { return subject; }
            set { subject = value; }
        }

        private List<GECourses> geData;

        public List<GECourses> GEData
        {
            get { return geData; }
            set { geData = value; }
        }


        public static List<GECourses> getGECoursesData()
        {
            List<GECourses> ge_courses = new List<GECourses>();
            using (CsvReader csv = new CsvReader(File.OpenText(@"C:\Users\Neha\Desktop\GECourses.csv")))
            {
                csv.Configuration.RegisterClassMap<GEMap>();
                ge_courses = csv.GetRecords<GECourses>().ToList();

            }

            return ge_courses;

        }


        public static List<string> getGECoursesName()
        {
            GECourses ge = new GECourses();
            ge.GEData = getGECoursesData();

            var distinctGECourses = ge.GEData.Select(x => x.Category).Distinct().ToList();


            return distinctGECourses;


        }
    }
}
