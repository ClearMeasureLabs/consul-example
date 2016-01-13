using System;
using System.Web.Http;
using Microphone.Core;
using Microphone.Core.ClusterProviders;
using Microphone.WebApi;

namespace Service1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Cluster.Bootstrap(new WebApiProvider(), new ConsulProvider(), "ConsulDemoService1API", "v1");
            Console.ReadLine();
        }
    }

    public class PingController : ApiController
    {
        public string Get()
        {
            return $"Service1 PING! {DateTime.Now}";
        }
    }
}
