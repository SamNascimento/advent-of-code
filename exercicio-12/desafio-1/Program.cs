Console.WriteLine("========= Exercício 12 - Desafio 1 =========");                  
 
var input = File.ReadAllLines("test.txt");
// var input = File.ReadAllLines("input.txt");

var x = input[0].Length;
var y = input.Length;

var inputMapped = MapInput(input, y, x);
var allPosition = FindNeighbors(inputMapped, y, x).ToList();

var source = allPosition.Where(r => r.Name == 'S').First();

Dijkstra(allPosition, source);

var smallDistanceTarget = allPosition.Where(r => r.Name == 'E').First();

Console.WriteLine("The small distance to reach your target is: " + smallDistanceTarget.SmallDistance);

#region Methods
Dictionary<(int y, int x), Position> MapInput(string[] input, int y, int x)
{
    var dictPosition = new Dictionary<(int y, int x), Position>();

    for (var i = 0; i < y; i++)
    {
        for (var j = 0; j < x; j++)
        {
            var currentChar = input[i][j];
            var elevation   = Elevation(currentChar);
            var neighbors   = new List<Position>();

            var position = new Position(currentChar, elevation, neighbors);

            dictPosition.Add((i, j), position);
        }
    }

    return dictPosition;
}

IEnumerable<Position> FindNeighbors (Dictionary<(int y, int x), Position> inputMapped, int y, int x)
{
    for (var i = 0; i < y; i++)
    {
        for (var j = 0; j < x; j++)
        {
            var currentPosition = inputMapped.GetValueOrDefault((i,j));

            //Up
            if((i - 1) >= 0)
            {
                var upPosition = inputMapped.GetValueOrDefault((i-1,j));

                if (currentPosition!.Elevation <= upPosition!.Elevation + 1)
                    currentPosition.ValidNeighbors.Add(upPosition);
            }

            //Down
            if((i + 1) <= (y - 1))
            {
                var downPosition = inputMapped.GetValueOrDefault((i+1,j));

                if (currentPosition!.Elevation <= downPosition!.Elevation + 1)
                    currentPosition.ValidNeighbors.Add(downPosition);
            }

            //Left
            if((j - 1) >= 0)
            {
                var leftPosition = inputMapped.GetValueOrDefault((i,j-1));

                if (currentPosition!.Elevation <= leftPosition!.Elevation + 1)
                    currentPosition.ValidNeighbors.Add(leftPosition);
            }

            //Right
            if((j + 1) <= (x - 1))
            {
                var rightPosition = inputMapped.GetValueOrDefault((i,j+1));

                if (currentPosition!.Elevation <= rightPosition!.Elevation + 1)
                    currentPosition.ValidNeighbors.Add(rightPosition);
            }
        }
    }

    return inputMapped.AsEnumerable().Select(d => d.Value);
}

int Elevation(char currentChar)
{
    var elevation = -1;
    
    if (currentChar == 'S')
    {
        elevation = 'a' - 'a';
    }
    else if (currentChar == 'E')
    {
        elevation = 'z' - 'a';
    }
    else
    {
        elevation = currentChar - 'a';
    }

    return elevation;
}

void Dijkstra(List<Position> allPosition, Position source)
{
    source.SmallDistance = 0;

    while(allPosition.Any(a => a.WasVisited == false))
    {
        var currentPosition         = allPosition.Where(a => a.WasVisited == false).MinBy(a => a.SmallDistance);
        currentPosition!.WasVisited = true;

        foreach (var neighbors in currentPosition!.ValidNeighbors)
        {
            if (!neighbors.WasVisited)
            {
                var newDistance = currentPosition!.SmallDistance + 1;

                if (newDistance < neighbors.SmallDistance)
                {
                    neighbors.SmallDistance    = newDistance;
                    neighbors.PreviousPosition = currentPosition;
                }
            }
        }
    }
}
#endregion

#region Classes
class Position
{
    public char Name { get; set; }
    public int Elevation { get; set; }
    public List<Position> ValidNeighbors { get; set; }
    public int SmallDistance { get; set; }
    public bool WasVisited { get; set; }
    public Position? PreviousPosition { get; set; }

    public Position(char name, int elevation, List<Position> validNeighbors)
    {
        this.Name           = name;
        this.Elevation      = elevation;
        this.ValidNeighbors = validNeighbors;
        this.SmallDistance  = int.MaxValue;
        this.WasVisited     = false;
    }
}
#endregion