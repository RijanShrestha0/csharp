using System;
using Library.Model;
using Library.Service;
using Library.CustomException;

namespace Library
{
    internal class Program
    {
        static void Main(string[] args)
        {
            LibraryService library = new LibraryService();

            while (true)
            {
                Console.WriteLine("\n=== Kesari Library ===");
                Console.WriteLine("1. Add Item");
                Console.WriteLine("2. Display All Items");
                Console.WriteLine("3. Search Items");
                Console.WriteLine("4. Sort Items");
                Console.WriteLine("5. Remove Item");
                Console.WriteLine("6. Update Item");
                Console.WriteLine("7. Exit");
                Console.Write("Choice: ");

                string choice = Console.ReadLine();

                try
                {
                    if (choice == "1")
                    {
                        AddItemMenu(library);
                    }
                    else if (choice == "2")
                    {
                        Console.WriteLine("\nLibrary Items:");
                        library.DisplayAllItems();
                    }
                    else if (choice == "3")
                    {
                        Console.Write("Search: ");
                        string query = Console.ReadLine();
                        library.SearchItems(query);
                    }
                    else if (choice == "4")
                    {
                        Console.WriteLine("Sort by: 1.Title  2.Year  3.Author");
                        string sortChoice = Console.ReadLine();
                        if (sortChoice == "1") library.SortItems("title");
                        else if (sortChoice == "2") library.SortItems("publication year");
                        else if (sortChoice == "3") library.SortItems("author");
                        else Console.WriteLine("Invalid option.");
                    }
                    else if (choice == "5")
                    {
                        Console.Write("Title: ");
                        string title = Console.ReadLine();
                        library.RemoveItem(title);
                    }
                    else if (choice == "6")
                    {
                        Console.Write("Title: ");
                        string title = Console.ReadLine();
                        library.UpdateItem(title, (item) => {
                            Console.WriteLine($"Updating '{item.Title}'. Press Enter to keep current value.");
                            
                            Console.Write($"New Publisher ({item.Publisher}): ");
                            string p = Console.ReadLine();
                            if (!string.IsNullOrWhiteSpace(p)) item.Publisher = p;
                            
                            Console.Write($"New Year ({item.PublicationYear}): ");
                            string y = Console.ReadLine();
                            if (!string.IsNullOrWhiteSpace(y)) item.PublicationYear = y;
                        });
                    }
                    else if (choice == "7")
                    {
                        Console.WriteLine("Goodbye!");
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Invalid choice.");
                    }
                }
                catch (InvalidItemDataException ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
                catch (DuplicateItemException ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
            }
        }

        static void AddItemMenu(LibraryService library)
        {
            Console.WriteLine("\nAdd Item:");
            Console.WriteLine("1. Book");
            Console.WriteLine("2. Magazine");
            Console.WriteLine("3. Newspaper");
            Console.Write("Type: ");
            string type = Console.ReadLine();

            if (type == "1")
            {
                Book book = new Book();
                Console.Write("Title: ");
                book.Title = Console.ReadLine();
                Console.Write("Publisher: ");
                book.Publisher = Console.ReadLine();
                Console.Write("Year: ");
                book.PublicationYear = Console.ReadLine();
                Console.Write("Author: ");
                book.Author = Console.ReadLine();
                library.AddItem(book);
            }
            else if (type == "2")
            {
                Magazine mag = new Magazine();
                Console.Write("Title: ");
                mag.Title = Console.ReadLine();
                Console.Write("Publisher: ");
                mag.Publisher = Console.ReadLine();
                Console.Write("Year: ");
                mag.PublicationYear = Console.ReadLine();
                Console.Write("Issue: ");
                mag.IssueNumber = Console.ReadLine();
                library.AddItem(mag);
            }
            else if (type == "3")
            {
                Newspaper news = new Newspaper();
                Console.Write("Title: ");
                news.Title = Console.ReadLine();
                Console.Write("Publisher: ");
                news.Publisher = Console.ReadLine();
                Console.Write("Year: ");
                news.PublicationYear = Console.ReadLine();
                Console.Write("Editor: ");
                news.Editor = Console.ReadLine();
                library.AddItem(news);
            }
            else
            {
                Console.WriteLine("Invalid type.");
            }
        }
    }
}