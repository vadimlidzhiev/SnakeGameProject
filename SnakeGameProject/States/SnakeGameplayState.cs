using SnakeGameProject.Config;
using SnakeGameProject.Renderer;

namespace SnakeGameProject.States
{
    public class SnakeGameplayState(ConsoleRenderer r) : BaseGameState
    {
        public struct Cell(int x, int y)
        {
            public int X = x, Y = y;
        }
        public enum SnakeDir
        {
            Left,
            Right,
            Up,
            Down
        }

        private readonly List<Cell> _body = [];
        private SnakeDir _dir;
        private float _moveInterval;
        private float _timeToMove;

        // яблоко
        private readonly Random _rng = new();
        private Cell _apple;
        private const char AppleCh = '0';
        private const byte AppleClr = 2;

        // состояние
        public bool GameOver { get; private set; }
        public bool Win { get; private set; }
        public int Level { get; set; }

        // отрисовка
        private readonly ConsoleRenderer _r = r;
        private const char SnakeCh = '■';
        private const byte SnakeClr = 1;

        public override void Reset()
        {
            _body.Clear();
            _body.Add(new Cell(GameSettings.Width / 2, GameSettings.Height / 2));

            _dir = SnakeDir.Right;
            _moveInterval = GameSettings.SpeedForLevel(Level);
            _timeToMove = 0;

            GameOver = Win = false;
            SpawnApple();
            Draw(_r);
        }

        public override void Update(float dt)
        {
            if (GameOver || Win) return;

            _timeToMove -= dt;
            if (_timeToMove <= 0f)     
            {
                do _timeToMove += _moveInterval;
                while (_timeToMove <= 0f);
                Step();
            }
        }

        private void Step()
        {
            var head = Next(_body[0]);

            if (head.X <= 0 || head.X >= GameSettings.Width - 1 ||
                head.Y <= 0 || head.Y >= GameSettings.Height - 1)
            {
                GameOver = true;
                return;
            }

            bool ate = head.X == _apple.X && head.Y == _apple.Y;

            _body.Insert(0, head);
            if (!ate) _body.RemoveAt(_body.Count - 1);
            else
            {
                if (_body.Count - 1 >= GameSettings.ApplesForLevel(Level))
                    Win = true;
                else
                    SpawnApple();
            }
        }

        private Cell Next(Cell c) => _dir switch
        {
            SnakeDir.Left => new(c.X - 1, c.Y),
            SnakeDir.Right => new(c.X + 1, c.Y),
            SnakeDir.Up => new(c.X, c.Y - 1),
            SnakeDir.Down => new(c.X, c.Y + 1),
            _ => c
        };

        private void SpawnApple()
        {
            Cell p;
            do
                p = new(_rng.Next(1, GameSettings.Width - 1),
                        _rng.Next(1, GameSettings.Height - 1));
            while (_body.Any(b => b.X == p.X && b.Y == p.Y));
            _apple = p;
        }

        public void SetDirection(SnakeDir d)
        {
            bool opposite = (_dir, d) switch
            {
                (SnakeDir.Left, SnakeDir.Right) or
                (SnakeDir.Right, SnakeDir.Left) or
                (SnakeDir.Up, SnakeDir.Down) or
                (SnakeDir.Down, SnakeDir.Up) => true,
                _ => false
            };
            if (!opposite) _dir = d;
        }

        public override void Draw(ConsoleRenderer r)
        {
            r.Clear();
            const char WallCh = '■';
            const byte WallClr = 3;      // индекс белого цвета

            // верх / низ
            for (int x = 0; x < GameSettings.Width; x++)
            {
                r.SetPixel(x, 0, WallCh, WallClr);
                r.SetPixel(x, GameSettings.Height - 1, WallCh, WallClr);
            }
            // левый / правый край
            for (int y = 0; y < GameSettings.Height; y++)
            {
                r.SetPixel(0, y, WallCh, WallClr);
                r.SetPixel(GameSettings.Width - 1, y, WallCh, WallClr);
            }

            // яблоко
            r.SetPixel(_apple.X, _apple.Y, AppleCh, AppleClr);

            // змейка
            foreach (var c in _body)
                r.SetPixel(c.X, c.Y, SnakeCh, SnakeClr);

            // HUD → заголовок окна
            Console.Title = $"Lvl:{Level} | 🍎 {_body.Count - 1}/{GameSettings.ApplesForLevel(Level)}";

            r.Render();
        }

        public override bool IsDone() => GameOver || Win;
    }
}