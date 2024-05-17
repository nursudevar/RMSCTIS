#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DataAccess_.Contexts;
using DataAccess_.Entities;
using Business.Services;
using Business.Models;
using DataAccess_.Results.Bases;

//Generated from Custom Template.
namespace MVC.Controllers
{
    public class UsersController : Controller
    {
       
        private readonly IUserService _userService;
		private readonly IRoleService _roleService;

		public UsersController(IUserService userService, IRoleService roleService)
		{
			_userService = userService;
			_roleService = roleService;
		}

		// GET: Users
		public IActionResult Index()
        {
            List<UserModel> userList = _userService.Query().ToList();
            return View(userList);
        }

       
        public IActionResult Details(int id)
        {
            UserModel user = _userService.Query().SingleOrDefault(e=>e.Id ==id); 
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

      
        public IActionResult Create()
        {
          

			ViewData["RoleId"] = new SelectList(_roleService.Query().ToList(), "Id", "Name");
			return View();
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(UserModel user)
        {
            if (ModelState.IsValid)
            {
                Result result = _userService.Add(user);
                if(result.IsSuccessfull)
                {
                   

                    TempData["Message"] = result.Message;
                    return RedirectToAction(nameof(Index));

                }

              

                ModelState.AddModelError("", result.Message);

               
            }
     
            ViewData["RoleId"] = new SelectList(new List<SelectListItem>(), "Value", "Text");
            return View(user);
        }


        public IActionResult Edit(int id)
        {
            UserModel user = _userService.Query().SingleOrDefault(e=> e.Id == id);
            if (user == null)
            {
                return NotFound();
            }
			// TODO: Add get related items service logic here to set ViewData if necessary
			var roleIds = _roleService.Query().Select(r => new SelectListItem
			{
				Value = r.Id.ToString(),
				Text = r.Name
			}).ToList();

			var selectedRoleId = user.RoleId.ToString();

			// Create a SelectList with the role IDs and set the selected value
			ViewData["RoleId"] = new SelectList(roleIds, "Value", "Text", selectedRoleId);

			return View(user);
			
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(UserModel user)
        {
            if (ModelState.IsValid)
            {
                Result result = _userService.Update(user);
                if(result.IsSuccessfull)
                {
                    TempData["Message"] = result.Message;
					return RedirectToAction(nameof(Details), new {id = user.Id});

				}
                ModelState.AddModelError("", result.Message);
			}
          
            ViewData["RoleId"] = new SelectList(new List<SelectListItem>(), "Value", "Text");
            return View(user);
        }

       
        public IActionResult Delete(int id)
        {
            UserModel user = _userService.Query().SingleOrDefault(e => e.Id == id); 
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        // POST: Users/Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
           Result result = _userService.Delete(id);
			TempData["Message"] = result.Message;
			return RedirectToAction(nameof(Index));
        }
	}
}
