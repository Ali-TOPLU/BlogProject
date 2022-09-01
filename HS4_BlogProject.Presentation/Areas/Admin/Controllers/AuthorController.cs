using HS4_BlogProject.Application.Models.DTOs;
using HS4_BlogProject.Application.Services.AuthorService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HS4_BlogProject.Presentation.Areas.Admin.Controllers
{
    [Area("Admin")]
    //[Authorize]
    public class AuthorController : Controller
    {
        private readonly IAuthorService _authorService;

        public AuthorController(IAuthorService authorService)
        {
            _authorService = authorService;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(await _authorService.GetAuthors());
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(CreateAuthorDTO model)
        {
            if (ModelState.IsValid)
            {
                _authorService.Create(model);
            }

            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Update(int id)
        {
            // id ile Author seçilir           
            return View(await _authorService.GetById(id));
        }

        [HttpPost]
        public async Task<IActionResult> Update(UpdateAuthorDTO model)
        {
            // seçilen Author Edit edildikten sonra veritabanında güncellenmek için gönderilir.
            await _authorService.Update(model);

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int id)
        {
            await _authorService.Delete(id);
            return RedirectToAction("Index");
        }


    }
}
