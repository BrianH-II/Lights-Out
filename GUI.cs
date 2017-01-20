using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LightsOut
{
    public partial class GUI : Form
    {
        private Label label = new Label();
        private TextBox input = new TextBox();
        private Button startGame = new Button();
        private Button showSolution = new Button();

        private Board board;
        private Timer timer = new Timer();

        private Font times12 = new Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        private Font times14 = new Font("Times New Roman", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));

        public GUI()
        {
            this.ClientSize = new Size(500, 250);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;

            label.AutoSize = true;
            label.Font = times12;
            label.Text = "Please enter an integer length." + Environment.NewLine + "The game will be a square board with that length." + Environment.NewLine + "(Ex: Entering 5 will create a 5 x 5 game board)";
            this.Controls.Add(label);
            label.Location = new Point((ClientSize.Width - label.Width) / 2, 25);


            input.Font = times12;
            input.Width = 25;
            input.Location = new Point((ClientSize.Width - input.Width) / 2, 100);
            this.Controls.Add(input);

            startGame.Height = 100;
            startGame.Width = 150;
            startGame.Font = times14;
            startGame.Text = "Create game!";
            startGame.Location = new Point((ClientSize.Width - startGame.Width) / 2, ClientSize.Height - startGame.Height);
            startGame.Click += new EventHandler(startGame_Click);
            this.Controls.Add(startGame);

            showSolution.Height = 100;
            showSolution.Width = 150;
            showSolution.Font = times14;
            showSolution.Text = "Show solution";
            showSolution.Click += new EventHandler(showSolution_Click);
            this.Controls.Add(showSolution);
            showSolution.Visible = false;


            timer.Interval = 100;
            timer.Tick += new EventHandler(timer_Tick);
        }

        private void startGame_Click(Object sender, EventArgs e)
        {
            try
            {
                byte boardLength = byte.Parse(input.Text);
                if (boardLength <= 1 | boardLength > 15)
                {
                    throw new OverflowException();
                }

                startGame.Visible = false;

                //comment why these sizes are what they are
                //200 because of the button show solution
                this.ClientSize = new Size(Math.Max(275, (100 + 35 * boardLength)), Math.Max(375, (200 + 35 * boardLength)));
                showSolution.Location = new Point((ClientSize.Width - showSolution.Width) / 2, ClientSize.Height - showSolution.Height);
                showSolution.Visible = true;

                board = new Board(boardLength, new Point((this.ClientSize.Width - 35 * boardLength) / 2, Math.Max(50, (this.ClientSize.Height - 100 - 35 * boardLength) / 2)));


                foreach (Button b in board.boardButtons)
                {
                    this.Controls.Add(b);
                }

                startGame.Visible = false;
                input.Visible = false;

                label.Text = "Good Luck!";
                label.Location = new Point((ClientSize.Width - label.Width) / 2, 20);

                timer.Start();
            }
            catch (FormatException)
            {
                label.Text = "Please enter a valid integer";
            }
            catch (OverflowException)
            {
                label.Text = "Please enter an integer between 2 and 15 (inclusive)";
            }
        }

        private void showSolution_Click(Object sender, EventArgs e)
        {
            board.ShowSolution();
            label.Text = "Click the buttons with a star";
            label.Location = new Point((ClientSize.Width - label.Width) / 2, 20);
        }

        private void timer_Tick(Object sender, EventArgs e)
        {
            if (board.winClicks.Count == 0)
            {
                label.Text = "Nice job! You won!";
                label.Location = new Point((ClientSize.Width - label.Width) / 2, 20);

                timer.Stop();
            }
        }
    }
}
