using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestApiProj.Models
{
    public class CrazyDealsModel
    {
        public int DealId { get; set; }
        public string DealTitle { get; set; }
        public decimal DealPrice { get; set; }
        public decimal DealDiscount { get; set; }
        public string BulletDescription { get; set; }
        public string FolderName { get; set; }
        public string AccountsTitle { get; set; }
        public decimal RegularPrice { get; set; }
        public DateTime SignupStartingDate { get; set; }
        public DateTime SignupClosingDate { get; set; }
        public bool IsShowStartingDate { get; set; }
        public byte QuantityRestrict { get; set; }
        public int QtnAfterBooking { get; set; }
        public string ShortDescription { get; set; }
        public int ProfileId { get; set; }
        public string CompanyName { get; set; }

        public int CategoryId { get; set; }
        public int SubCategoryId { get; set; }
        public int SubsubCategoryId { get; set; }
    }
}