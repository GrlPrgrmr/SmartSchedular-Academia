using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectCSULB.Model
{
    public class ConflictData
    {
        private string subjectName;

        public string SubjectName
        {
            get { return subjectName; }
            set { subjectName = value; }
        }

        private int totalCapacity;

        public int TotalCapacity
        {
            get { return totalCapacity; }
            set { totalCapacity = value; }
        }

        private int conflictingSectionCapacity;

        public int ConflictingSectionCapacity
        {
            get { return conflictingSectionCapacity; }
            set { conflictingSectionCapacity = value; }
        }

       
        private int demand;

        public int Demand
        {
            get { return demand; }
            set { demand = value; }
        }


    }
}
