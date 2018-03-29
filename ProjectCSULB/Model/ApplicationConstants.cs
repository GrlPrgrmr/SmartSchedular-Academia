using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectCSULB.Model
{
    public static class ApplicationConstants
    {
        public static List<string> CollegeList { get { return new List<string>() { "College of Engineering", "College of Natural Sciences and Maths","Health and Human Services","College of Business Administration" }; } }

        public static Dictionary<string, string> CoursesRoadmapNSM { get
            {
                return new Dictionary<string, string>()
                {
                    {"Mathematics","http://web.csulb.edu/divisions/aa/catalog/2012-2013/roadmaps/cnsm/math/index.html" },
                    {"Chemistry","http://web.csulb.edu/divisions/aa/catalog/2012-2013/roadmaps/cnsm/chem/index.html" },
                    {"BioChemistry","http://web.csulb.edu/divisions/aa/catalog/2012-2013/roadmaps/cnsm/chem/chembs02-4yr.html" },
                    {"Geology","http://web.csulb.edu/divisions/aa/catalog/2012-2013/roadmaps/cnsm/geol/index.html" },
                    {"Physics","http://web.csulb.edu/divisions/aa/catalog/2012-2013/roadmaps/cnsm/phys/index.html" },
                   
                };
            }
        }

        public static Dictionary<string, string> CoursesRoadmapHHS
            {
               get
               { return new Dictionary<string, string>()
                       {
                            { "Nursing-RN-2Yr","http://web.csulb.edu/divisions/aa/catalog/2012-2013/roadmaps/chhs/nrsg/nrsgbs02-2yr.html" },
                            {"Nursing-Basic-3Yr","http://web.csulb.edu/divisions/aa/catalog/2012-2013/roadmaps/chhs/nrsg/index.html" },
                            {"BS_HealthScience_RadiationTherapy","http://web.csulb.edu/divisions/aa/catalog/2012-2013/roadmaps/chhs/hsc/index.html" },
                            {"BS_HealthScience_CommunityHealthEd","http://web.csulb.edu/divisions/aa/catalog/2012-2013/roadmaps/chhs/hsc/hsc_bs02-4yr.html" },
                            {"BS_HealthScience_SchoolHealthEd","http://web.csulb.edu/divisions/aa/catalog/2012-2013/roadmaps/chhs/hsc/hsc_bs03-4yr.html" },
                            {"BS_HealthScience_HealthCare", "http://web.csulb.edu/divisions/aa/catalog/2012-2013/roadmaps/chhs/hsc/hsc_bs04-4yr.html"}

                       };
               } 
            }

        

        public static Dictionary<string,string> CourseRoadmapCBA
        {
            get
            {
                return new Dictionary<string, string>()
                    {
                        { "BBA_Accountancy","http://web.csulb.edu/divisions/aa/catalog/2012-2013/roadmaps/cba/acct/index.html" },
                        { "BBA_Finance","http://web.csulb.edu/divisions/aa/catalog/2012-2013/roadmaps/cba/fin/index.html"},
                        { "BBA_MIS","http://web.csulb.edu/divisions/aa/catalog/2012-2013/roadmaps/cba/is/index.html"},
                        { "BBA_IB","http://web.csulb.edu/divisions/aa/catalog/2012-2013/roadmaps/cba/ibus/index.html"},
                        { "BBA_Mgmt","http://web.csulb.edu/divisions/aa/catalog/2012-2013/roadmaps/cba/mgmt/index.html" },
                        { "BBA_OperationsAndSCM","http://web.csulb.edu/divisions/aa/catalog/2012-2013/roadmaps/cba/mgmt/mgmtbs02-4yr.html"},
                        { "BBA_HRM","http://web.csulb.edu/divisions/aa/catalog/2012-2013/roadmaps/cba/mgmt/mgmtbs03-4yr.html"},
                        { "BBA_Marketing","http://web.csulb.edu/divisions/aa/catalog/2012-2013/roadmaps/cba/mktg/index.html"}

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

        public static List<string> CourseTitles { get { return new List<string>() { "ENGR","CHEM","MATH","MAE","C E","PHYS","CECS","BIOL","MICR","ACCT","ECON","IS","STAT","FIN","N","NRSG"}; } }


        

       
    }
}
