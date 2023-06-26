using CommandLine;

internal class CommandLineOptions
{
    [Option(
        shortName: 'e',
        longName: "expression",
        Required = true,
        HelpText = "Expression in RPN notation to evaluate.")]
    public string? Expression { get; set; }
}
