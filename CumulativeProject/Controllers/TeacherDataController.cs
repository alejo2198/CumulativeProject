using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CumulativeProject.Models;
using MySql.Data.MySqlClient;
using System.Diagnostics;

namespace CumulativeProject.Controllers
{
    public class TeacherDataController : ApiController
    {
        private SchoolDbContext School = new SchoolDbContext();


        /// <summary>
        /// This method is responsible for retreiving all teacher data from the database and converting them into a list of Teacher instances
        /// </summary>
        /// <returns>IEnumerabl<Teacher></returns>
        /// <example>>GET api/TeacherData/ListTeachers -> {Teacher Object, Teacher Object, Teacher Object...}</example>
        [HttpGet]
        [Route("api/TeacherData/ListTeachers")]     
        public IEnumerable<Teacher> ListTeachers()
        {
            MySqlConnection Conn = School.AccessDatabase();
            Conn.Open();
            MySqlCommand cmd = Conn.CreateCommand();
            cmd.CommandText = "SELECT * FROM teachers";
            MySqlDataReader ResultSet = cmd.ExecuteReader();

            List<Teacher> Teachers = new List<Teacher>{};
            while (ResultSet.Read())
            {
                int teacherId = Convert.ToInt32(ResultSet["teacherid"]);
                string teacherFullName = ResultSet["teacherfname"].ToString() + " " + ResultSet["teacherlname"].ToString();
                string teacherEmployeeNumber = ResultSet["employeenumber"].ToString();
                DateTime teacherHireDate = (DateTime)ResultSet["hiredate"];
                decimal teacherHourly = Convert.ToDecimal(ResultSet["salary"]);     
                  

                Teacher NewTeacher = new Teacher();
                NewTeacher.TeacherId = teacherId;
                NewTeacher.TeacherFullName = teacherFullName;
                NewTeacher.TeacherEmployeeNumber = teacherEmployeeNumber;
                NewTeacher.TeacherHireDate = teacherHireDate;
                NewTeacher.TeacherHourlyWage = teacherHourly;

                //Add the Teacher Name to the List
                Teachers.Add(NewTeacher);
            }
           
                //Close the connection between the MySQL Database and the WebServer
                Conn.Close();

            

            //Return the final list of teacher names
            return Teachers;
        }

        /// <summary>
        /// This method returns a singular teacher data from a database and converts it into a Teacher instance
        /// </summary>
        /// <param name="id">teacher id</param>
        /// <returns>Teacher instance</returns>
        /// <example>>GET api/TeacherData/ListTeachers -> {Teacher Object}</example>
        [HttpGet]
        [Route("api/TeacherData/FindTeacher/{id}")]
        public Teacher FindTeacher(int id)
        {
            Teacher NewTeacher = new Teacher();

            MySqlConnection Conn = School.AccessDatabase();
            Conn.Open();
            MySqlCommand cmd = Conn.CreateCommand();
            cmd.CommandText = "SELECT * FROM teachers WHERE teacherid = " + id.ToString() + ";";
            MySqlDataReader ResultSet = cmd.ExecuteReader();

            while (ResultSet.Read())
            {
                int teacherId = Convert.ToInt32(ResultSet["teacherid"]);
                string teacherFullName = ResultSet["teacherfname"].ToString() + " " + ResultSet["teacherlname"].ToString();
                string teacherEmployeeNumber = ResultSet["employeenumber"].ToString();
                DateTime teacherHireDate = (DateTime)ResultSet["hiredate"];
                decimal teacherHourly = Convert.ToDecimal(ResultSet["salary"]);

                NewTeacher.TeacherId = teacherId;
                NewTeacher.TeacherFullName = teacherFullName;
                NewTeacher.TeacherEmployeeNumber = teacherEmployeeNumber;
                NewTeacher.TeacherHireDate = teacherHireDate;
                NewTeacher.TeacherHourlyWage = teacherHourly;
            }

            return NewTeacher;
        }
    }
}
