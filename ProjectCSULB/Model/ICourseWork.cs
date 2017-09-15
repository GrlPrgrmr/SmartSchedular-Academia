using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectCSULB.Model
{
    public interface ICourseWork
    {
        List<Subject> getSubjectsList(string courseName);
    }
}
