using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Die
{
    class Program
    {
        /// <summary>
        /// the count of two sixes in the row
        /// </summary>
        private static int numberOfTwoSixes;

        /// <summary>
        /// triggeres everytime when triggeres Rolling.Throwed event
        /// </summary>
        /// <param name="value">he throwed number</param>
        public static void Rolled(int value)
        {
            Console.WriteLine("new roll - {0}\n", value);
        }

        /// <summary>
        /// triggeres everytime when triggeres Rolling.TwoSix event
        /// </summary>
        public static void TwoSixes()
        {
            numberOfTwoSixes++;
            Console.WriteLine("-------Two sixes in the row-------\n");
        }

        /// <summary>
        /// triggeres everytime when triggeres Rolling.Five event
        /// </summary>
        /// <param name="sum">the sum of last five rollings</param>
        public static void Five(int sum)
        {
            Console.WriteLine("---------The sum of last five rollings is {0}------\n",sum);
        }
        static void Main(string[] args)
        {
            Rolling roll = new Rolling(1, 50);
            //Rolled subscribes to rolll.Throwed
            roll.Throwed += Rolled;
            //Five subscribes to roll.Five
            roll.Five += Five;
            //TwoSixes subscribes to roll.TwoSix
            roll.TwoSix += TwoSixes;
            //starts the rolling
            roll.Roll();
            Console.WriteLine("---The amount of two sixes in the row is {0}---", numberOfTwoSixes);
        }
    }
}
