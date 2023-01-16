Console.WriteLine("========= Exercício 10 - Desafio 1 =========");                  
 
// var input = File.ReadAllLines("test.txt");
var input = File.ReadAllLines("input.txt");

var x     = 1;
var listX = new List<int>();

var actionType  = EnumActionType.read;
var execNumber  = 0;
var actualCycle = 1;
int[] keyCycles = {20, 60, 100, 140, 180, 220};

for (var i=0; i < input.Length; i++)
{
    if (keyCycles.Contains(actualCycle))
        listX.Add(actualCycle * x);

    if (actionType == EnumActionType.read)
    {
        if (input[i].Contains("noop"))
        {
            actualCycle ++;
            continue;
        }
        
        var line   = input[i].Split(' ');
        execNumber = int.Parse(line[1]);
        actionType = EnumActionType.execute;
        
        actualCycle ++;
        i --;      
    }
    else
    {
        x += execNumber;
        actionType = EnumActionType.read;
        
        actualCycle ++;
    }
}

Console.WriteLine("The sum of X value in 20th, 60th, 100th, 140th, 180th, and 220th cycles is: ");
Console.WriteLine(listX.Sum());

#region Enums
public enum EnumActionType
{
    read = 0,
    execute
}
#endregion