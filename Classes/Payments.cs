using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PayX.Classes
{
    public class Payments
    {
        public string sn { get; set; }
        public string code { get; set; }
        public string xdesc { get; set; }
        public string transID { get; set; }
        public string init_amt { get; set; }
        public string tech_amt { get; set; }
        public string convenient_fee { get; set; }

        public string TransactionDate { get; set; }
       
      

        
    }
}