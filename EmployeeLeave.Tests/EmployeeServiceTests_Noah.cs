using System;
using System.Linq;
using Employee_leave_management_system.Controllers;
using Employee_leave_management_system.Data;
using Employee_leave_management_system.Models;
using Xunit;

namespace EmployeeLeave.Tests
{
    public class EmployeeServiceTests_Noah
    {
        private Database CreateFreshDatabase()
        {
            var db = new Database();
            db.Database.EnsureDeleted();
            db.Database.EnsureCreated();
            return db;
        }

        [Fact]
        public void SubmitLeaveRequest_SetsStatusToPending()
        {
            var db = CreateFreshDatabase();
            var service = new EmployeeService(db);

            var request = new LeaveRequest
            {
                EmployeeId = 1,
                ManagerId = 10,
                LeaveType = "Vacation",
                StartDate = DateTime.Today,
                EndDate = DateTime.Today.AddDays(2),
                Reason = "Trip"
            };

            var result = service.SubmitLeaveRequest(request);

            Assert.Equal("Pending", result.Status);
        }

        [Fact]
        public void SubmitLeaveRequest_InvalidEmployeeId_ThrowsException()
        {
            var db = CreateFreshDatabase();
            var service = new EmployeeService(db);

            var request = new LeaveRequest
            {
                EmployeeId = 0, // invalid
                ManagerId = 10,
                LeaveType = "Sick",
                StartDate = DateTime.Today,
                EndDate = DateTime.Today.AddDays(1),
                Reason = "Flu"
            };

            Assert.Throws<Exception>(() => service.SubmitLeaveRequest(request));
        }

        [Fact]
        public void GetMyLeaveRequests_ReturnsOnlyRequestsForEmployee()
        {
            var db = CreateFreshDatabase();
            var service = new EmployeeService(db);

            service.SubmitLeaveRequest(new LeaveRequest
            {
                EmployeeId = 1,
                ManagerId = 10,
                LeaveType = "Vacation",
                StartDate = DateTime.Today,
                EndDate = DateTime.Today.AddDays(1),
                Reason = "A"
            });

            service.SubmitLeaveRequest(new LeaveRequest
            {
                EmployeeId = 2,
                ManagerId = 10,
                LeaveType = "Sick",
                StartDate = DateTime.Today,
                EndDate = DateTime.Today.AddDays(1),
                Reason = "B"
            });

            var results = service.GetMyLeaveRequests(1);

            Assert.All(results, r => Assert.Equal(1, r.EmployeeId));
            Assert.NotEmpty(results);
        }
    }
}
