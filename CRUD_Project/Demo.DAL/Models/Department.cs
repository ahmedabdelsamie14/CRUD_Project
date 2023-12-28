using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.DAL.Models
{
    //Model must contain only properties
    public class Department:ModelBase
    {
       

        [Required(ErrorMessage ="Code is Required !!")]//Required tkon Fluent API
        public string Code { get; set; }

        [Required(ErrorMessage = "Name is Required !!")] //Not Clean Code mfrod tkon f view Model
        [MaxLength(5)]
        [MinLength(5)]
        public string Name { get; set; }

        [Display(Name = "Date Of Creation")]
        public DateTime DateOfCreation { get; set; }

        //Many
        public ICollection<Employee> Employees { get; set; } = new HashSet<Employee>();    
    }
}
