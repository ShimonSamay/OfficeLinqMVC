using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using OfficeLinqMVC.Models;

namespace OfficeLinqMVC.Controllers.api
{
    public class TeacherController : ApiController
    {
       
        static string connection = "Data Source=SHIMONSAMAY;Initial Catalog=CollegeDB;Integrated Security=True;Pooling=False";
        TeacherDBDataContext teacherDB = new TeacherDBDataContext(connection);
        public IHttpActionResult Get()
        {
            try
            {
                List<Teacher> teacherList = teacherDB.Teachers.ToList();
                return Ok(new { teacherList });
            }
            catch(SqlException sqlErr)
            {
                return BadRequest(sqlErr.Message);  
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

      
        public IHttpActionResult Get(int id)
        {
            try
            {
                Teacher someTeacher = teacherDB.Teachers.First(teacher => teacher.Id == id);
                return Ok(new { someTeacher });
            }
            catch (SqlException sqlErr)
            {
                return BadRequest(sqlErr.Message);
            }
            catch(Exception)
            {
                return NotFound();
            }
        }

        
        [HttpPost]
        public IHttpActionResult Post([FromBody] Teacher teacherValue)
        {
            try
            {
                teacherDB.Teachers.InsertOnSubmit(teacherValue);
                teacherDB.SubmitChanges();
                List<Teacher> teacherList = teacherDB.Teachers.ToList();
                return Ok(new { teacherList });
            }
            catch (SqlException sqlErr)
            {
                return BadRequest(sqlErr.Message);
            }
            catch (Exception x)
            {
                return BadRequest(x.Message);
            }
        }

      
        [HttpPut]
        public IHttpActionResult Put(int id, [FromBody] Teacher teachrValue)
        {
            try
            {
                Teacher someTeacher = teacherDB.Teachers.First(teacher => teacher.Id==id);
                someTeacher = teachrValue;
                return Ok(new { someTeacher });
            }
            catch (SqlException sqlErr)
            {
                return BadRequest (sqlErr.Message); 
            }
            catch (Exception x)
            {
                return BadRequest(x.Message);
            }
        }

        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            try {
                Teacher someTeacher = teacherDB.Teachers.First(teacher => teacher.Id == id);
                teacherDB.Teachers.DeleteOnSubmit(someTeacher);
                teacherDB.SubmitChanges();
                List<Teacher> teacherList = teacherDB.Teachers.ToList();
                return Ok(new { teacherList });
            }
            catch (SqlException x)
            {
                return BadRequest(x.Message);
            }
            catch (Exception x)
            {
                return BadRequest(x.Message);
            }
        } 
    }
}
