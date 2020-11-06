namespace Snake.Objects
{
    class Invoker
    {
        private Command _command;

        public void SetCommand(Command command)
        {
            this._command = command;
        }

        public void ExecuteCommand()
        {
            _command.Execute();
        }
        public void Undo()
        {
            _command.UnExecute();
        }
    }
}
