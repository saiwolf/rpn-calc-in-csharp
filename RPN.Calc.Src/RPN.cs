namespace Calc.Src;

internal class RPN
{
    internal Stack<string> Stack {  get; set; }
    internal Dictionary<string, string> Vars { get; set; }

    public RPN()
    {
        Stack = new Stack<string>();
        Vars = new();       
    }

    public void Push(string value) =>
        Stack.Push(value);

    public string Pop() => Stack.Pop();

    public void Add()
    {
        double x = double.Parse(Stack.Pop());
        double y = double.Parse(Stack.Pop());
        double result = x + y;
        Stack.Push(result.ToString());
    }

    public void Sub()
    {
        double x = double.Parse(Stack.Pop());
        double y = double.Parse(Stack.Pop());
        double result = y - x;
        Stack.Push(result.ToString());
    }

    public void Mul()
    {
        double x = double.Parse(Stack.Pop());
        double y = double.Parse(Stack.Pop());
        double result = x * y;
        Stack.Push(result.ToString());
    }

    public void Div()
    {
        double x = double.Parse(Stack.Pop());
        double y = double.Parse(Stack.Pop());
        double result = y / x;
        Stack.Push(result.ToString());
    }

    public string Peek() => Stack.Peek();

    public void Dump()
    {
        if (!Stack.Any())
            throw new Exception("Nothing to show!");
        foreach (string item in Stack)
            Console.WriteLine(item);
    }

    public void Clear() => Stack.Clear();

    public void Wipe()
    {
        Clear();
        Vars.Clear();
    }

    public void Exchange()
    {
        string t = Stack.Pop();
        string t1 = Stack.Pop();
        Stack.Push(t);
        Stack.Push(t1);
    }

    public void Parse(string expression)
    {
        string[] tokens = expression.Split(' ');
        if (tokens.Length == 0) throw new Exception("Nothing to parse!");
        
        foreach (string token in tokens)
        {
            try
            {
                double value = double.Parse(token);
                Stack.Push(value.ToString());
            }
            catch (FormatException)
            {
                if (token == "x" || token == "X")
                    Exchange();
                else if (token == "?")
                    Dump();
                else if (token == "+")
                    Add();
                else if (token == "-")
                    Sub();
                else if (token == "*")
                    Mul();
                else if (token == "/")
                    Div();
                else if (token[0] == '!')
                    Vars.Add(token[1..], Peek());
                else if (token[0] == '@')
                    Push(Vars[token[1..]] ?? string.Empty);
                else
                    throw new Exception($"Unknown operator or number: `{token}`");
            }
        }
    }
}
