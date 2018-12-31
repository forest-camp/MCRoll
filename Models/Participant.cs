using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace MCRoll.Models
{
    public class Participant
    {
        public Int32 ParticipantId { get; set; }

        public String Username { get; set; }

        public String Email { get; set; }

        public String Comment { get; set; }

        [ForeignKey("Roll")]
        public Int32 RollId { get; set; }

        public virtual Roll Roll { get; set; }
    }
}
