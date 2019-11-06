using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;
using Z02.Models;

namespace Z02.Models
{
    public class NoteEditViewModel
    {
        public Note Note {get;set;}
        public string NewCategory { get; set; }
        public string[] CategoriesToRemove { get; set; }
        public string OldTitle  {get;set;}
        public List<SelectListItem> Extensions = new List<SelectListItem>
            {
                new SelectListItem { Value = "md", Text = "md" },
                new SelectListItem { Value = "txt", Text = "txt" },
            };
        public NoteEditViewModel()
        {
        }
        public NoteEditViewModel(Note note)
        {
            OldTitle=note.Title;
            Note=note;
        }
         public NoteEditViewModel(NoteEditViewModel model)
        {
            Note=model.Note;
            OldTitle=model.OldTitle;
        }
    }
}