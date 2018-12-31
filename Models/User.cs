using System;

namespace MCRoll.Models
{
    public class User
    {
        public Int32 UserId { get; set; }

        public String UserName { get; set; }

        public String Password { get; set; }

        public String NickName { get; set; }

        public String Email { get; set; }

        public Boolean IsAdmin { get; set; }
    }
}
