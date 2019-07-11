using System;

namespace childhood_games_pack.tanks.Utils
{
    public class DummyStrategy : ICompTankStrategy
    {
        Random rnd = new Random(DateTime.Now.Millisecond);

        public DIRECTION GetNewDirection()
        {
            return (DIRECTION)(rnd.Next() % 4);
        }

        public bool IsNeedShoot()
        {
            int isNeedShot = rnd.Next() % 100;
            return isNeedShot < 25 ? true : false;
        }
    }
}
