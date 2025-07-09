using SnakeGameProject.Renderer;

namespace SnakeGameProject.States
{
    public class ShowTextState(ConsoleRenderer r, string text, float seconds = 2f) : BaseGameState
    {
        private readonly ConsoleRenderer _r = r;
        private readonly float _seconds = seconds;
        private float _timer;
        public string Text
        {
            get; set;
        } = text;

        public override void Reset() => 
            _timer = 0;
        public override void Update(float dt) => 
            _timer += dt;

        public override void Draw(ConsoleRenderer r)
        {
            r.Clear();
            int cx = (r.Width - Text.Length) / 2;
            int cy = r.Height / 2;
            r.DrawString(Text, cx, cy, ConsoleColor.White);
            r.Render();
        }

        public override bool IsDone() => _timer >= _seconds;
    }
}