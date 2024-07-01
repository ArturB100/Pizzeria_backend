using Microsoft.EntityFrameworkCore;
using Pizzeria.Model;

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

                // context.User.ExecuteDelete();
                // context.Ingredient.ExecuteDelete();
                // context.Pizza.ExecuteDelete();

                context.User.AddRange(
                    new Model.User ()
                    {
                        Email = "admin@gmail.com",
                        FirstName = "adminFirstName",
                        LastName = "adminSurname",
                        Password = "Qwerty1@",
                        Phone = "123123123",
                        Role = Model.RoleEnum.ADMIN,
                        Address = new Model.Address ()
                        {
                            City = "Warszawa",
                            FirstLine = "dsa",
                            SecondLine = "dsa",
                            Zipcode = "123"
                        }
                    }

                );

                List<Ingredient> ingredients = new List<Ingredient>()
                {
                    new Model.Ingredient()
                    {
                        Name = "Name",
                        PriceForBig = 15.00M,
                        PriceForMedium = 10.00M,
                        PriceForSmall = 5.00M,
                    }
                };

                context.Ingredient.AddRange(
                    ingredients   
                );

                context.SaveChanges();

                context.Pizza.AddRange(
                    new Model.Pizza ()
                    {
                        CreatedByUser = false,
                        Ingredients = new List<Ingredient> { ingredients[0] },
                        Name = "hawajska",
                        
                    }
                );

                context.SaveChanges();
            }
        }
    }
}
