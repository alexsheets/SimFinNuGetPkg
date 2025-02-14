using RestSharp;

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

        public async Task<JsonResult> RequestGeneralInfo(string api_key, string ticker)
        {

        }

        public async Task<JsonResult> RequestCompanyStatements(string api_key, string ticker, string statement_type, int year, string quarter)
        {
            try
            {
                // create link using passed in params
                string linkParams = $"?ticker={ticker}&statements={statement_type}&fyear={year}&period={quarter}";
                string link = "https://backend.simfin.com/api/v3/companies/statements/compact/" + linkParams;
                var options = new RestClientOptions(link);

                // instantiate client
                var client = new RestClient(options);
                var request = new RestRequest("");

                // set up headers
                request.AddHeader("Accept", "application/json, text/plain, */*");
                request.AddHeader("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/130.0.0.0 Safari/537.36");
                request.AddHeader("Authorization", api_key);

                // get link using params and return as jsonified content (the response will be json, this deserializes it)
                var response = await client.GetAsync(request);

                // return response
                if (response != null)
                {
                    return new JsonResult(response);
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
