using System.Net.Http.Json;
using System.Text.Json;

namespace ProjectBlazor.Components.Pages.Maintenance.Business.Locations;

public class LocationsServiceDebug
{
    private readonly HttpClient _http;

    public LocationsServiceDebug(HttpClient http)
    {
        _http = http;
    }

    public async Task<(int statusCode, string content, int count)> GetLocationsDebugAsync()
    {
        try
        {
            var response = await _http.GetAsync("ApiBusinessLoc/BusinessLocList");
            var content = await response.Content.ReadAsStringAsync();
            
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            var data = JsonSerializer.Deserialize<List<LocationDto>>(content, options);
            
            return ((int)response.StatusCode, content.Substring(0, Math.Min(200, content.Length)), data?.Count ?? 0);
        }
        catch (Exception ex)
        {
            return (0, $"Exception: {ex.Message}", 0);
        }
    }
}
