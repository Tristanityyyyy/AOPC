namespace ProjectBlazor.Components.Pages.Maintenance.Corporate;

public record CorporateItem(
    string Code, 
    string Name, 
    string Address, 
    string ContactNumber, 
    string EmailAddress,
    string MembershipTier, 
    int UserCount, 
    int VipCount, 
    string MembershipDescription, 
    string DateCreated, 
    string DateStart, 
    string DateEnd);