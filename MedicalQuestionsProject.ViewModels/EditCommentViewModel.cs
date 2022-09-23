using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MedicalQuestionsProject.ViewModels
{
    public class EditCommentViewModel
    {
        [Required]
        public int CommentID { get; set; }

        [Required]
        public string CommentText { get; set; }

        [Required]
        public DateTime CommentDateAndTime { get; set; }

        [Required]
        public int UserID { get; set; }

        [Required]
        public int AnswerID { get; set; }

        [Required]


        public virtual AnswerViewModel Answer { get; set; }
    }
}
