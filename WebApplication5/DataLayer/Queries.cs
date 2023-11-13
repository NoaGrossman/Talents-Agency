using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication5.DataLayer
{
    public class Queries
    {
        string GetAllTalents = "select * from agancy.talents order by talent_name, talent_specialization;";
    }
}