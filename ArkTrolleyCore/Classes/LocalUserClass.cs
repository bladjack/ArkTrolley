using System;
using SQLite;

namespace ArkTrolleyCore
{
	[Table("tbl_local_users")]
	public class LocalUserClass
	{
		[PrimaryKey, AutoIncrement, Column("int_id")]
		public int int_id { get; set; }
		public DateTime dte_last_login { get; set; }
		[MaxLength(64)]
		public string str_name { get; set; }
		[MaxLength(128)]
		public string str_email { get; set; }

		public LocalUserClass ()
		{
		}
	}
}