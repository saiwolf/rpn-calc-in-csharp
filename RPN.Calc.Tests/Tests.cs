using Calc.Src;

namespace Calc.Tests;

public class Tests
{
    [Test]
    public void BasicNotationParsing()
    {
        using RPN rpn = new();
        rpn.Parse("5 2 + -3 - 10 +");
        Assert.That(rpn.Peek(), Is.EqualTo("20"));
    }

    [Test]
    public void ManualOperations()
    {
        using RPN rpn = new();
        rpn.Push("10");
        Assert.That(rpn.Peek(), Is.EqualTo("10"));
        rpn.Push("99");
        Assert.That(rpn.Peek(), Is.EqualTo("99"));
        rpn.Add();
        Assert.That(rpn.Peek(), Is.EqualTo("109"));
    }

    [Test]
    public void VariableTesting()
    {
        using RPN rpn = new();
        rpn.Parse("50 20 + !temp");
        rpn.Pop();
        rpn.Parse("2 @temp *");
        Assert.That(rpn.Peek(), Is.EqualTo("140"));
    }
}