using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication5.DataLayer
{
    public class Queries
    {
        public string createTable = @"CREATE TABLE talents_agancy (
            talent_id INT IDENTITY(1,1) PRIMARY KEY,
            talent_name VARCHAR(255),
            talent_email VARCHAR(255),
            talent_dob DATE,
            talent_age INT,
            talent_specialization VARCHAR(255)
            -- Add other columns as needed
        );";

        public string getTalentsCount = @"SELECT COUNT(*) AS NumberOfTalents FROM talents_agancy";

        public string getInRangeTalents = @"SELECT * FROM talents_agancy ORDER BY talent_name, talent_specialization " +
                        "OFFSET @Offset ROWS FETCH NEXT @Fetch ROWS ONLY";

        public string getTalentsBySearchInput = @"SELECT * FROM talents_agancy WHERE talent_name LIKE @inputText OR talent_email LIKE @inputText OR talent_specialization LIKE @inputText";

        public string getNextId = @"SELECT IDENT_CURRENT('talents_agancy') + IDENT_INCR('talents_agancy') AS NextID;";

        public string addTalent = "INSERT INTO talents_agancy (talent_name, talent_dob, talent_email, talent_Specialization, talent_Age) " +
                                   "VALUES (@Name, @DOB, @Email, @Specialization, @Age)";

        public string updateTalentInfo = "UPDATE talents_agancy " +
                                   "SET talent_name = @Name, talent_dob = @DOB, talent_email = @Email, " +
                                   "talent_Specialization = @Specialization, talent_Age = @Age " +
                                   "WHERE talent_id = @TalentId";

        public string removeTalent = "DELETE FROM talents_agancy WHERE talent_id = @TalentId";

        public string getTalentById = "SELECT * FROM talents_agancy WHERE talent_id = @TalentId";



    }

}