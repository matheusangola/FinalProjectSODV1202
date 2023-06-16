using System;
using System.Numerics;

namespace Connect_4
{
    public class Board
    {
        const int Rows = 6;
        const int Columns = 7;
        char[,] board = new char[Rows, Columns];

        public void ViewBoard()
        {
            for (int r = 0; r < Rows; r++)
            {
                for (int c = 0; c < Columns; c++)
                {
                    Console.Write(board[r, c]);
                    Console.Write('-');
                }
                Console.WriteLine();
            }
        }


        public void NewGame()
        {
            ViewBoard();
        }
    }
    
    
    class Program
    {
        static void Main(string[] args)
        {
            Board LoadGame = new Board();
            LoadGame.NewGame();

        }
    }
}
