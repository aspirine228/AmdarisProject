using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;
using GameTracker.API.Infrastructure.Config;
using GameTracker.Common.Dtos.Account;
using GameTracker.Domain.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Linq;
using Microsoft.IdentityModel.Tokens;
using GameTracker.Services.Interfaces;
using GameTracker.Common.Dtos.Company;

namespace GameTracker.API.Controllers
{
    [Route("api/account")]
    public class AccountController :BaseController
    {
        private readonly AuthOptions _authOptions;
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager; 
        private readonly ICompanyService _companyService;
        public AccountController(IOptions<AuthOptions> authenticationOptions, SignInManager<User> signInManager,UserManager<User> userManager, RoleManager<Role> roleManager, ICompanyService companyService
            )
        {
            _authOptions = authenticationOptions.Value;
            _signInManager = signInManager;
            _userManager = userManager;
            _roleManager = roleManager;
            _companyService = companyService;
        }


        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login(UserForLoginDto userForLoginDto)
        {
            var checkingPasswordResult = await _signInManager.PasswordSignInAsync(userForLoginDto.Username, userForLoginDto.Password, false, false);

            if (checkingPasswordResult.Succeeded)
            {
                var signinCredentials = new SigningCredentials(_authOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256);
                var jwtSecurityToken = new JwtSecurityToken(
                     issuer: _authOptions.Issuer,
                     audience: _authOptions.Audience,
                     claims: new List<Claim>(),
                     expires: DateTime.Now.AddDays(30),
                     signingCredentials: signinCredentials
                );

                var tokenHandler = new JwtSecurityTokenHandler();

                var encodedToken = tokenHandler.WriteToken(jwtSecurityToken);

                return Ok(new { AccessToken = encodedToken });
            }

            return Unauthorized();
        }


        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register(UserForRegisterDto userForRegisterDto)
        {           
            if (ModelState.IsValid)
            {
                User user = new User { UserName = userForRegisterDto.UserName, PasswordHash = userForRegisterDto.Password, PhoneNumber = userForRegisterDto.PhoneNumber };

                var result = await _userManager.CreateAsync(user, userForRegisterDto.Password);
               
                if (result.Succeeded)
                {
                    // установка куки
                    await _userManager.AddToRoleAsync(user, "gamer");
                    await _signInManager.SignInAsync(user, false);
                    return RedirectToAction("login", "Account");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }
            return Ok(userForRegisterDto);
        }


        [AllowAnonymous]
        [HttpPost("register/company")]
        public async Task<IActionResult> RegisterCompany(CompanyForRegisterDto companyForRegister)
        {
            if (ModelState.IsValid)
            {
                User user = new User { UserName = companyForRegister.CompanyName, PasswordHash = companyForRegister.Password, Email = companyForRegister.CompanyEmail };
                CompanyDto company = new CompanyDto { CompanyName = companyForRegister.CompanyName };
           
                var result = await _userManager.CreateAsync(user, companyForRegister.Password);
                if (result.Succeeded)
                {                                   
                    await _userManager.AddToRoleAsync(user, "company");
                    await _companyService.CreateCompany(company);
                    await _signInManager.SignInAsync(user, false);
                    return RedirectToAction("login", "Account");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }
            return Ok(companyForRegister);
        }


        [HttpPatch("Update")]
        public async Task<IActionResult> UpdateUserPhone(UserDetailsDto user)
        {
            if (ModelState.IsValid)
            {               
                var gottenUser = await _userManager.FindByIdAsync(user.Id.ToString());
              
                var result = await _userManager.SetPhoneNumberAsync(gottenUser, user.PhoneNumber);
              
                if (result.Succeeded)
                {                
                    return RedirectToAction("login", "Account");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }
            return Ok();
        }

              
        [HttpGet("role/{id}")]
        public async Task<UserRoleDto> GetUserRole(int id)
        {
           var user= await this._userManager.FindByIdAsync(id.ToString());
            if (user != null)
            {
                var role = await this._userManager.GetRolesAsync(user);
                if (role != null)
                {

                    return new UserRoleDto { RoleName = role.First() };
                }
                else
                {
                    throw new Exception("No Role");
                }
            }
            throw new Exception("No User");          
        }


        [HttpDelete("delete/{id}")]
        public async Task RemoveUser(int id)
        {
            var user =await _userManager.FindByIdAsync(id.ToString());
            await _userManager.DeleteAsync(user);

        }


        [HttpGet("{userName}")]
        public async Task<UserDetailsDto> GetUserIdByUserName(string userName)
        {
            var user = await this._userManager.FindByNameAsync(userName);
            if (user != null)
            {
                return new UserDetailsDto { Id= user.Id , PhoneNumber=user.PhoneNumber, UserName=user.UserName};
            }
            throw new Exception("No User");
        }

    }
    
}
