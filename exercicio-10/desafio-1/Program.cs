Console.WriteLine("========= Exercício 10 - Desafio 1 =========");                  
 
// var input = File.ReadAllLines("test.txt");
var input = File.ReadAllLines("input.txt");

var x     = 1;
var listX = new List<int>();

var actionType  = enumActionType.read;
var execNumber  = 0;
var actualCycle = 1;
int[] keyCycles = {20, 60, 100, 140, 180, 220};

for (var i=0; i < input.Length; i++)
{
    if (keyCycles.Contains(actualCycle))
        listX.Add(actualCycle * x);

    if (actionType == enumActionType.read)
    {
        if (input[i].Contains("noop"))
        {
            actualCycle ++;
            continue;
        }
        
        var line   = input[i].Split(' ');
        execNumber = int.Parse(line[1]);
        actionType = enumActionType.execute;
        
        actualCycle ++;
        i --;      
    }
    else
    {
        x += execNumber;
        actionType = enumActionType.read;
        
        actualCycle ++;
    }
}

Console.WriteLine("The sum of X value in 20th, 60th, 100th, 140th, 180th, and 220th cycles is: ");
Console.WriteLine(listX.Sum());

#region Enums
public enum enumActionType
{
    read = 0,
    execute
}
#endregion