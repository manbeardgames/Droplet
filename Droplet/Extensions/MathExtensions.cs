using System;

namespace Droplet.Extensions
{
    public static class MathExtensions
    {
        //------------------------------------------------------------
        //  NearlyEqual
        //  Used to determine if two floats are nearly equal.  This is
        //  used becuase of the rounding errors that floats run into

        public static bool NearlyEqual(float a, float b)
        {
            return Math.Abs(a - b) < 0.001;
        }

        //------------------------------------------------------------
    }
}
