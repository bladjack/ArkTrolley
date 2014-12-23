using System;
using Cirrious.MvvmCross.Plugins.Sqlite;

namespace ArkTrolley.Core.Models
{
	public class StoreDataItem:ArkTrolley.WebService.Models.StoreItem
	{
		[PrimaryKey, AutoIncrement]
		public int Id { get; set; }
	}
}

