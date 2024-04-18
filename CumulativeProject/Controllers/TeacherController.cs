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

        //GET : "Teacher/Update/id
        /// <summary>
        /// Get the teacher using the id to fill up the update form
        /// </summary>
        /// <param name="id">teacher id</param>
        /// <returns>A dynamic pages with the update form full of inputs that contain the teacher values.</returns>
        public ActionResult Update(int id)
        {
            TeacherDataController controller = new TeacherDataController();
            Teacher selectedTeacher = controller.FindTeacher(id);

            return View(selectedTeacher);
        }

        //POST " TEacher/Update/id
        /// <summary>
        ///     receives a POST request containing information about exsitsting teaher. Will have the updated values. Send information to API and redirect to the SHow author page
        ///     using the teacher id.
        /// </summary>
        /// <param name="id">Id of teacher</param>
        /// <param name="TeacherFname">updated teacher first name</param>
        /// <param name="TeacherLname">updated teacher last name</param>
        /// <param name="TeacherEmployeeNumber">updated teacher employee number</param>
        /// <param name="TeacherHireDate">updated teacher hire date</param>
        /// <param name="TeacherHourlyWage">updated teacher hourly wage</param>
        /// <returns>A dynamic webpage showing the updated teacher</returns>
        /// <example>
        /// POST: /Teacher/Update/4
        /// POST DATA
        /// {
        /// "TeacherFname":"alejandro",
        /// "TeacherFname":"castro",
        /// "TeacherEmployeeNumber":" T435";
        /// "TeacherHireDate": "06/06/2014 12:00:00 AM",
        /// "TeacherHourlyWage":"24.56";
        /// }
        /// </example>
        [HttpPost]
        public ActionResult Update(int id,string TeacherFname, string TeacherLname, string TeacherEmployeeNumber, DateTime TeacherHireDate, Decimal TeacherHourlyWage)
        {
            //Create new teacher with form inputs
            Teacher TeacherInfo = new Teacher();
            TeacherInfo.TeacherFirstName = TeacherFname;
            TeacherInfo.TeacherLastName = TeacherLname;
            TeacherInfo.TeacherEmployeeNumber = TeacherEmployeeNumber;
            TeacherInfo.TeacherHireDate = TeacherHireDate;
            TeacherInfo.TeacherHourlyWage = TeacherHourlyWage;

            TeacherDataController controller = new TeacherDataController();
            controller.UpdateTeacher(id,TeacherInfo);
         

            return RedirectToAction("Show/"+ id);
        }
    }
}