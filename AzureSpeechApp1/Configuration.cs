using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace AzureSpeechApp1
{
    public class Configuration
    {
        public Configuration(IConfiguration config)
        {
            GreetingMessage = config.GetSection("Configuration").GetValue<string>("GreetingMessage");
            GoodDayMessage = config.GetSection("Configuration").GetValue<string>("GoodDayMessage");
            BadDayMessage = config.GetSection("Configuration").GetValue<string>("BadDayMessage");
            AverageDayMessage = config.GetSection("Configuration").GetValue<string>("AverageDayMessage");
            DidNotUnderstandMessage = config.GetSection("Configuration").GetValue<string>("DidNotUnderstandMessage");
        }

        public string GreetingMessage { get; set; }
        public string GoodDayMessage { get; set; }
        public string BadDayMessage { get; set; }
        public string AverageDayMessage { get; set; }
        public string DidNotUnderstandMessage { get; set; }
    }
}
