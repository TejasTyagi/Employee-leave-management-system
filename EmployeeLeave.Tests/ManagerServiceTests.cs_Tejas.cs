// Tejas Tyagi
// Description: Unit tests for manager business logic using ApprovalService.
// Tests pending requests, approval functionality, and team leave summary.

using Xunit;
using Employee_leave_management_system.Services;
using Employee_leave_management_system.Data;
using Employee_leave_management_system.Models;
using System.Linq;

namespace EmployeeLeave.Tests
{
    public class ManagerServiceTests
    {
        // Helper method to reset database before each test
        private void ResetDatabase()
        {
            using var db = new Database();
            db.LeaveRequests.RemoveRange(db.LeaveRequests);
            db.SaveChanges();
        }

        [Fact]
        public void GetPendingRequests_ReturnsOnlyPendingRequests()
        {
            ResetDatabase();

            using (var db = new Database())
            {
                db.LeaveRequests.Add(new LeaveRequest
                {
                    EmployeeId = 1,
                    ManagerId = 101,
                    LeaveType = "Vacation",
                    Status = "Pending"
                });

                db.LeaveRequests.Add(new LeaveRequest
                {
                    EmployeeId = 2,
                    ManagerId = 101,
                    LeaveType = "Sick",
                    Status = "Approved"
                });

                db.SaveChanges();
            }

            var service = new ApprovalService();
            var result = service.GetPendingRequests();

            Assert.Single(result);
            Assert.Equal("Pending", result.First().Status);
        }

        [Fact]
        public void ApproveRequest_ChangesStatusToApproved()
        {
            ResetDatabase();

            int requestId;

            using (var db = new Database())
            {
                var request = new LeaveRequest
                {
                    EmployeeId = 3,
                    ManagerId = 102,
                    LeaveType = "Personal",
                    Status = "Pending"
                };

                db.LeaveRequests.Add(request);
                db.SaveChanges();

                requestId = request.Id;
            }

            var service = new ApprovalService();
            var updated = service.ApproveRequest(requestId);

            Assert.NotNull(updated);
            Assert.Equal("Approved", updated.Status);
        }

        [Fact]
        public void GetTeamLeaves_ReturnsLeavesForCorrectManager()
        {
            ResetDatabase();

            using (var db = new Database())
            {
                db.LeaveRequests.Add(new LeaveRequest
                {
                    EmployeeId = 10,
                    ManagerId = 200,
                    LeaveType = "Vacation",
                    Status = "Pending"
                });

                db.LeaveRequests.Add(new LeaveRequest
                {
                    EmployeeId = 11,
                    ManagerId = 201,
                    LeaveType = "Sick",
                    Status = "Pending"
                });

                db.SaveChanges();
            }

            var service = new ApprovalService();
            var result = service.GetTeamLeaves(200);

            Assert.Single(result);
            Assert.Equal(200, result.First().ManagerId);
        }
    }
}
