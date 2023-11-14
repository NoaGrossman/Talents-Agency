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
        //public void AddTalent(string name, string spec, string email, DateTime dob)
        //{
        //    Talent talent = new Talent();
        //    talent.Name = name;
        //    talent.DOB = dob;
        //    talent.Email= email;
        //    talent.Specialization= spec;
        //    talent.Age= 0; //calc age by dob given
        //    dbService.AddNewTalent(talent);
        //}

    }
}