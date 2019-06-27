using System;
using System.Collections.Generic;
using System.IO;

namespace SeatRandomizer
{
    class Program
    {
        static string currentDir = Directory.GetCurrentDirectory();
        static string filePath = $"{currentDir}../../../../../seatArrangements.txt";

        // Manage program flow
        static void Main(string[] args)
        {
            // 1 Start program
            // 2 Load previous seat arrangement
            // 2.5 Write previous seat arrangement to the corresponding persons history
            // 3 Display previous seat arrangement
            // 4 Randomize new seats as array, never putting someone on the same seat as last time.
            // 5 Write array to file
            // 3 Display new seat arrangement

            StartProgram();
            LoadStudentSeatHistory();
            DisplaySeatArrangement();
            RandomizeNewSeats();
            WriteToFile();
            DisplaySeatArrangement();

        }

        // Print welcome message DONE
        private static void StartProgram()
        {
            string programName = "The Randomizer";
            string author = "Mattias Berglund";
            string version = "1.0.0";

            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine($"Welcome to \"{programName}\"");
            Console.WriteLine($"Author: {author}{Environment.NewLine}Version: {version}");
            Console.WriteLine();
            Console.ResetColor();
        }

        // Load the previous seating arrangement from file into the Student class
        private static void LoadStudentSeatHistory()
        {
            string[] seatArrangements = File.ReadAllLines(filePath);
            foreach (var entry in seatArrangements)
            {
                string[] personSeatArr = entry.Split(',', 2);
                Student student = new Student(personSeatArr[0], personSeatArr[1]);
            }
        }

        // Show seating arrangement
        private static void DisplaySeatArrangement()
        {
            throw new NotImplementedException();
        }

        // Give everyone new seats, never seating someone on the same seat as last time.
        private static void RandomizeNewSeats()
        {
            throw new NotImplementedException();
        }

        // Write new seat arrangement to file in same folder.
        private static void WriteToFile()
        {
            throw new NotImplementedException();
        }
    }
}
