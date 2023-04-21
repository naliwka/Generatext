using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Generatext
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string folderPath = Path.Combine(Directory.GetCurrentDirectory(), "..", "..", "Books");
            string[] files = Directory.GetFiles(folderPath);
            PrintListOfBooks(files);
            var sentences = SentencesParser.ParseSentences(BookChoice(files));
            var frequency = FrequencyAnalysis.GetMostFrequentNextWords(sentences);

            while (true)
            {
                string beginning = GetUserBeginningWord();
                int countWords = GetUserWordsCount();
                if (string.IsNullOrEmpty(beginning)) return;
                var phrase = TextGenerator.ContinuePhrase(frequency, beginning.ToLower(), countWords);
                Console.WriteLine($"Your sentence: \n{phrase}.");
                RetryOrExit();
            }
        }

        public static void PrintListOfBooks(string[] files)
        {
            Console.WriteLine("List of books:");
            for (int i = 0; i < files.Length; i++)
            {
                Console.WriteLine($"{i + 1}. {Path.GetFileName(files[i])}");
            }
            Console.WriteLine("0. {Your book}");
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
                Console.WriteLine("\nChoose the book (enter the number):");
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
                                Console.WriteLine(@"Enter the path to your book (C:\\example\\file.txt):");
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
                Console.WriteLine("Invalid input. Try again!");
            }
            else
            {
                Console.WriteLine("Too many attempts.");
                RetryOrExit();
            }
        }
        public static bool IsValidFilePath(string path)
        {
            return File.Exists(path);
        }
        public static void RetryOrExit()
        {
            Console.WriteLine("Press any key to try again or '0' to exit");
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
                Console.WriteLine("Enter the first word (for example, man): ");
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
                Console.WriteLine("Enter the count of words in sentence: ");
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