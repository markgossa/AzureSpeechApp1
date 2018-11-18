//using System.Collections.Generic;
//using Microsoft.Azure.CognitiveServices.Language.TextAnalytics;
//using Microsoft.Azure.CognitiveServices.Language.TextAnalytics.Models;
//using Microsoft.Extensions.Configuration;

//namespace AzureSpeechApp1
//{
//    public class SentimentRecognition
//    {
//        public SentimentRecognition(IConfiguration config)
//        {
//            // Create a client.
//            ITextAnalyticsClient client = new TextAnalyticsClient(new ApiKeyServiceClientCredentials(config))
//            {
//                Endpoint = "https://westeurope.api.cognitive.microsoft.com/"
//            };

//            double? GetSentimentDouble(string text)
//            {


//                SentimentBatchResult result = client.SentimentAsync(
//                    new MultiLanguageBatchInput(
//                        new List<MultiLanguageInput>()
//                        {
//                    new MultiLanguageInput("en", "0", text)
//                        })).Result;

//                return result.Documents[0].Score.Value;
//            }
//        }

//        public string SentimentRecognitionKey { get; set; }
//        public double? SentimentRecognitionResult { get; set; }
//    }
//}
