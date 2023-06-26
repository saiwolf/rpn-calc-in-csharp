using CommandLine;

internal class CommandLineOptions
{
    [Option(
        shortName: 'e',
        longName: "expression",
        Required = true,
        HelpText = "Expression in RPN notation to evaluate.")]
    public string? Expression { get; set; }

    [Option(        
        shortName: 'v',
        longName: "verbose",
        Required = false,
        Default = false,
        HelpText = "Enables verbose output.")]
    public bool Verbose { get; set; }

    [Option(
        shortName: 'd',
        longName: "stackDump",
        Required = false,
        Default = false,
        HelpText = "Dumps the numerator/operator stack to the console.")]
    public bool DumpStack { get; set; }

    [Option(
        shortName: 't',
        longName: "tempVarDump",
        Required = false,
        Default = false,
        HelpText = "Dumps the temporary variable list to the console.")]
    public bool DumpVar { get; set; }
}
