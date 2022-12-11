﻿using BaseProject.Identity.Entities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Strategy.DesignPattern.WebApp.Entities;
using Strategy.DesignPattern.WebApp.Enums;
using System.Security.Claims;

namespace Strategy.DesignPattern.WebApp.Controllers
{
    [Authorize]
    public class SettingsController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public SettingsController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public IActionResult Index()
        {
            Settings settings = new();

            if (User.Claims.Where(x => x.Type == Settings.claimDatabaseType).FirstOrDefault() != null)
                settings.DatabaseType = (EDatabaseType)int.Parse(User.Claims.First(X => X.Type == Settings.claimDatabaseType).Value);
            else
                settings.DatabaseType = settings.GetDefaultDataBaseType;
            
            
            return View(settings);
        }

        [HttpPost]
        public async Task<IActionResult> ChangeDatabase(int databaseType)
        {
            var user = await _userManager.FindByNameAsync(User.Identity?.Name);

            var newClaim = new Claim(Settings.claimDatabaseType, databaseType.ToString());

            var claims = await _userManager.GetClaimsAsync(user);

            var hasDatabaseType = claims.FirstOrDefault(x => x.Type == Settings.claimDatabaseType);

            if (hasDatabaseType != null)
                await _userManager.ReplaceClaimAsync(user, hasDatabaseType, newClaim);
            else
                await _userManager.AddClaimAsync(user, newClaim);

            await _signInManager.SignOutAsync();

            var authenticateResult = await HttpContext.AuthenticateAsync();

            await _signInManager.SignInAsync(user, authenticateResult.Properties);

            return RedirectToAction(nameof(Index));
        }
    }
}
