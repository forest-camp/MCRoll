using System;
using System.ComponentModel.DataAnnotations;

namespace MCRoll.Models
{
    public class RollVM
    {
        [Required]
        public String Name { get; set; }

        [Required]
        public String Description { get; set; }

        [Required]
        public String Creator { get; set; }

        [Required]
        public DateTime EndTime { get; set; }

        [Required]
        public Int32 WinnerNumber { get; set; }

        public Roll ToRoll()
        {
            return new Roll
            {
                Name = this.Name,
                Description = this.Description,
                Creator = this.Creator,
                CreateTime = DateTime.Now,
                EndTime = this.EndTime,
                WinnerNumber = this.WinnerNumber,
                IsOpen = true
            };
        }
    }
}
