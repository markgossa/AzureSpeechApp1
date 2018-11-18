//using System;
//using System.Threading.Tasks;
//using Microsoft.CognitiveServices.Speech;
//using System.Speech.Synthesis;
//using Microsoft.Azure.CognitiveServices.Language.TextAnalytics;
//using Microsoft.Azure.CognitiveServices.Language.TextAnalytics.Models;
//using System.Collections.Generic;
//using Microsoft.Rest;
//using System.Net.Http;
//using System.Threading;


//namespace AzureSpeechApp1
//{
//    class ProgramOld
//    {

//        private string SentimentConfigKey { get; set; }
//        private string SpeechConfigKey { get; set; }

//        public static async Task RecognizeSpeechAsync(string SpeechConfigKey, string SentimentConfigKey)
//        {
//            // Creates an instance of a speech config with specified subscription key and service region.
//            // Replace with your own subscription key and service region (e.g., "westus").
//            var speechConfig = SpeechConfig.FromSubscription(SpeechConfigKey, "WestEurope");

//            // Initialize a new instance of the SpeechSynthesizer.  
//            SpeechSynthesizer speechSynthesiser = new SpeechSynthesizer();

//            // Configure the audio output
//            speechSynthesiser.SetOutputToDefaultAudioDevice();

//            // Creates a speech recognizer.
//            using (var recognizer = new SpeechRecognizer(speechConfig))
//            {
//                speechSynthesiser.Speak("Hello, how's it going?");

//                // Performs recognition. RecognizeOnceAsync() returns when the first utterance has been recognized,
//                // so it is suitable only for single shot recognition like command or query. For long-running
//                // recognition, use StartContinuousRecognitionAsync() instead.
//                var result = await recognizer.RecognizeOnceAsync();

//                // Checks result.
//                if (result.Reason == ResultReason.RecognizedSpeech)
//                {
//                    if (GetSentiment(result.Text) <= 0.3)
//                    {
//                        speechSynthesiser.Speak("Oh no. I'm sorry to hear that. What happened?");
//                    }
//                    else if (GetSentiment(result.Text) >= 0.85)
//                    {
//                        speechSynthesiser.Speak("Really? That's great. What made it so special?");
//                    }
//                    else
//                    {
//                        speechSynthesiser.Speak("Cool. What did you get up to today?");
//                    }
//                }
//                else if (result.Reason == ResultReason.NoMatch)
//                {
//                    speechSynthesiser.Speak("I'm sorry, I didn't understand you");
//                }
//            }
//        }


//        private static double GetSentiment(string text)
//        {
//            // Create a client.
//            ITextAnalyticsClient client = new TextAnalyticsClient(new ApiKeyServiceClientCredentials())
//            {
//                Endpoint = "https://westeurope.api.cognitive.microsoft.com/"
//            };

//            SentimentBatchResult result = client.SentimentAsync(
//                new MultiLanguageBatchInput(
//                    new List<MultiLanguageInput>()
//                    {
//                        new MultiLanguageInput("en", "0", text)
//                    })).Result;
//            return result.Documents[0].Score.Value;
//        }

//        class ApiKeyServiceClientCredentials : ServiceClientCredentials
//        {
//            public override Task ProcessHttpRequestAsync(HttpRequestMessage request, CancellationToken cancellationToken)
//            {
//                request.Headers.Add("Ocp-Apim-Subscription-Key", SentimentConfigKey);
//                return base.ProcessHttpRequestAsync(request, cancellationToken);
//            }
//        }

//        //static void Main()
//        //{
//        //    RecognizeSpeechAsync().Wait();
//        //}
//    }
//}