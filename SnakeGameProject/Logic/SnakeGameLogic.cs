using SnakeGameProject.Renderer;
using SnakeGameProject.States;

namespace SnakeGameProject.Logic
{
    public class SnakeGameLogic : BaseGameLogic
    {
        private readonly ConsoleRenderer _r;
        private readonly SnakeGameplayState _play;
        private readonly ShowTextState _text;
        private int _level;

        public SnakeGameLogic(ConsoleRenderer r)
        {
            _r = r;
            _play = new SnakeGameplayState(r);
            _text = new ShowTextState(r, "SNAKE", 2f);

            ChangeToText("SNAKE");
        }

        public override void Update(float dt)
        {
            currentState.Update(dt);
            currentState.Draw(_r);

            if (!currentState.IsDone()) return;

            if (currentState == _text)          // после текста → игра
                StartLevel(_level);
            else if (_play.Win)                 // победили → следующий уровень
                ChangeToText($"LEVEL {_level += 1}");
            else if (_play.GameOver)            // проигрыш
                ChangeToText("GAME OVER");
        }

        public override void OnArrowUp() => 
            _play.SetDirection(SnakeGameplayState.SnakeDir.Up);
        public override void OnArrowDown() => 
            _play.SetDirection(SnakeGameplayState.SnakeDir.Down);
        public override void OnArrowLeft() => 
            _play.SetDirection(SnakeGameplayState.SnakeDir.Left);
        public override void OnArrowRight() => 
            _play.SetDirection(SnakeGameplayState.SnakeDir.Right);

        private void StartLevel(int lvl)
        {
            _play.Level = lvl;
            _play.Reset();
            currentState = _play;
        }

        private void ChangeToText(string msg)
        {
            _text.Text = msg;
            _text.Reset();
            currentState = _text;
        }

        public override ConsoleColor[] CreatePallete() =>
            [ConsoleColor.Black, ConsoleColor.Green, ConsoleColor.Red];
    }
}