using System;
using SQLite;

namespace ArkTrolleyCore
{
	[Table("tbl_users")]
	public class UserClass
	{
		[PrimaryKey, AutoIncrement, Column("int_id")]
		public int int_id { get; set; }
		public string enm_type { get; set; }
		public int int_active { get; set; }
		public int int_points { get; set; }
		public DateTime dte_register { get; set; }
		public DateTime dte_last_login { get; set; }
		[MaxLength(16)]
		public string str_password { get; set; }
		[MaxLength(64)]
		public string str_name { get; set; }
		[MaxLength(128)]
		public string str_email { get; set; }
		[MaxLength(128)]
		public string str_address { get; set; }

		public UserClass ()
		{
		}
	}
}