using SnakeGameProject.Config;
using SnakeGameProject.Input;
using SnakeGameProject.Logic;
using SnakeGameProject.Renderer;

namespace SnakeGameProject
{
    public static class Program
    {
        static void Main()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.CursorVisible = false;

            var renderer = new ConsoleRenderer(
                [ConsoleColor.Black,
                ConsoleColor.Green,
                ConsoleColor.Red,
                ConsoleColor.White])
            {
                BgColor = ConsoleColor.Black
            };
            var gameLogic = new SnakeGameLogic(renderer);
            var input = new ConsoleInput();
            gameLogic.InitializeInput(input);

            var target = TimeSpan.FromMilliseconds(GameSettings.FrameMs);
            var last = DateTime.UtcNow;

            while (true)
            {
                input.Update();

                var now = DateTime.UtcNow;
                var delta = (float)(now - last).TotalSeconds;
                last = now;
                gameLogic.Update(delta);

                var spent = DateTime.UtcNow - now;       
                var left = target - spent;
                if (left > TimeSpan.Zero)   
                    Thread.Sleep(left);                 
            }
        }
    }
}