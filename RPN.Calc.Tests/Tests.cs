using Calc.Src;

namespace Calc.Tests;

public class Tests
{
    [Test]
    public void BasicNotationParsing()
    {
        using RPN rpn = new();
        rpn.Parse("5 2 + -3 - 10 +"); // (5+2) - (-3) + 10 = 20
        Assert.That(rpn.Peek(), Is.EqualTo("20"));
    }

    [Test]
    public void ManualOperations()
    {
        using RPN rpn = new();
        rpn.Push("10"); // Push '10' to the top of the stack.
        Assert.That(rpn.Peek(), Is.EqualTo("10"));
        rpn.Push("99"); // Push '99' to the top of the stack.
        Assert.That(rpn.Peek(), Is.EqualTo("99"));
        rpn.Add(); // 99 + 10 = 109 ('99' is at the top of the stack, followed by '10')
        Assert.That(rpn.Peek(), Is.EqualTo("109"));
    }

    [Test]
    public void VariableTesting()
    {
        using RPN rpn = new();
        rpn.Parse("50 20 + !temp"); // 50 + 20 = 70 <-- Store result in temporary variable named 'temp'.
        rpn.Pop(); // Pops '70' off the stack; which should now be empty.
        rpn.Parse("2 @temp *"); // Retrieve 'temp' var, which should be '70'. 2 * `temp`(70) = 140.
        Assert.That(rpn.Peek(), Is.EqualTo("140"));
    }
}