using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace GelberGroupTakeHome
{
    /// <summary>
    /// Represents a train that goes from one station to another. Trains have a constant speed and direction 
    /// </summary>
    public class Train
    {
        private int maxCapacity;
        private List<Passenger> passengers;
        // positive timeBetweenStations indicates a train moving from station 1 to station n
        // negative timeBetweenStations indicates a train moving from station n to station 1 
        private int timeBetweenStations; 
        private int currentStation;
        private int timeArrivedAtCurrentStation;
        private int numberOfStations;

        public Train(int maxCapacity, int timeBetweenStations, int currentStation, int timeArrivedAtCurrentStation, int numberOfStations)
        {
            this.maxCapacity = maxCapacity;
            passengers = new List<Passenger>();
            this.timeBetweenStations = timeBetweenStations;
            this.currentStation = currentStation;
            this.timeArrivedAtCurrentStation = timeArrivedAtCurrentStation;
            this.numberOfStations = numberOfStations;
        }

        public void UpdateStation(int currentTimestamp)
        {
            if(timeArrivedAtCurrentStation < currentTimestamp && timeArrivedAtCurrentStation + Math.Abs(timeBetweenStations) == currentTimestamp)
            {
                timeArrivedAtCurrentStation = currentTimestamp;

                if(IsGoingForward())
                {
                    currentStation++;
                } else
                {
                    currentStation--;
                }
            }
        }

        public bool IsAtFinalStation()
        {
            if(IsGoingForward())
            {
                return currentStation == numberOfStations;
            }
            return currentStation == 1;
        }

        public void AddPassenger(Passenger passenger)
        {
            passengers.Add(passenger);
        }

        /// <summary>
        /// Checks for passengers that are exiting at the trains current station, removes them from the trains internal passenger list, 
        /// and returns the passengers that exited. 
        /// </summary>
        public List<Passenger> RemoveExitingPassengers(int currentTimestamp)
        {
            List<Passenger> exitingPassengers = passengers.Where(passenger => passenger.ShouldExitTrain(this)).ToList();

            foreach(Passenger exitingPassenger in exitingPassengers)
            {
                exitingPassenger.ReachedStationAtTimestamp(currentTimestamp);
                passengers.Remove(exitingPassenger);
            }

            return exitingPassengers;
        }

        public bool IsAtStation(int currentTimestamp)
        {
            return timeArrivedAtCurrentStation == currentTimestamp;
        }

        public int GetCurrentStation()
        {
            return currentStation;
        }

        public bool HasOpenSeat()
        {
            return maxCapacity - passengers.Count >= 1;
        }

        public bool IsHalfFullOrEmptier()
        {
            return maxCapacity >= passengers.Count * 2;
        }

        /// <summary>
        /// Returns true if the train is going from station 1 towards station n, false otherwise
        /// </summary>
        public bool IsGoingForward()
        {
            return timeBetweenStations > 0;
        }
    }
}
