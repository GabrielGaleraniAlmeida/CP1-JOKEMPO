using System;
using System.Collections.Generic;

namespace Jokempo
{
    public class Jogador
    {
        public string Nome { get; set; }
        public int Vitorias { get; set; }
        public int Derrotas { get; set; }
        public int Empates { get; set; }

        public Jogador(string nome)
        {
            Nome = nome;
        }
    }

    class Program
    {
        static Dictionary<string, Jogador> listaJogadores = new Dictionary<string, Jogador>(StringComparer.OrdinalIgnoreCase);
        static Jogador jogadorAtual;

        static void Main(string[] args)
        {
            Console.WriteLine("Bem-vindo ao Jokempo!");
            TrocarJogador(); 

            bool rodando = true;
            while (rodando)
            {
                rodando = ExibirMenu();
            }
        }

        static bool ExibirMenu()
        {
            Console.WriteLine($"\n--- JOKEMPO | Jogador Atual: {jogadorAtual.Nome} ---");
            Console.WriteLine("1. Jogar Clássico vs Computador");
            Console.WriteLine("2. Jogar Clássico Multiplayer (Local)");
            Console.WriteLine("3. Jogar Modo Round 6 (Menos Um) vs Computador");
            Console.WriteLine("4. Mudar de Jogador (Jogador 1)");
            Console.WriteLine("5. Ver Estatísticas");
            Console.WriteLine("6. Sair");
            Console.Write("Escolha uma opção: ");

            string opcao = Console.ReadLine();

            switch (opcao)
            {
                case "1":
                    Jogar();
                    return true;
                case "2":
                    JogarMultiplayer();
                    return true;
                case "3":
                    JogarRound6();
                    return true;
                case "4":
                    TrocarJogador();
                    return true;
                case "5":
                    ExibirEstatisticas();
                    return true;
                case "6":
                    Console.WriteLine("Saindo do jogo... Até mais!");
                    return false;
                default:
                    Console.WriteLine("Opção inválida. Pressione qualquer tecla para tentar novamente.");
                    Console.ReadKey();
                    return true;
            }
        }

        static void TrocarJogador()
        {
            Console.Write("\nDigite o nome do jogador: ");
            string nome = Console.ReadLine()?.Trim();

            while (string.IsNullOrEmpty(nome))
            {
                Console.Write("O nome não pode ser vazio. Digite novamente: ");
                nome = Console.ReadLine()?.Trim();
            }

            if (!listaJogadores.ContainsKey(nome))
            {
                listaJogadores[nome] = new Jogador(nome);
                Console.WriteLine($"Novo jogador '{nome}' registrado com sucesso!");
            }
            else
            {
                Console.WriteLine($"Bem-vindo de volta, {nome}!");
            }

            jogadorAtual = listaJogadores[nome];
        }

        static Jogador SelecionarAdversario()
        {
            Console.Write($"\nQuem será o adversário de {jogadorAtual.Nome}? Digite o nome: ");
            string nome = Console.ReadLine()?.Trim();

            while (string.IsNullOrEmpty(nome) || nome.Equals(jogadorAtual.Nome, StringComparison.OrdinalIgnoreCase))
            {
                Console.Write("Nome inválido ou igual ao Jogador 1. Digite novamente: ");
                nome = Console.ReadLine()?.Trim();
            }

            if (!listaJogadores.ContainsKey(nome))
            {
                listaJogadores[nome] = new Jogador(nome);
                Console.WriteLine($"Adversário '{nome}' registrado com sucesso!");
            }

            return listaJogadores[nome];
        }

        static void Jogar()
        {
            string[] opcoes = { "Pedra", "Papel", "Tesoura" };
            
            while (true) 
            {
                int escolhaUsuario = ObterEscolha(jogadorAtual.Nome, "escolha sua jogada");
                if (escolhaUsuario == 0) break; 

                Random rnd = new Random();
                int escolhaComputador = rnd.Next(1, 4);

                Console.WriteLine($"\n{jogadorAtual.Nome} escolheu: {opcoes[escolhaUsuario - 1]}");
                Console.WriteLine($"Computador escolheu: {opcoes[escolhaComputador - 1]}");

                DeterminarVencedor(escolhaUsuario, escolhaComputador);
            }
        }

        static void JogarMultiplayer()
        {
            Jogador adversario = SelecionarAdversario();
            string[] opcoes = { "Pedra", "Papel", "Tesoura" };

            Console.WriteLine("\n--- MODO MULTIPLAYER ---");
            Console.WriteLine("As jogadas serão secretas! Não olhem o teclado um do outro.");

            while (true)
            {
                int escolhaP1 = ObterEscolhaOculta(jogadorAtual.Nome);
                if (escolhaP1 == 0) break; 

                int escolhaP2 = ObterEscolhaOculta(adversario.Nome);
                if (escolhaP2 == 0) break; 

                Console.WriteLine("\n--- RESULTADO ---");
                Console.WriteLine($"{jogadorAtual.Nome} escolheu: {opcoes[escolhaP1 - 1]}");
                Console.WriteLine($"{adversario.Nome} escolheu: {opcoes[escolhaP2 - 1]}");

                DeterminarVencedorMultiplayer(escolhaP1, escolhaP2, jogadorAtual, adversario);
            }
        }

        static void JogarRound6()
        {
            string[] opcoes = { "Pedra", "Papel", "Tesoura" };
            Console.WriteLine("\n--- MODO ROUND 6 (MENOS UM) ---");
            Console.WriteLine("Regra: Escolha duas mãos. Veja as do adversário. Esconda uma e jogue com a outra!");

            while (true)
            {
                Console.WriteLine("\n--- NOVA RODADA ---");
                int mao1Usuario = ObterEscolha(jogadorAtual.Nome, "escolha sua PRIMEIRA mão");
                if (mao1Usuario == 0) break;

                int mao2Usuario = ObterEscolha(jogadorAtual.Nome, "escolha sua SEGUNDA mão");
                if (mao2Usuario == 0) break;

                Random rnd = new Random();
                int mao1Pc = rnd.Next(1, 4);
                int mao2Pc = rnd.Next(1, 4);

                Console.WriteLine("\n=== MÃOS REVELADAS ===");
                Console.WriteLine($"Suas mãos: [ {opcoes[mao1Usuario - 1]} ] e [ {opcoes[mao2Usuario - 1]} ]");
                Console.WriteLine($"Mãos do PC: [ {opcoes[mao1Pc - 1]} ] e [ {opcoes[mao2Pc - 1]} ]");

                Console.WriteLine("\n*** MENOS UM! ***");
                int escolhaFinalUsuario = 0;
                
                while (true)
                {
                    Console.WriteLine($"Qual mão você quer MANTER?");
                    Console.WriteLine($"1. {opcoes[mao1Usuario - 1]}");
                    Console.WriteLine($"2. {opcoes[mao2Usuario - 1]}");
                    Console.Write("Opção: ");
                    string decisao = Console.ReadLine();

                    if (decisao == "1") { escolhaFinalUsuario = mao1Usuario; break; }
                    else if (decisao == "2") { escolhaFinalUsuario = mao2Usuario; break; }
                    else { Console.WriteLine("Entrada inválida! Digite 1 ou 2."); }
                }

                // O PC escolhe aleatoriamente qual das duas mãos vai manter
                int escolhaFinalPc = rnd.Next(1, 3) == 1 ? mao1Pc : mao2Pc;

                Console.WriteLine($"\n{jogadorAtual.Nome} manteve: {opcoes[escolhaFinalUsuario - 1]}");
                Console.WriteLine($"Computador manteve: {opcoes[escolhaFinalPc - 1]}");

                DeterminarVencedor(escolhaFinalUsuario, escolhaFinalPc);
            }
        }

        static int ObterEscolha(string nome, string mensagem)
        {
            int escolha = 0;
            while (true)
            {
                Console.WriteLine($"\n{nome}, {mensagem}:");
                Console.WriteLine("1. Pedra | 2. Papel | 3. Tesoura | 0. Voltar ao Menu");
                Console.Write("Opção: ");
                
                string entrada = Console.ReadLine();

                if (int.TryParse(entrada, out escolha) && escolha >= 0 && escolha <= 3)
                {
                    break;
                }
                
                Console.WriteLine("Entrada inválida! Por favor, digite um número entre 0 e 3.");
            }
            return escolha;
        }

        static int ObterEscolhaOculta(string nome)
        {
            int escolha = 0;
            while (true)
            {
                Console.WriteLine($"\n{nome}, digite sua jogada (1. Pedra | 2. Papel | 3. Tesoura | 0. Voltar): ");
                
                var tecla = Console.ReadKey(true);
                string entrada = tecla.KeyChar.ToString();

                if (int.TryParse(entrada, out escolha) && escolha >= 0 && escolha <= 3)
                {
                    if (escolha != 0) Console.WriteLine("-> Jogada registrada em segredo!"); 
                    break;
                }
                
                Console.WriteLine("\nEntrada inválida! Pressione 1, 2, 3 ou 0.");
            }
            return escolha;
        }

        static void DeterminarVencedor(int usuario, int computador)
        {
            if (usuario == computador)
            {
                Console.WriteLine("Resultado: EMPATE!");
                jogadorAtual.Empates++;
            }
            else if ((usuario == 1 && computador == 3) || 
                     (usuario == 2 && computador == 1) || 
                     (usuario == 3 && computador == 2))   
            {
                Console.WriteLine($"Resultado: {jogadorAtual.Nome} VENCEU!");
                jogadorAtual.Vitorias++;
            }
            else
            {
                Console.WriteLine($"Resultado: {jogadorAtual.Nome} PERDEU!");
                jogadorAtual.Derrotas++;
            }
        }

        static void DeterminarVencedorMultiplayer(int p1, int p2, Jogador j1, Jogador j2)
        {
            if (p1 == p2)
            {
                Console.WriteLine("Resultado: EMPATE!");
                j1.Empates++;
                j2.Empates++;
            }
            else if ((p1 == 1 && p2 == 3) || 
                     (p1 == 2 && p2 == 1) || 
                     (p1 == 3 && p2 == 2))   
            {
                Console.WriteLine($"Resultado: A vitória é de {j1.Nome}!");
                j1.Vitorias++;
                j2.Derrotas++;
            }
            else
            {
                Console.WriteLine($"Resultado: A vitória é de {j2.Nome}!");
                j2.Vitorias++;
                j1.Derrotas++;
            }
        }

        static void ExibirEstatisticas()
        {
            Console.WriteLine("\n=== ESTATÍSTICAS DOS JOGADORES ===");
            foreach (var j in listaJogadores.Values)
            {
                int totalPartidas = j.Vitorias + j.Derrotas + j.Empates;
                Console.WriteLine($"- {j.Nome} | Partidas: {totalPartidas} | Vitórias: {j.Vitorias} | Derrotas: {j.Derrotas} | Empates: {j.Empates}");
            }
            Console.WriteLine("==================================");
        }
    }
}