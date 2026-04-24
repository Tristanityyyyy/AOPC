namespace ProjectBlazor.Components.Pages.Maintenance.Business.Types;

public record BusinessTypeItem(
    string Code, 
    string Name, 
    string ImageUrl, 
    string Promo, 
    string Description, 
    string DateCreated);