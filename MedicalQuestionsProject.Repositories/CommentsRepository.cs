using System;
using System.Collections.Generic;
using System.Linq;
using MedicalQuestionsProject.DomainModels;

namespace MedicalQuestionsProject.Repositories
{
    public interface ICommentsRepository
    {
        void InsertComment(Comment c);
        void UpdateComment(Comment c);
        void DeleteComment(int cid);
        List<Comment> GetCommentsByAnswerID(int aid);
        List<Comment> GetCommentsByCommentID(int CommentID);
    }
    public class CommentsRepository : ICommentsRepository
    {
        MedicalQuestionsDatabaseDbContext db;
        IAnswersRepository ar;
       
        public CommentsRepository()
        {
            db = new MedicalQuestionsDatabaseDbContext();
            ar = new AnswersRepository();
        }

        public void DeleteComment(int cid)
        {
            Comment cmt = db.Comments.Where(temp => temp.CommentID == cid).First();
            if (cmt != null)
            {
                db.Comments.Remove(cmt);
                db.SaveChanges();
                ar.UpdateAnswerCommentsCount(cmt.AnswerID, -1);
            }
        }

       
        public List<Comment> GetCommentsByAnswerID(int aid)
        {
            List<Comment> cmt = db.Comments.Where(temp => temp.AnswerID == aid).OrderByDescending(temp => temp.CommentDateAndTime).ToList();
            return cmt;
        }

        public List<Comment> GetCommentsByCommentID(int CommentID)
        {
            List<Comment> cmt = db.Comments.Where(temp => temp.CommentID == CommentID).ToList();
            return cmt;
        }

        public void InsertComment(Comment c)
        {
            db.Comments.Add(c);
            db.SaveChanges();
            ar.UpdateAnswerCommentsCount(c.AnswerID, 1);
        }

        public void UpdateComment(Comment c)
        {
            Comment cmt = db.Comments.Where(temp => temp.CommentID == c.CommentID).FirstOrDefault();
            if (cmt != null)
            {
                cmt.CommentText = c.CommentText;
                db.SaveChanges();
            }
        }
    }
}


