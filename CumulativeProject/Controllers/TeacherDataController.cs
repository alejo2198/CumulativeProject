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

            List<Teacher> Teachers = new List<Teacher> { };
            while (ResultSet.Read())
            {
                int teacherId = Convert.ToInt32(ResultSet["teacherid"]);
                string teacherFirstName = ResultSet["teacherfname"].ToString();
                string teacherLastName = ResultSet["teacherlname"].ToString();
                string teacherEmployeeNumber = ResultSet["employeenumber"].ToString();
                DateTime teacherHireDate = (DateTime)ResultSet["hiredate"];
                decimal teacherHourly = Convert.ToDecimal(ResultSet["salary"]);


                Teacher NewTeacher = new Teacher();
                NewTeacher.TeacherId = teacherId;
                NewTeacher.TeacherFirstName = teacherFirstName;
                NewTeacher.TeacherLastName = teacherLastName;
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
        /// <example>>GET api/TeacherData/ListTeachers ->
        ///  {
        ///     public int TeacherId;
        ///     public string TeacherFirstName;
        ///     public string TeacherLastName;
        ///     public string TeacherEmployeeNumber;
        ///     public DateTime TeacherHireDate;
        ///     public decimal TeacherHourlyWage;
        ///  }
        ///</example>
        ///
        [HttpGet]
        [Route("api/TeacherData/FindTeacher/{id}")]
        public Teacher FindTeacher(int id)
        {
            Teacher NewTeacher = new Teacher();

            MySqlConnection Conn = School.AccessDatabase();
            Conn.Open();
            MySqlCommand cmd = Conn.CreateCommand();
            cmd.CommandText = "SELECT * FROM teachers WHERE teacherid = @id";
            cmd.Parameters.AddWithValue("@id", id.ToString());
            MySqlDataReader ResultSet = cmd.ExecuteReader();

            while (ResultSet.Read())
            {
                int teacherId = Convert.ToInt32(ResultSet["teacherid"]);
                string teacherFirstName = ResultSet["teacherfname"].ToString();
                string teacherLastName = ResultSet["teacherlname"].ToString();
                string teacherEmployeeNumber = ResultSet["employeenumber"].ToString();
                DateTime teacherHireDate = (DateTime)ResultSet["hiredate"];
                decimal teacherHourly = Convert.ToDecimal(ResultSet["salary"]);

                NewTeacher.TeacherId = teacherId;
                NewTeacher.TeacherFirstName = teacherFirstName;
                NewTeacher.TeacherLastName = teacherLastName;
                NewTeacher.TeacherEmployeeNumber = teacherEmployeeNumber;
                NewTeacher.TeacherHireDate = teacherHireDate;
                NewTeacher.TeacherHourlyWage = teacherHourly;
            }

            return NewTeacher;
        }
        [HttpPost]
        public void AddTeacher(Teacher NewTeacher)
        {
            //Create a database connection
            MySqlConnection Conn = School.AccessDatabase();

            //open connection between web server and databse
            Conn.Open();

            //Establish a new comand
            MySqlCommand cmd = Conn.CreateCommand();

            cmd.CommandText = "insert into teachers (teacherfname,teacherlname,employeenumber,hiredate,salary) " +
                "values (@TeacherFname,@TeacherLname,@TeacherEmployeeNumber,@TeacherHireDate,@TeacherHourlyWage)";
            cmd.Parameters.AddWithValue("@TeacherFname", NewTeacher.TeacherFirstName);
            cmd.Parameters.AddWithValue("@TeacherLname", NewTeacher.TeacherLastName);
            cmd.Parameters.AddWithValue("@TeacherEmployeeNumber", NewTeacher.TeacherEmployeeNumber);
            cmd.Parameters.AddWithValue("@TeacherHireDate", NewTeacher.TeacherHireDate);
            cmd.Parameters.AddWithValue("@TeacherHourlyWage", NewTeacher.TeacherHourlyWage);
            cmd.Prepare();

            cmd.ExecuteNonQuery();
            Conn.Close();
        }

        [HttpPost]
        public void DeleteTeacher(int id)
        {
            //Create a database connection
            MySqlConnection Conn = School.AccessDatabase();

            //open connection between web server and databse
            Conn.Open();

            //Establish a new comand
            MySqlCommand cmd = Conn.CreateCommand();

            cmd.CommandText = "delete from teachers where teacherid = @id";
            cmd.Parameters.AddWithValue("@id", id);
          
            cmd.Prepare();
            cmd.ExecuteNonQuery();
            Conn.Close();
        }

        public void UpdateTeacher(int id,[FromBody] Teacher TeacherInfo)
        {
            //Create a database connection
            MySqlConnection Conn = School.AccessDatabase();

            //open connection between web server and databse
            Conn.Open();

            //Establish a new comand
            MySqlCommand cmd = Conn.CreateCommand();

            cmd.CommandText = "update  teachers set  teacherfname=@TeacherFname,teacherlname=@TeacherLname,employeenumber=@TeacherEmployeeNumber," +
                "hiredate=@TeacherHireDate,salary=@TeacherHourlyWage where teacherid=@TeacherId";
            cmd.Parameters.AddWithValue("@TeacherFname", TeacherInfo.TeacherFirstName);
            cmd.Parameters.AddWithValue("@TeacherLname", TeacherInfo.TeacherLastName);
            cmd.Parameters.AddWithValue("@TeacherEmployeeNumber", TeacherInfo.TeacherEmployeeNumber);
            cmd.Parameters.AddWithValue("@TeacherHireDate", TeacherInfo.TeacherHireDate);
            cmd.Parameters.AddWithValue("@TeacherHourlyWage", TeacherInfo.TeacherHourlyWage);
            cmd.Parameters.AddWithValue("@TeacherId", id);
            cmd.Prepare();

            cmd.ExecuteNonQuery();
            Conn.Close();

        }



    }
}
