using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Z02.Models;
using Z02;

namespace Z02.Repositories
{
    public class NoteRepository : IRepository<Note, string>
    {
        private const string directory = "./data";
        public Note FindById(string title)
        {
            Note note = null;
            
            using (StreamReader file = new StreamReader("./data/"+title+".txt"))
            {
                string line = file.ReadLine();
                HashSet<string> categories = extractCategories(line);
                line = file.ReadLine();
                DateTime date = extractDate(line);
                string content = "";
                while ((line = file.ReadLine()) != null)
                {
                    content += line;
                }

                note = new Note(title, categories.ToArray(), date, content);
            }

            return note;
        }

        public IEnumerable FindAll()
        {
            string[] files = Directory.GetFiles(directory);
            List<Note> notes = new List<Note>(); 
            foreach (string fileName in files) 
            {
                using (StreamReader file = new StreamReader(fileName))
                {
                    //reading line describing category
                    string line = file.ReadLine();
                    HashSet<string> categories = extractCategories(line);
                    line = file.ReadLine();
                    DateTime date = extractDate(line);
                    string text = file.ReadToEnd();
                    Note newNote = new Note(extractNoteTitle(fileName), categories.ToArray(), date,text);
                    notes.Add(newNote);
                }
            }

            return notes;
        }

        public void Update(Note oldNote, Note newNote)
        {
            //delete old 
            Delete(oldNote.Title);
            //save new
            Console.WriteLine(newNote);
            Save(newNote);
        }

        public void Save(Note note)
        {
            StringBuilder stringBuilder = new StringBuilder("");
            stringBuilder.Append("category: \n");
            stringBuilder.Append("date: ");
            stringBuilder.Append(note.Date.ToString("yyyy/MM/dd") + "\n");
            stringBuilder.Append(note.Text);
            string path = directory + "/" + note.Title + "." + note.Extension;
            File.WriteAllText(path, stringBuilder.ToString());
            //create new file
        }

        public void Delete(string title)
        {
            string[] files = Directory.GetFiles(directory);
            string fileToDelete = files.Single(file => extractNoteTitle(file).Equals(title));
            File.Delete(fileToDelete);
        }
        private HashSet<string> extractCategories(string categoryString) 
        {
            return categoryString.Split(':')[1].Split(',').Select(item => item.Trim()).ToHashSet();
        }

        private DateTime extractDate(string dateString) 
        {
            string date = dateString.Split(':')[1];
            date = date.Trim();
            
            //TODO:Add handling exceptions 
            return Convert.ToDateTime(date);
        }

        private string extractNoteTitle(string fileName) 
        {
            return fileName.Split('/').Last().Split('.')[0];
        }


    }
}