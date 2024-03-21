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
    public class StudentDataController : ApiController
    {
        private SchoolDbContext School = new SchoolDbContext();

        [HttpGet]
        [Route("api/StudentData/ListStudents")]
        public IEnumerable<Student> ListStudents()
        {
            MySqlConnection Conn = School.AccessDatabase();
            Conn.Open();
            MySqlCommand cmd = Conn.CreateCommand();
            cmd.CommandText = "SELECT * FROM students";
            MySqlDataReader ResultSet = cmd.ExecuteReader();

            List<Student> Students = new List<Student> {};
            while (ResultSet.Read())
            {
                int StudentId = Convert.ToInt32(ResultSet["studentid"]);
                string StudentFullName = ResultSet["studentfname"].ToString() + " " + ResultSet["studentlname"].ToString();
                string StudentNumber = ResultSet["studentnumber"].ToString();
                DateTime StudentEnrolDate = (DateTime)ResultSet["enroldate"];
                
                Student NewStudent = new Student();
                NewStudent.StudentId = StudentId;
                NewStudent.StudentFullName = StudentFullName;
                NewStudent.StudentNumber = StudentNumber;
                NewStudent.StudentEnrolDate = StudentEnrolDate;

                //Add the Student Name to the List
                Students.Add(NewStudent);
            }

            //Close the connection between the MySQL Database and the WebServer
            Conn.Close();



            //Return the final list of Student names
            return Students;
        }

        [HttpGet]
        [Route("api/StudentData/FindStudent/{id}")]
        public Student FindStudent(int id)
        {
            Student NewStudent = new Student();

            MySqlConnection Conn = School.AccessDatabase();
            Conn.Open();
            MySqlCommand cmd = Conn.CreateCommand();
            cmd.CommandText = "SELECT * FROM students WHERE studentid = " + id.ToString() + ";";
            MySqlDataReader ResultSet = cmd.ExecuteReader();

            while (ResultSet.Read())
            {
                int StudentId = Convert.ToInt32(ResultSet["studentid"]);
                string StudentFullName = ResultSet["studentfname"].ToString() + " " + ResultSet["studentlname"].ToString();
                string StudentNumber = ResultSet["studentnumber"].ToString();
                DateTime StudentEnrolDate = (DateTime)ResultSet["enroldate"];

                NewStudent.StudentId = StudentId;
                NewStudent.StudentFullName = StudentFullName;
                NewStudent.StudentNumber = StudentNumber;
                NewStudent.StudentEnrolDate = StudentEnrolDate;
            }

            return NewStudent;
        }
    }
}
