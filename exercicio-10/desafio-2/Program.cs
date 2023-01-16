Console.WriteLine("========= Exercício 10 - Desafio 2 =========");                  
 
// var input = File.ReadAllLines("test.txt");
var input = File.ReadAllLines("input.txt");

var x = 1;

var lineDraw = "";

var actionType  = EnumActionType.read;
var execNumber  = 0;
var actualCycle = 1;
int[] keyCycles = {40, 80, 120, 160, 200, 240};

for (var i=0; i < input.Length; i++)
{
    var actualSprite = DrawSprite(x);
    var currentChar  = actualSprite[(actualCycle-1)%40];

    if (keyCycles.Contains(actualCycle-1))
        lineDraw += '\n';

    if (actionType == EnumActionType.read)
    {
        if (input[i].Contains("noop"))
        {   
            lineDraw += currentChar == '#' ? '#' : '.';

            actualCycle ++;
            continue;
        }
        
        var line   = input[i].Split(' ');
        execNumber = int.Parse(line[1]);
        actionType = EnumActionType.execute;

        lineDraw += currentChar == '#' ? '#' : '.';
        
        actualCycle ++;
        i --;      
    }
    else
    {
        x += execNumber;
        actionType = EnumActionType.read;

        lineDraw += currentChar == '#' ? '#' : '.';

        actualCycle ++;
    }
}

Console.WriteLine("Draw generate by commands: ");
Console.WriteLine(lineDraw);

#region Methods
string DrawSprite(int x)
{
    var sprite = "";

    for (var i = 0; i < 40; i++)
    {
        if (i == x-1 || i == x || i == x+1)
            sprite += '#';
        else
            sprite += '.';
    }
    
    return sprite;
}
#endregion

#region Enums
enum EnumActionType
{
    read = 0,
    execute
}
#endregion