// Tejas Tyagi
// Description: Web API controller for manager operations.
// Handles pending requests, approval/rejection, team leave summary,
// and retrieval of manager ids used in the system.

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
        [HttpGet("pendingrequests")]
        public IActionResult GetPendingRequests()
        {
            var pending = _approvalService.GetPendingRequests();
            return Ok(pending);
        }

        // PUT: api/manager/approve/5
        [HttpPut("approve/{id}")]
        public IActionResult ApproveLeave(int id)
        {
            var result = _approvalService.ApproveRequest(id);

            if (result == null)
                return NotFound("Leave request not found.");

            return Ok(result);
        }

        // PUT: api/manager/reject/5
        [HttpPut("reject/{id}")]
        public IActionResult RejectLeave(int id)
        {
            var result = _approvalService.RejectRequest(id);

            if (result == null)
                return NotFound("Leave request not found.");

            return Ok(result);
        }

        // GET: api/manager/teamleaves/3
        [HttpGet("teamleaves/{managerId}")]
        public IActionResult GetTeamLeaves(int managerId)
        {
            var leaves = _approvalService.GetTeamLeaves(managerId);
            return Ok(leaves);
        }

        // GET: api/manager/managerids
        // Returns all distinct manager IDs used by employees
        [HttpGet("managerids")]
        public IActionResult GetManagerIds()
        {
            var ids = _approvalService.GetManagerIds();
            return Ok(ids);
        }
    }
}
