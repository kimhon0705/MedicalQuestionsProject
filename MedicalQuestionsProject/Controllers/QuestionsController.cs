using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MedicalQuestionsProject.ViewModels;
using MedicalQuestionsProject.ServiceLayer;
using MedicalQuestionsProject.CustomFilters;
using MedicalQuestionsProject.DomainModels;

namespace MedicalQuestionsProject.Controllers
{

    public class QuestionsController : Controller
    {

        private MedicalQuestionsDatabaseDbContext db = new MedicalQuestionsDatabaseDbContext();

        IQuestionsService qs;
        IAnswersService asr;
        ICommentsService csr;
        ICategoriesService cs;


        public ActionResult CheckRight(int answerId, int questionId)
        {
            Answer ans = db.Answers.Where(a => a.AnswerID == answerId).FirstOrDefault();
            Question question = db.Questions.Where(q => q.QuestionID == questionId).FirstOrDefault();

            if (ans != null)
            {
                ans.Istrue = true;
                question.Istrue = true;
                db.SaveChanges();
            }
     
            return RedirectToAction("View", "Questions", new { id = questionId });


        }

        public ActionResult UnCheckRight(int answerId, int questionId)
        {
            Answer ans = db.Answers.Where(a => a.AnswerID == answerId).FirstOrDefault();
            Question question = db.Questions.Where(q => q.QuestionID == questionId).FirstOrDefault();

            if (ans != null)
            {
                ans.Istrue = false;
                question.Istrue = false;
                db.SaveChanges();
            }

            return RedirectToAction("View", "Questions", new { id = questionId });


        }


        public ActionResult Delete(int commentId, int questionId)
        {
            asr.DeleteAnswer(commentId);
            return RedirectToAction("View", "Questions", new { id = questionId });
        }

        public QuestionsController(IQuestionsService qs, IAnswersService asr, ICommentsService csr,ICategoriesService cs)
        {
            this.qs = qs;
            this.asr = asr;
            this.csr = csr;
            this.cs = cs;
        }
        public ActionResult View(int id)
        {
            this.qs.UpdateQuestionViewsCount(id, 1);
            int uid = Convert.ToInt32(Session["CurrentUserID"]);
            QuestionViewModel qvm = this.qs.GetQuestionByQuestionID(id, uid);
            return View(qvm);
        }

       
        [ValidateAntiForgeryToken]
        [UserAuthorizationFilterAttribute]
        [HttpPost, ValidateInput(false)]
        public ActionResult AddAnswer(NewAnswerViewModel navm)
        {
            //currrent working user id
            navm.UserID = Convert.ToInt32(Session["CurrentUserID"]);
            //taking system date and time
            navm.AnswerDateAndTime = DateTime.Now;
            //by default vote count is 0
            navm.VotesCount = 0;
            //checking model state is valid or not
            if (ModelState.IsValid)
            {
                this.asr.InsertAnswer(navm);
                //after adding answer we are redirecting to questions controller view page
                return RedirectToAction("View", "Questions", new { id = navm.QuestionID });
            }
            else
            {
                ModelState.AddModelError("x", "Invalid Data");
                //
                QuestionViewModel qvm = this.qs.GetQuestionByQuestionID(navm.QuestionID, navm.UserID);
                return View("View", qvm);
            }
        }
        [HttpPost, ValidateInput(false)]
        [ValidateAntiForgeryToken]
        [UserAuthorizationFilterAttribute]
        public ActionResult AddComment(NewCommentViewModel ncvm)
        {
            //currrent working user id
            ncvm.UserID = Convert.ToInt32(Session["CurrentUserID"]);
            //taking system date and time
            ncvm.CommentDateAndTime = DateTime.Now;
            //by default vote count is 0
            
            //checking model state is valid or not
            if (ModelState.IsValid)
            {
                this.csr.InsertComment(ncvm);
                //after adding answer we are redirecting to questions controller view page
                return RedirectToAction("View", "Questions", new { id = ncvm.QuestionID });
            }
            else
            {
                ModelState.AddModelError("x", "Invalid Data");
                //
                AnswerViewModel avm = this.asr.GetAnswerByAnswerID(ncvm.AnswerID);
                return View("View", avm);
            }
        }


        [HttpPost, ValidateInput(false)]
        public ActionResult EditAnswer(EditAnswerViewModel avm)
        {
            if (ModelState.IsValid)
            {
                avm.UserID = Convert.ToInt32(Session["CurrentUserID"]);
                this.asr.UpdateAnswer(avm);
                return RedirectToAction("View", new { id = avm.QuestionID });
            }
            else
            {
                ModelState.AddModelError("x", "Invalid data");
                return RedirectToAction("View", new { id = avm.QuestionID });
            }
        }
        [HttpPost]
        public ActionResult EditComment(EditCommentViewModel cvm)
        {
            if (ModelState.IsValid)
            {
                cvm.UserID = Convert.ToInt32(Session["CurrentUserID"]);
                this.csr.UpdateComment(cvm);
                return RedirectToAction("View", new { id = cvm.AnswerID });
            }
            else
            {
                ModelState.AddModelError("x", "Invalid data");
                return RedirectToAction("View", new { id = cvm.AnswerID });
            }
        }

        
        [UserAuthorizationFilterAttribute]
        public ActionResult Create()
        {
            List<CategoryViewModel> categories = this.cs.GetCategories();
            ViewBag.categories = categories;
            return View();
        }

        [HttpPost, ValidateInput(false)]
        [ValidateAntiForgeryToken]
        [UserAuthorizationFilter]
        public ActionResult Create(NewQuestionViewModel qvm)
        {
            if (ModelState.IsValid)
            {
                qvm.AnswersCount = 0;
                qvm.ViewsCount = 0;
                qvm.VotesCount = 0;
                qvm.QuestionDateAndTime = DateTime.Now;
                qvm.UserID = Convert.ToInt32(Session["CurrentUserID"]);
                this.qs.InsertQuestion(qvm);
                return RedirectToAction("Questions", "Home");
            }
            else
            {
                ModelState.AddModelError("x", "Invalid data");
                return View();
            }
        }


    }
}