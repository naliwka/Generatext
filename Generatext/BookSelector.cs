using System;
using System.IO;

namespace Generatext
{
    static class BookSelector
    {
        public static void PrintListOfBooks(string[] files)
        {
            Console.WriteLine("List of books:");
            for (int i = 0; i < files.Length; i++)
            {
                Console.WriteLine($"\t{i + 1}. {Path.GetFileName(files[i])}");
            }
            Console.WriteLine("\t0. {Your book}");
        }
        public static string BookChoice(string[] files)
        {
            string input;
            int userChoice;
            string text = "";
            int attempts = 0;
            bool isValid = false;
            while (!isValid)
            {
                Console.Write("\nChoose the book (enter the number): ");
                input = Console.ReadLine();
                if (int.TryParse(input, out userChoice) && userChoice <= files.Length)
                {
                    for (int i = 0; i < files.Length; i++)
                    {
                        if (i + 1 == userChoice)
                        {
                            text = File.ReadAllText(files[i]);
                        }
                        else if (userChoice == 0)
                        {
                            string userFilePath;
                            do
                            {
                                Console.WriteLine("\n" + @"Enter the path to your book (C:\\example\\file.txt):");
                                userFilePath = Console.ReadLine();
                                if (IsValidFilePath(userFilePath))
                                {
                                    text = File.ReadAllText(userFilePath);
                                }
                                else
                                {
                                    InvalidInput(attempts);
                                    attempts++;
                                }
                            }
                            while (!IsValidFilePath(userFilePath));
                        }
                    }
                    isValid = true;
                }
                else
                {
                    InvalidInput(attempts);
                    attempts++;
                }
            }
            return text;
        }
        public static void InvalidInput(int attempts)
        {
            if (attempts < 2)
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("Invalid input. Try again!");
                Console.ResetColor();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("Too many attempts.");
                Console.ResetColor();
                RetryOrExit();
            }
        }
        public static bool IsValidFilePath(string path)
        {
            return File.Exists(path);
        }
        public static void RetryOrExit()
        {
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("\nPress any key to try again or '0' to exit");
            Console.ResetColor();
            string choice = Console.ReadLine();
            if (choice == "0")
            {
                Environment.Exit(0);
            }
        }
        public static string GetUserBeginningWord()
        {
            var beginning = "";
            int attempts = 0;
            bool isValid = false;
            while (!isValid)
            {
                Console.Write("\nEnter the first word (for example, start): ");
                beginning = Console.ReadLine();
                if (SentencesParser.IsWord(beginning))
                {
                    isValid = true;
                }
                else
                {
                    InvalidInput(attempts);
                    attempts++;
                }
            }
            return beginning;
        }
        public static int GetUserWordsCount()
        {
            int countWords = 0;
            int attempts = 0;
            bool isValid = false;
            while (!isValid)
            {
                Console.Write("\nEnter the count of words in sentence: ");
                string inputCount = Console.ReadLine();
                if (int.TryParse(inputCount, out countWords))
                {
                    isValid = true;
                }
                else
                {
                    InvalidInput(attempts++);
                }
            }
            return countWords;
        }
    }
}
