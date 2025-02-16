

using eCommerce.OrderService.BusinessLogicLayer.DTO;
using System.Net.Http.Json;

namespace eCommerce.OrderService.BusinessLogicLayer.Client;

public class UserMicroServiceClient
{
    private readonly HttpClient _httpClient;
    public UserMicroServiceClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<UserDTO?> GetUserByUserId(Guid userID)
    {
        HttpResponseMessage responseMessage = await _httpClient.GetAsync($"/api/users/{userID}");
        if (!responseMessage.IsSuccessStatusCode)
        {
            if (responseMessage.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return null;
            }
            else if(responseMessage.StatusCode==System.Net.HttpStatusCode.BadRequest)
            {
                throw new HttpRequestException("Bad Request", null,System.Net.HttpStatusCode.BadRequest);
            }
            else
            {
                throw new HttpRequestException($"Http request failed with status code{ responseMessage.StatusCode }");
            }
        }
        UserDTO? user = await responseMessage.Content.ReadFromJsonAsync<UserDTO>();
        if (user == null)
        {
            throw new ArgumentException("Invalid User");
        }
        return user;
    }
}
