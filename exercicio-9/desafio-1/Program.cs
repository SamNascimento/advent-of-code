Console.WriteLine("========= Exercício 9 - Desafio 1 =========");                  
 
// var input = File.ReadAllLines("input.txt");

// var x = 1000;
// var y = 1000;

// var initialX = 500;
// var initialY = 500;

var input = File.ReadAllLines("test.txt");

var x = 6;
var y = 5;

var initialX = 0;
var initialY = y - 1;

var actualX = initialX;
var actualY = initialY;

var planck = InicializePlanck(x, y);

planck[initialX, initialY] = 'H';

foreach (var line in input)
{
    var dividedLine  = line.Split(' ');
    var direction    = dividedLine[0];
    var numMovements = int.Parse(dividedLine[1]);
    var breakNumber  = 0;

    switch (direction)
    {
        case "R":
            breakNumber = actualX + numMovements;
            for (var i = actualX; i <= breakNumber; i++) 
            {
                actualX = i;

                if (actualX >= planck.GetLength(0))
                    throw new Exception($"Moviment out of bound while tryied to move \"{direction}\" to {numMovements} steps");
                else
                    planck[actualX, actualY] = 'H';
            }
            break;
        case "L":
            breakNumber = actualX - numMovements;
            for (var i = actualX; i >= breakNumber; i--)
            {
                actualX = i;
                
                if (actualX < 0)
                    throw new Exception($"Moviment out of bound while tryied to move \"{direction}\" to {numMovements} steps");
                else
                    planck[actualX, actualY] = 'H';
            }
            break;
        case "U":
            breakNumber = actualY - numMovements;
            for (var j = actualY; j >= breakNumber; j--)
            {
                actualY = j;

                if (actualY < 0)
                    throw new Exception($"Moviment out of bound while tryied to move \"{direction}\" to {numMovements} steps");
                else
                    planck[actualX, actualY] = 'H';
            }
            break;
        case "D":
            breakNumber = actualY + numMovements;
            for (var j = actualY; j <= breakNumber; j++)
            {
                actualY = j;

                if (actualY >= planck.GetLength(1))
                    throw new Exception($"Moviment out of bound while tryied to move \"{direction}\" to {numMovements} steps");
                else
                    planck[actualX, actualY] = 'H';
            }
            break;
        default:
            break;
    }
}

var totalMovedPlancks = CountMovedPlanck(x, y, planck);

var textPlanck = DrawPlanck(x, y, planck);

Console.WriteLine(textPlanck);
Console.WriteLine("Total moved plancks is: " + totalMovedPlancks);

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

string DrawPlanck(int x, int y, char[,] planck)
{
    var textPlanck = "";

    for (var i = 0; i < y; i++)
    {
        for (var j = 0; j < x; j++)
        {
            textPlanck += $" {planck[j, i]} ";
        }
        textPlanck += '\n';
    }

    return textPlanck;
}

#endregion