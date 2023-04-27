using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBook.Models
{
    public class CoverType
    {
        [Key]
        public int id { get; set; }
        [Display(Name = "Name")]
        [Required]
        [StringLength(50)]
        public string Name { get; set; }
    }
}
