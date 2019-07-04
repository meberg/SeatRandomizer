using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;

namespace SeatRandomizer
{
    class Program
    {
        static string currentDir = Directory.GetCurrentDirectory();
        static string filePath = $"{currentDir}/seatArrangements.txt";
        static List<Student> studentList = new List<Student>();
        static DateTime date = DateTime.Now;
        static string todaysSeat = "";

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
            if (!AlreadyPostedToday())
            {
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
                PostSlackMessage();
            }

        }

        // Fail safe against posting two times the same day.
        private static bool AlreadyPostedToday()
        {
            ////Test line
            //return false;

            string filePathLast = $"{currentDir}/lastDayPosted.txt";

            if (!File.Exists(filePathLast))
            {
                var file = File.Create(filePathLast);
                file.Close();
            }

            string lastTimeRun = File.ReadAllText(filePathLast);
            string tomorrow = date.AddDays(1).Date.ToString();

            if (date.DayOfWeek.ToString() == "Friday" || date.DayOfWeek.ToString() == "Saturday")
            {
                Console.WriteLine("Tomorrow is not a school day.");
                return true;
            }
            else if (tomorrow == lastTimeRun)
            {
                Console.WriteLine("You already randomized seats for tomorrow.");
                return true;
            }
            else
            {
                File.WriteAllText(filePathLast, tomorrow);
                return false;
            }

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
            string tomorrow = date.AddDays(1).DayOfWeek.ToString() + " " + date.AddDays(1).Day.ToString() + "/" + date.AddDays(1).Month.ToString();
            todaysSeat = $"\n\nSeats for {tomorrow} [Row:Seat]:\n\n";

            List<Student> sortedStudentList = studentList.OrderBy(s => s.currentSeat).ToList();
            List<string> seatList = new List<string> { "1:1", "1:2", "1:3", "1:4", "2:1", "2:2", "2:3", "2:4", "3:1", "3:2", "3:3", "3:4" };
            string currentString;
            int numOfSpaces;

            // Print seat arrangements
            int index = 0;
            for (int i = 0; i < 3; i++)
            {
                for (int i2 = 0; i2 < 4; i2++)
                {
                    currentString = $"{seatList[index]} {sortedStudentList[index].name}";
                    switch (sortedStudentList[index].name)
                    {
                        case "Linnéa":
                        case "Mikael":
                        case "Samira":
                        case "Tomas":
                        case "Mattias":
                            numOfSpaces = 23 - currentString.Length;
                            break;
                        case "Nick":
                        case "Arvid":
                        case "Ingrid":
                            numOfSpaces = 24 - currentString.Length;
                            break;
                        default:
                            numOfSpaces = 22 - currentString.Length;
                            break;
                    }

                    // Test code for console
                    //if (sortedStudentList[index].name == "Victoria")
                    //    {
                    //        currentString = $"{seatList[index]} {sortedStudentList[index].name}";
                    //        string spaces = new string(' ', 23 - currentString.Length);
                    //        todaysSeat = todaysSeat + currentString + spaces;
                    //        Console.Write($"{sortedStudentList[index].name}\t");
                    //    }
                    //    else
                    //    {
                    //        currentString = $"{seatList[index]} {sortedStudentList[index].name}";
                    //        string spaces = new string(' ', 22 - currentString.Length);
                    //        todaysSeat = todaysSeat + currentString + spaces;
                    //        //Console.Write($"{sortedStudentList[index].name}\t\t");
                    //    }

                    currentString = $"{seatList[index]} {sortedStudentList[index].name}";
                    string spaces = new string(' ', numOfSpaces);
                    todaysSeat = todaysSeat + currentString + spaces;

                    index++;
                }
                todaysSeat = todaysSeat + "\n\n";

                //Console.WriteLine();
                //Console.WriteLine();
            }

            //// Test line
            //Console.WriteLine(todaysSeat);

            //// Tip: Repeat a string:
            //var stringRepeat = string.Concat(Enumerable.Repeat("ABC", 100));
            //Console.WriteLine(stringRepeat);
            //// Tip: Repeat a char:
            //string testString = new string('A', 100);
            //Console.WriteLine(testString);

            //Print student name and student seat history to console
            //Console.WriteLine();
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

        private static void PostSlackMessage()
        {
            string urlWithAccessToken = "https://hooks.slack.com/services/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";
            var client = new SlackClient(urlWithAccessToken);

            client.PostMessage(todaysSeat);
        }
    }
}
