using ServerApp.Objects;
using Snake.Objects;
using Snake.Objects.Iterator;
using Snake.Objects.Levels;
using Snake.Objects.Memento;
using Snake.Objects.PowerUps;
using Snake.Objects.State;
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
        public PowerUpCollection PowerUps { get; set; }
        public List<Score> Scores { get; set; }
        public List<SnakeBody> Snakes { get; set; }
        public List<ProspectMemory> SnakesSave { get; set; }
        public int snakeId { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public Level Level { get; set; }
        public GameState gameState { get; set; }
        public bool isServer { get; set; }

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
            PowerUps = new PowerUpCollection();
            Width = 1000;
            Height = 969;
            Level = LevelFactory.CreateLevel();
            Snakes = new List<SnakeBody>();
            Scores = new List<Score>();
            gameState = new NotStarted();
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
            PowerUpContext power = new PowerUpContext(powerUp);
            if (powerUp.Random > 70)
            {
                RarePowerUp rare = new RarePowerUp();
                rare.SetComponent(powerUp);
                power = new PowerUpContext(rare);
                if (powerUp.Random > 80)
                {
                    ColorPowerUp color = new ColorPowerUp();
                    color.SetComponent(rare);
                    power = new PowerUpContext(color);
                    if (powerUp.Random > 95)
                    {
                        LenghtPowerUp len = new LenghtPowerUp();
                        len.SetComponent(color);
                        power = new PowerUpContext(len);
                        if (powerUp.Random > 97)
                        {
                            power = new PowerUpContext(new PowerUpAdapter());
                        }
                    }
                }
            }
            power.Eat(Snakes[snake]);
            return powerUp.Points;
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
                    PowerUpIterator i = PowerUps.CreateIterator();
                    PowerUp powerUp = i.First();
                    while (powerUp != null)
                    {
                        //Detect collision with food piece
                        if (part.X - 16 < powerUp.X && part.X + 16 > powerUp.X && part.Y - 16 < powerUp.Y && part.Y + 16 > powerUp.Y)
                        {
                            return Eat(snkId, powerUp);
                        }
                        powerUp = i.Next();
                    }
                }
                snkId++;
            }
            return -99999;
        }
        private int Eat(int snake,PowerUp powerUp)
        {
            PowerUps.Remove(powerUp);
            return eatFood(snake, powerUp);
        }
        public void MoveSnakes()
        {
            if (gameState is Started)
            {
                foreach (SnakeBody snake in Snakes)
                {
                    snake.MoveSnake(this);
                }
            }
        }
        public void addFood(int x, int y, bool isBuff, PowerUpType powerUpType, int random)
        {
            AbstractPowerUpFactory powerUpFactory = PowerUpFactoryProducer.getFactory(isBuff);
            PowerUp sizePowerUp = powerUpFactory.getPowerUp(x, y, powerUpType, random);
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
            PowerUps = new PowerUpCollection();
            Scores.Clear();
            Snakes.Clear();
        }
        public bool CheckIfAllIsDead()
        {
            bool allDead = true;
            foreach(SnakeBody s in Snakes)
            {
                if (!s.isDead)
                {
                    allDead = false;
                    break;
                }
            }
            return allDead;
        }
        public void SaveSnakesState()
        {
            SnakesSave = new List<ProspectMemory>();
            foreach (SnakeBody s in Snakes)
            {
                ProspectMemory mem = new ProspectMemory();
                mem.Memento = s.SaveMemento();
                SnakesSave.Add(mem);
            }
        }
        public void RestoreSnakesState()
        {
            int i = 0;
            foreach (ProspectMemory s in SnakesSave)
            {
                Snakes[i].RestoreMemento(s.Memento);
                i++;
            }
            SnakesSave.Clear();
        }
        //public void SetFromData(MapData map)
        //{
        //    Obstacles = map.Obstacles;
        //    PowerUps = map.PowerUps;
        //    Scores = map.Scores;
        //    Snakes = map.Snakes;
        //    snakeId = map.snakeId;
        //    Width = map.Width;
        //    Height = map.Height;
        //    Level = LevelFactory.CreateLevel(map.Level);
        //    gameStarted = map.gameStarted;
        //}
    }
}
