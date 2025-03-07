using LMS_Project_APIs.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using Microsoft.EntityFrameworkCore;

namespace LMS_Project_APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrainingTypeController : ControllerBase
    {
        private readonly LearningManagementSystemContext _context;

        public TrainingTypeController(LearningManagementSystemContext context)
        {
            _context = context;
        }

        [HttpGet("getTrainingType")]
        public async Task<ActionResult> getTrainingType()
        {
            var trainingtypes = await _context.TblTrainingTypes
                                       .FromSqlRaw("EXEC display_TrainingType")
                                       .ToListAsync();
            return Ok(trainingtypes);
        }

        [HttpPost("addTrainingType")]
        public async Task<ActionResult> addTrainingType([FromBody] TblTrainingType tblTrainingType)
        {
            try
            {
                await _context.Database.ExecuteSqlRawAsync("EXEC add_edit_TrainingType @p0, @p1, @p2",
                                                        tblTrainingType.Trainingtype_Id == 0 ? null : tblTrainingType.Trainingtype_Id,
                                                        tblTrainingType.TrainingtypeName,
                                                        tblTrainingType.Description);
                return Ok(new {Message = "Training Type Added/Updated."});
            }
            catch (Exception ex)
            {
                return StatusCode(500, new {Message = "An error:", Error = ex.Message});
            }
        }
    }
}
