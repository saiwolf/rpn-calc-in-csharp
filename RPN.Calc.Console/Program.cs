using RPN_Calc.Lib;

if (args.Length == 1)
{
    RPN rpn = new();
    rpn.Parse(args[0]);
    Console.WriteLine(rpn.Peek());
}
else
    Console.WriteLine("Help text goes here.");
