namespace ProjectBlazor.Components.Pages.Maintenance.Vendor;

public record VendorItem(
    string Code,
    string Name,
    string ImageUrl,
    string BusinessType,
    string Location,
    string ContactNumber,
    string Email,
    string WebsiteUrl,
    string Description,
    string DateCreated,
    string Status);
