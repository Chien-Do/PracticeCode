using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace OpenidConnect.Controllers
{
    public class AccountController : Controller
    {
        [AllowAnonymous]
        public async Task<IActionResult> SignInCallback()
        {
            var authenticateResult = await HttpContext.AuthenticateAsync("OpenID");
            // Handle the authentication result and sign in the user
            // ...

            return Accepted(authenticateResult);
        }

        [AllowAnonymous]
        public IActionResult Login()
        {
            var properties = new AuthenticationProperties
            {
                RedirectUri = Url.Action("SignInCallback")
            };

            return Challenge(properties, "OpenID");
        }
    }
}
