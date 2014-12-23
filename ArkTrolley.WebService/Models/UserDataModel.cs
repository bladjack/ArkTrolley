using System;

namespace ArkTrolley.WebService.Models
{
	public class UserDataItem
	{
		public string int_id { get; set; }
		public string enm_type { get; set; }
		public string int_active { get; set; }
		public string int_points { get; set; }
		public string dte_register { get; set; }
		public string dte_last_login { get; set; }
		public string dte_last_activity { get; set; }
		public string str_password { get; set; }
		public string str_name { get; set; }
		public string str_email { get; set; }
		public string str_address { get; set; }
	}

	public class UserDataModel
	{
		public int responseCode { get; set; }
		public string responseMessage { get; set; }
		public UserDataItem responseData { get; set; }
	}
}

