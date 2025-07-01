using SnakeGameProject.States;

namespace SnakeGameProject.Logic
{
    public class SnakeGameLogic : BaseGameLogic
    {
        private readonly SnakeGameplayState _gameplayState = new();

        public void GotoGameplay() => _gameplayState.Reset();

        public override void Update(float deltaTime) => _gameplayState.Update(deltaTime);

        public override void OnArrowUp() => 
            _gameplayState.SetDirection(SnakeGameplayState.SnakeDir.Up);
        public override void OnArrowDown() => 
            _gameplayState.SetDirection(SnakeGameplayState.SnakeDir.Down);
        public override void OnArrowLeft() => 
            _gameplayState.SetDirection(SnakeGameplayState.SnakeDir.Left);
        public override void OnArrowRight() => 
            _gameplayState.SetDirection(SnakeGameplayState.SnakeDir.Right);
    }
}