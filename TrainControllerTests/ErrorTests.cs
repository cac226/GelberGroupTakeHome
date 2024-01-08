using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrainControllerTests
{
    /// <summary>
    /// Testing that the appropriate exceptions are thrown given bad input data 
    /// </summary>
    public class ErrorTests
    {
        List<Passenger> passengers;
        TrainController controller;

        List<Passenger> resultingPassengerInfo;

        #region givens

        // train controller 
        private void givenTrainControllerAll2s()
        {
            controller = TrainController.Create(2, 2, 2, 2);
        }

        private void givenOnlyOneStation()
        {
            controller = TrainController.Create(1, 2, 2, 2);
        }

        private void givenInstantTravelTime()
        {
            controller = TrainController.Create(2, 0, 2, 2);
        }

        private void givenTrainsDepartEvery0Seconds()
        {
            controller = TrainController.Create(2, 2, 0, 2);
        }

        private void givenTrainsHave0Capacity()
        {
            controller = TrainController.Create(2, 2, 2, 0);
        }

        // passengers 
        private void givenPassengerWithDestinationStation9()
        {
            Passenger passenger = new Passenger(0, PassengerType.A, 1, 1, 9);
            passengers = new List<Passenger> { passenger };
        }

        private void givenPassengerWithArrivalStation9()
        {
            Passenger passenger = new Passenger(0, PassengerType.A, 1, 9, 1);
            passengers = new List<Passenger> { passenger };
        }

        private void givenPassengerWithNegativeArrivalTime()
        {
            Passenger passenger = new Passenger(0, PassengerType.A, -1, 2, 1);
            passengers = new List<Passenger> { passenger };
        }

        #endregion

        #region whens 

        private void whenSimulateProgram()
        {
            resultingPassengerInfo = controller.Simulate(passengers);
        }

        #endregion

        #region tests
        [Fact]
        public void InvalidPassengerDestinationTest()
        {   
            givenTrainControllerAll2s();
            givenPassengerWithDestinationStation9();

            Assert.Throws<ArgumentException>(whenSimulateProgram);
        }

        [Fact]
        public void InvalidPassengerArrivalStationTest()
        {
            givenTrainControllerAll2s();
            givenPassengerWithArrivalStation9();

            Assert.Throws<ArgumentException>(whenSimulateProgram);
        }

        [Fact]
        public void InvalidPassengerArrivalTimeTest()
        {
            givenTrainControllerAll2s();
            givenPassengerWithNegativeArrivalTime();

            Assert.Throws<ArgumentException>(whenSimulateProgram);
        }

        [Fact]
        public void InvalidTrainCreationTests()
        {
            Assert.Throws<ArgumentException>(givenOnlyOneStation);
            Assert.Throws<ArgumentException>(givenTrainsDepartEvery0Seconds);
            Assert.Throws<ArgumentException>(givenInstantTravelTime);
            Assert.Throws<ArgumentException>(givenTrainsHave0Capacity);
        }
        #endregion 
    }
}
