using System.Net;
using System.Net.Http.Json;
using System.Runtime;
using System.Text.Json.Nodes;

// using System.Web.Helpers;
// using System.Web.Mvc;
using Microsoft.AspNetCore.Mvc;

using static System.Net.WebRequestMethods;

// https://www.milanjovanovic.tech/blog/the-right-way-to-use-httpclient-in-dotnet

namespace SimFinAPI
{
    public class SimFin
    {

        public void Log(string text)
        {
            Console.WriteLine(text);
        }

        public async Task<JsonResult> RequestCompanyStatements(string api_key, string ticker, string statement_type, int year, string quarter)
        {
            try
            {
                // instantiate client
                using var client = new HttpClient();

                // set up headers
                client.DefaultRequestHeaders.Add("Accept", "application/json, text/plain, */*");
                client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/130.0.0.0 Safari/537.36");
                client.DefaultRequestHeaders.Add("Authorization", api_key);

                string linkParams = $"?ticker={ticker}&statements={statement_type}&fyear={year}&period={quarter}";
                string link = "https://backend.simfin.com/api/v3/companies/statements/compact/" + linkParams;
                client.BaseAddress = new Uri(link);

                // get link using params and return as jsonified content (the response will be json, this deserializes it)
                JsonResult response = await client.GetFromJsonAsync<JsonResult>(link);

                // return 
                if (response != null)
                {
                    return response;
                }
                else
                {
                    Log("Error returning company statements.");
                    return new JsonResult(response);
                }
            } catch (Exception ex) {
                Log("Error returning company statements.");
                return new JsonResult(ex);
            }
        }
    }
}
