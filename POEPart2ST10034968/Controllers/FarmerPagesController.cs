using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Xml.Linq;

namespace POEPart2ST10034968.Controllers
{
    public class FarmerPagesController : Controller
    {
        string constr = "Server=(LocalDB)\\MSSQLLocalDB;Database=PROG7311_POEPart2;TrustServerCertificate=true;Trusted_Connection=True;MultipleActiveResultSets=true";
        public IActionResult AddProduct()
        {
            return View();
        }

        //Post method for saving a product to DB when "submit" button is pressed on addProduct page
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddProduct(IFormCollection collection)
        {
            try
            {
                //checking all fields are filled 
                if (collection["txtName"] == "")
                {
                    throw new Exception("Please enter all values correctly.");
                }

                string tempName = collection["txtName"];
                string tempProdtype = collection["ddProdType"];
                DateOnly tempProdDate = DateOnly.Parse(collection["dpProdDate"]);
                string farmerName = CurrentUser.u.Username;

                
                using (SqlConnection con = new SqlConnection(constr))
                {
                    string strInsert = $"INSERT INTO Product VALUES('{tempName}', '{tempProdtype}', '{tempProdDate.ToString("yyyy-MM-dd")}', '{farmerName}');";
                    con.Open();
                    SqlCommand cmdInsert = new SqlCommand(strInsert, con);
                    cmdInsert.ExecuteNonQuery();
                }

                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
                return View("AddProduct");
            }
            
        }

    }
}
