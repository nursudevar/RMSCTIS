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
        // TODO: Add service injections here
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        // GET: Users
        public IActionResult Index()
        {
            List<UserModel> userList = _userService.Query().ToList(); // TODO: Add get collection service logic here
            return View(userList);
        }

        // GET: Users/Details/5
        public IActionResult Details(int id)
        {
            UserModel user = _userService.Query().SingleOrDefault(e=>e.Id ==id); // TODO: Add get item service logic here
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        // GET: Users/Create
        public IActionResult Create()
        {
            // TODO: Add get related items service logic here to set ViewData if necessary
            ViewData["RoleId"] = new SelectList(new List<SelectListItem>(), "Value", "Text");
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(UserModel user)
        {
            if (ModelState.IsValid)
            {
                Result result = _userService.Add(user);
                if(result.IsSuccessfull)
                {
                    // return RedirectToAction("Index");

                    TempData["Message"] = result.Message;
                    return RedirectToAction(nameof(Index), "User");

                }

                // ViewData["View Message"] = result.Message;

                // ViewBag.ViewMessage = result.Message;

                ModelState.AddModelError("", result.Message);

               
            }
            // TODO: Add get related items service logic here to set ViewData if necessary
            ViewData["RoleId"] = new SelectList(new List<SelectListItem>(), "Value", "Text");
            return View(user);
        }

        // GET: Users/Edit/5
        public IActionResult Edit(int id)
        {
            UserModel user = _userService.Query().SingleOrDefault(e=> e.Id == id);
            if (user == null)
            {
                return NotFound();
            }
            // TODO: Add get related items service logic here to set ViewData if necessary
            ViewData["RoleId"] = new SelectList(new List<SelectListItem>(), "Value", "Text");
            return View(user);
        }

        // POST: Users/Edit
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
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
            // TODO: Add get related items service logic here to set ViewData if necessary
            ViewData["RoleId"] = new SelectList(new List<SelectListItem>(), "Value", "Text");
            return View(user);
        }

        // GET: Users/Delete/5
        public IActionResult Delete(int id)
        {
            UserModel user = _userService.Query().SingleOrDefault(e => e.Id == id); // TODO: Add get item service logic here
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
