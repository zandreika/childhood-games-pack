using System;

namespace childhood_games_pack.tanks.Utils
{
    public class BaseKillStrategy : ICompTankStrategy
    {
        UserBase UBase { get; set; }
        CompTank Comp { get; set; }

        public BaseKillStrategy(CompTank comp, UserBase ubase)
        {
            UBase = ubase;
            Comp = comp;
        }

        public DIRECTION GetNewDirection()
        {
            if (Comp.Location.X >= UBase.Location.X && Comp.Location.X + TanksGame.tankWidth <= UBase.Location.X + TanksGame.tankWidth)
            {
                if (Comp.Location.Y > UBase.Location.Y)
                {
                    return DIRECTION.U;
                }
                else
                {
                    return DIRECTION.D;
                }
            }
            else
            {
                if (Comp.Location.X < UBase.Location.X)
                {
                    return DIRECTION.R;
                }
                else
                {
                    return DIRECTION.L;
                }
            }
        }

        public bool IsNeedShoot()
        {
            if (Comp.Location.X >= UBase.Location.X - 25 && Comp.Location.X + TanksGame.tankWidth <= UBase.Location.X + TanksGame.tankWidth + 25)
            {
                if (Comp.Location.Y < UBase.Location.Y && Comp.Direction == DIRECTION.D)
                {
                    return true;
                }
                else if (Comp.Location.Y > UBase.Location.Y && Comp.Direction == DIRECTION.U)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else if (Comp.Location.Y >= UBase.Location.Y - 25 && Comp.Location.Y + TanksGame.tankHeight <= UBase.Location.Y + TanksGame.tankHeight + 25)
            {
                if (Comp.Location.X < UBase.Location.X && Comp.Direction == DIRECTION.R)
                {
                    return true;
                }
                else if (Comp.Location.X > UBase.Location.X && Comp.Direction == DIRECTION.L)
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }
            else
            {
                return false;
            }
        }
    }
}
