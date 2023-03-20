Console.WriteLine("========= Exercício 14 - Desafio 2 =========");                  
 
var input = File.ReadAllLines("test.txt");
// var input = File.ReadAllLines("input.txt");

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
var maxY = map.nodes.Select(n => n.y).Max() + 2;

var numSand = SandResting(map, maxY);

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

int SandResting(CaveMap map, int maxY)
{
    var isFinished = false;

    while(!isFinished)
        (isFinished, map) = DropSand(map, maxY);

    return map.nodes.Where(n => n.obj == 'o').Count();
}

(bool isFinished, CaveMap mapUpdated) DropSand(CaveMap map, int maxY)
{
    var isSandResting = false;

    var actualY = 0;
    var actualX = 500;

    if (!map.nodes.Where(n => n.x == actualX && n.y == actualY).Any())
        map.nodes.Add(new Node(actualX, actualY, '.'));

    var actualNode = map.nodes.Where(n => n.x == actualX && n.y == actualY).First();
    actualNode.obj = 'o';

    var nextSandSpot = map.nodes.Where(n => n.x == actualX && n.y == actualY + 1).FirstOrDefault();

    while(!isSandResting)
    {
        if (nextSandSpot != null)
        {
            var leftSandSpot  = map.nodes.Where(n => n.x == nextSandSpot.x - 1 && n.y == nextSandSpot.y).FirstOrDefault();
            var rightSandSpot = map.nodes.Where(n => n.x == nextSandSpot.x + 1 && n.y == nextSandSpot.y).FirstOrDefault();

            if (leftSandSpot == null)
            {
                var newNode = new Node(nextSandSpot.x - 1, nextSandSpot.y, 'o');

                map.nodes.Add(newNode);
                map.nodes.Remove(actualNode);

                actualNode = map.nodes.Where(n => n.x == newNode.x && n.y == newNode.y).First();
                actualX    = actualNode.x;
                actualY    = actualNode.y;

                nextSandSpot = map.nodes.Where(n => n.x == actualX && n.y == actualY + 1).FirstOrDefault();
            }
            else if (rightSandSpot == null)
            {
                var newNode = new Node(nextSandSpot.x + 1, nextSandSpot.y, 'o');

                map.nodes.Add(newNode);
                map.nodes.Remove(actualNode);

                actualNode = map.nodes.Where(n => n.x == newNode.x && n.y == newNode.y).First();
                actualX    = actualNode.x;
                actualY    = actualNode.y;

                nextSandSpot = map.nodes.Where(n => n.x == actualX && n.y == actualY + 1).FirstOrDefault();
            }
            else 
            {
                isSandResting = true;
            }   
        }
        else 
        {
            if (actualY >= maxY)
            {
                isSandResting = true;
            }
            else
            {
                var newNode = new Node(actualX, actualY + 1, 'o');

                map.nodes.Add(newNode);
                map.nodes.Remove(actualNode);

                actualNode   = map.nodes.Where(n => n.x == newNode.x && n.y == newNode.y).First();
                nextSandSpot = map.nodes.Where(n => n.x == actualNode.x && n.y == actualNode.y + 1).FirstOrDefault();

                actualY = actualNode.y;
            }
        }

        if (isSandResting && actualY == 0 && actualX == 500)
        {
            return (true, map);
        }
    }
    return (false, map);
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