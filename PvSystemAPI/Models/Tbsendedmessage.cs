using System;
using System.Collections.Generic;

#nullable disable

namespace PvSystemAPI.Models
{
    public partial class Tbsendedmessage
    {
        public long Id { get; set; }
        public long? Idmsg { get; set; }
        public DateTime? Cdate { get; set; }
        public string Confimationcode { get; set; }

        public virtual TbMessage IdmsgNavigation { get; set; }
    }
}
