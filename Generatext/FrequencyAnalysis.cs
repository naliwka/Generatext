using System;
using System.Collections.Generic;
using System.Linq;

namespace Generatext
{
    static class FrequencyAnalysis
    {
        public static Dictionary<string, Dictionary<string, int>> FillBigrams(List<List<string>> text)
        {
            var bigrams = new Dictionary<string, Dictionary<string, int>>();
            foreach (var sentence in text)
            {
                for (int i = 0; i < sentence.Count - 1; i++)
                {
                    if (!bigrams.ContainsKey(sentence[i]))
                    {
                        bigrams.Add(sentence[i], new Dictionary<string, int>());
                    }
                    if (!bigrams[sentence[i]].ContainsKey(sentence[i + 1]))
                    {
                        bigrams[sentence[i]].Add(sentence[i + 1], 1);
                    }
                    else
                    {
                        bigrams[sentence[i]][sentence[i + 1]] += 1;
                    }
                }
            }
            return bigrams;
        }
        public static Dictionary<string, Dictionary<string, int>> FillTrigrams(List<List<string>> text)
        {
            var trigrams = new Dictionary<string, Dictionary<string, int>>();
            foreach (var sentence in text)
            {
                for (int i = 0; i < sentence.Count - 2; i++)
                {
                    if (!trigrams.ContainsKey(sentence[i] + " " + sentence[i + 1]))
                    {
                        trigrams.Add(sentence[i] + " " + sentence[i + 1], new Dictionary<string, int>());
                    }
                    if (!trigrams[sentence[i] + " " + sentence[i + 1]].ContainsKey(sentence[i + 2]))
                    {
                        trigrams[sentence[i] + " " + sentence[i + 1]].Add(sentence[i + 2], 1);
                    }
                    else
                    {
                        trigrams[sentence[i] + " " + sentence[i + 1]][sentence[i + 2]] += 1;
                    }
                }
            }
            return trigrams;
        }

        public static Dictionary<string, string> GetMostFrequentNextWords(List<List<string>> text)
        {
            var biAndTriGrams = new Dictionary<string, Dictionary<string, int>>();
            biAndTriGrams = FillBigrams(text).Concat(FillTrigrams(text)).ToDictionary(x => x.Key, x => x.Value);

            var result = new Dictionary<string, string>();

            foreach (var pair in biAndTriGrams)
            {
                var maxRepeatValue = 0;
                string mostFrequentContinue = null;
                foreach (var continuation in pair.Value)
                {
                    if (continuation.Value == maxRepeatValue)
                    {
                        if (string.CompareOrdinal(mostFrequentContinue, continuation.Key) > 0)
                        {
                            mostFrequentContinue = continuation.Key;
                        }
                    }
                    else if (continuation.Value > maxRepeatValue)
                    {
                        mostFrequentContinue = continuation.Key;
                        maxRepeatValue = continuation.Value;
                    }
                    if (!result.ContainsKey(pair.Key)) result.Add(pair.Key, mostFrequentContinue);
                    else result[pair.Key] = mostFrequentContinue;
                }
            }
            return result;
        }
    }
}
