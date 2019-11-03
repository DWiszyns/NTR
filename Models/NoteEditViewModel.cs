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
        public NoteEditViewModel()
        {

        }
        public NoteEditViewModel(Note note)
        {
            Note=note;
        }
    }
}