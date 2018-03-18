using GigHub.MVC.DTOs;
using GigHub.MVC.Models;
using Microsoft.AspNet.Identity;
using System.Linq;
using System.Web.Http;

namespace GigHub.MVC.Controllers
{
    [Authorize]
    public class AttendancesController : ApiController
    {
        private ApplicationDbContext _context;

        public AttendancesController()
        {
            _context = new ApplicationDbContext();
        }

        [HttpPost]
        public IHttpActionResult Attend(AttendancesDTO dto)
        {
            var userId = User.Identity.GetUserId();
            var duplicated = _context.Attendances.Any(a => a.AttendeeId == userId && a.GigId == dto.GigId);

            if (duplicated)
                return BadRequest("The attendance already exists.");

            var attendace = new Attendance
            {
                GigId = dto.GigId,
                AttendeeId = userId
            };

            _context.Attendances.Add(attendace);
            _context.SaveChanges();

            return Ok();
        }
    }
}