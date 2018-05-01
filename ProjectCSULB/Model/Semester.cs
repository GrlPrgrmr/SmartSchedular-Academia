using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectCSULB.Model
{
    public class Semester
    {
        public Semester()
        {
            SubjectList = new List<Subject>();
        }
        private int _semId;

        public int SemId
        {
            get { return _semId; }
            set { _semId = value; }
        }

        private string semestername;

        public string SemesterName
        {
            get { return semestername; }
            set { semestername = value; }
        }


        private int _totalUnits;

        public int TotalUnits
        {
            get { return _totalUnits; }
            set { _totalUnits = value; }
        }

        private List<Subject> _subjectList;

        public List<Subject> SubjectList
        {
            get
            {
                return _subjectList;
            }
            set { _subjectList = value; }
        }

        private int batchSize;

        public int BatchSize
        {
            get { return batchSize; }
            set { batchSize = value; }
        }

        private int avgStudentsEffected;

        public int AvgStudentEffected
        {
            get { return avgStudentsEffected; }
            set { avgStudentsEffected = value; }
        }



    }
}
