Console.WriteLine("========= Exercício 8 - Desafio 2 =========");                  
 
// var input = File.ReadAllLines("test.txt");
var input = File.ReadAllLines("input.txt");

var x = input[0].Length;
var y = input.Length;

var treeArray    = ReadInput(x, y, input);
var bestScenic = 0;

for (var i = 0; i < x; i++)
{
    for (var j = 0; j < y; j++)
    {
        var scenicNorth = 0;
        var scenicSouth = 0;
        var scenicWest  = 0;
        var scenicEast  = 0;
        var scenicTotal = 0;

        var numAtual = treeArray[i, j];

        // North
        for (var l = i-1; l >= 0; l--)
        {
            if (i-1 == -1)
                break;

            scenicNorth += 1;

            var numNorth = treeArray[l,j];

            if (numNorth >= numAtual)
                break;
                
        }
        
        // South
        for (var l = i+1; l < x; l++)
        {
            if (i+1 == x)
                break;

            scenicSouth += 1;

            var numSouth = treeArray[l,j];

            if (numSouth >= numAtual)
                break;
        }

        // West
        for (var l = j-1; l >= 0; l--)
        {
            if (j-1 == -1)
                break;

            scenicWest += 1;

            var numWest = treeArray[i,l];

            if (numWest >= numAtual)
                break;
        }

        // Eeast
        for (var l = j+1; l < y; l++)
        {
            if (j+1 == y)
                break;

            scenicEast += 1;

            var numEast = treeArray[i,l];

            if (numEast >= numAtual)
                break;
        }

        scenicTotal = scenicNorth * scenicSouth * scenicWest * scenicEast;

        if (scenicTotal > bestScenic)
            bestScenic = scenicTotal;
    } 
}

Console.WriteLine(bestScenic);

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