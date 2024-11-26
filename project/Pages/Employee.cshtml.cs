using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MySql.Data.MySqlClient;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Data;
namespace project.Pages
{
    public class EmployeeModel : PageModel
    {
        private readonly IConfiguration _configuration;
        public EmployeeModel(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [BindProperty]
        public Employee Employee { get; set; } = new Employee();
        public List<Employee> Employees { get; set; } = new List<Employee>();

        public void OnGet()
        {
            LoadEmployees();
        }

        public void OnPostCreate()
        {
            using var connection = new MySqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            connection.Open();
            var query = "INSERT INTO Employees (Name, Age, Address, Salary, Department) VALUES (@Name, @Age, @Address, @Salary, @Department)";
            using var command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@Name", Employee.Name);
            command.Parameters.AddWithValue("@Age", Employee.Age);
            command.Parameters.AddWithValue("@Address", Employee.Address);
            command.Parameters.AddWithValue("@Salary", Employee.Salary);
            command.Parameters.AddWithValue("@Department", Employee.Department);
            command.ExecuteNonQuery();
            LoadEmployees();
        }

        public void OnPostUpdate()
        {
            using var connection = new MySqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            connection.Open();
            var query = "UPDATE Employees SET Name=@Name, Age=@Age, Address=@Address, Salary=@Salary, Department=@Department WHERE Emp_ID=@Emp_ID";
            using var command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@Emp_ID", Employee.Emp_ID);
            command.Parameters.AddWithValue("@Name", Employee.Name);
            command.Parameters.AddWithValue("@Age", Employee.Age);
            command.Parameters.AddWithValue("@Address", Employee.Address);
            command.Parameters.AddWithValue("@Salary", Employee.Salary);
            command.Parameters.AddWithValue("@Department", Employee.Department);
            command.ExecuteNonQuery();
            LoadEmployees();
        }

        public void OnGetEdit(int id)
        {
            using var connection = new MySqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            connection.Open();
            var query = "SELECT * FROM Employees WHERE Emp_ID=@Emp_ID";
            using var command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@Emp_ID", id);
            using var reader = command.ExecuteReader();
            if (reader.Read())
            {
                Employee.Emp_ID = reader.GetInt32("Emp_ID");
                Employee.Name = reader.GetString("Name");
                Employee.Age = reader.GetInt32("Age");
                Employee.Address = reader.GetString("Address");
                Employee.Salary = reader.GetDecimal("Salary");
                Employee.Department = reader.GetString("Department");
            }
            LoadEmployees();
        }

        public void OnGetDelete(int id)
        {
            using var connection = new MySqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            connection.Open();
            var query = "DELETE FROM Employees WHERE Emp_ID=@Emp_ID";
            using var command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@Emp_ID", id);
            command.ExecuteNonQuery();
            LoadEmployees();
        }

        private void LoadEmployees()
        {
            Employees.Clear();
            using var connection = new MySqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            connection.Open();
            var query = "SELECT * FROM Employees";
            using var command = new MySqlCommand(query, connection);
            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                Employees.Add(new Employee
                {
                    Emp_ID = reader.GetInt32("Emp_ID"),
                    Name = reader.GetString("Name"),
                    Age = reader.GetInt32("Age"),
                    Address = reader.GetString("Address"),
                    Salary = reader.GetDecimal("Salary"),
                    Department = reader.GetString("Department")
                });
            }
        }
    }
}
public class Employee
{
    public int Emp_ID { get; set; }
    public string Name { get; set; }
    public int Age { get; set; }
    public string Address { get; set; }
    public decimal Salary { get; set; }
    public string Department { get; set; }
}

