using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrainControllerTests
{
    /// <summary>
    /// Tests with more than 2 stations 
    /// </summary>
    public class FastTrainManyStationsTest
    {
        List<Passenger> passengers;
        TrainController controller;

        List<Passenger> resultingPassengerInfo;

        #region givens

        // train controller 
        private void givenFastTrains9Stations()
        {
            controller = TrainController.Create(9, 1, 6, 3);
        }

        private void givenFastTrain4Stations()
        {
            controller = TrainController.Create(4, 1, 2, 1);
        }

        // passengers 
        private void given1Passenger()
        {
            Passenger passenger = new Passenger(0, PassengerType.A, 1, 1, 2);
            passengers = new List<Passenger> { passenger };
        }

        private void given2Passengers()
        {
            Passenger passenger1 = new Passenger(0, PassengerType.A, 1, 4, 1);
            Passenger passenger2 = new Passenger(1, PassengerType.A, 1, 4, 3);

            passengers = new List<Passenger> { passenger1, passenger2 };
        }

        private void given3PassengersTypeA() 
        {
            Passenger passenger1 = new Passenger(0, PassengerType.A, 1, 8, 3);
            Passenger passenger2 = new Passenger(1, PassengerType.A, 1, 5, 9);
            Passenger passenger3 = new Passenger(2, PassengerType.A, 2, 6, 5);

            passengers = new List<Passenger> { passenger1, passenger2, passenger3 };
        }

        private void given3PassengersTypeB()
        {
            Passenger passenger1 = new Passenger(0, PassengerType.B, 1, 1, 3);
            Passenger passenger2 = new Passenger(1, PassengerType.B, 1, 1, 9);
            Passenger passenger3 = new Passenger(2, PassengerType.B, 1, 1, 5);

            passengers = new List<Passenger> { passenger1, passenger2, passenger3 };
        }

        private void given3PassengersMultipleTypes()
        {
            Passenger passenger1 = new Passenger(0, PassengerType.A, 1, 8, 3);
            Passenger passenger2 = new Passenger(1, PassengerType.B, 2, 7, 6);
            Passenger passenger3 = new Passenger(2, PassengerType.A, 2, 7, 6);

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
        private void thenFirstPassengerArrivedAtTime5()
        {
            Assert.Equal(0, resultingPassengerInfo[0].GetID());
            Assert.Equal(5, resultingPassengerInfo[0].GetTimeReachedDestination());
        }

        private void thenFirstPassengerArrivedAtTime6()
        {
            Assert.Equal(0, resultingPassengerInfo[0].GetID());
            Assert.Equal(6, resultingPassengerInfo[0].GetTimeReachedDestination());
        }

        private void thenFirstPassengerArrivedAtTime7()
        {
            Assert.Equal(0, resultingPassengerInfo[0].GetID());
            Assert.Equal(7, resultingPassengerInfo[0].GetTimeReachedDestination());
        }

        private void thenFirstPassengerArrivedAtTime14()
        {
            Assert.Equal(0, resultingPassengerInfo[0].GetID());
            Assert.Equal(14, resultingPassengerInfo[0].GetTimeReachedDestination());
        }

        private void thenSecondPassengerArrivedAtTime5()
        {
            Assert.Equal(1, resultingPassengerInfo[1].GetID());
            Assert.Equal(5, resultingPassengerInfo[1].GetTimeReachedDestination());
        }

        private void thenSecondPassengerArrivedAtTime8()
        {
            Assert.Equal(1, resultingPassengerInfo[1].GetID());
            Assert.Equal(8, resultingPassengerInfo[1].GetTimeReachedDestination());
        }

        private void thenSecondPassengerArrivedAtTime9()
        {
            Assert.Equal(1, resultingPassengerInfo[1].GetID());
            Assert.Equal(9, resultingPassengerInfo[1].GetTimeReachedDestination());
        }

        private void thenSecondPassengerArrivedAtTime14()
        {
            Assert.Equal(1, resultingPassengerInfo[1].GetID());
            Assert.Equal(14, resultingPassengerInfo[1].GetTimeReachedDestination());
        }
        private void thenThirdPassengerArrivedAtTime3()
        {
            Assert.Equal(2, resultingPassengerInfo[2].GetID());
            Assert.Equal(3, resultingPassengerInfo[2].GetTimeReachedDestination());
        }

        private void thenThirdPassengerArrivedAtTime4()
        {
            Assert.Equal(2, resultingPassengerInfo[2].GetID());
            Assert.Equal(4, resultingPassengerInfo[2].GetTimeReachedDestination());
        }

        private void thenThirdPassengerArrivedAtTime10()
        {
            Assert.Equal(2, resultingPassengerInfo[2].GetID());
            Assert.Equal(10, resultingPassengerInfo[2].GetTimeReachedDestination());
        }

        #endregion

        #region tests

        [Fact]
        public void SinglePassengerTest()
        {
            givenFastTrains9Stations();
            given1Passenger();

            whenSimulateProgram();

            thenFirstPassengerArrivedAtTime7();
        }

        [Fact]
        public void TwoPassengersTest()
        {
            // like example 4
            givenFastTrain4Stations();
            given2Passengers();

            whenSimulateProgram();

            thenFirstPassengerArrivedAtTime5();
            thenSecondPassengerArrivedAtTime5();
        }

        [Fact]
        public void ThreePassengersTypeATest()
        {
            // like example 2
            givenFastTrains9Stations();
            given3PassengersTypeA();

            whenSimulateProgram();

            thenFirstPassengerArrivedAtTime6();
            thenSecondPassengerArrivedAtTime8();
            thenThirdPassengerArrivedAtTime4();
        }

        [Fact]
        public void ThreePassengersTypeBTest()
        {
            givenFastTrains9Stations();
            given3PassengersTypeB();

            whenSimulateProgram();

            thenFirstPassengerArrivedAtTime14(); // will not board first train because is too crowded 
            thenSecondPassengerArrivedAtTime14();
            thenThirdPassengerArrivedAtTime10();
        }

        [Fact]
        public void ThreePassengersMultipleTypesTest()
        {
            // like example 5
            givenFastTrains9Stations();
            given3PassengersMultipleTypes();

            whenSimulateProgram();

            thenFirstPassengerArrivedAtTime6(); 
            thenSecondPassengerArrivedAtTime9();// will not board first train because is too crowded 
            thenThirdPassengerArrivedAtTime3();
        }

        #endregion
    }
}
