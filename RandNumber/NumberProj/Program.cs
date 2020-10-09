using System;

namespace NumberProj
{
    internal class Program
    {
        public static void Main()
        {
            var random = new Random().Next(0,100);
            var numAttemp = 0;
            while (numAttemp < 7)
            {
                
                Console.Write($"Enter number(Left - {7-numAttemp} attempts)");
                var str1 = string.Format("sfslkd {0} skdjfds {1}", numAttemp, random);
                var str = Console.ReadLine();
                
                if (!int.TryParse(str, out var userNum))
                {
                    if (str != "q")
                    {
                        Console.WriteLine("You enter wrong data. Try again");
                        continue;
                    }

                    Console.WriteLine("You are exit ^)");
                    break;
                }
                
                if (random < userNum)
                {
                    Console.WriteLine("Spec number is less.");
                }
                else
                {
                    if (random != userNum)
                    {
                        Console.WriteLine("Spec number is greater");
                    }
                    else
                    {
                        Console.WriteLine("You are guessed");
                        break;
                    }
                }
                
                if (numAttemp == 6)
                {
                    Console.WriteLine("You didn't guess");
                }
                
                numAttemp++;
            }
        }
    }
}