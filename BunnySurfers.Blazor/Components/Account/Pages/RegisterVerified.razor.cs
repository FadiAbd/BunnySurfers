using BunnySurfers.API.DTOs;
using BunnySurfers.Blazor.Data;
using BunnySurfers.Blazor.Services;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using System.ComponentModel.DataAnnotations;
using System.Text.Encodings.Web;
using System.Text;

namespace BunnySurfers.Blazor.Components.Account.Pages
{
    public partial class RegisterVerified
    {
        [Inject]
        private UserManager<ApplicationUser> UserManager { get; set; } = null!;
        [Inject]
        private IUserStore<ApplicationUser> UserStore { get; set; } = null!;
        [Inject]
        private SignInManager<ApplicationUser> SignInManager { get; set; } = null!;
        [Inject]
        private IEmailSender<ApplicationUser> EmailSender { get; set; } = null!;
        [Inject]
        private ILogger<Register> Logger { get; set; } = null!;
        [Inject]
        private NavigationManager NavigationManager { get; set; } = null!;
        [Inject]
        private IdentityRedirectManager RedirectManager { get; set; } = null!;
        [Inject]
        private IUserService UserService { get; set; } = null!;

        private readonly List<IdentityError> identityErrors = [];

        [SupplyParameterFromForm]
        private InputModel Input { get; set; } = new();

        [SupplyParameterFromQuery]
        private string? ReturnUrl { get; set; }

        private string? Message => identityErrors.Count == 0 ? null : $"Error: {string.Join(", ", identityErrors.Select(error => error.Description))}";

        public async Task RegisterUser(EditContext editContext)
        {
            var succeeded = await Input.RetrieveUser(UserService, identityErrors);
            if (!succeeded)
                return;

            var user = CreateUser();
            user.UserId = Input.UserDTO!.UserId;
            user.Name = Input.UserDTO!.Name;
            user.Role = Input.UserDTO!.Role;

            await UserStore.SetUserNameAsync(user, Input.Username, CancellationToken.None);
            var emailStore = GetEmailStore();
            await emailStore.SetEmailAsync(user, Input.Username, CancellationToken.None);
            var result = await UserManager.CreateAsync(user, Input.Password);

            if (!result.Succeeded)
            {
                identityErrors.AddRange(result.Errors);
                return;
            }

            Logger.LogInformation("User created a new account with password.");

            var userId = await UserManager.GetUserIdAsync(user);
            var code = await UserManager.GenerateEmailConfirmationTokenAsync(user);
            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
            var callbackUrl = NavigationManager.GetUriWithQueryParameters(
                NavigationManager.ToAbsoluteUri("Account/ConfirmEmail").AbsoluteUri,
                new Dictionary<string, object?> { ["userId"] = userId, ["code"] = code, ["returnUrl"] = ReturnUrl });

            await EmailSender.SendConfirmationLinkAsync(user, Input.Username, HtmlEncoder.Default.Encode(callbackUrl));

            if (UserManager.Options.SignIn.RequireConfirmedAccount)
            {
                RedirectManager.RedirectTo(
                    "Account/RegisterConfirmation",
                    new() { ["email"] = Input.Username, ["returnUrl"] = ReturnUrl });
            }

            await SignInManager.SignInAsync(user, isPersistent: false);
            RedirectManager.RedirectTo(ReturnUrl);
        }

        private ApplicationUser CreateUser()
        {
            try
            {
                return Activator.CreateInstance<ApplicationUser>();
            }
            catch
            {
                throw new InvalidOperationException($"Can't create an instance of '{nameof(ApplicationUser)}'. " +
                    $"Ensure that '{nameof(ApplicationUser)}' is not an abstract class and has a parameterless constructor.");
            }
        }

        private IUserEmailStore<ApplicationUser> GetEmailStore()
        {
            if (!UserManager.SupportsUserEmail)
            {
                throw new NotSupportedException("The default UI requires a user store with email support.");
            }
            return (IUserEmailStore<ApplicationUser>)UserStore;
        }

        private sealed class InputModel
        {
            [Required]
            [Display(Name = "Name")]
            public string Name { get; set; } = "";

            [Required]
            [Display(Name = "Email")]
            [EmailAddress]
            public string Email { get; set; } = "";

            [Required]
            [Display(Name = "Username")]
            public string Username { get; set; } = "";

            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; } = "";

            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; } = "";

            public UserGetDTO? UserDTO { get; set; } = null!;

            public async Task<bool> RetrieveUser(IUserService userService, List<IdentityError> identityErrors)
            {
                UserDTO = await userService.FindUserByEmail(Email);
                if (UserDTO is null)
                {
                    identityErrors.Add(new()
                    {
                        Code = nameof(RetrieveUser),
                        Description = $"Could not find the user with Email='{Email}' in the API database."
                    });
                    return false;
                }
                return true;
            }
        }
    }
}
