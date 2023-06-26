using System.Text;

namespace RPN_Calc.Lib;

public sealed class RPN : IDisposable
{
    /// <summary>
    /// <para>The main stack. Numbers and operators go here.</para>
    /// </summary>
    private Stack<string> Stack { get; set; }
    /// <summary>
    /// <para>A Dictionary used to hold temporary variables for advanced processing.</para>
    /// <para>Example: <c>Vars.Add(tempVarName, tempVarValue);</c></para>
    /// </summary>
    private Dictionary<string, string> Vars { get; set; }

    /// <summary>
    /// <para>Debug info about the stack.</para>
    /// </summary>
    public string? StackDumpInfo { get; set; }
    /// <summary>
    /// <para>Debug info about temporary variables.</para>
    /// </summary>
    public string? VarDumpInfo { get; set; }

    public RPN()
    {
        Stack = new();
        Vars = new();
        StackDumpInfo = "STACK:\n";
        VarDumpInfo = "TEMP VARS:\n";
    }

    /// <summary>
    /// <para>Clears both <see cref="Stack"/> and <see cref="Vars"/> on Dispose.</para>
    /// </summary>
    public void Dispose() => Wipe();

    /// <summary>
    /// <para>Inserts a value at the top of <see cref="Stack"/>.</para>
    /// </summary>
    /// <param name="value">The value to push onto <see cref="Stack"/>.</param>
    public void Push(string value)
    {
        Stack.Push(value);
        StackDump();
        VarDump();
    }

    /// <summary>
    /// <para>Removes the first entry from <see cref="Stack"/> and returns said value.</para>
    /// </summary>
    /// <returns>First entry from <see cref="Stack"/> after its removal.</returns>
    public string Pop() 
    {
        StackDump();
        VarDump();
        return Stack.Pop();
    }

    /// <summary>
    /// <para>Adds the first two values on <see cref="Stack"/> and
    /// pushes the sum to the top of <see cref="Stack"/>.</para>
    /// </summary>
    public void Add()
    {
        double x = double.Parse(Pop());
        double y = double.Parse(Pop());
        double result = x + y;
        Stack.Push(result.ToString());
    }

    /// <summary>
    /// <para>Subtracts the first two values on <see cref="Stack"/> and
    /// pushes the difference to the top of <see cref="Stack"/>.</para>
    /// <para>The equation here is <c>Stack[1] - Stack[0]</c> due the stack ordering.</para>
    /// </summary>
    public void Sub()
    {
        double x = double.Parse(Pop());
        double y = double.Parse(Pop());
        double result = y - x;
        Stack.Push(result.ToString());
    }

    /// <summary>
    /// <para>Multiplies the first two values on <see cref="Stack"/> and
    /// pushes the result to the top of <see cref="Stack"/>.</para>
    /// </summary>
    public void Mul()
    {
        double x = double.Parse(Pop());
        double y = double.Parse(Pop());
        double result = x * y;
        Stack.Push(result.ToString());
    }

    /// <summary>
    /// <para>Divides the first two values on <see cref="Stack"/> and
    /// pushes the result to the top of <see cref="Stack"/>.</para>
    /// <para>The equation here is <c>Stack[1] / Stack[0]</c> due the stack ordering.</para>
    /// </summary>
    public void Div()
    {
        double x = double.Parse(Pop());
        double y = double.Parse(Pop());
        double result = y / x;
        Stack.Push(result.ToString());
    }

    public void Exponent()
    {
        double baseVal = double.Parse(Pop());
        double power = double.Parse(Pop());
        double result = Math.Pow(baseVal, power);
        Stack.Push(result.ToString());
    }

    /// <summary>
    /// <para>
    /// Returns the value at the top of <see cref="Stack"/> without removing it.
    /// </para>
    /// </summary>
    /// <returns>The value at the top of <see cref="Stack"/>.</returns>
    /// <exception cref="InvalidOperationException"></exception>
    public string Peek() => Stack.Peek();

    /// <summary>
    /// <para>Prints the contents of <see cref="Stack"/> to standard output.</para>
    /// </summary>
    public void StackDump()
    {
        if (Stack.Any())
        {
            StringBuilder sb = new();
            sb.Append("{\n");
            foreach ((string value, int index) in Stack.WithIndex())
                sb.Append($"  Stack[{index}] = {value}\n");
            sb.Append("}\n");
            StackDumpInfo = StackDumpInfo += sb.ToString();
        }
    }

    /// <summary>
    /// <para>Prints the contents of <see cref="Vars"/> to standard output.</para>
    /// </summary>
    public void VarDump()
    {
        if (Vars.Any())
        {
            StringBuilder sb = new();
            sb.Append("{\n");
            foreach ((string key, string value) in Vars)
                sb.Append($"  Key: {key} = {value}");
            sb.Append("}\n");
            VarDumpInfo = VarDumpInfo += sb.ToString();
        }
    }

    /// <summary>
    /// <para>Removes all values from <see cref="Stack"/>.</para>
    /// </summary>
    public void Clear() => Stack.Clear();

    /// <summary>
    /// <para>Removes all values from <see cref="Stack"/>.</para>
    /// <para>Removes all values from <see cref="Vars"/>.</para>
    /// </summary>
    public void Wipe()
    {
        Clear();
        Vars.Clear();
    }

    /// <summary>
    /// <para>Exchanges the position of the first two values on <see cref="Stack"/>.</para>
    /// <para>If <see cref="Stack"/> had <c>10, 2</c>, then <see cref="Exchange"/> would change this
    /// to <c>2, 10</c></para>
    /// </summary>
    public void Exchange()
    {
        string t = Stack.Pop();
        string t1 = Stack.Pop();
        Stack.Push(t);
        Stack.Push(t1);
    }

    /// <summary>
    /// <para>Parses a Reverse Polish Notation Equation and calculates the result.</para>
    /// </summary>
    /// <param name="expression">Equation in RPN format to parse</param>    
    public void Parse(string expression)
    {
        // Break up the expression into an array
        // using a space as the delimiter.
        string[] tokens = expression.Split(' ');
        // If there are no tokens, then print a message
        // abort.
        if (tokens.Length == 0)
        {
            Console.WriteLine("Nothing to parse!");
            return;
        }

        // Iterate over the expression array created above.
        foreach (string token in tokens)
        {
            if (double.TryParse(token, out double result))
            {
                if (tokens.Last() == result.ToString())
                    throw new Exception("Last item needs to be an operator!");
                else
                    Stack.Push(result.ToString());
            }
            else
            {
                if (token == "x" || token == "X") // Exchange top of stack
                    Exchange();
                else if (token == "?") // Dump the stack to console.
                    StackDump();
                else if (token == "&") // Dump temp vars to console.
                    VarDump();
                else if (token == "+")
                    Add();
                else if (token == "-")
                    Sub();
                else if (token == "*")
                    Mul();
                else if (token == "/")
                    Div();
                else if (token == "^")
                    Exponent();
                else if (token[0] == '!')
                    Vars.Add(token[1..], Peek()); // Store top of stack in tempVar
                else if (token[0] == '@')
                    Push(Vars[token[1..]] ?? string.Empty); // Retrieve tempVar and push it to the stack
                else // `token` did not match, so it's invalid.
                    throw new Exception($"Unknown operator or number: `{token}`");
            }
        }
    }
}
