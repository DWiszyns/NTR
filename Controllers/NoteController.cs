using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Z02.Models;

namespace Z02.Controllers
{
    public class NoteController : Controller
    {
        public List<Note> Notes;
        public NoteController()
        {
            var notes = new List<Note>();
            notes.Add(new Note{
                Title = "Some note",
                Date = DateTime.Today.AddDays(-1),
                Category = "sport",
                Text = "tralala, some note"
            });
            notes.Add(new Note{
                Title = "Some other note",
                Date = DateTime.Today.AddDays(1),
                Category = "notsport",
                Text = "tralala, some note"
            });
            this.Notes=notes;
        }
        public IActionResult Index()
        {
            return View(new NoteIndexViewModel(Notes));
        }

        public IActionResult Add(string Title)
        {
            return View("Index");
        }
        public IActionResult Delete(string Title)
        {
            return View("Index");
        }
        public IActionResult Clear(string Title)
        {
            return View("Index");
        }
        [HttpPost]
        public IActionResult Filter(NoteIndexViewModel model)
        {
            foreach(var n in Notes)
            {
                if(n.Date>=model.DateFrom && n.Date<=model.DateTo && n.Category == model.Category)
                {
                    model.Notes.Add(n);
                }
            }
            return View("Index",model);
        }
    }
}
