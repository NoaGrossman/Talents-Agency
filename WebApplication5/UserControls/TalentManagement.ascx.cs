using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using WebApplication5.Models;

namespace WebApplication5.UserControls
{

    public partial class TalentManagement : System.Web.UI.UserControl
    {
        DBService dbService = new DBService();
        // Event handler for the Add button
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            // Access the values entered in the textboxes
            string name = txtName.Text;
            string email = txtEmail.Text;
            string dob = txtDob.Text;
            string specialization = txtSpecialization.Text;

            // Create a new Talent object (you may need to parse the date)
            Talent newTalent = new Talent
            {
                Name = name,
                Email = email,
                DOB = DateTime.ParseExact(dob, "dd-MM-yyyy", CultureInfo.InvariantCulture),
                Specialization = specialization,
                // Set other properties as needed
            };

            // Add the new talent to your data source
            dbService.AddNewTalent(newTalent);

            // Clear the textboxes after adding
            txtName.Text = "";
            txtEmail.Text = "";
            txtDob.Text = "";
            txtSpecialization.Text = "";

            // Optionally, update the GridView to reflect the changes
            dbService.GetAllTalents();
        }


        // Event handler for the Edit button
        protected void btnEdit_Click(object sender, EventArgs e)
        {
            // Access the talent ID from the UI (you need to define this)
            int talentId = 1; // Implement this method

            // Retrieve the talent to be edited from your data source
            Talent talentToEdit = dbService.GetTalentById(talentId); // Implement this method

            if (talentToEdit != null)
            {
                // Access the updated values from the UI
                string name = txtName.Text;
                string email = txtEmail.Text;
                string dob = txtDob.Text;
                string specialization = txtSpecialization.Text;

                // Update the talent's properties
                talentToEdit.Name = name;
                talentToEdit.Email = email;
                talentToEdit.DOB = DateTime.ParseExact(dob, "dd-MM-yyyy", CultureInfo.InvariantCulture);
                talentToEdit.Specialization = specialization;

                // Save the changes to your data source
                dbService.UpdateTalent(talentId, talentToEdit); 

                // Optionally, refresh the GridView to reflect the changes
                dbService.GetAllTalents();
            }
            else
            {
                // Handle the case where the talent to edit was not found
                // You can display an error message or take appropriate action.
            }
        }


        // Event handler for the Delete button
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            // Implement logic to delete a talent
        }
    }
}