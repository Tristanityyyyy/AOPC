using System.Net.Http.Json;
using System.Text.Json;

namespace ProjectBlazor.Components.Pages.Maintenance.Corporate;

public class CorporateService
{
    private readonly HttpClient _http;
    private readonly CorporateAuditLogService _auditLog;
    private static readonly JsonSerializerOptions _options = new() { PropertyNameCaseInsensitive = true };

    public CorporateService(HttpClient http)
    {
        _http = http;
        _auditLog = new CorporateAuditLogService();
    }

    public async Task<List<CorporateDto>> GetCorporatesAsync()
    {
        try
        {
            var response = await _http.GetAsync("api/ApiCorporate/CompanyList");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var data = JsonSerializer.Deserialize<List<CorporateDto>>(content, _options);
                return data ?? new List<CorporateDto>();
            }
        }
        catch
        {
        }
        return new List<CorporateDto>();
    }

    public async Task<bool> UpdateCorporateAsync(int id, string corporateName, string address, string cNo, string emailAddress, int membershipID, int count, int vipCount, string companyID, DateTime dateUsed, DateTime dateEnded, DateTime dateCreated, CorporateDto? beforeData = null)
    {
        try
        {
            var payload = new
            {
                id,
                corporateName,
                address,
                cNo,
                emailAddress,
                status = 0,
                membershipID,
                count,
                vipCount,
                companyID,
                dateUsed,
                dateEnded,
                dateCreated
            };

            var response = await _http.PostAsJsonAsync("api/ApiCorporate/UpdateCorporate", payload);
            if (response.IsSuccessStatusCode)
            {
                if (id == 0)
                {
                    await _auditLog.LogAddAsync("Corporate", payload);
                }
                else
                {
                    var auditData = beforeData ?? new CorporateDto { Id = id, CorporateName = corporateName, Address = address, CNo = cNo, EmailAddress = emailAddress, CompanyID = companyID };
                    await _auditLog.LogUpdateAsync("Corporate", auditData, payload);
                }
                return true;
            }
            return false;
        }
        catch
        {
            return false;
        }
    }

    public async Task<bool> DeleteCorporateAsync(int id, CorporateDto? deletedData = null)
    {
        try
        {
            var payload = new { id };
            var response = await _http.PostAsJsonAsync("api/ApiCorporate/DeleteCorporate", payload);
            if (response.IsSuccessStatusCode)
            {
                var auditData = deletedData ?? new CorporateDto { Id = id };
                await _auditLog.LogDeleteAsync("Corporate", auditData);
                return true;
            }
            return false;
        }
        catch
        {
            return false;
        }
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