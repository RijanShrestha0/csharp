using System;
using Library.CustomException;

namespace Library.Model
{
    public class Magazine : LibraryItemBase
    {
        private string _issueNumber;

        public string IssueNumber
        {
            get => _issueNumber;
            set
            {
                if (string.IsNullOrEmpty(value) || value.Length < 1)
                    throw new InvalidItemDataException("Invalid Issue Number.");
                _issueNumber = value;
            }
        }

        public override void DisplayItems()
        {
            Console.WriteLine($"[Magazine] Title: {Title}, Publisher: {Publisher}, Year: {PublicationYear}, Issue Number: {IssueNumber}");
        }
    }
}
