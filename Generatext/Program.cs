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
            var text = File.ReadAllText("HarryPotterText.txt");
            var sentences = SentencesParser.ParseSentences(text);
            var frequency = FrequencyAnalysis.GetMostFrequentNextWords(sentences);
        }
    }
}
