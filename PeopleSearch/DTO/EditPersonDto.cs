using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PeopleSearch.DTO
{
    public class EditPersonDto
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string FullName { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public int Age { get; set; }
        [Required]
        public string Interests { get; set; }
        [Required]
        public string PicturePath { get; set; }
    }
}
