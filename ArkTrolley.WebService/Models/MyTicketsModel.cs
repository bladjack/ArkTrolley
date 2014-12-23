using System;
using System.Collections.Generic;

namespace ArkTrolley.WebService
{
	public class MyTicketsModel
	{
		public string int_id { get; set; }
		public string enm_type { get; set; }
		public string int_active { get; set; }
		public string int_store_id { get; set; }
		public string int_user_id { get; set; }
		public string dte_register { get; set; }
		public string dte_closed { get; set; }
		public string str_comments { get; set; }
		public string str_recipe_name { get; set; }
		public string str_recipe_description { get; set; }
		public string flt_total_cost { get; set; }
	}

	public class MyTicketsResponse
	{
		public int responseCode { get; set; }
		public string responseMessage { get; set; }
		public List<MyTicketsModel> responseData { get; set; }
	}
}

