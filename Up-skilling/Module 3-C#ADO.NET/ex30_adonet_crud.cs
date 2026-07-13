using System;
using Microsoft.Data.SqlClient;

class Program
{
    private static readonly string ConnectionString = "Server=localhost;Database=CivicPortalDB;Trusted_Connection=True;TrustServerCertificate=True;";

    static void Main()
    {
        Console.WriteLine("=== ADO.NET CRUD Pipeline Operations ===");
        
        try
        {
            InitializeSchema();
            InsertUserRecord("Liam O'Connor", "liam@metroportal.gov");
            QueryUserRecords();
        }
        catch (Exception ex)
        {
            Console.WriteLine("ADO.NET Operation Failed: " + ex.Message);
        }
    }

    static void InitializeSchema()
    {
        using var connection = new SqlConnection(ConnectionString);
        connection.Open();

        string createTableSql = @"
            IF OBJECT_ID('Users', 'U') IS NULL
            BEGIN
                CREATE TABLE Users (
                    UserId INT IDENTITY(1,1) PRIMARY KEY,
                    FullName NVARCHAR(100) NOT NULL,
                    Email NVARCHAR(100) NOT NULL UNIQUE
                )
            END";

        using var command = new SqlCommand(createTableSql, connection);
        command.ExecuteNonQuery();
        Console.WriteLine("Database Schema Verified.");
    }

    static void InsertUserRecord(string name, string email)
    {
        using var connection = new SqlConnection(ConnectionString);
        connection.Open();

        // Parameterized query prevents SQL Injection vulnerabilities
        string insertSql = "INSERT INTO Users (FullName, Email) VALUES (@Name, @Email)";
        
        using var command = new SqlCommand(insertSql, connection);
        command.Parameters.AddWithValue("@Name", name);
        command.Parameters.AddWithValue("@Email", email);

        int rowsAffected = command.ExecuteNonQuery();
        Console.WriteLine($"Successfully inserted {rowsAffected} row(s).");
    }

    static void QueryUserRecords()
    {
        using var connection = new SqlConnection(ConnectionString);
        connection.Open();

        string selectSql = "SELECT UserId, FullName, Email FROM Users";
        using var command = new SqlCommand(selectSql, connection);
        using SqlDataReader reader = command.ExecuteReader();

        Console.WriteLine("\n--- Roster Output ---");
        while (reader.Read())
        {
            int id = reader.GetInt32(0);
            string name = reader.GetString(1);
            string email = reader.GetString(2);
            Console.WriteLine($"ID: {id} | Name: {name} | Email: {email}");
        }
    }
}
