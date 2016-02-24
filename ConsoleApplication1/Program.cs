using System;
using System.Linq;
using System.Net.Http.Headers;
using ConsoleApplication1.ServiceReference;
using Microsoft.IdentityModel.Clients.ActiveDirectory;

namespace ConsoleApplication1
{
    class Program
    {
        private const string Authority = "XXX";
        private const string Resource = "XXX";
        private const string NativeClientId = "XXX";
        private const string NativeClientRedirectUri = "http://localhost";
        private static readonly string ServiceAddress = $"{Resource}/WcfDataService.svc";

        static void Main()
        {
            var authContext = new AuthenticationContext(Authority);
            var result = authContext.AcquireToken(Resource, NativeClientId, new Uri(NativeClientRedirectUri));

            SampleContext context = new SampleContext(new Uri(ServiceAddress));
            context.SendingRequest2 += (sender, args) =>
            {
                var header = new AuthenticationHeaderValue("Bearer", result.AccessToken);
                args.RequestMessage.SetHeader("Authorization", header.ToString());
            };
            var employees = context.Employees.ToArray();
            Console.WriteLine(employees.Length);
        }
    }
}
