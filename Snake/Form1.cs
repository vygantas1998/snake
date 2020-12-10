using Snake.Objects;
using Snake.Objects.Proxy;
using Snake.Objects.State;
using System;
using System.Drawing;
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
            InitializeComponent();
            map.ClearMap();
            lblGameOver.Visible = false;
            ControlsAfterConnect(false);
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox1.Items.Add("Easy");
            comboBox1.Items.Add("Medium");
            comboBox1.Items.Add("Hard");
            comboBox1.SelectedItem = "Easy";
            comboBox2.Items.Add("Black");
            comboBox2.Items.Add("Violet");
            comboBox2.Items.Add("Aqua");
            comboBox2.SelectedItem = "Black";
            map.Width = pbCanvas.Width;
            map.Height = pbCanvas.Height;
        }

        public void ControlsAfterConnect(bool show)
        {
            button1.Visible = show;
            comboBox1.Visible = show;
            label2.Visible = show;
        }

        private void pbCanvas_Paint(object sender, PaintEventArgs e)
        {
            if (map.gameState is Started)
            {
                Graphics canvas = e.Graphics;
                new DrawProxy().DrawSnakes(canvas, map);
                new DrawProxy().DrawPowerUps(canvas, map);
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
            client.GameStart(comboBox1.SelectedItem.ToString(), map.Width, map.Height);
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
            map.gameState.Handle(map);
            gameTimer.Interval = 1000 / 16;
            gameTimer.Tick += UpdateScreen;
            gameTimer.Start();
            ControlsAfterConnect(false);
            button2.Visible = false;
            textBox1.Visible = false;
            textBox2.Visible = false;
            pbCanvas.Width = map.Width;
            pbCanvas.Height = map.Height;
        }
        public Direction updateDirection(Direction Direction)
        {
            Direction newDirection = Direction;
            if (Input.KeyPressed(Keys.Right) && Direction != Direction.Left)
                newDirection = Direction.Right;
            else if (Input.KeyPressed(Keys.Left) && Direction != Direction.Right)
                newDirection = Direction.Left;
            else if (Input.KeyPressed(Keys.Up) && Direction != Direction.Down)
                newDirection = Direction.Up;
            else if (Input.KeyPressed(Keys.Down) && Direction != Direction.Up)
                newDirection = Direction.Down;
            return newDirection;
        }
        private void UpdateScreen(object sender, EventArgs e)
        {
            Direction direction = map.Snakes[map.snakeId].Direction;
            Direction newDirection = updateDirection(direction);
            if (direction != newDirection)
            {
                client.ChangeDirection(newDirection);
            }
            map.MoveSnakes();
            int points = map.checkForFood();
            if (points != -99999 && map.snakeId == 0)
            {
                client.AddPowerUp();
            }
            if(points != -99999)
            {
                map.addScore(map.snakeId, points);
            }
            lblScore.Text = map.Scores[map.snakeId].Points.ToString();
            if (map.CheckIfAllIsDead() && map.gameState is Started)
            {
                map.gameState.Handle(map);
                lblGameOver.Visible = true;
            }
            if (Input.KeyPressed(Keys.P))
            {
                client.PauseGame();
            }
            pbCanvas.Invalidate();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (client == null)
            {
                client = new ClientSocket(textBox1.Text, textBox2.Text, richTextBox1, this);
                client.AddSnake(map.addSnake(10, 10, comboBox2.SelectedItem.ToString(), "Green", 16));
                comboBox2.Visible = false;
                label3.Visible = false;
                ControlsAfterConnect(true);
            }
        }
    }
}
