Console.WriteLine("========= Exercício 4 - Desafio 1 =========");

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

    if(listaPar1.Min() <= listaPar2.Min() && listaPar1.Max() >= listaPar2.Max())
    {
        numParesIneficientes += 1;
        continue;
    }

    if(listaPar1.Min() >= listaPar2.Min() && listaPar1.Max() <= listaPar2.Max())
    {
        numParesIneficientes += 1;
        continue;
    }
}

Console.WriteLine($"O número de pares ineficientes é: {numParesIneficientes}");

(int, int) ValoresIntervalo(string intervalo)
{
    var par  = intervalo.Split('-');
    var num1 = int.Parse(par[0]);
    var num2 = int.Parse(par[1]);

    return (num1, num2);
}