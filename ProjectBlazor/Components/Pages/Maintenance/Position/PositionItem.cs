namespace ProjectBlazor.Components.Pages.Maintenance.Position;

public record PositionItem(
    int Id,
    string PositionID,
    string Name,
    string Description,
    DateTime DateCreated,
    int Status);
