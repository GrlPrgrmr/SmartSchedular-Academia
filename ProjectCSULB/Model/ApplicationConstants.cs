using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectCSULB.Model
{
    public static class ApplicationConstants
    {
        public static List<string> CourseListStrings { get { return new List<string>()
        { "Chemical Engineering","Computer Engineering","Civil Engineering" }; } }
        public static Dictionary<string,string> CourseRoadmapLinks { get
            {
                return new Dictionary<string, string>()
                {
                    { "Chemical Engineering", "http://web.csulb.edu/divisions/aa/catalog/2012-2013/roadmaps/coe/che/index.html" },
                    { "Computer Engineering","http://web.csulb.edu/divisions/aa/catalog/2012-2013/roadmaps/coe/cecs/index.html"},
                    { "Civil Engineering" ,"http://web.csulb.edu/divisions/aa/catalog/2012-2013/roadmaps/coe/ce/index.html"}
                };
            } }


        public static Dictionary<string , string[]> DaysMapping { get { return new Dictionary<string, string[]>()
                {
                        { "MW",new string[]{"M","W"} },
                        {"TuTh",new string[]{"Tu","Th"} },
                        
                };
        }
        }

        public static List<string> CourseTitles { get { return new List<string>() { "ENGR","CHEM","MATH","MAE","C E","PHYS","CECS"}; } }

       
    }
}
