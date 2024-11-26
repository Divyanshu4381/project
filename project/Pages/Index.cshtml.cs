using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MySql.Data.MySqlClient;

namespace project.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }
        [BindProperty]
        public string Email { get; set; }

        [BindProperty]
        public string Password { get; set; }

        public IActionResult OnPostLogin()
        {
            // Database connection string
            string connectionString = "server=127.0.0.1;database=CompanyDB;user=root;password=password;";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                // Query to check if the user exists
                string query = "SELECT COUNT(*) FROM User WHERE Email = @email AND Password = @password";
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@email", Email);
                    command.Parameters.AddWithValue("@password", Password); // Store passwords securely in production!

                    int userCount = Convert.ToInt32(command.ExecuteScalar());
                    if (userCount > 0)
                    {
                        // Redirect to the dashboard or authenticated page
                        return RedirectToPage("/welcome");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Invalid login credentials");
                        return Page();
                    }
                }
            }
        }
        public void OnGet()
        {

        }
    }
}
