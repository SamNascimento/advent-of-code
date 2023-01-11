Console.WriteLine("========= Exercício 8 - Desafio 1 =========");                  
 
// var input = File.ReadAllLines("test.txt");
var input = File.ReadAllLines("input.txt");

var x = input[0].Length;
var y = input.Length;

var treeArray    = ReadInput(x, y, input);
var visibleTrees = 0;

for (var i = 0; i < x; i++)
{
    for (var j = 0; j < y; j++)
    {
        if (i == 0 || i == x-1)
        {
            visibleTrees += 1;
            continue;
        }
        else if (j == 0 || j == y-1)
        {
            visibleTrees += 1;
            continue;
        }

        var isVisibleNorth = false;
        var isVisibleSouth = false;
        var isVisibleWest  = false;
        var isVisibleEast  = false;

        var numAtual = treeArray[i, j];

        // North
        for (var l = i-1; l >= 0; l--)
        {
            var numNorth = treeArray[l,j];

            if (numNorth < numAtual)
                isVisibleNorth = true;
            else 
            {
                isVisibleNorth = false;
                break;
            }
        }
        
        // South
        for (var l = i+1; l < x; l++)
        {
            var numSouth = treeArray[l,j];

            if (numSouth < numAtual)
                isVisibleSouth = true;
            else 
            {   
                isVisibleSouth = false;
                break;
            }
        }

        // West
        for (var l = j-1; l >= 0; l--)
        {
            var numWest = treeArray[i,l];

            if (numWest < numAtual)
                isVisibleWest = true;
            else 
            {
                isVisibleWest = false;
                break;
            } 
        }

        // Eeast
        for (var l = j+1; l < y; l++)
        {
            var numEast = treeArray[i,l];

            if (numEast < numAtual)
                isVisibleEast = true;
            else 
            {
                isVisibleEast = false;
                break;
            }
        }

        if (isVisibleNorth || isVisibleSouth || isVisibleWest || isVisibleEast)
            visibleTrees += 1;
    } 
}

Console.WriteLine(visibleTrees);

int[,] ReadInput(int x, int y, string[] lines)
{
    var treeArray = new int[x, y];

    for (var i = 0; i < x; i++)
    {
        for (var j = 0; j < y; j++)
        {
            treeArray[i, j] = Convert.ToInt32(lines[i][j].ToString());
        }
    }

    return treeArray;
}