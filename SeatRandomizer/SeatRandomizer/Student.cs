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
        public string name { get; private set; }
        public int currentSeat { get; private set; }
        public List<int> seatHistory;
        // Valid seats
        public static int[] validSeats = new int[] { 11, 12, 13, 14, 21, 22, 23, 24, 31, 32, 33, 34 };

        public Student(string aName, string aSeatHistory)
        {
            seatHistory = new List<int>();
            SetStudentName(aName);
            SetStudentSeatHistory(aSeatHistory);
            SetCurrentSeat();
        }



        private void SetCurrentSeat()
        {
            currentSeat = seatHistory[0];
        }

        private void SetStudentSeatHistory(string aSeatHistory)
        {
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

        private void SetStudentName(string aName)
        {
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
        }

        // Look if aSeat was used last time for student. If not, set it as current seat.
        public bool TryNewStudentSeat(string aSeat)
        {
            // Keep seat history to max 5 seats.
            if (seatHistory.Count >= 5)
            {
                seatHistory.RemoveAt(4);
            }

            if (IsValidSeat(aSeat))
            {
                if (aSeat == currentSeat.ToString())
                {
                    return false;
                }
                else
                {
                    seatHistory.Insert(0, int.Parse(aSeat));
                    SetCurrentSeat();
                    return true;
                }
            }
            else
            {
                Console.WriteLine($"Seat {aSeat} was an invalid seat. Press a key to continue.");
                Console.ReadKey();
                throw new ArgumentException();
            }
        }

        // Sets the student seat even if it is the same as last time if it has tried to find
        // a new one for 15 times.
        public void SetStudentSeat(string aSeat)
        {
            seatHistory.Insert(0, int.Parse(aSeat));
            SetCurrentSeat();

        }

        // Checks if seat is a seat that exists (Should never happen)
        private bool IsValidSeat(string aSeat)
        {
            try
            {
                int intSeat = int.Parse(aSeat);
                if (validSeats.Contains(intSeat))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                Console.WriteLine($"Seat {aSeat} was an invalid seat. Press a key to continue.");
                Console.ReadKey();
                throw;
            }
        }
    }
}
