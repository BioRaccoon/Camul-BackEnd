using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Dtos
{
    public class GPSCoordinatesDto
    {
        public Guid LocationID { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }

    public class GPSCoordinatesCreationDto : GPSCoordinatesAddAndUpdateDto { }

    public class GPSCoordinatesUpdateDto : GPSCoordinatesAddAndUpdateDto { }

    public abstract class GPSCoordinatesAddAndUpdateDto
    {

        [Display(Name = "Latitude")]
        [Required(ErrorMessage = "Latitude is a required field.")]
        [Range(-90.0, 90.0, ErrorMessage = "Latitude value must be between -90º and 90º degrees.")]
        public double Latitude { get; set; }

        [Display(Name = "Longitude")]
        [Required(ErrorMessage = "Longitude is a required field.")]
        [Range(-180.0, 180.0, ErrorMessage = "Longitude value must be between -180º and 180º degrees.")]
        public double Longitude { get; set; }
    }
}
