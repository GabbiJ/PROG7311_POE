namespace POEPart2ST10034968
{
    public class FUser
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public bool IsEmployee { get; set; }
        public bool IsLoggedIn { get; set; } = false;

        public FUser(string username, string password, string name, string surname, bool isEmployee)
        {
            Username = username;
            Password = password;
            Name = name;
            Surname = surname;
            IsEmployee = isEmployee;
        }

        public FUser()
        {
        }
    }
    
    public static class CurrentUser 
    {
        public static FUser u { get; set; } = new FUser();
        public static string SelectedFarmerUsername { get; set; }
    }
}
