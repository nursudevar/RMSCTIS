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
using MVC.Controllers.Bases;

namespace MVC.Controllers
{
    public class ResourcesController : MvcControllerBase
    {
        private readonly IResourceService _resourceService;
		private readonly IUserService _userService;


		public ResourcesController(IResourceService resourceService, IUserService userService)
		{
			_resourceService = resourceService;
			_userService = userService;
		}

		public IActionResult Index()
        {
			List<ResourceModel> resourceList = _resourceService.GetList();

			return View(resourceList);
        }

        public IActionResult Details(int id)
        {
			ResourceModel resource = _resourceService.GetItem(id);
			if (resource == null)
            {
				return View("_Error", "Resource not found!");
			}
            return View(resource);
        }

        // GET: Resources/Create
        public IActionResult Create()
        {
			ViewBag.UserId = new MultiSelectList(_userService.Query().ToList(), "Id", "UserName");
			return View();
		}

      
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ResourceModel resource)
        {
            if (ModelState.IsValid)
            {
				var selectedUserIds = resource.UserIdsInput;
				var result = _resourceService.Add(resource);
                if (result.IsSuccessfull)
                {
                    TempData["Message"] = result.Message;
                    return RedirectToAction(nameof(Index));
                }
                ModelState.AddModelError("", result.Message);
            }


            ViewBag.UserId = new MultiSelectList(_userService.Query().ToList(), "Id", "UserName");


            return View(resource);
        }

        // GET: Resources/Edit/5
        public IActionResult Edit(int id)
        {
            ResourceModel resource = _resourceService.GetItem(id);
            if (resource == null)
            {
                return View("_Error", "Resource not found!");
            }
            ViewBag.UserId = new MultiSelectList(_userService.Query().ToList(), "Id", "UserName");
            return View(resource);
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(ResourceModel resource)
        {
            if (ModelState.IsValid)
            {
                var result = _resourceService.Update(resource);
                if (result.IsSuccessfull)
                {
                    TempData["Message"] = result.Message;
                    return RedirectToAction(nameof(Details), new { id = resource.Id });
                }
                ModelState.AddModelError("", result.Message);
            }

            ViewBag.UserId = new MultiSelectList(_userService.Query().ToList(), "Id", "UserName");
            return View(resource);
        }

        public IActionResult Delete(int id)
        {
            ResourceModel resource = null;
            if (resource == null)
            {
                return NotFound();
            }
            return View(resource);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
			var result = _resourceService.Delete(id);
			TempData["Message"] = result.Message;
			return RedirectToAction(nameof(Index));
		}
	}
}
