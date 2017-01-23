using System;
using System.Drawing;
using System.Windows.Forms;

namespace FormExample
{
    public partial class Form1 : Form
    {

        int x;
        int y;
        int cellSize;
        int margin=10;
        int row = -1;
        int col = -1;
        int turncount = 0;
        int [,] gameboard = new int[3,3];
        int victor = -1;


        public Form1()
        {
            InitializeComponent();
            DoubleBuffered = true;
            UpdateSize();
            for (int k = 0; k < 3; k++)
            {
                for (int j = 0; j < 3; j++)
                {
                    gameboard[k, j] = -1;
                }
            }
        }

        public Boolean fullboard()
        {
            for (int k = 0; k < 3; k++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (gameboard[k, j] == -1)
                        return false;
                }
            }
            return true;
        }

        public void resetboard()
        {
            for (int k = 0; k < 3; k++)
            {
                for (int j = 0; j < 3; j++)
                {
                    gameboard[k, j] = -1;
                }
            }
            turncount = 0;
        }

        public void checkwinner()
        {
            if (fullboard() == false)
            {
                if (gameboard[0, 0] == gameboard[0, 1] && gameboard[0, 1] == gameboard[0, 2])
                {
                    victor = gameboard[0, 0];
                }
                if (gameboard[0, 0] == gameboard[1, 0] && gameboard[1, 0] == gameboard[2, 0])
                {
                    victor = gameboard[0, 0];
                }
                if (gameboard[0, 0] == gameboard[1, 1] && gameboard[1, 1] == gameboard[2, 2])
                {
                    victor = gameboard[0, 0];
                }
                if (gameboard[1, 0] == gameboard[1, 1] && gameboard[1, 1] == gameboard[1, 2])
                {
                    victor = gameboard[1, 0];
                }
                if (gameboard[2, 0] == gameboard[2, 1] && gameboard[2, 1] == gameboard[2, 2])
                {
                    victor = gameboard[2, 0];
                }
                if (gameboard[0, 1] == gameboard[1, 1] && gameboard[1, 1] == gameboard[2, 1])
                {
                    victor = gameboard[0, 1];
                }
                if (gameboard[0, 2] == gameboard[1, 2] && gameboard[1, 2] == gameboard[2, 2])
                {
                    victor = gameboard[0, 2];
                }
                if (gameboard[2, 0] == gameboard[1, 1] && gameboard[1, 1] == gameboard[0, 2])
                {
                    victor = gameboard[2, 0];
                }
            }
        }

        private void UpdateSize()
        {
            cellSize = (Math.Min(ClientSize.Width, ClientSize.Height) - 2 * margin)/3;
            if(ClientSize.Width> ClientSize.Height)
            {
                x = (ClientSize.Width - 3 * cellSize) / 2;
                y = margin;
            }
            else
            {
                x = margin;
                y= (ClientSize.Height - 3 * cellSize) / 2;
            }
        }

        protected override void OnResize(EventArgs e)
        {

            base.OnResize(e);
            UpdateSize();
            Refresh();
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            col = (int)Math.Floor((e.X - x)*1.0 / cellSize);
            row = (int)Math.Floor((e.Y - y)*1.0 / cellSize);
            
            if (fullboard() || victor != -1) resetboard();
            else if (col > -1 && row > -1 && col < 3 && row < 3 && gameboard[col, row] == -1)
            {
                gameboard[col, row] = turncount;
                turncount = 1 - turncount;
            }
            checkwinner();
            Refresh();
            //base.OnMouseDown(e);
        }

        protected override void OnPaint(PaintEventArgs e)
        {

            //base.OnPaint(e);
            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 3; j++)
                {
                    Rectangle rect = new Rectangle(x + i * cellSize, y + j * cellSize, cellSize, cellSize);
                    e.Graphics.DrawRectangle(Pens.Chocolate, rect);
                    System.Drawing.Font font = new System.Drawing.Font("Ubuntu", cellSize * 3 * 55 / 96 / 4);
                    if (victor != -1)
                    {
                        e.Graphics.DrawString("Player " + (victor + 1) + " wins.", font, Brushes.DarkSalmon, ClientSize.Width/50, ClientSize.Height/3);
                    }
                    if (fullboard() == true)
                    {
                        e.Graphics.DrawString("Draw.", font, Brushes.DarkSalmon, ClientSize.Width / 50, ClientSize.Height / 3);
                    }
                    if (gameboard[i, j] == 0)
                    {
                        e.Graphics.DrawString("X", font, Brushes.ForestGreen, x + (cellSize / 8) + (cellSize * i), y + (cellSize / 8) + (cellSize * j));
                    }
                    if (gameboard[i, j] == 1)
                    {
                        e.Graphics.DrawString("O", font, Brushes.Tomato, x + (cellSize / 8) + (cellSize * i), y + (cellSize / 8) + (cellSize * j));
                    }
                    
                }
            checkwinner();
        }

    }
    
}
