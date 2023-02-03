using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace CanHazFunny.Tests
{
    [TestClass]
    public class JesterTests
    {
        private Mock<JokeService>? _mockGetJoke;
        private Mock<JokeWriter>? _mockWriteJoke;

        [TestInitialize]
        public void TestInitialize()
        {
            _mockGetJoke = new();
            _mockWriteJoke = new();
        }



        [TestMethod]
        public void GetJoke_CreateJoke_ReturnsJoke()
        {
            // Arrange

            JokeService testJoke = new();

            // Act

            string generatedJoke = testJoke.GetJoke();

            // Assert

            Assert.IsNotNull(generatedJoke);
        }

        [TestMethod]
        public void TellJoke_GenAndWrite_DoesNotContainChuck()
        {
            // Arrange

            JokeService testJoke = new();
            JokeWriter testWriter = new();
            Jester testJester = new Jester(testJoke, testWriter);

            // Act

            testJester.TellJoke();
            
            // Assert

            Assert.IsFalse((testJoke.Joke!).Contains("Chuck Norris"));
        }

        [TestMethod]
        public void FilterJoke_If_ChuckNorris()
        {
            // Arrange
            Jester jester = new Jester(_mockGetJoke!.Object, _mockWriteJoke!.Object!);
            _mockGetJoke.SetupSequence(joke => joke.GetJoke()).Returns("Chuck Norris");
            // Act
            jester.TellJoke();
            
            // Assert
            _mockWriteJoke.Verify(joke => joke.SayJoke("Chuck Norris"), Times.Never);
        }
    }
}
