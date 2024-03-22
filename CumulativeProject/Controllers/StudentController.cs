using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CumulativeProject.Models;

namespace CumulativeProject.Controllers
{
    public class StudentController : Controller
    {
        // GET: Student
        public ActionResult Index()
        {
            return View();
        }

        // GET: Student/List
        /// <summary>
        /// This method directs to a page that displays all the students
        /// </summary>
        /// <returns>All students</returns>
        public ActionResult List()
        {
            StudentDataController controller = new StudentDataController();
            IEnumerable<Student> Students = controller.ListStudents();
            return View(Students);
        }

        // GET: Student/Show/{id}
        /// <summary>
        /// This method will show a view of an individual student,given the student id.
        /// </summary>
        /// <param name="id">student id</param>
        /// <returns>a singular student</returns>
        public ActionResult Show(int id)
        {
            StudentDataController controller = new StudentDataController();
            Student selectedStudent = controller.FindStudent(id);
            return View(selectedStudent);
        }
    }
}