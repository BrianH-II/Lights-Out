using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LightsOut
{
    class Board
    {

        public Button[,] boardButtons;
        public List<Point> winClicks = new List<Point>();

        private int buttonLength = 35;

        public Board(int sideLength, Point offset)
        {
            boardButtons = new Button[sideLength, sideLength];
            for (int i = 0; i < sideLength; i++)
            {
                for (int j = 0; j < sideLength; j++)
                {
                    boardButtons[i, j] = new Button();
                    boardButtons[i, j].Size = new Size(buttonLength, buttonLength);
                    boardButtons[i, j].Location = new Point(offset.X + i * buttonLength, offset.Y + j * buttonLength);
                    boardButtons[i, j].Click += new EventHandler(BoardButton_Click);
                }
            }

            LightBoard();
        }

        public void ShowSolution()
        {
            foreach (Button b in boardButtons)
            {
                b.Text = "";
            }

            foreach (Point p in winClicks)
            {
                boardButtons[p.X, p.Y].Text = "*";
            }
        }

        private void BoardButton_Click(Object sender, EventArgs e)
        {
            if (winClicks.Count != 0)
            {
                bool done = false;
                for (int i = 0; i < Math.Sqrt(boardButtons.Length); i++)
                {
                    for (int j = 0; j < Math.Sqrt(boardButtons.Length); j++)
                    {
                        if (sender == boardButtons[i, j])
                        {
                            ChangeLightValues(i, j);
                            boardButtons[i, j].Text = "";


                            if (!winClicks.Contains(new Point(i, j)))
                            {
                                winClicks.Add(new Point(i, j));
                            }
                            else
                            {
                                winClicks.Remove(new Point(i, j));
                            }

                            done = true;
                            break;
                        }
                    }

                    if (done)
                    {
                        break;
                    }
                }
            }
        }

        private void ChangeLightValues(int i, int j)
        {
            if (boardButtons[i, j].BackColor == Color.Red)
            {
                boardButtons[i, j].BackColor = SystemColors.Control;
            }
            else
            {
                boardButtons[i, j].BackColor = Color.Red;
            }

            if (i > 0)
            {
                if (boardButtons[i - 1, j].BackColor == Color.Red)
                {
                    boardButtons[i - 1, j].BackColor = SystemColors.Control;
                }
                else
                {
                    boardButtons[i - 1, j].BackColor = Color.Red;
                }
            }

            if (i < Math.Sqrt(boardButtons.Length) - 1)
            {
                if (boardButtons[i + 1, j].BackColor == Color.Red)
                {
                    boardButtons[i + 1, j].BackColor = SystemColors.Control;
                }
                else
                {
                    boardButtons[i + 1, j].BackColor = Color.Red;
                }
            }

            if (j > 0)
            {
                if (boardButtons[i, j - 1].BackColor == Color.Red)
                {
                    boardButtons[i, j - 1].BackColor = SystemColors.Control;
                }
                else
                {
                    boardButtons[i, j - 1].BackColor = Color.Red;
                }
            }

            if (j < Math.Sqrt(boardButtons.Length) - 1)
            {
                if (boardButtons[i, j + 1].BackColor == Color.Red)
                {
                    boardButtons[i, j + 1].BackColor = SystemColors.Control;
                }
                else
                {
                    boardButtons[i, j + 1].BackColor = Color.Red;
                }
            }
        }

        private void LightBoard()
        {
            Random r = new Random();
            int i = 0;
            while (i < boardButtons.Length * 5)
            {
                int x = r.Next(0, (byte)Math.Sqrt(boardButtons.Length));
                int y = r.Next(0, (byte)Math.Sqrt(boardButtons.Length));

                ChangeLightValues(x, y);

                if (!winClicks.Contains(new Point(x, y)))
                {
                    winClicks.Add(new Point(x, y));
                }
                else
                {
                    winClicks.Remove(new Point(x, y));
                }

                i++;
            }

            if (winClicks.Count == 0)
            {
                LightBoard();
            }
        }
    }
}
