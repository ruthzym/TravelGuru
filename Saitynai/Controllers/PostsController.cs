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
    public class PostsController : ControllerBase
    {
        private IPostRepository db = new PostRepository();

        // GET: api/<PostController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Post>>> GetAll()
        {
            return Ok(await db.GetPosts());
        }

        // GET api/<PostController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Post>> GetPost (string id)
        {
            var post = await db.GetPost(id);
            if (post == null)
            {
                return NotFound();
            }
            return Ok(post);
        }

        // POST api/<PostController>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Post post)
        {
            if (post == null)
            {
                return NotFound();
            }
            if (post.Body == string.Empty)
            {
                ModelState.AddModelError("Body", "The body shouldn't be empty");
            }
            await db.Create(post);
            return Created("Created", true);
        }

        // PUT api/<PostController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromBody] Post post, string id)
        {
            if (post == null)
            {
                return NotFound();
            }
            post.Id = new string(id);
            await db.Update(post);
            return NoContent();
        }

        // DELETE api/<PostController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var post = await db.GetPost(id);
            if (post == null)
            {
                return NotFound();
            }
            await db.Delete(id);
            return Ok();
        }
        //******************************************************************************
        [HttpDelete("{id}/comment/{commentId}")]
        public async Task<IActionResult> DeleteComment(string id, string commentId)
        {
            var post = await db.GetPost(id);
            var comm = await db.GetComment(id, commentId);
            if (post == null || comm == null)
            {
                return NotFound();
            }
            await db.DeleteComment(id, commentId);
            return Ok();
        }

        [HttpPut("{id}/comment/{commentId}")]
        public async Task<IActionResult> UpdateComment([FromBody] Comment post, string id, string commentId)
        {
            if (post == null)
            {
                return NotFound();
            }
            post.Id = new string(commentId);
            await db.UpdateComment(post, id, commentId);
            return NoContent();
        }

        [HttpPost("{id}/comment")]
        public async Task<IActionResult> CreateComment([FromBody] Comment post, string id)
        {

            if (post == null)
            {
                return NotFound();
            }
            if (post.Text == string.Empty)
            {
                ModelState.AddModelError("Body", "The body shouldn't be empty");
            }
            await db.CreateComment(post, id);
            return Created("Created", true);
        }

        [HttpGet("{id}/comment")]
        public async Task<ActionResult<IEnumerable<Post>>> GetAllComments(string id)
        {
            var post = await db.GetPost(id);
            if (post == null)
            {
                return NotFound();
            }
            return Ok(await db.GetComments(id));
        }

        // GET api/<PostController>/5
        [HttpGet("{id}/comment/{commentId}")]
        public async Task<ActionResult<Comment>> GetComment(string id, string commentId)
        {
            var post = await db.GetComment(id, commentId);
            if (post == null)
            {
                return NotFound();
            }
            return Ok(post);
        }

    }
}
