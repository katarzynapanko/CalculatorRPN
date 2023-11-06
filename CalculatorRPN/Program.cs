// See https://aka.ms/new-console-template for more information

Console.WriteLine("Hello, World!");
Console.WriteLine("Enter your function to calculate");
var symbols = GetFunction(); 
var operators = new Dictionary<string, int>()
{
    { "+", 0 },
    { "-", 0 },
    { "*", 1 },
    { "/", 1 }
};
CheckSymbols(symbols, operators);
var equation = TransformToRPN(symbols, operators);
Console.WriteLine(ComputeResult(equation));

static string[] GetFunction()
{
    var function = Console.ReadLine();
    while (function is null)
    {
        Console.WriteLine("rong input gupolu");
        function = Console.ReadLine();
    }
    return function.Split(" ");
}
 static void CheckSymbols(string[] symbols, Dictionary<string, int> operators)
{
    if (!symbols.All(x => int.TryParse(x, out _) || operators.ContainsKey(x)))
    {
        Console.WriteLine("Incorrect elements found in equation.");
        return;
    }
}

static List<string> TransformToRPN(string[] symbols, Dictionary<string, int> operators)
{
    var stack = new Stack<string>();
    var operatorsStack = new Stack<string>();
    foreach (var symbol in symbols)
    {
        if (!operators.ContainsKey(symbol))
        {
            stack.Push(symbol);
            continue;
        }
        while (operatorsStack.TryPeek(out var lastOperator))
        {
            if (operators[lastOperator] >= operators[symbol])
            {
                stack.Push(operatorsStack.Pop());
                continue;
            }
            break;
        }
        operatorsStack.Push(symbol);
    }
    while (operatorsStack.Any())
    {
        stack.Push(operatorsStack.Pop());
    }
    var rpnEquation = stack.ToList();
    rpnEquation.Reverse();
    return rpnEquation;
}
static string ComputeResult(List<string> rpnEquation)

{
    var rpnStack = new Stack<string>();
    foreach (var symbol in rpnEquation)
    {
        if (int.TryParse(symbol, out _))
        {
            rpnStack.Push(symbol);
        }
        else
        {
            var x = int.Parse(rpnStack.Pop());
            var y = int.Parse(rpnStack.Pop());
            switch (symbol)
            {
                case "+":
                    rpnStack.Push((x + y).ToString());
                    break;
                case "-":
                    rpnStack.Push((y - x).ToString());
                    break;
                case "*":
                    rpnStack.Push((x * y).ToString());
                    break;
                case "/":
                    rpnStack.Push((y / x).ToString());
                    break;

            }
        }
    }
    return rpnStack.Pop();
}


