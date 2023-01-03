Console.WriteLine("========= Exercício 4 - Desafio 2 =========");

/*
--- Part Two ---

It seems like there is still quite a bit of duplicate work planned. Instead, the Elves would like to know the number of pairs that overlap at all.

In the above example, the first two pairs (2-4,6-8 and 2-3,4-5) don't overlap, while the remaining four pairs (5-7,7-9, 2-8,3-7, 6-6,4-6, and 2-6,4-8) do overlap:

    5-7,7-9 overlaps in a single section, 7.
    2-8,3-7 overlaps all of the sections 3 through 7.
    6-6,4-6 overlaps in a single section, 6.
    2-6,4-8 overlaps in sections 4, 5, and 6.

So, in this example, the number of overlapping assignment pairs is 4.

In how many assignment pairs do the ranges overlap?
*/

var input = File.ReadAllLines("input.txt");

var numParesIneficientes = 0;

foreach(var line in input)
{
    var pares = line.Split(',');

    var (par1Area1, par1Area2) = ValoresIntervalo(pares[0]);
    var (par2Area1, par2Area2) = ValoresIntervalo(pares[1]);

    var listaPar1 = new List<int>();
    var listaPar2 = new List<int>();

    for(var i = par1Area1; i <= par1Area2; i++)
    {
        listaPar1.Add(i);
    }

    for(var i = par2Area1; i <= par2Area2; i++)
    {
        listaPar2.Add(i);
    }
    
    var temPar = listaPar1
        .Where(l => listaPar2.Contains(l))
        .Any();
    
    if(temPar)
        numParesIneficientes += 1;
}

Console.WriteLine($"O número de pares ineficientes é: {numParesIneficientes}");

(int, int) ValoresIntervalo(string intervalo)
{
    var par  = intervalo.Split('-');
    var num1 = int.Parse(par[0]);
    var num2 = int.Parse(par[1]);

    return (num1, num2);
}