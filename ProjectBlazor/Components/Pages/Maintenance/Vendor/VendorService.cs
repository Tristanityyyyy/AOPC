using System.Net.Http.Json;
using System.Text.Json;

namespace ProjectBlazor.Components.Pages.Maintenance.Vendor;

public class VendorService
{
    private readonly HttpClient _http;
    private static readonly JsonSerializerOptions _options = new() { PropertyNameCaseInsensitive = true };

    public VendorService(HttpClient http)
    {
        _http = http;
    }

    public async Task<List<VendorDto>> GetVendorsAsync()
    {
        try
        {
            var response = await _http.GetAsync("api/ApiVendor/VendorList");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Vendor API Response: {content.Substring(0, Math.Min(500, content.Length))}...");
                
                var data = JsonSerializer.Deserialize<List<VendorDto>>(content, _options);
                return data ?? new List<VendorDto>();
            }
            else
            {
                Console.WriteLine($"Vendor API error: {response.StatusCode}");
            }
        }
        catch (Exception ex) 
        { 
            Console.WriteLine($"Vendor API exception: {ex.Message}"); 
        }
        return new List<VendorDto>();
    }
}

public class VendorDto
{
    public string Id { get; set; } = "";
    public string VendorName { get; set; } = "";
    public string VendorID { get; set; } = "";
    public string? BusinessName { get; set; }
    public string? BusinessId { get; set; }
    public string Description { get; set; } = "";
    public string Services { get; set; } = "";
    public string? BusinessTypeName { get; set; }
    public string? Country { get; set; }
    public string? City { get; set; }
    public string? PostalCode { get; set; }
    public string WebsiteUrl { get; set; } = "";
    public string FeatureImg { get; set; } = "";
    public string Gallery { get; set; } = "";
    public string Cno { get; set; } = "";
    public string Email { get; set; } = "";
    public string VideoUrl { get; set; } = "";
    public string VrUrl { get; set; } = "";
    public string DateCreated { get; set; } = "";
    public string Status { get; set; } = "";
    public int StatusId { get; set; }
    public string? Location { get; set; }
    public string? OfferingDesc { get; set; }
    public string? PromoDesc { get; set; }
    public string FileUrl { get; set; } = "";
    public string Map { get; set; } = "";
    public string VendorLogo { get; set; } = "";
    public string Address { get; set; } = "";
    public string? Vendorlocation { get; set; }
    public string? Bid { get; set; }
    public string? BtypeID { get; set; }
}
