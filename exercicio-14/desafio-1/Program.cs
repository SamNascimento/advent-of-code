Console.WriteLine("========= Exercício 14 - Desafio 1 =========");                  
 
// var input = File.ReadAllLines("test.txt");
var input = File.ReadAllLines("input.txt");

var map = new CaveMap();

foreach (var line in input)
{
    
}

#region Methods
public void ConstructMap (CaveMap map, string begin, string end)
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
                var newNode = new Node(i, endY, '#');
            }
        }
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