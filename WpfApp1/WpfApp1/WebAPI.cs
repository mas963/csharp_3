using System.Configuration;
using System.Net;
using System.Net.Http;

namespace WpfApp1;

class WebAPI
{
    public static Task<HttpResponseMessage> GetCall(string url)
    {
        try
        {
            var API_BASE_URL = ConfigurationManager.AppSettings["API_BASE_URL"];
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            string apiUrl = API_BASE_URL + url;
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiUrl);
                client.Timeout = TimeSpan.FromSeconds(900);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                var response = client.GetAsync(apiUrl);
                response.Wait();
                return response;
            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }
}
