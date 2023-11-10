using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebApplication5.Models;
using WebApplication5.UserControls;

namespace WebApplication5
{
    public partial class Default : System.Web.UI.Page
    {
        static TalentsList talentsList = new TalentsList();
        static TalentCard tCard = new TalentCard();
        protected void Page_Load(object sender, EventArgs e)
        {
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
        public static void EditButton_Click(int id, Talent talent)
        {
            // Ensure talentsList is instantiated
            if (talentsList == null)
            {
                talentsList = new TalentsList();
            }

            // Call DeleteTalent on the talentsList instance
            talentsList.EditTalent(id, talent);
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
    }
}