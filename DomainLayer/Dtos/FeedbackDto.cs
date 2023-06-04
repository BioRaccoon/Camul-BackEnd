using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainLayer.Models;

namespace DomainLayer.Dtos
{
    public class FeedbackDto
    {
        public Guid FeedbackID { get; set; }
        public Guid ClientID { get; set; }
        public Guid FeedbackLocationID { get; set; }
        public DateTime FeedbackDate { get; set; }
        public string? FeedbackDescription { get; set; }
        public string CloudFolderURL { get; set; }
    }

    public class FeedbackCreationDto : FeedbackAddAndUpdateDto { }

    public class FeedbackUpdateDto : FeedbackAddAndUpdateDto { }

    public abstract class FeedbackAddAndUpdateDto
    {
        /// <summary>
        /// Unique identifier of the User that sent the Feedback for database storage purposes
        /// </summary>
        [Required]
        public Guid ClientID { get; set; }

        /// <summary>
        /// Unique identifier of the GPSCoordinates where the Feedback was submitted
        /// </summary>
        public Guid FeedbackLocationID { get; set; }

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
    }
}
