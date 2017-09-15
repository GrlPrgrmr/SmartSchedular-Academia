using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectCSULB.Model
{
    public class Course
    {
        public Course(string name)
        {
            this.Name = name;
        }

        public Course()
        { }
        private List<Semester> semesterList;

        public List<Semester> SemesterList
        {
            get { return semesterList; }
            set { semesterList = value; }
        }

        private string _name;

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        private int _totalunits;

        public int TotalUnits
        {
            get { return _totalunits; }
            set { _totalunits = value; }
        }



    }
}
