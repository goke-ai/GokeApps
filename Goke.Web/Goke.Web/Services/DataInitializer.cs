
using Goke.Core;
using Goke.Core.Entities;
using Goke.Web;
using Goke.Web.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Text.Encodings.Web;

namespace Goke.Web.Services;

internal class DataInitializer
{
    internal static async Task Initialize(WebApplication app)
    {
        // seeding
        using (var scope = app.Services.CreateScope())
        {
            var serviceProvider = scope.ServiceProvider;
            var db = serviceProvider.GetRequiredService<ApplicationDbContext>();

            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            if (app.Environment.IsDevelopment())
            {
                db.Database.EnsureDeleted();
                db.Database.Migrate();

                // roles
                await SeedRoles(roleManager);

                // users
                await SeedUsers(userManager);
            }
            else
            {
                db.Database.Migrate();

                await SeedRoles(roleManager);
                await SeedAdmins(userManager);

            }
        }
    }

    static async Task SeedRoles(RoleManager<IdentityRole> roleManager)
    {
        string[] roles = ["SystemAdministrators", "Administrators", "Managers", "Officers", "Users"];

        foreach (var role in roles)
        {
            if (!await roleManager.RoleExistsAsync(role))
            {
                await roleManager.CreateAsync(new IdentityRole(role));
            }
        }
    }

    static async Task SeedUsers(UserManager<ApplicationUser> userManager)
    {
        await SeedAdmins(userManager);

        string managerEmail = "manager@ark.com";
        string managerPassword = "Secure3Pa$$word!";
        await UserRoleService.CreateUserAddToRoles(userManager, managerEmail, managerPassword, ["Managers"]);


        string officerEmail = "officer@ark.com";
        string officerPassword = "Secure2Pa$$word!";
        await UserRoleService.CreateUserAddToRoles(userManager, officerEmail, officerPassword, ["Officers"]);


        string userEmail = "user@ark.com";
        string userPassword = "Secure1Pa$$word!";
        await UserRoleService.CreateUserAddToRoles(userManager, userEmail, userPassword, ["Users"]);
    }

    public static async Task SeedAdmins(UserManager<ApplicationUser> userManager, IEmailSender<ApplicationUser>? emailSender = null)
    {
        string gokeEmail = "goke@ark.com";
        string gokePassword = emailSender is null ? "goke@ARK#246800" : Text.GeneratePin();
        string[] roles = ["Administrators", "SystemAdministrators"];
        var receiverEmail = "gokeladokun@gmail.com";
        await UserRoleService.CreateUserAddToRoles(userManager, gokeEmail, gokePassword, roles, emailSender, receiverEmail);

        string sysAdminEmail = "sysadmin@ark.com";
        string sysAdminPassword = emailSender is null ? "sysadmin@ARK#789" : Text.GeneratePin();
        await UserRoleService.CreateUserAddToRoles(userManager, sysAdminEmail, sysAdminPassword, roles, emailSender, "admin@evirtuallab.com");


        string adminEmail = "admin@ark.com";
        string adminPassword = emailSender is null ? "admin@ARK#135" : Text.GeneratePin();     
        await UserRoleService.CreateUserAddToRoles(userManager, adminEmail, adminPassword, ["Administrators"], emailSender, "admin@evirtuallab.com");
    }

    public static async Task SeedResetAdmins(UserManager<ApplicationUser> userManager, IEmailSender<ApplicationUser>? emailSender = null)
    {
        string gokeEmail = "goke@ark.com";
        var gokeUser = await userManager.FindByEmailAsync(gokeEmail);
        if (gokeUser != null)
        {
            await userManager.DeleteAsync(gokeUser);
        }

        string sysAdminEmail = "sysadmin@ark.com";
        var sysAdminUser = await userManager.FindByEmailAsync(sysAdminEmail);
        if (sysAdminUser != null)
        {
            await userManager.DeleteAsync(sysAdminUser);
        }

        string adminEmail = "admin@ark.com";
        var adminUser = await userManager.FindByEmailAsync(adminEmail);
        if (adminUser != null)
        {
            await userManager.DeleteAsync(adminUser);
        }

        await SeedAdmins(userManager, emailSender);
    }
}