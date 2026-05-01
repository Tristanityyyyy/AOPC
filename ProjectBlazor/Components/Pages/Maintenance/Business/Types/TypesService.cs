using System.Net.Http.Json;
using System.Text.Json;

namespace ProjectBlazor.Components.Pages.Maintenance.Business.Types;

public class TypesService
{
    private readonly HttpClient _http;
    private static readonly JsonSerializerOptions _options = new() { PropertyNameCaseInsensitive = true };

    public TypesService(HttpClient http)
    {
        _http = http;
    }

    public async Task<List<BusinessTypeDto>> GetBusinessTypesAsync()
    {
        try
        {
            var response = await _http.GetAsync("api/ApiBusinessType/BusinessTypeListv2");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Types API Response: {content.Substring(0, Math.Min(500, content.Length))}...");
                
                // Try direct array first
                try
                {
                    var data = JsonSerializer.Deserialize<List<BusinessTypeDto>>(content, _options);
                    if (data != null && data.Count > 0)
                    {
                        Console.WriteLine($"Types: Successfully parsed {data.Count} items");
                        return data;
                    }
                }
                catch
                {
                    // Try wrapped response
                    var wrappedData = JsonSerializer.Deserialize<ApiResponse>(content, _options);
                    if (wrappedData?.Data != null)
                    {
                        Console.WriteLine($"Types: Successfully parsed {wrappedData.Data.Count} items from wrapped response");
                        return wrappedData.Data;
                    }
                }
            }
            else
            {
                Console.WriteLine($"Types API error: {response.StatusCode}");
            }
        }
        catch (Exception ex) 
        { 
            Console.WriteLine($"Types error: {ex.Message}");
        }
        return new List<BusinessTypeDto>();
    }
}

public class BusinessTypeDto
{
    public int Id { get; set; }
    public string BusinessTypeID { get; set; } = "";
    public string BusinessTypeName { get; set; } = "";
    public string Description { get; set; } = "";
    public string PromoText { get; set; } = "";
    public string ImgURL { get; set; } = "";
    public string DateCreated { get; set; } = "";
    public string Status { get; set; } = "";
}

public class ApiResponse
{
    public List<BusinessTypeDto>? Data { get; set; }
}