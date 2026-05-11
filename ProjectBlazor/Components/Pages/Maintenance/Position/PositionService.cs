using System.Net.Http.Json;
using System.Text.Json;

namespace ProjectBlazor.Components.Pages.Maintenance.Position;

public class PositionService
{
    private readonly HttpClient _http;
    private static readonly JsonSerializerOptions _options = new() { PropertyNameCaseInsensitive = true };

    public PositionService(HttpClient http)
    {
        _http = http;
    }

    public async Task<List<PositionDto>> GetPositionsAsync()
    {
        try
        {
            var response = await _http.GetAsync("api/ApiRegister/PositionList");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Position API Response: {content.Substring(0, Math.Min(500, content.Length))}...");
                
                var data = JsonSerializer.Deserialize<List<PositionDto>>(content, _options);
                return data ?? new List<PositionDto>();
            }
            else
            {
                Console.WriteLine($"Position API error: {response.StatusCode}");
            }
        }
        catch (Exception ex) 
        { 
            Console.WriteLine($"Position API exception: {ex.Message}"); 
        }
        return new List<PositionDto>();
    }
}

public class PositionDto
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public string Description { get; set; } = "";
    public string DateCreated { get; set; } = "";
    public int Status { get; set; }
    public string PositionID { get; set; } = "";
}
