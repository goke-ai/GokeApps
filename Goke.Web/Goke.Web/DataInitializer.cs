
using Goke.Core;
using Goke.Web.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Text.Encodings.Web;

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
        await SeedAdmins(userManager);

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

    private static async Task SeedAdmins(UserManager<ApplicationUser> userManager)
    {
        string sysAdminEmail = "sysadmin@ark.com";
        string sysAdminPassword = "admin@ARK#11";
        if (await userManager.FindByEmailAsync(sysAdminEmail) == null)
        {
            var sysAdminUser = new ApplicationUser { Email = sysAdminEmail, UserName = sysAdminEmail, EmailConfirmed = true };
            await userManager.CreateAsync(sysAdminUser, sysAdminPassword);
            await userManager.AddToRolesAsync(sysAdminUser, ["Administrators", "SystemAdministrators"]);
        }

        string adminEmail = "admin@ark.com";
        string adminPassword = "admin@ARK#1";
        if (await userManager.FindByEmailAsync(adminEmail) == null)
        {
            var adminUser = new ApplicationUser { Email = adminEmail, UserName = adminEmail, EmailConfirmed = true };
            await userManager.CreateAsync(adminUser, adminPassword);
            await userManager.AddToRolesAsync(adminUser, ["Administrators"]);
        }
    }

    public static async Task SeedAdmins(UserManager<ApplicationUser> userManager, IEmailSender<ApplicationUser>? emailSender = null)
    {
        string gokeEmail = "goke@ark.com";
        string gokePassword = emailSender is null ? "goke@ARK#246800" : Text.GeneratePin();
        if (await userManager.FindByEmailAsync(gokeEmail) == null)
        {
            var gokeUser = new ApplicationUser { Email = gokeEmail, UserName = gokeEmail, EmailConfirmed = true };
            await userManager.CreateAsync(gokeUser, gokePassword);
            await userManager.AddToRolesAsync(gokeUser, ["Administrators", "SystemAdministrators"]);

            if (emailSender != null)
            {
                await emailSender.SendPasswordResetCodeAsync(gokeUser, "gokeladokun@gmail.com", HtmlEncoder.Default.Encode($"G|{gokePassword}"));
            }
        }

        string sysAdminEmail = "sysadmin@ark.com";
        string sysAdminPassword = emailSender is null ? "sysadmin@ARK#789" : Text.GeneratePin();
        if (await userManager.FindByEmailAsync(sysAdminEmail) == null)
        {
            var sysAdminUser = new ApplicationUser { Email = sysAdminEmail, UserName = sysAdminEmail, EmailConfirmed = true };
            await userManager.CreateAsync(sysAdminUser, sysAdminPassword);
            await userManager.AddToRolesAsync(sysAdminUser, ["Administrators", "SystemAdministrators"]);

            if (emailSender != null)
            {
                await emailSender.SendPasswordResetCodeAsync(sysAdminUser, "admin@evirtuallab.com", HtmlEncoder.Default.Encode($"1|{sysAdminPassword}"));
            }
        }

        string adminEmail = "admin@ark.com";
        string adminPassword = emailSender is null ? "admin@ARK#135" : Text.GeneratePin();
        if (await userManager.FindByEmailAsync(adminEmail) == null)
        {
            var adminUser = new ApplicationUser { Email = adminEmail, UserName = adminEmail, EmailConfirmed = true };
            await userManager.CreateAsync(adminUser, adminPassword);
            await userManager.AddToRolesAsync(adminUser, ["Administrators"]);

            if (emailSender != null)
            {
                await emailSender.SendPasswordResetCodeAsync(adminUser, "admin@evirtuallab.com", HtmlEncoder.Default.Encode($"2|{adminPassword}"));
            }
        }
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

    /*
    public static async Task<bool> ReadyToLoginWithPinAsync(IConfiguration configuration, ApplicationDbContext db, UserManager<ApplicationUser> userManager, string username, string pin)
    {
        bool result = false;

        var emails = configuration["Emails"];
        var users = emails?.Split(",", StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);

        if (users?.Any(a => a == username) == true)
        {
            return result = true;
        }

        var card = await db.Cards.FirstOrDefaultAsync(f => f.Pin == pin);

        // card exist
        if (card != null)
        {
            if (card.To > DateTime.UtcNow)
            {
                // new card
                if (string.IsNullOrWhiteSpace(card.PinUser))
                {
                    // existing user
                    var user = await userManager.FindByNameAsync(username);
                    if (user != null)
                    {
                        await userManager.DeleteAsync(user);
                    }

                    await CreateUserWithPinAsync(db, userManager, username, pin);

                    result = true;
                }
                else
                {
                    result = card.PinUser == username;
                }
            }
        }

        return result;
    }


    private static async Task CreateUserWithPinAsync(ApplicationDbContext db, UserManager<ApplicationUser> userManager, string username, string pin)
    {
        var user = new ApplicationUser { Email = username, UserName = username, EmailConfirmed = true };
        var result = await userManager.CreateAsync(user, pin);
        if (result.Succeeded)
        {
            await userManager.AddToRoleAsync(user, "Users");
        }

        var entity = await db.Cards.FirstAsync(f => f.Pin == pin);
        entity.PinUser = user.UserName;
        db.Cards.Update(entity);
        var row = await db.SaveChangesAsync();
    }
    */

}