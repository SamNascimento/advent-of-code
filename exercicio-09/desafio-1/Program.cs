Console.WriteLine("========= Exercício 9 - Desafio 1 =========");                  
 
var input = File.ReadAllLines("input.txt");
// var input = File.ReadAllLines("test.txt");

var head      = new Position(0, 0);
var tailsList = new List<Position>(); 

tailsList.Add(new Position(0, 0));

foreach(var line in input)
{
    var lineSplited = line.Split(' ');
    var direction   = lineSplited[0];
    var numSteps    = int.Parse(lineSplited[1]);

    for (var i = 0; i < numSteps; i++)
    {
        head = MoveHead(head, direction);
        tailsList.Add(MoveTail(head, tailsList.Last()));
    }
}

var uniqueListOfTails = new List<Position>();

foreach (var tail in tailsList)
    uniqueListOfTails = UniqueLocations(uniqueListOfTails, tail);

var countUniqueLocationsTail = uniqueListOfTails.Count();

Console.WriteLine(countUniqueLocationsTail);

#region Methods

Position MoveHead(Position head, string moviment)
{
    switch (moviment)
    {
        case "U":
            head.y += 1;
            break;
        case "D":
            head.y -= 1;
            break;
        case "L":
            head.x -= 1;
            break;
        case "R":
            head.x += 1;
            break;
        default:
            break;
    }

    return head;
}

Position MoveTail(Position head, Position tail)
{
    if (tail.x == head.x && tail.y == head.y)
        return tail;

    var newTail = new Position(tail.x, tail.y);
    
    if (head.x > newTail.x + 1)
    {
        if (head.y > newTail.y)
            newTail.y += 1;
        else if (head.y < newTail.y)
            newTail.y -= 1;

        newTail.x += 1;
    }    
    else if (head.x < newTail.x - 1)
    {
        if (head.y > newTail.y)
            newTail.y += 1;
        else if (head.y < newTail.y)
            newTail.y -= 1;

        newTail.x -= 1;
    }
    else if (head.y > newTail.y + 1)
    {
        if (head.x > newTail.x)
            newTail.x += 1;
        else if (head.x < newTail.x)
            newTail.x -= 1;

        newTail.y += 1;
    }    
    else if (head.y < newTail.y - 1)
    {
        if (head.x > newTail.x)
            newTail.x += 1;
        else if (head.x < newTail.x)
            newTail.x -= 1;

        newTail.y -= 1;
    }

    return newTail;
}

List<Position> UniqueLocations(List<Position> listTails, Position currentTail)
{
    var haveSameTail = listTails.Where(l => l.x == currentTail.x && l.y == currentTail.y).Any();

    if (!haveSameTail)
        listTails.Add(currentTail);

    return listTails;
}
#endregion

#region Class
public class Position
{
    public int x {get; set;}
    public int y {get; set;}

    public Position(int x, int y)
    {
        this.x    = x;
        this.y    = y;
    }
}
#endregion