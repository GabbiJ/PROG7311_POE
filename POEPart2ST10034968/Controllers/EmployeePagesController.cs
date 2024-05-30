using Microsoft.AspNetCore.Mvc;
using System.Collections.ObjectModel;
using System.Data.SqlClient;

namespace POEPart2ST10034968.Controllers
{
    public class EmployeePagesController : Controller
    {
        string constr = "Server=(LocalDB)\\MSSQLLocalDB;Database=PROG7311_POEPart2;TrustServerCertificate=true;Trusted_Connection=True;MultipleActiveResultSets=true";

        public IActionResult AddFarmer()
        {
            return View();
        }

        public IActionResult SelectFarmer()
        {
            //making a list of all the farmers in the database
            List<FUser> allFarmers = new List<FUser>();
              
            string strSelect = $"SELECT * FROM Farmer;";
            using (SqlConnection con = new SqlConnection(constr))
            {
                con.Open();
                SqlCommand cmdSelect = new SqlCommand(strSelect, con);
                using (SqlDataReader r = cmdSelect.ExecuteReader())
                {
                    while (r.Read())
                    {
                        allFarmers.Add( new FUser(r.GetString(0), r.GetString(1), r.GetString(2), r.GetString(3), false));
                    }
                }
            }
            return View(allFarmers);
        }

        public IActionResult ViewFarmerProducts()
        {
            //fetching farmer info from DB
            FUser tempFarmer = new FUser();

            using (SqlConnection con = new SqlConnection(constr))
            {
                //fetching user from the database that matches the inputted username
                string strSelect = $"SELECT * FROM Farmer WHERE Username = '{CurrentUser.SelectedFarmerUsername}';";
                con.Open();
                SqlCommand cmdSelect = new SqlCommand(strSelect, con);
                //assigning student object out of fetched data
                using (SqlDataReader r = cmdSelect.ExecuteReader())
                {
                    while (r.Read())
                    {
                        tempFarmer = new FUser(r.GetString(0), r.GetString(1), r.GetString(2), r.GetString(3), false);
                    }
                }
            }

            //fetching all products under farmer in DB and storing in a list
            List<Product> tempProducts = new List<Product>();

            using (SqlConnection con = new SqlConnection(constr))
            {
                //fetching user from the database that matches the inputted username
                string strSelect = $"SELECT * FROM Product WHERE FarmerUName = '{CurrentUser.SelectedFarmerUsername}';";
                con.Open();
                SqlCommand cmdSelect = new SqlCommand(strSelect, con);
                //assigning student object out of fetched data
                using (SqlDataReader r = cmdSelect.ExecuteReader())
                {
                    while (r.Read())
                    {
                        tempProducts.Add(new Product(r.GetInt32(0).ToString(), r.GetString(1), r.GetString(2), DateOnly.FromDateTime(r.GetDateTime(3)), r.GetString(4)));
                    }
                }
            }

            //making anonymous type to store farmer and list info
            var viewModel = new 
            {
                farmer = tempFarmer,
                allProducts = tempProducts
            };
            return View(viewModel);
        }

        //post method for allowing adding a farmer when "submit" ubtton is clicked on AddFarmer page
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddFarmer(IFormCollection collection)
        {
            try
            {
                //checking all fields have valid values
                if (collection["txtEmail"] == "" || collection["txtPass"] == "" || collection["txtFName"] == "" || collection["txtSurname"] == "")
                {
                    throw new Exception("Please ensure all fields are filled");
                }
                //checking if username is taken
                FUser newUser = null;

                //fetching user from the database that matches the inputted username
                string strSelect = $"SELECT * FROM Farmer WHERE Username = '{collection["txtEmail"]}';";
                using (SqlConnection con = new SqlConnection(constr))
                {
                    con.Open();
                    SqlCommand cmdSelect = new SqlCommand(strSelect, con);
                    using (SqlDataReader r = cmdSelect.ExecuteReader())
                    {
                        while (r.Read())
                        {
                            newUser = new FUser(r.GetString(0), r.GetString(1), r.GetString(2), r.GetString(3), false);
                        }
                    }
                }
                //registers user if username is not taken
                if (newUser == null)
                {
                    //fetching info to insert into DB
                    string username = collection["txtEmail"];
                    string pass = collection["txtPass"];
                    string name = collection["txtFName"];
                    string surname = collection["txtSurname"];
                    //in farmer DB
                    string strInsert = $"INSERT INTO Farmer VALUES('{username}', '{pass}', '{name}', '{surname}');";
                    using (SqlConnection con = new SqlConnection(constr))
                    {
                        con.Open();
                        SqlCommand cmdInsert = new SqlCommand(strInsert, con);
                        cmdInsert.ExecuteNonQuery();
                    }
                }
                else
                {
                    throw new Exception("This username is taken.");
                }
                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
                return View("AddFarmer");
            }
        }

        //post method for handling what happens when a farmer button is clicked on SelectFarmer page
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SelectFarmer(IFormCollection collection)
        {
            //getting username of farmer from button clicked
            CurrentUser.SelectedFarmerUsername = collection["btnFarmer"];

            return RedirectToAction("ViewFarmerProducts", "EmployeePages");
        }

        //post method for filtering by date range
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ViewFarmerProductsByDate(IFormCollection collection)
        {
            //fetching farmer info from DB
            FUser tempFarmer = new FUser();

            using (SqlConnection con = new SqlConnection(constr))
            {

                //fetching user from the database that matches the inputted username
                string strSelect = $"SELECT * FROM Farmer WHERE Username = '{CurrentUser.SelectedFarmerUsername}';";
                con.Open();
                SqlCommand cmdSelect = new SqlCommand(strSelect, con);
                //assigning student object out of fetched data
                using (SqlDataReader r = cmdSelect.ExecuteReader())
                {
                    while (r.Read())
                    {
                        tempFarmer = new FUser(r.GetString(0), r.GetString(1), r.GetString(2), r.GetString(3), false);
                    }
                }
            }

            //fetching all products under farmer in DB and storing in a list 
            List<Product> tempProducts = new List<Product>();
            DateOnly startDate = DateOnly.Parse(collection["dpStart"]);
            DateOnly endDate = DateOnly.Parse(collection["dpEnd"]);

            using (SqlConnection con = new SqlConnection(constr))
            {
                //fetching user from the database that matches the inputted username
                string strSelect = $"SELECT * FROM Product WHERE FarmerUName = '{CurrentUser.SelectedFarmerUsername}' AND ProdDate BETWEEN '{startDate.ToString("yyyy-MM-dd")}' AND '{endDate.ToString("yyyy-MM-dd")}';";
                con.Open();
                SqlCommand cmdSelect = new SqlCommand(strSelect, con);
                //assigning student object out of fetched data
                using (SqlDataReader r = cmdSelect.ExecuteReader())
                {
                    while (r.Read())
                    {
                        tempProducts.Add(new Product(r.GetInt32(0).ToString(), r.GetString(1), r.GetString(2), DateOnly.FromDateTime(r.GetDateTime(3)), r.GetString(4)));
                    }
                }
            }

            //making anonymous type to store farmer and list info
            var viewModel = new
            {
                farmer = tempFarmer,
                allProducts = tempProducts
            };
            return View("ViewFarmerProducts", viewModel);
        }


        //post method for filtering by product type
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ViewFarmerProductsByType(IFormCollection collection)
        {

            //fetching farmer info from DB
            FUser tempFarmer = new FUser();

            using (SqlConnection con = new SqlConnection(constr))
            {

                //fetching user from the database that matches the inputted username
                string strSelect = $"SELECT * FROM Farmer WHERE Username = '{CurrentUser.SelectedFarmerUsername}';";
                con.Open();
                SqlCommand cmdSelect = new SqlCommand(strSelect, con);
                //assigning student object out of fetched data
                using (SqlDataReader r = cmdSelect.ExecuteReader())
                {
                    while (r.Read())
                    {
                        tempFarmer = new FUser(r.GetString(0), r.GetString(1), r.GetString(2), r.GetString(3), false);
                    }
                }
            }

            //fetching all products under farmer in DB and storing in a list 
            List<Product> tempProducts = new List<Product>();
            string prodType = collection["ddProdType"];

            using (SqlConnection con = new SqlConnection(constr))
            {
                //fetching user from the database that matches the inputted username
                string strSelect = $"SELECT * FROM Product WHERE FarmerUName = '{CurrentUser.SelectedFarmerUsername}' AND ProdType = '{prodType}';";
                con.Open();
                SqlCommand cmdSelect = new SqlCommand(strSelect, con);
                //assigning student object out of fetched data
                using (SqlDataReader r = cmdSelect.ExecuteReader())
                {
                    while (r.Read())
                    {
                        tempProducts.Add(new Product(r.GetInt32(0).ToString(), r.GetString(1), r.GetString(2), DateOnly.FromDateTime(r.GetDateTime(3)), r.GetString(4)));
                    }
                }
            }

            //making anonymous type to store farmer and list info
            var viewModel = new
            {
                farmer = tempFarmer,
                allProducts = tempProducts
            };
            return View("ViewFarmerProducts", viewModel);
        }
    }
}
