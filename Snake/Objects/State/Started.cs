namespace Snake.Objects.State
{
    public class Started : GameState
    {
        public override void Handle(Map map)
        {
            bool isAllDead = map.CheckIfAllIsDead();
            if (isAllDead)
            {
                map.gameState = new GameOver();
            }
            else
            {
                map.gameState = new Pause();
            }
        }
    }
}
