using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CanHazFunny.Tests
{
    [TestClass]
    public class JesterTests
    {
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
    }
}
