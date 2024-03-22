using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CumulativeProject.Models;
using MySql.Data.MySqlClient;

namespace CumulativeProject.Controllers
{
    
    public class ClassDataController : ApiController
    {
        //create a db context
        private SchoolDbContext School = new SchoolDbContext();
        /// <summary>
        /// This method accesses a database and queries the database to get all classes
        /// </summary>
        /// <returns>a list of class Objects</returns>
        /// <example>GET api/ClassData/ListClasses -> {Class Object, Class Object, Class Object...}</example>
        [HttpGet]
        [Route("api/ClassData/ListClasses")]
        public IEnumerable<Class> ListClasses()
        {
            //opens the database connection
            MySqlConnection Conn = School.AccessDatabase();
            Conn.Open();
            //creates the command to get all classes
            MySqlCommand cmd = Conn.CreateCommand();
            cmd.CommandText = "SELECT * FROM classes";
            MySqlDataReader ResultSet = cmd.ExecuteReader();

            List<Class> Classes = new List<Class> { };

            //loops through every class to create a class instance with the Resuklt set
            while (ResultSet.Read())
            {
                int ClassId = Convert.ToInt32(ResultSet["classid"]);
                string ClassCode = ResultSet["classcode"].ToString();
                string ClassName = ResultSet["classname"].ToString();
                DateTime ClassStartDate = (DateTime)ResultSet["startdate"];
                DateTime ClassFinishDate = (DateTime)ResultSet["finishdate"];
                int TeacherId = Convert.ToInt32(ResultSet["teacherid"]);

                Class NewClass = new Class();
                NewClass.ClassId = ClassId;
                NewClass.ClassCode = ClassCode;
                NewClass.ClassName = ClassName;
                NewClass.ClassStartDate = ClassStartDate;
                NewClass.ClassFinishDate = ClassFinishDate;
                NewClass.TeacherId = TeacherId;

                //Add the Class Name to the List
                Classes.Add(NewClass);
            }

            //Close the connection between the MySQL Database and the WebServer
            Conn.Close();



            //Return the final list of Class names
            return Classes;
        }

        /// <summary>
        /// This method will take a class id and return a class object
        /// </summary>
        /// <param name="id">classid neccessary to retreive singular class</param>
        /// <returns>{Class object}</returns>
        /// <example>GET api/CLassData/FindClass -> {Class Object}</example>
        [HttpGet]
        [Route("api/ClassData/FindClass/{id}")]
        public Class FindClass(int id)
        {
            Class NewClass = new Class();

            MySqlConnection Conn = School.AccessDatabase();
            Conn.Open();
            MySqlCommand cmd = Conn.CreateCommand();
            cmd.CommandText = "SELECT * FROM classes WHERE classid = " + id.ToString() + ";";
            MySqlDataReader ResultSet = cmd.ExecuteReader();

            while (ResultSet.Read())
            {
                int ClassId = Convert.ToInt32(ResultSet["classid"]);
                string ClassCode = ResultSet["classcode"].ToString();
                string ClassName = ResultSet["classname"].ToString();
                DateTime ClassStartDate = (DateTime)ResultSet["startdate"];
                DateTime ClassFinishDate = (DateTime)ResultSet["finishdate"];
                int TeacherId = Convert.ToInt32(ResultSet["teacherid"]);

                NewClass.ClassId = ClassId;
                NewClass.ClassCode = ClassCode;
                NewClass.ClassName = ClassName;
                NewClass.ClassStartDate = ClassStartDate;
                NewClass.ClassFinishDate = ClassFinishDate;
                NewClass.TeacherId = TeacherId;
            }

            return NewClass;
        }

        /// <summary>
        /// This method will return the classes taught by a specific teacher
        /// </summary>
        /// <param name="id">teacherid necessary to query the classes by teacher id</param>
        /// <returns>{Class Object,Class Object, ...}</returns>
        [HttpGet]
        [Route("api/ClassData/ListClassesByTeacher/{id}")]
        public IEnumerable<Class> ListClassesByTeacher(int id)
        {
            MySqlConnection Conn = School.AccessDatabase();
            Conn.Open();
            MySqlCommand cmd = Conn.CreateCommand();
            cmd.CommandText = "SELECT * FROM classes WHERE teacherid =" + id.ToString() + ";"; ;
            MySqlDataReader ResultSet = cmd.ExecuteReader();

            List<Class> Classes = new List<Class> { };
            while (ResultSet.Read())
            {
                int ClassId = Convert.ToInt32(ResultSet["classid"]);
                string ClassCode = ResultSet["classcode"].ToString();
                string ClassName = ResultSet["classname"].ToString();
                DateTime ClassStartDate = (DateTime)ResultSet["startdate"];
                DateTime ClassFinishDate = (DateTime)ResultSet["finishdate"];
                int TeacherId = Convert.ToInt32(ResultSet["teacherid"]);

                Class NewClass = new Class();
                NewClass.ClassId = ClassId;
                NewClass.ClassCode = ClassCode;
                NewClass.ClassName = ClassName;
                NewClass.ClassStartDate = ClassStartDate;
                NewClass.ClassFinishDate = ClassFinishDate;
                NewClass.TeacherId = TeacherId;

                //Add the Class Name to the List
                Classes.Add(NewClass);
            }

            //Close the connection between the MySQL Database and the WebServer
            Conn.Close();



            //Return the final list of Class names
            return Classes;
        }
    }
}
