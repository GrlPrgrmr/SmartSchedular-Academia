using System;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using ProjectCSULB.Model;
using System.Collections.ObjectModel;
using System.Windows;
using System.Collections.Generic;
using HtmlAgilityPack;
using System.Linq;
using CsvHelper;
using System.IO;
using System.Text.RegularExpressions;
using System.Globalization;

namespace ProjectCSULB.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// See http://www.mvvmlight.net
    /// </para>
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        private string url;

        public string URL
        {
            get { return url; }
            set { url = value; RaisePropertyChanged(()=>this.URL); }
        }

        private string courseData;
        public  string CourseData
        {
            get { return courseData; }
            set { courseData = value; RaisePropertyChanged();
            }
        }

        private Schedule  currentSchedule;

        public Schedule  CurrentSchedule
        {
            get { return currentSchedule; }
            set { currentSchedule = value; }
        }


        private List<ScheduleReportItem> schdule;

        public List<ScheduleReportItem> Schedule
        {
            get { return schdule; }
            set { schdule = value; RaisePropertyChanged(()=>Schedule); }
        }


        private Course currentCourse;

        public Course CurrentCourse
        {
            get { return currentCourse; }
            set { currentCourse = value; RaisePropertyChanged(()=>CurrentCourse); }
        }


        private ObservableCollection<string> courseNames;

        public ObservableCollection<string> CourseNames
        {
            get { return courseNames; }
            set { courseNames = value; }
        }

        private string selectedCourse;

        public string SelectedCourse
        {
            get { return selectedCourse; }
            set
            {
                selectedCourse = value;
                URL = ApplicationConstants.CourseRoadmapLinks[selectedCourse];
                RaisePropertyChanged(()=>SelectedCourse);
            }
        }

        private ObservableCollection<ScheduleReportItem> scheduleForSem;

        public ObservableCollection<ScheduleReportItem> ScheduleForSem
        {
            get { return scheduleForSem; }
            set { scheduleForSem = value; RaisePropertyChanged(()=> ScheduleForSem); }
        }

        private RelayCommand getConflictsCommand;

        public RelayCommand GetConflictsCommand
        {
            get { return getConflictsCommand; }
            private set { getConflictsCommand = value; }
        }

        private List<GECourses> geData;

        public List<GECourses> GEData
        {
            get { return geData; }
            set { geData = value; }
        }


        private RelayCommand getDataCommand;

        public RelayCommand GetDataCommand
        {
            get { return getDataCommand; }
            private set { getDataCommand = value; }
        }

        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel(IDataService dataService)
        {
            GetDataCommand = new RelayCommand(getData);
            GetConflictsCommand = new RelayCommand(getConflicts);

            CourseNames = new ObservableCollection<string>();
            CourseNames = new ObservableCollection<string>( ApplicationConstants.CourseListStrings);

        }

        private void getConflicts()
        {

            List<Subject> subByCourse = CurrentCourse.SemesterList.Select(s => s.SubjectList).ToList()[0];

            var temp = Schedule.Select(s => s.Subject.Replace("    ", String.Empty)).ToList();

            ScheduleForSem =new ObservableCollection<ScheduleReportItem>
                ( Schedule 
                .Where(s => subByCourse
                .Any(c => c.Title == s.Subject.Replace("    ", String.Empty))));

            foreach (var item in ScheduleForSem)
            {
                if (!(String.IsNullOrEmpty(item.Begin_Time) && String.IsNullOrEmpty(item.End_Time)))
                {
                    DateTime dt = DateTime.ParseExact(item.Begin_Time, "hh:mmtt", CultureInfo.InvariantCulture);
                    var temp1 = dt.TimeOfDay;


                    TimeSpan b_time = TimeSpan.Parse(item.Begin_Time.Substring(0, 5));
                    item.B_Time = b_time;
                    TimeSpan e_time = TimeSpan.Parse(item.End_Time.Substring(0, 5));
                    item.E_Time = e_time;
                }


            }

            //var query = ScheduleForSem.Where(s1 => ScheduleForSem.Any(s2 =>
            //                                    s1.Subject != s2.Subject
            //                                    && s1.Days == s2.Days &&
            //                                    (s1.B_Time <= s2.E_Time) && (s2.B_Time <= s1.E_Time))).GroupBy(s => s.Days)
            //                                    .ToList();

            var groupsFromSchedule = ScheduleForSem.GroupBy(s => s.Days);

            foreach(var group in groupsFromSchedule)
            {
                var days = group.Key;

                //main logic to find the time overlap
                var query = group.Where(s1 => group.Any(s2 => s1.Subject != s2.Subject && ((s1.B_Time <= s2.E_Time) && (s2.B_Time <= s1.E_Time)))).ToList();

                foreach(var item in query)
                {
                    ScheduleForSem.Where(s => s.Class_Nbr == item.Class_Nbr).Select(c => { c.Color = "Color"; return c; }).ToList();
                    RaisePropertyChanged(() => ScheduleForSem);
                }
                var count = query.Count;
            }

            //var data = (from sub in subByCourse
            //           join schd in Schedule on sub.Title equals schd.Subject.Replace(" ",String.Empty) + " " + schd.Catalog_Nbr
            //           select new { sub.Name, sub.Title, schd.Units, schd.Begin_Time, schd.End_Time }).ToList();


        }

        private void getData()
        {
            Course data = new Course();
            string r="";
          
            try
            {
                if (String.IsNullOrEmpty(SelectedCourse))
                {
                    MessageBox.Show("Please Select a Course Work");
                }
                else
                {
                    data = scrapeDataFromURL(URL, SelectedCourse);
                    CurrentCourse = data;
                    //CourseData = data.Name + " | " + data.SemesterList.SelectMany(p => p.SubjectList.Select(s => s.Name + " | " + s.Units+" | "+ p.SemesterName+" | "+p.TotalUnits));

                    /*var r = from sem in data.SemesterList
                            from sub in sem.SubjectList
                            select new { data = sem.SemesterName+" | "+sem.TotalUnits+" | "+sub.Name+" | "+sub.Units};*/
                    

                    foreach (var sem in data.SemesterList)
                    {
                        foreach (var sub in sem.SubjectList)
                        {

                            if (ApplicationConstants.CourseTitles.Any(s => sub.Name.Contains(s)))
                            {
                                //checking for GE-B2 type of string in the string for general education courses
                                var temp = Regex.IsMatch(sub.Name, @"GE\-[A-Z]\d");
                                if (!temp)
                                {
                                    //if subject is not GE course then extract string as follows
                                    var code = Regex.Replace(sub.Name, @"[^0-9]+", "");
                                    sub.Title = ApplicationConstants.CourseTitles.Where(s => sub.Name.Contains(s)).FirstOrDefault() + code;
                                }
                                else
                                {
                                        var code = Regex.Replace(sub.Name, @"[^0-9]+", "").Substring(0, 3);
                                        sub.Title = ApplicationConstants.CourseTitles.Where(s => sub.Name.Contains(s)).FirstOrDefault() + code;
                                }
                                
                            }
                            
                                
                        }
                    }

                    addGEDataToRoadmap();

                    foreach (var sem in data.SemesterList)
                    {
                        foreach (var sub in sem.SubjectList)
                        {
                            r += sem.SemesterName + " | " + sem.TotalUnits + " | " + sub.Name + " | " + sub.Title + " | " + sub.Units + " \n ";
                        }
                    }

                    CourseData = data.Name + "\n" + " | " + r.ToString();

                    using (CsvReader csv = new CsvReader(File.OpenText(@"E:\New Job\schedule\CSULB Fall 12 schedule.csv")))
                    {
                        csv.Configuration.RegisterClassMap<ScheduleMap>();
                        IEnumerable<ScheduleReportItem> dataReport = csv.GetRecords<ScheduleReportItem>().ToList();
                        Schedule = dataReport.ToList();

                        CurrentSchedule = new Schedule() { SemID = 1, SemScheduleReport = Schedule };
                    }

                }
                
                //Schedule = CSVHelper.getDataFromScheduleCSV(@"C:\Users\Neha\Desktop\ScheduleFall2006.csv");
            }
            catch(Exception ex)
            {
                string s = ex.Message;
            }
        }


        private void addGEDataToRoadmap()
        {
            GEData = new List<GECourses>();
            GEData = GECourses.getGECoursesData();

            List<string> distinctGECourses = GECourses.getGECoursesName();

            for(int i=0;i<CurrentCourse.SemesterList.Count;i++)
            {
                for(int j=0;j<CurrentCourse.SemesterList[i].SubjectList.Count;j++)
                {
                    var sub = CurrentCourse.SemesterList[i].SubjectList[j];
                    var tempString = Regex.Match(sub.Name, @"GE\-[A-Z]\d");

                    if (distinctGECourses.Contains(tempString.ToString()))
                    {
                        var code = "GE";
                        List<GECourses> GESubs = GEData.Where(s => s.Category == tempString.ToString()).ToList();

                        foreach (var ge_sub in GESubs)
                        {
                            CurrentCourse.SemesterList[i].SubjectList.Add(new Subject() { Name = "GE", Title = ge_sub.Subject.Replace(" ",String.Empty) });
                        }

                        //string subs = String.Join(",",(object[]) GESubs.Select(s=>s.Subject).ToArray()).ToString();
                        // sub.Title = code;
                    }
                }
            }

           
        }

        private static Course scrapeDataFromURL(string URL,string name)
        {
            Course course = new Course(name);
            List<Semester> semList = new List<Semester>();
            try
            {
                var web = new HtmlWeb();
                var doc = web.Load(URL);

                var node = doc.DocumentNode.SelectSingleNode("//table/tbody");

                Console.WriteLine("Node Name:" + node.Name + "\n" + node.OuterHtml);

                List<List<string>> table = doc.DocumentNode.SelectSingleNode("//table")
                .Descendants("tr")
                .Skip(1)
                .Where(tr => tr.Elements("td").Count() > 1)
                .Select(tr => tr.Elements("td").Select(td => td.InnerText.Trim()).ToList())
                .ToList();

               
                int offsetSemester = 3;
                int offsetRow = 2;
                Semester sem1 = new Semester();
                Semester sem2 = new Semester();

                for (int i = 0; i < table.Count; i+=3)
                {
                    for (int j = i % 3; j < 3; j++)
                    {


                        if (j == 0)
                        {
                            sem1 = new Semester() { SemesterName = table[i][0] };
                            sem2 = new Semester() { SemesterName = table[i][2] };
                        }
                        else if (j == 1)
                        {
                            List<Subject> sem1Subjects = new List<Subject>();
                            List<Subject> sem2Subjects = new List<Subject>();


                            var indexes1 = new List<int> { 0, 1 };
                            var indexes2 = new List<int> { 2, 3 };

                            var listSubNamesOddSem = table.Select(x => indexes1.Select(r => x[r].Split('\n')).ToList()).ToList();
                            var listSubNamesEvenSem = table.Select(x => indexes2.Select(r => x[r].Split('\n')).ToList()).ToList();

                            var namesListSemOdd = listSubNamesOddSem[j][0].ToList().Select(x => x.Trim()).ToList();
                            var creditListSemOdd = listSubNamesOddSem[j][1].ToList().Select(x => x.Trim()).ToList();

                            var namesListSemEven = listSubNamesEvenSem[j][0].ToList();
                            var creditListSemEven = listSubNamesEvenSem[j][1].ToList();


                            for (int lv = 0; lv < namesListSemOdd.Count(); lv++)
                            {
                                sem1.SubjectList.Add(new Subject() { Name = namesListSemOdd[lv].Trim(), Units = Convert.ToInt32(creditListSemOdd[lv]) });
                            }

                            for (int lv1 = 0; lv1 < namesListSemEven.Count(); lv1++)
                            {
                                sem2.SubjectList.Add(new Subject() { Name = namesListSemEven[lv1].Trim(), Units = Convert.ToInt32(creditListSemEven[lv1]) });
                            }

                            sem1.TotalUnits = creditListSemOdd.Sum(x => Convert.ToInt32(x));

                            sem2.TotalUnits = creditListSemEven.Sum(x => Convert.ToInt32(x));

                            semList.Add(sem1);
                            semList.Add(sem2);


                        }
                    }

                }

                course.SemesterList = semList;

              
            }
            catch(Exception ex)
            {
                string error = ex.Message;
            }

            return course; 

        }

        ////public override void Cleanup()
        ////{
        ////    // Clean up if needed

        ////    base.Cleanup();
        ////}
    }
}