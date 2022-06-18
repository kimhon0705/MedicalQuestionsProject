using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MedicalQuestionsProject.ViewModels;
using MedicalQuestionsProject.ServiceLayer;
using MedicalQuestionsProject.CustomFilters;

namespace MedicalQuestionsProject.Controllers
{
    public class QuestionsController : Controller
    {
        IQuestionsService qs;
        IAnswersService asr;
        ICategoriesService cs;


        public QuestionsController(IQuestionsService qs, IAnswersService asr, ICategoriesService cs)
        {
            this.qs = qs;
            this.asr = asr;
            this.cs = cs;
        }
        public ActionResult View(int id)
        {
            this.qs.UpdateQuestionViewsCount(id, 1);
            int uid = Convert.ToInt32(Session["CurrentUserID"]);
            QuestionViewModel qvm = this.qs.GetQuestionByQuestionID(id, uid);
            return View(qvm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [UserAuthorizationFilterAttribute]
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


        [HttpPost]
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
        [UserAuthorizationFilterAttribute]
        public ActionResult Create()
        {
            List<CategoryViewModel> categories = this.cs.GetCategories();
            ViewBag.categories = categories;
            return View();
        }

        [HttpPost]
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