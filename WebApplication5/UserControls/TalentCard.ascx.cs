using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebApplication5.Models;

namespace WebApplication5.UserControls
{
    public partial class TalentCard : UserControl
    {
        static DBService dbService = new DBService(); // Create an instance of the DBService class

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public Talent ShowTalentCard(int id)
        {
            Talent talent = dbService.GetTalentById(id);
            return talent;
        }
        public Talent EditTalentCard(int id)
        {
            Talent t = dbService.GetTalentById(id);
            return t;
        }
        public int GetNextId()
        {
            int nextId = dbService.GetNextId();
            return nextId;
        }

    }
}