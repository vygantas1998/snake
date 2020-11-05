using Snake.Objects;
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
        }

        public void ControlsAfterConnect(bool show)
        {
            button1.Visible = show;
            comboBox1.Visible = show;
            label2.Visible = show;
        }

        private void pbCanvas_Paint(object sender, PaintEventArgs e)
        {
            if (map.gameStarted)
            {
                Graphics canvas = e.Graphics;
                drawSnakes(canvas);
                drawPowerUps(canvas);
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
            client.GameStart(comboBox1.SelectedItem.ToString());
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
            ControlsAfterConnect(false);
            button2.Visible = false;
            textBox1.Visible = false;
            textBox2.Visible = false;
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
            if (points != 0 && map.snakeId == 0)
            {
                map.addScore(map.snakeId, points);
                client.AddPowerUp();
                lblScore.Text = map.Scores[map.snakeId].Points.ToString();
            }
            if (!lblGameOver.Visible && map.Snakes[map.snakeId].isDead)
            {
                lblGameOver.Visible = true;
            }
            if (Input.KeyPressed(Keys.P))
            {
                map.isPause = !map.isPause;
            }
            pbCanvas.Invalidate();
        }

        public void drawSnakes(Graphics canvas)
        {
            foreach (SnakeBody snake in map.Snakes)
            {
                foreach (BodyPart part in snake.BodyParts)
                {
                    DrawBodyPart(part, canvas);
                }
            }
        }
        public void drawPowerUps(Graphics canvas)
        {
            foreach (PowerUp powerUp in map.PowerUps)
            {
                DrawPowerUp(powerUp, canvas);
            }
        }

        public void DrawBodyPart(BodyPart part, Graphics canvas)
        {
            canvas.FillEllipse(part.getColor(),
                          new Rectangle(part.X,
                                         part.Y,
                                         16, 16));
        }

        public void DrawPowerUp(PowerUp part, Graphics canvas)
        {
            canvas.FillEllipse(part.Color,
                          new Rectangle(part.X,
                                         part.Y,
                                         16, 16));
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (client == null)
            {
                client = new ClientSocket(textBox1.Text, textBox2.Text, richTextBox1, this);
                client.AddSnake(map.addSnake(10, 10, "Black", "Green", 16));
                ControlsAfterConnect(true);
            }
        }

        //private void button3_Click(object sender, EventArgs e)
        //{
        //    map.isPause = button3.Text == "Pause";
        //    button3.Text = button3.Text == "Pause" ? "UnPause" : "Pause";
        //}
    }
}
