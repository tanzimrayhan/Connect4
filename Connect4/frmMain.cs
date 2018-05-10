using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Connect4
{
    public partial class frmMain : Form
    {
        private Rectangle[] boardColumns;
        private int[,] board;
        private int turn;

        public frmMain()
        {
            InitializeComponent();
            //FormBorderStyle = FormBorderStyle.None;
            WindowState = FormWindowState.Maximized;


            boardColumns = new Rectangle[7];
            board = new int[6, 7];
            turn = 1;
            DoubleBuffered = true;
        }

        private void frmMain_Paint(object sender, PaintEventArgs e)
        {

            e.Graphics.FillRectangle(Brushes.Black, 104, 104, 1200, 680);
            for (int i = 0; i < 6; i++)
            {
                for (int j = 0; j < 7; j++)
                {
                    if (i == 0)
                    {
                        boardColumns[j] = new Rectangle(150 + 170 * j, 48, 100, 680);
                    }
                    e.Graphics.FillEllipse(Brushes.White, 150 + 170 * j, 120 + 100* i, 75, 75);
                }
            }
        }

        private void frmMain_MouseClick(object sender, MouseEventArgs e)
        {
            int columIndex = ColumnNumber(e.Location);
            if (columIndex != -1)
            {
                int rowIndex = EmptyRow(columIndex);
                if (rowIndex != -1)
                {
                    board[rowIndex, columIndex] = turn;
                    if (turn == 1)
                    {
                        Graphics g = CreateGraphics();
                        g.FillEllipse(Brushes.Red, 150 + 170 * columIndex, 120 + 100 * rowIndex, 75, 75);
                    }
                    else if (turn == 2)
                    {
                        Graphics g = CreateGraphics();
                        g.FillEllipse(Brushes.Yellow, 150 + 170 * columIndex, 120 + 100 * rowIndex, 75, 75);

                    }
                    int winner = WinningPlayer(turn);
                    if (winner != -1)
                    {
                        string player = (winner == 1) ? "Red" : "Blue";
                        MessageBox.Show("Congratulations! " + player + " Player has won!");
                        Application.Restart();
                    }

                    if (turn == 1)
                    {
                        turn = 2;
                    }
                    else turn = 1;
                }
            }
        }

        private int WinningPlayer(int playerToCheck)
        {
            // Vertical check (|)
            for (int row = 0; row < board.GetLength(0)-3; row++)
            {
                for (int col = 0; col < board.GetLength(1); col++)
                {
                    if (NumbersEqual(playerToCheck, board[row, col], board[row+1, col], board[row+2, col], board[row+3, col]))
                    {
                        return playerToCheck;
                    }
                }
            }

            // Horizontal check (-)
            for (int row = 0; row < board.GetLength(0); row++)
            {
                for (int col = 0; col < board.GetLength(1)-3; col++)
                {
                    if (NumbersEqual(playerToCheck, board[row, col], board[row, col+1], board[row, col+2], board[row, col+3]))
                    {
                        return playerToCheck;
                    }
                }
            }

            // top-left diagonal win check (\)
            for (int row = 0; row < board.GetLength(0) -3; row++)
            {
                for (int col = 0; col < board.GetLength(1) - 3; col++)
                {
                    if (NumbersEqual(playerToCheck, board[row, col], board[row + 1, col + 1], board[row + 2, col + 2], board[row + 3, col + 3]))
                    {
                        return playerToCheck;
                    }
                }
            }

            // top-right diagonal win check (\)
            for (int row = 0; row < board.GetLength(0) - 3; row++)
            {
                for (int col = 3; col < board.GetLength(1) - 3; col++)
                {
                    if (NumbersEqual(playerToCheck, board[row, col], board[row + 1, col - 1], board[row + 2, col - 2], board[row + 3, col -3]))
                    {
                        return playerToCheck;
                    }
                }
            }

            return -1;
        }

        private bool NumbersEqual(int toCheck, params int[] numbers)
        {
            foreach (int num in numbers)
            {
                if (num != toCheck)
                {
                    return false;
                }
            }return true;
        }

        private int ColumnNumber(Point mouse)
        {
            for (int i = 0; i < boardColumns.Length; i++)
            {
                if ((mouse.X >= boardColumns[i].X) && (mouse.Y >= boardColumns[i].Y))
                {
                    if((mouse.X <= boardColumns[i].X + boardColumns[i].Width) && (mouse.Y <= boardColumns[i].Y + boardColumns[i].Height))
                    {
                        return i;
                    }
                }
            }return -1;
        }

        private int EmptyRow(int col)
        {
            for (int i = 5; i >= 0 ; i--)
            {
                if (board[i, col] == 00)
                    return i;
            }
            return -1;
        }

        private void frmMain_Load(object sender, EventArgs e)
        {

        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnRestart_Click(object sender, EventArgs e)
        {
            Application.Restart();
        }
    }
}
