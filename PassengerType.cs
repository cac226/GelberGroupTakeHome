using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GelberGroupTakeHome
{
    /// <summary>
    /// Passenger type A is willing to board any non-full train heading to its destination.
    /// Passenger type B will only board a train if it is no more than half-full.
    /// </summary>
    public enum PassengerType
    {
        A = 0,
        B = 1
    }
}
