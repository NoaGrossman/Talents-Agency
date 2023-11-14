using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Web.Services;
using System.Web.UI.HtmlControls;
using WebApplication5.Models;
using WebApplication5.DataLayer;

namespace WebApplication5
{
    public class DBService
    {
        public string connectionString;
        private Queries queries = new Queries(); // Instance of Queries class

        public DBService()
        {
            connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DB"].ConnectionString;
            CreateTalentsTable();
        }
        private void CreateTalentsTable()
        {

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(queries.createTable, connection))
                {
                    try
                    {
                        connection.Open();
                        command.ExecuteNonQuery();
                        Console.WriteLine("Table 'talents' created successfully.");
                    }
                    catch (SqlException e)
                    {
                        Console.WriteLine("Error occurred while creating the table: " + e.Message);
                    }
                }
            }
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

                    using (SqlCommand command = new SqlCommand(queries.getTalentsCount, connection))
                    {
                        talentsCount = (int)command.ExecuteScalar();
                    }
                }
                catch (SqlException e)
                {
                    Console.WriteLine("Error occurred while fetching talents count from table: " + e.Message);
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
                    using (SqlCommand cmd = new SqlCommand(queries.getInRangeTalents, connection))
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
                                        Specialization = reader.GetString(reader.GetOrdinal("talent_Specialization")),
                                        Age = reader.GetInt32(reader.GetOrdinal("talent_Age"))
                                    };

                                    talents.Add(talent);
                                }
                            }
                        }
                    }
                }
                catch (SqlException e)
                {
                    Console.WriteLine("Error occurred while tring to get talents from table: " + e.Message);
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

                    using (SqlCommand cmd = new SqlCommand(queries.getTalentsBySearchInput, connection))
                    {
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
                                        Specialization = reader.GetString(reader.GetOrdinal("talent_Specialization")),
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
                    Console.WriteLine("SQL Error: " + ex.Message);
                    throw;
                }
            }

            return talents;
        }

        [WebMethod]
        public int GetNextId()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                // Open the connection
                conn.Open();

                // Create a SqlCommand to execute the query
                using (SqlCommand cmd = new SqlCommand(queries.getNextId, conn))
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

                    using (SqlCommand cmd = new SqlCommand(queries.addTalent, connection))
                    {
                        // Add parameters to the SQL command to prevent SQL injection
                        cmd.Parameters.AddWithValue("@Name", talent.Name);
                        cmd.Parameters.AddWithValue("@DOB", talent.DOB);
                        cmd.Parameters.AddWithValue("@Email", talent.Email);
                        cmd.Parameters.AddWithValue("@Specialization", talent.Specialization);
                        cmd.Parameters.AddWithValue("@Age", talent.Age);

                        cmd.ExecuteNonQuery();
                    }
                }
                catch (SqlException ex)
                {
                    Console.WriteLine("SQL Error: " + ex.Message);
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

                    using (SqlCommand cmd = new SqlCommand(queries.updateTalentInfo, connection))
                    {
                        // Add parameters to the SQL command
                        cmd.Parameters.AddWithValue("@Name", talent.Name);
                        cmd.Parameters.AddWithValue("@DOB", talent.DOB);
                        cmd.Parameters.AddWithValue("@Email", talent.Email);
                        cmd.Parameters.AddWithValue("@Specialization", talent.Specialization);
                        cmd.Parameters.AddWithValue("@Age", talent.Age);
                        cmd.Parameters.AddWithValue("@TalentId", talentId);

                        cmd.ExecuteNonQuery();
                    }
                }
                catch (SqlException ex)
                {
                    Console.WriteLine("SQL Error: " + ex.Message);
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

                    using (SqlCommand cmd = new SqlCommand(queries.removeTalent, connection))
                    {
                        // Add a parameter to the SQL command
                        cmd.Parameters.AddWithValue("@TalentId", talentId);

                        cmd.ExecuteNonQuery();
                    }
                }
                catch (SqlException ex)
                {
                    Console.WriteLine("SQL Error: " + ex.Message);
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

                    using (SqlCommand cmd = new SqlCommand(queries.getTalentById, connection))
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
                                    Specialization = reader.GetString(reader.GetOrdinal("talent_Specialization")),
                                    Age = reader.GetInt32(reader.GetOrdinal("talent_Age"))
                                };

                                return talent;
                            }
                        }
                    }
                }
                catch (SqlException ex)
                {
                    Console.WriteLine("SQL Error: " + ex.Message);
                    throw;
                }
            }

            return null; // Return null if no Talent with the specified ID is found
        }


    }
}
