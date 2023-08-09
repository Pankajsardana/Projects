using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Lab_EFCodeFirst_New.Models
{

        public class DepartmentViewModel
        { 
            public int DepartmentId { get; set; }
            [Required(ErrorMessage = "Please Enter Name")] 
            public string Name { get; set; }
        }
    }

