using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using WebApplication5.Models;

namespace WebApplication5.UserControls
{
    public partial class TalentsList : System.Web.UI.UserControl
    {
        static DBService dbService = new DBService(); // Create an instance of the DBService class

        protected HtmlTable talentTable;

        protected void Page_Load()
        {
            
        }
        public List<Talent> GetKTalents(int k, int curPage)
        {
            return dbService.GetKTalents(k, curPage);
        }
        public int GetTalentsCount()
        {
            return dbService.GetTalentsCount();
        }

        public void DeleteTalent(int id)
        {
            dbService.DeleteTalent(id);
        }

        public List<Talent> SearchClicked(string inputText)
        {
            return dbService.Search(inputText);
        }

    }
}