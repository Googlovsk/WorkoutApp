using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WorkoutAppApi.Data;
using WorkoutAppApi.Models.DTOs;

namespace WorkoutAppApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeacherScheduleController : ControllerBase
    {
        private readonly AppDbContext _context;

        public TeacherScheduleController(AppDbContext context)
        {
            _context = context;
        }
        [HttpGet("{teacherId}")]
        public async Task<IActionResult> GetSchedule(string teacherId)
        {
            var schedule = await _context.Schedules.FirstOrDefaultAsync(s => s.TeacherId == teacherId);
            return Ok(schedule);
        }
        [HttpPut("{teacherId}")]
        public async Task<IActionResult> UpdateSchedule(int id, TeacherScheduleDTO scheduleDto)
        {
            if (id != scheduleDto.Id)
            {
                return BadRequest();
            }
            var schedule = await _context.Schedules.FindAsync(id);
            if (schedule != null)
            {
                schedule.CanWorkWith = scheduleDto.CanWorkWith;
                schedule.CanWorkBy = scheduleDto.CanWorkBy;

                _context.Entry(schedule).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                return NoContent();
            }
            return NotFound();
        }
    }
}
