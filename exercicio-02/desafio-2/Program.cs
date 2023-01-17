class Desafio
{
    static void Main(string[] args)
    {
        Console.WriteLine("========= Exercício 2 - Desafio 2 =========");

        var input = File.ReadAllLines("input.txt");
        
        var totalScore = 0;

        foreach(var line in input)
        {
            var letraEscolha = line.Split(' ');

            var escolhaOponente    = letraEscolha[0];
            var resultadoEsperado  = letraEscolha[letraEscolha.Length - 1];

            totalScore += ResultadoJogo(escolhaOponente, resultadoEsperado);
        }

        Console.WriteLine(totalScore);
    }

    /*
        Escolha Oponente:
        A - Pedra   (1 Pontos)
        B - Papel   (2 Pontos)
        C - Tesoura (3 Pontos)

        Resultado Jogador:
        X - Derrota (0 Pontos)
        Y - Empate  (3 Pontos)
        Z - Vitória (6 Pontos)
    */
    public static int ResultadoJogo(string escolhaOponente, string resultadoEsperado)
    {
        var oponente   = ValorJogada(escolhaOponente);
        var resultado  = ValorResultado(resultadoEsperado);

        const int derrota = 0;
        const int empate  = 3;
        const int vitoria = 6;

        const int pedra   = 1;
        const int papel   = 2;
        const int tesoura = 3;

        switch(oponente)
        {
            case pedra:
                switch(resultado)
                {
                    case derrota:
                        return tesoura + derrota;
                    case empate:
                        return pedra + empate;
                    case vitoria:
                        return papel + vitoria;
                    default:
                        return 0;
                }
            case papel:
                switch(resultado)
                {
                    case derrota:
                        return pedra + derrota;
                    case empate:
                        return papel + empate;
                    case vitoria:
                        return tesoura + vitoria;
                    default:
                        return 0;
                }
            case tesoura:
                switch(resultado)
                {
                    case derrota:
                        return papel + derrota;
                    case empate:
                        return tesoura + empate;
                    case vitoria:
                        return pedra + vitoria;
                    default:
                        return 0;
                }
            default:
                return 0;
        }       
    }

    public static int ValorJogada(string jogada)
    {
        if(jogada == "A")
            return 1;

        if(jogada == "B")
            return 2; 

        if(jogada == "C")
            return 3;

        return 0; 
    }

    public static int ValorResultado(string resultado)
    {
        if(resultado == "X")
            return 0;

        if(resultado == "Y")
            return 3; 

        if(resultado == "Z")
            return 6;

        return -1; 
    }
}