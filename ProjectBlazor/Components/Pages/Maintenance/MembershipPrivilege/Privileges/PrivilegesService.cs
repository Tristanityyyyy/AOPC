using System.Text.Json;

namespace ProjectBlazor.Components.Pages.Maintenance.MembershipPrivilege.Privileges;

public class PrivilegesService
{
    private readonly HttpClient _http;
    private static readonly JsonSerializerOptions _options = new() { PropertyNameCaseInsensitive = true };

    public PrivilegesService(HttpClient http)
    {
        _http = http;
    }

    public async Task<List<PrivilegeDto>> GetPrivilegesAsync()
    {
        try
        {
            var response = await _http.GetAsync("api/ApiPrivilege/PrivilegeList");
            
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Privileges API Response: {content.Substring(0, Math.Min(500, content.Length))}...");
                
                // Try direct array first
                try
                {
                    var data = JsonSerializer.Deserialize<List<PrivilegeDto>>(content, _options);
                    if (data != null && data.Count > 0)
                    {
                        Console.WriteLine($"Privileges: Successfully parsed {data.Count} items");
                        return data;
                    }
                }
                catch
                {
                    // Try wrapped response
                    var wrappedData = JsonSerializer.Deserialize<ApiResponse>(content, _options);
                    if (wrappedData?.Data != null)
                    {
                        Console.WriteLine($"Privileges: Successfully parsed {wrappedData.Data.Count} items from wrapped response");
                        return wrappedData.Data;
                    }
                }
            }
            else
            {
                Console.WriteLine($"Privileges API error: {response.StatusCode}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Privileges error: {ex.Message}");
        }
        return new List<PrivilegeDto>();
    }
}

public class PrivilegeDto
{
    public int Id { get; set; }
    public string Title { get; set; } = "";
    public string Description { get; set; } = "";
    public string Validity { get; set; } = "";
    public int? NoExpiry { get; set; }
    public string ImgUrl { get; set; } = "";
    public string PrivilegeID { get; set; } = "";
    public int? IsVIP { get; set; }
    public string? FeatureImg { get; set; }
    public string Tmc { get; set; } = "";
    public string? VendorID { get; set; }
    public string? VendorName { get; set; }
    public string BusinessTypeName { get; set; } = "";
    public string BusinessTypeID { get; set; } = "";
    public string Status { get; set; } = "";
    public string DateCreated { get; set; } = "";
    public string Mechanics { get; set; } = "";
    public string? Active { get; set; }
}

public class ApiResponse
{
    public List<PrivilegeDto>? Data { get; set; }
}
