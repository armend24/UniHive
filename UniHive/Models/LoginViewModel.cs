namespace UniHive.Models
{

    // ViewModel used to capture login form input and display errors to the user. 
    public class LoginViewModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string ErrorMessage { get; set; }
    }
}
