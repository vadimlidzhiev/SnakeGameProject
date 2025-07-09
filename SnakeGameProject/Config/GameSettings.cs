namespace SnakeGameProject.Config
{
    public static class GameSettings
    {
        public const int Width = 40;
        public const int Height = 20;

        public const int FrameMs = 16;                           // 60 FPS (отрисовка)

        public const float MoveIntervalBase = 0.12f;             // базовый шаг (уровень 0)

        public static int ApplesForLevel(int lvl) => 3 + lvl;    // 3,4,5…
        public static float SpeedForLevel(int lvl)
            => MoveIntervalBase / (1 + lvl * 0.25f);             // всё быстрее
    }
}