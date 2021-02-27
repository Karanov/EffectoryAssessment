using EffectoryAssessment.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Hosting;

namespace EffectoryAssessment.Repositories
{
    public class DataRepository : IDataRepository
    {
        private Questionnaire _questionnaireSource;

        public DataRepository(IWebHostEnvironment env)
        {
            string filePath = @$"{env.ContentRootPath}\bin\{(System.Diagnostics.Debugger.IsAttached ? "Debug\\net5.0\\" : "")}Data\questionnaire.json";
            _questionnaireSource = JsonConvert.DeserializeObject<Questionnaire>(File.ReadAllText(filePath));

            AnswersSource = new List<EmployeeAnswers>();
        }

        public Questionnaire DataSource { get { return _questionnaireSource; } }

        public List<EmployeeAnswers> AnswersSource { get; set; }

        public List<string> Departments { get { return new List<string>() { "marketing", "sales", "development", "reception" }; }}
    }
}
