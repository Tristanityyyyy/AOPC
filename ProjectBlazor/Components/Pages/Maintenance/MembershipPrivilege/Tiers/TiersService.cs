using System.Text.Json;

namespace ProjectBlazor.Components.Pages.Maintenance.MembershipPrivilege.Tiers;

public class TiersService
{
    private readonly HttpClient _http;
    private static readonly JsonSerializerOptions _options = new() { PropertyNameCaseInsensitive = true };

    public TiersService(HttpClient http)
    {
        _http = http;
    }

    public async Task<List<MembershipTierDto>> GetTiersAsync()
    {
        try
        {
            var response = await _http.GetAsync("api/ApiMembership/MembershipList");
            
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Tiers API Response: {content.Substring(0, Math.Min(500, content.Length))}...");
                
                // Try direct array first
                try
                {
                    var data = JsonSerializer.Deserialize<List<MembershipTierDto>>(content, _options);
                    if (data != null && data.Count > 0)
                    {
                        Console.WriteLine($"Tiers: Successfully parsed {data.Count} items");
                        return data;
                    }
                }
                catch
                {
                    // Try wrapped response
                    var wrappedData = JsonSerializer.Deserialize<ApiResponse>(content, _options);
                    if (wrappedData?.Data != null)
                    {
                        Console.WriteLine($"Tiers: Successfully parsed {wrappedData.Data.Count} items from wrapped response");
                        return wrappedData.Data;
                    }
                }
            }
            else
            {
                Console.WriteLine($"Tiers API error: {response.StatusCode}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Tiers error: {ex.Message}");
        }
        return new List<MembershipTierDto>();
    }
}

public class MembershipTierDto
{
    public string Id { get; set; } = "";
    public string MembershipName { get; set; } = "";
    public string Description { get; set; } = "";
    public string MembershipCard { get; set; } = "";
    public string VipCard { get; set; } = "";
    public string QrFrame { get; set; } = "";
    public string VipBadge { get; set; } = "";
    public string DateStarted { get; set; } = "";
    public string DateCreated { get; set; } = "";
    public string MembershipID { get; set; } = "";
    public int UserCount { get; set; }
    public int VipCount { get; set; }
    public string TextCardColor { get; set; } = "";
    public string DateEnded { get; set; } = "";
    public string Status { get; set; } = "";
    public string Country { get; set; } = "";
}

public class ApiResponse
{
    public List<MembershipTierDto>? Data { get; set; }
}
