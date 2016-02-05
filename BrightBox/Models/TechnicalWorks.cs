namespace BrightBox.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;

    public class TechnicalWorks
    {
        public string Token { get; set; }

        public int Status { get; set; }

        public string Datetime { get; set; }

        public bool ShouldSerializeToken()
        {
            return Token != null;
        }
        public bool ShouldSerializeDatetime()
        {
            return Datetime != null;
        }
    }
}