using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace MCRoll.Models
{
    public class Winner
    {
        public Int32 WinnerId { get; set; }

        public String Username { get; set; }

        public String Email { get; set; }

        [ForeignKey("Roll")]
        public Int32 RollId { get; set; }

        public virtual Roll Roll { get; set; }

        public static Winner FromParticipant(Participant participant)
        {
            return new Winner
            {
                Username = participant.Username,
                Email = participant.Email,
                RollId = participant.RollId,
                Roll = participant.Roll
            };
        }
    }
}
