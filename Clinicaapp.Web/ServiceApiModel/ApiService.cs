using System.Text;
using Newtonsoft.Json;

public  class ApiService
{
    private readonly string _baseUrl;

    public ApiService(string baseUrl)
    {
        _baseUrl = baseUrl;
    }

    
    public async Task<T> MakeApiRequest<T>(string endpoint, HttpMethod method, object data = null)
    {
        using (var client = new HttpClient())
        {
            client.BaseAddress = new Uri(_baseUrl);
            HttpRequestMessage request = new HttpRequestMessage(method, endpoint);

            if (data != null)
            {
                
                request.Content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
            }

            var responseTask = await client.SendAsync(request);
            if (responseTask.IsSuccessStatusCode)
            {
                string response = await responseTask.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<T>(response);
            }
            else
            {
               
                throw new HttpRequestException($"Error con la solicitud de API. Status Code: {responseTask.StatusCode}");
            }
        }
    }
}
