namespace ProjectBlazor.Components.Pages.Maintenance.Business.Hotels;

public record HotelItem(
    string Code, 
    string Name, 
    string ImageUrl, 
    string Location, 
    string Address, 
    string ContactNo, 
    string Email, 
    string Url, 
    string Description, 
    string DateCreated);