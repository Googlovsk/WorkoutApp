
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Schedule.Models.Domain;
using Schedule.Models;
using static Schedule.Models.Domain.Lession;
using Schedule.Data;
using Microsoft.EntityFrameworkCore;

namespace Schedule.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LessionController : ControllerBase
    {
        private readonly AppDbContext _context;

        public LessionController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Lessions
        [HttpGet]
        public async Task<IActionResult> GetLessions()
        {
            var lessions = await _context.Lessions
                .Include(l => l.Teacher)
                .Include(l => l.Category)
                .ToListAsync();

            return Ok(lessions);
        }

        // POST: api/Lessions
        [HttpPost]
        public async Task<IActionResult> CreateLession([FromBody] LessionDTO lessionDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Check teacher availability
            if (!IsTeacherAvailable(lessionDto.TeacherId, lessionDto.StartDate, lessionDto.EndDate))
                return Conflict("The teacher is not available for the selected time slot.");

            // Check room availability if room is specified
            if (lessionDto.RoomId.HasValue && !IsRoomAvailable(lessionDto.RoomId.Value, lessionDto.StartDate, lessionDto.EndDate))
                return Conflict("The room is not available for the selected time slot.");

            var lession = new Lession
            {
                Name = lessionDto.Name,
                TeacherId = lessionDto.TeacherId,
                CategoryId = lessionDto.CategoryId,
                StartDate = lessionDto.StartDate,
                EndDate = lessionDto.EndDate,
                Status = LessionStatus.Scheduled,
                Notes = lessionDto.Notes,
                IsGroup = lessionDto.IsGroup
            };

            _context.Lessions.Add(lession);
            await _context.SaveChangesAsync();

            // Create a schedule entry for the teacher
            var schedule = new Schedule.Models.Domain.Schedule
            {
                TeacherId = lessionDto.TeacherId,
                LocationId = lessionDto.RoomId,
                StartTime = lessionDto.StartDate,
                EndTime = lessionDto.EndDate,
                LessionId = lession.Id,
            };

            _context.Schedules.Add(schedule);
            await _context.SaveChangesAsync();

            //return CreatedAtAction("GetLession", new { id = lession.Id }, lession);
            return Ok(lession);
        }

        // POST: api/Lessions/{id}/Register
        [HttpPost("{id}/Register")]
        public async Task<IActionResult> RegisterStudent(int id, [FromBody] RegisterStudentDTO dto)
        {
            var lession = await _context.Lessions
                .Include(l => l.Students)
                .FirstOrDefaultAsync(l => l.Id == id);

            if (lession == null)
                return NotFound("Lession not found.");

            // Check if the student is already registered
            if (lession.Students.Any(s => s.StudentId == dto.StudentId.ToString()))
                return Conflict("The student is already registered for this lession.");

            // Check teacher availability
            if (!IsTeacherAvailable(lession.TeacherId, lession.StartDate.Value, lession.EndDate.Value))
                return Conflict("The teacher is not available for the selected time slot.");

            var lessionStudent = new LessionStudent
            {
                LessionId = id,
                StudentId = dto.StudentId.ToString()
            };

            _context.LessionStudents.Add(lessionStudent);
            await _context.SaveChangesAsync();

            return Ok();
        }

        private bool IsTeacherAvailable(string teacherId, DateTime startTime, DateTime endTime)
        {
            return !_context.Schedules.Any(s =>
                s.TeacherId == teacherId &&
                ((s.StartTime < endTime && s.EndTime > startTime)));
        }

        private bool IsRoomAvailable(int roomId, DateTime startTime, DateTime endTime)
        {
            return !_context.Schedules.Any(s =>
                s.LocationId == roomId &&
                ((s.StartTime < endTime && s.EndTime > startTime)));
        }
    }
}
