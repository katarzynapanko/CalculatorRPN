// See https://aka.ms/new-console-template for more information

using System.Collections;

Console.WriteLine("Hello, World!");
Console.WriteLine("Enter your function to calculate");
var symbols = GetFunction();
var stack = new Stack<string>();
var operatorsStack = new Stack<string>();
var operators = new Dictionary<string, int>()
{
    { "+", 0 },
    { "-", 0 },
    { "*", 1 },
    { "/", 1 }
};
string[] brackets = { "(", ")" };
CheckSymbols(symbols, operators, brackets);
var equation = TransformToRPN(symbols, brackets, operators, stack, operatorsStack);
Console.WriteLine(string.Join(" ", equation));
Console.WriteLine(ComputeResult(equation));

static string[] GetFunction()
{
    var function = Console.ReadLine();
    while (function is null)
    {
        Console.WriteLine("Wrong input");
        function = Console.ReadLine();
    }
    return function.Split(" ");
}
 static void CheckSymbols(string[] symbols, Dictionary<string, int> operators, string[] brackets)
{
    if (!symbols.All(x => int.TryParse(x, out _) || operators.ContainsKey(x) || brackets.Any()))
    {
        Console.WriteLine("Incorrect elements found in equation.");
        return;
    }
}

static List<string> TransformToRPN(string[] symbols, string[] brackets, Dictionary<string, int> operators, Stack<string> stack, Stack<string> operatorsStack)
{
    foreach (var symbol in symbols)
    {
        if (!operators.ContainsKey(symbol) && !brackets.Contains(symbol))
        {
            stack.Push(symbol);
            continue;
        }
        else if (symbol == "(")
        {
            operatorsStack.Push(symbol);
            continue;
        }
        else if (symbol == ")")
        {
            while (operatorsStack.Any() && !operatorsStack.Peek().Equals("("))
            {
                stack.Push(operatorsStack.Pop());
            }
            _ = operatorsStack.Pop();
            continue;
        }
        while (operatorsStack.TryPeek(out var lastOperator))
        {
            if (operators.ContainsKey(lastOperator) && 
                operators.ContainsKey(symbol) &&
                operators[lastOperator] >= operators[symbol])
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
    rpnEquation.Reverse();;
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


