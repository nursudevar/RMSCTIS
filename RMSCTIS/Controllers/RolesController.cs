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
using Microsoft.AspNetCore.Authorization;

namespace MVC.Controllers
{

    
    [Authorize(Roles = "admin")]
    public class RolesController : Controller
    {
        private readonly IRoleService _roleService;

        public RolesController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        public IActionResult Index()
        {
            List<RoleModel> roleList = _roleService.Query().ToList();
            return View(roleList);
        }

        public IActionResult Details(int id)
        {
            RoleModel role = _roleService.Query().SingleOrDefault(r => r.Id == id);
            if (role == null)
            {
                return NotFound();
            }
            return View(role);
        }
        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(RoleModel role)
        {
            if (ModelState.IsValid)
            {
                var result = _roleService.Add(role);
                if (result.IsSuccessfull)
                {
                    TempData["Message"] = result.Message;
                    return RedirectToAction(nameof(Index));
                }
                ModelState.AddModelError("", result.Message);
            }
            return View(role);
        }

     

        public IActionResult Edit(int id)
        {
            RoleModel role = _roleService.Query().SingleOrDefault(r => r.Id == id);
            if (role == null)
            {
                return NotFound();
            }
            return View(role);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(RoleModel role)
        {
            if (ModelState.IsValid)
            {
                var result = _roleService.Update(role);
                if (result.IsSuccessfull)
                {
                    TempData["Message"] = result.Message; 
                    return RedirectToAction(nameof(Index));
                }
                ModelState.AddModelError("", result.Message);
            }
            return View(role);
        }


        public IActionResult Delete(int id)
        {
             RoleModel role = _roleService.Query().SingleOrDefault(r => r.Id == id); 
            if (role == null)
            {
                return NotFound();
            }
            return View(role);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            foreach (var modelStateEntry in ModelState.Values)
            {
                foreach (var error in modelStateEntry.Errors)
                {
                    Console.WriteLine($"ModelState error: {error.ErrorMessage}");
                }
            }

            try
            {
                var result = _roleService.Delete(id);
                TempData["Message"] = result.Message;
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "An error occurred while deleting the role. Please try again later.";
                return RedirectToAction(nameof(Index)); 
            }
        }
    }
}
