using childhood_games_pack.tanks.Unit;

namespace childhood_games_pack.tanks.Strategy
{
    public class UserKillStrategy : ICompTankStrategy
    {
        UserTank User { get; set; }
        CompTank Comp { get; set; }

        public UserKillStrategy(CompTank comp, UserTank user)
        {
            User = user;
            Comp = comp;
        }

        public DIRECTION GetNewDirection()
        {
            if (Comp.Location.X >= User.Location.X && Comp.Location.X + TanksGame.tankWidth <= User.Location.X + TanksGame.tankWidth)
            {
                if (Comp.Location.Y > User.Location.Y)
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
                if (Comp.Location.X < User.Location.X)
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
            if (Comp.Location.X >= User.Location.X - 25 && Comp.Location.X + TanksGame.tankWidth <= User.Location.X + TanksGame.tankWidth + 25)
            {
                if (Comp.Location.Y < User.Location.Y && Comp.Direction == DIRECTION.D)
                {
                    return true;
                }
                else if (Comp.Location.Y > User.Location.Y && Comp.Direction == DIRECTION.U)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else if (Comp.Location.Y >= User.Location.Y - 25 && Comp.Location.Y + TanksGame.tankHeight <= User.Location.Y + TanksGame.tankHeight + 25)
            {
                if (Comp.Location.X < User.Location.X && Comp.Direction == DIRECTION.R)
                {
                    return true;
                }
                else if (Comp.Location.X > User.Location.X && Comp.Direction == DIRECTION.L)
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
