namespace childhood_games_pack.tanks.Strategy
{
    public interface ICompTankStrategy
    {
        DIRECTION GetNewDirection();
        bool IsNeedShoot();
    }
}
