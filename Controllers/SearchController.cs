using LMS_Project_APIs.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace LMS_Project_APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SearchController : ControllerBase
    {
        private readonly LearningManagementSystemContext _context;

        public SearchController(LearningManagementSystemContext context)
        {
            _context = context;
        }

        [HttpGet("SearchIDP")]
        public async Task<IActionResult> SearchIDP(string searchValue)
        {
            try
            {
                var searchParam = new SqlParameter("@searchvalue", searchValue ?? (object)DBNull.Value);

                var searchResults = await _context.IDPSearchings
                    .FromSqlRaw("EXEC Search_IDP @searchvalue", searchParam)
                    .ToListAsync();

                return Ok(searchResults);

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return StatusCode(500, "An error occurred while fetching the course catalog.");
            }

        }

        [HttpGet("SearchTranscript")]
        public async Task<IActionResult> SearchTranscript(string searchValue)
        {
            try
            {
                var searchParam = new SqlParameter("@searchvalue", searchValue ?? (object)DBNull.Value);

                var searchResults = await _context.TranscriptSearchings
                    .FromSqlRaw("EXEC Search_TrainingTranscript @searchvalue", searchParam)
                    .ToListAsync();

                return Ok(searchResults);

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return StatusCode(500, "An error occurred while fetching the course catalog.");
            }

        }
    }
}
