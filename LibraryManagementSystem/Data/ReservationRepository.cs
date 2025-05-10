using LibraryManagementSystem.Models;
using MySql.Data.MySqlClient;

namespace LibraryManagementSystem.Data
{
	public class ReservationRepository
	{
		private string connectionString = "server=localhost;user=root;password=root;database=siemens_assignment";

		public void TableExists()
		{
			using (var connection = new MySqlConnection(connectionString))
			{
				connection.Open();
				var command = new MySqlCommand("CREATE TABLE IF NOT EXISTS reservations "
					+
					"(Id INT AUTO_INCREMENT PRIMARY KEY, " +
					"BookId INT, " +
					"ReservedBy VARCHAR(255), " +
					"FOREIGN KEY (BookId) REFERENCES books(Id));", connection);
				command.ExecuteNonQuery();
			}
		}

		public List<Reservation> LoadReservations()
		{
			var reservations = new List<Reservation>();
			using (var connection = new MySqlConnection(connectionString))
			{
				connection.Open();
				var command = new MySqlCommand("SELECT Id, BookId, ReservedBy FROM reservations", connection);

				using (var reader = command.ExecuteReader())
				{
					while (reader.Read())
					{
						reservations.Add(new Reservation
						{
							Id = reader.GetInt32(0),
							BookId = reader.GetInt32(1),
							ReservedBy = reader.GetString(2)
						});
					}
				}
			}
			return reservations;
		}

		public void AddReservation(Reservation reservation)
		{
			using (var connection = new MySqlConnection(connectionString))
			{
				connection.Open();
				var command = new MySqlCommand("INSERT INTO reservations (BookId, ReservedBy) VALUES (@BookId, @ReservedBy)", connection);
				command.Parameters.AddWithValue("@BookId", reservation.BookId);
				command.Parameters.AddWithValue("@ReservedBy", reservation.ReservedBy);
				command.ExecuteNonQuery();
			}
		}

		public void RemoveReservation(int reservationId)
		{
			using (var connection = new MySqlConnection(connectionString))
			{
				connection.Open();
				var command = new MySqlCommand("DELETE FROM reservations WHERE Id = @Id", connection);
				command.Parameters.AddWithValue("@Id", reservationId);
				command.ExecuteNonQuery();
			}
		}

		public Reservation GetReservation(int bookId, string reservedBy = null)
		{
			using (var conn = new MySqlConnection(connectionString))
			{
				conn.Open();
				string query = "SELECT BookId, ReservedBy FROM Reservations WHERE BookId = @BookId";
				if (!string.IsNullOrEmpty(reservedBy))
					query += " AND ReservedBy = @ReservedBy";
				var cmd = new MySqlCommand(query, conn);
				cmd.Parameters.AddWithValue("@BookId", bookId);
				if (!string.IsNullOrEmpty(reservedBy))
					cmd.Parameters.AddWithValue("@ReservedBy", reservedBy);
				using (var reader = cmd.ExecuteReader())
				{
					if (reader.Read())
					{
						return new Reservation
						{
							BookId = reader.GetInt32(0),
							ReservedBy = reader.GetString(1)
						};
					}
				}
			}
			return null;
		}
	}
}
