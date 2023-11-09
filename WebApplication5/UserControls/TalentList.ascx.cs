using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using WebApplication5.Models;

namespace WebApplication5.UserControls
{
    public partial class TalentsList : System.Web.UI.UserControl
    {
        // Create an instance of the DBService class
        DBService dbService = new DBService();
        protected HtmlTable talentTable;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Find the HTML table using its ID
                HtmlTable talentTable = (HtmlTable)FindControl("talentTable");

                // Make sure it's not null before adding rows
                if (talentTable != null)
                {
                    // Bind the talent data to the HTML table
                    BindTalentTable(talentTable);
                }
            }
        }

        private void BindTalentTable(HtmlTable talentTable)
        {
            List<Talent> talents = dbService.GetAllTalents();

            foreach (Talent talent in talents)
            {
                // Create a new row for the HTML table
                HtmlTableRow row = new HtmlTableRow();

                // Add cells to the row with talent information
                row.Cells.Add(new HtmlTableCell { InnerText = talent.ID.ToString() });
                row.Cells.Add(new HtmlTableCell { InnerText = talent.Name });
                row.Cells.Add(new HtmlTableCell { InnerText = talent.DOB.ToString("dd/MM/yyyy") });
                row.Cells.Add(new HtmlTableCell { InnerText = talent.Email });
                row.Cells.Add(new HtmlTableCell { InnerText = talent.Specialization });
                row.Cells.Add(new HtmlTableCell { InnerText = talent.Age.ToString() });

                //// Create an "Edit" button in the last cell
                //Button editButton = new Button
                //{
                //    Text = "Edit",
                //    CommandName = "Edit",
                //    CommandArgument = talent.ID.ToString(),
                //    ID = "editBtn"
                //};
                //editButton.Click += EditButton_Click;

                //HtmlTableCell editCell = new HtmlTableCell();
                //editCell.Controls.Add(editButton);
                //row.Cells.Add(editCell);

                // Add the row to the HTML table
                talentTable.Rows.Add(row);
            }
        }

        private void EditButton_Click(object sender, EventArgs e)
        {
            // Handle the "Edit" button click event (implement editing logic here)
            // You can identify the talent to edit using the CommandArgument of the button.

        }

        protected void SearchButton_Click(object sender, EventArgs e)
        {
            string searchKeyword = searchTextBox.Text.Trim();

            // Fetch your talents from the database, or use the existing data.
            List<Talent> talents = dbService.GetAllTalents();

            // Create a new list to store the search results
            List<Talent> searchResults = new List<Talent>();

            // Perform the search based on the keyword
            foreach (Talent talent in talents)
            {
                if (talent.Name.IndexOf(searchKeyword, StringComparison.OrdinalIgnoreCase) >= 0 ||
                    talent.Email.IndexOf(searchKeyword, StringComparison.OrdinalIgnoreCase) >= 0 ||
                    talent.Specialization.IndexOf(searchKeyword, StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    searchResults.Add(talent);
                }
            }

            // Clear the existing rows from the HTML table
            talentTable.Rows.Clear();

            // Bind the search results to the HTML table
            BindTalentTable(talentTable);
        }
    }
}