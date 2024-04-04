using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CumulativeProject.Models;
using CumulativeProject.ViewModels;
using System.Diagnostics;

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

        //GET : /Teacher/New
        public ActionResult New()
        {
            return View();
        }

        //POST : /Teacher/Create
        [HttpPost]
        public ActionResult Create(string TeacherFname,string TeacherLname,string TeacherEmployeeNumber,DateTime TeacherHireDate,Decimal TeacherHourlyWage) 
        {
            //Create new teacher with form inputs
            Teacher NewTeacher = new Teacher();
            NewTeacher.TeacherFirstName = TeacherFname;
            NewTeacher.TeacherLastName = TeacherLname;
            NewTeacher.TeacherEmployeeNumber = TeacherEmployeeNumber;
            NewTeacher.TeacherHireDate = TeacherHireDate;
            NewTeacher.TeacherHourlyWage = TeacherHourlyWage;

            TeacherDataController controller = new TeacherDataController();
            controller.AddTeacher(NewTeacher);

            return RedirectToAction("List");
        }

        // GET: Teacher/DeleteConfirm/{id}
        /// <summary>
        /// This method directs to a page that displays the delete confirm page of a single teacher
        /// </summary>
        /// <returns>delete confirm page</returns>
        public ActionResult DeleteConfirm(int id)
        {
            //Grab teacher data
            TeacherDataController controller = new TeacherDataController();
            Teacher selectedTeacher = controller.FindTeacher(id);
            
            return View(selectedTeacher);
        }

        //POST " TEacher/Delete/id
        public ActionResult Delete(int id)
        {
            TeacherDataController controller = new TeacherDataController();
            controller.DeleteTeacher(id);

            return RedirectToAction("List");
        }
    }
}