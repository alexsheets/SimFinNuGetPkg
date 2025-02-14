using RestSharp;

// using System.Web.Helpers;
// using System.Web.Mvc;
using Microsoft.AspNetCore.Mvc;

using static System.Net.WebRequestMethods;
using Microsoft.Extensions.Configuration;

// https://www.milanjovanovic.tech/blog/the-right-way-to-use-httpclient-in-dotnet

namespace SimFinAPI
{
    public class SimFin
    {

        private readonly IConfiguration _configuration;
        private RestClient _restClient;
        private RestClientOptions _restClientOptions;
        private RestRequest _request;
        private string _url = "https://backend.simfin.com/api/v3/companies/";

        public SimFin(IConfiguration configuration)
        {
            _configuration = configuration;
            // instantiate rest request members
            _request = new RestRequest();
            _restClient = new RestClient();
            _restClientOptions = new RestClientOptions();
            // add necessary headers for any request
            _request.AddHeader("Accept", "application/json, text/plain, */*");
            _request.AddHeader("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/130.0.0.0 Safari/537.36");
        }

        public void Log(string text)
        {
            Console.WriteLine(text);
        }

        public async Task<JsonResult> RequestGeneralInfo(string api_key, string ticker)
        {
            try
            {
                // create link using passed in params
                _url = _url + $"?ticker={ticker}";
                _restClientOptions = new RestClientOptions(_url);

                // instantiate client
                _restClient = new RestClient(_restClientOptions);
                _request = new RestRequest("");

                // add api key/auth header
                _request.AddHeader("Authorization", api_key);

                // get link using params and return as jsonified content
                var response = await _restClient.GetAsync(_request);

                // return response
                if (response != null)
                {
                    return new JsonResult(response);
                }
                else
                {
                    Log("Error returning general company info.");
                    return new JsonResult(response);
                }
            }
            catch (Exception ex)
            {
                Log("Error returning general company info.");
                return new JsonResult(ex);
            }
        }

        public async Task<JsonResult> RequestCompanyFinancialStatements(string api_key, string ticker, string statement_type, int fyear, string period)
        {
            try
            {
                // create link using passed in params
                _url = _url + "statements/compact/";
                string linkParams = $"?ticker={ticker}&statements={statement_type}&fyear={fyear}&period={period}";
                _url = _url + linkParams;
                _restClientOptions = new RestClientOptions(_url);

                // instantiate client
                _restClient = new RestClient(_restClientOptions);
                _request = new RestRequest("");

                // add api key/auth header
                _request.AddHeader("Authorization", api_key);

                // get link using params and return as jsonified content
                var response = await _restClient.GetAsync(_request);

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
