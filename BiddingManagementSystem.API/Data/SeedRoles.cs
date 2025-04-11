using Microsoft.AspNetCore.Identity;

namespace BiddingManagementSystem.API.Data
{
    public static class SeedRoles
    {
        public static async Task Initialize(IHost app)
        {
            using var scope = app.Services.CreateScope();
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            string[] roleNames = { "User", "ProcurementOfficer", "Bidder", "Evaluator" };

            foreach (var roleName in roleNames)
            {
                var roleExists = await roleManager.RoleExistsAsync(roleName);
                if (!roleExists)
                {
                    await roleManager.CreateAsync(new IdentityRole(roleName));
                }
            }
        }
    }
}
