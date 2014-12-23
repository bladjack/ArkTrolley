using System;

namespace ArkTrolley.WebService
{
	public class NewRecipePostModel
	{
		public NewRecipePostModel ()
		{
		}
		public int int_user_id{ get; set;}
		public int int_store_id{ get; set;} 
		public int int_id{ get; set;}
		public string arr_items{ get; set;}
		public string str_recipe_name{ get; set;}
		public string str_recipe_description{ get; set;}
		public byte[] byt_picture{ get; set;}
	}
}

