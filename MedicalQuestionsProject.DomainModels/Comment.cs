using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MedicalQuestionsProject.DomainModels
{
    public class Comment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CommentID { get; set; }
        public string CommentText { get; set; }
        public DateTime CommentDateAndTime { get; set; }
        public int UserID { get; set; }
        public int AnswerID { get; set; }

        [ForeignKey("UserID")]
        public virtual User User { get; set; }
        
    }
}
