using System;
using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace Z02.Models
{
    public class Note
    {
        [Required(ErrorMessage = "Title is required")]
        [Remote("doesFileNameExist", "Note", HttpMethod = "POST", ErrorMessage = "User name already exists. Please enter a different user name.")]
        public string Title { get; set; }
        public DateTime Date { get; set; }
        public string[] Categories { get; set; }
        public string Text { get; set; }
        public string Extension{get;set;}
        public Note()
        {
            Title="write some title";
            Categories=new string []{};
            Date=DateTime.Now;
            Text="Write some text";
        }
        public Note(string title, string[] categories, DateTime date, string text, string extension)
        {
            Title=title;
            Categories=categories;
            Date=date;
            Text=text;
            Extension=extension;
        }
         public Note(string title, string[] categories, DateTime date)
        {
            Title=title;
            Categories=categories;
            Date=date;
        }
    }
}