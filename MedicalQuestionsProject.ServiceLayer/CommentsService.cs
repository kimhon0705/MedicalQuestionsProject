using System;
using System.Collections.Generic;
using System.Linq;
using MedicalQuestionsProject.DomainModels;
using MedicalQuestionsProject.ViewModels;
using MedicalQuestionsProject.Repositories;
using AutoMapper;
using AutoMapper.Configuration;

namespace MedicalQuestionsProject.ServiceLayer
{
    public interface ICommentsService
    {
        void InsertComment(NewCommentViewModel cvm);
        void UpdateComment(EditCommentViewModel cvm);
        void DeleteComment(int cid);
        List<CommentViewModel> GetCommentsByAnswerID(int aid);
        CommentViewModel GetCommentByCommentID(int CommentID);
    }
    public class CommentsService : ICommentsService
    {
        ICommentsRepository cr;

        public CommentsService()
        {
            cr = new CommentsRepository();
        }

        public void DeleteComment(int cid)
        {
            cr.DeleteComment(cid);
        }

        public CommentViewModel GetCommentByCommentID(int CommentID)
        {
            Comment c = cr.GetCommentsByCommentID(CommentID).FirstOrDefault();
            CommentViewModel cvm = null;
            if (c != null)
            {
                var config = new MapperConfiguration(cfg => { cfg.CreateMap<Comment, CommentViewModel>(); cfg.IgnoreUnmapped(); });
                IMapper mapper = config.CreateMapper();
                cvm = mapper.Map<Comment, CommentViewModel>(c);
            }
            return cvm;
        }

        public List<CommentViewModel> GetCommentsByAnswerID(int aid)
        {
            List<Comment> c = cr.GetCommentsByAnswerID(aid);
            var config = new MapperConfiguration(cfg => { cfg.CreateMap<Comment, CommentViewModel>(); cfg.IgnoreUnmapped(); });
            IMapper mapper = config.CreateMapper();
            List<CommentViewModel> cvm = mapper.Map<List<Comment>, List<CommentViewModel>>(c);
            return cvm;
        }

        public void InsertComment(NewCommentViewModel cvm)
        {
            var config = new MapperConfiguration(cfg => { cfg.CreateMap<NewCommentViewModel, Comment>(); cfg.IgnoreUnmapped(); });
            IMapper mapper = config.CreateMapper();
            Comment c = mapper.Map<NewCommentViewModel, Comment>(cvm);
            cr.InsertComment(c);
        }

        public void UpdateComment(EditCommentViewModel cvm)
        {
            var config = new MapperConfiguration(cfg => { cfg.CreateMap<EditCommentViewModel, Comment>(); cfg.IgnoreUnmapped(); });
            IMapper mapper = config.CreateMapper();
            Comment c = mapper.Map<EditCommentViewModel, Comment>(cvm);
            cr.UpdateComment(c);
        }
    }
}
