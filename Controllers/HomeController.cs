using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication3.DAL;
using WebApplication3.Models;
namespace WebApplication3.Controllers
{
   
public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        // To create View of this Action result
        public ActionResult create()
        {
            return View();
        }

        // Specify the type of attribute i.e.
        // it will add the record to the database
        [HttpPost]
        public ActionResult create(Product model)
        {
            // To open a connection to the database
            using (var context = new AppDbContext()) // Ensure ApplicationDbContext is correctly defined in your project
            {
                // Cast the Products property to the correct type
                var products = context.Products as DbSet<Product>;
                if (products == null)
                {
                    throw new InvalidOperationException("The Products property is not configured correctly in AppDbContext.");
                }

                // Add data to the particular table
                products.Add(model);

                // Save the changes
                context.SaveChanges();
            }
            string message = "Created the record successfully";

            // To display the message on the screen
            // after the record is created successfully
            ViewBag.Message = message;

            // Write @Viewbag.Message in the created
            // view at the place where you want to
            // display the message
            return View();
        }
    }
}