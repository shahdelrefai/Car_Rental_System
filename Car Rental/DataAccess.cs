using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using CarRental.Models;

public class DataAccess
{
    // Connection string to connect to the SQL Server database
    private string connectionString = "Server=DESKTOP-BS4C66A;Database=car_rental_system;Trusted_Connection=True;TrustServerCertificate=True;Encrypt=False;";
    private SqlConnection? connection = null;
    private SqlCommand? command = null;

    public DataAccess()
    {
        connection = new SqlConnection(connectionString);
    }

    // Open a connection to the database
    public bool Connect()
    {
        try
        {
            connection?.Open();
            return true; // Return true if connection is successful
        }
        catch (SqlException e)
        {
            return false; // Return false if connection fails
        }
    }

    // Execute stored procedure to add a car
    public string AddCar(Car car)
    {
        try
        {
            command = new SqlCommand("addCar", connection)
            {
                CommandType = CommandType.StoredProcedure
            };

            command.Parameters.AddWithValue("@plate_id", car.PlateId);
            command.Parameters.AddWithValue("@model", car.Model);
            command.Parameters.AddWithValue("@year", car.Year);
            command.Parameters.AddWithValue("@status", car.Status);
            command.Parameters.AddWithValue("@office_id", car.OfficeId);

            command.ExecuteNonQuery();
            return "Car added successfully."; // Return success message
        }
        catch (Exception ex)
        {
            return $"Error adding car: {ex.Message}"; // Return error message
        }
    }

    // Execute stored procedure to get all cars
    public List<Car> GetAllCars()
    {
        List<Car> cars = new List<Car>();

        try
        {
            command = new SqlCommand("get_all_cars", connection)
            {
                CommandType = CommandType.StoredProcedure
            };

            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                Car car = new Car
                {
                    PlateId = reader["plate_id"].ToString(),
                    Model = reader["model"].ToString(),
                    Year = Convert.ToInt32(reader["year"]),
                    Status = Convert.ToInt32(reader["status"])
                };
                cars.Add(car);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error retrieving cars: {ex.Message}");
        }

        return cars; // Return the list of cars
    }

    // Execute stored procedure to get cars filtered by one or more criteria
    public List<Car> GetCarsFiltered(string? model = null, int? year = null, int? status = null)
    {
        List<Car> cars = new List<Car>();

        try
        {
            command = new SqlCommand("get_filtered_cars", connection)
            {
                CommandType = CommandType.StoredProcedure
            };

            if (!string.IsNullOrEmpty(model))
                command.Parameters.AddWithValue("@model", model);
            if (year.HasValue)
                command.Parameters.AddWithValue("@year", year.Value);
            if (status.HasValue)
                command.Parameters.AddWithValue("@status", status.Value);
            //if(customer.HasValue)
             //   command.Parameters.AddWithValue("@cust", customer.Value);

            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                Car car = new Car
                {
                    PlateId = reader["plate_id"].ToString(),
                    Model = reader["model"].ToString(),
                    Year = Convert.ToInt32(reader["year"]),
                    Status = Convert.ToInt32(reader["status"]),
                    OfficeId = Convert.ToInt32(reader["office_id"]),
                    //CustomerId = Convert.ToInt32(reader["customer_id"])
                };
                cars.Add(car);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error filtering cars: {ex.Message}");
        }

        return cars; // Return the list of filtered cars
    }

    public List<Car> GetCarsFilteredByResvCustomer(int customerId)
    {
        List<Car> cars = new List<Car>();

        try
        {
            command = new SqlCommand("get_cars_with_customer_id", connection)
            {
                CommandType = CommandType.StoredProcedure
            };

            command.Parameters.AddWithValue("@customer_id", customerId);

            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                Car car = new Car
                {
                    PlateId = reader["plate_id"].ToString(),
                    Model = reader["model"].ToString(),
                    Year = Convert.ToInt32(reader["year"]),
                    Status = Convert.ToInt32(reader["status"]),
                    OfficeId = Convert.ToInt32(reader["office_id"]),
                    CustomerId = Convert.ToInt32(reader["customer_id"])
                };
                cars.Add(car);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error filtering cars: {ex.Message}");
        }
        return cars;
    }

    public List<Car> GetCarsFilteredByResvCustomerAndModel(string model, int customerId)
    {
        List<Car> cars = new List<Car>();

        try
        {
            command = new SqlCommand("get_cars_with_model_and_customer_id", connection)
            {
                CommandType = CommandType.StoredProcedure
            };

            command.Parameters.AddWithValue("@customer_id", customerId);
            command.Parameters.AddWithValue("@mode", model);

            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                Car car = new Car
                {
                    PlateId = reader["plate_id"].ToString(),
                    Model = reader["model"].ToString(),
                    Year = Convert.ToInt32(reader["year"]),
                    Status = Convert.ToInt32(reader["status"]),
                    OfficeId = Convert.ToInt32(reader["office_id"]),
                    CustomerId = Convert.ToInt32(reader["customer_id"])
                };
                cars.Add(car);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error filtering cars: {ex.Message}");
        }
        return cars;
    }

    // Execute stored procedure for customer login
    public bool Login(string email, string password)
    {
        try
        {
            command = new SqlCommand("login", connection)
            {
                CommandType = CommandType.StoredProcedure
            };

            command.Parameters.AddWithValue("@p_email", email);
            command.Parameters.AddWithValue("@p_password", password);

            SqlDataReader reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                reader.Read();
                return true; // Customer exists and login is successful
            }
            else
            {
                return false; // Customer not found, login failed
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error during login: {ex.Message}");
            return false; // Return false if an error occurs
        }
    }

    // Execute stored procedure for customer registration
    public string RegisterCustomer(Customer customer)
    {
        try
        {
            command = new SqlCommand("sign_up", connection)
            {
                CommandType = CommandType.StoredProcedure
            };

            command.Parameters.AddWithValue("@p_name", customer.Name);
            command.Parameters.AddWithValue("@p_email", customer.Email); // Fixed typo in parameter name
            command.Parameters.AddWithValue("@p_password", customer.Password);
            command.Parameters.AddWithValue("@p_phone_num", customer.PhoneNum);

            command.ExecuteNonQuery();
            return "Customer registered successfully."; // Return success message
        }
        catch (Exception ex)
        {
            return $"Error registering customer: {ex.Message}"; // Return error message
        }
    }

    // Execute stored procedure to reserve a car
    public string ReserveCar(int customerId, string plateId, int payment, int cost, DateOnly startDate, DateOnly endDate)
    {
        try
        {
            command = new SqlCommand("reserve_car", connection)
            {
                CommandType = CommandType.StoredProcedure
            };

            command.Parameters.AddWithValue("@p_customer_id", customerId);
            command.Parameters.AddWithValue("@p_plate_id", plateId);
            command.Parameters.AddWithValue("@p_payment", payment);
            command.Parameters.AddWithValue("@p_cost", cost);
            command.Parameters.AddWithValue("@p_start_date", startDate);
            command.Parameters.AddWithValue("@p_end_date", endDate);

            command.ExecuteNonQuery();
            return "Car reserved successfully."; // Return success message
        }
        catch (Exception ex)
        {
            return $"Error reserving car: {ex.Message}"; // Return error message
        }
    }

    // Close the database connection
    public void CloseConnection()
    {
        try
        {
            connection?.Close();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error closing connection: {ex.Message}");
        }
    }
}
