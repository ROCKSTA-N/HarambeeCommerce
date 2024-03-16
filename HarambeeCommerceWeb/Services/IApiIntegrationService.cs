namespace HarambeeCommerceWeb.Services;

public interface IApiIntegrationService
{
    Task<object> GetCustomerByIdAsync(long customerId);

    Task<List<object>> GetAllproductsAsync();

    Task<object> AddProductToBasketAsync(long basketId, long productId);

    Task<decimal> GetbasketValueAsync(long basketId);


}

public  class ApiIntegrationService : IApiIntegrationService
{
    private readonly HttpClient _httpClient;

    public ApiIntegrationService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<object> AddProductToBasketAsync(long basketId, long productId)
    {
        var request = new HttpRequestMessage
        {
            Method = HttpMethod.Post,
            RequestUri = new($"basket/addProduct")
        };
        var response = await _httpClient.SendAsync(request);

        var content = await response.Content.ReadFromJsonAsync<object>();

        return content;
    }

    public async Task<List<object>> GetAllproductsAsync()
    {
        var request = new HttpRequestMessage
        {
            Method = HttpMethod.Get,
            RequestUri = new($"product")
        };
        var response = await _httpClient.SendAsync(request);

        var content = await response.Content.ReadFromJsonAsync<List<object>>();

        return content;
    }

    public async Task<decimal> GetbasketValueAsync(long basketId)
    {
        var request = new HttpRequestMessage
        {
            Method = HttpMethod.Get,
            RequestUri = new($"basket/calculatevalue/{basketId}")
        };
        var response = await _httpClient.SendAsync( request );

        var content = await response.Content.ReadAsStringAsync(); 

        return response.IsSuccessStatusCode ? Convert.ToDecimal( content ) : 0;
    }

    public async Task<object> GetCustomerByIdAsync(long customerId)
    {
        var request = new HttpRequestMessage
        {
            Method = HttpMethod.Get,
            RequestUri = new($"customer/{customerId}")
        };
        var response = await _httpClient.SendAsync(request);

        var content = await response.Content.ReadFromJsonAsync<object>();

        return content;
    }
}
