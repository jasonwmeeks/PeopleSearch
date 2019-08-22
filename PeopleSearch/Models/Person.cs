using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PeopleSearch.Models
{
    public class Person
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [Column(TypeName = "nvarchar(100)")]
        public string FullName { get; set; }
        [Required]
        [Column(TypeName = "nvarchar(255)")]
        public string Address { get; set; }
        [Required]
        public int Age { get; set; }
        [Required]
        public string Interests { get; set; }
        [Required]
        [Column(TypeName = "nvarchar(100)")]
        public string PicturePath { get; set; }
    }
}
