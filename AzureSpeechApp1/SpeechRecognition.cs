//using System.Threading.Tasks;
//using Microsoft.Extensions.Configuration;
//using Microsoft.CognitiveServices.Speech;
//using System.Speech.Synthesis;

//namespace AzureSpeechApp1
//{
//    public class SpeechRecognition
//    {
//        private async Task<string> RecognizeSpeechAsync(string SpeechConfigKey)
//        {
//            // Creates an instance of a speech config with specified subscription key and service region.
//            // Replace with your own subscription key and service region (e.g., "westus").
//            var speechConfig = SpeechConfig.FromSubscription(SpeechConfigKey, "WestEurope");

//            // Creates a speech recognizer.
//            using (var recognizer = new SpeechRecognizer(speechConfig))
//            {
//                var result = await recognizer.RecognizeOnceAsync();

//                if (result.Reason == ResultReason.NoMatch)
//                {
//                    Program.SaySomething("I'm sorry, I didn't understand you.");
//                    return null;
//                }
//                else
//                {
//                    return result.Text;
//                }
//            }
//        }

//        public string RecognizeSpeech(string SpeechConfigKey)
//        {
//            return RecognizeSpeechAsync(SpeechConfigKey).Wait();
//        }
//    }
//}
