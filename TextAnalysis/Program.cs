using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.CognitiveServices.Language.TextAnalytics;
using Microsoft.Azure.CognitiveServices.Language.TextAnalytics.Models;

namespace TextAnalysis
{
    class Program
    {
               
        public static void Main(string[] args)
        {
            ITextAnalyticsAPI​ client​ = new TextAnalyticsAPI();
            client.AzureRegion​ = AzureRegions.Westus;
            client.SubscriptionKey = "5cc94376d64945528093ba3a25f4f042";

            while (true)
            {
                // Language Detection
                Console.WriteLine("-----------------------------------------------------------------------------------------------\nDetect language for text:");
                string detectString = Console.ReadLine();

                detectLanguage(client​, detectString);


                // Sentiment Score
                Console.WriteLine("Detect sentiment score for text:");
                string detectSentiment = Console.ReadLine();
                Console.WriteLine("Enter language code for inserted text (e.g. en):");
                string language = Console.ReadLine();

                returnSentiment(client​, detectSentiment, language);
            }

        }

        public static void detectLanguage(ITextAnalyticsAPI​ cl, string userInput)
        {

            LanguageBatchResult language = cl.DetectLanguage(new BatchInput(new List<Input>{new Input("1", userInput)}));
           
            foreach (LanguageBatchResultItem document in language.Documents)
            {
                Console.WriteLine("Inserted text is written in {0} language.\n", document.DetectedLanguages[0].Name);
            }

        }

        public static void returnSentiment(ITextAnalyticsAPI​ cl, string userInput, string lang)
        {
            SentimentBatchResult sentiment = cl.Sentiment(new MultiLanguageBatchInput(new List<MultiLanguageInput>(){new MultiLanguageInput(lang, "0", userInput)}));

            foreach (var document in sentiment.Documents)
            {
                Console.WriteLine("Sentiment score of inserted text is {0}\n", Math.Round(document.Score.Value,2));
            }
        }
    }
}
