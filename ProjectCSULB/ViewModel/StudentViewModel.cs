using CsvHelper;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using ProjectCSULB.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectCSULB.ViewModel
{
    
    public class StudentViewModel:ViewModelBase
    {
        private ObservableCollection<Student> studentData;

        public ObservableCollection<Student> StudentData
        {
            get { return studentData; }
            set { studentData = value; RaisePropertyChanged(() => StudentData); }
        }

      
       public class studetDataAnalysis
        {
            public string studentId;
            public int subInConflict;
            public List<ScheduleReportItem> subList;

        }

        private string studentHeadCount;

        public string StudentHeadCount
        {
            get { return studentHeadCount; }
            set { studentHeadCount = value; RaisePropertyChanged(() => StudentHeadCount); }
        }

        private int studentsAffected;

        public int StudentsAffected
        {
            get { return studentsAffected; }
            set { studentsAffected = value; RaisePropertyChanged(() => StudentsAffected); }
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

        [PreferredConstructor]
        public StudentViewModel()
        {

        }
        public  StudentViewModel(ObservableCollection<ScheduleReportItem> ScheduleForSem)
        {
            StudentData = new ObservableCollection<Student>();
            StudentData = new ObservableCollection<Student>( getStudentData());
            int studentCountInMajor = StudentData.Count;
            //trying monte carlo simulation to generate probabblity distribution for students taking up sections
            foreach (var student in StudentData)
            {

                student.SubjectList = new List<ScheduleReportItem>();
                var groupSubjects = ScheduleForSem.Where(s => s.Components == "SEM" || s.Components == "LEC").GroupBy(x => x.Subject);

                foreach (var sub in groupSubjects)
                {
                    Random random = new Random();
                    int indexRandom = random.Next(sub.ToList().Count - 1);

                    var subjectPicked = sub.ToList()[indexRandom];
                    student.SubjectList.Add(subjectPicked);
                }



            }


            bool flagStduentConflictFound = false;

            //figure out how many students from above list have been assigned conflicting sections
            //var queryComplex = StudentData.Where(s => s.SubjectList.Any(sub => ScheduleForSem.Where(sc => sc.Color == "Color").Contains(sub)));
            
            foreach (var student in StudentData)
            {
                var queryStud = student.SubjectList.Where(s1 => student.SubjectList.Any(s2 => !s1.Subject.Equals(s2.Subject) && ((s1.B_Time <= s2.E_Time) && (s2.B_Time <= s1.E_Time)))).ToList().Select(c => { c.Color = "ColorStud"; flagStduentConflictFound = true; student.SubInConflict++; return c; }).ToList();

                if (flagStduentConflictFound)
                {
                    StudentsAffected++;
                    flagStduentConflictFound = false;
                }
            }



            StudentHeadCount = StudentData.Count.ToString();

            
        }


    }
}
