//using System.Net.Http;
//using System.Threading;
//using System.Threading.Tasks;
//using Microsoft.Rest;
//using Microsoft.Extensions.Configuration;

//namespace AzureSpeechApp1
//{
//    public class ApiKeyServiceClientCredentials : ServiceClientCredentials
//    {
//        string SentimentRecognitionKey { get; set; }

//        public ApiKeyServiceClientCredentials(IConfiguration config)
//        {
//            SentimentRecognitionKey = config.GetSection("Keys").GetValue<string>("SentimentRecognitionKey");
//        }

//        public override Task ProcessHttpRequestAsync(HttpRequestMessage request, CancellationToken cancellationToken)
//        {
//            request.Headers.Add("Ocp-Apim-Subscription-Key", SentimentRecognitionKey);
//            return base.ProcessHttpRequestAsync(request, cancellationToken);
//        }
//    }
//}
