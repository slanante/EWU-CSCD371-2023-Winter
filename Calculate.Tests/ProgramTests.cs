namespace Calculate.Tests;
using System;

[TestClass]
public class ProgramTests
{
    [TestMethod]
    public void ReadLine_Expected_Behavior()
    {
        // Arrange
        Program program = new Program();

        // Act
        string input = "Apple sauce comes from apples";
        Console.SetIn(new System.IO.StringReader(input));
        string result = program.ReadLine?.Invoke();

        // Assert
        Assert.AreEqual(input, result);
    }

    [TestMethod]
    public void WriteLine_Expected_Behavior()
    {
        //Arrange
        Program program = new Program();

        //Assert
        string output = "Babies eat applesauce";
        TextWriter writer = new System.IO.StringWriter();
        Console.SetOut(writer);
        program.WriteLine.Invoke(output);

        Assert.AreEqual(output + Environment.NewLine, writer.ToString());
    }
}
