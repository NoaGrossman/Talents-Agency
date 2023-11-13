using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
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
        public static void UpdateButton_Click(int id, string name, string spec, string email, DateTime dob)
        {
            if (tCard == null)
            {
                tCard = new TalentCard();
            }
            Talent talent = new Talent();
            talent.Name = name;
            talent.DOB = dob;
            talent.Email = email;
            talent.Specialization = spec;
            talent.Age = DateTime.Today.Year - dob.Year; //calc age by dob given

            tCard.UpdateTalentCard(id, talent);
        }

        [WebMethod]
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
        public static int GetTalentsCount()
        {
            // Ensure talentsList is instantiated
            if (talentsList == null)
            {
                talentsList = new TalentsList();
            }

            return talentsList.GetTalentsCount();
        }
        [WebMethod]
        public static void AddNewTalent(string name, string spec, string email, DateTime dob)
        {
            if (tManagement == null)
            {
                tManagement = new TalentManagement();
            }
            Talent talent = new Talent();
            talent.Name = name;
            talent.DOB = dob;
            talent.Email = email;
            talent.Specialization = spec;
            talent.Age = DateTime.Today.Year - dob.Year; //calc age by dob given

            tManagement.AddNewTalent(talent);
        }
    }
}