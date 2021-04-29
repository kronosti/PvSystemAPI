using System;
using System.Collections.Generic;

#nullable disable

namespace PvSystemAPI.Models
{
    public partial class TbMessage
    {
        public TbMessage()
        {
            Tbsendedmessages = new HashSet<Tbsendedmessage>();
        }

        public long Id { get; set; }
        public DateTime? Crdate { get; set; }
        public string Tomsg { get; set; }
        public string Messagetxt { get; set; }

        public virtual ICollection<Tbsendedmessage> Tbsendedmessages { get; set; }
    }
}
