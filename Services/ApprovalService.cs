// Tejas Tyagi
// Description: Business logic for manager actions such as getting pending leave requests,
// approving/rejecting requests, viewing team leave summary, and retrieving manager ids.

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
            var request = _db.LeaveRequests.FirstOrDefault(l => l.Id == id);

            if (request == null)
                return null;

            if (request.Status == "Pending")
            {
                request.Status = "Approved";
                _db.SaveChanges();
            }

            return request;
        }

        public LeaveRequest RejectRequest(int id)
        {
            var request = _db.LeaveRequests.FirstOrDefault(l => l.Id == id);

            if (request == null)
                return null;

            if (request.Status == "Pending")
            {
                request.Status = "Rejected";
                _db.SaveChanges();
            }

            return request;
        }

        // Returns all leave requests for employees under a specific manager
        public List<LeaveRequest> GetTeamLeaves(int managerId)
        {
            return _db.LeaveRequests
                .Where(l => l.ManagerId == managerId)
                .OrderByDescending(l => l.StartDate)
                .ToList();
        }

        // Returns distinct manager IDs used by employees when submitting leave requests
        public List<int> GetManagerIds()
        {
            return _db.LeaveRequests
                .Select(l => l.ManagerId)
                .Distinct()
                .OrderBy(id => id)
                .ToList();
        }
    }
}
