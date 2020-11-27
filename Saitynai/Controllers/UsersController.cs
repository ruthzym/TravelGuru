using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MongoDB.Bson;
using MongoDB.Driver;
using Saitynai.Models;
using Saitynai.Repository;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Saitynai.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private IUserRepository db = new UserRepository();

        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetAll()
        {
            var users = await db.GetUsers();
            if (users == null)
            {
                return NotFound();
            }
            return Ok(users);
        }

        // GET api/<Users>/5
        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(string id)
        {
            var user = await db.GetUser(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        // POST api/<Users>
        [AllowAnonymous]
        [HttpPost("signup")]
        public async Task<IActionResult> Signup([FromBody] UserCredentials cred)
        {
            User user = new User();
            if (cred == null)
            {
                return NotFound();
            }
            if (cred.Email == string.Empty)
            {
                ModelState.AddModelError("Name", "The name shouldn't be empty");
            }
            user.Email = cred.Email;
            user.Password = cred.Password;
            user.UserName = cred.Username;
            user.Role = "User";
            await db.Create(user);
            return Created("Created", true);
        }
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] User user)
        {
            if (user == null)
            {
                return NotFound();
            }
            if (user.Name == string.Empty)
            {
                ModelState.AddModelError("Name", "The name shouldn't be empty");
            }
            await db.Create(user);
            return Created("Created", true);
        }

        // PUT api/<Users>/5
        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromBody] User updatedUser, string id)
        {
            if (updatedUser == null)
            {
                return NotFound();
            }
            updatedUser.Id = new string(id);
            await db.Update(updatedUser);
            return NoContent();
        }

        // DELETE api/<Users>/5
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var user = await db.GetUser(id);
            if (user == null)
            {
                return NotFound();
            }
            await db.Delete(id);
            return Ok();
        }
        ///*********************************************************************
        // DELETE api/<Users>/5
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}/post/{postId}/comment/{commentId}")]
        public async Task<IActionResult> DeleteComment(string id, string postId, string commentId)
        {
            var post = await db.GetUser(id);
            var comm = await db.GetPost(id, postId);
            var x = await db.GetPostComment(id, postId, commentId);
            if (post == null || comm == null || x == null)
            {
                return NotFound();
            }
            await db.DeletePostComment(id, postId, commentId);
            return Ok();
        }
        // DELETE api/<Users>/5
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}/post/{postId}")]
        public async Task<IActionResult> DeletePost(string id, string postId)
        {
            var post = await db.GetUser(id);
            var comm = await db.GetPost(id, postId);
            if (post == null || comm == null)
            {
                return NotFound();
            }
            await db.DeletePost(id, postId);
            return Ok();
        }

        // PUT api/<Users>/5
        [Authorize]
        [HttpPut("{id}/post/{postId}/comment/{commentId}")]
        public async Task<IActionResult> UpdateComment([FromBody] Comment post, string id, string postId, string commentId)
        {
            if (post == null)
            {
                return NotFound();
            }
            post.Id = commentId;
            await db.UpdatePostComment(post, id, postId, commentId);
            return NoContent();
        }

        // PUT api/<Users>/5
        [Authorize]
        [HttpPut("{id}/post/{postId}")]
        public async Task<IActionResult> UpdatePost([FromBody] Post post, string id, string postId)
        {
            if (post == null)
            {
                return NotFound();
            }
            post.Id = new string(postId);
            await db.UpdatePost(post, id, postId);
            return NoContent();
        }

        // POST api/<Users>
        [Authorize]
        [HttpPost("{id}/post/{postId}/comment")]
        public async Task<IActionResult> CreateComment([FromBody] Comment comment, string id, string postId)
        {
            if (comment == null)
            {
                return NotFound();
            }
            if (comment.Text == string.Empty)
            {
                ModelState.AddModelError("Body", "The body shouldn't be empty");
            }
            await db.CreatePostComment(comment, id, postId);
            return Created("Created", true);
        }
        // POST api/<Users>
        [Authorize]
        [HttpPost("{id}/post")]
        public async Task<IActionResult> CreatePost([FromBody] Post post, string id)
        {
            if (post == null)
            {
                return NotFound();
            }
            if (post.Body == string.Empty)
            {
                ModelState.AddModelError("Body", "The body shouldn't be empty");
            }
            await db.CreatePost(post, id);
            return Created("Created", true);
        }
        [Authorize]
        [HttpGet("{id}/post/{postId}/comment")]
        public async Task<ActionResult<IEnumerable<Comment>>> GetAllComments(string id, string postId)
        {
            var post = await db.GetUser(id);
            if (post == null)
            {
                return NotFound();
            }
            var all = await db.GetPostComments(id, postId);
            if (all == null)
            {
                return NotFound();
            }
            return Ok(all);
        }
        [Authorize]
        // GET api/<Users>/5
        [HttpGet("{id}/post/{postId}/comment/{commentId}")]
        public async Task<ActionResult<Comment>> GetUserComment(string id, string postId, string commentId)
        {
            var post = await db.GetPostComment(id, postId, commentId);
            if (post == null)
            {
                return NotFound();
            }
            return Ok(post);
        }
        [AllowAnonymous]
        [HttpGet("{id}/post")]
        public async Task<ActionResult<IEnumerable<Post>>> GetAllPosts(string id)
        {
            var post = await db.GetUser(id);
            if (post == null)
            {
                return NotFound();
            }
            var allposts = await db.GetPosts(id);
            if (allposts == null)
            {
                return NotFound();
            }
            return Ok(allposts);
        }
        [AllowAnonymous]
        // GET api/<Users>/5
        [HttpGet("{id}/post/{postId}")]
        public async Task<ActionResult<User>> GetUserPost(string id, string postId)
        {
            var post = await db.GetPost(id, postId);
            if (post == null)
            {
                return NotFound();
            }
            return Ok(post);
        }
    }
}
