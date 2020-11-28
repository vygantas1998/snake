namespace Snake.Objects.State
{
    public class Started : GameState
    {
        public override void Handle(Map map)
        {
            map.gameStarted = new NotStarted();
        }
    }
}
