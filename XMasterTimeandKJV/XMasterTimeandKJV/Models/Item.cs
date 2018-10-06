using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace XMasterTimeandKJV.Models
{
    public class Item
    {
        public string Id { get; set; }
        public string Text { get; set; }
        public string Description { get; set; }
    }
    public class WorkInstance : BaseObject
    {


        public WorkInstance() { }
        public string Comment { get; set; }
        public DateTime ClockIn { get; set; }
        public DateTime ClockOut { get; set; }
        public DateTime Date { get; set; }
        public HourlyRate HourlyRate { get; set; }
        [Display(AutoGenerateField = false)] 
        [Key] 
        public int Oid { get; set; }

    }

    public class BaseObject
    {
    }
    public class HourlyRate : BaseObject
    {
        public HourlyRate() { }
        public HourlyRate(float rate) : base()
        {
            Rate = rate;
        }

        public string GetRowString()
        {
            return this.ToString();
            //return base.GetRowString() + this.ToString();
        }
        public string ToString()
        {
            return $"Oid={Oid.ToString()},Rate={Rate.ToString()}, Date={Date.ToString()}";
        }

        public DateTime Date { get; set; }
        /**********************/
        [Key]
        public int Oid { get; set; }
        /**********************/
        public float Rate { get; set; }
    }
}

/* 
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
...
   
*/
