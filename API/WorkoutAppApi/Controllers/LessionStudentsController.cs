using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WorkoutAppApi.Data;
using WorkoutAppApi.Models.DTOs;

namespace WorkoutAppApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LessionStudentsController : ControllerBase
    {
        private readonly AppDbContext _context;
        public LessionStudentsController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetLessionStudents()
        {
            var students = await _context.LessionStudents.Select(s => new LessionStudentDTO
            {
                LessionId = s.LessionId,
                StudentId = s.StudentId
            })
            .ToListAsync();
            return Ok(students);
        }
    }
}
