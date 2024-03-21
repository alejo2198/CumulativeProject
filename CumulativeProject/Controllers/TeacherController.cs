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
        public ActionResult List()
        {
            TeacherDataController controller = new TeacherDataController();
            IEnumerable<Teacher> Teachers = controller.ListTeachers();
            return View(Teachers);
        }

        // GET: Teacher/Show/{id}
        public ActionResult Show(int id)
        {
            TeacherDataController controller = new TeacherDataController();
            Teacher selectedTeacher = controller.FindTeacher(id);

            ClassDataController classcontroller = new ClassDataController();
            IEnumerable<Class> classesByTeacher = classcontroller.ListClassesByTeacher(selectedTeacher.TeacherId);

            TeacherClassesViewData teacherClassData = new TeacherClassesViewData();
            teacherClassData.teacher = selectedTeacher;
            teacherClassData.classes = classesByTeacher;
            
            return View(teacherClassData);
        }
    }
}