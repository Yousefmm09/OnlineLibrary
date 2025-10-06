namespace OnlineLibrary.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
   
    public class AccountController : ControllerBase
    {
        private readonly OBDbcontext _dbcontext;
        private readonly UserManager<ApplicationUser>_userManager;
        private readonly SignInManager<ApplicationUser> _signinManager;
        private readonly IConfiguration _configuration;
        private readonly JwtTokenCreation _jwt;
        public AccountController( OBDbcontext oBDbcontext ,UserManager<ApplicationUser> userManager , SignInManager<ApplicationUser> signInManager , JwtTokenCreation jwt)
        {
            _dbcontext = oBDbcontext;
            _userManager = userManager;
            _signinManager = signInManager;
            _jwt = jwt;
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (registerDto.Password != registerDto.ConfirmPassword)
            {
                return BadRequest("The Password and ConfirmPassword do not match");
            }

            var user = new ApplicationUser()
            {
                UserName = registerDto.Name,
                Email = registerDto.Email,
                PhoneNumber = registerDto.PhoneNumber,
            };

            var res = await _userManager.CreateAsync(user, registerDto.Password);

            if (!res.Succeeded)
            {
                var errors = res.Errors.Select(e => e.Description);
                return BadRequest(new { Errors = errors });
            }

            // Assign default role USER
            await _userManager.AddToRoleAsync(user, "USER");

            var customer = new Customer()
            {
                Name = registerDto.Name,
                EmailAddress = registerDto.Email,
                Adress = registerDto.Address,
                PhoneNumber = registerDto.PhoneNumber,
                UserId = user.Id,
            };

            var token= await _userManager.GenerateEmailConfirmationTokenAsync(user);
            var encodedtoken = Uri.EscapeDataString(token);
            _dbcontext.Customers.Add(customer);
            await _dbcontext.SaveChangesAsync();

            return Ok(new
            {
                Token=encodedtoken,
                Message = "User registered successfully"
            });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid model state");

            var user = await _userManager.FindByNameAsync(loginDto.UserName);
            if (user == null)
                return Unauthorized("Invalid username or password");

            var res = await _signinManager.CheckPasswordSignInAsync(user, loginDto.Password, false);
            if (!res.Succeeded)
                return Unauthorized("Invalid username or password");

            var customer = _dbcontext.Customers.FirstOrDefault(c => c.UserId == user.Id);
            if (customer == null)
                return Unauthorized("Customer record not found for this user.");
            var check= await  _userManager.IsEmailConfirmedAsync(user);
            if (check)
            {
                var token = await _jwt.CreateTokenAsync(customer);

                return Ok(new { Token = token });
            }
            return BadRequest("Please,Confirm your email");
        }
        // forgotten password
        [HttpPost("forgottenPassword")]
        [AllowAnonymous]
        public async Task<IActionResult> forgottenPassword(forgottenPasswordDto forgottenPassword)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(forgottenPassword.Email);
                if (user != null)
                {
                    var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                    var encodedToken = Uri.EscapeDataString(token);
                    return Ok(new { Token = encodedToken });
                }
                return NotFound();
            }
            return BadRequest(ModelState);
        }

        // reset password
        [HttpPost("resetPassword")]
        public async Task<IActionResult> resetPassword(ResetPassowordDto resetPassoword)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(resetPassoword.Email);
                if (user != null)
                {
                    var decodedToken = Uri.UnescapeDataString(resetPassoword.token);
                    var res = await _userManager.ResetPasswordAsync(user, decodedToken, resetPassoword.password);

                    if (res.Succeeded)
                        return Ok("Password changed successfully");
                    else
                        return BadRequest(new { Errors = res.Errors.Select(e => e.Description) });
                }
                return NotFound();
            }
            return BadRequest(ModelState);
        }
        [HttpPost("Change Email")]
        public async Task<IActionResult> changeEmail(ChangeEmailDto changeEmail)
        {
            if (ModelState.IsValid)
            {
                var userEmail= await _userManager.FindByEmailAsync(changeEmail.OldEmail);
                if (userEmail != null)
                {
                   userEmail.Email= changeEmail.NewEmail;
                     var res= await _userManager.UpdateAsync(userEmail);
                    if (res.Succeeded)
                        return Ok("Email changed successfully");
                    else
                    {
                        var errors = res.Errors.Select(e => e.Description);
                        return BadRequest(new { Errors = errors });
                    }

                }
            }
            return BadRequest(ModelState);
        }
        // confirm Email
        [HttpPost("ConfirmEmail")]
        public async Task<IActionResult> ConfirmEmail(ConfirmEmailDto confirmEmail)
        {
            if(ModelState.IsValid)
            {
                var user =  await _userManager.FindByEmailAsync(confirmEmail.Email);
                if(user != null)
                {
                    var decodedtoken= Uri.UnescapeDataString(confirmEmail.token);
                    var confirm =  await _userManager.ConfirmEmailAsync(user, decodedtoken);
                    if (confirm.Succeeded)
                        return Ok("your email is successfully Confirmation");
                    else
                        return BadRequest(new { Errors = confirm.Errors.Select(e => e.Description) });
                }
                return NotFound();
            }
            return BadRequest(ModelState);
        }
        [HttpPost("ChangePassword")]
        [Authorize]
        public async Task<IActionResult> ChangePassword(ChangePasswordDto changePassword)
        {
            if (ModelState.IsValid)
            {
                var user =  await _userManager.GetUserAsync(User);
                if (user != null)
                {
                    var change= await _userManager.ChangePasswordAsync(user, changePassword.Password,changePassword.NewPassword);
                    if (change.Succeeded)
                        return Ok("Password changed successfully");
                    else
                        return BadRequest(new { Errors = change.Errors.Select(e => e.Description) });
                }
                else
                    return Unauthorized();
            }
            return BadRequest(ModelState);
        }
        [HttpGet("me")]
        [Authorize]
        public async Task<IActionResult> AccountInformation()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await _userManager.GetUserAsync(User);
            if (user != null)
            {
                var info = await _dbcontext.Customers
                    .Where(x => x.UserId == user.Id)
                    .Include(c => c.Borrows)
                    .ThenInclude(b => b.Book)
                    .Select(c => new
                    {
                        c.Name,
                        c.EmailAddress,
                        MyBorrow = c.Borrows.Select(b => new
                        {
                            b.Book.Title,
                            b.BorrowDate,
                            b.ReturnDate,
                            b.Status
                        })
                    }).ToListAsync();
                return Ok(new
                {
                    Data = info
                });
            }
            else
            {
                return Unauthorized();
            }
        }
    }
}
