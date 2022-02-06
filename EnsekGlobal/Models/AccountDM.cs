using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EnsekGlobal.Models
{
	public class AccountDM
	{
		[Key]
		public Int64 AccountId { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }

		[NotMapped]
		public string FullName { get { return $"{FirstName} {LastName}"; } }
	}
}
