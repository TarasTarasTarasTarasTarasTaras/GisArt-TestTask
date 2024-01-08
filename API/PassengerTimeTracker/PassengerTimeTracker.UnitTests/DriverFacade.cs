using FluentAssertions;
using Moq;
using PassengerTimeTracker.Business.Drivers;
using PassengerTimeTracker.Data;
using PassengerTimeTracker.Entities.DTOs;

namespace PassengerTimeTracker.UnitTests
{
    public class Tests
    {
        private IDriverFacade _driverFacade;

        private Mock<ITripRepository> _tripRepositoryMock;
        private Mock<IDriverRepository> _tripDriverRepositoryMock;

        [SetUp]
        public void Setup()
        {
            _tripRepositoryMock = new Mock<ITripRepository>();
            _tripDriverRepositoryMock = new Mock<IDriverRepository>();

            _driverFacade = new DriverFacade(_tripRepositoryMock.Object, _tripDriverRepositoryMock.Object);
        }

        [Test]
        public void CalculatePassengerPeriodsTest()
        {
            // Arrange
            var trips = new List<TripDTO>
            {
                new TripDTO { DriverId = 1, PickUp = new DateTime(2023, 12, 1, 8, 0, 0), DropOff = new DateTime(2023, 12, 1, 9, 0, 0) },
                new TripDTO { DriverId = 1, PickUp = new DateTime(2023, 12, 1, 9, 0, 0), DropOff = new DateTime(2023, 12, 1, 10, 0, 0) },
                new TripDTO { DriverId = 1, PickUp = new DateTime(2023, 12, 1, 10, 0, 0), DropOff = new DateTime(2023, 12, 1, 11, 0, 0) },

                new TripDTO { DriverId = 2, PickUp = new DateTime(2023, 12, 1, 8, 0, 0), DropOff = new DateTime(2023, 12, 1, 9, 0, 0) },
                new TripDTO { DriverId = 2, PickUp = new DateTime(2023, 12, 1, 9, 0, 0), DropOff = new DateTime(2023, 12, 1, 10, 0, 0) },
                new TripDTO { DriverId = 2, PickUp = new DateTime(2023, 12, 1, 10, 0, 0), DropOff = new DateTime(2023, 12, 1, 11, 0, 0) },
                new TripDTO { DriverId = 2, PickUp = new DateTime(2023, 12, 1, 11, 0, 0), DropOff = new DateTime(2023, 12, 1, 12, 0, 0) },

                new TripDTO { DriverId = 3, PickUp = new DateTime(2023, 12, 1, 8, 0, 0), DropOff = new DateTime(2023, 12, 1, 9, 0, 0) },
                new TripDTO { DriverId = 3, PickUp = new DateTime(2023, 12, 1, 9, 0, 0), DropOff = new DateTime(2023, 12, 1, 10, 0, 0) },
                new TripDTO { DriverId = 3, PickUp = new DateTime(2023, 12, 1, 10, 0, 0), DropOff = new DateTime(2023, 12, 1, 11, 0, 0) },
                new TripDTO { DriverId = 3, PickUp = new DateTime(2023, 12, 1, 11, 0, 0), DropOff = new DateTime(2023, 12, 1, 12, 0, 0) },
                new TripDTO { DriverId = 3, PickUp = new DateTime(2023, 12, 1, 12, 0, 0), DropOff = new DateTime(2023, 12, 1, 13, 0, 0) }
            };

            _tripRepositoryMock.Setup(x => x.GetAllTrips(null)).Returns(trips);

            var expected = new List<DriverDTO>
            {
                new DriverDTO { DriverId = 1, PayableTime = new TimeSpan(3, 0, 0) },
                new DriverDTO { DriverId = 2, PayableTime = new TimeSpan(4, 0, 0) },
                new DriverDTO { DriverId = 3, PayableTime = new TimeSpan(5, 0, 0) }
            };

            // Act
            var actual = _driverFacade.GetAllDrivers(new() { CalculatePayableTime = true }).OrderBy(d => d.DriverId);

            // Assert
            actual.Should().BeEquivalentTo(expected);
        }

        [Test]
        public void CalculatePassengerPeriodsTest_WhenSeveralPassengersInCar()
        {
            // Arrange
            var trips = new List<TripDTO>
            {
                new TripDTO { DriverId = 1, PickUp = new DateTime(2023, 12, 1, 8, 0, 0), DropOff = new DateTime(2023, 12, 1, 12, 0, 0) },
                new TripDTO { DriverId = 1, PickUp = new DateTime(2023, 12, 1, 9, 0, 0), DropOff = new DateTime(2023, 12, 1, 10, 0, 0) },
                new TripDTO { DriverId = 1, PickUp = new DateTime(2023, 12, 1, 10, 0, 0), DropOff = new DateTime(2023, 12, 1, 11, 0, 0) },

                new TripDTO { DriverId = 2, PickUp = new DateTime(2023, 12, 1, 8, 0, 0), DropOff = new DateTime(2023, 12, 1, 9, 0, 0) },
                new TripDTO { DriverId = 2, PickUp = new DateTime(2023, 12, 1, 9, 0, 0), DropOff = new DateTime(2023, 12, 1, 10, 0, 0) },
                new TripDTO { DriverId = 2, PickUp = new DateTime(2023, 12, 1, 10, 0, 0), DropOff = new DateTime(2023, 12, 1, 13, 0, 0) },
                new TripDTO { DriverId = 2, PickUp = new DateTime(2023, 12, 1, 11, 0, 0), DropOff = new DateTime(2023, 12, 1, 15, 0, 0) },

                new TripDTO { DriverId = 3, PickUp = new DateTime(2023, 12, 1, 8, 0, 0), DropOff = new DateTime(2023, 12, 1, 9, 0, 0) },
                new TripDTO { DriverId = 3, PickUp = new DateTime(2023, 12, 1, 9, 0, 0), DropOff = new DateTime(2023, 12, 1, 10, 0, 0) },
                new TripDTO { DriverId = 3, PickUp = new DateTime(2023, 12, 1, 15, 0, 0), DropOff = new DateTime(2023, 12, 1, 23, 0, 0) },
                new TripDTO { DriverId = 3, PickUp = new DateTime(2023, 12, 1, 11, 0, 0), DropOff = new DateTime(2023, 12, 1, 12, 0, 0) },
                new TripDTO { DriverId = 3, PickUp = new DateTime(2023, 12, 1, 11, 0, 0), DropOff = new DateTime(2023, 12, 1, 13, 0, 0) }
            };

            _tripRepositoryMock.Setup(x => x.GetAllTrips(null)).Returns(trips);

            var expected = new List<DriverDTO>
            {
                new DriverDTO { DriverId = 1, PayableTime = new TimeSpan(4, 0, 0) },
                new DriverDTO { DriverId = 2, PayableTime = new TimeSpan(7, 0, 0) },
                new DriverDTO { DriverId = 3, PayableTime = new TimeSpan(12, 0, 0) }
            };

            // Act
            var actual = _driverFacade.GetAllDrivers(new() { CalculatePayableTime = true }).OrderBy(d => d.DriverId);

            // Assert
            actual.Should().BeEquivalentTo(expected);
        }

        [Test]
        public void CalculatePassengerPeriodsTest_WhenPassengerGoesLessTime()
        {
            // Arrange
            var trips = new List<TripDTO>
            {
                new TripDTO { DriverId = 1, PickUp = new DateTime(2023, 12, 1, 6, 0, 0), DropOff = new DateTime(2023, 12, 1, 15, 0, 0) },
                new TripDTO { DriverId = 1, PickUp = new DateTime(2023, 12, 1, 9, 0, 0), DropOff = new DateTime(2023, 12, 1, 10, 0, 0) },
                new TripDTO { DriverId = 1, PickUp = new DateTime(2023, 12, 1, 13, 0, 0), DropOff = new DateTime(2023, 12, 1, 14, 0, 0) },

                new TripDTO { DriverId = 2, PickUp = new DateTime(2023, 12, 1, 8, 0, 0), DropOff = new DateTime(2023, 12, 1, 9, 0, 0) },
                new TripDTO { DriverId = 2, PickUp = new DateTime(2023, 12, 1, 9, 0, 0), DropOff = new DateTime(2023, 12, 1, 10, 0, 0) },
                new TripDTO { DriverId = 2, PickUp = new DateTime(2023, 12, 1, 6, 0, 0), DropOff = new DateTime(2023, 12, 1, 13, 0, 0) },
                new TripDTO { DriverId = 2, PickUp = new DateTime(2023, 12, 1, 11, 0, 0), DropOff = new DateTime(2023, 12, 1, 15, 0, 0) },

                new TripDTO { DriverId = 3, PickUp = new DateTime(2023, 12, 1, 8, 0, 0), DropOff = new DateTime(2023, 12, 1, 9, 0, 0) },
                new TripDTO { DriverId = 3, PickUp = new DateTime(2023, 12, 1, 9, 0, 0), DropOff = new DateTime(2023, 12, 1, 10, 0, 0) },
                new TripDTO { DriverId = 3, PickUp = new DateTime(2023, 12, 1, 15, 0, 0), DropOff = new DateTime(2023, 12, 1, 23, 0, 0) },
                new TripDTO { DriverId = 3, PickUp = new DateTime(2023, 12, 1, 11, 0, 0), DropOff = new DateTime(2023, 12, 1, 12, 0, 0) },
                new TripDTO { DriverId = 3, PickUp = new DateTime(2023, 12, 1, 5, 0, 0), DropOff = new DateTime(2023, 12, 1, 18, 0, 0) }
            };

            _tripRepositoryMock.Setup(x => x.GetAllTrips(null)).Returns(trips);

            var expected = new List<DriverDTO>
            {
                new DriverDTO { DriverId = 1, PayableTime = new TimeSpan(9, 0, 0) },
                new DriverDTO { DriverId = 2, PayableTime = new TimeSpan(9, 0, 0) },
                new DriverDTO { DriverId = 3, PayableTime = new TimeSpan(18, 0, 0) }
            };

            // Act
            var actual = _driverFacade.GetAllDrivers(new() { CalculatePayableTime = true }).OrderBy(d => d.DriverId);

            // Assert
            actual.Should().BeEquivalentTo(expected);
        }
    }
}