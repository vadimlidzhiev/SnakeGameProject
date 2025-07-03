using SnakeGameProject.Config;
using SnakeGameProject.Renderer;

namespace SnakeGameProject.States
{
    public class SnakeGameplayState : BaseGameState
    {
        public struct Cell
        {
            public int X;
            public int Y;
            public Cell(int x, int y)
            {
                X = x;
                Y = y;
            }
        }

        public enum SnakeDir 
        { 
            Left, 
            Right, 
            Up, 
            Down 
        }

        private readonly List<Cell> _body = new();
        private SnakeDir _currentDir;

        private float _timeToMove;
        private readonly float _moveInterval = GameSettings.MoveInterval;

        private readonly ConsoleRenderer _renderer;
        private const char SnakeChar = '■';
        private const byte SnakeColorIdx = 1;   // 0 = черный, 1 = зеленый

        public SnakeGameplayState(ConsoleRenderer renderer)
        {
            _renderer = renderer;
            Reset();
        }

        public override void Reset()
        {
            _body.Clear();
            // Стартовая позиция в центре поля
            _body.Add(new Cell(GameSettings.Width / 2, GameSettings.Height / 2));
            _currentDir = SnakeDir.Right; // направление по умолчанию
            _timeToMove = 0f;
            Draw();
        }

        public override void Update(float deltaTime)
        {
            _timeToMove -= deltaTime;
            var moved = false;

            while (_timeToMove <= 0f)
            {
                _timeToMove += _moveInterval;

                var nextHead = ShiftTo(_body[0]);
                _body.Insert(0, nextHead);

                // Пока длина змейки 1 только голова
                if (_body.Count > 1)
                    _body.RemoveAt(_body.Count - 1);

                moved = true;
            }

            if (moved)
                Draw();
        }

        public void SetDirection(SnakeDir dir)
        {
            if ((_currentDir == SnakeDir.Left && dir == SnakeDir.Right) ||
                (_currentDir == SnakeDir.Right && dir == SnakeDir.Left) ||
                (_currentDir == SnakeDir.Up && dir == SnakeDir.Down) ||
                (_currentDir == SnakeDir.Down && dir == SnakeDir.Up))
                return;

            _currentDir = dir;
        }

        private Cell ShiftTo(Cell from) => _currentDir switch
        {
            SnakeDir.Left => new Cell(from.X - 1, from.Y),
            SnakeDir.Right => new Cell(from.X + 1, from.Y),
            SnakeDir.Up => new Cell(from.X, from.Y - 1),
            SnakeDir.Down => new Cell(from.X, from.Y + 1),
            _ => from
        };

        private void Draw()
        {
            _renderer.Clear();

            foreach (var cell in _body)
            {
                if (cell.X >= 0 && cell.X < GameSettings.Width &&
                    cell.Y >= 0 && cell.Y < GameSettings.Height)
                {
                    _renderer.SetPixel(cell.X, cell.Y, SnakeChar, SnakeColorIdx);
                }
            }

            _renderer.Render();
        }
    }
}