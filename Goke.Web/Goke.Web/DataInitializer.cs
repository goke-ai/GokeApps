
using Goke.Web.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

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
        string sysAdminEmail = "sysadmin@ark.com";
        string sysAdminPassword = "admin@ARK#11";
        if (await userManager.FindByEmailAsync(sysAdminEmail) == null)
        {
            var sysAdminUser = new ApplicationUser { Email = sysAdminEmail, UserName = sysAdminEmail, EmailConfirmed = true };
            await userManager.CreateAsync(sysAdminUser, sysAdminPassword);
            await userManager.AddToRolesAsync(sysAdminUser, ["Administrators","SystemAdministrators"]);
        }

        string adminEmail = "admin@ark.com";
        string adminPassword = "admin@ARK#1";
        if (await userManager.FindByEmailAsync(adminEmail) == null)
        {
            var adminUser = new ApplicationUser { Email = adminEmail, UserName = adminEmail, EmailConfirmed = true };
            await userManager.CreateAsync(adminUser, adminPassword);
            await userManager.AddToRolesAsync(adminUser, ["Administrators"]);
        }

        string managerEmail = "manager@ark.com";
        string managerPassword = "Secure3Pa$$word!";
        if (await userManager.FindByEmailAsync(managerEmail) == null)
        {
            var managerUser = new ApplicationUser { Email = managerEmail, UserName = managerEmail, EmailConfirmed = true };
            await userManager.CreateAsync(managerUser, managerPassword);
            await userManager.AddToRoleAsync(managerUser, "Managers");
        }

        string officerEmail = "officer@ark.com";
        string officerPassword = "Secure2Pa$$word!";
        if (await userManager.FindByEmailAsync(officerEmail) == null)
        {
            var officerUser = new ApplicationUser { Email = officerEmail, UserName = officerEmail, EmailConfirmed = true };
            await userManager.CreateAsync(officerUser, officerPassword);
            await userManager.AddToRoleAsync(officerUser, "Officers");
        }

        string userEmail = "user@ark.com";
        string userPassword = "Secure1Pa$$word!";
        if (await userManager.FindByEmailAsync(userEmail) == null)
        {
            var userUser = new ApplicationUser { Email = userEmail, UserName = userEmail, EmailConfirmed = true };
            await userManager.CreateAsync(userUser, userPassword);
            await userManager.AddToRoleAsync(userUser, "Users");
        }
    }

}