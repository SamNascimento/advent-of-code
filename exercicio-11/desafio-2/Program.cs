using System.Numerics;

Console.WriteLine("========= Exercício 11 - Desafio 2 =========");                  
 
// var input = File.ReadAllText("test.txt");
var input = File.ReadAllText("input.txt");

var inputMonkeys     = input.Split("\n\n");
var listMonkeys      = new List<Monkey>();
var divisibleNumbers = new List<BigInteger>();
var numOfRounds      = 10000;

foreach (var inputMonkey in inputMonkeys)
{
    var monkeyData = inputMonkey.Split('\n');
    
    var monkeyNumber = int.Parse(monkeyData[0].Replace("Monkey", "").Replace(":", ""));

    var listItensText = monkeyData[1].Replace("Starting items:", "").Split(",");
    var listItens     = listItensText.Select(l => BigInteger.Parse(l));

    var operationText        = monkeyData[2].Replace("Operation: new =", "");
    Func<BigInteger, BigInteger> operation = old => old * old;
    if (operationText.Contains('*'))
    {
        var values = operationText.Split('*');
        
        var value1IsNumber = BigInteger.TryParse(values[0], out var value1);
        var value2IsNumber = BigInteger.TryParse(values[1], out var value2);

        operation = old => (value1IsNumber ? value1 : old) * (value2IsNumber ? value2 : old);
    }
    else if (operationText.Contains('+'))
    {
        var values = operationText.Split('+');
        
        var value1IsNumber = BigInteger.TryParse(values[0], out var value1);
        var value2IsNumber = BigInteger.TryParse(values[1], out var value2);

        operation = old => (value1IsNumber ? value1 : old) + (value2IsNumber ? value2 : old);
    }

    var divNumber = BigInteger.Parse(monkeyData[3].Replace("Test: divisible by", ""));
    divisibleNumbers.Add(divNumber);

    var trueMonkeyTarget  = int.Parse(monkeyData[4].Replace("If true: throw to monkey", ""));
    var falseMonkeyTarget = int.Parse(monkeyData[5].Replace("If false: throw to monkey", ""));

    Action<BigInteger, BigInteger> action = (monkeyNumber, number) => 
    {
        var isDivisible  = number % divNumber == 0;
        var actualMonkey = listMonkeys.Where(l => l.NumberOfMonkey == monkeyNumber).FirstOrDefault();
        var trueMonkey   = listMonkeys.Where(l => l.NumberOfMonkey == trueMonkeyTarget).FirstOrDefault();
        var falseMonkey  = listMonkeys.Where(l => l.NumberOfMonkey == falseMonkeyTarget).FirstOrDefault();

        if (isDivisible)
            trueMonkey?.AddNewItem(number);
        else
            falseMonkey?.AddNewItem(number);

        actualMonkey?.IncrementInspections();
    };

    var newMonkey = new Monkey(monkeyNumber, listItens, operation, action);
    listMonkeys.Add(newMonkey);
}

var divisibleNumber = divisibleNumbers.Aggregate(1, (current, test) => (int)(current * test));

for (var i = 0; i < numOfRounds; i++)
{
    foreach (var monkey in listMonkeys)
    {
        foreach (var item in monkey.Itens)
        {
            var result = monkey.Operation(item);
            result %= divisibleNumber;
            monkey.Test(monkey.NumberOfMonkey, result);
        }
        monkey.Itens = new List<BigInteger>();
    }
}

var topActiveMonkeys = listMonkeys.OrderByDescending(l => l.NumInspections).Take(2);
var monkeyBussines   = topActiveMonkeys.First().NumInspections * topActiveMonkeys.Last().NumInspections;

Console.WriteLine("The most active monkeys are: ");
foreach (var monkey in topActiveMonkeys)
    Console.WriteLine($"Monkey {monkey.NumberOfMonkey} inspected items {monkey.NumInspections} times.");

Console.WriteLine("The level of monkey bussiness is: " + monkeyBussines);

#region Classes
class Monkey
{
    public int NumberOfMonkey { get; set; }
    public List<BigInteger> Itens { get; set; }
    public ulong NumInspections { get; set;} = 0;
    public Func<BigInteger, BigInteger> Operation { get; set; }
    public Action<BigInteger, BigInteger> Test { get; set; }

    public Monkey(
        int numberOfMonkey, 
        IEnumerable<BigInteger>? itens, 
        Func<BigInteger, BigInteger> operation, 
        Action<BigInteger, BigInteger> test)
    {
        NumberOfMonkey = numberOfMonkey;
        Itens          = itens?.ToList() ?? new List<BigInteger>();
        Operation      = operation;
        Test           = test;
    }

    public void AddNewItem(BigInteger item)
    {
        Itens.Add(item);
    }

    public void RemoveItem(BigInteger item)
    {
        Itens.Remove(item);
    }
    
    public void IncrementInspections()
    {
        NumInspections ++;
    }
}
#endregion