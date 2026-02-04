using System;

namespace Library.Interface
{
    public interface ILibraryItem
    {
        string Title { get; set; }
        string Publisher { get; set; }
        string PublicationYear { get; set; }
        
        void DisplayItems();
    }
}