using System;

namespace ArkTrolley.WebService.Models.PostModel
{
	public class PostUserData
	{
		public int gen_id{ get; set;}
		public string enm_type{ get; set;}
		public string str_password{ get; set;}
		public string str_name{ get; set;}
		public string str_email{ get; set;}
		public string str_address{ get; set;}
		public byte[] byt_picture{ get; set;}
	}
}

