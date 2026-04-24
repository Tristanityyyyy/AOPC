using System.Net.Http.Json;
using System.Text.Json;

namespace ProjectBlazor.Components.Pages.Maintenance.Business.Locations;

public class LocationsService
{
    private readonly HttpClient _http;
    private static readonly JsonSerializerOptions _options = new() { PropertyNameCaseInsensitive = true };

    public LocationsService(HttpClient http)
    {
        _http = http;
    }

    public async Task<List<LocationDto>> GetLocationsAsync()
    {
        try
        {
            var response = await _http.GetAsync("api/ApiBusinessLoc/BusinessLocList");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var data = JsonSerializer.Deserialize<List<LocationDto>>(content, _options);
                return data ?? new List<LocationDto>();
            }
        }
        catch (Exception ex) 
        { 
            Console.WriteLine($"Locations error: {ex.Message}"); 
        }
        return new List<LocationDto>();
    }
}

public class LocationDto
{
    public int Id { get; set; }
    public string BusinessLocID { get; set; } = "";
    public string City { get; set; } = "";
    public string? PostalCode { get; set; }
    public string Country { get; set; } = "";
    public string DateCreated { get; set; } = "";
    public string Status { get; set; } = "";
}