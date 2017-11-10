using System;
using System.Collections.Generic;
using Microsoft.Azure.CognitiveServices.Language.TextAnalytics;
using Microsoft.Azure.CognitiveServices.Language.TextAnalytics.Models;

namespace TextAnalysis
{
    class Program
    {

        private ITextAnalyticsAPI client;

        public static void Main(string[] args)
        {
            string detectString;
            string detectSentiment;
            string language;

            Program program = new Program();

            program.client​ = new TextAnalyticsAPI();
            program.client.AzureRegion​ = AzureRegions.Westus;
            program.client.SubscriptionKey = "5cc94376d64945528093ba3a25f4f042";

            while (true)
            {
                // Language Detection
                Console.WriteLine("-----------------------------------------------------------------------------------------------\nDetect language for text:");
                detectString = Console.ReadLine();

                program.DetectLanguage(detectString);


                // Sentiment Score
                Console.WriteLine("Detect sentiment score for text:");
                detectSentiment = Console.ReadLine();
                Console.WriteLine("Enter language code for inserted text (e.g. en):");
                language = Console.ReadLine();

                program.ReturnSentiment(detectSentiment, language);
            }

        }

        private void DetectLanguage(string userInput)
        {

            LanguageBatchResult language = client.DetectLanguage(new BatchInput(new List<Input>{new Input("1", userInput)}));
           
            foreach (LanguageBatchResultItem document in language.Documents)
            {
                Console.WriteLine("Inserted text is written in {0} language.\n", document.DetectedLanguages[0].Name);
            }

        }

        private void ReturnSentiment(string userInput, string lang)
        {
            SentimentBatchResult sentiment = client.Sentiment(new MultiLanguageBatchInput(new List<MultiLanguageInput>(){new MultiLanguageInput(lang, "0", userInput)}));

            foreach (var document in sentiment.Documents)
            {
                Console.WriteLine("Sentiment score of inserted text is {0}\n", Math.Round(document.Score.Value,2));
            }
        }
    }
}
