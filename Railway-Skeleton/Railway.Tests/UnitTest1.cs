using NUnit.Framework;
using System;
using System.Linq;
using System.Collections.Generic;

namespace Railway.Tests
{
    [TestFixture]
    public class Tests
    {
        [Test]
        public void Constructor_WithValidName_InitiallizesStation()
        {
            string stationName = "TestStation";
            
            RailwayStation station = new RailwayStation(stationName);

            Assert.AreEqual(stationName, station.Name);
            Assert.IsNotNull(station.ArrivalTrains);
            Assert.IsNotNull(station.DepartureTrains);
        }

        [Test]
        public void Constructor_WithInvalidName_ThrowsArgumentException()
        {
            string invalidName = null;

            Assert.Throws<ArgumentException>(() => new RailwayStation(invalidName));
        }
        [Test]
        public void NewArrivalOnABoard_AddsTrainToArrivalQueue()
        {
            string stationName = "TestStation";
            RailwayStation station = new RailwayStation(stationName);
            string trainInfo = "Train123";

            station.NewArrivalOnBoard(trainInfo);

            Assert.Contains(trainInfo, station.ArrivalTrains);
        }

        [Test]
        public void TrainHasArrived_ValidTrainInfo_ReturnsExcpectedMessage()
        {
            string stationName = "TestStation";
            RailwayStation station = new RailwayStation(stationName);
            string trainInfo = "Train123";

            station.NewArrivalOnBoard(trainInfo);

            string result = station.TrainHasArrived(trainInfo);

            Assert.AreEqual($"{trainInfo} is on the platform and will leave in 5 minutes.",result);
            Assert.IsEmpty(station.ArrivalTrains);
            Assert.Contains(trainInfo, station.DepartureTrains);
        }
        
        [Test]
        public void TrainHasArrived_InvalidTrainInfo_ReturnsErrorMessage()
        {
            string stationName = "TestStation";
            RailwayStation station = new RailwayStation(stationName);
            string trainInfo = "Train123";

            station.NewArrivalOnBoard("Train456");

            string result = station.TrainHasArrived(trainInfo);

            Assert.AreEqual($"There are other trains to arrive before {trainInfo}.", result);
            Assert.IsNotEmpty(station.ArrivalTrains);
            Assert.IsEmpty(station.DepartureTrains);
        }

        [Test]
        public void TrainHasLeft_ValidTrainInfo_RemoveTrainFromDepartureQueue()
        {
            string stationName = "TestStation";
            RailwayStation station = new RailwayStation(stationName);
            string trainInfo = "Train123";

            station.NewArrivalOnBoard(trainInfo);
            station.TrainHasArrived(trainInfo);

            bool result = station.TrainHasLeft(trainInfo);

            Assert.IsTrue(result);
            Assert.IsEmpty(station.DepartureTrains);
        }
        
        [Test]
        public void TrainHasLeft_InvalidTrainInfo_ReturnsFalse()
        {
            string stationName = "TestStation";
            RailwayStation station = new RailwayStation(stationName);
            string trainInfo = "Train123";

            bool result = station.TrainHasLeft(trainInfo);

            Assert.IsFalse(result);
        }


    }
}