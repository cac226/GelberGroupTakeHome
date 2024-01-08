

namespace TrainControllerTests
{
    /// <summary>
    /// Tests with only 2 stations 
    /// </summary>
    public class SimpleTrainControllerTests
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

        private void givenTrainControllerSpeedAndCapacity1()
        {
            controller = TrainController.Create(2, 1, 2, 1);
        }

        // passengers 
        private void given1Passenger()
        {
            Passenger passenger = new Passenger(0, PassengerType.A, 1, 1, 2);
            passengers = new List<Passenger> { passenger };
        }

        private void given2PassengersArriveAtSameTime()
        {
            Passenger passenger1 = new Passenger(0, PassengerType.A, 1, 1, 2);
            Passenger passenger2 = new Passenger(1, PassengerType.B, 1, 1, 2);

            passengers = new List<Passenger> { passenger1, passenger2 };
        }

        private void given3PassengersArriveAtSameTime()
        {
            Passenger passenger1 = new Passenger(0, PassengerType.A, 1, 1, 2);
            Passenger passenger2 = new Passenger(1, PassengerType.B, 1, 1, 2);
            Passenger passenger3 = new Passenger(2, PassengerType.B, 1, 1, 2);

            passengers = new List<Passenger> { passenger1, passenger2, passenger3 };
        }

        private void given3PassengersDifferentStations()
        {
            Passenger passenger1 = new Passenger(0, PassengerType.A, 1, 2, 1);
            Passenger passenger2 = new Passenger(1, PassengerType.A, 2, 1, 2);
            Passenger passenger3 = new Passenger(2, PassengerType.A, 3, 1, 2);

            passengers = new List<Passenger> { passenger1, passenger2, passenger3 };
        }

        #endregion

        #region whens 

        private void whenSimulateProgram()
        {
            resultingPassengerInfo = controller.Simulate(passengers);
        }

        #endregion

        #region thens

        private void thenFirstPassengerArrivedAtTime4()
        {
            Assert.Equal(0, resultingPassengerInfo[0].GetID());
            Assert.Equal(4, resultingPassengerInfo[0].GetTimeReachedDestination());
        }

        private void thenFirstPassengerArrivedAtTime3()
        {
            Assert.Equal(0, resultingPassengerInfo[0].GetID());
            Assert.Equal(3, resultingPassengerInfo[0].GetTimeReachedDestination());
        }

        private void thenSecondPassengerArrivedAtTime4()
        {
            Assert.Equal(1, resultingPassengerInfo[1].GetID());
            Assert.Equal(4, resultingPassengerInfo[1].GetTimeReachedDestination());
        }

        private void thenSecondPassengerArrivedAtTime3()
        {
            Assert.Equal(1, resultingPassengerInfo[1].GetID());
            Assert.Equal(3, resultingPassengerInfo[1].GetTimeReachedDestination());
        }

        private void thenThirdPassengerArrivedAtTime6()
        {
            Assert.Equal(2, resultingPassengerInfo[2].GetID());
            Assert.Equal(6, resultingPassengerInfo[2].GetTimeReachedDestination());
        }

        private void thenThirdPassengerArrivedAtTime5()
        {
            Assert.Equal(2, resultingPassengerInfo[2].GetID());
            Assert.Equal(5, resultingPassengerInfo[2].GetTimeReachedDestination());
        }
        #endregion

        #region tests
        [Fact]
        public void SinglePassengerTest()
        {
            // like example 1
            givenTrainControllerAll2s();
            given1Passenger();

            whenSimulateProgram();

            thenFirstPassengerArrivedAtTime4();
        }

        [Fact]
        public void TwoPassengersTest()
        {
            givenTrainControllerAll2s();
            given2PassengersArriveAtSameTime();

            whenSimulateProgram();

            thenFirstPassengerArrivedAtTime4();
            thenSecondPassengerArrivedAtTime4();
        }

        [Fact] 
        public void ThreePassengersTest()
        {
            givenTrainControllerAll2s();
            given3PassengersArriveAtSameTime();

            whenSimulateProgram();

            thenFirstPassengerArrivedAtTime4();
            thenSecondPassengerArrivedAtTime4();
            thenThirdPassengerArrivedAtTime6(); // first train will be too fill for 3rd passenger to board 
        }

        [Fact]
        public void FasterTrainTest()
        {
            // like example 3
            givenTrainControllerSpeedAndCapacity1();
            given3PassengersDifferentStations();

            whenSimulateProgram();

            thenFirstPassengerArrivedAtTime3();
            thenSecondPassengerArrivedAtTime3();
            thenThirdPassengerArrivedAtTime5();
        }
        #endregion
    }
}