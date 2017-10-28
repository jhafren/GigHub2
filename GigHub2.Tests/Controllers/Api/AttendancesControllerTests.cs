using FluentAssertions;
using GigHub2.Controllers.Api;
using GigHub2.Core;
using GigHub2.Core.Dtos;
using GigHub2.Core.Models;
using GigHub2.Core.Repositories;
using GigHub2.Tests.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Web.Http.Results;

namespace GigHub2.Tests.Controllers.Api
{
    [TestClass]
    public class AttendancesControllerTests
    {
        private AttendancesController _controller;
        private Mock<IAttendanceRepository> _mockRepositoty;
        private string _userId;

        [TestInitialize]
        public void TestInitialize()
        {
            _mockRepositoty = new Mock<IAttendanceRepository>();
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(m => m.Attendances).Returns(_mockRepositoty.Object);
            _controller = new AttendancesController(mockUnitOfWork.Object);
            _userId = "2";
            _controller.MockCurrentUser(_userId, "user@domain.com");
        }

        [TestMethod]
        public void Attend_AttendaceAlreadyExists_ShouldReturnBadRequest()
        {
            var gigId = 1;
            var dto = new AttendanceDto { GigId = gigId };
            var attendance = new Attendance();
            _mockRepositoty.Setup(m => m.GetAttendance(_userId, gigId)).Returns(attendance);

            var result = _controller.Attend(dto);

            result.Should().BeOfType<BadRequestErrorMessageResult>();
        }

        [TestMethod]
        public void Attend_ValidResult_ShouldReturnOk()
        {
            var gigId = 1;
            var dto = new AttendanceDto { GigId = gigId };

            var result = _controller.Attend(dto);

            result.Should().BeOfType<OkResult>();
        }

        [TestMethod]
        public void DeleteAttendance_AttendanceDoesNotExist_ShouldReturnNotFound()
        {
            var gigId = 1;

            var result = _controller.DeleteAttendance(gigId);

            result.Should().BeOfType<NotFoundResult>();
        }

        [TestMethod]
        public void DeleteAttendance_ValidResult_ShouldReturnOk()
        {
            var gigId = 1;
            var attendance = new Attendance();
            _mockRepositoty.Setup(m => m.GetAttendance(_userId, gigId)).Returns(attendance);

            var result = _controller.DeleteAttendance(gigId);

            result.Should().BeOfType<OkNegotiatedContentResult<int>>();
        }


    }
}
