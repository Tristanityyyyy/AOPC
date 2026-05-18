using System.Net.Http.Json;
using System.Text.Json;

namespace ProjectBlazor.Components.Pages.Maintenance.Position;

public class PositionService
{
    private readonly HttpClient _http;
    private readonly PositionAuditLogService _auditLog;
    private static readonly JsonSerializerOptions _options = new() { PropertyNameCaseInsensitive = true };

    public PositionService(HttpClient http)
    {
        _http = http;
        _auditLog = new PositionAuditLogService();
    }

    public async Task<List<PositionDto>> GetPositionsAsync()
    {
        try
        {
            var response = await _http.GetAsync("api/ApiRegister/PositionList");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var data = JsonSerializer.Deserialize<List<PositionDto>>(content, _options);
                return data ?? new List<PositionDto>();
            }
        }
        catch
        {
        }
        return new List<PositionDto>();
    }

    public async Task<bool> SavePositionAsync(int id, string name, string description, string positionID, DateTime dateCreated, PositionDto? beforeData = null)
    {
        try
        {
            var payload = new
            {
                id,
                name,
                description
            };

            var response = await _http.PostAsJsonAsync("api/ApiRegister/SavePosition", payload);
            if (response.IsSuccessStatusCode)
            {
                if (id == 0)
                {
                    await _auditLog.LogAddAsync("Position", payload);
                }
                else
                {
                    var auditData = beforeData ?? new PositionDto { Id = id, Name = name, Description = description, PositionID = positionID };
                    await _auditLog.LogUpdateAsync("Position", auditData, payload);
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

    public async Task<bool> DeletePositionAsync(int id, PositionDto? deletedData = null)
    {
        try
        {
            var payload = new { id };
            var response = await _http.PostAsJsonAsync("api/ApiRegister/DeletePosition", payload);
            if (response.IsSuccessStatusCode)
            {
                var auditData = deletedData ?? new PositionDto { Id = id };
                await _auditLog.LogDeleteAsync("Position", auditData);
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

public class PositionDto
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public string Description { get; set; } = "";
    public string DateCreated { get; set; } = "";
    public int Status { get; set; }
    public string PositionID { get; set; } = "";
}
