Console.WriteLine("========= Exercício 5 - Desafio 1 =========");

var input      = File.ReadAllText("input.txt");
var cleanInput = input.Replace("\r", "");

var dividedInput = cleanInput.Split("\n\n");

var topCrate = new List<char>();

var inputCrates = dividedInput[0].Split('\n');
var commands    = dividedInput[1].Split('\n');

var cratesSize = (inputCrates[inputCrates.Length - 1].Length + 1) / 4;

var crates = new Stack<char>[cratesSize];

for (var i = 0; i < crates.Length; i++)
{
    crates[i] = new Stack<char>();
}

for (var i = inputCrates.Length - 2; i >= 0; i--)
{
    for (var cratesIndex = 0; cratesIndex < crates.Length; cratesIndex++)
    {
        var index = (4 * cratesIndex) + 1; 
        var letra = inputCrates[i][index];

        if(letra != ' ')
            crates[cratesIndex].Push(letra);
    }
}

foreach (var command in commands)
{
    var lines = command.Split(' ');

    var moveNumber = int.Parse(lines[1]);
    var fromNumber = int.Parse(lines[3]) - 1;
    var toNumber   = int.Parse(lines[5]) - 1;

    for (var i = moveNumber; i > 0; i--)
    {
        crates[toNumber].Push(crates[fromNumber].Pop());
    }
}

foreach (var crate in crates)
{
    topCrate.Add(crate.Peek());
}

Console.WriteLine(new string(topCrate.ToArray()));