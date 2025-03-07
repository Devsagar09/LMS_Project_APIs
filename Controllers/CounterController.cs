using LMS_Project_APIs.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace LMS_Project_APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CounterController : ControllerBase
    {

        private readonly LearningManagementSystemContext _context;

        public CounterController(LearningManagementSystemContext context)
        {
            _context = context;
        }

        [HttpGet("GetEnrollmentCount/{studentId}")]
        public async Task<IActionResult> GetEnrollmentCount(int studentId)
        {
            try
            {
                var countParam = new SqlParameter
                {
                    ParameterName = "@Count",
                    SqlDbType = System.Data.SqlDbType.Int,
                    Direction = System.Data.ParameterDirection.Output
                };

                var studentIdParam = new SqlParameter("@StudentId", studentId);

                await _context.Database.ExecuteSqlRawAsync("EXEC getCountOfEnroll @StudentId, @Count OUTPUT",
                    studentIdParam, countParam);

                int count = (int)countParam.Value;

                return Ok(new { count });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }


    }
}
