using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DomainLayer.Models
{
    /// <summary>
    /// Represents a specific PointLocation
    /// </summary>
    public class GPSCoordinates
    {
        /// <summary>
        /// Location's unique identifier
        /// </summary>
        [Column("LocationID")]
        [Key]
        public Guid LocationID { get; set; }

        /// <summary>
        /// GPS's Latitude value in degrees
        /// </summary>
        [Display(Name = "Latitude")]
        [Required(ErrorMessage = "Latitude is a required field.")]
        [Range(-90.0, 90.0, ErrorMessage = "Latitude value must be between -90º and 90º degrees.")]
        public double Latitude { get; set; }

        /// <summary>
        /// GPS's Longitude value in degrees
        /// </summary>
        [Display(Name = "Longitude")]
        [Required(ErrorMessage = "Longitude is a required field.")]
        [Range(-180.0, 180.0, ErrorMessage = "Longitude value must be between -180º and 180º degrees.")]
        public double Longitude { get; set; }

        public GPSCoordinates() { }

        /// <summary>
        /// Public constructor for GPS Coordinates
        /// </summary>
        /// <param Name="Latitude">Latitude in degrees</param>
        /// <param Name="Longitude">Longitude in degrees</param>
        public GPSCoordinates(double latitude, double longitude)
        {
            LocationID = Guid.NewGuid();
            Latitude = latitude;
            Longitude = longitude;
        }
    }
}
