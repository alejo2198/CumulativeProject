using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CumulativeProject.Models;

namespace CumulativeProject.ViewModels
{
    public class TeacherClassesViewData
    {
        public Teacher teacher;
        public IEnumerable<Class> classes;
    }
}