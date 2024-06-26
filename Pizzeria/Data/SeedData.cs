using Microsoft.EntityFrameworkCore;

namespace Pizzeria.Data
{
    public class SeedData
    {
        public static void Initialize (IServiceProvider serviceProvider) 
        {
            using (var context = new PizzeriaContext (serviceProvider.GetRequiredService<DbContextOptions<PizzeriaContext>>()))
            {
                /* if (context.User.Any())
                 {
                     return;
                 }*/

                context.User.ExecuteDelete();

                context.User.AddRange(
                    new Model.User ()
                    {
                        Email = "admin@gmail.com",
                        FirstName = "adminFirstName",
                        LastName = "adminSurname",
                        Password = "Qwerty1@",
                        Role = Model.RoleEnum.ADMIN
                    }

                );

                context.SaveChanges();
            }
        }
    }
}
