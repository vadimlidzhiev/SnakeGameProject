using System.Collections.Concurrent;

namespace SnakeGameProject.Input
{
    public class ConsoleInput
    {
        private readonly List<IArrowListener> _listeners = [];

        public void Subscribe(IArrowListener listener) => _listeners.Add(listener);

        public ConsoleInput()
        {
            new Thread(ReadKeys) { IsBackground = true }.Start();
        }

        private readonly ConcurrentQueue<ConsoleKey> _keys = new();

        private void ReadKeys()
        {
            while (true)
                _keys.Enqueue(Console.ReadKey(true).Key);
        }

        public void Update()
        {
            while (_keys.TryDequeue(out var key))
                foreach (var l in _listeners)
                    switch (key)
                    {
                        case ConsoleKey.UpArrow or ConsoleKey.W: l.OnArrowUp(); break;
                        case ConsoleKey.DownArrow or ConsoleKey.S: l.OnArrowDown(); break;
                        case ConsoleKey.LeftArrow or ConsoleKey.A: l.OnArrowLeft(); break;
                        case ConsoleKey.RightArrow or ConsoleKey.D: l.OnArrowRight(); break;
                    }
        }
    }
}