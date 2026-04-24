namespace ProjectBlazor.Components.Pages.Maintenance.MembershipPrivilege.Privileges;

public record PrivilegeItem(
    string Code,
    string Name,
    string ImageUrl,
    string Description,
    string Validity,
    string NoExpiry,
    string BusinessTypeName,
    string DateCreated,
    string TermsHtml = "",
    string MechanicsHtml = "");
