Console.WriteLine("========= Exercício 1 - Desafio 2 =========");                  
 
var input = File.ReadAllText("input.txt");
// var input = File.ReadAllText("test.txt");

var listElfsBackpacks = input.Split("\n\n");

var listElfs = new List<Elf>();

foreach (var elfBackpack in listElfsBackpacks)
{
    var listItens = elfBackpack.Split('\n');
    var calories  = listItens.Select(l => int.Parse(l));

    var elf = new Elf(calories);
    listElfs.Add(elf);
}

var topCalories = listElfs.Select(l => l.SumCalories).OrderByDescending(l => l).Take(3);

Console.WriteLine("The top three calories number carrying by an elf is: ");
foreach (var calories in topCalories)
    Console.WriteLine(calories);
Console.WriteLine("And the sum of that calories is: " + topCalories.Sum());

#region Classes
class Elf
{
    public IEnumerable<int> ItensCalories { get; set; }
    public long SumCalories { get; set; }

    public Elf(IEnumerable<int> itens)
    {
        ItensCalories = itens;
        SumCalories   = itens.Sum();
    }
}
#endregion