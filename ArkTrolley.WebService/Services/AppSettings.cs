using System;
using System.Net.Http;
using System.Collections.Generic;

namespace ArkTrolley.WebService
{
	public class AppSettings
	{
		public const string baseurl="http://198.38.82.51/~adminat/index.php/";

		public AppSettings ()
		{
		}

		public static string GetSrvConfigManagement(DeviceSize size,DeviceType type, string model)
		{
			return baseurl + string.Format ("srvConfigManagement/getConfigsData/enm_device_size/{0}/enm_device_type/{1}/str_device_model/{2}", size.ToString (), type.ToString (), model);
		}

		public static string GetItemDataUrl (string code,int storeid, int range)
		{
			return baseurl + String.Format ("srvItemManagement/getItemData/code/{0}/storeid/{1}/range/{2}/format/json",code,storeid,range);
		}

		public static string GetItemPriceScrappingUrl (string code,int storeid)
		{
			return baseurl + String.Format ("srvItemManagement/getItemPriceScrapping/code/{0}/storeid/{1}/format/json",code,storeid);
		}
		public static string GetItemGoogleSearchUrl (string code)
		{
			return baseurl + String.Format ("srvItemManagement/getItemGoogleSearch/code/{0}/format/json",code);
		}

		public static string  GetUserNewPasswordUrl (){
			return baseurl + String.Format ("srvUserManagement/setUserNewPassword/format/json");
		}





		public static string SetUserLogoutUrl (string id)
		{
			return baseurl + String.Format ("srvUserManagement/setUserLogout/format/json",id);
		}


		public static string GetUserDataUrl (string user, string password)
		{
			return baseurl + String.Format ("srvUserManagement/getUserData/gen_id/{0}/str_password/{1}/format/json",user,password);
		}

		public static string GetItemsDataUrl (string str_keywords,float flt_lat,float flt_lon, int int_store_id)
		{
			return baseurl + String.Format ("srvItemManagement/getItemsData/str_keywords/{0}/flt_lat/{1}/flt_lon/{2}/int_store_id/{3}/format/json", str_keywords, flt_lat, flt_lon, int_store_id);
		}

		//Create/Edit user profile

		public static string SetUserDataUrl ()
		{
			return baseurl + String.Format ("srvUserManagement/setUserData/format/json");
		}

		public static string CreateNewRecipeUrl ()
		{
			return baseurl + String.Format ("srvListManagement/setMyRecipeData/format/json");
		}

		//- Main menu screen

		public static string SetUserLogoutPostUrl (string user, string password)
		{
			return baseurl + String.Format ("srvUserManagement/setUserLogout");
		}

		public static string GetStoreDataUrl (float flt_lat, float flt_lon,int  int_range,int int_id)
		{
			return baseurl + String.Format ("srvStoreManagement/getStoreData/flt_lat/{0}/flt_lon/{1}/int_range/{2}/int_id/{3}/format/json",flt_lat,flt_lon,int_range,int_id);
		}

		public static string GetCurrentTrolleyDataUrl (string int_user_id, float flt_lat,float flt_lon)
		{
			return baseurl + String.Format ("srvListManagement/getCurrentTrolleyData/int_user_id/{0}/flt_lat/{1}/flt_lon/{2}/format/json",int_user_id,flt_lat,flt_lon);
		}

	
		public static string SetRemoveListItemUrl ()
		{
			return baseurl + String.Format ("srvListManagement/setRemoveListItem/format/json");
		}

		public static string GetMyRecipeDataUrl (int int_user_id,int int_id)
		{
			return baseurl + String.Format ("srvListManagement/getMyRecipeData/int_user_id/{0}/int_id/{1}/format/json",int_user_id,int_id);
		}

		public static string SetMyRecipeDataUrl ()
		{
			return baseurl + String.Format ("srvListManagement/setMyRecipeData");
		}

		public static string SetRemoveListUrl ()
		{
			return baseurl + String.Format ("srvListManagement/setRemoveList/format/json");
		}

		public static string SetCurrentTrolleyStatusUrl()
		{
			return baseurl + String.Format ("srvListManagement/setCurrentTrolleyStatus/format/json");
		}

		public static string GetMyListDataUrl (string int_user_id)
		{
			return baseurl + String.Format ("srvListManagement/getMyListsData/int_user_id/{0}/format/json",int_user_id);
		}

		public static string GetMyTicketsDataUrl (string int_user_id)
		{
			return baseurl + String.Format ("srvListManagement/getMyTicketsData/int_user_id/{0}/format/json",int_user_id);
		}

		public static string GetMyTicketDetailDataUrl (string int_user_id,string int_id)
		{
			return baseurl + String.Format ("srvListManagement/getMyTicketData/int_user_id/{0}/int_id/{1}/format/json",int_user_id,int_id);
		}

		public static string GetMyRecipesDataUrl (string int_user_id)
		{
			return baseurl + String.Format ("srvListManagement/getMyRecipesData/int_user_id/{0}/format/json",int_user_id);
		}

		public static string GetMyRecipeDetailDataUrl (string int_user_id,string int_id)
		{
			return baseurl + String.Format ("srvListManagement/getMyRecipeData/int_user_id/{0}/int_id/{1}/format/json",int_user_id,int_id);
		}

		public static string GetMyListDetailDataUrl (string int_user_id,string int_id)
		{
			return baseurl + String.Format ("srvListManagement/getMyListData/int_user_id/{0}/int_id/{1}/format/json",int_user_id,int_id);
		}


		////// App Configuration path



		///

	}

}

