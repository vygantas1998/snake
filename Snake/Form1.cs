using Newtonsoft.Json.Linq;
using ServerApp.Objects;
using Snake.Objects;
using Snake.Objects.PowerUps;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text.Json;
using System.Threading;
using System.Windows.Forms;

namespace Snake
{
    public enum Direction
    {
        Up,
        Down,
        Left,
        Right
    }
    public partial class Form1 : Form
    {
        private Map map = Map.GetInstance;

        public bool ServerOn = false;
        ClientSocket client;

        public Form1()
        {
            map.form = this;
            InitializeComponent();
            //Start New game
            map.StartGame(lblGameOver, pbCanvas);
            button1.Visible = false;
        }
        

        private void pbCanvas_Paint(object sender, PaintEventArgs e)
        {
            if (map.gameStarted)
            {
                Graphics canvas = e.Graphics;
                //if (!map.Snakes[map.snakeId].isDead)
                //{
                    map.drawSnakes(canvas);
                    map.drawPowerUps(canvas);
                //}
                //else
                //{
                //    string gameOver = "Game over \nYour final score is: " + map.Scores[map.snakeId].Points + "\nPress Enter to try again";
                //    lblGameOver.Text = gameOver;
                //    lblGameOver.Visible = true;
                //}
            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            Input.ChangeState(e.KeyCode, true);
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            Input.ChangeState(e.KeyCode, false);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            client.GameStart();
            client.AddPowerUp();
        }

        public void gameStart()
        {
            if (InvokeRequired)
            {
                this.Invoke(new Action(() => gameStartTest()));
                return;
            }
            else
            {
                gameStartTest();
            }
            
        }
        private void gameStartTest()
        {
            map.gameStarted = true;
            gameTimer.Interval = 1000 / 16;
            gameTimer.Tick += UpdateScreen;
            gameTimer.Start();
            button1.Visible = false;
            button2.Visible = false;
            textBox1.Visible = false;
            textBox2.Visible = false;
        }
        private void UpdateScreen(object sender, EventArgs e)
        {

            map.Snakes[map.snakeId].updateDirection(client, map.snakeId, map);
            map.MoveSnakes(pbCanvas);
            map.checkForFood(pbCanvas, lblScore);
            pbCanvas.Invalidate();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (client == null)
            {
                client = new ClientSocket(textBox1.Text, textBox2.Text, richTextBox1, this);
                map.client = client;
                map.addSnake(10, 10, "Black", "Green", 16);
                button1.Visible = true;
            }
        }
    }
}
