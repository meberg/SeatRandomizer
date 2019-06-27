using System;
using System.Collections.Generic;
using System.Text;

namespace SeatRandomizer
{
    class Seat
    {
        public int[] seatNumbers = 
            { 11, 12, 13, 14, 21, 22, 23, 24, 31, 32, 33, 34 };

        private List<int> occupiedSeats;
        private List<int> emptySeats;

        public Seat()
        {
            occupiedSeats = new List<int>();
            emptySeats = new List<int>();
        }

        // Choose random seat method

    }
}
