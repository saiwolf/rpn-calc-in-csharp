using CommandLine;
using CommandLine.Text;
using RPN_Calc.Lib;
using Serilog;

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateLogger();

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
        if (options.Verbose)
            Log.Information("Verbose logging enabled.\n");
        if (string.IsNullOrEmpty(options.Expression))
            throw new Exception("Expression is required.");

        if (options.DumpStack)
            options.Expression += (" ?");
        if (options.DumpVar)
            options.Expression += (" &");


        RPN rpn = new();
        rpn.Parse(options.Expression);
        Console.WriteLine($"Result: {rpn.Peek()}");

        if (options.DumpStack)
            Console.WriteLine(rpn.StackDumpInfo);
        if (options.DumpVar)
            Console.WriteLine(rpn.VarDumpInfo);
    }
    catch (Exception ex)
    {
        Log.Error($"{ex.Message}");        
    }
    finally
    {
        if (options.Verbose)
            Log.Information($"Expression: \"{options.Expression}\"");
    }
}