using System;
using Library.CustomException;

namespace Library.Model
{
    public class Book : LibraryItemBase
    {
        private string _author;

        public string Author
        {
            get => _author;
            set
            {
                if (string.IsNullOrEmpty(value) || value.Length < 5 || !char.IsUpper(value[0]))
                    throw new InvalidItemDataException("Invalid Author name.");
                _author = value;
            }
        }

        public override void DisplayItems()
        {
            Console.WriteLine($"[Book] Title: {Title}, Publisher: {Publisher}, Year: {PublicationYear}, Author: {Author}");
        }
    }
}