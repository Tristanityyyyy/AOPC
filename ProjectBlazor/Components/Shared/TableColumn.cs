namespace ProjectBlazor.Components.Shared;
using Microsoft.AspNetCore.Components;

public class TableColumn<T>
{
    public string Header { get; set; } = string.Empty;
    public string? Width { get; set; } = "auto";
    public string? SortField { get; set; }

    // This makes the RenderCell required and safer
    [Parameter]
    public RenderFragment<T> RenderCell { get; set; } = default!;

    // Optional: For simple text columns without custom template
    public Func<T, object?>? Property { get; set; }
}