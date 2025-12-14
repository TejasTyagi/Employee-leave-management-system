/*
Author: Shaymaa Hasan
Description: Handles admin-related API endpoints for managing leave types.
*/

using Microsoft.AspNetCore.Mvc;
using Employee_leave_management_system.Models;
using Employee_leave_management_system.Services;

namespace Employee_leave_management_system.Controllers
{
    [ApiController]
    [Route("api/admin")]
    public class AdminController : ControllerBase
    {
        private readonly AdminService _adminService;

        public AdminController(AdminService adminService)
        {
            _adminService = adminService;
        }

        [HttpGet("leave-types")]
        public async Task<IActionResult> GetLeaveTypes()
        {
            var leaveTypes = await _adminService.GetAllLeaveTypesAsync();
            return Ok(leaveTypes);
        }

        [HttpPost("leave-types")]
        public async Task<IActionResult> CreateLeaveType([FromBody] LeaveType leaveType)
        {
            var created = await _adminService.CreateLeaveTypeAsync(leaveType);
            return Ok(created);
        }

        [HttpDelete("leave-types/{id}")]
        public async Task<IActionResult> DeactivateLeaveType(int id)
        {
            var result = await _adminService.DeactivateLeaveTypeAsync(id);
            if (!result)
                return NotFound();

            return NoContent();
        }
    }
}
