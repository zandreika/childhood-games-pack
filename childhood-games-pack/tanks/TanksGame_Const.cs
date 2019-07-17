namespace childhood_games_pack.tanks
{
    public enum GAME_STATUS : int
    {
        LEVEL_SELECT    = 0,
        GAME_STARTED    = 1,
        GAME_PAUSED     = 2
    }

    public enum TANK_TYPE : int
    {
        NONE    = 0,
        LIGHT   = 1,
        MEDIUM  = 2,
        HEAVY   = 3
    }

    public enum WALL_TYPE : int
    {
        NONE    = 0,
        LIGHT   = 1,
        MEDIUM  = 2,
        HEAVY   = 3
    }

    public enum SPEED_LEVEL : int
    {
        NONE    = 0,
        LOW     = 1,
        MEDIUM  = 2,
        HIGHT   = 3
    }

    public enum DIRECTION : int
    {
        U = 1,
        D = 2,
        L = 3,
        R = 4
    }

    public enum BULLET_TYPE : int
    {
        USER = 1,
        COMP = 2
    }

    public partial class TanksGame
    {
        public const int gameFieldWidth  = 1200;
        public const int gameFieldHeight = 700;
        public const int gameFieldLocationX = 25;
        public const int gameFieldLocationY = 25;

        public const int wallWidth  = 25;
        public const int wallHeight = 25;

        public const int tankHeight = 50;
        public const int tankWidth  = 50;
        public const int bulletHeight = 8;
        public const int bulletWidth  = 8;
        public const int baseHeight = 50;
        public const int baseWidth  = 50;

        public const int bulletStep = 15;
        public const int bulletStepTimer = 150;

        public const int compTankStep = 20;
        public const int compTankStepTimer = 2000;

        public const int reloadTimerMs = 2000;
        public const int resultGameCheckerMs = 300;
    }
}
