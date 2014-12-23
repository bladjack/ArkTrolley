using System;
using System.Collections.Generic;

namespace ArkTrolley.WebService.Models
{
	public class StoreDataModel
	{
		public int responseCode { get; set; }
		public string responseMessage { get; set; }
		public List<StoreItem> responseData { get; set; }
	}

	public class StoreItem
	{
		public int int_id { get; set; }
		public string enm_type { get; set; }
		public int int_chain { get; set; }
		public int int_active { get; set; }
		public string dte_register { get; set; }
		public string str_lat { get; set; }
		public string str_lon { get; set; }
		public string str_name { get; set; }
		public string str_address { get; set; }
	}
}

