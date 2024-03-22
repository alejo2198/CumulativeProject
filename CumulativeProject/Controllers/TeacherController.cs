using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CumulativeProject.Models;
using CumulativeProject.ViewModels;

namespace CumulativeProject.Controllers
{
    public class TeacherController : Controller
    {
        // GET: Teacher
     
        public ActionResult Index()
        {
            return View();
        }

        // GET: Teacher/List
        /// <summary>
        /// This method directs to a page that displays all the teachers
        /// </summary>
        /// <returns>All Teachers</returns>
        public ActionResult List()
        {
            TeacherDataController controller = new TeacherDataController();
            IEnumerable<Teacher> Teachers = controller.ListTeachers();
            return View(Teachers);
        }

        // GET: Teacher/Show/{id}
        /// <summary>
        /// This method directs to a page that displays a single teacher
        /// </summary>
        /// <returns>one teacher</returns>
        public ActionResult Show(int id)
        {
            //Grab teacher data
            TeacherDataController controller = new TeacherDataController();
            Teacher selectedTeacher = controller.FindTeacher(id);
            //Grab class data from teacher
            ClassDataController classcontroller = new ClassDataController();
            IEnumerable<Class> classesByTeacher = classcontroller.ListClassesByTeacher(selectedTeacher.TeacherId);
            //create TeacherClasses View Data ViewModel;
            TeacherClassesViewData teacherClassData = new TeacherClassesViewData();
            teacherClassData.teacher = selectedTeacher;
            teacherClassData.classes = classesByTeacher;
            //Send the ViewModel to the view
            return View(teacherClassData);
        }
    }
}