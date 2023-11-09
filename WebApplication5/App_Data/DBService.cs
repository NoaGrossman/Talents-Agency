using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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

        public List<Talent> GetAllTalents()
        {
            List<Talent> talents = new List<Talent>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    // Define the SQL query to retrieve data from the database
                    string query = "select * from agancy.talents order by talent_name, talent_specialization;";

                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
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
                                        DOB = reader.GetDateTime(reader.GetOrdinal("talent_dateOfBirth")),
                                        Email = reader.GetString(reader.GetOrdinal("talent_email")),
                                        Specialization = reader.GetString(reader.GetOrdinal("talent_Specialization")),
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
        public void AddNewTalent(Talent talent)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    // Define the SQL INSERT statement
                    string query = "INSERT INTO agancy.talents (talent_name, talent_dateOfBirth, talent_email, talent_Specialization, talent_Age) " +
                                   "VALUES (@Name, @DOB, @Email, @Specialization, @Age)";

                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        // Add parameters to the SQL command to prevent SQL injection
                        cmd.Parameters.AddWithValue("@Name", talent.Name);
                        cmd.Parameters.AddWithValue("@DOB", talent.DOB);
                        cmd.Parameters.AddWithValue("@Email", talent.Email);
                        cmd.Parameters.AddWithValue("@Specialization", talent.Specialization);
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
        public void UpdateTalent(int talentId, Talent talent)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    // Define the SQL UPDATE statement
                    string query = "UPDATE agancy.talents " +
                                   "SET talent_name = @Name, talent_dateOfBirth = @DOB, talent_email = @Email, " +
                                   "talent_Specialization = @Specialization, talent_Age = @Age " +
                                   "WHERE talent_id = @TalentId";

                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        // Add parameters to the SQL command
                        cmd.Parameters.AddWithValue("@Name", talent.Name);
                        cmd.Parameters.AddWithValue("@DOB", talent.DOB);
                        cmd.Parameters.AddWithValue("@Email", talent.Email);
                        cmd.Parameters.AddWithValue("@Specialization", talent.Specialization);
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
        public void DeleteTalent(int talentId)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    // Define the SQL DELETE statement
                    string query = "DELETE FROM agancy.talents WHERE talent_id = @TalentId";

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

        public Talent GetTalentById(int talentId)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    // Define the SQL SELECT statement to retrieve a Talent by ID
                    string query = "SELECT * FROM agancy.talents WHERE talent_id = @TalentId";

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
                                    DOB = reader.GetDateTime(reader.GetOrdinal("talent_dateOfBirth")),
                                    Email = reader.GetString(reader.GetOrdinal("talent_email")),
                                    Specialization = reader.GetString(reader.GetOrdinal("talent_Specialization")),
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
