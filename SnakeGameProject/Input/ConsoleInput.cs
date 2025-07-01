namespace SnakeGameProject.Input
{
    public class ConsoleInput
    {
        private readonly List<IArrowListener> _listeners = new();

        public void Subscribe(IArrowListener listener) => _listeners.Add(listener);

        public void Update()
        {
            while (Console.KeyAvailable)
            {
                var key = Console.ReadKey(true).Key;
                foreach (var l in _listeners)
                {
                    switch (key)
                    {
                        case ConsoleKey.UpArrow: l.OnArrowUp(); break;
                        case ConsoleKey.DownArrow: l.OnArrowDown(); break;
                        case ConsoleKey.LeftArrow: l.OnArrowLeft(); break;
                        case ConsoleKey.RightArrow: l.OnArrowRight(); break;

                        case ConsoleKey.W: l.OnArrowUp(); break;
                        case ConsoleKey.S: l.OnArrowDown(); break;
                        case ConsoleKey.A: l.OnArrowLeft(); break;
                        case ConsoleKey.D: l.OnArrowRight(); break;
                    }
                }
            }
        }
    }
}