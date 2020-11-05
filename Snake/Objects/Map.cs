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
        public bool isServer { get; set; }
        public bool isPause { get; set; }

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
            Width = 1000;
            Height = 969;
            Level = LevelFactory.CreateLevel();
            Snakes = new List<SnakeBody>();
            Scores = new List<Score>();
            gameStarted = false;
            isPause = false;
            ClearMap();
        }
        public SnakeBody addSnake(int X, int Y, string headColor, string bodyColor, int speed)
        {
            Scores.Add(new Score(0, 0));
            SnakeBody body = new SnakeBody(X, Y, speed, Direction.Down, headColor, bodyColor);
            return body;
        }
        public int eatFood(int snake, PowerUp powerUp)
        {
            Random rnd = new Random();
            int bonusPoints = 0;
            PowerUpContext power = new PowerUpContext(powerUp);
            if (rnd.Next(0, 100) > 95)
            {
                SpeedUpPowerUp rare = new SpeedUpPowerUp();
                rare.SetComponent(powerUp);
                power = new PowerUpContext(rare);
                if (rnd.Next(0, 100) > 95)
                {
                    ColorPowerUp color = new ColorPowerUp();
                    color.SetComponent(rare);
                    power = new PowerUpContext(color);
                    if (rnd.Next(0, 100) > 95)
                    {
                        LenghtPowerUp len = new LenghtPowerUp();
                        len.SetComponent(color);
                        power = new PowerUpContext(len);
                        bonusPoints += 50;
                    }
                    bonusPoints += 50;
                }
                bonusPoints += 50;
            }
            power.Eat(Snakes[snake]);
            return powerUp.Points + bonusPoints;
        }
        public void addScore(int snake, int points)
        {
            Scores[snake].Points += points;
        }
        public int checkForFood()
        {
            int snkId = 0;
            foreach (SnakeBody snake in Snakes)
            {
                if (!snake.isDead)
                {
                    BodyPart part = snake.BodyParts[0];
                    foreach (PowerUp powerUp in PowerUps)
                    {
                        //Detect collision with food piece
                        if (part.X - 16 < powerUp.X && part.X + 16 > powerUp.X && part.Y - 16 < powerUp.Y && part.Y + 16 > powerUp.Y)
                        {
                            return Eat(snkId, powerUp);
                        }
                    }
                }
                snkId++;
            }
            return 0;
        }
        private int Eat(int snake,PowerUp powerUp)
        {
            PowerUps.Remove(powerUp);
            return eatFood(snake, powerUp);
        }
        public void MoveSnakes()
        {
            if (!isPause)
            {
                foreach (SnakeBody snake in Snakes)
                {
                    snake.MoveSnake(this);
                }
            }
        }
        public void addFood(int x, int y, bool isBuff, PowerUpType powerUpType)
        {
            AbstractPowerUpFactory powerUpFactory = PowerUpFactoryProducer.getFactory(isBuff);
            PowerUp sizePowerUp = powerUpFactory.getPowerUp(x, y, powerUpType);
            PowerUps.Add(sizePowerUp);
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
