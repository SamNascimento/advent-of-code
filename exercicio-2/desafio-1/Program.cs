class Desafio
{
    static void Main(string[] args)
    {
        Console.WriteLine("========= Exercício 2 - Desafio 1 =========");

        var input = File.ReadAllLines("input.txt");
        
        var totalScore = 0;

        foreach(var line in input)
        {
            var letraEscolha = line.Split(' ');

            var escolhaOponente = letraEscolha[0];
            var escolhaJogador  = letraEscolha[letraEscolha.Length - 1];

            totalScore += ResultadoJogo(escolhaOponente, escolhaJogador);
        }

        Console.WriteLine(totalScore);
    }

    /*
        Escolha Oponente:
        A - Pedra   (1 Pontos)
        B - Papel   (2 Pontos)
        C - Tesoura (3 Pontos)

        Escolha Jogador:
        X - Pedra   (1 Pontos)
        Y - Papel   (2 Pontos)
        Z - Tesoura (3 Pontos)

        Resultado:
        Derrota - 0 Pontos
        Empate  - 3 Pontos
        Vitória - 6 Pontos
    */
    public static int ResultadoJogo(string escolhaOponente, string escolhaJogador)
    {
        var oponente = ValorJogada(escolhaOponente);
        var jogador  = ValorJogada(escolhaJogador);

        const int derrota = 0;
        const int empate  = 3;
        const int vitoria = 6;

        const int pedra   = 1;
        const int papel   = 2;
        const int tesoura = 3;

        switch(oponente)
        {
            case pedra:
                switch(jogador)
                {
                    case pedra:
                        return jogador + empate;
                    case papel:
                        return jogador + vitoria;
                    case tesoura:
                        return jogador + derrota;
                    default:
                        return 0;
                }
            case papel:
                switch(jogador)
                {
                    case pedra:
                        return jogador + derrota;
                    case papel:
                        return jogador + empate;
                    case tesoura:
                        return jogador + vitoria;
                    default:
                        return 0;
                }
            case tesoura:
                switch(jogador)
                {
                    case pedra:
                        return jogador + vitoria;
                    case papel:
                        return jogador + derrota;
                    case tesoura:
                        return jogador + empate;
                    default:
                        return 0;
                }
            default:
                return 0;
        }       
    }

    public static int ValorJogada(string jogada)
    {
        if(jogada == "A" || jogada == "X")
            return 1;

        if(jogada == "B" || jogada == "Y")
            return 2; 

        if(jogada == "C" || jogada == "Z")
            return 3;

        return 0; 
    }
}