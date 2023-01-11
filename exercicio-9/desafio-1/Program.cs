Console.WriteLine("========= Exercício 9 - Desafio 1 =========");                  
 
// var input = File.ReadAllLines("test.txt");
var input = File.ReadAllLines("input.txt");

var x = 40;
var y = 40;

var planck = InicializePlanck(x, y);

var totalMovedPlancks = CountMovedPlanck(x, y, planck);

Console.WriteLine(totalMovedPlancks);


#region Methods

char[,] InicializePlanck(int x, int y)
{
    var planck = new char[x, y];

    for (var i = 0; i < y; i++)
    {
        for (var j = 0; j < x; j++)
        {
            planck[j, i] = 'S';
        }
    }

    return planck;
}

int CountMovedPlanck(int x, int y, char[,] planck)
{
    var count = 0;

    for (var i = 0; i < y; i++)
    {
        for (var j = 0; j < x; j++)
        {
            if (planck[j, i] != 'S')
                count += 1;
        }
    }

    return count;
}

#endregion