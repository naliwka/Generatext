using System;
using System.IO;

namespace Generatext
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string folderPath = Path.Combine(Directory.GetCurrentDirectory(), "..", "..", "Books");
            string[] files = Directory.GetFiles(folderPath);
            BookSelector.PrintListOfBooks(files);
            var sentences = SentencesParser.ParseSentences(BookSelector.BookChoice(files));
            var frequency = FrequencyAnalysis.GetMostFrequentNextWords(sentences);

            while (true)
            {
                string beginning = BookSelector.GetUserBeginningWord();
                int countWords = BookSelector.GetUserWordsCount();
                if (string.IsNullOrEmpty(beginning)) return;
                var phrase = TextGenerator.ContinuePhrase(frequency, beginning.ToLower(), countWords);
                Console.WriteLine($"\nYour sentence: \n{phrase}.");
                BookSelector.RetryOrExit();
            }
        }
    }
}