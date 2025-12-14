//noahb
//description: EmployeeController handles leave requests. it lets employees submit and view their leave requests.
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Employee_leave_management_system.Data;
using Employee_leave_management_system.Models;


namespace Employee_leave_management_system.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private EmployeeService _service = new EmployeeService();

        // POST: /api/employee/leave
        [HttpPost("leave")]
        public IActionResult SubmitLeaveRequest([FromBody] LeaveRequest request)
        {
            try
            {
                var created = _service.SubmitLeaveRequest(request);
                return Ok(created);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET: /api/employee/1/leaves
        [HttpGet("{employeeId}/leaves")]
        public IActionResult GetMyLeaveRequests(int employeeId)
        {
            try
            {
                var results = _service.GetMyLeaveRequests(employeeId);
                return Ok(results);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET: /api/employee/leave/5
        [HttpGet("leave/{leaveRequestId}")]
        public IActionResult GetLeaveRequestById(int leaveRequestId)
        {
            var result = _service.GetLeaveRequestById(leaveRequestId);
            if (result == null)
                return NotFound();
            return Ok(result);
        }
    }

    public class EmployeeService
    {
        private Database _db = new Database();
        public EmployeeService()
    {
        _db = new Database();
    }
    public EmployeeService(Database db)
    {
        _db = db;
    }

        public LeaveRequest SubmitLeaveRequest(LeaveRequest request)
        {
            if (request == null)
                throw new Exception("Request body is missing.");

            if (request.EmployeeId <= 0)
                throw new Exception("EmployeeId must be provided.");

            if (string.IsNullOrWhiteSpace(request.LeaveType))
                throw new Exception("LeaveType must be provided.");

            if (request.StartDate > request.EndDate)
                throw new Exception("StartDate cannot be after EndDate.");

            // Force default status
            request.Status = "Pending";

            _db.LeaveRequests.Add(request);
            _db.SaveChanges();

            return request;
        }

        public List<LeaveRequest> GetMyLeaveRequests(int employeeId)
        {
            if (employeeId <= 0)
                throw new Exception("Invalid employeeId.");

            return _db.LeaveRequests
                .Where(l => l.EmployeeId == employeeId)
                .OrderByDescending(l => l.StartDate)
                .ToList();
        }

        public LeaveRequest GetLeaveRequestById(int leaveRequestId)
        {
            return _db.LeaveRequests.FirstOrDefault(l => l.Id == leaveRequestId);
        }
    }
}
