Console.WriteLine("========= Exercício 1 - Desafio 1 =========");                  
 
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

var topCalorie = listElfs.Max(l => l.SumCalories);

Console.WriteLine("The max calorie number carrying by an elf is: " + topCalorie);

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