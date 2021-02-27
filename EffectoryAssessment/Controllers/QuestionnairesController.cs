using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EffectoryAssessment.Models;
using EffectoryAssessment.Repositories;

namespace EffectoryAssessment.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class QuestionnairesController : Controller
    {
        private IDataRepository _dataRepository;

        [HttpGet]
        [Route("")]
        public IActionResult GetQuestionnaire()
        {
            try
            {
                return Ok(_dataRepository.DataSource);
            }
            catch (Exception ex)
            {
                // ToDo: Error logging
                return StatusCode(500);
            }
        }

        // ToDo: There is some ugly replication of code for the pagination. If time permits clean this up.

        [HttpGet]
        [Route("subjects")]
        public IActionResult GetSubject(int offset = 0, int limit = -1)
        {
            try
            {
                // Get the correct subject
                var questionnaire = _dataRepository.DataSource;

                int rangeStart = offset <= questionnaire.QuestionnaireItems.Count ? offset : 0;
                int rangeEnd = limit <= -1 ? questionnaire.QuestionnaireItems.Count : (offset + limit <= questionnaire.QuestionnaireItems.Count ? limit : questionnaire.QuestionnaireItems.Count - offset);

                var subjects = questionnaire.QuestionnaireItems.GetRange(rangeStart, rangeEnd);

                return Ok(subjects);

            }
            catch (Exception ex)
            {
                // ToDo: Error logging
                return StatusCode(500);
            }
        }

        [HttpGet]
        [Route("subjects/{subjectId}")]
        public IActionResult GetSubject(int subjectId)
        {
            try
            {
                // Get the correct subject
                var subject = _dataRepository.DataSource.QuestionnaireItems.Find(c => c.SubjectId == subjectId);

                if (subject == null)
                    return BadRequest();

                return Ok(subject);

            } 
            catch (Exception ex)
            {
                // ToDo: Error logging
                return StatusCode(500);
            }
        }

        [HttpGet]
        [Route("subjects/{subjectId}/questions")]
        public IActionResult GetQuestions(int subjectId, int offset = 0, int limit = -1)
        {
            try
            {
                // Get the correct subject
                var subject = _dataRepository.DataSource.QuestionnaireItems.Find(c => c.SubjectId == subjectId);

                if (subject == null)
                    return BadRequest();

                int rangeStart = offset <= subject.QuestionnaireItems.Count ? offset : 0;
                int rangeEnd = limit <= -1 ? subject.QuestionnaireItems.Count : (offset + limit <= subject.QuestionnaireItems.Count ? limit : subject.QuestionnaireItems.Count - offset);

                var questions = subject.QuestionnaireItems.GetRange(rangeStart, rangeEnd);

                return Ok(questions);

            }
            catch (Exception ex)
            {
                // ToDo: Error logging
                return StatusCode(500);
            }
        }

        [HttpGet]
        [Route("subjects/{subjectId}/questions/{questionsId}")]
        public IActionResult GetQuestiosn(int subjectId, int questionsId)
        {
            try
            {
                // Get the correct subject
                var subject = _dataRepository.DataSource.QuestionnaireItems.Find(c => c.SubjectId == subjectId);

                if (subject == null)
                    return BadRequest();

                var question = subject.QuestionnaireItems.Find(c => c.QuestionId == questionsId);

                if (question == null)
                    return BadRequest();

                return Ok(question);

            }
            catch (Exception ex)
            {
                // ToDo: Error logging
                return StatusCode(500);
            }
        }

        [HttpGet]
        [Route("subjects/{subjectId}/questions/{questionId}/answers")]
        public IActionResult GetAnswers(int subjectId, int questionId, int offset = 0, int limit = -1)
        {
            try
            {
                // Get the correct subject
                var subject = _dataRepository.DataSource.QuestionnaireItems.Find(c => c.SubjectId == subjectId);

                if (subject == null)
                    return BadRequest();

                var question = subject.QuestionnaireItems.Find(c => c.QuestionId == questionId);

                int rangeStart = offset <= subject.QuestionnaireItems.Count ? offset : 0;
                int rangeEnd = limit <= -1 ? subject.QuestionnaireItems.Count : (offset + limit <= subject.QuestionnaireItems.Count ? limit : subject.QuestionnaireItems.Count - offset);

                var answers = question.QuestionnaireItems.GetRange(rangeStart, rangeEnd);

                return Ok(answers);

            }
            catch (Exception ex)
            {
                // ToDo: Error logging
                return StatusCode(500);
            }
        }

        [HttpGet]
        [Route("subjects/{subjectId}/questions/{questionsId}/answers/{answerId}")]
        public IActionResult GetAnswer(int subjectId, int questionsId, int answerId)
        {
            try
            {
                // Get the correct subject
                var subject = _dataRepository.DataSource.QuestionnaireItems.Find(c => c.SubjectId == subjectId);

                if (subject == null)
                    return BadRequest();

                var question = subject.QuestionnaireItems.Find(c => c.QuestionId == questionsId);

                if (question == null)
                    return BadRequest();

                var answer = question.QuestionnaireItems.Find(c => c.AnswerId == answerId);

                if (answer == null)
                    return BadRequest();

                return Ok(answer);

            }
            catch (Exception ex)
            {
                // ToDo: Error logging
                return StatusCode(500);
            }
        }

        public QuestionnairesController(IDataRepository dataRepository)
        {
            _dataRepository = dataRepository;
        }
    }
}
