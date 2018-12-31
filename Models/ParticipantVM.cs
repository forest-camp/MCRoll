using System;
using System.ComponentModel.DataAnnotations;

namespace MCRoll.Models
{
    public class ParticipantVM
    {
        [Required]
        public String Username { get; set; }

        [Required]
        [EmailAddress]
        public String Email { get; set; }

        [Required]
        public String Comment { get; set; }

        [Required]
        public Int32 RollId { get; set; }

        public Participant ToParticipant()
        {
            return new Participant
            {
                Username = this.Username,
                Email = this.Email,
                Comment = this.Comment,
                RollId = this.RollId
            };
        }
    }
}
