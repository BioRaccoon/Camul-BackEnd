using DomainLayer.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Dtos
{
    public class PointDto
    {

        public Guid PointID { get; set; }
        public string? PointName { get; set; }
        public string? Image360 { get; set; }
        public string? Description { get; set; }
        public Guid PointLocationID { get; set; }

    }

    public class PointCreationDto : PointAddAndUpdateDto { }

    public class PointUpdateDto : PointAddAndUpdateDto { }

    public abstract class PointAddAndUpdateDto
    {

        [Display(Name = "Point Name")]
        [Required(ErrorMessage = "Point Name is a required field.")]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "Point Name must be between 1 and 100 characters long.")]
        public string? PointName { get; set; }

        public string Image360 { get; set; }

        [Display(Name = "Point Description")]
        [Required(ErrorMessage = "Point Description is a required field.")]
        [StringLength(500, MinimumLength = 1, ErrorMessage = "Point must have a description between 1 and 500 characters long.")]
        public string? Description { get; set; }

        [ForeignKey(nameof(GPSCoordinates))]
        public Guid PointLocationID { get; set; }
    }
}
