﻿using System;
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
        static DBService dbService = new DBService(); // Create an instance of the DBService class

        public void AddNewTalent(Talent talent)
        {
            dbService.AddNewTalent(talent);
        }

        public void UpdateTalent(int id, Talent talent)
        {
            dbService.UpdateTalent(id, talent);
        }
    }
}