// Tejas Tyagi
// Description: Web API controller for manager operations. Uses endpoints for pending requests, approval/rejection, and viewing team leave summary.

using Microsoft.AspNetCore.Mvc;
using Employee_leave_management_system.Services;

namespace Employee_leave_management_system.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ManagerController : ControllerBase
    {
        private ApprovalService _approvalService = new ApprovalService();

        // GET: api/manager/pendingrequests
        // Returns all leave requests with Status = "Pending"
        [HttpGet("pendingrequests")]
        public IActionResult GetPendingRequests()
        {
            var pending = _approvalService.GetPendingRequests();
            return Ok(pending);
        }

        // PUT: api/manager/approve/5
        // Approves a leave request by id
        [HttpPut("approve/{id}")]
        public IActionResult ApproveLeave(int id)
        {
            var result = _approvalService.ApproveRequest(id);

            if (result == null)
            {
                return NotFound("Leave request not found.");
            }

            return Ok(result);
        }

        // PUT: api/manager/reject/5
        // Rejects a leave request by id
        [HttpPut("reject/{id}")]
        public IActionResult RejectLeave(int id)
        {
            var result = _approvalService.RejectRequest(id);

            if (result == null)
            {
                return NotFound("Leave request not found.");
            }

            return Ok(result);
        }

        // GET: api/manager/teamleaves/3
        // Returns all leave requests for employees under a manager
        [HttpGet("teamleaves/{managerId}")]
        public IActionResult GetTeamLeaves(int managerId)
        {
            var leaves = _approvalService.GetTeamLeaves(managerId);
            return Ok(leaves);
        }
    }
}
