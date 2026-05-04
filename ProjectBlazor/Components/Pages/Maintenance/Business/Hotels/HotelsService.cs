using System.Net.Http.Json;
using System.Text.Json;

namespace ProjectBlazor.Components.Pages.Maintenance.Business.Hotels;

public class HotelsService
{
    private readonly HttpClient _http;
    private static readonly JsonSerializerOptions _options = new() { PropertyNameCaseInsensitive = true };

    public HotelsService(HttpClient http)
    {
        _http = http;
    }

    public async Task<List<HotelDto>> GetHotelsAsync()
    {
        try
        {
            var response = await _http.GetAsync("api/ApiBusiness/BusinessList");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                
                // Try direct array first
                try
                {
                    var data = JsonSerializer.Deserialize<List<HotelDto>>(content, _options);
                    if (data != null && data.Count > 0)
                    {
                        return data;
                    }
                }
                catch
                {
                    // Try wrapped response
                    var wrappedData = JsonSerializer.Deserialize<ApiResponse>(content, _options);
                    if (wrappedData?.Data != null)
                    {
                        return wrappedData.Data;
                    }
                }
            }
        }
        catch (Exception ex) 
        { 
            // Log error if needed
        }
        return new List<HotelDto>();
    }
}

public class ApiResponse
{
    public List<HotelDto>? Data { get; set; }
}

public class HotelDto
{
    public string Id { get; set; } = "";
    public string BusinessID { get; set; } = "";
    public string BusinessName { get; set; } = "";
    public string FeatureImg { get; set; } = "";
    public string City { get; set; } = "";
    public string Address { get; set; } = "";
    public string Cno { get; set; } = "";
    public string Email { get; set; } = "";
    public string Url { get; set; } = "";
    public string DateCreated { get; set; } = "";
    public string BusinessTypeName { get; set; } = "";
    public string Country { get; set; } = "";
    public string Description { get; set; } = "";
}