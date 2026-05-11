using System.Net.Http.Json;
using System.Text.Json;

namespace ProjectBlazor.Components.Pages.Maintenance.Registration.AdminRegistration;

public class AdminRegistrationService
{
    private readonly HttpClient _http;
    private static readonly JsonSerializerOptions _options = new() { PropertyNameCaseInsensitive = true };

    public AdminRegistrationService(HttpClient http)
    {
        _http = http;
    }

    public async Task<List<AdminRegistrationDto>> GetAdminListAsync()
    {
        try
        {
            var response = await _http.GetAsync("api/ApiRegister/AdminList");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Admin Registration API Response: {content.Substring(0, Math.Min(500, content.Length))}...");
                
                var data = JsonSerializer.Deserialize<List<AdminRegistrationDto>>(content, _options);
                return data ?? new List<AdminRegistrationDto>();
            }
            else
            {
                Console.WriteLine($"Admin Registration API error: {response.StatusCode}");
            }
        }
        catch (Exception ex) 
        { 
            Console.WriteLine($"Admin Registration API exception: {ex.Message}"); 
        }
        return new List<AdminRegistrationDto>();
    }
}

public class AdminRegistrationDto
{
    public int Id { get; set; }
    public string Fullname { get; set; } = "";
    public string Username { get; set; } = "";
    public string Fname { get; set; } = "";
    public string Lname { get; set; } = "";
    public string Email { get; set; } = "";
    public string Gender { get; set; } = "";
    public string EmployeeID { get; set; } = "";
    public string Position { get; set; } = "";
    public string Corporatename { get; set; } = "";
    public int UserTypeId { get; set; }
    public string UserType { get; set; } = "";
    public string DateCreated { get; set; } = "";
    public string CorporateID { get; set; } = "";
    public string MembershipID { get; set; } = "";
    public string PositionID { get; set; } = "";
    public string Status { get; set; } = "";
    public string FilePath { get; set; } = "";
    public string IsVIP { get; set; } = "";
}
