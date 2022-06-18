using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MedicalQuestionsProject.ViewModels
{
    public class NewCategoryViewModel
    {
        public class EditQuestionViewModel
        {
            [Required]
            public int CategoryID { get; set; }

            [Required]
            public string CategoryName { get; set; }
        }
    }
}
