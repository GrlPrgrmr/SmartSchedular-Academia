using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectCSULB.Model
{
    public class Subject
    {
        private string title;

        public string Title
        {
            get { return title; }
            set { title = value; }
        }

        private string _name;

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        private int _units;

        public int Units
        {
            get { return _units; }
            set { _units = value; }
        }


    }
}
