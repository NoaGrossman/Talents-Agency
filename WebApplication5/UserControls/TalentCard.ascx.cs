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
        DBService dbService = new DBService();

        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void TalentCardBtn_Click(object sender, EventArgs e)
        {
            //Talent talent = dbService.GetTalentById(id);
        }

        public Talent ShowTalentCard(int id)
        {
            Talent talent = dbService.GetTalentById(id);
            return talent;
        }

    }
}