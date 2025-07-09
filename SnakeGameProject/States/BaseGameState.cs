namespace SnakeGameProject.States
{
    public abstract class BaseGameState
    {
        public abstract void Reset();
        public abstract void Update(float deltaTime);

        public abstract void Draw(Renderer.ConsoleRenderer r);
        public abstract bool IsDone();
    }
}