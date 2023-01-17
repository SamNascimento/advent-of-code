Console.WriteLine("========= Exercício 6 - Desafio 1 =========");                  
 
// var input      = File.ReadAllText("test-1.txt");
var input      = File.ReadAllText("input.txt");
var cleanInput = input.Replace("\r", "");

var indexOfStart = 0;

for (var i = 3; i < cleanInput.Length; i++)
{
    var hashString = cleanInput.Substring(i-3, 4);
    var sizeOfHash = hashString.Distinct().Count();

    if (sizeOfHash == 4)
    {
        indexOfStart = i + 1;
        break;
    }
}

Console.WriteLine("The start-of-packet index is: " + indexOfStart);