using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
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

        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetAll()
        {
            return Ok(await db.GetUsers());
        }

        // GET api/<Users>/5
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
        [HttpDelete("{id}/post/{postId}/comment/{commentId}")]
        public async Task<IActionResult> DeleteComment(string id, string postId, string commentId)
        {
            return Ok();
        }
        // DELETE api/<Users>/5
        [HttpDelete("{id}/post/{postId}")]
        public async Task<IActionResult> DeletePost(string id, string postId)
        {
            return Ok();
        }

        // PUT api/<Users>/5
        [HttpPut("{id}/post/{postId}/comment/{commentId}")]
        public async Task<IActionResult> UpdateComment([FromBody] Comment updatedUser, string id, string postId, string commentId)
        {
            return NoContent();
        }

        // PUT api/<Users>/5
        [HttpPut("{id}/post/{postId}")]
        public async Task<IActionResult> UpdatePost([FromBody] Post updatedUser, string id, string postId)
        {
            return NoContent();
        }

        // POST api/<Users>
        [HttpPost("{id}/post/{postId}/comment")]
        public async Task<IActionResult> CreateComment([FromBody] User user, string id, string postId)
        {
            return Created("Created", true);
        }
        // POST api/<Users>
        [HttpPost("{id}/post")]
        public async Task<IActionResult> CreatePost([FromBody] User user, string id)
        {
            return Created("Created", true);
        }

        [HttpGet("{id}/post/{postId}/comment")]
        public async Task<ActionResult<IEnumerable<User>>> GetAllComments(string id)
        {
            return Ok();
        }

        // GET api/<Users>/5
        [HttpGet("{id}/post/{postId}/comment/{commentId}")]
        public async Task<ActionResult<User>> GetUserComment(string id, string postId, string commentId)
        {
            return Ok();
        }

        [HttpGet("{id}/post")]
        public async Task<ActionResult<IEnumerable<User>>> GetAllPosts(string id)
        {
            return Ok();
        }

        // GET api/<Users>/5
        [HttpGet("{id}/post/{postId}")]
        public async Task<ActionResult<User>> GetUserPost(string id, string postId)
        {
            return Ok();
        }
    }
}
