namespace ProjectBlazor.Components.Pages.Maintenance.Business.Types;

public record BusinessTypeItem(
    int Id,
    string Code, 
    string Name, 
    string ImageUrl, 
    string Promo, 
    string Description, 
    string DateCreated);