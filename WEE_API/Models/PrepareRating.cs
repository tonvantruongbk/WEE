using System.Collections.Generic;

namespace WEE_API.Models
{
    public class PrepareRating
    {
        public IEnumerable<Answer> ListAnswer { get; set; }
        public IEnumerable<QuestionType> ListQuestionType { get; set; }
        public IEnumerable<SalaryLevel> ListSalaryLevel { get; set; }

        public IEnumerable<WorkingStatus> ListWorkingStatus { get; set; }
        public IEnumerable<WorkingTime> ListWorkingTime { get; set; }
        public IEnumerable<JobPosition> ListJobPosition { get; set; } 

    }
}