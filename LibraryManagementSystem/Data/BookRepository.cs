using System.Collections.Generic;
using LibraryManagementSystem.Models;
using MySql.Data.MySqlClient;

namespace LibraryManagementSystem.Data
{
	public class BookRepository
	{
		private string connectionString = "server=localhost;user=root;password=root;database=siemens_assignment";


		public void TableExists()
		{
			using (var connection = new MySqlConnection(connectionString))
			{
				connection.Open();
				var command = new MySqlCommand("CREATE TABLE IF NOT EXISTS books " +
					"(Id INT AUTO_INCREMENT PRIMARY KEY, " +
					"Title VARCHAR(255)," +
					" Author VARCHAR(255)," +
					" Quantity INT)", connection);
				command.ExecuteNonQuery();
			}
		}
		public List<Book> LoadBooks()
		{
			var books = new List<Book>();
			using (var connection = new MySqlConnection(connectionString))
			{
				connection.Open();
				var command = new MySqlCommand("SELECT Id, Title, Author, Quantity FROM books", connection);
				using (var reader = command.ExecuteReader())
				{
					while (reader.Read())
					{
						books.Add(new Book
						{
							Id = reader.GetInt32(0),
							Title = reader.GetString(1),
							Author = reader.GetString(2),
							Quantity = reader.GetInt32(3)
						});
					}
				}
			}
			return books;
		}

		public void AddBook(Book book)
		{
			using (var connection = new MySqlConnection(connectionString))
			{
				connection.Open();
				var command = new MySqlCommand("INSERT INTO books (Title, Author, Quantity) VALUES (@Title, @Author, @Quantity)", connection);
				command.Parameters.AddWithValue("@Title", book.Title);
				command.Parameters.AddWithValue("@Author", book.Author);
				command.Parameters.AddWithValue("@Quantity", book.Quantity);
				command.ExecuteNonQuery();
			}

		}

		public void UpdateBook(Book book)
		{
			using (var connection = new MySqlConnection(connectionString))
			{
				connection.Open();
				var command = new MySqlCommand("UPDATE books SET Title = @Title, Author = @Author, Quantity = @Quantity WHERE Id = @Id", connection);
				command.Parameters.AddWithValue("@Id", book.Id);
				command.Parameters.AddWithValue("@Title", book.Title);
				command.Parameters.AddWithValue("@Author", book.Author);
				command.Parameters.AddWithValue("@Quantity", book.Quantity);
				command.ExecuteNonQuery();
			}
		}

		public void DeleteBook(int bookId)
		{
			using (var connection = new MySqlConnection(connectionString))
			{
				connection.Open();
				var command = new MySqlCommand("DELETE FROM books WHERE Id = @Id", connection);
				command.Parameters.AddWithValue("@Id", bookId);
				command.ExecuteNonQuery();
			}
		}

	}
}
