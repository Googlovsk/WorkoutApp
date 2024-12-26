using Microsoft.AspNetCore.Mvc;
using WorkoutAppApi.Models.Domain;
using static WorkoutAppApi.Models.Domain.Lession;
using WorkoutAppApi.Data;
using Microsoft.EntityFrameworkCore;
using WorkoutAppApi.Models.DTOs;

namespace WorkoutAppApi.Controllers
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

        [HttpGet]
        public async Task<IActionResult> GetLessions()
        {
            var lessions = await _context.Lessions
                .Include(l => l.Category)
                .Include(l => l.Teacher)
                .Select(l => new LessionDTO
                {
                    Id = l.Id,
                    Name = l.Name,
                    TeacherId = l.TeacherId,
                    Status = (int)l.Status,
                    StartDate = (DateTime)l.StartDate,
                    EndDate = (DateTime)l.EndDate,
                    CategoryId = l.CategoryId,
                    Notes = l.Notes,
                    IsGroup = l.IsGroup,
                    Location = l.Location,
                    MaxGroupSize = l.MaxGroupSize,
                })
                .ToListAsync();
            return Ok(lessions);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Lession>> GetLession(int id)
        {
            var lession = await _context.Lessions
                .Include(l => l.Category)
                .Include(l => l.Teacher)
                .Select(l => new LessionDTO
                {
                    Id = l.Id,
                    Name = l.Name,
                    TeacherId = l.TeacherId,
                    Status = (int)l.Status,
                    StartDate = (DateTime)l.StartDate,
                    EndDate = (DateTime)l.EndDate,
                    CategoryId = l.CategoryId,
                    Notes = l.Notes,
                    IsGroup = l.IsGroup,
                    Location = l.Location,
                    MaxGroupSize = l.MaxGroupSize,
                })
                .FirstOrDefaultAsync(l => l.Id == id);

            if (lession == null)
            {
                return NotFound();
            }
            return Ok(lession);
        }

        [HttpPost]
        public async Task<IActionResult> CreateLession([FromBody] LessionDTO lessionDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var lession = new Lession
            {
                Name = lessionDto.Name,
                TeacherId = lessionDto.TeacherId,
                CategoryId = lessionDto.CategoryId,
                StartDate = lessionDto.StartDate,
                EndDate = lessionDto.EndDate,
                Status = LessionStatus.Scheduled,
                Notes = lessionDto.Notes,
                IsGroup = lessionDto.IsGroup,
                MaxGroupSize = lessionDto.MaxGroupSize,
                Location = lessionDto.Location
            };

            _context.Lessions.Add(lession);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetLession), new { id = lession.Id }, lession);
        }
        [HttpPost("{id}/Register")]
        public async Task<IActionResult> RegisterStudent(int id, [FromBody] RegisterStudentDTO dto)
        {
            var lession = await _context.Lessions
                .Include(l => l.Teacher)
                .Include(l => l.Students)
                .FirstOrDefaultAsync(l => l.Id == id);

            if (lession == null)
                return NotFound("Занятие не найдено.");

            if (lession.Students.Any(s => s.StudentId == dto.StudentId.ToString()))
                return Conflict("Студент уже зарегистрирован на это занятие.");

            var schedule = await _context.Schedules
                .FirstOrDefaultAsync(s => s.TeacherId == lession.TeacherId);

            if (schedule == null)
                return BadRequest("Для преподавателя не указано расписание.");

            if (lession.StartDate.HasValue && lession.EndDate.HasValue)
            {
                if (lession.StartDate.Value.Date < schedule.CanWorkWith.ToDateTime(TimeOnly.MinValue) ||
                    lession.EndDate.Value.Date > schedule.CanWorkBy.ToDateTime(TimeOnly.MaxValue))
                {
                    return BadRequest("Преподаватель в данный момент не доступен. Выберите другое время.");
                }
            }
            var lessionStudent = new LessionStudent
            {
                LessionId = id,
                StudentId = dto.StudentId.ToString()
            };

            _context.LessionStudents.Add(lessionStudent);
            await _context.SaveChangesAsync();

            return Ok();
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateLession(int id, LessionDTO lessionDto)
        {
            if (id != lessionDto.Id)
            {
                return BadRequest();
            }
            var lession = await _context.Lessions.FindAsync(id);
            if (lession != null)
            {
                lession.Name = lessionDto.Name;
                lession.Status = (LessionStatus)lessionDto.Status;
                lession.Notes = lessionDto.Notes;
                lession.Location = lessionDto.Location;
                lession.MaxGroupSize = lessionDto.MaxGroupSize;
                lession.IsGroup = lessionDto.IsGroup;
                lession.CategoryId = lessionDto.CategoryId;

                _context.Entry(lessionDto).State = EntityState.Modified;

                if (!lession.IsGroup)
                {
                    lession.MaxGroupSize = 1;
                }
                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    return StatusCode(500, "Ошибка при обновлении базы данных");
                }
                return NoContent();
            }
            return NotFound();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLession(int id)
        {
            var lession = await _context.Lessions.FindAsync(id);

            if (lession == null)
            {
                return NotFound();
            }

            _context.Lessions.Remove(lession);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
