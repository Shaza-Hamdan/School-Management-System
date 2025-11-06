using System.ComponentModel.DataAnnotations;

namespace Trial.DTO
{
    public record EmailRequest(
        string To,
        string Subject,
        string Body

    );

    public record AssignRoleRequest(
        string UserEmail,
        string NewRole
    );

    public record CreateAdminRequest(
        string UserEmail,
        string Password,
        string UserName

    );
    public record CreateNewAccount(
        string UserName,
        string Password,
        [Required(ErrorMessage = "Email is required.")]
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$",
         ErrorMessage = "Email format must include a valid domain (e.g. user@mail.com).")]
        string Email,
        DateOnly? DateOfBirth,
        string Address,
        string PhoneNumber,
        string Role
    );

    public record LoginRequest(
        string Email,
        string Password
    );

    public record PasswordResetRequest(
     string Email
    );
    public record ResetPasswordRequest
    (
        string Email,
        string Token,
        string NewPassword
    );



}
