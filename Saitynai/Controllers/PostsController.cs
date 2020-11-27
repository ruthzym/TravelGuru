using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
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
        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Post>>> GetAll()
        {
            var x = await db.GetPosts();
            if (x == null)
            {
                return NotFound();
            }

            return Ok(x);
        }
        [AllowAnonymous]
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
        [Authorize]
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
        [Authorize]
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
        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = UserRoles.Admin + UserRoles.Registered)]
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
        [Authorize]
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
        [Authorize]
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
        [Authorize]
        [HttpGet("{id}/comment")]
        public async Task<ActionResult<IEnumerable<Post>>> GetAllComments(string id)
        {
            var post = await db.GetPost(id);
            if (post == null)
            {
                return NotFound();
            }
            var x = await db.GetComments(id);
            if (x == null)
            {
                return NotFound();
            }
            return Ok(x);
        }

        // GET api/<PostController>/5
        [Authorize]
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
