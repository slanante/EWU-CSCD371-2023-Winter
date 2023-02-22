namespace Calculate.Tests;
using System;

[TestClass]
public class CalculatorTests
{
    [TestMethod]
    public void TestAdd()
    {
        int result = Calculator.Add(2, 3);
        Assert.AreEqual(5, result);
    }

    [TestMethod]
    public void TestSubtract()
    {
        int result = Calculator.Subtract(6, 4);
        Assert.AreEqual(2, result);
    }

    [TestMethod]
    public void TestMultiply()
    {
        int result = Calculator.Multiply(5, 4);
        Assert.AreEqual(20, result);
    }

    [TestMethod]
    public void TestDivide()
    {
        int result = Calculator.Divide(10, 2);
        Assert.AreEqual(5, result);
    }

    [TestMethod]
    public void TestTryCalculate()
    {
        // Test valid input
        bool success = Calculator.TryCalculate("2 + 3", out int result);
        Assert.IsTrue(success);
        Assert.AreEqual(5, result);

        // Test invalid input
        success = Calculator.TryCalculate("invalid input", out result);
        Assert.IsFalse(success);
    }
}