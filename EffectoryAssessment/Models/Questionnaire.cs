using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace EffectoryAssessment.Models
{
    public class Texts
    {
        [JsonProperty("nl-NL")]
        public string NL { get; set; }
        [JsonProperty("en-US")]
        public string EN { get; set; }
    }

    public class QuestionnaireSubjectItem
    {
        public int SubjectId { get; set; }
        public int OrderNumber { get; set; }
        public Texts Texts { get; set; }
        public int ItemType { get; set; }
        public List<QuestionnaireQuestionItem> QuestionnaireItems { get; set; }
    }

    public class QuestionnaireQuestionItem
    {
        public int SubjectId { get; set; }
        public int QuestionId { get; set; }
        public int AnswerCategoryType { get; set; }
        public int OrderNumber { get; set; }
        public Texts Texts { get; set; }
        public int ItemType { get; set; }
        public List<QuestionnaireAnswerItem> QuestionnaireItems { get; set; }
    }

    public class QuestionnaireAnswerItem
    {
        public int? AnswerId { get; set; }
        public int QuestionId { get; set; }
        public int AnswerType { get; set; }
        public int OrderNumber { get; set; }
        public Texts Texts { get; set; }
        public int ItemType { get; set; }
        public List<object> QuestionnaireItems { get; set; }
        public int AnswerCategoryType { get; set; }
    }


    public class Questionnaire
    {
        public int QuestionnaireId { get; set; }
        public List<QuestionnaireSubjectItem> QuestionnaireItems { get; set; }
    }

}
