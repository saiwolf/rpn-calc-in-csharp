using CommandLine;
using CommandLine.Text;
using RPN_Calc.Lib;

Parser parser = new(with => with.HelpWriter = null);
ParserResult<CommandLineOptions> parserResult = parser.ParseArguments<CommandLineOptions>(args);
parserResult
  .WithParsed(Run)
  .WithNotParsed(_ => DisplayHelp(parserResult));

static void DisplayHelp<T>(ParserResult<T> result)
{
    var helpText = HelpText.AutoBuild(result, h =>
    {
        h.AdditionalNewLineAfterOption = false;
        return HelpText.DefaultParsingErrorsHandler(result, h);
    }, e => e);
    Console.WriteLine(helpText);
}

static void Run(CommandLineOptions options)
{
    try
    {
        if (string.IsNullOrEmpty(options.Expression))
            throw new Exception("Expression is required.");


        RPN rpn = new();
        rpn.Parse(options.Expression);
        Console.WriteLine($"Result: {rpn.Peek()}");

        if (rpn.StackDumpInfo.Contains('{'))
            Console.WriteLine(rpn.StackDumpInfo);
        if (rpn.VarDumpInfo.Contains('{'))
            Console.WriteLine(rpn.VarDumpInfo);

        Console.WriteLine($"Expression: \"{options.Expression}\"");
    }
    catch (Exception ex)
    {
        Console.Error.WriteLine($"ERROR: {ex.Message}");
        Environment.Exit(-1);
    }
}