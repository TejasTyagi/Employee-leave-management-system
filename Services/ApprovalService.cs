// Author: Tejas Tyagi
// Description: Business logic for manager actions such as getting pending leave requests, approving/rejecting requests, and viewing team leave summary.
using Employee_leave_management_system.Data;
using Employee_leave_management_system.Models;
using System.Collections.Generic;
using System.Linq;

namespace Employee_leave_management_system.Services
{
    public class ApprovalService
    {
        private Database _db = new Database();
        public List<LeaveRequest> GetPendingRequests()
        {
            return _db.LeaveRequests
                .Where(l => l.Status == "Pending")
                .ToList();
        }
        public LeaveRequest ApproveRequest(int id)
        {
            var request = _db.LeaveRequests
                .FirstOrDefault(l => l.Id == id);

            if (request == null)
            {
                return null;
            }
            if (request.Status == "Pending")
            {
                request.Status = "Approved";
                _db.SaveChanges();
            }

            return request;
        }
        public LeaveRequest RejectRequest(int id)
        {
            var request = _db.LeaveRequests
                .FirstOrDefault(l => l.Id == id);

            if (request == null)
            {
                return null;
            }

            if (request.Status == "Pending")
            {
                request.Status = "Rejected";
                _db.SaveChanges();
            }

            return request;
        }

        // Get all leave requests for a manager's team
        // Assumes LeaveRequest has a ManagerId property
        public List<LeaveRequest> GetTeamLeaves(int managerId)
        {
            return _db.LeaveRequests
                .Where(l => l.ManagerId == managerId)
                .OrderByDescending(l => l.StartDate)
                .ToList();
        }
    }
}
