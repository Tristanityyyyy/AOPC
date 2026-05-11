namespace ProjectBlazor.Components.Pages.Maintenance.Business.Hotels;

public record HotelItem(
    int Id,
    string Code, 
    string Name, 
    string ImageUrl, 
    string Location, 
    string Address, 
    string ContactNo, 
    string Email, 
    string Url, 
    string Description, 
    string DateCreated,
    int TypeId = 0,
    int LocationId = 0,
    string Services = "",
    string Gallery = "",
    string Map = "");