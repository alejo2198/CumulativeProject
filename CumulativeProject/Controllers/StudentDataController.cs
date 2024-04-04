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
        //create a database context
        private SchoolDbContext School = new SchoolDbContext();

        /// <summary>
        /// This method is responsible for retreiving all student data from the database and converting them into a list of Student instances
        /// </summary>
        /// <returns>IEnumerabl<Student></returns>
        /// <example>>GET api/StudentData/ListStudents -> {Student Object, Student Object, Student Object...}</example>
        [HttpGet]
        [Route("api/StudentData/ListStudents")]
        public IEnumerable<Student> ListStudents()
        {
            //Access database and send a query for all students
            MySqlConnection Conn = School.AccessDatabase();
            Conn.Open();
            MySqlCommand cmd = Conn.CreateCommand();
            cmd.CommandText = "SELECT * FROM students";
            MySqlDataReader ResultSet = cmd.ExecuteReader();

            List<Student> Students = new List<Student> {};
            while (ResultSet.Read())
            {
                int StudentId = Convert.ToInt32(ResultSet["studentid"]);
                string StudentFirstName = ResultSet["studentfname"].ToString();
                string StudentLastName = ResultSet["studentlname"].ToString();
                string StudentNumber = ResultSet["studentnumber"].ToString();
                DateTime StudentEnrolDate = (DateTime)ResultSet["enroldate"];
                
                Student NewStudent = new Student();
                NewStudent.StudentId = StudentId;
                NewStudent.StudentFirstName = StudentFirstName;
                NewStudent.StudentLastName = StudentLastName;
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
        /// <summary>
        /// This method returns a singular student data from a database and converts it into a Student instance
        /// </summary>
        /// <param name="id">student id</param>
        /// <returns>Student instance</returns>
        /// <example>>GET api/StudentData/ListStudents -> 
        ///     public int StudentId;
        ///     public string StudentFirstName;
        ///     public string StudentLastName;
        ///     public string StudentNumber;
        ///     public DateTime StudentEnrolDate;
        /// </example>

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
                string StudentFirstName = ResultSet["studentfname"].ToString();
                string StudentLastName = ResultSet["studentlname"].ToString();
                string StudentNumber = ResultSet["studentnumber"].ToString();
                DateTime StudentEnrolDate = (DateTime)ResultSet["enroldate"];

                NewStudent.StudentId = StudentId;
                NewStudent.StudentFirstName = StudentFirstName;
                NewStudent.StudentLastName = StudentLastName;
                NewStudent.StudentNumber = StudentNumber;
                NewStudent.StudentEnrolDate = StudentEnrolDate;
            }

            return NewStudent;
        }
    }
}
