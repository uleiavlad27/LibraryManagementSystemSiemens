using System;
using System.Collections.Generic;
using System.Linq;
using LibraryManagementSystem.Data;
using LibraryManagementSystem.Models;
using LibraryManagementSystem.Service;

namespace UI
{
	public class ConsoleUI
	{
		private LibraryService service = new LibraryService();

		public ConsoleUI()
		{
			
			var bookRepo = new BookRepository();
			bookRepo.TableExists();
			var reservationRepo = new ReservationRepository();
			reservationRepo.TableExists();
			var wishListRepo = new WishListRepository();
			wishListRepo.TableExists();
		}

		public void Run()
		{
			while (true)
			{
				Console.WriteLine("\nLibrary Management System");
				Console.WriteLine("1. Add Book");
				Console.WriteLine("2. View All Books");
				Console.WriteLine("3. Update Book");
				Console.WriteLine("4. Delete Book");
				Console.WriteLine("5. Search Books");
				Console.WriteLine("6. Lend Book");
				Console.WriteLine("7. Return Book");
				Console.WriteLine("8. Add to Wish List");
				Console.WriteLine("9. View Wish List");
				Console.WriteLine("10. Remove from Wish List");
				Console.WriteLine("0. Exit\n");
				Console.Write("Select an option: \n");
				var choice = Console.ReadLine();
				switch (choice)
				{
					case "1":
						AddBook();
						break;
					case "2":
						ViewAllBooks();
						break;
					case "3":
						UpdateBook();
						break;
					case "4":
						DeleteBook();
						break;
					case "5":
						SearchBooks();
						break;
					case "6":
						LendBook();
						break;
					case "7":
						ReturnBook();
						break;
					case "8":
						AddToWishList();
						break;
					case "9":
						ViewWishList();
						break;
					case "10":
						RemoveFromWishList();
						break;
					case "0":
						return;
					default:
						Console.WriteLine("Invalid option.");
						break;
				}
			}
		}

		private void AddBook()
		{
			Console.Write("Title: ");
			var title = Console.ReadLine();
			Console.Write("Author: ");
			var author = Console.ReadLine();
			Console.Write("Quantity: ");
			int.TryParse(Console.ReadLine(), out int qty);
			service.AddBook(new Book { Title = title, Author = author, Quantity = qty });
			Console.WriteLine("Book added.");
		}

		private void ViewAllBooks()
		{
			foreach (var b in service.GetAllBooks())
				Console.WriteLine($"ID: {b.Id}, Title: {b.Title}, Author: {b.Author}, Qty: {b.Quantity}");
		}

		private void UpdateBook()
		{
			Console.Write("Book ID to update: ");
			int.TryParse(Console.ReadLine(), out int upId);
			Console.Write("New Title: ");
			var upTitle = Console.ReadLine();
			Console.Write("New Author: ");
			var upAuthor = Console.ReadLine();
			Console.Write("New Quantity: ");
			int.TryParse(Console.ReadLine(), out int upQty);
			service.UpdateBook(new Book { Id = upId, Title = upTitle, Author = upAuthor, Quantity = upQty });
			Console.WriteLine("Book updated.");
		}

		private void DeleteBook()
		{
			Console.Write("Book ID to delete: ");
			int.TryParse(Console.ReadLine(), out int delId);
			service.DeleteBook(delId);
			Console.WriteLine("Book deleted.");
		}

		private void SearchBooks()
		{
			Console.Write("Search Title (leave blank to skip): ");
			var sTitle = Console.ReadLine();
			Console.Write("Search Author (leave blank to skip): ");
			var sAuthor = Console.ReadLine();
			var results = service.SearchBooks(sTitle, sAuthor);
			foreach (var b in results)
				Console.WriteLine($"ID: {b.Id}, Title: {b.Title}, Author: {b.Author}, Qty: {b.Quantity}");
		}

		private void LendBook()
		{
			Console.Write("Book ID to lend: ");
			int.TryParse(Console.ReadLine(), out int lendId);
			Console.Write("Your name: ");
			var user = Console.ReadLine();
			if (service.LendBook(lendId, user))
				Console.WriteLine("Book lent successfully.");
			else
				Console.WriteLine("Book not available. Reservation made if not already reserved.");
		}

		private void ReturnBook()
		{
			Console.Write("Book ID to return: ");
			int.TryParse(Console.ReadLine(), out int retId);
			if (service.ReturnBook(retId))
				Console.WriteLine("Book returned.");
			else
				Console.WriteLine("Book not found.");
		}

		

		

		

		private void AddToWishList()
		{
			Console.Write("Enter book ID: ");
			if (!int.TryParse(Console.ReadLine(), out int bookId))
			{
				Console.WriteLine("Invalid book ID.");
				return;
			}

			Console.Write("Enter your name: ");
			var userName = Console.ReadLine();

			service.AddToWishList(bookId, userName);
			Console.WriteLine("Book added to wish list!");
		}

		private void ViewWishList()
		{
			Console.Write("Enter your name: ");
			var userName = Console.ReadLine();

			var wishList = service.GetUserWishList(userName);
			if (!wishList.Any())
			{
				Console.WriteLine("Your wish list is empty.");
				return;
			}

			Console.WriteLine("\nYour Wish List:");
			foreach (var book in wishList)
			{
				Console.WriteLine($"ID: {book.Id}, Title: {book.Title}, Author: {book.Author}");
			}
		}

		private void RemoveFromWishList()
		{
			Console.Write("Enter wish list item ID to remove: ");
			if (!int.TryParse(Console.ReadLine(), out int wishListId))
			{
				Console.WriteLine("Invalid wish list item ID.");
				return;
			}

			service.RemoveFromWishList(wishListId);
			Console.WriteLine("Item removed from wish list!");
		}
	}
}