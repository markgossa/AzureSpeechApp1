using System;
using System.Speech.Synthesis;
using System.IO;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using System.Threading;
using Microsoft.CognitiveServices.Speech;
using Microsoft.Azure.CognitiveServices.Language.TextAnalytics;
using Microsoft.Azure.CognitiveServices.Language.TextAnalytics.Models;
using System.Collections.Generic;
using Microsoft.Rest;
using System.Net.Http;

namespace AzureSpeechApp1
{
    public class Program
    {
        static void Main()
        {
            Program.RecognizeSpeechAsync().Wait();
        }

        public Program()
        {
            // New collection for all dependency types
            var services = new ServiceCollection();
            //Add dependencies to collection
            ConfigureServices(services);
            var serviceProvider = services.BuildServiceProvider();
        
            // Get keys
            Keys keys = serviceProvider.GetService<Keys>();
            Configuration configuration= serviceProvider.GetService<Configuration>();

            SpeechRecognitionKey = keys.SpeechConfigKey;
            SentimentRecognitionKey = keys.SentimentConfigKey;
            GreetingMessage = configuration.GreetingMessage;
            GoodDayMessage = configuration.GoodDayMessage;
            BadDayMessage = configuration.BadDayMessage;
            AverageDayMessage = configuration.AverageDayMessage;
            DidNotUnderstandMessage = configuration.DidNotUnderstandMessage;
        }
        

        private string SpeechRecognitionKey { get; set; }
        private string SentimentRecognitionKey { get; set; }
        private string GreetingMessage { get; set; }
        private string GoodDayMessage { get; set; }
        private string BadDayMessage { get; set; }
        private string AverageDayMessage { get; set; }
        private string DidNotUnderstandMessage { get; set; }

        public static async Task RecognizeSpeechAsync()
        {
            
            Program program = new Program();
            SaySomething(program.GreetingMessage);

            // Creates an instance of a speech config with specified subscription key and service region.
            // Replace with your own subscription key and service region (e.g., "westus").
            var speechConfig = SpeechConfig.FromSubscription(program.SpeechRecognitionKey, "WestEurope");

            // Initialize a new instance of the SpeechSynthesizer.  
            SpeechSynthesizer speechSynthesiser = new SpeechSynthesizer();

            // Configure the audio output
            speechSynthesiser.SetOutputToDefaultAudioDevice();

            // Creates a speech recognizer.
            using (var recognizer = new SpeechRecognizer(speechConfig))
            {
                // Performs recognition. RecognizeOnceAsync() returns when the first utterance has been recognized,
                // so it is suitable only for single shot recognition like command or query. For long-running
                // recognition, use StartContinuousRecognitionAsync() instead.
                Console.WriteLine($"App: {program.GreetingMessage}");
                var result = await recognizer.RecognizeOnceAsync();
                Console.WriteLine($"You: {result.Text}");


                double? sentiment = GetSentiment(result.Text);

                // Checks result.
                if (result.Reason == ResultReason.RecognizedSpeech)
                {
                    if (sentiment <= 0.3)
                    {
                        speechSynthesiser.Speak(program.BadDayMessage);
                        Console.WriteLine(program.BadDayMessage);
                    }
                    else if (sentiment >= 0.95)
                    {
                        speechSynthesiser.Speak(program.GoodDayMessage);
                        Console.WriteLine(program.GoodDayMessage);
                    }
                    else
                    {
                        speechSynthesiser.Speak(program.AverageDayMessage);
                        Console.WriteLine(program.AverageDayMessage);
                    }
                }
                else if (result.Reason == ResultReason.NoMatch)
                {
                    speechSynthesiser.Speak(program.DidNotUnderstandMessage);
                }
            }
        }

        private static double GetSentiment(string text)
        {
            // Create a client.
            ITextAnalyticsClient client = new TextAnalyticsClient(new ApiKeyServiceClientCredentials())
            {
                Endpoint = "https://westeurope.api.cognitive.microsoft.com/"
            };

            SentimentBatchResult result = client.SentimentAsync(
                new MultiLanguageBatchInput(
                    new List<MultiLanguageInput>()
                    {
                        new MultiLanguageInput("en", "0", text)
                    })).Result;
            return result.Documents[0].Score.Value;
        }

        private static void ConfigureServices(ServiceCollection services)
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile("appsettings-development.json", optional: false, reloadOnChange: true);

            IConfiguration configuration = config.Build();

            services.AddSingleton<IConfiguration>(configuration);
            services.AddTransient<Keys>();
            services.AddTransient<Configuration>();
        }

        class ApiKeyServiceClientCredentials : ServiceClientCredentials
        {
            Program program = new Program();
            public override Task ProcessHttpRequestAsync(HttpRequestMessage request, CancellationToken cancellationToken)
            {
                request.Headers.Add("Ocp-Apim-Subscription-Key", program.SentimentRecognitionKey);
                return base.ProcessHttpRequestAsync(request, cancellationToken);
            }
        }

        public static void SaySomething(string text)
        {
            // Initialize a new instance of the SpeechSynthesizer
            SpeechSynthesizer speechSynthesiser = new SpeechSynthesizer();

            // Configure the audio output
            speechSynthesiser.SetOutputToDefaultAudioDevice();

            // Speak
            speechSynthesiser.Speak(text);
        }
    }
}
