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

                context.OrderDetails.ExecuteDelete();
                context.PizzaOrder.ExecuteDelete();
                context.User.ExecuteDelete();
                context.Ingredient.ExecuteDelete();
                context.Pizza.ExecuteDelete();

                if (!context.User.Any())
                {
                    context.User.AddRange(
                   new Model.User()
                   {
                       Email = "admin@gmail.com",
                       FirstName = "adminFirstName",
                       LastName = "adminSurname",
                       Password = "Qwerty1@",
                       Phone = "123123123",
                       Role = Model.RoleEnum.ADMIN,
                       Address = new Model.Address()
                       {
                           City = "Warszawa",
                           FirstLine = "dsa",
                           SecondLine = "dsa",
                           Zipcode = "123"
                       }
                   }

               );
                }

                if (!context.Ingredient.Any())
                {
                    List<Ingredient> ingredients = new List<Ingredient>()
                    {
                        new Ingredient() { Name = "Ciasto", PriceForBig = 15.00M, PriceForMedium = 10.00M, PriceForSmall = 5.00M },
                        new Ingredient() { Name = "Ser", PriceForBig = 8.00M, PriceForMedium = 5.50M, PriceForSmall = 3.00M },
                        new Ingredient() { Name = "Szynka", PriceForBig = 12.00M, PriceForMedium = 8.00M, PriceForSmall = 4.00M },
                        new Ingredient() { Name = "Ananas", PriceForBig = 10.00M, PriceForMedium = 7.00M, PriceForSmall = 3.50M },
                        new Ingredient() { Name = "Pieczarki", PriceForBig = 9.00M, PriceForMedium = 6.00M, PriceForSmall = 3.00M },
                        new Ingredient() { Name = "Papryka", PriceForBig = 7.00M, PriceForMedium = 5.00M, PriceForSmall = 2.50M },
                        new Ingredient() { Name = "Kukurydza", PriceForBig = 6.00M, PriceForMedium = 4.00M, PriceForSmall = 2.00M },
                        new Ingredient() { Name = "Salami", PriceForBig = 11.00M, PriceForMedium = 7.50M, PriceForSmall = 4.00M },
                        new Ingredient() { Name = "Oliwki", PriceForBig = 8.50M, PriceForMedium = 6.00M, PriceForSmall = 3.00M },
                        new Ingredient() { Name = "Cebula", PriceForBig = 5.00M, PriceForMedium = 3.50M, PriceForSmall = 2.00M }
                    };

                    context.Ingredient.AddRange(ingredients);
                    context.SaveChanges();

                    List<Pizza> pizzas = new List<Pizza>()
                    {
                        new Pizza() { CreatedByUser = false, Ingredients = new List<Ingredient> { ingredients[0], ingredients[1], ingredients[2] }, Name = "Margarita" },
                        new Pizza() { CreatedByUser = false, Ingredients = new List<Ingredient> { ingredients[0], ingredients[1], ingredients[3], ingredients[4] }, Name = "Hawajska" },
                        new Pizza() { CreatedByUser = false, Ingredients = new List<Ingredient> { ingredients[0], ingredients[1], ingredients[5], ingredients[6] }, Name = "Vegetarian" },
                        new Pizza() { CreatedByUser = false, Ingredients = new List<Ingredient> { ingredients[0], ingredients[1], ingredients[7] }, Name = "Salami" },
                        new Pizza() { CreatedByUser = false, Ingredients = new List<Ingredient> { ingredients[0], ingredients[1], ingredients[2], ingredients[8] }, Name = "Capricciosa" },
                        new Pizza() { CreatedByUser = false, Ingredients = new List<Ingredient> { ingredients[0], ingredients[1], ingredients[4], ingredients[9] }, Name = "Fungi" },
                        new Pizza() { CreatedByUser = false, Ingredients = new List<Ingredient> { ingredients[0], ingredients[1], ingredients[6], ingredients[9] }, Name = "Mexicana" },
                        new Pizza() { CreatedByUser = false, Ingredients = new List<Ingredient> { ingredients[0], ingredients[1], ingredients[3], ingredients[7] }, Name = "Tropical" },
                        new Pizza() { CreatedByUser = false, Ingredients = new List<Ingredient> { ingredients[0], ingredients[1], ingredients[8], ingredients[5] }, Name = "Mediterranean" },
                        new Pizza() { CreatedByUser = false, Ingredients = new List<Ingredient> { ingredients[0], ingredients[1], ingredients[2], ingredients[4], ingredients[7] }, Name = "Deluxe" }
                    };

                    context.Pizza.AddRange(pizzas);

                    context.SaveChanges();


                }


            }
        }
    }
}
