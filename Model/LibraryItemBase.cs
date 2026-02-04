using System;
using System.Text.Json.Serialization;
using Library.Interface;
using Library.CustomException;

namespace Library.Model
{
    [JsonDerivedType(typeof(Book), typeDiscriminator: "book")]
    [JsonDerivedType(typeof(Magazine), typeDiscriminator: "magazine")]
    [JsonDerivedType(typeof(Newspaper), typeDiscriminator: "newspaper")]
    public abstract class LibraryItemBase : ILibraryItem
    {
        private string _title;
        private string _publisher;
        private string _publicationYear;

        public string Title
        {
            get => _title;
            set
            {
                if (string.IsNullOrEmpty(value) || value.Length < 5 || !char.IsUpper(value[0]))
                    throw new InvalidItemDataException("Invalid title format.");
                _title = value;
            }
        }

        public string Publisher
        {
            get => _publisher;
            set
            {
                if (string.IsNullOrEmpty(value) || value.Length < 6 || !char.IsUpper(value[0]))
                    throw new InvalidItemDataException("Invalid publisher format.");
                _publisher = value;
            }
        }

        public string PublicationYear
        {
            get => _publicationYear;
            set
            {
                if (string.IsNullOrEmpty(value) || value.Length != 4)
                {
                    throw new InvalidItemDataException("Invalid year format.");
                }
                _publicationYear = value;
            }
        }

        public abstract void DisplayItems();
    }
}
