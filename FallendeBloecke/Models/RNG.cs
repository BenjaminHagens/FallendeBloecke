using System;
using System.Collections.Generic;
using System.Text;

namespace FallendeBloecke.Models
{
    // [R]andom [N]umber [G]enerator
    public class RNG
    {
        /// <summary>
        /// Generates an number greater or equal as minValue and
        /// less or equal then maxValue
        /// </summary>
        /// <param name="minValue"></param>
        /// <param name="maxValue"></param>
        /// <returns>uint</returns>
        public static int GenerateRNG(int minValue, int maxValue)
        {
            Random rand = new Random();
            maxValue++;
            int res = rand.Next(minValue, maxValue);

            return res;
        }
    }
}
