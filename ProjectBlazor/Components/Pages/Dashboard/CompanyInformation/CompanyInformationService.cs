using System.Net.Http.Json;
using System.Text.Json;

namespace ProjectBlazor.Components.Pages.Dashboard.CompanyInformation;

public class CompanyInformationService
{
    private readonly HttpClient _http;
    private static readonly JsonSerializerOptions _options = new() { PropertyNameCaseInsensitive = true };

    public CompanyInformationService(HttpClient http)
    {
        _http = http;
    }

    public async Task<List<CompanyInformationDto>> GetCompanyListAsync()
    {
        try
        {
            var response = await _http.GetAsync("api/ApiCorporateListing/CorporateUserCountAll");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Company Information API Response: {content.Substring(0, Math.Min(500, content.Length))}...");
                
                var data = JsonSerializer.Deserialize<List<CompanyInformationDto>>(content, _options);
                return data ?? new List<CompanyInformationDto>();
            }
            else
            {
                Console.WriteLine($"Company Information API error: {response.StatusCode}");
            }
        }
        catch (Exception ex) 
        { 
            Console.WriteLine($"Company Information API exception: {ex.Message}"); 
        }
        return new List<CompanyInformationDto>();
    }
}

public class CompanyInformationDto
{
    public string CorporateName { get; set; } = "";
    public string Registered { get; set; } = "";
    public string Unregistered { get; set; } = "";
    public string UnregisteredVIP { get; set; } = "";
    public string RegisteredVIP { get; set; } = "";
    public string TotalVIP { get; set; } = "";
    public string RemainingVip { get; set; } = "";
    public string UserCount { get; set; } = "";
    public string TotalUser { get; set; } = "";
}
