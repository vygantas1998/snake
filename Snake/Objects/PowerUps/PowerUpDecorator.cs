namespace Snake.Objects.PowerUps
{
    abstract class PowerUpDecorator : PowerUp
    {
        protected PowerUp component;

        public void SetComponent(PowerUp component)
        {
            this.component = component;
        }

        public override void Eat(SnakeBody snake)
        {
            if (component != null)
            {
                component.Eat(snake);
            }
        }
    }
}
