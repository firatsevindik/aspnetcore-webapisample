using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using University.Controllers.v1;
using University.Models;

namespace University.Controllers
{
    public class StudentsController : BaseV1Controller
    {
        int page = 1;
        int itemCount = 50;

        // GET v1/Students?page=1&count=600
        /// <summary>
        /// List students
        /// </summary>
        /// <param name="page">Page number</param>
        /// <param name="count">Total Item Count</param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(ApiPagedReturn<IEnumerable<StudentModel>>))]
        [ProducesResponseType(400, Type = typeof(ApiPagedReturn<int>))]
        public ActionResult<ApiPagedReturn<IEnumerable<StudentModel>>> Get([FromQuery] int page, [FromQuery] int count)
        {
            if(count > 50)
            {
                count = 50;
            }

            var totalItemCount = Convert.ToDouble(Students.Count);
            var convertedCount = Convert.ToDouble(count);
            var totalPage = Math.Ceiling(totalItemCount / convertedCount);
            var pagedData = Students.Skip(page * count - count).Take(count).ToList();

            var returnData = new ApiPagedReturn<IEnumerable<StudentModel>>
            {
                Code = 200,
                Success = true,
                Message = "Students listed successfully.",
                Data = pagedData,
                TotalItemCount = (int)totalItemCount,
                TotalPage = (int)totalPage,
                CurrentPage = page
            };

            return returnData;
        }

        // GET v1/Students/5
        [HttpGet("{id}")]
        public ActionResult<ApiReturn<StudentModel>> Get(int id)
        {
            var student = Students.FirstOrDefault(a => a.Id == id);
            ApiReturn<StudentModel> returnData;

            if (student == null)
            {
                returnData = new ApiReturn<StudentModel>
                {
                    Code = 404,
                    Success = false,
                    Message = "Student not found!",
                    Data = null
                };

                return NotFound(returnData);
            }

            returnData = new ApiReturn<StudentModel>
            {
                Code = 200,
                Success = true,
                Message = "Student found successfully.",
                Data = student
            };
            return StatusCode(201, returnData);
            return returnData;
        }

        // POST v1/Students
        [HttpPost]
        public void Post([FromBody] StudentModel value)
        {
            Students.Add(value);
        }

        // PUT v1/Students/5
        [HttpPut("{id}")]
        public ActionResult<bool> Put(int id, [FromBody] StudentModel value)
        {
            var student = Students.FirstOrDefault(a => a.Id == id);

            if (student == null)
            {
                return NotFound();
            }

            Students.Remove(student);
            Students.Add(value);

            return true;
        }

        // DELETE v1/Students/5
        [HttpDelete("{id}")]
        public ActionResult<bool> Delete(int id)
        {
            var student = Students.FirstOrDefault(a => a.Id == id);

            if (student == null)
            {
                return NotFound();
            }

            Students.Remove(student);

            return true;
        }
		
		// GET v1/Students/filtered
		[HttpGet("filtered")]
        public ActionResult<List<StudentModel>> FilteredStudents()
        {
            var students = Students.Where(p => p.Age >= 18 && p.Age <= 40).ToList();

            if (students == null)
            {
                return NotFound();
            }

            return students;
        }
    }
}
