using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BackendTravelApp.Models;
using BackendTravelApp.Services.EmailService;
using Microsoft.AspNetCore.WebUtilities;
using BackendTravelApp.Services;
using BackendTravelApp.Models.Authentification;
using NETCore.MailKit.Core;


namespace BackendTravelApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly TravelContext _context;
        private readonly IEmailSender _emailSender;

        public UsersController(TravelContext context, IEmailSender emailSender)
        {
            _context = context;
            _emailSender = emailSender;
        }

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Users>>> GetUsers()
        {
            return await _context.Users.ToListAsync();
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Users>> GetUserById(int id)
        {
            var users = await _context.Users.FindAsync(id);

            if (users == null)
            {
                return NotFound();
            }

            return users;
        }

        [Route("username")]
        [HttpGet]
        public async Task<ActionResult<Users>> GetUserByUsername(string username)
        {
            var loggedUser = await _context.Users.FirstOrDefaultAsync(x => (x.Username == username));

            if (loggedUser == null)
            {
                return NotFound();
            }

            return loggedUser;
        }

        // PUT: api/Users/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUsers(int id, Users users)
        {
            if (id != users.UserId)
            {
                return BadRequest();
            }

            _context.Entry(users).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UsersExists(id))
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

        // POST: api/Users
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Users>> PostUsers(Users user)
        {
            _context.Users.Add(user);
            user.Role = "USER";
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUsers", new { id = user.UserId }, user);
        }

        // POST: api/Users/login
        [HttpPost("Login")]
        public async Task<ActionResult<Users>> LoginUser(UserLogin user)
        {
            var loggedInUser = await _context.Users.FirstOrDefaultAsync(x => (x.Username == user.Username && x.Password == user.Password));

            if (loggedInUser == null)
            {
                return BadRequest("Parola sau numele utilizatorului nu au fost gasite");
            }

            return loggedInUser;
        }


        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUsers(int id)
        {
            var users = await _context.Users.FindAsync(id);
            if (users == null)
            {
                return NotFound();
            }

            _context.Users.Remove(users);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPost("ForgotPassword")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordDto forgotPasswordDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var user = await _context.Users.FirstOrDefaultAsync(x => x.Email == forgotPasswordDto.Email);
            if (user == null)
                return BadRequest("Invalid Request");

            var token = "";
            var param = new Dictionary<string, string>
            {
                {"token", token },
                {"email", forgotPasswordDto.Email }
            };

            var callback = QueryHelpers.AddQueryString(forgotPasswordDto.ClientURI, param);

            var message = new Message(new string[] { forgotPasswordDto.Email }, "Resetează parola", "Accesează link-ul de mai jos pentru a reseta parola.\n\n" + callback, null);
            await _emailSender.SendEmailAsync(message);

            return Ok();
        }

        [HttpPost("ResetPassword")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordDto resetPasswordDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var user = await _context.Users.FirstOrDefaultAsync(x => x.Email == resetPasswordDto.Email);
            if (user == null)
            {
                return BadRequest("Invalid Request");
            }

            if (await TryUpdateModelAsync<Users>(user, "", x => x.Password))
            {
                try
                {
                    user.Password = resetPasswordDto.Password;
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateException)
                {
                    ModelState.AddModelError("", "Unable to save changes. " +
                        "Try again, and if the problem persists, " +
                        "see your system administrator.");
                }
            }

            return Ok();
        }

        [HttpPut("UpdateUser")]
        public async Task<IActionResult> UpdateUser([FromBody] UpdateUser updateUser)
        {
            Console.WriteLine(updateUser.FirstLastName);
            Console.WriteLine(updateUser.PhoneNumber);
            Console.WriteLine(updateUser.Email);

            if (!ModelState.IsValid)
                return BadRequest();

            var user = await _context.Users.FirstOrDefaultAsync(x => x.UserId == updateUser.UserId);
            Console.WriteLine(user.UserId);
            Console.WriteLine(updateUser.UserId);
            if (user == null)
            {
                return BadRequest("Invalid Request");
            }

            if (await TryUpdateModelAsync<Users>(user, "", x => x.FirstLastName) && await TryUpdateModelAsync<Users>(user, "", x => x.PhoneNumber)
                && await TryUpdateModelAsync<Users>(user, "", x => x.Email))
            {
                try
                {
                    user.FirstLastName = updateUser.FirstLastName;
                    user.PhoneNumber = updateUser.PhoneNumber;
                    user.Email = updateUser.Email;
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateException)
                {
                    ModelState.AddModelError("", "Unable to save changes. " +
                        "Try again, and if the problem persists, " +
                        "see your system administrator.");
                }
            }

            return Ok();
        }

        private bool UsersExists(int id)
        {
            return _context.Users.Any(e => e.UserId == id);
        }
    }
}
