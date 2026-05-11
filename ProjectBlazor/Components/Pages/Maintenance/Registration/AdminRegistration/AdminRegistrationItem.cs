namespace ProjectBlazor.Components.Pages.Maintenance.Registration.AdminRegistration;

public record AdminRegistrationItem(
    int Id,
    string EmployeeID,
    string Fullname,
    string Username,
    string Fname,
    string Lname,
    string Email,
    string Gender,
    string Position,
    string Corporatename,
    int UserTypeId,
    string UserType,
    DateTime DateCreated,
    string CorporateID,
    string MembershipID,
    string PositionID,
    string Status,
    string FilePath,
    bool IsVIP);
