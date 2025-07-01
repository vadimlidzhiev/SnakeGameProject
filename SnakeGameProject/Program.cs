using SnakeGameProject.Config;
using SnakeGameProject.Input;
using SnakeGameProject.Logic;

namespace SnakeGameProject
{
    public static class Program
    {
        static void Main(string[] args)
        {
            var gameLogic = new SnakeGameLogic();
            var input = new ConsoleInput();
            gameLogic.InitializeInput(input);
            gameLogic.GotoGameplay();

            var lastFrameTime = DateTime.UtcNow;

            while (true)
            {
                input.Update();

                var frameStart = DateTime.UtcNow;
                var deltaTimeSec = (float)(frameStart - lastFrameTime).TotalSeconds;

                gameLogic.Update(deltaTimeSec);

                lastFrameTime = frameStart;
                Thread.Sleep(GameSettings.FrameMs);
            }
        }
    }
}
