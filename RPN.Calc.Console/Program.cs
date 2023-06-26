using RPN_Calc.Lib;

try
{
    if (string.IsNullOrEmpty(args[0]))
        throw new Exception("Expression is required.");
    if (args.Length > 1)
        throw new Exception("Only one expression is allowed at a time.");

    RPN rpn = new();
    rpn.Parse(args[0]);
    Console.WriteLine($"Result: {rpn.Peek()}");

    if (rpn.StackDumpInfo.Contains('{'))
        Console.WriteLine(rpn.StackDumpInfo);
    if (rpn.VarDumpInfo.Contains('{'))
        Console.WriteLine(rpn.VarDumpInfo);

    Console.WriteLine($"Expression: \"{args[0]}\"");
}
catch (Exception ex)
{
    Console.Error.WriteLine($"ERROR: {ex.Message}");
    Environment.Exit(-1);
}
