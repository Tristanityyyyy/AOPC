namespace ProjectBlazor.Components.Shared;
using Microsoft.AspNetCore.Components;

public partial class TableColumn<T>
{
    public string Header { get; set; } = string.Empty;
    public string? Width { get; set; } = "auto";
    public string? SortField { get; set; }

    [Parameter]
    public RenderFragment<T> RenderCell { get; set; } = default!;

    [Parameter]
    public RenderFragment<T>? CellTemplate { get; set; }

    public Func<T, object?>? Property { get; set; }
}