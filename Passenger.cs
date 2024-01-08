using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GelberGroupTakeHome
{
    /// <summary>
    /// Represents a passenger that can ride trains
    /// </summary>
    public class Passenger
    {
        private int id;
        private PassengerType type;
        private int arrivalTime;
        private int reachedDestinationTime;
        private int startingStation;
        private int destinationStation;

        public Passenger(int id, PassengerType type, int arrivalTime, int startingStation, int destinationStation)
        {
            this.id = id;
            this.type = type;
            this.arrivalTime = arrivalTime;
            this.startingStation = startingStation;
            this.destinationStation = destinationStation;
            reachedDestinationTime = -1; // hasn't reached destination yet
        }

        public bool CanBoardTrain(Train train)
        {
            // verify train + passenger are going in the same direction  
            bool isPassengerGoingForward = startingStation < destinationStation;

            if(train.IsGoingForward() != isPassengerGoingForward)
            {
                return false;
            }

            // verify train capacity is acceptable to passenger
            switch(type)
            {
                case PassengerType.A:
                    return train.HasOpenSeat();
                
                case PassengerType.B:
                default:
                    return train.IsHalfFullOrEmptier();
            }
        }

        public void ReachedStationAtTimestamp(int arrivalTime)
        {
            reachedDestinationTime = arrivalTime;
        }

        public bool ShouldExitTrain(Train train)
        {
            return train.GetCurrentStation() == destinationStation;
        }

        public int GetID()
        {
            return id;
        }

        public int GetStartingStation()
        {
            return startingStation;
        }

        public int GetDestinationStation()
        {
            return destinationStation;
        }

        public int GetArrivalTime()
        {
            return arrivalTime;
        }

        public int GetTimeReachedDestination()
        {
            return reachedDestinationTime;
        }

        public PassengerType GetPassengerType()
        {
            return type;
        }

        public override string ToString()
        {
            return "Passenger " + id + " arrived at minute " + reachedDestinationTime;
        }
    }
}
