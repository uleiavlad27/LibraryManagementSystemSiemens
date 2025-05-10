using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Models
{
	public class WishList
	{
		public int Id { get; set; }
		public int BookId { get; set; }
		public string UserName { get; set; }
	}
}
