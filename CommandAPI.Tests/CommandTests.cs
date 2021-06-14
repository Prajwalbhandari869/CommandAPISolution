using System;
using Xunit;
using CommandAPI.Models;
namespace CommandAPI.Tests
{
    public class CommandTests :IDisposable
    {
        Command testCommand;
        public CommandTests()
        {
            testCommand = new Command
            {
                HowTo = "Do something awesome",
                Platform = "xUnit",
                CommandLine = "dotnet test"
            };
        }
        public void Dispose()
        {
            testCommand = null;
        }
        [Fact]
        public void CanChangeHowTo()
        {
            //Arrange
           
            //Act
            testCommand.HowTo = "Execute unit test";
            //Assert
            Assert.Equal("Execute unit test", testCommand.HowTo);
        }
        [Fact]
        public void CanChangePlatforn()
        {
            //Arrange

            //Acts
            testCommand.Platform = "Prajwal";
            //Assert
            Assert.Equal("Prajwal", testCommand.Platform);
        }
        [Fact]
        public void CanChangeCommandLine()
        {
            //Arrange

            //Acts
            testCommand.CommandLine = "Bhandari";
            Assert.Equal("Bhandari",testCommand.CommandLine);
        }

    }
}
