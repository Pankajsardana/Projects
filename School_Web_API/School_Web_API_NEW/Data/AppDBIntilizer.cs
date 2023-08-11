using Microsoft.AspNetCore.Identity;
using School_Web_API_NEW.Data.Helpers;

namespace School_Web_API_NEW.Data
{
    public class AppDBIntilizer
    {
        public static async Task SeedRolesToDB(IApplicationBuilder applicationBuilder) 
        {
            using (var servicesScope = applicationBuilder.ApplicationServices.CreateScope()) 
            {
                var rolemanager = servicesScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                if(!await rolemanager.RoleExistsAsync(UserRoles.Manager))
                {
                    await rolemanager.CreateAsync(new IdentityRole(UserRoles.Manager));


                }
                if (!await rolemanager.RoleExistsAsync(UserRoles.Student))
                {
                    await rolemanager.CreateAsync(new IdentityRole(UserRoles.Student));


                }


            }
        }
    }
}
