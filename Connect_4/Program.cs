using System;
using System.Collections.Generic;
using System.Numerics;

namespace Connect_4
{
    public abstract class Player
    {
        public string Name { get; set; }
        public char PlayerSymbol { get; set; }
        public int PlayerScore { get; set; }
        public bool Human { get; set; }

        public Player(string name, char playersymbol)
        {
            Name = name;
            PlayerSymbol = playersymbol;
        }
    }

    public class Human : Player
    {
        public Human(string name, char playersymbol) : base(name, playersymbol)
        {
            PlayerScore = 0;
            Human = true;
        }

    }

    public class AI : Player
    {
        public AI(string name, char playersymbol) : base(name, playersymbol)
        {
            PlayerScore = 0;
            Human = false;
        }

    }

    public class Board
    {
        public int Rows;
        public int Columns;
        public char[,] board;
        public List<int> AllowedInput;

        public Board()
        {
            AllowedInput = new List<int> { 1, 2, 3, };
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
            Console.Write(boardView);
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



    public class Setup
    {

        //Board LoadBoard = new Board();
        //LoadBoard.FillBoard();
        public List<Player> ListOfPlayers { get; set; }
        public int Turn { get; set; }
        public bool GameOver { get; set; }
        public Board LoadBoard { get; set; }

        public Setup()
        {
            LoadBoard = new Board();
            LoadBoard.FillBoard();
            LoadBoard.ViewBoard();
            Turn = 0;
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

        public void NewPlayers()
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

        public void PlayerMove(Player playerPlaying)
        {
            Console.WriteLine($"Player {playerPlaying.Name}, enter your column choice (1-7): ");
            int chosenColumn = int.Parse(Console.ReadLine());
            for (int r = LoadBoard.Rows - 1; r >= 0; r--)
            {
                if (LoadBoard.board[r, chosenColumn - 1] == '#')
                {
                    LoadBoard.board[r, chosenColumn - 1] = playerPlaying.PlayerSymbol;
                    break;
                }
            }
        }

        public void Restart()
        {

        }
    }


    class Program
    {
        static void Main(string[] args)
        {

            Setup gameSetup = new Setup();
            Player playerPlaying;
            do
            {
                playerPlaying = gameSetup.ListOfPlayers[gameSetup.Turn % 2];
                gameSetup.PlayerMove(playerPlaying);
                gameSetup.LoadBoard.ViewBoard();
                gameSetup.Turn++;
            }
            while (!gameSetup.GameOver);


        }
    }
}

