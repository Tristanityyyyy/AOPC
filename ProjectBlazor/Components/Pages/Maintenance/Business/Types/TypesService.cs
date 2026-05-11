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
                
                // Try direct array first
                try
                {
                    var data = JsonSerializer.Deserialize<List<BusinessTypeDto>>(content, _options);
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
        return new List<BusinessTypeDto>();
    }

    public async Task<bool> UpdateBusinessTypeAsync(int id, string businessTypeName, string description, string imgURL, string promoText, string businessTypeID, DateTime dateCreated)
    {
        try
        {
            var payload = new
            {
                id,
                businessTypeName,
                description,
                imgURL,
                promoText,
                status = 1,
                isVIP = 0,
                businessTypeID,
                dateCreated
            };

            var response = await _http.PostAsJsonAsync("api/ApiBusinessType/UpdateBusinessType", payload);
            return response.IsSuccessStatusCode;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Update Business Type error: {ex.Message}");
            return false;
        }
    }

    public async Task<bool> DeleteBusinessTypeAsync(int id)
    {
        try
        {
            var payload = new { id };
            var response = await _http.PostAsJsonAsync("api/ApiBusinessType/DeleteBusinessType", payload);
            return response.IsSuccessStatusCode;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Delete Business Type error: {ex.Message}");
            return false;
        }
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