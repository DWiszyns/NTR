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
        public List<Note> Notes { get; set; }
        public List<SelectListItem> Categories { get; } = new List<SelectListItem>
        {
            new SelectListItem { Value = "sport", Text = "sport" },
            new SelectListItem { Value = "notsport", Text = "notsport" },
        };
        public NoteIndexViewModel()
        {
            Notes = new List<Note>{};
        }
        public NoteIndexViewModel(List<Note> notes)
        {
            Notes=notes;
        }
    }
}