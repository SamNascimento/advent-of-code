Console.WriteLine("========= Exercício 3 - Desafio 2 =========");

var input = File.ReadAllLines("input.txt");

var prioridade = 0;

for(int i = 0; i < input.Length; i += 3)
{
    var elfo1 = input[i];
    var elfo2 = input[i+1];
    var elfo3 = input[i+2];

    var ch = '4';

    foreach(var item1 in elfo1)
    {
        foreach(var item2 in elfo2)
        {
            foreach(var item3 in elfo3)
            {
                if (item1 == item2 && item2 == item3)
                {
                    ch = item1;
                    continue;
                }
            }
        }
    }

    prioridade += ValorPrioridade(ch);
}

Console.WriteLine($"A prioridade é: {prioridade}");

int ValorPrioridade(char item)
{
    if(isMinusculo(item))
        return (int)(item - 'a')+1;

    if(isMaiusculo(item))
        return (int)(item - 'A')+27;

    return 0;
}

bool isMinusculo(char ch) => ch >= 'a' && ch <= 'z';
bool isMaiusculo(char ch) => ch >= 'A' && ch <= 'Z';