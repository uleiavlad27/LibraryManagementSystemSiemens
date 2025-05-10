using System.Collections.Generic;
using MySql.Data.MySqlClient;
using LibraryManagementSystem.Models;

namespace LibraryManagementSystem.Data
{
	public class WishListRepository
	{
		private string connectionString = "server=localhost;user=root;password=root;database=siemens_assignment;";

		public void TableExists()
		{
			using (var connection = new MySqlConnection(connectionString))
			{
				connection.Open();
				var cmd = new MySqlCommand(
					@"CREATE TABLE IF NOT EXISTS WishList (
                        Id INT AUTO_INCREMENT PRIMARY KEY,
                        BookId INT,
                        UserName VARCHAR(255),
                        FOREIGN KEY (BookId) REFERENCES Books(Id)
                    );", connection);
				cmd.ExecuteNonQuery();
			}
		}

		public void AddToWishList(WishList wishList)
		{
			using (var connection = new MySqlConnection(connectionString))
			{
				connection.Open();
				var cmd = new MySqlCommand(
					@"INSERT INTO WishList (BookId, UserName) 
                      VALUES (@BookId, @UserName)", connection);

				cmd.Parameters.AddWithValue("@BookId", wishList.BookId);
				cmd.Parameters.AddWithValue("@UserName", wishList.UserName);

				cmd.ExecuteNonQuery();
			}
		}

		public List<WishList> GetUserWishList(string userName)
		{
			var wishList = new List<WishList>();
			using (var connection = new MySqlConnection(connectionString))
			{
				connection.Open();
				var cmd = new MySqlCommand(
					@"SELECT Id, BookId, UserName 
                      FROM WishList 
                      WHERE UserName = @UserName", connection);

				cmd.Parameters.AddWithValue("@UserName", userName);

				using (var reader = cmd.ExecuteReader())
				{
					while (reader.Read())
					{
						wishList.Add(new WishList
						{
							Id = reader.GetInt32("Id"),
							BookId = reader.GetInt32("BookId"),
							UserName = reader.GetString("UserName")
						});
					}
				}
			}
			return wishList;
		}

		public void RemoveFromWishList(int wishListId)
		{
			using (var connection = new MySqlConnection(connectionString))
			{
				connection.Open();
				var cmd = new MySqlCommand(
					"DELETE FROM WishList WHERE Id = @Id", connection);

				cmd.Parameters.AddWithValue("@Id", wishListId);
				cmd.ExecuteNonQuery();
			}
		}

		public bool IsInWishList(int bookId, string userName)
		{
			using (var connection = new MySqlConnection(connectionString))
			{
				connection.Open();
				var cmd = new MySqlCommand(
					@"SELECT COUNT(*) FROM WishList 
                      WHERE BookId = @BookId AND UserName = @UserName", connection);

				cmd.Parameters.AddWithValue("@BookId", bookId);
				cmd.Parameters.AddWithValue("@UserName", userName);

				return Convert.ToInt32(cmd.ExecuteScalar()) > 0;
			}
		}
	}
}