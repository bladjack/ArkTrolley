using System;
using System.Collections.Generic;

namespace ArkTrolley.WebService.Models
{
	public class ItemDataModel
	{
		public int responseCode { get; set; }
		public string responseMessage { get; set; }
		public ItemData responseData { get; set; 
	}
		public class ItemData
		{
			public List<object> coles { get; set; }
			public List<Woolworth> woolworths { get; set; }
			public List<object> aldi { get; set; }
			public List<object> google { get; set; }
		}

		public class Woolworth
		{
			public string URL { get; set; }
			public string Name { get; set; }
			public string Description { get; set; }
			public string Price { get; set; }
			public string OriginalPrice { get; set; }
			public string VolumeSize { get; set; }
			public string UnitSize { get; set; }
			public string UnitPrice { get; set; }
			public string Image { get; set; }
			public string Brand { get; set; }
		}
	}
}

