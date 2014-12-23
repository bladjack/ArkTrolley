using System;

namespace ArkTrolley.WebService
{
	public class NewPasswordModel
	{
		public int responseCode { get; set; }
		public string responseMessage { get; set; }
		public PasswordItem responseData { get; set; }
	}

	public class PasswordItem
	{
		public string str_password { get; set; }
	}
}

