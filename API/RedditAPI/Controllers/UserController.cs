using Core.Services;
using DataLayer.Entities;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RedditAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserCollectionService _userCollectionService;

        public UserController(IUserCollectionService userCollectionService)
        {
            _userCollectionService = userCollectionService ?? throw new ArgumentNullException(nameof(UserCollectionService));
        }

        // GET: api/<UserController>
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_userCollectionService.GetAll());
        }

        // GET api/<UserController>/5
        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            return Ok(_userCollectionService.Get(id));
        }

        // POST api/<UserController>
        [HttpPost]
        public IActionResult Post([FromBody] User user)
        {
            _userCollectionService.Create(user);

            return Ok();
        }

        // PUT api/<UserController>/5
        [HttpPut("{id}")]
        public IActionResult Put([FromBody] User user)
        {
            _userCollectionService.Update(user);

            return Ok();
        }

        // DELETE api/<UserController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            _userCollectionService.Delete(id);

            return Ok();
        }
    }
}
