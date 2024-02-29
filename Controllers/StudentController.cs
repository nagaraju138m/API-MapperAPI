using apiSample.Models;
using Microsoft.AspNetCore.Mvc;
using Repository.Dtos;
using Repository.Interfaces;

namespace apiSample.Controllers
{
    public class StudentController : Controller
    {
        public readonly iStudent iStudent;
        private readonly ILogger logger;

        public StudentController(iStudent iStudent)
        {
            this.iStudent = iStudent;
        }

        [HttpGet("getStudents")]
        public async Task<ActionResult<IEnumerable<Student>>> getstudents()
        {
            var retuslt = await iStudent.GetAllAsync();
            return Ok(retuslt);
        }

        [HttpPost("CreateStudent")]
        public async Task<ActionResult<Student>> addStudent([FromBody] Student student)
        {
            try
            {
                var students = await iStudent.CreateAsync(student);
                return Ok(students);
            }
            catch (Exception ex) { 
                //logger.LogInformation(ex.ToString());
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("updateStudent")]
        public async Task<ActionResult<Student>> updatestudent([FromBody] Student student)
        {
            try
            {
                await iStudent.UpdateAsync(student);
                return Ok(student);
            }
            catch (Exception ex)
            {
                logger.LogInformation(ex.ToString());
                return BadRequest(ex.ToString());
            }
        }

        [HttpPost("getByGroupid")]
        public async Task<ActionResult<IEnumerable<Student>>> getbyGroupId(int groupId)
        {
            var res = await iStudent.getBygroupid(groupId);
            if (res.Any())
            {
                return Ok(res);
            }
            return BadRequest("not found");
        }

        [HttpPost("getStudentInfo")]
        public async Task<ActionResult<StudentInfoDTO>> getStudentInfo(int id)
        {
            var res = await iStudent.getById(id);
            if (res !=null)
            {
                return Ok(res);
            }
            return BadRequest("not found");
        }

    }
}
