using SnakeGameProject.Input;

namespace SnakeGameProject.Logic
{
    public abstract class BaseGameLogic : IArrowListener
    {
        public void InitializeInput(ConsoleInput input) =>
            input.Subscribe(this);

        public abstract void Update(float deltaTime);

        public abstract void OnArrowUp();
        public abstract void OnArrowDown();
        public abstract void OnArrowLeft();
        public abstract void OnArrowRight();
    }
}