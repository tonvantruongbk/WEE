using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using Microsoft.Ajax.Utilities;
using WEE_API.Models;

namespace WEE_API.Controllers.API
{

    public class PrepareRatingController : ApiController
    {
        private DBContext db = new DBContext();
        private PrepareRating result = new PrepareRating();
        public PrepareRating GetPrepareRating()
        {

            result = new PrepareRating
            {
                ListAnswer = db.Answer.Include(a => a.ListAnswerDetail).AsEnumerable(),
                ListJobPosition = db.JobPosition.AsEnumerable(),
                ListQuestionType = db.QuestionType.Include(a=>a.ListQuestion).AsEnumerable(),
                ListSalaryLevel = db.SalaryLevel.AsEnumerable(),
                ListWorkingStatus = db.WorkingStatus.AsEnumerable(),
                ListWorkingTime = db.WorkingTime.AsEnumerable()
            };
            return result;
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
                result = null;
                db = null;
            }

            base.Dispose(disposing);
        }
    }
}