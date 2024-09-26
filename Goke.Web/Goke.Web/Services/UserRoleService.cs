using Goke.Core.Entities;
using Goke.Web.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Text.Encodings.Web;


namespace Goke.Web.Services;

public class UserRoleService
{

    public static async Task CreateUserAddToRoles(UserManager<ApplicationUser> userManager, string email, string password, IEnumerable<string> roles, IEmailSender<ApplicationUser>? emailSender = null, string? receiverEmail = null)
    {
        if (await userManager.FindByEmailAsync(email) == null)
        {
            var user = new ApplicationUser
            {
                Email = email,
                UserName = email,
                EmailConfirmed = true,
                UserDetail = new()
                {
                    Name = email,
                    Email = email,
                }
            };
            await userManager.CreateAsync(user, password);

            if (roles != null)
            {
                await userManager.AddToRolesAsync(user, roles);
            }

            if (emailSender is not null && receiverEmail is not null)
            {
                await emailSender.SendPasswordResetCodeAsync(user, receiverEmail, HtmlEncoder.Default.Encode($"{user.UserName.ToUpper()[0..3]}|{password}"));
            }
        }
    }

    public static async Task CreateUserWithPinAsync(ApplicationDbContext db, UserManager<ApplicationUser> userManager, string username, string pin)
    {
        var user = new ApplicationUser
        {
            Email = username,
            UserName = username,
            EmailConfirmed = true,
            UserDetail = new UserDetail { Email = username }
        };

        var result = await userManager.CreateAsync(user, pin);
        if (result.Succeeded)
        {
            await userManager.AddToRoleAsync(user, "Users");
        }

        var entity = await db.Cards.Include(i => i.UserDetail).FirstAsync(f => f.Pin == pin);
        entity.UserDetail.Email = user.UserName;
        db.Cards.Update(entity);
        var row = await db.SaveChangesAsync();
    }

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
                if (card.UserDetailId is null)
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
                    result = card.UserDetail.Email == username;
                }
            }
        }

        return result;
    }

    
}