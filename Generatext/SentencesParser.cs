using System;
using System.Collections.Generic;
using System.Text;

namespace Generatext
{
    static class SentencesParser
    {
        public static bool IsWord(string word)
        {
            bool isLetter = true;
            foreach (var symbol in word)
            {
                if (char.IsLetter(symbol) || symbol == '\'') isLetter = isLetter && true;
                else isLetter = false;
            }
            return isLetter;
        }
        public static List<List<string>> ParseSentences(string text)
        {
            var sentencesList = new List<List<string>>();
            char[] separators = { '.', '!', '?', '(', ')', ':', ';' };
            var splitText = text.Split(separators, System.StringSplitOptions.RemoveEmptyEntries);
            foreach (var sentence in splitText)
            {
                var builder = new StringBuilder();
                foreach (char symbol in sentence)
                {
                    if (char.IsLetter(symbol) || symbol == '\'') builder.Append(symbol);
                    else builder.Append(" ");
                }
                var splitSentence = builder.ToString().Split(' ', (char)StringSplitOptions.RemoveEmptyEntries);
                var wordsInOneSentence = new List<string>();
                foreach (var word in splitSentence)
                {
                    if (!string.IsNullOrWhiteSpace(word)) wordsInOneSentence.Add(word.ToLower());
                }
                sentencesList.Add(wordsInOneSentence);
            }
            sentencesList.RemoveAll(x => x == null || x.Count == 0);
            return sentencesList;
        }
    }
}
