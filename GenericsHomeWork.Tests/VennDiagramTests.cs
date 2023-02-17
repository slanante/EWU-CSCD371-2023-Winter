using System;
using GenericsHomework;


namespace NodeTests
{
    [TestClass]
	public class VennDiagramTests
	{
        [TestMethod]
        public void OutputTheNameOfVennDiag_Correct()
        {
            VennDiagram<Circle<string>> vennDiag = new VennDiagram<Circle<string>>();

            string actualName = vennDiag.Name;

            Assert.AreEqual("Venn Diagram", actualName);
        }

        [TestMethod]
        public void ToString_ReturnsCorrectValue_WhenCirclesNotEmpty()
        {
            // Arrange
            var circles = new List<int> { 1, 2, 3 };
            var vennDiagram = new VennDiagram<int>("My Venn Diagram") { Circles = circles };

            // Act
            var actualResult = vennDiagram.ToString();

            // Assert
            var expectedResult = "1 2 3 ";
            Assert.AreEqual(expectedResult, actualResult);
        }



        [TestMethod]
        public void VerifyItemsOfCircle_Correct()
        {
            Circle<string> circle = new Circle<string>("Birds", new string[] { "Penguin", "Chicken", "Hummingbird" });

            string result = circle.ToString();
            string answer = "Penguin Chicken Hummingbird ";

            Assert.IsNotNull(result);
            Assert.AreEqual(result, answer);
        }

        [TestMethod]
        public void VerifyVennIntersection_Correct()
        {
            Circle<string> circle = new Circle<string>("Birds", new string[] { "Penguin", "Chicken", "Hummingbird" });
            Circle<string> circle2 = new Circle<string>("Thanksgiving Food", new string[] { "Turkey", "Chicken", "Mashed Potatoes" });
            string[] intersect = circle.Intersect(circle2);
            Assert.AreEqual(intersect[0], "Chicken");
        }
    }
}

