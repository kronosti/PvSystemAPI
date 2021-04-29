using System;
using System.Collections.Generic;

#nullable disable

namespace PvSystemAPI.Models
{
    public partial class VsSendedMessage
    {
        public long Id { get; set; }
        public DateTime? Crdate { get; set; }
        public string Tomsg { get; set; }
        public string Messagetxt { get; set; }
        public string Confimationcode { get; set; }
    }
}
