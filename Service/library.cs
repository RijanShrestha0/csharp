using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Linq;
using Library.Model;
using Library.Interface;
using Library.CustomException;
 
namespace Library.Service
{
    public class LibraryService
    {
        private List<LibraryItemBase> _items = new();
        private const string FilePath = "libraryData.txt";

        public LibraryService()
        {
            LoadData();
        }

        public void AddItem(LibraryItemBase item)
        {
            if (_items.Any(i => IsDuplicate(i, item)))
                throw new DuplicateItemException("Duplicate item.");

            _items.Add(item);
            SaveData();
            Console.WriteLine($"Item '{item.Title}' added.");
        }

        public void RemoveItem(string title)
        {
            var item = FindByTitle(title);
            if (item == null)
            {
                Console.WriteLine("Item not found.");
                return;
            }

            _items.Remove(item);
            SaveData();
            Console.WriteLine($"Item '{title}' removed.");
        }

        public void UpdateItem(string title, Action<LibraryItemBase> updateAction)
        {
            var item = FindByTitle(title);
            if (item == null)
            {
                Console.WriteLine("Item not found.");
                return;
            }

            try
            {
                updateAction(item);
                SaveData();
                Console.WriteLine("Item updated.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Update failed: {ex.Message}");
                LoadData();
            }
        }

        public void SearchItems(string query)
        {
            var results = _items.Where(i =>
                i.Title.Contains(query, StringComparison.OrdinalIgnoreCase) ||
                (i is Book b && b.Author.Contains(query, StringComparison.OrdinalIgnoreCase))
            );

            if (!results.Any())
            {
                Console.WriteLine("No match found.");
                return;
            }

            foreach (var item in results)
                item.DisplayItems();
        }

        public void SortItems(string criteria)
        {
            IEnumerable<LibraryItemBase> sorted = criteria.ToLower() switch
            {
                "title" => _items.OrderBy(i => i.Title),
                "publication year" => _items.OrderBy(i => i.PublicationYear),
                "author" => _items.OrderBy(i => i is Book b ? b.Author : ""),
                _ => _items.AsEnumerable()
            };

            foreach (var item in sorted)
                item.DisplayItems();
        }

        public void DisplayAllItems()
        {
            if (!_items.Any())
            {
                Console.WriteLine("No items in the library.");
                return;
            }

            foreach (var item in _items)
                item.DisplayItems();
        }

        private LibraryItemBase FindByTitle(string title) =>
            _items.FirstOrDefault(i => i.Title.Equals(title, StringComparison.OrdinalIgnoreCase));

        private bool IsDuplicate(ILibraryItem a, ILibraryItem b)
        {
            if (a.GetType() != b.GetType()) return false;
            if (!a.Title.Equals(b.Title, StringComparison.OrdinalIgnoreCase)) return false;
            if (!a.Publisher.Equals(b.Publisher, StringComparison.OrdinalIgnoreCase)) return false;
            if (a.PublicationYear != b.PublicationYear) return false;

            return (a, b) switch
            {
                (Book x, Book y) => x.Author.Equals(y.Author, StringComparison.OrdinalIgnoreCase),
                (Magazine x, Magazine y) => x.IssueNumber.Equals(y.IssueNumber, StringComparison.OrdinalIgnoreCase),
                (Newspaper x, Newspaper y) => x.Editor.Equals(y.Editor, StringComparison.OrdinalIgnoreCase),
                _ => true
            };
        }

        private void SaveData()
        {
            var json = JsonSerializer.Serialize(_items, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(FilePath, json);
        }

        private void LoadData()
        {
            if (!File.Exists(FilePath)) return;
            var json = File.ReadAllText(FilePath);
            _items = JsonSerializer.Deserialize<List<LibraryItemBase>>(json) ?? new();
        }
    }
}