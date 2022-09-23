using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MedicalQuestionsProject.ViewModels
{
    public class CommentViewModel
    {
        public int CommentID { get; set; }
        public string CommentText { get; set; }
        public DateTime CommentDateAndTime { get; set; }
        public int UserID { get; set; }
        public int AnswerID { get; set; }
        public virtual UserViewModel User { get; set; }
        
    }
}
