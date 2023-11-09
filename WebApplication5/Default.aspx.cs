using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebApplication5.Models;

namespace WebApplication5
{
    public partial class Default : System.Web.UI.Page
    {
        DBService dbService = new DBService();

        protected void Page_Load(object sender, EventArgs e)
        {
        }
    }
}