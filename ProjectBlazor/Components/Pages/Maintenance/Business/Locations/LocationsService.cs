using System.Net.Http.Json;
using System.Text.Json;
using ProjectBlazor.Components.Pages.Maintenance.Business;

namespace ProjectBlazor.Components.Pages.Maintenance.Business.Locations;

public class LocationsService
{
    private readonly HttpClient _http;
    private readonly BusinessAuditLogService _auditLog;
    private static readonly JsonSerializerOptions _options = new() { PropertyNameCaseInsensitive = true };

    public LocationsService(HttpClient http)
    {
        _http = http;
        _auditLog = new BusinessAuditLogService();
    }

    public async Task<List<LocationDto>> GetLocationsAsync()
    {
        try
        {
            var response = await _http.GetAsync("api/ApiBusinessLoc/BusinessLocList");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                
                // Try to deserialize as direct array first
                try
                {
                    var data = JsonSerializer.Deserialize<List<LocationDto>>(content, _options);
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
        return new List<LocationDto>();
    }

    public async Task<bool> UpdateLocationAsync(int id, string city, string postalCode, string country, string businessLocID, DateTime dateCreated, LocationDto? beforeData = null)
    {
        try
        {
            var payload = new
            {
                id,
                country,
                city,
                postalCode,
                active = 1,
                dateCreated,
                businessLocID
            };

            var response = await _http.PostAsJsonAsync("api/ApiBusinessLoc/UpdateBusinessLoc", payload);
            Console.WriteLine($"UpdateLocation Response: {response.StatusCode}");
            if (response.IsSuccessStatusCode)
            {
                if (id == 0)
                {
                    await _auditLog.LogAddAsync("Location", payload);
                }
                else
                {
                    var auditData = beforeData ?? new LocationDto { Id = id, City = city, PostalCode = postalCode, Country = country, BusinessLocID = businessLocID };
                    await _auditLog.LogUpdateAsync("Location", auditData, payload);
                }
                return true;
            }
            return false;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Update Location error: {ex.Message}");
            return false;
        }
    }
    public async Task<bool> DeleteLocationAsync(int id, LocationDto? deletedData = null)
    {
        try
        {
            var payload = new { id };
            var response = await _http.PostAsJsonAsync("api/ApiBusinessLoc/DeleteBusinessLoc", payload);
            if (response.IsSuccessStatusCode)
            {
                var auditData = deletedData ?? new LocationDto { Id = id };
                await _auditLog.LogDeleteAsync("Location", auditData);
                return true;
            }
            return false;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Delete Location error: {ex.Message}");
            return false;
        }
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

public class ApiResponse
{
    public List<LocationDto>? Data { get; set; }
}