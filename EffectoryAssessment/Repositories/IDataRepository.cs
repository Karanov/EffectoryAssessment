using EffectoryAssessment.Models;
using System.Collections.Generic;

namespace EffectoryAssessment.Repositories
{
    public interface IDataRepository
    {
        Questionnaire DataSource { get; }

        List<EmployeeAnswers> AnswersSource { get; set; }

        List<string> Departments { get; }
    }
}