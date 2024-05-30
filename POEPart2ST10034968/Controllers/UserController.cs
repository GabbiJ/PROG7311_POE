using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using System.Data.SqlClient;

namespace POEPart2ST10034968.Controllers
{
    public class UserController : Controller
    {
        string constr = "Server=(LocalDB)\\MSSQLLocalDB;Database=PROG7311_POEPart2;TrustServerCertificate=true;Trusted_Connection=True;MultipleActiveResultSets=true";
        public IActionResult Register()
        {
            return View();
        }
        public IActionResult Login()
        {
            CurrentUser.u = new FUser();
            CurrentUser.SelectedFarmerUsername = null;

            return View();
        }

        //post method for Registering User, activated by clicking submit button on regitration page
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register(IFormCollection collection)
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
                //depending on if user selected is farmer or employee
                string selectedType = collection["ddUserType"];
                //checking farmer DB
                if (selectedType.Equals("Farmer"))
                {
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

                }
                //checking employee DB
                else if (selectedType.Equals("Employee"))
                {
                    string strSelect = $"SELECT * FROM Employee WHERE Username = '{collection["txtEmail"]}';";
                    using (SqlConnection con = new SqlConnection(constr))
                    {
                        con.Open();
                        SqlCommand cmdSelect = new SqlCommand(strSelect, con);
                        using (SqlDataReader r = cmdSelect.ExecuteReader())
                        {
                            while (r.Read())
                            {
                                newUser = new FUser(r.GetString(0), r.GetString(1), r.GetString(2), r.GetString(3), true);
                            }
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
                    if (selectedType.Equals("Farmer"))
                    {
                        string strInsert = $"INSERT INTO Farmer VALUES('{username}', '{pass}', '{name}', '{surname}');";
                        using (SqlConnection con = new SqlConnection(constr))
                        {
                            con.Open();
                            SqlCommand cmdInsert = new SqlCommand(strInsert, con);
                            cmdInsert.ExecuteNonQuery();
                        }
                    }
                    //in employee DB
                    else if (selectedType.Equals("Employee"))
                    {
                        string strInsert = $"INSERT INTO Employee VALUES('{username}', '{pass}', '{name}', '{surname}');";
                        using (SqlConnection con = new SqlConnection(constr))
                        {
                            con.Open();
                            SqlCommand cmdInsert = new SqlCommand(strInsert, con);
                            cmdInsert.ExecuteNonQuery();
                        }
                    }
                }
                else
                {
                    throw new Exception("This username is taken.");
                }
                return View("Login");

            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
                return View("Register");
            }
        }

        //post method for Logging in user, activated by clicking submit button on login page
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(IFormCollection collection)
        {
            try
            {
                //checking if all fields have been filled correctly
                if (collection["txtEmail"] == "" || collection["txtPass"] == "")
                {
                    throw new Exception("Please enter a username and password.");
                }

                FUser newUser = null;
                string username = collection["txtEmail"];
                string password = collection["txtPass"];
                //checking tables based on which user type user selected
                string selectedType = collection["ddUserType"];

                if (selectedType.Equals("Farmer"))
                {
                    using (SqlConnection con = new SqlConnection(constr))
                    {
                        //fetching user from the database that matches the inputted username
                        string strSelect = $"SELECT * FROM Farmer WHERE Username = '{username}';";
                        con.Open();
                        SqlCommand cmdSelect = new SqlCommand(strSelect, con);
                        //assigning student object out of fetched data
                        using (SqlDataReader r = cmdSelect.ExecuteReader())
                        {
                            while (r.Read())
                            {
                                newUser = new FUser(r.GetString(0), r.GetString(1), r.GetString(2), r.GetString(3), false);
                            }
                        }
                    }
                }
                else if (selectedType.Equals("Employee"))
                {
                    using (SqlConnection con = new SqlConnection(constr))
                    {
                        //fetching user from the database that matches the inputted username
                        string strSelect = $"SELECT * FROM Employee WHERE Username = '{username}';";
                        con.Open();
                        SqlCommand cmdSelect = new SqlCommand(strSelect, con);
                        //assigning user object out of fetched data
                        using (SqlDataReader r = cmdSelect.ExecuteReader())
                        {
                            while (r.Read())
                            {
                                newUser = new FUser(r.GetString(0), r.GetString(1), r.GetString(2), r.GetString(3), true);
                            }
                        }
                    }
                }

                //throw exception if no matching user found
                if (newUser == null)
                {
                    throw new Exception("Username or password is incorrect.");
                }

                //if username and password match log student in
                if(newUser.Username.Equals(username) && newUser.Password.Equals(password))
                {
                    using (SqlConnection con = new SqlConnection())
                    {
                        //assigning user object as static "currentUser" object
                        CurrentUser.u = newUser;
                        CurrentUser.u.IsLoggedIn = true;
                    }
                    //closing this window and going to homepage
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    throw new Exception("Username or password is incorrect.");
                }
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
                return View("Login");
            }
        }

        
    }
}
