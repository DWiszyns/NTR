using System;
using System.Collections;
using System.Linq;


namespace Z02.Models
{
    public class Note
    {
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