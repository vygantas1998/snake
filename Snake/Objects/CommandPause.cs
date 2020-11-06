namespace Snake.Objects
{
    class CommandPause : Command

    {
        public CommandPause(Map receiver) : base(receiver){}

        public override void Execute()
        {
            receiver.isPause = true;
        }
        public override void UnExecute()
        {
            receiver.isPause = false;
        }
    }
}