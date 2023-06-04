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
    public class CourseDto
    {
        public Guid CourseID { get; set; }
        public Guid InitialPointID { get; set; }
        public Guid EndPointID { get; set; }
        public string? Description { get; set; }
        public bool IncapacityAcessible { get; set; }
    }

    public class CourseCreationDto : CourseAddAndUpdateDto { }

    public class CourseUpdateDto : CourseAddAndUpdateDto { }

    public abstract class CourseAddAndUpdateDto
    {
        [ForeignKey("InitialPointID")]
        public Guid InitialPointID { get; set; }

        [ForeignKey("EndPointID")]
        //[Compare("InitialPointID", ErrorMessage = "Initial Point and End Point cannot be the same.")]
        public Guid EndPointID { get; set; }

        [Display(Name = "Course Description")]
        [Required(ErrorMessage = "Course Description is a required field.")]
        [StringLength(500, MinimumLength = 1, ErrorMessage = "Course must have a description between 1 and 500 characters long.")]
        public string? Description { get; set; }

        [Display(Name = "Course Accessibility", Description = "Is this course available for people with mobility difficulties?")]
        [Required(ErrorMessage = "Course Accessibility is a required field.")]
        public bool IncapacityAcessible { get; set; }
    }
}
