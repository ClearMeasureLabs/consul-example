using System;
using System.Threading.Tasks;
using System.Web.Http;
using Microphone.Core;
using Microphone.Core.ClusterProviders;
using Microphone.WebApi;
using RestSharp;

namespace Service2
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Cluster.Bootstrap(new WebApiProvider(), new ConsulProvider(), "ConsulDemoService2API", "v1");
            Console.ReadLine();
        }
    }

    public class PingController : ApiController
    {
        public string Get()
        {
            return $"Service2 PING! {DateTime.Now}";
        }
    }

    public class CustomersController: ApiController
    {
        public async Task<string> Get()
        {
            var instance = await Cluster.FindServiceInstanceAsync("ConsulDemoService1API");

            var url = $"http://{instance.Address.ToLower()}:{instance.Port}";

            var client = new RestClient(url);

            var request = new RestRequest("/ping");

            var response = await client.ExecuteTaskAsync(request);
            
            return $"Service 1 at {url} returns {response.StatusCode} with {response.Content}";
        }
    }
}
