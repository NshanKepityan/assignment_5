using System;
using System.Threading;

namespace Die
{
    class Rolling
    {
        /// <summary>
        /// the number that have been roled
        /// </summary>
        private int rolledNumber;

        /// <summary>
        /// the frequancy of rollings
        /// </summary>
        private readonly int frequancyOfRollings;

        /// <summary>
        /// the amount of rollings that should be
        /// </summary>
        private readonly int rollAmount;

        /// <summary>
        /// is true if the previous rolled number was six and false otherwise
        /// </summary>
        private bool six;

        /// <summary>
        /// the sum of las five roled numbers
        /// </summary>
        private int sumOfFive;

        /// <summary>
        /// random number generator
        /// </summary>
        private readonly Random random;

        /// <summary>
        /// the delegte of functions wich recive as argument an integer and reurn void
        /// </summary>
        /// <param name="value">the integer argument that recives function</param>
        public delegate void Rolled(int value);

        /// <summary>
        /// this event is triggered everytime when a new die is throwed
        /// </summary>
        public event Rolled Throwed;
        /// <summary>
        /// this event is triggered everytime when the sum of last five rollings is equal or greather than 20
        /// </summary>
        public event Rolled Five;

        /// <summary>
        /// the delegte of functions wich haven't arguments and reurn void
        /// </summary>
        public delegate void Sixes();
        /// <summary>
        /// this event is triggered everytime when two last rolled numbers where 6
        /// </summary>
        public event Sixes TwoSix;

        /// <summary>
        /// generates new Rolling with new frequancy and amount of rollings
        /// </summary>
        /// <param name="frequancy">the frequancy of rollings</param>
        /// <param name="amount">the amount of rollings</param>
        public Rolling(int frequancy, int amount)
        {
            this.rolledNumber = 0;
            this.frequancyOfRollings = frequancy;
            this.sumOfFive = 0;
            this.six = false;
            this.random = new Random(1);
            //the amount of rollings can't be negative
            if (amount > 0)
                this.rollAmount = amount;
            else
            {
                Console.WriteLine("The amount of rollings must be a native number");
                return;
            }
        }

        /// <summary>
        /// Teh function with rolles a die
        /// </summary>
        public void Roll()
        {
            //all rolled numbers are stored in this array
            int[] rollings = new int[this.rollAmount];
            for (int i = 0; i < this.rollAmount; i++)
            {
                //everytime after rolling waits so seconds how much was given as frequancy
                Thread.Sleep(this.frequancyOfRollings * 1000);
                this.rolledNumber = this.random.Next(1, 7);
                this.Throwed(this.rolledNumber);
                rollings[i] = this.rolledNumber;
                this.sumOfFive += this.rolledNumber;
                if (i >= 4)
                {
                    if (this.sumOfFive >= 20)
                        this.Five(this.sumOfFive);
                    this.sumOfFive -= rollings[i - 4]; //starting from fifth throwing is triggered event if the sum is >= 20 and from the sum of five is removed the first one
                }

                if (this.rolledNumber != 6)
                    this.six = false; 
                else
                {
                    if (this.six == false)
                    {
                        this.six = true;
                    }
                    else
                    {
                        this.TwoSix(); //if the value of this.six is true and the rolled number is 6 it means that there is two six in the row and is triggered TwoSix event
                    }
                }
            }
        }
    }
}
