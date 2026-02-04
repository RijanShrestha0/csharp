using System;
using Library.CustomException;

namespace Library.Model
{
    public class Newspaper : LibraryItemBase
    {
        private string _editor;

        public string Editor
        {
            get => _editor;
            set
            {
                 if (string.IsNullOrEmpty(value) || value.Length < 3 || !char.IsUpper(value[0]))
                    throw new InvalidItemDataException("Invalid Editor name.");
                 _editor = value;
            }
        }

        public override void DisplayItems()
        {
            Console.WriteLine($"[Newspaper] Title: {Title}, Publisher: {Publisher}, Year: {PublicationYear}, Editor: {Editor}");
        }
    }
}
