using Snake;
using Snake.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerApp.Objects
{
    public class MapData
    {
        public List<Obstacle> Obstacles { get; set; }
        public List<PowerUp> PowerUps { get; set; }
        public List<Score> Scores { get; set; }
        public List<SnakeBody> Snakes { get; set; }
        public int snakeId { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public string Level { get; set; }
        public bool gameStarted { get; set; }

        public MapData() { }

        public MapData(Map map)
        {
            Obstacles = map.Obstacles;
            PowerUps = map.PowerUps;
            Scores = map.Scores;
            Snakes = map.Snakes;
            snakeId = map.snakeId;
            Width = map.Width;
            Height = map.Height;
            Level = map.Level.levelType;
            gameStarted = map.gameStarted;
        }
    }
}
