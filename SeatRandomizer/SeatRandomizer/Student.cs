using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace SeatRandomizer
{
    // Valid student names
    public enum StudentEnum { Håkan, Mattias, Mikael, Ingrid, Adam, Victoria, Tomas, Samira, Linnéa, Arvid, Joakim, Nick };


    class Student
    {
        public string name { get; }
        public int currentSeat { get; }
        public List<int> seatHistory;
        // Valid seats
        public static int[] validSeats = new int[] { 11, 12, 13, 14, 21, 22, 23, 24, 31, 32, 33, 34 };

        // Check that input is valid and add name and seathistory to their corresponding variables.
        public Student(string aName, string aSeatHistory)
        {
            seatHistory = new List<int>();

            if (Enum.TryParse(aName, true, out StudentEnum student))
            {
                name = aName;
            }
            else
            {
                Console.WriteLine($"Name \"{aName}\" is not valid. Press any key to continue.");
                Console.ReadKey();
                throw new ArgumentException();
            }
            string[] seatHistoryArr = aSeatHistory.Split(',');
            foreach (var seat in seatHistoryArr)
            {
                try
                {
                    int intSeat = int.Parse(seat);
                    if (validSeats.Contains(intSeat))
                    {
                        seatHistory.Add(intSeat);
                    }
                    else
                    {
                        throw new ArgumentException();
                    }
                }
                catch (Exception)
                {
                    Console.WriteLine($"Seat {seat} was an invalid seat. Press a key to continue.");
                    Console.ReadKey();
                    throw;
                }

            }
        }
    }
}
