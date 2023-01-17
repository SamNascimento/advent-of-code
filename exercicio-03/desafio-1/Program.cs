Console.WriteLine("========= Exercício 3 - Desafio 1 =========");

var input = File.ReadAllLines("input.txt");

var prioridade = 0;

foreach(var line in input)
{
    var listaItens = new List<char>();

    var primeiroCompartimento = line.Substring(0, (int)(line.Length / 2));
    var segundoCompartimento  = line.Substring((int)(line.Length / 2), (int)(line.Length / 2));

    foreach(var item1 in primeiroCompartimento)
    {
        if(listaItens.Contains(item1))
            continue;

        foreach(var item2 in segundoCompartimento)
        {
            if (item1 == item2)
            {
                if(listaItens.Contains(item2))
                    continue;

                listaItens.Add(item1);
            }
        }
    }

    foreach(var item in listaItens)
    {
        prioridade += ValorPrioridade(item);
    }
}

Console.WriteLine("Prioridade dos itens repetidos é: " + prioridade);

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