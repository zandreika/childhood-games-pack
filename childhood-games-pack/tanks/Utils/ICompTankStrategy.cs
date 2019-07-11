using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace childhood_games_pack.tanks.Utils
{
    public interface ICompTankStrategy
    {
        DIRECTION GetNewDirection();
        bool IsNeedShoot();
    }
}
