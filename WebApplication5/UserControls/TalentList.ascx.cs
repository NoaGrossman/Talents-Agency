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
        // Create an instance of the DBService class
        static DBService dbService = new DBService();
        protected HtmlTable talentTable;

        protected void Page_Load()
        {
            if (!IsPostBack)
            {
                GetKTalents(2, 0);
            }
        }
        public List<Talent> GetKTalents(int k, int curPage)
        {
            return dbService.GetKTalents(k, curPage);
        }
        public int GetTalentsCount()
        {
            return dbService.GetTalentsCount();
        }

        //private void BindTalentTable(List<Talent> talents)
        //{
        //    // Clear existing rows and header from the table
        //    talentTable.Rows.Clear();

        //    // Create a new row for the header
        //    HtmlTableRow headerRow = new HtmlTableRow();

        //    // Add header cells to the header row
        //    headerRow.Cells.Add(new HtmlTableCell { InnerText = "Talent ID" });
        //    headerRow.Cells.Add(new HtmlTableCell { InnerText = "Name" });
        //    headerRow.Cells.Add(new HtmlTableCell { InnerText = "Date of Birth" });
        //    headerRow.Cells.Add(new HtmlTableCell { InnerText = "Email" });
        //    headerRow.Cells.Add(new HtmlTableCell { InnerText = "Specialization" });
        //    headerRow.Cells.Add(new HtmlTableCell { InnerText = "Age" });

        //    // Add the header row to the thead section of the HTML table
        //    talentTable.Rows.Add(headerRow);

        //    // Iterate through talents and create rows for each talent
        //    foreach (Talent talent in talents)
        //    {
        //        // Create a new row for the HTML table
        //        HtmlTableRow row = new HtmlTableRow();

        //        // Set the data-id attribute to the talent ID
        //        row.Attributes.Add("data-id", talent.ID.ToString());

        //        // Add onclick attribute to the row
        //        row.Attributes.Add("onclick", $"rowClicked({talent.ID})");

        //        // Add cells to the row with talent information
        //        row.Cells.Add(new HtmlTableCell { InnerText = talent.ID.ToString() });
        //        row.Cells.Add(new HtmlTableCell { InnerText = talent.Name });
        //        row.Cells.Add(new HtmlTableCell { InnerText = talent.DOB.ToString("dd/MM/yyyy") });
        //        row.Cells.Add(new HtmlTableCell { InnerText = talent.Email });
        //        row.Cells.Add(new HtmlTableCell { InnerText = talent.Specialization });
        //        row.Cells.Add(new HtmlTableCell { InnerText = talent.Age.ToString() });

        //        // Add the row to the HTML table
        //        talentTable.Rows.Add(row);
        //    }
        //}

        public void DeleteTalent(int id)
        {
            dbService.DeleteTalent(id);
        }

        public void EditTalent(int id, Talent talent)
        {
            dbService.UpdateTalent(id, talent);
        }


        public List<Talent> SearchClicked(string inputText)
        {
            return dbService.Search(inputText);
        }

    }
}