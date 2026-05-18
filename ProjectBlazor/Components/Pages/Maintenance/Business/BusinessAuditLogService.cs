using System.Text.Json;

namespace ProjectBlazor.Components.Pages.Maintenance.Business;

public class BusinessAuditLogService
{
    private readonly string _logBasePath = @"C:\Applications\AOPC\Logs\Business";

    public async Task LogAddAsync(string entityName, object data)
    {
        var logEntry = new
        {
            action = $"Add{entityName}",
            timestamp = DateTime.UtcNow,
            module = "Business",
            entityName,
            data
        };

        await WriteLogAsync($"Add{entityName}", logEntry);
    }

    public async Task LogUpdateAsync(string entityName, object beforeData, object afterData)
    {
        var logEntry = new
        {
            action = $"Edit{entityName}",
            timestamp = DateTime.UtcNow,
            module = "Business",
            entityName,
            before = beforeData,
            after = afterData
        };

        await WriteLogAsync($"Edit{entityName}", logEntry);
    }

    public async Task LogDeleteAsync(string entityName, object data)
    {
        var logEntry = new
        {
            action = $"Delete{entityName}",
            timestamp = DateTime.UtcNow,
            module = "Business",
            entityName,
            data
        };

        await WriteLogAsync($"Delete{entityName}", logEntry);
    }

    private async Task WriteLogAsync(string actionName, object logEntry)
    {
        try
        {
            Directory.CreateDirectory(_logBasePath);

            var timestamp = DateTime.UtcNow.ToString("yyyy-MM-dd-HHmmss");
            var fileName = $"{actionName}-{timestamp}.json";
            var filePath = Path.Combine(_logBasePath, fileName);

            var json = JsonSerializer.Serialize(logEntry, new JsonSerializerOptions { WriteIndented = true });
            await File.WriteAllTextAsync(filePath, json);
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Business audit log error: {ex.Message}");
        }
    }
}
