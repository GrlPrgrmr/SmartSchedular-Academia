using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectCSULB.Model
{
    public static class ApplicationConstants
    {
        public static List<string> CollegeList { get { return new List<string>() { "College of Engineering","College of Natural Sciences and Maths"}; } }

        public static Dictionary<string,string> CoursesRoadmapNSM { get
            {
                return new Dictionary<string, string>()
                {
                    {"Mathematics","http://web.csulb.edu/divisions/aa/catalog/2012-2013/roadmaps/cnsm/math/index.html" },
                    {"Chemistry","http://web.csulb.edu/divisions/aa/catalog/2012-2013/roadmaps/cnsm/chem/index.html" },
                    {"BioChemistry","http://web.csulb.edu/divisions/aa/catalog/2012-2013/roadmaps/cnsm/chem/chembs02-4yr.html" },
                    {"Geology","http://web.csulb.edu/divisions/aa/catalog/2012-2013/roadmaps/cnsm/geol/index.html" },
                    {"Physics","http://web.csulb.edu/divisions/aa/catalog/2012-2013/roadmaps/cnsm/phys/index.html" }
                };
            }
        }
        public static List<string> YearsList { get { return new List<string>() { "2012","2013","2014","2015","2016"}; } }

        public static string[] SemesterList { get { return new string[] { "Fall","Winter","Spring","Summer"}; } }
        
        public static List<string> CoursesCOE { get { return new List<string>()
        { "Chemical Engineering","Computer Engineering","Civil Engineering" }; } }
        public static Dictionary<string,string> CourseRoadmapCOE { get
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
