using ServerApp.Objects;
using Snake.Objects;
using Snake.Objects.Levels;
using Snake.Objects.PowerUps;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Windows.Forms;

namespace Snake
{
    public sealed class Map
    {
        public List<Obstacle> Obstacles { get; set; }
        public List<PowerUp> PowerUps { get; set; }
        public List<Score> Scores { get; set; }
        public List<SnakeBody> Snakes { get; set; }
        public int snakeId { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public Level Level { get; set; }
        public bool gameStarted = false;
        public Form1 form { get; set; }
        public bool isServer { get; set; }
        public ClientSocket client { get; set; }

        private static Map instance = null;
        public static Map GetInstance
        {
            get
            {
                if (instance == null)
                    instance = new Map();
                return instance;
            }
        }

        public Map()
        {
            Obstacles = new List<Obstacle>();
            PowerUps = new List<PowerUp>();
            Width = 16;
            Height = 16;
            Level = LevelFactory.CreateLevel();
            Snakes = new List<SnakeBody>();
            Scores = new List<Score>();
            gameStarted = false;
            ClearMap();
        }
        public void addSnake(int X, int Y, string headColor, string bodyColor, int speed)
        {
            Scores.Add(new Score(0, 0));
            SnakeBody body = new SnakeBody(X, Y, speed, Direction.Down, headColor, bodyColor);
            client.AddSnake(body);
        }
        public void drawSnakes(Graphics canvas)
        {
            foreach (SnakeBody snake in Snakes)
            {
                snake.Draw(canvas, this);
            }
        }
        public void drawPowerUps(Graphics canvas)
        {
            foreach (PowerUp powerUp in PowerUps)
            {
                powerUp.Draw(canvas, this);
            }
        }
        public void eatFood(int snake, PowerUp powerUp, Label lblScore)
        {
            powerUp.Eat(Snakes[snake]);
            addScore(snake, powerUp.Points, lblScore);
            if(snakeId == 0)
            {
                client.AddPowerUp();
            }
        }
        public void addScore(int snake, int points, Label lblScore)
        {
            Scores[snake].Points += points;
            lblScore.Text = Scores[snake].Points.ToString();
        }
        public void checkForFood(PictureBox pbCanvas, Label lblScore)
        {
            int snkId = 0;
            foreach (SnakeBody snake in Snakes)
            {
                BodyPart part = snake.BodyParts[0];
                foreach (PowerUp powerUp in PowerUps)
                {
                    //Detect collision with food piece
                    if (part.X == powerUp.X && part.Y == powerUp.Y)
                    {
                        Eat(snkId, powerUp, pbCanvas, lblScore);
                        break;
                    }
                }
                snkId++;
            }
        }
        private void Eat(int snake,PowerUp powerUp, PictureBox pbCanvas, Label lblScore)
        {
            PowerUps.Remove(powerUp);
            eatFood(snake, powerUp, lblScore);
        }
        public void MoveSnakes(PictureBox pbCanvas)
        {
            foreach(SnakeBody snake in Snakes)
            {
                snake.MoveSnake(pbCanvas, this);
            }
        }
        public void addFood(int x, int y, bool isBuff, PowerUpType powerUpType)
        {
            AbstractPowerUpFactory powerUpFactory = PowerUpFactoryProducer.getFactory(isBuff);
            PowerUp sizePowerUp = powerUpFactory.getPowerUp(x, y, powerUpType);
            PowerUps.Add(sizePowerUp);
        }
        public void startGame()
        {
            form.gameStart();
        }
        
        public void changeSnakeDirection(int snakeId, Direction direction)
        {
            if (Snakes[snakeId].Direction != direction)
            {
                Snakes[snakeId].Direction = direction;
            }
        }
        public static Direction getDirection(string direction)
        {
            switch (direction)
            {
                case "Up":
                    return Direction.Up;
                case "Down":
                    return Direction.Down;
                case "Right":
                    return Direction.Right;
                case "Left":
                    return Direction.Left;
                default:
                    return Direction.Up;
            }
        }
        public static string getStringDirection(Direction direction)
        {
            switch (direction)
            {
                case Direction.Up:
                    return "Up";
                case Direction.Down:
                    return "Down";
                case Direction.Right:
                    return "Right";
                case Direction.Left:
                    return "Left";
                default:
                    return "";
            }
        }
        public void StartGame(Label lblGameOver, PictureBox pbCanvas)
        {
            ClearMap();
            lblGameOver.Visible = false;
        }
        public void ClearMap()
        {
            Obstacles.Clear();
            PowerUps.Clear();
            Scores.Clear();
            Snakes.Clear();
        }
        public void SetFromData(MapData map)
        {
            Obstacles = map.Obstacles;
            PowerUps = map.PowerUps;
            Scores = map.Scores;
            Snakes = map.Snakes;
            snakeId = map.snakeId;
            Width = map.Width;
            Height = map.Height;
            Level = LevelFactory.CreateLevel(map.Level);
            gameStarted = map.gameStarted;
        }
    }
}
