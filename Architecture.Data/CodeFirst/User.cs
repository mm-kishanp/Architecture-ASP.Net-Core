using Architecture.Data;

namespace Architecture.DataBase.CodeFirst
{
    public class User : BaseEntities
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailId{ get; set; }
        public string ContactNo  { get; set; }
        public string Password { get; set; }
        public string ProfilePic{ get; set; }
        public string LoginAttempt { get; set; }
    }
}