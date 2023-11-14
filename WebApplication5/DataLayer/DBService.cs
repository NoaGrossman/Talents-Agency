using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Web.Services;
using System.Web.UI.HtmlControls;
using WebApplication5.Models;

namespace WebApplication5
{
    public class DBService
    {
        public string connectionString;
        public DBService()
        {
            connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DB"].ConnectionString;
        }
        [WebMethod]
        public int GetTalentsCount()
        {
            int talentsCount = 0;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    // Define the SQL query to retrieve data from the database
                    string query = "SELECT COUNT(*) AS NumberOfTalents FROM talents";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        // Execute the query and get the result
                        talentsCount = (int)command.ExecuteScalar();
                    }
                }
                catch (Exception ex)
                {
                    // Handle connection or database-related errors.
                    // You might want to log the error or handle it differently.
                    throw;
                }
            }

            return talentsCount;
        }

        [WebMethod]
        public List<Talent> GetKTalents(int k, int curPage)
        {
            List<Talent> talents = new List<Talent>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    // Define the SQL query to retrieve data from the database
                    string query = "SELECT t.talent_id, t.talent_name, t.talent_email, t.talent_dob, t.talent_age, s.value AS talent_specialization FROM talents t INNER JOIN specializations s ON t.talent_specialization = s.id " +
                        "ORDER BY t.talent_name, t.talent_specialization " +
                        "OFFSET @Offset ROWS " +
                        "FETCH NEXT @Fetch ROWS ONLY;";

                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@Offset", k * (curPage - 1));
                        cmd.Parameters.AddWithValue("@Fetch", k);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            // Check if there are rows to read
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    Talent talent = new Talent
                                    {
                                        ID = reader.GetInt32(reader.GetOrdinal("talent_id")),
                                        Name = reader.GetString(reader.GetOrdinal("talent_name")),
                                        DOB = reader.GetDateTime(reader.GetOrdinal("talent_dob")),
                                        Email = reader.GetString(reader.GetOrdinal("talent_email")),
                                        Specialization = (Specialization)Enum.Parse(typeof(Specialization), reader.GetString(reader.GetOrdinal("talent_specialization"))),
                                        Age = reader.GetInt32(reader.GetOrdinal("talent_Age"))
                                    };

                                    talents.Add(talent);
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Handle connection or database-related errors.
                    // You might want to log the error or handle it differently.
                    throw;
                }
            }

            return talents;
        }
        [WebMethod]
        public List<Talent> Search(string inputText)
        {
            List<Talent> talents = new List<Talent>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    // Define the SQL query to retrieve data from the database
                    string query = "SELECT * FROM talents WHERE talent_name LIKE @inputText OR talent_email LIKE @inputText";

                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        // Use parameters to avoid SQL injection
                        cmd.Parameters.AddWithValue("@inputText", "%" + inputText + "%");

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            // Check if there are rows to read
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    Talent talent = new Talent
                                    {
                                        ID = reader.GetInt32(reader.GetOrdinal("talent_id")),
                                        Name = reader.GetString(reader.GetOrdinal("talent_name")),
                                        DOB = reader.GetDateTime(reader.GetOrdinal("talent_dob")),
                                        Email = reader.GetString(reader.GetOrdinal("talent_email")),
                                        Specialization = (Specialization)Enum.Parse(typeof(Specialization), reader.GetString(reader.GetOrdinal("talent_specialization"))),
                                        Age = reader.GetInt32(reader.GetOrdinal("talent_Age"))
                                    };

                                    talents.Add(talent);
                                }
                            }
                        }
                    }
                }
                catch (SqlException ex)
                {
                    // Log or handle database-related errors
                    Console.WriteLine("SQL Error: " + ex.Message);
                    throw;
                }
            }

            return talents;
        }



        [WebMethod]
        public int GetNextId()
        {
            // SQL query to get the next identity value
            string query = "SELECT IDENT_CURRENT('talents') + IDENT_INCR('talents') AS NextID;";

            // Using a SqlConnection within a using block ensures it's disposed properly
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                // Open the connection
                conn.Open();

                // Create a SqlCommand to execute the query
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    // Execute the query and get the result
                    object result = cmd.ExecuteScalar();

                    // Check if the result is not null and return it
                    if (result != null && int.TryParse(result.ToString(), out int nextId))
                    {
                        return nextId;
                    }

                    // If the result is null, or conversion failed, throw an exception or return a default value
                    throw new Exception("Unable to retrieve the next ID.");
                }
            }
        }
        [WebMethod]
        public void AddNewTalent(Talent talent)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    // Define the SQL INSERT statement
                    string query = "INSERT INTO talents (talent_name, talent_dob, talent_email, talent_Specialization, talent_Age) " +
                                   "VALUES (@Name, @DOB, @Email, @Specialization, @Age)";

                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        // Add parameters to the SQL command to prevent SQL injection
                        cmd.Parameters.AddWithValue("@Name", talent.Name);
                        cmd.Parameters.AddWithValue("@DOB", talent.DOB);
                        cmd.Parameters.AddWithValue("@Email", talent.Email);
                        cmd.Parameters.AddWithValue("@Specialization", (int)talent.Specialization); // Assuming Specialization is an enum
                        cmd.Parameters.AddWithValue("@Age", talent.Age);

                        // Execute the INSERT statement
                        cmd.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    // Handle connection or database-related errors.
                    // You might want to log the error or handle it differently.
                    throw;
                }
            }
        }
        [WebMethod]
        public void UpdateTalent(int talentId, Talent talent)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    // Define the SQL UPDATE statement
                    string query = "UPDATE talents " +
                                   "SET talent_name = @Name, talent_dob = @DOB, talent_email = @Email, " +
                                   "talent_Specialization = @Specialization, talent_Age = @Age " +
                                   "WHERE talent_id = @TalentId";

                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        // Add parameters to the SQL command
                        cmd.Parameters.AddWithValue("@Name", talent.Name);
                        cmd.Parameters.AddWithValue("@DOB", talent.DOB);
                        cmd.Parameters.AddWithValue("@Email", talent.Email);
                        cmd.Parameters.AddWithValue("@Specialization", (int)talent.Specialization); // Assuming Specialization is an enum
                        cmd.Parameters.AddWithValue("@Age", talent.Age);
                        cmd.Parameters.AddWithValue("@TalentId", talentId);

                        // Execute the UPDATE statement
                        cmd.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    // Handle connection or database-related errors.
                    // You might want to log the error or handle it differently.
                    throw;
                }
            }
        }
        [WebMethod]
        public void DeleteTalent(int talentId)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    // Define the SQL DELETE statement
                    string query = "DELETE FROM talents WHERE talent_id = @TalentId";

                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        // Add a parameter to the SQL command
                        cmd.Parameters.AddWithValue("@TalentId", talentId);

                        // Execute the DELETE statement
                        cmd.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    // Handle connection or database-related errors.
                    // You might want to log the error or handle it differently.
                    throw;
                }
            }
        }
        [WebMethod]
        public Talent GetTalentById(int talentId)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    // Define the SQL SELECT statement to retrieve a Talent by ID
                    string query = "SELECT * FROM talents WHERE talent_id = @TalentId";

                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        // Add the parameter for the Talent ID
                        cmd.Parameters.AddWithValue("@TalentId", talentId);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                // Create a Talent object and populate it with the data from the database
                                Talent talent = new Talent
                                {
                                    ID = reader.GetInt32(reader.GetOrdinal("talent_id")),
                                    Name = reader.GetString(reader.GetOrdinal("talent_name")),
                                    DOB = reader.GetDateTime(reader.GetOrdinal("talent_dob")),
                                    Email = reader.GetString(reader.GetOrdinal("talent_email")),
                                    Specialization = (Specialization)Enum.Parse(typeof(Specialization), reader.GetString(reader.GetOrdinal("talent_specialization"))),
                                    Age = reader.GetInt32(reader.GetOrdinal("talent_Age"))
                                };

                                return talent;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Handle connection or database-related errors.
                    // You might want to log the error or handle it differently.
                    throw;
                }
            }

            return null; // Return null if no Talent with the specified ID is found
        }


    }
}
