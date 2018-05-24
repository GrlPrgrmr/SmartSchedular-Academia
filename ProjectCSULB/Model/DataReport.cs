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

        private List<Semester> sems;

        public List<Semester> Sems
        {
            get { return sems; }
            set { sems = value; }
        }



        public DataReport()
        {

        }
        public DataReport(int batchSize, Course currentCourse, string year, int studentAffected, List<Student> studentData,int iterCount,List<ScheduleReportItem> schd, List<ScheduleReportItem> fullSchedule)
        {
            BatchSize = batchSize.ToString();
            Major = currentCourse.Name;
            RunId = new Random(1).ToString();
            Year = year;
            StudentsAffected = studentAffected;
            Sems = new List<Semester>();
            Sems.AddRange(currentCourse.SemesterList);
            Sems[0].AvgStudentEffected = studentAffected;
            Sems[0].BatchSize = batchSize;
            if (StudentsAffected > 0)
            {
                NoConflictStudentCount = (Convert.ToInt32(BatchSize) - studentAffected).ToString();
            }
            else
            {
                NoConflictStudentCount = "0";
            }

            chainingSemesters(studentData, iterCount, schd, fullSchedule);


        }

        private void chainingSemesters(List<Student> StudentData,int iterations,List<ScheduleReportItem> ScheduleForSem, List<ScheduleReportItem> fullSchedule)
        {
           for(int j=1;j<Sems.Count;j++)
            {
                Sems[j].BatchSize = Sems[j - 1].BatchSize - Sems[j - 1].AvgStudentEffected;



                for (int i = 0; i < iterations; i++)
                {
                    //trying monte carlo simulation to generate probability distribution for students taking up sections
                    foreach (var student in StudentData.GetRange(0,Sems[j].BatchSize))
                    {

                        student.SubjectList = new List<ScheduleReportItem>();
                        var groupSubjects = ScheduleForSem.Where(s => s.Components == "SEM" || s.Components == "LEC" || s.Components == "ACT" || s.Components == "LAB").GroupBy(x => x.Subject);

                        foreach (var sub in groupSubjects)
                        {
                            Random random = new Random();
                            int indexRandom = random.Next(sub.ToList().Count - 1);

                            var subjectPicked = sub.ToList()[indexRandom];
                            student.SubjectList.Add(subjectPicked);
                        }


                        //foreach (var sub in groupSubjects)
                        //{
                        //    CryptoRandom random = new CryptoRandom();
                        //    int indexRandom = random.Next(sub.ToList().Count - 1);

                        //    var subjectPicked = sub.ToList()[indexRandom];
                        //    student.SubjectList.Add(subjectPicked);
                        //}


                        bool flagStduentConflictFound = false;


                        //figure out how many students from above list have been assigned conflicting sections
                        //var queryComplex = StudentData.Where(s => s.SubjectList.Any(sub => ScheduleForSem.Where(sc => sc.Color == "Color").Contains(sub))); && s1.Days.Equals(s2.Days)

                        var queryStud = student.SubjectList.Where(s1 => student.SubjectList.Any(s2 => !s1.Subject.Equals(s2.Subject) && (s1.Days.Length > 0 && s2.Days.Length > 0) && (s1.Days.Contains(s2.Days) || s2.Days.Contains(s1.Days)) && ((s1.B_Time <= s2.E_Time) && (s2.B_Time <= s1.E_Time)))).ToList().Select(c => { c.Color = " "; flagStduentConflictFound = true; student.SubInConflict++; return c; }).ToList();

                        if (flagStduentConflictFound)
                        {
                            StudentsAffected++;
                            flagStduentConflictFound = false;
                        }



                        student.SubjectList.Clear();

                    }


                    //DataReport dRObj = new DataReport(batchSize, currentCourse, year, StudentsAffected, studentHeadCount, StudentData.ToList(), iterCount);

                    //DataReportList.Add(dRObj);
                    Sems[j].AvgStudentEffected = StudentsAffected;
                    
                    StudentsAffected = 0;

                }

            }
        }

    }
}
