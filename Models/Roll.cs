using System;
using System.Collections.Generic;

namespace MCRoll.Models
{
    public class Roll
    {
        public Int32 RollId { get; set; }

        public String Name { get; set; }

        public String Description { get; set; }

        public String Creator { get; set; }

        public DateTime CreateTime { get; set; }

        public DateTime EndTime { get; set; }

        public Int32 WinnerNumber { get; set; }

        public virtual ICollection<Participant> Participants { get; set; }

        public virtual ICollection<Winner> Winners { get; set; }

        public Boolean IsOpen { get; set; }
    }
}
