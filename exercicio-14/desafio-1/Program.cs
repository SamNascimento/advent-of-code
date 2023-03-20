Console.WriteLine("========= Exercício 14 - Desafio 1 =========");                  
 
// var input = File.ReadAllLines("test.txt");
var input = File.ReadAllLines("input.txt");

var map = new CaveMap();

foreach (var line in input)
{
    var coordenates = line.Split("->");

    for (var i = 0; i < coordenates.Length - 1; i++)
    {
        var coordBegin = coordenates[i];
        var coordEnd   = coordenates[i+1];

        map = ConstructMap(map, coordBegin, coordEnd);
    }
}

var minX = map.nodes.Select(n => n.x).Min();
var maxX = map.nodes.Select(n => n.x).Max();
var minY = 0;
var maxY = map.nodes.Select(n => n.y).Max();

map = FillAirInMap(map, minX, maxX, minY, maxY);

var numSand = SandResting(map);
DrawnMap(map, minX, maxX, minY, maxY);

Console.WriteLine("Units of sand: " + numSand);

#region Methods
CaveMap ConstructMap (CaveMap map, string begin, string end)
{
    var coordBegin = begin.Trim().Split(',');
    var coordEnd   = end.Trim().Split(',');

    var beginX = int.Parse(coordBegin[0]);
    var beginY = int.Parse(coordBegin[1]);

    var endX = int.Parse(coordEnd[0]);
    var endY = int.Parse(coordEnd[1]);

    if (beginX != endX)
    {
        if (beginX < endX)
        {
            for (int i = beginX; i <= endX; i++)
            {
                var nodeExist = map.nodes.Where(n => n.x == i && n.y == beginY).FirstOrDefault() != null;

                if (!nodeExist)
                {
                    var newNode = new Node(i, beginY, '#');
                    map.nodes.Add(newNode);
                }
            }
        }
        else 
        {
            for (int i = endX; i <= beginX; i++)
            {
                var nodeExist = map.nodes.Where(n => n.x == i && n.y == beginY).FirstOrDefault() != null;

                if (!nodeExist)
                {
                    var newNode = new Node(i, beginY, '#');
                    map.nodes.Add(newNode);
                }
            }
        }
    }

    if (beginY != endY)
    {
        if (beginY < endY)
        {
            for (int i = beginY; i <= endY; i++)
            {
                var nodeExist = map.nodes.Where(n => n.x == beginX && n.y == i).FirstOrDefault() != null;

                if (!nodeExist)
                {
                    var newNode = new Node(beginX, i, '#');
                    map.nodes.Add(newNode);
                }
            }
        }
        else 
        {
            for (int i = endY; i <= beginY; i++)
            {
                var nodeExist = map.nodes.Where(n => n.x == beginX && n.y == i).FirstOrDefault() != null;

                if (!nodeExist)
                {
                    var newNode = new Node(beginX, i, '#');
                    map.nodes.Add(newNode);
                }
            }
        }
    }

    return map;
}

CaveMap FillAirInMap (CaveMap map, int minX, int maxX, int minY, int maxY)
{
    for (int i = minX; i <= maxX; i++)
    {
        for (int j = minY; j <= maxY; j++)
        {
            var actualNode = map.nodes.Where(n => n.x == i && n.y == j).FirstOrDefault();

            if (actualNode == null)
            {
                var newNode = new Node(i, j, '.');
                map.nodes.Add(newNode);
            }
        }
    }

    return map;
}

int SandResting(CaveMap map)
{
    var isFinished = false;

    while(!isFinished)
        (isFinished, map) = DropSand(map);

    return map.nodes.Where(n => n.obj == 'o').Count();
}

(bool isFinished, CaveMap mapUpdated) DropSand(CaveMap map)
{
    var actualX = 500;
    var actualY = 0;

    var isSandResting = false;

    var actualNode = map.nodes.Where(n => n.x == actualX && n.y == actualY).First();
    actualNode.obj = 'o';

    var nextSandSpot = map.nodes.Where(n => n.x == actualX && n.y == actualY + 1).FirstOrDefault();

    while(!isSandResting)
    {
        if (nextSandSpot != null)
        {
            if (nextSandSpot.obj == '.')
            {
                actualNode.obj = '.';
                actualNode     = map.nodes.Where(n => n.x == nextSandSpot.x && n.y == nextSandSpot.y).First();
                actualNode.obj = 'o';

                nextSandSpot = map.nodes.Where(n => n.x == nextSandSpot.x && n.y == nextSandSpot.y + 1).FirstOrDefault();
            }
            else
            {
                var leftSandSpot  = map.nodes.Where(n => n.x == nextSandSpot.x - 1 && n.y == nextSandSpot.y).FirstOrDefault();
                var rightSandSpot = map.nodes.Where(n => n.x == nextSandSpot.x + 1 && n.y == nextSandSpot.y).FirstOrDefault();

                if (leftSandSpot == null)
                {
                    isSandResting  = true;
                    actualNode.obj = '.';
                    return (true, map);
                }
                else if (leftSandSpot.obj == '.')
                {
                    nextSandSpot   = map.nodes.Where(n => n.x == leftSandSpot.x && n.y == leftSandSpot.y).First();
                    actualNode.obj = '.';
                    actualNode     = map.nodes.Where(n => n.x == nextSandSpot.x && n.y == nextSandSpot.y).First();
                    actualNode.obj = 'o';
    
                    nextSandSpot = map.nodes.Where(n => n.x == nextSandSpot.x && n.y == nextSandSpot.y + 1).FirstOrDefault();
                }
                else if (rightSandSpot == null)
                {
                    isSandResting  = true;
                    actualNode.obj = '.';
                    return (true, map);
                }
                else if (rightSandSpot.obj == '.')
                {
                    nextSandSpot   = map.nodes.Where(n => n.x == rightSandSpot.x && n.y == rightSandSpot.y).First();
                    actualNode.obj = '.';
                    actualNode     = map.nodes.Where(n => n.x == nextSandSpot.x && n.y == nextSandSpot.y).First();
                    actualNode.obj = 'o';
    
                    nextSandSpot = map.nodes.Where(n => n.x == nextSandSpot.x && n.y == nextSandSpot.y + 1).FirstOrDefault();
                }
                else 
                {
                    isSandResting = true;
                }   
            }
        }
        else 
        {
            actualNode.obj = '.';
            return (true, map);
        }
    }
    return (false, map);
}

void DrawnMap(CaveMap map, int minX, int maxX, int minY, int maxY) 
{
    for (int i = minY; i <= maxY; i++)
    {
        for (int j = minX; j <= maxX; j++)
        {
            var actualNode = map.nodes.Where(n => n.x == j && n.y == i).First();

            Console.Write(actualNode.obj);
        }
        Console.Write('\n');
    }
}
#endregion

#region Classes
public class CaveMap 
{
    public List<Node> nodes = new List<Node>();
}
public class Node 
{
    public int x;
    public int y;
    public char obj;

    public Node(int x, int y, char obj)
    {
        this.x   = x;
        this.y   = y;
        this.obj = obj;
    }
}
#endregion