using CsvHelper;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using ProjectCSULB.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;

using System.Windows;

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

        private ObservableCollection<DataReport> dataReportList;

        public ObservableCollection<DataReport> DataReportList
        {
            get { return dataReportList; }
            set { dataReportList = value; RaisePropertyChanged(() => DataReportList); }
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



        public static List<Student> getStudentData(string major)
        {
            using (CsvReader csv = new CsvReader(File.OpenText(@"E:\New Job\Students Data\"+major+".csv")))
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
        public  StudentViewModel(ObservableCollection<ScheduleReportItem> ScheduleForSem,int batchSize,Course currentCourse,string year,int iterCount)
        {
            StudentData = new ObservableCollection<Student>();
            DataReportList = new ObservableCollection<DataReport>();

            try
            {
                
                StudentData = new ObservableCollection<Student>(getStudentData(currentCourse.Name.Replace(" ", String.Empty)).Where(s => s.Major == currentCourse.Name && s.FreshmanYear == year).ToList().GetRange(0, batchSize));
                int studentCountInMajor = StudentData.Count;
                StudentHeadCount = StudentData.Count.ToString();

                for (int i = 0; i < iterCount; i++)
                {
                    //trying monte carlo simulation to generate probability distribution for students taking up sections
                    foreach (var student in StudentData)
                    {

                        student.SubjectList = new List<ScheduleReportItem>();
                        var groupSubjects = ScheduleForSem.Where(s => s.Components == "SEM" || s.Components == "LEC" || s.Components == "ACT").GroupBy(x => x.Subject);

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
                        //var queryComplex = StudentData.Where(s => s.SubjectList.Any(sub => ScheduleForSem.Where(sc => sc.Color == "Color").Contains(sub)));
                        
                        var queryStud = student.SubjectList.Where(s1 => student.SubjectList.Any(s2 => !s1.Subject.Equals(s2.Subject) && s1.Days.Equals(s2.Days) && ((s1.B_Time <= s2.E_Time) && (s2.B_Time <= s1.E_Time)))).ToList().Select(c => { c.Color = " "; flagStduentConflictFound = true; student.SubInConflict++; return c; }).ToList();

                        if (flagStduentConflictFound)
                        {
                            StudentsAffected++;
                            flagStduentConflictFound = false;
                        }


                        DataReport dRObj = new DataReport(batchSize, currentCourse, year, StudentsAffected, studentHeadCount);

                        DataReportList.Add(dRObj);

                        student.SubjectList.Clear();


                    }
                }

                FileHelper.CreateCSVFromGenericList(new List<DataReport>(DataReportList), @"E:\New Job\DataReportCSULB.csv");


                
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            
        }

       

    }
}
