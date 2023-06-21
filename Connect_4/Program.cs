using System;
using System.Collections.Generic;
using System.Numerics;

namespace Connect_4
{
    // SUPER CLASS PLAYER
    // Parent of Human and AI (Computer)

    // Using INTERFACE (IComparable) for score comparison

    public abstract class Player : IComparable<Player>
    {
        public string Name { get; set; }
        public char PlayerSymbol { get; set; }
        public int PlayerScore { get; set; }
        public bool Human { get; set; }

        // Player Constructor

        public Player(string name, char playersymbol)
        {
            Name = name;
            PlayerSymbol = playersymbol;
        }

        // abstract method for player's move. Overriden in Human and AI

        public abstract int Move();

        // CompareTo using INTERFACE

        public int CompareTo(Player other)
        {
            return PlayerScore.CompareTo(other.PlayerScore);
        }
    }

    // Child of Player.

    public class Human : Player
    {
        public Human(string name, char playersymbol) : base(name, playersymbol)
        {
            PlayerScore = 0;
            Human = true;
        }

        public override int Move()
        {
            return int.Parse(Console.ReadLine());
        }

    }

    // Child of Player

    public class AI : Player
    {
        public AI(string name, char playersymbol) : base(name, playersymbol)
        {
            PlayerScore = 0;
            Human = false;
        }

        public override int Move()
        {
            Random rnd = new Random();
            int num = rnd.Next(0,7);
            return num;
        }

    }

    // Class that creates the board and fills it with '#'

    public class Board
    {
        public int Rows;
        public int Columns;
        public char[,] board;

        public Board()
        {
            Rows = 6;
            Columns = 7;
            board = new char[Rows, Columns];
        }

        public void ViewBoard()
        {
            string boardView = "";
            for (int r = 0; r < Rows; r++)
            {
                boardView += "| ";
                for (int c = 0; c < Columns; c++)
                {
                    boardView += board[r, c] + " ";
                }
                boardView += "| \n";
            }
            boardView += " ";
            for (int i = 1; i < 8; i++)
            {
                boardView += " " + i;
            }
            Console.WriteLine(boardView);
        }

        public void FillBoard()
        {
            for (int r = 0; r < Rows; r++)
            {

                for (int c = 0; c < Columns; c++)
                {
                    board[r, c] = '#';
                }

            }
        }
    }

    // This class initializes most of the program

    public class Setup
    {

        public List<Player> ListOfPlayers { get; set; }
        public int Turn { get; set; }
        public bool MatchOver { get; set; }
        public Board LoadBoard { get; set; }
        public bool GameOver { get; set; }

        // Setup CONSTRUCTOR

        public Setup()
        {
            LoadBoard = new Board();
            LoadBoard.FillBoard();
            LoadBoard.ViewBoard();
            Turn = 0;
            MatchOver = false;
            GameOver = false;
            ListOfPlayers = new List<Player>(2);
            Welcome();
            NewPlayers();
        }


        public void Welcome()
        {
            string welcome = "Welcome to Connect 4!";
            Console.WriteLine(welcome);
        }

        // Gets number of players and initializes NewPlayers.

        public void NewPlayers()
        {
            string NumberOfPlayers;
            do
            {
                Console.WriteLine("How many Players will play? 1 or 2?");
                NumberOfPlayers = Console.ReadLine();
            }
            while (!(NumberOfPlayers == "1" || NumberOfPlayers == "2"));

            // IF ONLY 1 PLAYER, THEN THE OTHER WILL AUTOMATICALLY BE "AI".

            if (NumberOfPlayers == "1")
            {
                Console.WriteLine("Enter Player 1 name: ");
                String Player1Name = Console.ReadLine();
                Player PlayerOne = new Human(Player1Name, 'X');
                ListOfPlayers.Add(PlayerOne);

                Console.WriteLine("Computer name is: AI");
                String Player2Name = "AI";
                Player PlayerTwo = new AI(Player2Name, 'O');
                ListOfPlayers.Add(PlayerTwo);
            }
            else if (NumberOfPlayers == "2")
            {
                Console.WriteLine("Enter Player 1 name: ");
                String Player1Name = Console.ReadLine();
                Player PlayerOne = new Human(Player1Name, 'X');
                ListOfPlayers.Add(PlayerOne);


                Console.WriteLine("Enter Player 2 name: ");
                String Player2Name = Console.ReadLine();
                Player PlayerTwo = new Human(Player2Name, 'O');
                ListOfPlayers.Add(PlayerTwo);
            }
            
        }

        // INPUT OPTIONS FOR PLAYER. IF INVALID INPUT THE PROGRAM CATCHES THE ERROR AND LOOPS BACK FOR ANOTHER INPUT.

        public void PlayerMove(Player playerPlaying)
        {
            Console.WriteLine($"Player {playerPlaying.Name}, enter your column choice (1-7): ");

            int chosenColumn = 0;

            do
            {
                try
                {
                    chosenColumn = playerPlaying.Move();
                    for (int r = LoadBoard.Rows - 1; r >= 0; r--)
                    {
                        if (LoadBoard.board[r, chosenColumn - 1] == '#')
                        {
                            LoadBoard.board[r, chosenColumn - 1] = playerPlaying.PlayerSymbol;
                            break;
                        }
                    }
                } catch (IndexOutOfRangeException e)
                {
                    Console.WriteLine($"{playerPlaying.Name}, Invalid input. Please enter a column from 1-7. Error: {e.Message}");
                }
                catch (FormatException e)
                {
                    Console.WriteLine($"{playerPlaying.Name}, Invalid input. Please enter a column from 1-7. Error: {e.Message}");
                }
                catch (Exception e)
                {
                    Console.WriteLine($"{playerPlaying.Name}, Invalid input. Please enter a column from 1-7. Error: {e.Message}");
                }

            } while (!(chosenColumn >= 1 && chosenColumn <= 7));
            
        }

        //RESET MATCH, FILLS THE BOARD WITH '#'AGAIN AND RESETS TURN.

        public void Restart()
        {
            LoadBoard.FillBoard();
            MatchOver = false;
            Turn = 0;
            LoadBoard.ViewBoard();
        }

        // ALL THE LOGIC FOR CHECKING IF PLAYER WON.

        public bool CheckWin(Player playerPlaying)
        {
            char currentSymbol = playerPlaying.PlayerSymbol;

            //CHECK COLUMNS

            for (int r = 0; r < 3; r++)
            {
                for (int c = 0; c < 7; c++)
                {
                    if ((LoadBoard.board[r, c] == currentSymbol) && (LoadBoard.board[r + 1, c] == currentSymbol) && (LoadBoard.board[r + 2, c] == currentSymbol) && (LoadBoard.board[r + 3, c] == currentSymbol))
                    {
                        MatchOver = true;
                        playerPlaying.PlayerScore += 1;
                        return true;
                    }
                    
                }
                
            }

            //CHECK ROWS

            for (int r = 0; r < 6; r++)
            {
                for (int c = 0; c < 4; c++)
                {

                        if ((LoadBoard.board[r, c] == currentSymbol) && (LoadBoard.board[r, c + 1] == currentSymbol) && (LoadBoard.board[r, c + 2] == currentSymbol) && (LoadBoard.board[r, c + 3] == currentSymbol))
                    {
                        MatchOver = true;
                        playerPlaying.PlayerScore += 1;
                        return true;
                    }
                }
            }

            //CHECK DIAGONALS TOP LEFT TO BOTTOM RIGHT

            for (int r = 0; r < 3; r++)
            {
                for (int c = 0; c < 4; c++)
                {
                    if ((LoadBoard.board[r, c] == currentSymbol) && (LoadBoard.board[r + 1, c + 1] == currentSymbol) && (LoadBoard.board[r + 2, c + 2] == currentSymbol) && (LoadBoard.board[r + 3, c + 3] == currentSymbol))
                    {
                        MatchOver = true;
                        playerPlaying.PlayerScore += 1;
                        return true;
                    }
                }
            }

            //CHECK DIAGONALS BOTTOM LEFT TO TOP RIGHT

            for (int r = 3; r < 6; r++)
            {
                for (int c = 0; c < 4; c++)
                {
                    if ((LoadBoard.board[r, c] == currentSymbol) && (LoadBoard.board[r - 1, c + 1] == currentSymbol) && (LoadBoard.board[r - 2, c + 2] == currentSymbol) && (LoadBoard.board[r - 3, c + 3] == currentSymbol))
                    {
                        MatchOver = true;
                        playerPlaying.PlayerScore += 1;
                        return true;
                    }
                }
            }
            return false;
        }

        // IN CASE OF ALL SLOTS FILLED, RETURN TRUE.

        public bool Tie()
        {
            for (int r = 0; r < 6; r++)
            {
                for (int c = 0; c < 7; c++)
                {
                    if (LoadBoard.board[r, c] == '#')
                    {
                        return false;
                    } 
                }
            }
            MatchOver = true;
            return true;
        }
    }

    // START PROGRAM AND LOOPS GAME.

    class Program
    {
        static void Main(string[] args)
        {

            Setup gameSetup = new Setup();
            Player playerPlaying;

            do
            {
                do
                {
                    playerPlaying = gameSetup.ListOfPlayers[gameSetup.Turn % 2];
                    gameSetup.PlayerMove(playerPlaying);
                    gameSetup.LoadBoard.ViewBoard();
                    gameSetup.Turn++;
                    if (gameSetup.CheckWin(playerPlaying) == true)
                    {
                        Console.WriteLine($"{playerPlaying.Name} WON!!\n{playerPlaying.Name} won {playerPlaying.PlayerScore} time(s).");
                    }
                    if (gameSetup.Tie() == true)
                    {
                        Console.WriteLine("DRAW!");
                    }
                }
                while (!gameSetup.MatchOver);
                
                string restart;
                int ComparedScore;
                do
                {
                    ComparedScore = gameSetup.ListOfPlayers[0].CompareTo(gameSetup.ListOfPlayers[1]);
                    if(ComparedScore == 1)
                    {
                        Console.WriteLine($"So far {gameSetup.ListOfPlayers[0].Name} had more victories!");
                    } else if (ComparedScore == -1)
                    {
                        Console.WriteLine($"So far {gameSetup.ListOfPlayers[1].Name} had more victories!");
                    } else
                    {
                        Console.WriteLine($"So far it's a tie between {gameSetup.ListOfPlayers[1].Name} and {gameSetup.ListOfPlayers[0].Name}!");
                    }
                    Console.WriteLine("Restart? YES(1) NO(0)");
                    restart = Console.ReadLine();
                }
                while (!(restart == "0" || restart == "1"));
                if(restart == "1")
                {
                    gameSetup.Restart();
                } else
                {
                    gameSetup.GameOver = true;
                }
            }
            while (!gameSetup.GameOver);
            

        }
    }
}

