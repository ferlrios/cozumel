using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Cozumel.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Cozumel.Areas.Identity.Pages.Account.Manage
{
    public partial class IndexModel : PageModel
    {
        private readonly UserManager<MyUser> _userManager;
        private readonly SignInManager<MyUser> _signInManager;

        public IndexModel(
            UserManager<MyUser> userManager,
            SignInManager<MyUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [Display(Name = "Nombre de usuario")]
        public string Username { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Display(Name = "Link a tu Instagram")]
            public string InstagramURL { get; set; }
            [Display(Name = "Link a tu Youtube")]
            public string YoutubeURL { get; set; }
            [Display(Name = "Link a tu Spotify")]
            public string SpotifyURL { get; set; }
            [Phone]
            [Display(Name = "Número de telefono")]
            public string PhoneNumber { get; set; }
            [Display(Name = "Foto de perfil")]
            public byte[] ProfileImg { get; set; }
        }
        private async Task LoadAsync(MyUser user)
        {
            var userName = await _userManager.GetUserNameAsync(user);
            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);
            var profileImg = user.ProfileImg;
            var youtubeUrl = user.YoutubeURL;
            var spotifyUrl = user.SpotifyURL;
            var instagramUrl = user.InstagramURL;

            Username = userName;

            Input = new InputModel
            {
                PhoneNumber = phoneNumber,
                ProfileImg = profileImg,
                YoutubeURL = youtubeUrl,
                SpotifyURL = spotifyUrl,
                InstagramURL = instagramUrl,
            };
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            await LoadAsync(user);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"No se encontró usuario con el ID '{_userManager.GetUserId(User)}'.");
            }

            if (!ModelState.IsValid)
            {
                await LoadAsync(user);
                return Page();
            }

            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);
            if (Input.PhoneNumber != phoneNumber)
            {
                var setPhoneResult = await _userManager.SetPhoneNumberAsync(user, Input.PhoneNumber);
                if (!setPhoneResult.Succeeded)
                {
                    StatusMessage = "Unexpected error when trying to set phone number.";
                    return RedirectToPage();
                }
            }

            var youtubeUrl = user.YoutubeURL;
            var spotifyUrl = user.SpotifyURL;
            var instagramUrl = user.InstagramURL;
            if (Input.YoutubeURL != youtubeUrl)
            {
                user.YoutubeURL = Input.YoutubeURL;
                await _userManager.UpdateAsync(user);
            }
            if (Input.SpotifyURL != spotifyUrl)
            {
                user.SpotifyURL = Input.SpotifyURL;
                await _userManager.UpdateAsync(user);
            }
            if (Input.InstagramURL != instagramUrl)
            {
                user.InstagramURL = Input.InstagramURL;
                await _userManager.UpdateAsync(user);
            }
            //Guarda la foto de perfil
            if (Request.Form.Files.Count > 0)
            {
                IFormFile file = Request.Form.Files.FirstOrDefault();
                using (var dataStream = new MemoryStream())
                {
                    await file.CopyToAsync(dataStream);
                    user.ProfileImg = dataStream.ToArray();
                }
                await _userManager.UpdateAsync(user);
            }

            await _signInManager.RefreshSignInAsync(user);
            StatusMessage = "Tu perfil ha sido actualizado";
            return RedirectToPage();
        }
    }
}
