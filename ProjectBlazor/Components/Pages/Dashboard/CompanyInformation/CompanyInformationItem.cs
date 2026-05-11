namespace ProjectBlazor.Components.Pages.Dashboard.CompanyInformation;

public record CompanyInformationItem(
    string CompanyName,
    int RegisteredUsers,
    int UnregisteredUsers,
    int UnregisteredVipCount,
    int VipRegistered,
    int VipUserCount,
    int UserCount,
    int TotalUsers,
    int RemainingVipCount = 0
);
