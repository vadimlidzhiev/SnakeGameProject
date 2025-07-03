using SnakeGameProject.Config;

namespace SnakeGameProject.States
{
    public class SnakeGameplayState : BaseGameState
    {
        public struct Cell
        {
            public int X;
            public int Y;
            public Cell(int x, int y)
            { X = x; Y = y; }
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

        private float _timeToMove;                       // секунд до следующего шага
        private readonly float _moveInterval = GameSettings.MoveInterval;

        public override void Reset()
        {
            _body.Clear();
            _currentDir = SnakeDir.Right;                // направление по умолчанию
            _body.Add(new Cell(0, 0));                   // голова в начале координат
            _timeToMove = 0f;
        }

        public override void Update(float deltaTime)
        {
            _timeToMove -= deltaTime;

            while (_timeToMove <= 0f)
            {
                _timeToMove += _moveInterval;

                var head = _body[0];
                var next = ShiftTo(head);

                _body.RemoveAt(_body.Count - 1);        // срез хвоста
                _body.Insert(0, next);                  // новая голова в начало списка
            }

            // вывод координат головы
            var cur = _body[0];
            Console.SetCursorPosition(0, 0);
            Console.Write($"Head: X={cur.X}, Y={cur.Y}   ");
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

        private Cell ShiftTo(Cell from)
        {
            return _currentDir switch
            {
                SnakeDir.Left => new Cell(from.X - 1, from.Y),
                SnakeDir.Right => new Cell(from.X + 1, from.Y),
                SnakeDir.Up => new Cell(from.X, from.Y + 1),
                SnakeDir.Down => new Cell(from.X, from.Y - 1),
                _ => from
            };
        }
    }
}