using PassengerTimeTracker.Data;
using PassengerTimeTracker.Entities.DTOs;
using PassengerTimeTracker.Entities.Filters;
using System.Collections.Concurrent;
using System.Diagnostics;

namespace PassengerTimeTracker.Business.Drivers
{
    public class DriverFacade : IDriverFacade
    {
        private readonly ITripRepository _tripRepository;
        private readonly IDriverRepository _driverRepository;

        public DriverFacade(ITripRepository tripRepository, IDriverRepository driverRepository)
        {
            _tripRepository = tripRepository;
            _driverRepository = driverRepository;
        }

        public IEnumerable<DriverDTO> GetAllDrivers(DriverFilterModel filters)
        {
            if (!filters.CalculatePayableTime)
                return _driverRepository.GetAllDrivers(filters);

            IEnumerable<TripDTO> allTrips = _tripRepository.GetAllTrips();
            var stopwatch = Stopwatch.StartNew();

            var result = CalculateFirstPassengerPeriods(allTrips);

            stopwatch.Stop();
            var executionTimeMilliseconds = stopwatch.ElapsedMilliseconds;

            return result ?? Enumerable.Empty<DriverDTO>();
        }

        private IEnumerable<DriverDTO> CalculateFirstPassengerPeriods(IEnumerable<TripDTO> trips)
        {
            var sortedTrips = trips.OrderBy(x => x.DriverId).ThenBy(x => x.PickUp).ToList();
            var passengerPeriods = new ConcurrentBag<DriverDTO>();

            var driverGroups = sortedTrips.GroupBy(x => x.DriverId).ToList();

            Parallel.ForEach(driverGroups, new ParallelOptions { MaxDegreeOfParallelism = 4 }, driverGroup =>
            {
                var driverTrips = driverGroup.OrderBy(x => x.PickUp).ToList();

                for (int i = 0; i < driverTrips.Count;)
                {
                    var period = new DriverDTO
                    {
                        DriverId = driverTrips[i].DriverId,
                        PayableTime = driverTrips[i].DropOff - driverTrips[i].PickUp
                    };

                    int j = i + 1;
                    while (j < driverTrips.Count)
                    {
                        if (driverTrips[j].PickUp < driverTrips[i].DropOff && driverTrips[j].DropOff > driverTrips[i].DropOff)
                        {
                            period.PayableTime += driverTrips[j].DropOff - driverTrips[i].DropOff;
                        }
                        else if (driverTrips[j].DropOff >= driverTrips[i].DropOff)
                        {
                            passengerPeriods.Add(period);
                            break;
                        }
                        j++;
                    }

                    if (j >= driverTrips.Count)
                    {
                        passengerPeriods.Add(period);
                    }

                    i = j;
                }
            });

            var groupedPassengerPeriods = passengerPeriods
                .GroupBy(p => p.DriverId)
                .Select(g => new DriverDTO
                {
                    DriverId = g.Key,
                    PayableTime = new TimeSpan(g.Sum(p => p.PayableTime.Ticks))
                });

            return groupedPassengerPeriods;
        }
    }
}
