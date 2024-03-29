﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MedicalQuestionsProject.ViewModels
{
    public class VoteViewModel
    {
        public int VoteID { get; set; }
        public int UserID { get; set; }
        public int AnswerID { get; set; }
        public bool IsTrue { get; set; }
        public int VoteValue { get; set; }
    }
}