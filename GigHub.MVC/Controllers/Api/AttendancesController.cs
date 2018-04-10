using GigHub.MVC.Core;
using GigHub.MVC.Core.DTOs;
using GigHub.MVC.Core.Models;
using Microsoft.AspNet.Identity;
using System.Web.Http;

namespace GigHub.MVC.Controllers.Api
{
    [Authorize]
    public class AttendancesController : ApiController
    {
        private readonly IUnitOfWork _unitOfWork;

        public AttendancesController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpPost]
        public IHttpActionResult Attend(AttendancesDTO dto)
        {
            var userId = User.Identity.GetUserId();
            var duplicated = _unitOfWork.Attendances.GetAttendance(dto.GigId, userId) != null;

            if (duplicated)
                return BadRequest("The attendance already exists.");

            var attendace = new Attendance
            {
                GigId = dto.GigId,
                AttendeeId = userId
            };

            _unitOfWork.Attendances.Add(attendace);
            _unitOfWork.Complete();

            return Ok();
        }

        [HttpDelete]
        public IHttpActionResult DeleteAttendance(int gigId)
        {
            var userId = User.Identity.GetUserId();
            var attendance = _unitOfWork.Attendances.GetAttendance(gigId, userId);

            if (attendance == null)
                return NotFound();

            _unitOfWork.Attendances.Remove(attendance);
            _unitOfWork.Complete();

            return Ok(gigId);
        }
    }
}