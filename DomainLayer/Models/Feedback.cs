using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Models
{
    public class Feedback
    {
        /// <summary>
        /// Feedback's unique identifier
        /// </summary>
        [Column("FeedbackID")]
        [Key]
        public Guid FeedbackID { get; set; }

        /// <summary>
        /// Unique identifier of the User that sent the Feedback for database storage purposes
        /// </summary>
        [Required]
        public Guid ClientID { get; set; }

        /// <summary>
        /// Client that sent the Feedback
        /// </summary>
        [ForeignKey("ClientID")]
        public virtual Client Client { get; set; }

        /// <summary>
        /// Unique identifier of the GPSCoordinates where the Feedback was submitted
        /// </summary>
        public Guid FeedbackLocationID { get; set; }

        /// <summary>
        /// Location where the Feedback was submitted
        /// </summary>
        [ForeignKey("FeedbackLocationID")]
        public virtual GPSCoordinates FeedbackLocation { get; set; }

        /// <summary>
        /// Date and Time when the Feedback was submitted
        /// </summary>
        [Required]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime FeedbackDate { get; set; }

        /// <summary>
        /// Feedback Description
        /// </summary>
        [Display(Name = "Feedback Description")]
        [StringLength(500, MinimumLength = 1, ErrorMessage = "Point must have a description between 1 and 500 characters long.")]
        public string FeedbackDescription { get; set; }

        /// <summary>
        /// URL of the folder that contains other Feedback files of other types, like Image, Video or Audio
        /// </summary>
        public string CloudFolderURL { get; set; } 

        /// <summary>
        /// 
        /// </summary>
        public Feedback() { }

        /// <summary>
        /// Public constructor for creating Feedback
        /// </summary>
        /// <param name="clientID">Client's ID</param>
        /// <param name="feedbackLocationID">Location's ID</param>
        /// <param name="feedbackDate">Feedback Date and Time</param>
        /// <param name="feedbackDescription">Feedback Description</param>
        /// <param name="cloudFolderURL">Cloud folder URL</param>
        public Feedback(Guid clientID, Guid feedbackLocationID, DateTime feedbackDate, string feedbackDescription, string cloudFolderURL)
        {
            FeedbackID = Guid.NewGuid();
            ClientID = clientID;
            FeedbackLocationID = feedbackLocationID;
            FeedbackDate = feedbackDate;
            FeedbackDescription = feedbackDescription;
            CloudFolderURL = cloudFolderURL;
        }
    }
}
