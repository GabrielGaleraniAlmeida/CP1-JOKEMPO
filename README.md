# CP1-JOKEMPO

```markdown
# Jokempo (Pedra, Papel e Tesoura) - C# 🎮

Este projeto é uma implementação clássica e expandida do jogo Jokempo para o terminal, desenvolvido em C#. O objetivo do repositório é aplicar conceitos de Engenharia de Software, como Orientação a Objetos, modularização de código, *game loops* e validação de dados.

## 🚀 Funcionalidades Implementadas

O jogo foi construído com um menu interativo contendo as seguintes *features*:

* **Modularização e POO:** Separação de responsabilidades utilizando a classe `Jogador` para gerenciamento de estado e a classe `Program` para a lógica do jogo.
* **Gestão de Jogadores:** Sistema para registrar e armazenar os nomes dos jogadores em memória durante a execução.
* **Modo Clássico vs Computador:** O jogador enfrenta a máquina em um *loop* contínuo de partidas.
* **Modo Clássico Multiplayer (Local):** Partidas entre dois jogadores físicos utilizando o mesmo teclado, com captura de teclas oculta (`Console.ReadKey(true)`) para manter o segredo das jogadas.
* **Modo Round 6 (Menos Um):** Uma variante estratégica do jogo onde o usuário escolhe duas mãos simultâneas, analisa as mãos sorteadas pelo computador e decide qual das suas duas opções vai esconder antes de resolver a rodada.
* **Estatísticas de Partida:** Rastreamento em tempo real de Vitórias, Derrotas e Empates para cada jogador registrado na sessão.
* **Validação de Dados:** Prevenção contra entradas vazias (nomes) e tratamento de erros de digitação (escolhas inválidas no menu e durante o jogo).

## 💻 Como Executar

Certifique-se de ter o [.NET SDK](https://dotnet.microsoft.com/download) instalado em sua máquina.

1. Clone este repositório:
   ```bash
   git clone [https://github.com/GabrielGaleraniAlmeida/CP1-JOKEMPO.git](https://github.com/GabrielGaleraniAlmeida/CP1-JOKEMPO.git)

2. Acesse a pasta do projeto:
```bash
cd CP1-JOKEMPO

```


3. Execute o jogo através da CLI do .NET:
```bash
dotnet run

```



## 👥 Membros do Grupo

* **Gabriel Galerani Almeida** (RM: 557421)
* **Gabriel Dias** (RM: 556830)
* **Guatavo Texeira** (RM: 557876)
* **Pedro Paulo** (RM: 554880)
* **Leonardo Taschin** (RM: 554583)

---

*Projeto desenvolvido para a disciplina de C# - FIAP (Engenharia de Software).*

```

