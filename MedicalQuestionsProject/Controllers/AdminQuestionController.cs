using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MedicalQuestionsProject.DomainModels;

namespace MedicalQuestionsProject.Controllers
{
    public class AdminQuestionController : Controller
    {
        private MedicalQuestionsDatabaseDbContext db = new MedicalQuestionsDatabaseDbContext();

        // GET: AdminQuestion
        public ActionResult Index()
        {
            var questions = db.Questions.Include(q => q.Category).Include(q => q.User);
            return View(questions.ToList());
        }


        // GET: AdminQuestion/Create
        public ActionResult Create()
        {
            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "CategoryName");
            ViewBag.UserID = new SelectList(db.Users, "UserID", "Email");
            return View();
        }

        // POST: AdminQuestion/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "QuestionID,QuestionName,QuestionDateAndTime,UserID,CategoryID,VotesCount,AnswersCount,ViewsCount")] Question question)
        {
            if (ModelState.IsValid)
            {
                db.Questions.Add(question);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "CategoryName", question.CategoryID);
            ViewBag.UserID = new SelectList(db.Users, "UserID", "Email", question.UserID);
            return View(question);
        }

        // GET: AdminQuestion/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Question question = db.Questions.Find(id);
            if (question == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "CategoryName", question.CategoryID);
            ViewBag.UserID = new SelectList(db.Users, "UserID", "Email", question.UserID);
            return View(question);
        }

        // POST: AdminQuestion/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "QuestionID,QuestionName,QuestionDateAndTime,UserID,CategoryID,VotesCount,AnswersCount,ViewsCount")] Question question)
        {
            if (ModelState.IsValid)
            {
                db.Entry(question).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "CategoryName", question.CategoryID);
            ViewBag.UserID = new SelectList(db.Users, "UserID", "Email", question.UserID);
            return View(question);
        }

        // GET: AdminQuestion/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Question question = db.Questions.Find(id);
            if (question == null)
            {
                return HttpNotFound();
            }
            return View(question);
        }

        // POST: AdminQuestion/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Question question = db.Questions.Find(id);
            db.Questions.Remove(question);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
