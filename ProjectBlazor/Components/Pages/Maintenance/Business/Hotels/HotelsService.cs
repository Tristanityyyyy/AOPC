using System.Net.Http.Json;
using System.Text.Json;
using ProjectBlazor.Components.Pages.Maintenance.Business;

namespace ProjectBlazor.Components.Pages.Maintenance.Business.Hotels;

public class HotelsService
{
    private readonly HttpClient _http;
    private readonly BusinessAuditLogService _auditLog;
    private static readonly JsonSerializerOptions _options = new() { PropertyNameCaseInsensitive = true };

    public HotelsService(HttpClient http)
    {
        _http = http;
        _auditLog = new BusinessAuditLogService();
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

    public async Task<bool> DeleteBusinessAsync(int id, HotelDto? deletedData = null)
    {
        try
        {
            var payload = new { id };
            var response = await _http.PostAsJsonAsync("api/ApiBusiness/DeleteBusiness", payload);
            if (response.IsSuccessStatusCode)
            {
                var auditData = deletedData ?? new HotelDto { Id = id.ToString() };
                await _auditLog.LogDeleteAsync("Hotel", auditData);
                return true;
            }
            return false;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Delete Business error: {ex.Message}");
            return false;
        }
    }

    public async Task<bool> SaveBusinessAsync(int id, string businessName, int typeId, int locationID, string description, string address, string cno, string email, string url, string services, string featureImg, string gallery, string map, string businessID, DateTime dateCreated, HotelDto? beforeData = null)
    {
        try
        {
            var payload = new
            {
                id,
                businessName,
                typeId,
                locationID,
                description,
                address,
                cno,
                email,
                url,
                services,
                featureImg,
                gallery,
                active = 1,
                filePath = featureImg,
                map,
                businessID,
                dateCreated
            };

            var response = await _http.PostAsJsonAsync("api/ApiBusiness/SaveBusiness", payload);
            if (response.IsSuccessStatusCode)
            {
                if (id == 0)
                {
                    await _auditLog.LogAddAsync("Hotel", payload);
                }
                else
                {
                    var auditData = beforeData ?? new HotelDto { Id = id.ToString(), BusinessName = businessName, Description = description, Address = address, Cno = cno, Email = email, Url = url, FeatureImg = featureImg, BusinessID = businessID };
                    await _auditLog.LogUpdateAsync("Hotel", auditData, payload);
                }
                return true;
            }
            return false;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Save Business error: {ex.Message}");
            return false;
        }
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