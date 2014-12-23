using System;

namespace ArkTrolley.WebService.Models.PostModel
{
	public class UserDataResponse
	{
		public int responseCode { get; set; }
			public string responseMessage { get; set; }
			public object responseData { get; set; }
	}
}

