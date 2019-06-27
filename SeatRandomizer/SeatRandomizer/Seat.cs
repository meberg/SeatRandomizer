using System;
using System.Collections.Generic;
using System.Text;

namespace SeatRandomizer
{
    class Seat
    {
        private static List<int> occupiedSeats = new List<int>();
        private static List<int> emptySeats = new List<int> { 11, 12, 13, 14, 21, 22, 23, 24, 31, 32, 33, 34 };
        static Random random = new Random();


        // Give a random seat to caller
        public static int GetRandomSeat()
        {
            int randomNum = random.Next(emptySeats.Count);
            int seat = emptySeats[randomNum];
            occupiedSeats.Add(seat);
            emptySeats.Remove(seat);
            return seat;
        }

        // If the seat provided in GetRandomSeat was invalid, return it to the emptySeats list
        public static void InvalidSeat(int seatNum)
        {
            occupiedSeats.Remove(seatNum);
            emptySeats.Add(seatNum);
        }

        // Force a seat to be given if GetRandomSeat has failed 15 times.
        public static void ForceSeat(int aSeat)
        {
            occupiedSeats.Add(aSeat);
            emptySeats.Remove(aSeat);
        }

        public static void ResetAllSeats()
        {
            List<int> seatList = new List<int> { 11, 12, 13, 14, 21, 22, 23, 24, 31, 32, 33, 34 };

            occupiedSeats.Clear();
            foreach (var newSeat in seatList)
            {
                emptySeats.Add(newSeat);
            }
        }
    }
}
