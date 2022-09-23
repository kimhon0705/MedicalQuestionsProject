using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;


namespace MedicalQuestionsProject.DomainModels
{
    public class MedicalQuestionsDatabaseDbContext:DbContext
    {

        public MedicalQuestionsDatabaseDbContext() : base("MedicalQuestionsDatabaseDbContext")
        {

        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Question> Questions { get; set;}
        public DbSet<Answer> Answers { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Vote> Votes { get; set; }
        
    }
}
