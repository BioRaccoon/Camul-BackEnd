using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DomainLayer.Models
{
    /// <summary>
    /// Class responsible for creating Users with the 'Client' role.
    /// </summary>
    public class Client : User
    {

        /// <summary>
        /// User's Incapacity if any
        /// </summary>
        [Display(Name = "Incapacities")]
        public string Incapacity { get; set; }

        /// <summary>
        /// Unique identifier of User's Location for database storage purposes.
        /// </summary>
        public Guid UserLocationID { get; set; }

        /// <summary>
        /// User's PointLocation
        /// </summary>
        [ForeignKey("UserLocationID")]
        public virtual GPSCoordinates? UserLocation { get; set; }

        public Client() { }

        /// <summary>
        /// Public constructor for creating Clients
        /// </summary>
        /// <param Name="userName">User's Name</param>
        /// <param Name="Email">User's Email address</param>
        /// <param Name="Password">User's Password</param>
        /// <param Name="Avatar">User's Avatar image</param>
        /// <param Name="age">User's Age</param>
        /// <param Name="incapacity">User's Incapacity</param>
        /// <param Name="PointLocation">User's PointLocation</param>
        public Client(string username, string email, string password, string avatar, bool isActive,
            int age, string incapacity, Guid userLocationID) {

            UserID = new Guid();
            Username = username;
            Age = age;
            Email = email;
            Password = password;
            Avatar = avatar;
            Incapacity = incapacity;
            UserLocationID = userLocationID;
            IsActive = isActive;
        }
    }
}
