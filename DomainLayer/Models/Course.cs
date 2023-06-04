using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DomainLayer.Models
{
    /// <summary>
    /// Represents a path between two Points
    /// </summary>
    public class Course
    {
        /// <summary>
        /// Course's unique identifier
        /// </summary>
        [Column("CourseID")]
        [Key]
        public Guid CourseID { get; set; }

        /// <summary>
        /// Unique identifier of Course's Initial Point for database storage purposes.
        /// </summary>
        [Required]
        public Guid InitialPointID { get; set; }

        /// <summary>
        /// Course's starting point
        /// </summary>
        [ForeignKey("InitialPointID")]
        public virtual Point InitialPoint { get; set; }

        /// <summary>
        /// Unique identifier of Course's End Point for database storage purposes.
        /// </summary>
        [Required]
        //[Compare("InitialPointID", ErrorMessage = "Initial Point and End Point cannot be the same.")]
        public Guid EndPointID { get; set; }

        /// <summary>
        /// Course's destination point
        /// </summary>
        [ForeignKey("EndPointID")]
        public virtual Point EndPoint { get; set; }

        /// <summary>
        /// Course's Description
        /// </summary>
        [Display(Name = "Course Description")]
        [Required(ErrorMessage = "Course Description is a required field.")]
        [StringLength(500, MinimumLength = 1, ErrorMessage = "Course must have a description between 1 and 500 characters long.")]
        public string Description { get; set; }

        /// <summary>
        /// Checks if the path between the two Points is acessible to people with incapacities
        /// </summary>
        [Display(Name = "Course Accessibility", Description = "Is this course available for people with mobility difficulties?")]
        [Required(ErrorMessage = "Course Accessibility is a required field.")]
        public bool IncapacityAcessible { get; set; }

        public Course() { }

        /// <summary>
        /// Public constructor for creating Courses
        /// </summary>
        /// <param Name="InitialPoint">Course's starting Point</param>
        /// <param Name="EndPoint">Course's destination Point</param>
        /// <param Name="Description">Course's Description</param>
        /// <param Name="IncapacityAcessible">Course acessibility</param>
        public Course(Guid initialPointID, Guid endPointID, string description, bool incapacityAcessible)
        {
            CourseID = new Guid();
            InitialPointID = initialPointID;
            EndPointID = endPointID;
            Description = description;
            IncapacityAcessible = incapacityAcessible;
        }
    }
}
