using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WorkoutAppApi.Data;
using WorkoutAppApi.Models.Domain;
using WorkoutAppApi.Models.DTOs;

namespace WorkoutAppApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        private readonly AppDbContext _context;

        public NotificationController(AppDbContext context)
        {
            _context = context;
        }
        [HttpGet("{userId}")]
        public async Task<IActionResult> GetNotifications(string userId)
        {
            var notifys = await _context.Notifications.ToListAsync();
            return Ok(notifys);
        }
        [HttpPost]
        public async Task<IActionResult> SendNotification(NotificationDTO notificationDto)
        {
            var notify = new Notification
            {
                Message = notificationDto.Message,
                SentDate = notificationDto.SentDate,
                RecipientId = notificationDto.RecipientId
            };
            _context.Notifications.Add(notify);
            await _context.SaveChangesAsync();
            return Ok(notify);
        }
    }
}
