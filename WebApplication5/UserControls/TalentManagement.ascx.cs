using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using System.Xml.Linq;
using WebApplication5.Models;

namespace WebApplication5.UserControls
{

    public partial class TalentManagement : System.Web.UI.UserControl
    {
        DBService dbService = new DBService();
        

        public void AddNewTalent(Talent talent)
        {
            dbService.AddNewTalent(talent);
        }
    }
}