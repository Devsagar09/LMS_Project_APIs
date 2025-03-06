using LMS_Project_APIs.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using Microsoft.EntityFrameworkCore;

namespace LMS_Project_APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrainingController : ControllerBase
    {
        private readonly LearningManagementSystemContext _context;

        public TrainingController(LearningManagementSystemContext context)
        {
            _context = context;
        }

        [HttpGet("getTraining")]
        public IActionResult getTraining()
        {
            var trainings =  _context.TblTrainings
                            .FromSqlRaw("EXEC display_Training")
                            .AsEnumerable()
                            .ToList();
            return Ok(trainings);
        }

        [HttpPost("addTraining")]
        public async Task<ActionResult> addTraining([FromBody] TblTraining tblTraining)
        {
            try
            {
                await _context.Database.ExecuteSqlRawAsync("EXEC add_edit_training @p0, @p1, @p2, @p3, @p4, @p5, @p6, @p7, @p8, @p9, @p10, @p11, @p12, @p13",
                                                          
                                                               tblTraining.TrainingId == 0 ? null : tblTraining.TrainingId,
                                                               tblTraining.TrainingName,
                                                               tblTraining.TrainingCode,
                                                               tblTraining.TrainingtypeId,
                                                               tblTraining.DocumentFile,
                                                               tblTraining.ExternalLinkUrl,
                                                               tblTraining.TrainingHours,
                                                               tblTraining.RequiresApproval,
                                                               tblTraining.ArchiveDate,
                                                               tblTraining.Summary,
                                                               tblTraining.CourseCatalog,
                                                               tblTraining.CstartDate,
                                                               tblTraining.CendDate,
                                                               tblTraining.ThumbnailImage
                                                           );
                 return Ok(new {Message = "Training Added/Updated"});
            }
            catch(Exception ex) 
            {
                return StatusCode(500, new {Message = "An error:", Error = ex.Message});
            }
        }

        [HttpDelete]
        [Route("deleteTraining")]
        public async Task<ActionResult<IEnumerable<TblTraining>>> deleteTraining([FromBody] List<int> trainingIds)
        {
            if (trainingIds == null || trainingIds.Count == 0)
            {
                return BadRequest(new { Message = "No trainingIds provide for deletion." });
            }

            try
            {
                string multrainingId = string.Join(",", trainingIds);
                await _context.Database.ExecuteSqlRawAsync("EXEC delete_Training @p0", multrainingId);
                return Ok(new { Message = "Training deleted successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "an error : ", Error = ex.Message });
            }
        }
    }
}
