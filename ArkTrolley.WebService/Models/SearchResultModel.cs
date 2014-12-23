using System;
using System.Collections.Generic;

namespace ArkTrolley.WebService
{
	public class SearchResultModel
	{
		public SearchResultModel ()
		{
		}

		public int responseCode { get; set; }
		public string responseMessage { get; set; }
		public List<List<SearchItem>> responseData { get; set; }
	}

	public class SearchItem
	{
		public string int_id { get; set; }
		public string str_link { get; set; }
		public string int_store_id { get; set; }
		public string int_active { get; set; }
		public string int_views { get; set; }
		public string dte_register { get; set; }
		public object dte_last_view { get; set; }
		public string str_size { get; set; }
		public string str_code { get; set; }
		public string str_name { get; set; }
		public string str_description { get; set; }
		public string dte_review { get; set; }
		public string flt_price { get; set; }
		public string enm_price_type { get; set; }
		public bool bln_price_updated { get; set; }
		public string int_chain { get; set; }
		public string str_store_name { get; set; }
	}
}

