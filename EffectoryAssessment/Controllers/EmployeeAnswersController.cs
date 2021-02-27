using EffectoryAssessment.Models;
using EffectoryAssessment.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EffectoryAssessment.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EmployeeAnswersController : Controller
    {
        public IDataRepository _dataRepository;

        [HttpGet]
        [Route("")]
        public IActionResult GetEmployeeAnswer()
        {
            try
            {
                return Ok(_dataRepository.AnswersSource);
            }
            catch (Exception ex)
            {
                // ToDo: Error logging
                return StatusCode(500);
            }
        }

        [HttpPost]
        [Route("")]
        public IActionResult PostEmployeeAnswers([FromBody] EmployeeAnswers employeeAnswers)
        {
            try
            {
                if (employeeAnswers == null)
                    // Unable to deserialize the object
                    return BadRequest();

                //ToDo: Validation
                if (!_dataRepository.Departments.Contains(employeeAnswers.DepartmentId))
                    return BadRequest();

                _dataRepository.AnswersSource.Add(employeeAnswers);

                return Ok();
            }
            catch (Exception ex)
            {
                // ToDo: Error logging
                return StatusCode(500);
            }
        }

        [HttpGet]
        [Route("averages/{subjectId}/{questionId}/{departmentId}")]
        public IActionResult GetDepartmentAverage(int subjectId, int questionId, string departmentId)
        {
            try
            {
                if (!_dataRepository.Departments.Contains(departmentId))
                    return BadRequest();


                var subject = _dataRepository.DataSource.QuestionnaireItems.Find(c => c.SubjectId == subjectId);

                if (subject == null)
                    return BadRequest();

                var questionSource = subject.QuestionnaireItems.Find(c => c.QuestionId == questionId);

                if (questionSource == null)
                    return BadRequest();

                // I know this would be neater as decent Linq statement
                var questionAnswers = _dataRepository.AnswersSource
                    .FindAll(c => c.DepartmentId == departmentId)
                    .SelectMany(c => c.SubjectItems).ToList()
                    .FindAll(c => c.SubjectId == subjectId)
                    .SelectMany(c => c.AnswerItems).ToList()
                    .FindAll(c => c.QuestionId == questionId);

      
                int total = 0;

                foreach (AnswerItem item in questionAnswers)
                {
                    // Since I don't have the scoring master data I'll be using the order number.
                    var answerSource = questionSource.QuestionnaireItems.Find(c => c.AnswerId == item.AnswerId);
                    total += answerSource.OrderNumber;
                }

                int average = questionAnswers.Count > 0 ? total / questionAnswers.Count() : 0;

                return Ok(new {
                    average,
                    texts = questionAnswers.Count > 0 ? questionSource.QuestionnaireItems.Find(c => c.OrderNumber == average).Texts : null
                });
            }
            catch (Exception ex)
            {
                // ToDo: Error logging
                return StatusCode(500);
            }
        }

        public EmployeeAnswersController(IDataRepository dataRepository)
        {
            _dataRepository = dataRepository;
        }
    }
}
