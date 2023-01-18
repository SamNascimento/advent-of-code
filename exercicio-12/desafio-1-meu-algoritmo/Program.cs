Console.WriteLine("========= Exercício 12 - Desafio 1 =========");                  
 
// var input = File.ReadAllLines("test.txt");
var input = File.ReadAllLines("input.txt");

var x = input[0].Length;
var y = input.Length;

var S = new RemarkablePosition("S", new Cordenates(0, 0));
var E = new RemarkablePosition("E", new Cordenates(0, 0));

var actualX = 0;
var actualY = 0;

var map = new char[y, x];

for (var i = 0; i < y; i++)
{
    for (var j = 0; j < x; j++)
    {
        var charAtual = input[i][j];

        if (charAtual == 'S')
        {
            S.Cordenates.X = actualX;
            S.Cordenates.Y = actualY;
            charAtual = 'a';
        }
        else if (charAtual == 'E')
        {
            E.Cordenates.X = actualX;
            E.Cordenates.Y = actualY;
            charAtual = 'z';
        }

        map[actualY, actualX] = charAtual;
        if (actualX < x-1)
            actualX ++;
    }
    actualY ++;
    actualX = 0;
}

DrawMap(map);

Console.WriteLine($"The location of initial S is X: {S.Cordenates.X} Y: {S.Cordenates.Y}");
Console.WriteLine($"The location of initial E is X: {E.Cordenates.X} Y: {E.Cordenates.Y}");

var visitedCoord = new List<Cordenates>();
var allRoutesFound = BruteForceFindRoute(map, S, E, 0, visitedCoord);

var shorterRoute = allRoutesFound.Where(r => r.IsComplete).Min(r => r.Steps);
Console.WriteLine($"The fewest steps required is: " + shorterRoute);

#region Methods
void DrawMap(char[,] map)
{
    var xLimit = map.GetLength(1);
    var yLimit = map.GetLength(0);

    for (var y = 0; y < yLimit; y ++)
    {
        for (var x = 0; x < xLimit; x++)
        {
            Console.Write(map[y, x]);
        }
        Console.Write('\n');
    }
}

List<Routes> BruteForceFindRoute(
    char[,] map, 
    RemarkablePosition S, 
    RemarkablePosition E, 
    int count, 
    List<Cordenates> visitedCordenates)
{
    var visitedCoord = new List<Cordenates>();
    var routes       = new List<Routes>();

    visitedCoord.AddRange(visitedCordenates);
    visitedCoord.Add(new Cordenates(S.Cordenates.X, S.Cordenates.Y));

    if (S.Cordenates.Y == E.Cordenates.Y && S.Cordenates.X == E.Cordenates.X)
    {
        var newRouteTrue = new Routes(count, true);

        routes.Add(newRouteTrue);
        return routes;
    }

    var actualGroundS = map[S.Cordenates.Y, S.Cordenates.X];

    if (S.Cordenates.Y != 0)
    {   
        //Up
        if (map[S.Cordenates.Y - 1, S.Cordenates.X] <= actualGroundS + 1)
        {
            if (!visitedCoord.Any(v => v.Y == S.Cordenates.Y - 1 && v.X == S.Cordenates.X))
            {
                var newS = new RemarkablePosition("S", new Cordenates(S.Cordenates.X, S.Cordenates.Y - 1));

                routes.AddRange(BruteForceFindRoute(map, newS, E, count + 1, visitedCoord));
            }
        }
    }

    if (S.Cordenates.Y < map.GetLength(0) - 1)
    {
        //Down
        if (map[S.Cordenates.Y + 1, S.Cordenates.X] <= actualGroundS + 1)
        {
            if (!visitedCoord.Any(v => v.Y == S.Cordenates.Y + 1 && v.X == S.Cordenates.X))
            {
                var newS = new RemarkablePosition("S", new Cordenates(S.Cordenates.X, S.Cordenates.Y + 1));

                routes.AddRange(BruteForceFindRoute(map, newS, E, count + 1, visitedCoord));
            }
        }
    }

    if (S.Cordenates.X != 0)
    {
        //Left
        if (map[S.Cordenates.Y, S.Cordenates.X - 1] <= actualGroundS + 1)
        {
            if (!visitedCoord.Any(v => v.Y == S.Cordenates.Y && v.X == S.Cordenates.X - 1))
            {
                var newS = new RemarkablePosition("S", new Cordenates(S.Cordenates.X - 1, S.Cordenates.Y));

                routes.AddRange(BruteForceFindRoute(map, newS, E, count + 1, visitedCoord));
            }
        }
    }

    if (S.Cordenates.X < map.GetLength(1) - 1)
    {
        //Right
        if (map[S.Cordenates.Y, S.Cordenates.X + 1] <= actualGroundS + 1)
        {
            if (!visitedCoord.Any(v => v.Y == S.Cordenates.Y && v.X == S.Cordenates.X + 1))
            {
                var newS = new RemarkablePosition("S", new Cordenates(S.Cordenates.X + 1, S.Cordenates.Y));

                routes.AddRange(BruteForceFindRoute(map, newS, E, count + 1, visitedCoord));
            }
        }
    }

    var newRouteFalse = new Routes(count, false);
    routes.Add(newRouteFalse);

    return routes;
}
#endregion

#region Classes
class RemarkablePosition
{
    public string Name { get; set; }
    public Cordenates Cordenates { get; set; }

    public RemarkablePosition(string name, Cordenates cordenates)
    {
        this.Name       = name;
        this.Cordenates = cordenates;
    }
}

class Routes 
{
    public long Steps { get; set; }
    public bool IsComplete { get; set; }

    public Routes(long steps, bool isComplete)
    {
        this.Steps      = steps;
        this.IsComplete = isComplete;
    }
}

class Cordenates
{
    public int X { get; set; }
    public int Y { get; set; }

    public Cordenates(int x, int y)
    {
        this.X = x;
        this.Y = y;
    }
}
#endregion