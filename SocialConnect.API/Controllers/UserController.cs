﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SocialConnect.Core.DTO;
using SocialConnect.Core.Models;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;

namespace SocialConnect.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        UserManager<IdentityUser> userManager;
        private readonly RoleManager<IdentityRole> role;

        public UserController(UserManager<IdentityUser> userManager,RoleManager<IdentityRole>role)
        {
            this.userManager = userManager;
            this.role = role;
        }
        [HttpPost]
        [SwaggerOperation(
            Summary = "Add new User",
            Description = "Registeration to the system"
        )]
        [SwaggerResponse(201, "User Add successfully", typeof(RegisterUser))]
        [SwaggerResponse(400, "Invalid User data")]
        public IActionResult AddUser(RegisterUser model)
        {
            if (ModelState.IsValid)
            {
                User user = new User()
                {
                    Email = model.Email,
                    UserName = model.Email,
                    FullName=model.username,
                    PhoneNumber = model.PhoneNumber,
                    address = model.Address,


                };
                IdentityResult r= userManager.CreateAsync(user,model.password).Result;
                if (r.Succeeded)
                {
                 IdentityRole ro=  role.FindByNameAsync("User").Result;
                    if (ro != null)
                    {
                        IdentityResult rr = userManager.AddToRoleAsync(user, ro.Name).Result;
                        if (rr.Succeeded)
                        {
                            return Created();
                        }
                        else return BadRequest(rr.Errors);
                    }
                    else return BadRequest("Role Not Found");
                }
                else return BadRequest(r.Errors);
            }

            return BadRequest(ModelState.ErrorCount);
        }
        [HttpGet]
        [SwaggerOperation(
            Summary = "Get all User",
            Description = "Retrieve a list of User "
        )]
        [SwaggerResponse(200, "Successfully retrieved User", typeof(List<UserDto>))]

        public IActionResult Get()
        {
            // var Users = db.Userrepository.Selectall();
            var Users = userManager.GetUsersInRoleAsync("User").Result.OfType<User>().ToList();
            List<UserDto> Usersdto = new List<UserDto>();
            foreach (var User in Users)
            {
                var cs = new UserDto(){ 
                Id = User.Id,
                username = User.FullName,
                Email = User.Email,
                Address = User.address,
                PhoneNumber = User.PhoneNumber};

                Usersdto.Add(cs);
            }
            if (Usersdto.Count == 0)
                return NotFound();
            return Ok(Usersdto);
        }
        [HttpGet("{id}")]
        [SwaggerOperation(
            Summary = "Get User by ID",
            Description = "Retrieve a specific User using its unique identifier"
        )]
        [SwaggerResponse(200, "User found", typeof(UserDto))]
        [SwaggerResponse(404, "User not found")]
        public IActionResult GetbyId(string id)
        {
            var cs = (User)userManager.GetUsersInRoleAsync("User").Result.Where(c => c.Id == id).SingleOrDefault();
            if (cs == null) return NotFound();
            var CS = new UserDto()
            {
                Id=id,
                username = cs.FullName,
              
                Email = cs.Email,
                Address = cs.address,
                PhoneNumber = cs.PhoneNumber
            };
            return Ok(CS);
        }
        [HttpPut]
        [SwaggerOperation(
            Summary = "Update an existing Useer",
            Description = "Modify details of an existing User"
        )]
        [SwaggerResponse(200, "User Data updated successfully", typeof(UserDto))]
        [SwaggerResponse(400, "Invalid User data")]
        [SwaggerResponse(404, "User not found")]
        public IActionResult Edit(UserDto UserEditDTO)
        {
            if (ModelState.IsValid)
            {
                var cs = (User)userManager.FindByIdAsync(UserEditDTO.Id).Result;
                if (cs == null) return NotFound();
                
                cs.PhoneNumber = UserEditDTO.PhoneNumber;
                cs.address = UserEditDTO.Address??cs.PhoneNumber;
                cs.Email = UserEditDTO.Email??cs.Email;
                cs.UserName = UserEditDTO.Email ?? cs.Email;

                cs.FullName = UserEditDTO.username??cs.FullName;
               
                var res = userManager.UpdateAsync(cs).Result;
                if (res.Succeeded)
                    return Ok();
                else
                    return BadRequest();
                //db.Userrepository.Edit(cs);
                //db.Save();
                //return Ok();
            }
            else
                return BadRequest(ModelState);
        }
    }
}
