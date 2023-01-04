Console.WriteLine("========= Exercício 5 - Desafio 1 =========");

var input        = File.ReadAllText("input.txt");
var dividedInput = input.Split("\n\n");

var topCrate = new List<char>();

var inputCrates = dividedInput[0].Split('\n');
var commands    = dividedInput[1].Split('\n');

var crates = new Stack<char>[9];

for (var i = 0; i < 9; i++)
{
    crates[i] = new Stack<char>();
}

//int cratesIndex = 0;

for(var i = inputCrates.Length - 2; i >= 0; i--)
{
    for(var cratesIndex = 0; cratesIndex < crates.Length; cratesIndex++)
    {
        var index = (4 * cratesIndex) + 1; 
        var letra = inputCrates[i][index];

        if(letra != ' ')
            crates[cratesIndex].Push(letra);
    }
}

foreach(var command in commands)
{
    
}

foreach(var crate in crates)
{
    topCrate.Add(crate.Peek());
}

Console.WriteLine(new string(topCrate.ToArray()));