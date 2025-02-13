using System.Net;
using System.Net.Http.Json;

// https://www.milanjovanovic.tech/blog/the-right-way-to-use-httpclient-in-dotnet

namespace SimFinAPI
{
    public class Class1
    {
        public void Log(string text)
        {
            Console.WriteLine(text);
        }

        public async Task<JsonContent> RequestCompanyStatements(string ticker, string statement_type, int year, string quarter)
        {
            // instantiate client
            using var client = new HttpClient();

        }
    }
}
