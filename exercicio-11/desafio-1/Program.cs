Console.WriteLine("========= Exercício 11 - Desafio 1 =========");                  
 
// var input = File.ReadAllText("test.txt");
var input = File.ReadAllText("input.txt");

var inputMonkeys = input.Split("\n\n");
var listMonkeys  = new List<Monkey>();
var numOfRounds  = 20;

foreach (var inputMonkey in inputMonkeys)
{
    var monkeyData = inputMonkey.Split('\n');
    
    var monkeyNumber = int.Parse(monkeyData[0].Replace("Monkey", "").Replace(":", ""));

    var listItensText = monkeyData[1].Replace("Starting items:", "").Split(",");
    var listItens     = listItensText.Select(l => int.Parse(l));

    var operationText        = monkeyData[2].Replace("Operation: new =", "");
    Func<int, int> operation = old => old * old;
    if (operationText.Contains('*'))
    {
        var values = operationText.Split('*');
        
        var value1IsNumber = int.TryParse(values[0], out var value1);
        var value2IsNumber = int.TryParse(values[1], out var value2);

        operation = old => (value1IsNumber ? value1 : old) * (value2IsNumber ? value2 : old);
    }
    else if (operationText.Contains('+'))
    {
        var values = operationText.Split('+');
        
        var value1IsNumber = int.TryParse(values[0], out var value1);
        var value2IsNumber = int.TryParse(values[1], out var value2);

        operation = old => (value1IsNumber ? value1 : old) + (value2IsNumber ? value2 : old);
    }

    var divisibleNumber     = int.Parse(monkeyData[3].Replace("Test: divisible by", ""));
    var trueMonkeyTarget    = int.Parse(monkeyData[4].Replace("If true: throw to monkey", ""));
    var falseMonkeyTarget   = int.Parse(monkeyData[5].Replace("If false: throw to monkey", ""));
    Action<int, int> action = (monkeyNumber, number) => 
    {
        var isDivisible  = number % divisibleNumber == 0;
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

for (var i = 0; i < numOfRounds; i++)
{
    foreach (var monkey in listMonkeys)
    {
        foreach (var item in monkey.Itens)
        {
            var result = monkey.Operation(item) / 3;
            monkey.Test(monkey.NumberOfMonkey, result);
        }
        monkey.Itens = new List<int>();
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
    public List<int> Itens { get; set; }
    public long NumInspections { get; set;} = 0;
    public Func<int, int> Operation { get; set; }
    public Action<int, int> Test { get; set; }

    public Monkey(int numberOfMonkey, IEnumerable<int>? itens, Func<int, int> operation, Action<int, int> test)
    {
        NumberOfMonkey = numberOfMonkey;
        Itens          = itens?.ToList() ?? new List<int>();
        Operation      = operation;
        Test           = test;
    }

    public void AddNewItem(int item)
    {
        Itens.Add(item);
    }

    public void RemoveItem(int item)
    {
        Itens.Remove(item);
    }
    
    public void IncrementInspections()
    {
        NumInspections ++;
    }
}
#endregion