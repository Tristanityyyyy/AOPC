using System.Net.Http.Json;
using System.Text.Json;

namespace ProjectBlazor.Components.Pages.Maintenance.Corporate;

public class CorporateService
{
    private readonly HttpClient _http;
    private static readonly JsonSerializerOptions _options = new() { PropertyNameCaseInsensitive = true };

    public CorporateService(HttpClient http)
    {
        _http = http;
    }

    public async Task<List<CorporateDto>> GetCorporatesAsync()
    {
        try
        {
            var response = await _http.GetAsync("api/ApiCorporate/CompanyList");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Corporate API Response: {content.Substring(0, Math.Min(500, content.Length))}...");
                
                var data = JsonSerializer.Deserialize<List<CorporateDto>>(content, _options);
                return data ?? new List<CorporateDto>();
            }
            else
            {
                Console.WriteLine($"Corporate API error: {response.StatusCode}");
            }
        }
        catch (Exception ex) 
        { 
            Console.WriteLine($"Corporate API exception: {ex.Message}"); 
        }
        return new List<CorporateDto>();
    }
}

public class CorporateDto
{
    public int Id { get; set; }
    public string CorporateName { get; set; } = "";
    public string CompanyID { get; set; } = "";
    public string Address { get; set; } = "";
    public string CNo { get; set; } = "";
    public string EmailAddress { get; set; } = "";
    public string Status { get; set; } = "";
    public string DateCreated { get; set; } = "";
    public string UserCount { get; set; } = "";
    public string VipCount { get; set; } = "";
    public string Tier { get; set; } = "";
    public string Description { get; set; } = "";
    public string Memid { get; set; } = "";
    public string? Status_ID { get; set; }
    public string DateUsed { get; set; } = "";
    public string DateEnded { get; set; } = "";
}