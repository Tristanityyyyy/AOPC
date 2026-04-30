namespace ProjectBlazor.Components.Pages.Maintenance.MembershipPrivilege.Tiers;

public record MembershipTierItem(
    string Code,
    string Name,
    string Description,
    string DateStarted,
    string DateEnded,
    int UserCount,
    int VipCount,
    string DateCreated)
{
    public string? TextCardColor { get; set; }
    public string? MembershipCountry { get; set; }
}