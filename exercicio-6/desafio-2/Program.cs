Console.WriteLine("========= Exercício 6 - Desafio 2 =========");                  
 
// var input      = File.ReadAllText("test-1.txt");
var input      = File.ReadAllText("input.txt");
var cleanInput = input.Replace("\r", "");

var indexOfStart = 0;

for (var i = 13; i < cleanInput.Length; i++)
{
    var hashString = cleanInput.Substring(i-13, 14);
    var sizeOfHash = hashString.Distinct().Count();

    if (sizeOfHash == 14)
    {
        indexOfStart = i + 1;
        break;
    }
}

Console.WriteLine("The start-of-packet index is: " + indexOfStart);