using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EffectoryAssessment.Models
{
    public class AnswerItem
    { 
        public int QuestionId { get; set; }
        public int? AnswerId { get; set; }
        public string Additional { get; set; }
    }

    public class SubjectItem
    {
        public int SubjectId { get; set; }
        public List<AnswerItem> AnswerItems { get; set; }
    }

    public class EmployeeAnswers
    {
        public int QuestionnaireId { get; set; }
        public string EmployeeId { get; set; }
        public string DepartmentId { get; set; }
        public List<SubjectItem> SubjectItems { get; set; }
    }
}
