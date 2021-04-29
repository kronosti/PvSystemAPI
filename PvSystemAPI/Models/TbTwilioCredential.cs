using System;
using System.Collections.Generic;

#nullable disable

namespace PvSystemAPI.Models
{
    public partial class TbTwilioCredential
    {
        public string AccountSid { get; set; }
        public string AuthToken { get; set; }
        public string PhoneNumber { get; set; }
    }
}
