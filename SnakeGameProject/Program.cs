using SnakeGameProject.Config;
using SnakeGameProject.Input;
using SnakeGameProject.Logic;
using SnakeGameProject.Renderer;

namespace SnakeGameProject
{
    public static class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.CursorVisible = false;
            Console.SetWindowSize(GameSettings.Width, GameSettings.Height);

            var renderer = new ConsoleRenderer(
                new[] { ConsoleColor.Black, ConsoleColor.Green })
            {
                bgColor = ConsoleColor.Black   // если в классе есть такое свойство
            };

            var gameLogic = new SnakeGameLogic(renderer);

            var input = new ConsoleInput();
            gameLogic.InitializeInput(input);
            gameLogic.GotoGameplay();

            var lastFrameTime = DateTime.UtcNow;

            while (true)
            {
                input.Update();

                var now = DateTime.UtcNow;
                var deltaSec = (float)(now - lastFrameTime).TotalSeconds;
                lastFrameTime = now;

                gameLogic.Update(deltaSec);

                Thread.Sleep(GameSettings.FrameMs);
            }
        }
    }
}