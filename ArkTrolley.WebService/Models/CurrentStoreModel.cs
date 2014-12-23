using System;
using System.Collections.Generic;

namespace ArkTrolley.WebService
{
	public class CurrentTrolleyItem
	{
		public string int_id { get; set; }
		public string int_store_id { get; set; }
		public string int_active { get; set; }
		public string int_views { get; set; }
		public string dte_register { get; set; }
		public object dte_last_view { get; set; }
		public string str_size { get; set; }
		public string str_code { get; set; }
		public string str_name { get; set; }
		public string str_description { get; set; }
		public string enm_price_type { get; set; }
		public string flt_quantity { get; set; }
		public string flt_price { get; set; }
		public string flt_amount { get; set; }
		public string flt_tax { get; set; }
	}

	public class ResponseData
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
		public string str_store_name { get; set; }
		public List<CurrentTrolleyItem> arr_items { get; set; }
	}

	public class CurrentStoreModel
	{
		public int responseCode { get; set; }
		public string responseMessage { get; set; }
		public ResponseData responseData { get; set; }
	}
}

