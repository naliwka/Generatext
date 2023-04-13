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
            var text = File.ReadAllText("HarryPotterText.txt");
            var sentences = SentencesParser.ParseSentences(text);
            var frequency = FrequencyAnalysis.GetMostFrequentNextWords(sentences);

            Console.WriteLine("Enter the book (choose the number).");
            for (int i = 0; i < files.Length; i++)
            {
                Console.WriteLine($"{i+1}. {Path.GetFileName(files[i])}");
            }
            int userChoice = Convert.ToInt32(Console.ReadLine());
            // switch or smth to bring SentencesParcer.cs choisen path
            while (true)
            {
                Console.Write("Enter the first word (for example, he): ");
                var beginning = Console.ReadLine();
                Console.Write("Enter the count of words in sentence: ");
                int countWords = Convert.ToInt32(Console.ReadLine());
                if (string.IsNullOrEmpty(beginning)) return;
                var phrase = TextGenerator.ContinuePhrase(frequency, beginning.ToLower(), countWords);
                Console.WriteLine(phrase);
            }
        }

    }
}