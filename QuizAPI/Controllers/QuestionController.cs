﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuizAPI.Models;
using QuizAPI._dbContext;

namespace QuizAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionController : ControllerBase
    {
        private readonly QuizDbContext _context;

        public QuestionController(QuizDbContext context)
        {
            _context = context;
        }

        // GET: api/Question
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Questions>>> GetQuestions()
        {
            var randomxQns = await (_context.Questions
                 .Select(x => new
                 {
                     QnId = x.QnId,
                     QnInWords = x.QnInWords,
                     ImageName = x.ImageName,
                     Options = new string[] { x.Option1, x.Option2, x.Option3, x.Option4 }
                 })
                 .OrderBy(y => Guid.NewGuid())
                 .Take(10)
                 ).ToListAsync();
            return Ok(randomxQns);
        }

        // GET: api/Question/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Questions>> GetQuestions(int id)
        {
          if (_context.Questions == null)
          {
              return NotFound();
          }
            var questions = await _context.Questions.FindAsync(id);

            if (questions == null)
            {
                return NotFound();
            }

            return questions;
        }

        // PUT: api/Question/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutQuestions(int id, Questions questions)
        {
            if (id != questions.QnId)
            {
                return BadRequest();
            }

            _context.Entry(questions).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!QuestionsExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Question/GetAnswers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Route("GetAnswers")]
        public async Task<ActionResult<Questions>> RetrieveAnswers(int[] qnsId)
        {
            var answers = await (_context.Questions
                  .Where(x => qnsId.Contains(x.QnId))
                  .Select(y => new
                  {
                      QnId = y.QnId,
                      QnInWords = y.QnInWords,
                      ImageName = y.ImageName,
                      Options = new string[] { y.Option1, y.Option2, y.Option3, y.Option4 },
  
                      Answer = y.Answer
                  })).ToListAsync();
            return Ok(answers);
        }

        // DELETE: api/Question/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteQuestions(int id)
        {
            if (_context.Questions == null)
            {
                return NotFound();
            }
            var questions = await _context.Questions.FindAsync(id);
            if (questions == null)
            {
                return NotFound();
            }

            _context.Questions.Remove(questions);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool QuestionsExists(int id)
        {
            return (_context.Questions?.Any(e => e.QnId == id)).GetValueOrDefault();
        }
    }
}
