using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using WebApplication5.Models;
using WebApplication5.UserControls;

namespace WebApplication5
{
    public partial class Default : System.Web.UI.Page
    {
        static TalentsList talentsList = new TalentsList();
        static TalentCard tCard = new TalentCard();
        static TalentManagement tManagement = new TalentManagement();
        protected void Page_Load(object sender, EventArgs e)
        {
        }
        [WebMethod]
        [ScriptMethod(UseHttpGet = true)]
        public static List<Talent> SearchClicked(string inputText)
        {
            // Ensure talentsList is instantiated
            if (talentsList == null)
            {
                talentsList = new TalentsList();
            }
            return talentsList.SearchClicked(inputText);
        }
        [WebMethod]
        [ScriptMethod(UseHttpGet = true)]
        public static List<Talent> ShowTalents(int k, int curPage)
        {
            System.Diagnostics.Debug.WriteLine("showTalents() backend");

            // Ensure talentsList is instantiated
            if (talentsList == null)
            {
                talentsList = new TalentsList();
            }

            return talentsList.GetKTalents(k, curPage);
        }
        [WebMethod]
        public static void RemoveButton_Click(int id)
        {
            // Ensure talentsList is instantiated
            if (talentsList == null)
            {
                talentsList = new TalentsList();
            }

            // Call DeleteTalent on the talentsList instance
            talentsList.DeleteTalent(id);
        }

        [WebMethod]
        public static Validation UpdateOrAdd(int id, string name, string spec, string email, DateTime dob, bool isAdd)
        {
            // Initialize the Validation response object
            var response = new Validation
            {
                IsSuccess = false,
                Message = "Validation failed"
            };

            // Perform validation
            if (string.IsNullOrWhiteSpace(name))
            {
                response.Message = "Name is required.";
                return response;
            }

            if (string.IsNullOrWhiteSpace(spec) || !Enum.TryParse(spec, out Specialization specialization))
            {
                response.Message = "Specialization is required.";
                return response;
            }

            if (string.IsNullOrWhiteSpace(email) || !IsValidEmail(email))
            {
                response.Message = "Valid email is required.";
                return response;
            }

            if (dob == default(DateTime))
            {
                response.Message = "Valid date of birth is required.";
                return response;
            }

            // If validation passes, perform the update operation
            if (tManagement == null)
            {
                tManagement = new TalentManagement();
            }

            Talent talent = new Talent
            {
                //ID = id,
                Name = name,
                DOB = dob,
                Email = email,
                Specialization = specialization,
                Age = DateTime.Today.Year - dob.Year // Calculate age by dob given
            };
            if (isAdd)
            {
                tManagement.AddNewTalent(talent);
            }
            else {
                tManagement.UpdateTalent(id, talent);
            }

            // Update the Validation object for a successful operation
            response.IsSuccess = true;
            response.Message = "Talent updated successfully.";
            return response;
        }

        // Utility method to validate email
        private static bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }


        [WebMethod]
        [ScriptMethod(UseHttpGet = true)]
        public static Talent ShowButton_Click(int id)
        {
            if (tCard == null)
            {
                tCard = new TalentCard();
            }
            Talent t = tCard.ShowTalentCard(id);
            return t;
        }
        [WebMethod]
        public static Talent EditButton_Click(int id)
        {
            if (tCard == null)
            {
                tCard = new TalentCard();
            }
            Talent t = tCard.EditTalentCard(id);
            return t;
        }
        [WebMethod]
        [ScriptMethod(UseHttpGet = true)]
        public static int GetNextId()
        {
            if (tCard == null)
            {
                tCard = new TalentCard();
            }
            int nextId = tCard.GetNextId();
            return nextId;
        }
        [WebMethod]
        [ScriptMethod(UseHttpGet = true)]
        public static int GetTalentsCount()
        {
            // Ensure talentsList is instantiated
            if (talentsList == null)
            {
                talentsList = new TalentsList();
            }

            return talentsList.GetTalentsCount();
        }
    }
}