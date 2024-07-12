using Books.DataContext;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Books.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class UserDetailsController : ControllerBase
    {
        private readonly BooksContext _context;

        public UserDetailsController(BooksContext context)
        {
            _context = context;
        }

        // GET: api/v1/UserDetails
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDetail>>> GetUserDetails()
        {
            return await _context.UserDetails.ToListAsync();
        }

        // GET: api/v1/UserDetails/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserDetail>> GetUserDetail(int id)
        {
            var userDetail = await _context.UserDetails.FindAsync(id);

            if (userDetail == null)
            {
                return NotFound();
            }

            return userDetail;
        }

        // PUT: api/v1/UserDetails/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUserDetail(int id, UserDetailDto userDetailDto)
        {
            if (id != userDetailDto.Id)
            {
                return BadRequest();
            }

            var userDetail = await _context.UserDetails.FindAsync(id);
            if (userDetail == null)
            {
                return NotFound();
            }

            userDetail.UserId = userDetailDto.UserId;
            userDetail.Name = userDetailDto.Name;
            userDetail.Surname = userDetailDto.Surname;
            userDetail.EmployeeId = userDetailDto.EmployeeId;
            userDetail.Manager = userDetailDto.Manager;
            userDetail.Title = userDetailDto.Title;
            userDetail.Department = userDetailDto.Department;
            userDetail.MyProfile = userDetailDto.MyProfile;
            userDetail.WhyIVolunteer = userDetailDto.WhyIVolunteer;
            userDetail.CountryId = userDetailDto.CountryId;
            userDetail.CityId = userDetailDto.CityId;
            userDetail.Availability = userDetailDto.Availability;
            userDetail.LinkdInUrl = userDetailDto.LinkdInUrl;
            userDetail.MySkills = userDetailDto.MySkills;
            userDetail.UserImage = userDetailDto.UserImage;
            userDetail.Status = userDetailDto.Status;

            _context.Entry(userDetail).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserDetailExists(id))
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

        // POST: api/v1/UserDetails
        [HttpPost]
        public async Task<ActionResult<UserDetail>> PostUserDetail(UserDetailDto userDetailDto)
        {
            var userDetail = new UserDetail
            {
                UserId = userDetailDto.UserId,
                Name = userDetailDto.Name,
                Surname = userDetailDto.Surname,
                EmployeeId = userDetailDto.EmployeeId,
                Manager = userDetailDto.Manager,
                Title = userDetailDto.Title,
                Department = userDetailDto.Department,
                MyProfile = userDetailDto.MyProfile,
                WhyIVolunteer = userDetailDto.WhyIVolunteer,
                CountryId = userDetailDto.CountryId,
                CityId = userDetailDto.CityId,
                Availability = userDetailDto.Availability,
                LinkdInUrl = userDetailDto.LinkdInUrl,
                MySkills = userDetailDto.MySkills,
                UserImage = userDetailDto.UserImage,
                Status = userDetailDto.Status
            };

            _context.UserDetails.Add(userDetail);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUserDetail", new { id = userDetail.Id }, userDetail);
        }

        // DELETE: api/v1/UserDetails/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserDetail(int id)
        {
            var userDetail = await _context.UserDetails.FindAsync(id);
            if (userDetail == null)
            {
                return NotFound();
            }

            _context.UserDetails.Remove(userDetail);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserDetailExists(int id)
        {
            return _context.UserDetails.Any(e => e.Id == id);
        }

        [HttpPost("registerUser")]
        public async Task<IActionResult> RegisterUser([FromBody] RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = new User
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                PhoneNumber = model.PhoneNumber,
                EmailAddress = model.EmailAddress,
                UserType = model.UserType,
                Password = model.Password,
                UserFullName = $"{model.FirstName} {model.LastName}"
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            // Perform the operation on the client side
            var userDetails = await _context.UserDetails.ToListAsync();
            int maxEmployeeId = userDetails
                .Where(ud => ud.EmployeeId.Length > 3 && int.TryParse(ud.EmployeeId.Substring(3), out _))
                .Select(ud => int.Parse(ud.EmployeeId.Substring(3)))
                .DefaultIfEmpty(0)
                .Max();


            // After creating the User record, you can create a UserDetail record
            var userDetail = new UserDetail
            {
                UserId = user.Id,
                Name = user.FirstName,
                Surname = user.LastName,
                EmployeeId = $"EMP{(maxEmployeeId + 1):000}",
                Manager = "Sample Manager",
                Title = string.Empty,
                Department = "IT Department ",
                MyProfile = string.Empty,
                WhyIVolunteer = string.Empty,
                CountryId = 0,
                CityId = 0,
                Availability = string.Empty,
                LinkdInUrl = string.Empty,
                MySkills = string.Empty,
                UserImage = string.Empty,
                Status = true
            };

            _context.UserDetails.Add(userDetail);
            await _context.SaveChangesAsync();

            // Return a custom response object
            return Ok(new { Message = "User registered successfully" });
        }

        [HttpGet("getUserById/{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            var user = await _context.Users
                                     .Include(u => u.UserDetails)
                                     .FirstOrDefaultAsync(u => u.Id == id);

            if (user == null)
            {
                return NotFound(new { Message = "User not found" });
            }

            var userDetails = user.UserDetails.FirstOrDefault();

            var result = new
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                PhoneNumber = user.PhoneNumber,
                EmailAddress = user.EmailAddress,
                UserType = user.UserType,
                Password = user.Password,
                ConfirmPassword = user.Password, // assuming you want to return the same password for both fields
                UserDetails = new
                {
                    userDetails.Name,
                    userDetails.Surname,
                    userDetails.EmployeeId,
                    userDetails.Manager,
                    userDetails.Title,
                    userDetails.Department,
                    userDetails.MyProfile,
                    userDetails.WhyIVolunteer,
                    userDetails.CountryId,
                    userDetails.CityId,
                    userDetails.Availability,
                    userDetails.LinkdInUrl,
                    userDetails.MySkills,
                    userDetails.UserImage,
                    userDetails.Status
                }
            };

            return Ok(new { result });
        }


        [HttpPut("updateUser")]
        public async Task<IActionResult> UpdateUser([FromBody] UpdateUserViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await _context.Users.FindAsync(model.Id);
            if (user == null)
            {
                return NotFound(new { Message = "User not found" });
            }

            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.PhoneNumber = model.PhoneNumber;
            user.EmailAddress = model.EmailAddress;
            user.Password = model.Password;
            user.UserFullName = $"{model.FirstName} {model.LastName}";
            user.UserType = "user";

            _context.Users.Update(user);

            var userDetail = await _context.UserDetails.SingleOrDefaultAsync(ud => ud.UserId == user.Id);
            if (userDetail != null)
            {
                userDetail.Name = user.FirstName;
                userDetail.Surname = user.LastName;
                userDetail.EmployeeId = userDetail.EmployeeId;
                userDetail.Manager = userDetail.Manager;
                userDetail.Title = userDetail.Title;
                userDetail.Department = userDetail.Department;
                userDetail.MyProfile = userDetail.MyProfile;
                userDetail.WhyIVolunteer = userDetail.WhyIVolunteer;
                userDetail.CountryId = userDetail.CountryId;
                userDetail.CityId = userDetail.CityId;
                userDetail.Availability = userDetail.Availability;
                userDetail.LinkdInUrl = userDetail.LinkdInUrl;
                userDetail.MySkills = userDetail.MySkills;
                userDetail.UserImage = userDetail.UserImage;
                userDetail.Status = userDetail.Status;

                _context.UserDetails.Update(userDetail);
            }
            else
            {
                return NotFound(new { Message = "User details not found" });
            }

            await _context.SaveChangesAsync();

            return Ok(new { Message = "User updated successfully" });
        }



    }


    public class UserDetailDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string EmployeeId { get; set; }
        public string Manager { get; set; }
        public string Title { get; set; }
        public string Department { get; set; }
        public string MyProfile { get; set; }
        public string WhyIVolunteer { get; set; }
        public int CountryId { get; set; }
        public int CityId { get; set; }
        public string Availability { get; set; }
        public string LinkdInUrl { get; set; }
        public string MySkills { get; set; }
        public string UserImage { get; set; }
        public bool Status { get; set; }
    }
}