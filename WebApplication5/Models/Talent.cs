using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication5.Models
{
    public enum Specialization
    {
        Actor,
        Musician,
        Dancer,
        Designer,
        Developer
    }

    public class Talent
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public DateTime DOB { get; set; }
        public string Email { get; set; }
        public Specialization Specialization { get; set; }
        public int Age { get; set; }
    }

}