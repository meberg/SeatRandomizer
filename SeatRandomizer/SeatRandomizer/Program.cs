using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;

namespace SeatRandomizer
{
    class Program
    {
        static string currentDir = Directory.GetCurrentDirectory();
        static string filePath = $"{currentDir}../../../../../seatArrangements.txt";
        static List<Student> studentList = new List<Student>();

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

            if (!File.Exists(filePath))
            {
                CreateFile();
            }

            LoadStudentSeatHistory();
            RandomizeNewSeats();
            WriteToFile();
            DisplaySeatArrangement();
            ResetVariables();
        }
        
        private static void CreateFile()
        {
            string[] studentArray = 
                { "Håkan,11", "Mattias,12", "Mikael,13", "Ingrid,14", "Adam,21", "Victoria,22",
                "Tomas,23", "Samira,24", "Linnéa,31", "Arvid,32", "Joakim,33", "Nick,34" };

            File.WriteAllLines(filePath, studentArray);
        }

    private static void ResetVariables()
        {
            Seat.ResetAllSeats();
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

        // Load the previous seating arrangement from file into the Student class and add student to studentList.
        private static void LoadStudentSeatHistory()
        {
            string[] seatArrangements = File.ReadAllLines(filePath);
            foreach (var entry in seatArrangements)
            {
                string[] personSeatArr = entry.Split(',', 2);
                Student student = new Student(personSeatArr[0], personSeatArr[1]);
                studentList.Add(student);
            }
        }

        // Show seating arrangement
        private static void DisplaySeatArrangement()
        {
            List<Student> sortedStudentList = studentList.OrderBy(s => s.currentSeat).ToList();

            foreach (var student in sortedStudentList)
            {
                student.name 
            }

            // Print seat arrangements
            
            for (int i = 0; i < 3; i++)
            {
                for (int i = 0; i < 4; i++)
                {

                }
            }

            // Print student name and student seat history to console
            //foreach (var student in studentList)
            //{
            //    Console.WriteLine($"Student: {student.name}");

            //    string seatHistory = "";
            //    foreach (var seat in student.seatHistory)
            //    {
            //        seatHistory = seatHistory + $"{seat} ";
            //    }
            //    Console.WriteLine($"Seat history: {seatHistory}");
            //    Console.WriteLine();
            //}
        }

        // Give everyone new seats, trying to never seat someone on the same seat as last time.
        private static void RandomizeNewSeats()
        {

            foreach (var student in studentList)
            {
                bool done = false;

                int randomSeat;
                int numIterations = 0;

                do
                {
                    randomSeat = Seat.GetRandomSeat();
                    if (!student.TryNewStudentSeat(randomSeat.ToString()))
                    {
                        Seat.InvalidSeat(randomSeat);

                    }
                    else
                    {
                        done = true;
                    }

                    if (numIterations > 15 && !done)
                    {
                        student.SetStudentSeat(randomSeat.ToString());
                        done = true;
                    }
                    numIterations++;
                } while (!done);

            }
        }

        // Write new seat arrangement to file seatarrangements.txt
        private static void WriteToFile()
        {
            List<string> studentSeatData = new List<string>();

            foreach (var student in studentList)
            {
                string studentData = student.name;
                foreach (var seat in student.seatHistory)
                {
                    studentData = studentData + "," + seat;
                }
                studentSeatData.Add(studentData);
            }

            string[] outData = studentSeatData.ToArray();

            File.WriteAllLines(filePath, outData);
        }
    }
}
