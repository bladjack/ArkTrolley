using System;
using Cirrious.MvvmCross.Plugins.Sqlite;

namespace ArkTrolley.Core.Models
{
	public class UserData: ArkTrolley.WebService.Models.UserDataItem
	{
		[PrimaryKey, AutoIncrement]
		public int Id { get; set; }
	}
}

