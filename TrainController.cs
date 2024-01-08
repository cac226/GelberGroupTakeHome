using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GelberGroupTakeHome
{
    /// <summary>
    /// A controller for all trains and passengers on a line. Simulates train/passenger behavior on a minute-by-minute basis. 
    /// </summary>
    public class TrainController
    {
        private int numberOfStations;
        private int minutesToTravelBetweenStations;
        private int frequencyOfTrainDepartures;
        private int trainCapacity;

        private List<Train> trains;

        private int minuteTimestamp;

        private TrainController(int numberOfStations, int minutesToTravelBetweenStations, int frequencyOfTrainDepartures, int trainCapacity)
        {
            this.numberOfStations = numberOfStations;
            this.minutesToTravelBetweenStations = minutesToTravelBetweenStations;
            this.frequencyOfTrainDepartures = frequencyOfTrainDepartures;
            this.trainCapacity = trainCapacity;

            trains = new List<Train>();

            minuteTimestamp = 0;
        }

        public static TrainController Create(int numberOfStations, int minutesToTravelBetweenStations, int frequencyOfTrainDepartures, int trainCapacity)
        {
            if(numberOfStations < 2 ||  // 1 station is a case that wouldn't require any simulation or trains, and thus is invalid
                minutesToTravelBetweenStations < 1 || 
                frequencyOfTrainDepartures < 1 || 
                trainCapacity < 1)
            {
                throw new ArgumentException("Invalid train station data");
            }
            return new TrainController(numberOfStations, minutesToTravelBetweenStations, frequencyOfTrainDepartures, trainCapacity);
        }

        public List<Passenger> Simulate(List<Passenger> allPassengers)
        {
            if(!arePassengersValid(allPassengers))
            {
                throw new ArgumentException("Invalid passenger data");
            }

            int totalPassengers = allPassengers.Count;
            
            /* 
             NOTE: if we wanted to improve performance (especially for scenarios with large number of passengers or stations), we could instead organize 
            passengers based on what station they start at (perhaps in a dictionary with arrival stations as keys.) That way, we would not have to look 
            through all passengers in order to find the ones that were boarding at a given station. 
            
            However, for the example cases, which all had fewer than 10 passengers/train stations, using a list is sufficient. 
             */
            List<Passenger> passengersNotOnTrain = allPassengers;
            List<Passenger> passengersThatCompletedJourney = new List<Passenger>();

            while(passengersThatCompletedJourney.Count < totalPassengers)
            {
                simulateMinute(passengersNotOnTrain, passengersThatCompletedJourney);
            }

            return sortPassengersById(passengersThatCompletedJourney);
        }

        private void simulateMinute(List<Passenger> passengersNotOnTrain, List<Passenger> passengersThatCompletedJourney)
        {
            if (minuteTimestamp % frequencyOfTrainDepartures == 0)
            {
                addTrains();
            }

            List<Train> trainsToRemove = updateTrains(passengersNotOnTrain, passengersThatCompletedJourney);

            foreach(Train toRemove in trainsToRemove)
            {
                trains.Remove(toRemove);
            }

            minuteTimestamp++;
        }

        private List<Train> updateTrains(List<Passenger> passengersNotOnTrain, List<Passenger> passengersThatCompletedJourney)
        {
            List<Train> trainsToRemove = new List<Train>();

            foreach (Train train in trains)
            {
                train.UpdateStation(minuteTimestamp);

                if (train.IsAtStation(minuteTimestamp))
                {
                    // passengers leave 
                    List<Passenger> exitedPassengers = train.RemoveExitingPassengers(minuteTimestamp);
                    passengersThatCompletedJourney.AddRange(exitedPassengers);

                    // passengers enter 
                    List<Passenger> passengersAttemptingToBoard = getPassengersAttemptingToBoardAtStation(passengersNotOnTrain, train.GetCurrentStation());
                    foreach (Passenger passenger in passengersAttemptingToBoard)
                    {
                        if (passenger.CanBoardTrain(train))
                        {
                            passengersNotOnTrain.Remove(passenger);
                            train.AddPassenger(passenger);
                        }
                    }

                    // if needed, remove train
                    if (train.IsAtFinalStation())
                    {
                        trainsToRemove.Add(train);
                    }
                }
            }

            return trainsToRemove;
        }

        private List<Passenger> getPassengersAttemptingToBoardAtStation(List<Passenger> possiblePassengers, int station)
        {
            List<Passenger> unsortedPassengers = possiblePassengers
                                                    .Where(passenger => passenger.GetStartingStation() == station 
                                                        && passenger.GetArrivalTime() <= minuteTimestamp)
                                                    .ToList();

            return sortPassengersByBoardOrder(unsortedPassengers, station);
        }

        /// <summary>
        /// If two or more passengers attempt to board a train at the same time, the passenger whose destination is more distant boards first. If there is a tie, type A passengers board first.
        /// </summary>
        private List<Passenger> sortPassengersByBoardOrder(List<Passenger> passengers, int station)
        {
            return passengers.OrderByDescending(passenger => Math.Abs(station - passenger.GetDestinationStation()))
                             .ThenBy(passenger => passenger.GetPassengerType())
                             .ToList();
        }
        
        private List<Passenger> sortPassengersById(List<Passenger> passengers)
        {
            return passengers.OrderBy(passenger => passenger.GetID()).ToList();
        }

        private void addTrains()
        {
            Train forwardTrain = new Train(trainCapacity, minutesToTravelBetweenStations, 1, minuteTimestamp, numberOfStations);
            Train backwardsTrain = new Train(trainCapacity, minutesToTravelBetweenStations * -1, numberOfStations, minuteTimestamp, numberOfStations);
            trains.Add(forwardTrain);
            trains.Add(backwardsTrain);
        }

        private bool arePassengersValid(List<Passenger> passengers)
        {
            foreach(Passenger passenger in passengers)
            {
                // validate arrival times
                if(passenger.GetArrivalTime() <= 0)
                {
                    return false;
                }

                // validate stations 
                if(!isStationInRange(passenger.GetStartingStation()) || !isStationInRange(passenger.GetDestinationStation()))
                {
                    return false;
                }
            }

            return true;
        }

        private bool isStationInRange(int station)
        {
            return 1 <= station && station <= numberOfStations;
        }
    }
}
