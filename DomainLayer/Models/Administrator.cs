namespace DomainLayer.Models
{
    /// <summary>
    /// Class responsible for creating Users with the 'Administrator' role.
    /// </summary>
    public class Administrator : User
    {
        public Administrator() { }

        /// <summary>
        /// Public constructor for creating Administrators
        /// </summary>
        /// <param Name="userName">User's Name</param>
        /// <param Name="Email">User's Email address</param>
        /// <param Name="Password">User's Password</param>
        /// <param Name="Avatar">User's Avatar image</param>
        public Administrator(string username, string email, string password, int age, string avatar, bool isActive)
        {
            UserID = new Guid();
            Username = username;
            Age = age;
            Email = email;
            Password = password;
            Avatar = avatar;
            IsActive = isActive;
        }

    }
}
