using LMS_Project_APIs.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Plugins;

namespace LMS_Project_APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly LearningManagementSystemContext _context;

        public StudentController(LearningManagementSystemContext context)
        {
            _context = context;
        }

        [HttpGet("GetStudents")]
        public async Task<ActionResult<IEnumerable<TblStudent>>> GetStudents()
        {
            var students = await _context.TblStudents.FromSqlRaw("EXEC display_Student").ToListAsync();
            return Ok(students);
        }


        [HttpPost("AddStudent")]
        public async Task<ActionResult> AddStudent([FromBody] TblStudent tbstud)
        {
            try
            {
                await _context.Database.ExecuteSqlRawAsync("EXEC add_edit_Student @p0, @p1, @p2, @p3, @p4, @p5, @p6, @p7, @p8, @p9, @p10, @p11, @p12,@p13, @p14, @p15, @p16",

                    tbstud.StudentId == 0 ? null : tbstud.StudentId,
                    tbstud.StudentNo,
                    tbstud.Firstname,
                    tbstud.Middlename,
                    tbstud.Lastname,
                    tbstud.Username,
                    tbstud.Password,
                    tbstud.Email,
                    tbstud.RoleId,
                    tbstud.ProfileImage,
                    tbstud.ArchiveDate,
                    tbstud.PhoneNo,
                    tbstud.Address,
                    tbstud.City,
                    tbstud.PostalCode,
                    tbstud.State,
                    tbstud.Country
                  );
                return Ok(new { Message = "Student Added/Updated." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An error : ", Error = ex.Message });
            }
        }


        [HttpDelete]
        [Route("DeleteStudents")]
        public async Task<ActionResult<IEnumerable<TblStudent>>> DeleteStudents([FromBody] List<int> studentIds)
        {
            if(studentIds == null || studentIds.Count == 0)
            {
                return BadRequest(new { Message = "No studentIds provide for deletion." });
            }

            try
            {
                string studentIdStr = string.Join(",", studentIds);
                await _context.Database.ExecuteSqlRawAsync("EXEC delete_Student @p0", studentIdStr);
                return Ok(new { Message = "Students deleted successfully." });
            }
            catch(Exception ex)
            {
                return StatusCode(500, new { Message = "an error : ", Error = ex.Message });
            }
        }
    }
}
