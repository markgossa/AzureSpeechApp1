using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace AzureSpeechApp1
{
    public class Keys
    {
        public Keys(IConfiguration config)
        {
            SpeechConfigKey = config.GetSection("Keys").GetValue<string>("SpeechRecognitionKey");
            SentimentConfigKey = config.GetSection("Keys").GetValue<string>("SentimentRecognitionKey");

        }

        public string SpeechConfigKey { get; set; }
        public string SentimentConfigKey { get; set; }
    }
}
