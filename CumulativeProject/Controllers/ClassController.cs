using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CumulativeProject.Models;

namespace CumulativeProject.Controllers
{
    public class ClassController : Controller
    {
        // GET: Class
        public ActionResult Index()
        {
            return View();
        }

        // GET: Class/List
        /// <summary>
        /// This method directs the page to a view that displays all classes
        /// </summary>
        /// <returns>all classes</returns>
        public ActionResult List()
        {
            ClassDataController controller = new ClassDataController();
            IEnumerable<Class> Classes = controller.ListClasses();
            return View(Classes);
        }
        // GET: Class/Show/{id}
        /// <summary>
        /// This method will show a view of an individual class, given the class id
        /// </summary>
        /// <param name="id">class id</param>
        /// <returns>details on an individual class</returns>
        public ActionResult Show(int id)
        {
            ClassDataController controller = new ClassDataController();
            Class selectedClass = controller.FindClass(id);
            return View(selectedClass);
        }
    }
}