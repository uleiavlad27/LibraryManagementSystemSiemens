using LibraryManagementSystem.Data;
using LibraryManagementSystem.Models;

namespace LibraryManagementSystem.Service
{
	public class LibraryService
	{
		private BookRepository repo = new BookRepository();
		private ReservationRepository reservationRepo = new ReservationRepository();
		private WishListRepository wishListRepo = new WishListRepository();

		public void AddBook(Book book)
		{
			repo.AddBook(book);
		}

		public List<Book> GetAllBooks()
		{
			return repo.LoadBooks();
		}	

		public void DeleteBook(int id)
		{
			repo.DeleteBook(id);
		}

		public void UpdateBook(Book book)
		{
			repo.UpdateBook(book);
		}

		public List<Book> SearchBooks(string title = null, string author = null)
		{
			var books = repo.LoadBooks();
			if (!string.IsNullOrEmpty(title))
				books = books.Where(b => b.Title.Contains(title, StringComparison.CurrentCultureIgnoreCase)).ToList();
			if (!string.IsNullOrEmpty(author))
				books = books.Where(b => b.Author.Contains(author, StringComparison.CurrentCultureIgnoreCase)).ToList();
			return books;
		}

		public bool LendBook(int bookId, string user)
		{
			var books = repo.LoadBooks();
			var book = books.FirstOrDefault(b => b.Id == bookId);
			if (book == null || book.Quantity <= 0)
			{
				reservationRepo.AddReservation(new Reservation { BookId = bookId, ReservedBy = user });
				return false;
			}
			book.Quantity--;
			repo.UpdateBook(book);
			return true;
		}

		public bool ReturnBook(int bookId)
		{
			var books = repo.LoadBooks();
			var book = books.FirstOrDefault(b => b.Id == bookId);
			if (book == null) return false;
			book.Quantity++;
			repo.UpdateBook(book);
			var reservation = reservationRepo.GetReservation(bookId);
			if (reservation != null)
			{
				Console.WriteLine($"Book ID {bookId} is now available for {reservation.ReservedBy} (reservation notified).");
				reservationRepo.RemoveReservation(bookId);
			}
			return true;
		}

		public void AddToWishList(int bookId, string userName)
		{
			if (!wishListRepo.IsInWishList(bookId, userName))
			{
				wishListRepo.AddToWishList(new WishList
				{
					BookId = bookId,
					UserName = userName
				});
			}
		}

		public List<Book> GetUserWishList(string userName)
		{
			var wishList = wishListRepo.GetUserWishList(userName);
			var books = repo.LoadBooks();
			return wishList.Select(w => books.FirstOrDefault(b => b.Id == w.BookId))
						  .Where(b => b != null)
						  .ToList();
		}

		public void RemoveFromWishList(int wishListId)
		{
			wishListRepo.RemoveFromWishList(wishListId);
		}
	}
}
