using Snake.Objects.State;

namespace Snake.Objects
{
    class CommandPause : Command

    {
        public CommandPause(Map receiver) : base(receiver){}

        public override void Execute()
        {
            if (receiver.gameState is Started)
            {
                receiver.gameState.Handle(receiver);
            }
        }
        public override void UnExecute()
        {
            if (receiver.gameState is Pause)
            {
                receiver.gameState.Handle(receiver);
            }
        }
    }
}