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
        private SchoolDbContext School = new SchoolDbContext();

        [HttpGet]
        [Route("api/ClassData/ListClasses")]
        public IEnumerable<Class> ListClasses()
        {
            MySqlConnection Conn = School.AccessDatabase();
            Conn.Open();
            MySqlCommand cmd = Conn.CreateCommand();
            cmd.CommandText = "SELECT * FROM classes";
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
