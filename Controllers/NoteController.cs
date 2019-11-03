using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Z02.Models;
using Z02.Repositories;
using Z02;

namespace Z02.Controllers
{
    public class NoteController : Controller
    {
        public NoteRepository _notesRepository;
        public List<Note> Notes;
        public NoteController()
        {
            _notesRepository=new NoteRepository{};
            var notes = new List<Note>();
            // notes.Add(new Note{
            //     Title = "Some note",
            //     Date = DateTime.Today.AddDays(-1),
            //     Categories = new string[]{"sport","someothercategory"},
            //     Text = "tralala, some note"
            // });
            // notes.Add(new Note{
            //     Title = "Some other note",
            //     Date = DateTime.Today.AddDays(1),
            //     Categories = new string[]{"notsport"},
            //     Text = "tralala, some note"
            // });
            this.Notes=_notesRepository.FindAll().Cast<Note>().ToList();
        }
        public IActionResult Index()
        {
            string[] possibleCategories= new string[]{};
            foreach(var n in Notes)
            {
                foreach(var c in n.Categories)
                {
                    if(!possibleCategories.Contains(c)) possibleCategories=possibleCategories.Append(c).ToArray();

                }
            }

            PaginatedList<Note> list = new PaginatedList<Note>(Notes,1,10);
            return View(new NoteIndexViewModel(list,possibleCategories));
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
                if(n.Date>=model.DateFrom && n.Date<=model.DateTo && n.Categories.Contains(model.Category))
                {
                    model.Notes.Add(n);
                }
            }
            return View("Index",model);
        }
        [HttpPost]
        public IActionResult AddCategory(NoteEditViewModel model)
        {
            Note prevNote=model.Note=Notes.Where(m=>m.Title==model.Note.Title).FirstOrDefault();
            model.Note.Categories=model.Note.Categories.Append(model.NewCategory).ToArray();
            Notes[Notes.IndexOf(prevNote)]=model.Note;
            model.NewCategory="";
            return View("Edit",model);
        }
        [HttpPost]
        public IActionResult RemoveCategories(NoteEditViewModel model)
        {
            Note prevNote=model.Note=Notes.Where(m=>m.Title==model.Note.Title).FirstOrDefault();
            foreach(var c in model.CategoriesToRemove ?? new string[] { })
            {
                model.Note.Categories=model.Note.Categories.Where(v=>v!=c).ToArray();
            }
            Notes[Notes.IndexOf(prevNote)]=model.Note;
            return View("Edit",model);
        }
        public IActionResult Add()
        {
            return View();
        }
        public IActionResult Edit(string title)
        {
            Note n = Notes.Where(m => m.Title == title).FirstOrDefault();
            return View(new NoteEditViewModel(n));
        }
        [HttpPost]
        public IActionResult Edit(NoteEditViewModel model)
        {            
            Note n = Notes.Where(m=>m.Title==model.OldTitle).FirstOrDefault();
            return View("Index");
        }
         [HttpPost]
        public IActionResult Remove(NoteIndexViewModel model)
        {
            foreach(var n in Notes)
            {
                if(n.Date>=model.DateFrom && n.Date<=model.DateTo && n.Categories.Contains(model.Category))
                {
                    model.Notes.Remove(n);
                }
            }
            return View("Index",model);
        }
    }
}
