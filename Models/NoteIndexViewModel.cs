using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;
using Z02.Models;

namespace Z02.Models
{
    public class NoteIndexViewModel
    {
        public string Category { get; set; }
        public DateTime DateTo { get; set; }
        public DateTime DateFrom { get; set; }
        public PaginatedList<Note> Notes { get; set; }
        public List<SelectListItem> Categories { get; }
        public NoteIndexViewModel()
        {
            List <Note> emptyNoteList = new List<Note> {};
            Notes = new PaginatedList<Note>(emptyNoteList , 0, 10);
            Categories = new List<SelectListItem>
            {
                new SelectListItem { Value = "sport", Text = "sport" },
                new SelectListItem { Value = "notsport", Text = "notsport" },
            };
        }
        public NoteIndexViewModel(PaginatedList<Note> notes, string[] categories)
        {
            Notes=notes;
            Categories = new List<SelectListItem> {};
            foreach(var c in categories)
            {
                Categories.Add(new SelectListItem { Value = c, Text = c });
            }
        }
    }
}