using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PeopleSearch.Models
{
    public class PersonContext : DbContext
    {
        public DbSet<Person> Person { get; set; }

        public PersonContext(DbContextOptions<PersonContext> options):base(options)
        {

        }
    }
}
