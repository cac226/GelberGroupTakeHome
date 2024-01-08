using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GelberGroupTakeHome
{
    /// <summary>
    /// Converts a .txt file into train and passenger data. 
    /// 
    /// Checks for syntax/formatting errors, but does not check for logical errors (e.g. does not check for a passenger 
    /// destined for station 5 when only 2 stations exist) as those are handled in the TrainController class
    /// </summary>
    internal class TrainFileReader
    {
        private string fileLocation;
        private TrainController trainController;
        private List<Passenger> passengers;

        public TrainFileReader(string fileLocation)
        {
            this.fileLocation = fileLocation;
        }

        public TrainController GetTrainController()
        {
            if(trainController != null)
            {
                return trainController; 
            }

            readFile();
            return trainController;
        }

        public List<Passenger> GetPassengers()
        {
            if(passengers != null)
            {
                return passengers;
            }
            readFile();
            return passengers;
        }

        private void readFile()
        {
            using(StreamReader sr = new StreamReader(fileLocation)) 
            {
                string line = sr.ReadLine();
                // get train controller info 
                trainController = createTrainControllerFromString(line);

                int passengerId = 1;
                passengers = new List<Passenger>();
                // get passenger info 
                while ((line = sr.ReadLine()) != null)
                {
                    Passenger passenger = createPassengerFromString(line, passengerId);
                    passengerId++;
                    passengers.Add(passenger);
                }
            }
        }

        private TrainController createTrainControllerFromString(string str)
        {
            string[] inputs = str.Split(' ');
            if(inputs.Length != 4)
            {
                throw new ArgumentException("First line of file must have 4 inputs (number of stations, travel time between stations, frequency of train departures, train capacity)");
            }

            try
            {
                int stationCount = Int32.Parse(inputs[0]);
                int travelTime = Int32.Parse(inputs[1]);
                int departureFrequency = Int32.Parse(inputs[2]);
                int trainCapacity = Int32.Parse(inputs[3]);

                return TrainController.Create(stationCount, travelTime, departureFrequency, trainCapacity);
            } catch (Exception e)
            {
                throw new ArgumentException("Inputs must be numbers (except for passenger type)");
            }
        }

        private Passenger createPassengerFromString(string str, int id)
        {
            string[] inputs = str.Split(" ");
            if (inputs.Length != 4)
            {
                throw new ArgumentException("Each passenger must have 4 inputs (type, arrival time, destination station, starting station)");
            }

            try
            {
                PassengerType passengerType = Enum.Parse<PassengerType>(inputs[0], true);
                if (!Enum.IsDefined(passengerType))
                {
                    throw new ArgumentException();
                }

                int arrivalTime = Int32.Parse(inputs[1]);
                int destination = Int32.Parse(inputs[2]);
                int arrivalStation = Int32.Parse(inputs[3]);

                return new Passenger(id, passengerType, arrivalTime, arrivalStation, destination);
            }
            catch (Exception e)
            {
                throw new ArgumentException("Invalid passenger data input");
            }
        }
    }
}
