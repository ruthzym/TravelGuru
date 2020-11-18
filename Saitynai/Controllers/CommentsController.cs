using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using Saitynai.Models;
using Saitynai.Repository;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Saitynai.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        ICommentRepository db = new CommentRepository();

        // GET: api/<CommentsController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Comment>>> GetAll()
        {
            return Ok(await db.GetComments());
        }

        // GET api/<CommentsController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Comment>> GetComment(string id)
        {
            var comment = await db.GetComment(id);
            if (comment == null)
            {
                return NotFound();
            }
            return Ok(comment);
        }

        // POST api/<CommentController>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Comment comment)
        {
            if (comment == null)
            {
                return BadRequest();
            }
            if (comment.Text == string.Empty)
            {
                ModelState.AddModelError("Text", "The text shouldn't be empty");
            }
            await db.Create(comment);
            return Created("Created", true);
        }

        // PUT api/<CommentController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromBody] Comment comment, string id)
        {
            if (comment == null)
            {
                return NotFound();
            }
            comment.Id = new string(id);
            await db.Update(comment);
            return NoContent();
        }
        // DELETE api/<CommentController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var comment = await db.GetComment(id);
            if (comment == null)
            {
                return NotFound();
            }
            await db.Delete(id);
            return Ok();
        }
    }
}
