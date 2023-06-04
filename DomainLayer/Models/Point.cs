
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DomainLayer.Models
{
    /// <summary>
    /// Represents a Beacon at ISEP
    /// </summary>
    [Index(nameof(PointName), IsUnique = true)]
    public class Point
    {
        /// <summary>
        /// Point's unique identifier
        /// </summary>
        [Column("PointID")]
        [Key]
        public Guid PointID { get; set; }

        /// <summary>
        /// Point's Name
        /// </summary>
        [Display(Name = "Point Name")]
        [Required(ErrorMessage = "Point Name is a required field.")]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "Point Name must be between 1 and 100 characters long.")]
        public string? PointName { get; set; }

        /// <summary>
        /// 360º image of the Point PointLocation
        /// </summary>
        [StringLength(500, MinimumLength = 1, ErrorMessage = "Point Image URL must be between 1 and 500 characters long.")]
        public string Image360 { get; set; }

        /// <summary>
        /// Description of Point
        /// </summary>
        [Display(Name = "Point Description")]
        [Required(ErrorMessage = "Point Description is a required field.")]
        [StringLength(500, MinimumLength = 1, ErrorMessage = "Point must have a description between 1 and 500 characters long.")]
        public string? Description { get; set; }

        /// <summary>
        /// Unique identifier of Point's Location for database storage purposes.
        /// </summary>
        [Required]
        public Guid PointLocationID { get; set; }

        /// <summary>
        /// Location of the Point
        /// </summary>
        [ForeignKey("PointLocationID")]
        public virtual GPSCoordinates PointLocation { get; set; }

        public Point() { }

        /// <summary>
        /// Public construct for creating Points
        /// </summary>
        /// <param Name="Image360">Point's 360º image</param>
        /// <param Name="Description">Point's Description</param>
        /// <param Name="PointLocation">Point's GPS coordinates</param>
        public Point(string image360, string name, string description, Guid locationID)
        {
            PointID = new Guid();
            Image360 = image360;
            PointName = name;
            Description = description;
            PointLocationID = locationID;
        }
    }
}
