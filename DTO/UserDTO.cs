namespace ContactApplication.DTO
{
        public class UserDTO
    {
        public long UserId { get; set; } //Primary key
        
        public string FirstName { get; set; }   //Name of the user
        public string LastName { get; set; }   //Name of the user
        public string Role { get; set; }   //Role of the person
        public string MobileNumber { get; set; } //Contanct number of the user
        public string Email { get; set; }   //EmailId of the user Used for LogIn purpose
        public string Password{get;set;}

        
    }

}