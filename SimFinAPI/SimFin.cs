using RestSharp;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using static System.Net.WebRequestMethods;

// https://www.milanjovanovic.tech/blog/the-right-way-to-use-httpclient-in-dotnet
// https://simfin.readme.io/reference/getting-started-1

namespace SimFinAPI
{
    public class SimFin
    {

        private RestClient _restClient;
        private RestClientOptions _restClientOptions;
        private RestRequest _request;
        private string _url = "https://prod.simfin.com/api/v3/companies/";

        public SimFin(string api_key)
        {
            // instantiate rest request members
            _request = new RestRequest();
            _restClient = new RestClient();
            _restClientOptions = new RestClientOptions();

            // add necessary headers for any request
            _request.AddHeader("Accept", "application/json, text/plain, */*");
            _request.AddHeader("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/130.0.0.0 Safari/537.36");

            // add api key/auth header
            _request.AddHeader("Authorization", api_key);
        }

        public void Log(string text)
        {
            Console.WriteLine(text);
        }

        // TODO:
        // implement comma separation for necessary parameters


        /*
         * Functions written to retain general information of a company, their filings, or get a json file of all companies stored for view.
         */
        
        public async Task<string> CreateJsonOfCompanies(string path)
        {
            try
            {
                string url = _url;
                _restClientOptions = new RestClientOptions(url);

                // instantiate client
                _restClient = new RestClient(_restClientOptions);
                _request = new RestRequest("");

                // get link using params and return as jsonified content
                var response = await _restClient.GetAsync(_request);

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    // write to json using provided file path
                    using (StreamWriter file = File.CreateText(path))
                    {
                        JsonSerializer serializer = new JsonSerializer();
                        // serialize object directly into file stream
                        serializer.Serialize(file, response.Content);
                    }
                    return "Successfully wrote companies JSON file to provided path";
                } else
                {
                    return "Error writing companies JSON file to provided path";
                }
            }
            catch (Exception ex)
            {
                return "Error writing companies JSON file to provided path";
            }
        }

        public async Task<JsonResult> GeneralInfo(string ticker)
        {
            try
            {
                // create link using passed in params
                string url = _url + $"?ticker={ticker}";
                _restClientOptions = new RestClientOptions(url);

                // instantiate client
                _restClient = new RestClient(_restClientOptions);
                _request = new RestRequest("");

                // get link using params and return as jsonified content
                var response = await _restClient.GetAsync(_request);

                // return response
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
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

        public async Task<JsonResult> RetrieveFilingsList(string ticker)
        {
            try
            {
                // create link using passed in params
                string url = $"https://prod.simfin.com/api/v3/filings/by-company?ticker={ticker}";
                _restClientOptions = new RestClientOptions(url);

                // instantiate client
                _restClient = new RestClient(_restClientOptions);
                _request = new RestRequest("");

                // get link using params and return as jsonified content
                var response = await _restClient.GetAsync(_request);

                // return response
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
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

        /*
         * Functions written to view the financial statements with given parameters.
         */

        public async Task<JsonResult> FinancialStatementsCompact(string ticker, string statement_type, int fyear, string period)
        {
            try
            {
                // create link using passed in params
                string url = _url + "statements/compact/";
                string linkParams = $"?ticker={ticker}&statements={statement_type}&fyear={fyear}&period={period}";
                url = url + linkParams;
                _restClientOptions = new RestClientOptions(url);

                // instantiate client
                _restClient = new RestClient(_restClientOptions);
                _request = new RestRequest("");

                // get link using params and return as jsonified content
                var response = await _restClient.GetAsync(_request);

                // return response
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    return new JsonResult(response);
                }
                else
                {
                    Log("Error returning company financial statements.");
                    return new JsonResult(response);
                }
            } catch (Exception ex) {
                Log("Error returning company financial statements.");
                return new JsonResult(ex);
            }
        }

        public async Task<JsonResult> FinancialStatementsVerbose(string ticker, string statement_type, int fyear, string period)
        {
            try
            {
                // create link using passed in params
                string url = _url + "statements/verbose/";
                string linkParams = $"?ticker={ticker}&statements={statement_type}&fyear={fyear}&period={period}";
                url = url + linkParams;
                _restClientOptions = new RestClientOptions(url);

                // instantiate client
                _restClient = new RestClient(_restClientOptions);
                _request = new RestRequest("");

                // get link using params and return as jsonified content
                var response = await _restClient.GetAsync(_request);

                // return response
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    return new JsonResult(response);
                }
                else
                {
                    Log("Error returning company financial statements.");
                    return new JsonResult(response);
                }
            }
            catch (Exception ex)
            {
                Log("Error returning company financial statements.");
                return new JsonResult(ex);
            }
        }

        /*
         * Functions written to view the outstanding shares of a given company.
         */
        public async Task<JsonResult> CommonSharesOutstanding(string ticker, string start, string end)
        {
            try
            {
                // create link using passed in params
                string url = _url + $"?ticker={ticker}&start={start}&end={end}";
                _restClientOptions = new RestClientOptions(url);

                // instantiate client
                _restClient = new RestClient(_restClientOptions);
                _request = new RestRequest("");

                // get link using params and return as jsonified content
                var response = await _restClient.GetAsync(_request);

                // return response
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    return new JsonResult(response);
                }
                else
                {
                    Log("Error returning common shares outstanding.");
                    return new JsonResult(response);
                }
            }
            catch (Exception ex)
            {
                Log("Error returning common shares outstanding.");
                return new JsonResult(ex);
            }
        }

        public async Task<JsonResult> WeightedSharesOutstanding(string ticker, string period, string fyear, string start, string end)
        {
            try
            {
                // create link using passed in params
                string url = _url + $"?ticker={ticker}&period={period}&fyear={fyear}&start={start}&end={end}";
                _restClientOptions = new RestClientOptions(url);

                // instantiate client
                _restClient = new RestClient(_restClientOptions);
                _request = new RestRequest("");

                // get link using params and return as jsonified content
                var response = await _restClient.GetAsync(_request);

                // return response
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    return new JsonResult(response);
                }
                else
                {
                    Log("Error returning weighted shares outstanding.");
                    return new JsonResult(response);
                }
            }
            catch (Exception ex)
            {
                Log("Error returning weighted shares outstanding.");
                return new JsonResult(ex);
            }
        }

        /*
         * Functions for pulling price data and performing machine learning using the data.
         */

        public async Task<JsonResult> PullPriceDataCompact(string ticker, string period, string fyear, string start, string end)
        {
            try
            {
                // create link using passed in params
                string url = _url + "prices/compact";
                url = _url + $"?ticker={ticker}&ratios=false&asreported=true&start={start}&end={end}";
                _restClientOptions = new RestClientOptions(url);

                // instantiate client
                _restClient = new RestClient(_restClientOptions);
                _request = new RestRequest("");

                // get link using params and return as jsonified content
                var response = await _restClient.GetAsync(_request);

                // return response
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    return new JsonResult(response);
                }
                else
                {
                    Log("Error returning compact price data.");
                    return new JsonResult(response);
                }
            }
            catch (Exception ex)
            {
                Log("Error returning compact price data");
                return new JsonResult(ex);
            }
        }

        public async Task<JsonResult> PullPriceDataVerbose(string ticker, string period, string fyear, string start, string end)
        {
            try
            {
                // create link using passed in params
                string url = _url + "prices/verbose";
                url = _url + $"?ticker={ticker}&ratios=false&asreported=true&start={start}&end={end}";
                _restClientOptions = new RestClientOptions(url);

                // instantiate client
                _restClient = new RestClient(_restClientOptions);
                _request = new RestRequest("");

                // get link using params and return as jsonified content
                var response = await _restClient.GetAsync(_request);

                // return response
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    return new JsonResult(response);
                }
                else
                {
                    Log("Error returning verbose price data.");
                    return new JsonResult(response);
                }
            }
            catch (Exception ex)
            {
                Log("Error returning compact price data.");
                return new JsonResult(ex);
            }
        }
    }
}
