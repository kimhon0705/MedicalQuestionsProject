using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace MedicalQuestionsProject.DomainModels
{
    public class Answer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AnswerID { get; set; }
        public string AnswerText { get; set; }
        public DateTime AnswerDateAndTime { get; set; }
        public int UserID { get; set; }
        public int QuestionID { get; set; }
        public int CommentsCount { get; set; }
        public int VotesCount { get; set; }
        public bool Istrue { get; set; }

        [ForeignKey("UserID")]
        public virtual User User { get; set; }

        public virtual List<Vote> Votes { get; set; }
        public virtual List<Comment> Comments { get; set; }
        
    }

}
