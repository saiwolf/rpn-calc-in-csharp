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
}
