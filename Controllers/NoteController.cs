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
        public IActionResult Index(DateTime dateFrom, DateTime dateTo, string category="",int pageNumber=1)
        {
            if(dateFrom==DateTime.MinValue) dateFrom = DateTime.Today.AddYears(-1);
            if(dateTo==DateTime.MinValue) dateTo = DateTime.Today;
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
            var notes = new List<Note>();
            foreach(var n in Notes)
            {
                 if(n.Date>=dateFrom && n.Date<=dateTo && (category==""||n.Categories.Contains(category)))
                {
                    notes.Add(n);
                }
            }
            PaginatedList<Note> list = new PaginatedList<Note>(notes,pageNumber,10);
            return View("Index",new NoteIndexViewModel(list,possibleCategories,category,dateFrom,dateTo));
        }
        public IActionResult Clear(string Title)
        {
            return View("Index");
        }
        [HttpPost]
        public IActionResult AddCategory(NoteEditViewModel model)
        {
            model.Note.Categories=model.Note.Categories.Append(model.NewCategory).ToArray();
            model.NewCategory="";
            if(Notes.Where(m=>m.Title==model.Note.Title).Any())
                return View("Edit",model);
            else return View("Add",model);
        }
        [HttpPost]
        public IActionResult RemoveCategories(NoteEditViewModel model)
        {
            foreach(var c in model.CategoriesToRemove ?? new string[] { })
            {
                model.Note.Categories=model.Note.Categories.Where(v=>v!=c).ToArray();
            }
            model.CategoriesToRemove=new string[]{};
            if(Notes.Where(m=>m.Title==model.Note.Title).Any())
                return View("Edit",new NoteEditViewModel(model.Note));
            else return View("Add",new NoteEditViewModel(model.Note));
        }
        public IActionResult Add()
        {
            Note n = new Note{};
            return View(new NoteEditViewModel(n));
        }
        [HttpPost]
        public IActionResult Add(NoteEditViewModel model)
        {
            Notes.Add(model.Note);
            _notesRepository.Save(model.Note);
            return Index(DateTime.MinValue,DateTime.MinValue,"");
        }
        public IActionResult Edit(string title)
        {
            Note n = Notes.Where(m => m.Title == title).FirstOrDefault();
            return View(new NoteEditViewModel(n));
        }
        [HttpPost]
        public IActionResult Edit(NoteEditViewModel model)
        {            
            Note oldNote = Notes.Where(m=>m.Title==model.OldTitle).FirstOrDefault();
            Note newNote = model.Note;
            _notesRepository.Update(oldNote,newNote);
            return Index(DateTime.MinValue,DateTime.MinValue,"");
        }
        public IActionResult Delete(string title)
        {
            Note note=Notes.Where(m=>m.Title==title).FirstOrDefault();
            Notes.Remove(note);
            _notesRepository.Delete(title);
            return Index(DateTime.MinValue,DateTime.MinValue);
        }
        [HttpPost]
        public JsonResult doesFileNameExist(string title)
        {
        var file = _notesRepository.FindById(title);
        return Json(file == null);
        }
    }
}
