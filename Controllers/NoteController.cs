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
            this.Notes=_notesRepository.FindAll().Cast<Note>().ToList();
        }
        public IActionResult Index(DateTime dateFrom, DateTime dateTo, string category)
        {
            if(dateFrom==null) dateFrom = new DateTime{};
            if(dateFrom==null) dateFrom = DateTime.Today;
            if(category==null) category="";
            string[] possibleCategories= new string[]{};
            this.Notes=_notesRepository.FindAll().Cast<Note>().ToList();
            foreach(var n in Notes)
            {
                foreach(var c in n.Categories)
                {
                    if(!possibleCategories.Contains(c)) possibleCategories=possibleCategories.Append(c).ToArray();

                }
            }
            foreach(var n in Notes)
            {
                if(n.Date<=dateFrom || n.Date>=dateTo || (category!=""&&!n.Categories.Contains(category)))
                {
                    Notes.Remove(n);
                }
            }
            PaginatedList<Note> list = new PaginatedList<Note>(Notes,1,10);
            return View("Index",new NoteIndexViewModel(list,possibleCategories));
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
        public IActionResult Filter(DateTime dateFrom, DateTime dateTo, string category)
        {
            string[] possibleCategories= new string[]{};
            this.Notes=_notesRepository.FindAll().Cast<Note>().ToList();
            foreach(var n in Notes)
            {
                foreach(var c in n.Categories)
                {
                    if(!possibleCategories.Contains(c)) possibleCategories=possibleCategories.Append(c).ToArray();

                }
            }
            foreach(var n in Notes)
            {
                if(n.Date<=dateFrom || n.Date>=dateTo || !n.Categories.Contains(category))
                {
                    Notes.Remove(n);
                }
            }
            PaginatedList<Note> list = new PaginatedList<Note>(Notes,1,10);
            return View("Index",new NoteIndexViewModel(list,possibleCategories));
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
