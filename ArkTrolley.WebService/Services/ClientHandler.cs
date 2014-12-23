using System;
using ArkTrolley.WebService.HttpClientHandler;
using System.Threading.Tasks;
using ArkTrolley.WebService.Models;
using System.Net;
using System.Net.Http;
using System.Collections.Generic;
using System.Xml;
using System.Linq;
using Newtonsoft.Json;
using System.Xml.Serialization;
using ArkTrolley.WebService.Models.PostModel;

namespace ArkTrolley.WebService.Services
{
	public class ClientHandler:IClientHandler
	{

		public ClientHandler ()
		{
		}

		public async Task<ConfigurationModel> ServerConfigManagement (DeviceSize size, DeviceType type, string model)
		{
			try {
				var url = AppSettings.GetSrvConfigManagement (size, type, model);
				var responce = await HttpClientController.GetDocument (url);
				if (!responce.Response.IsSucceeded)
					return null;
				var serviceResponse = Newtonsoft.Json.JsonConvert.DeserializeObject<ConfigurationModel> (responce.DataString);
				return serviceResponse;
			} catch (Exception exp) {
				throw exp;
			}
		}


		public async Task<ItemDataModel> GetItemData (string code,int storeid, int range)
		{
			try {
				var url = AppSettings.GetItemDataUrl (code, storeid, range);
				var responce = await HttpClientController.GetDocument (url);
				if (!responce.Response.IsSucceeded)
					return null;
				var serviceResponse = Newtonsoft.Json.JsonConvert.DeserializeObject<ItemDataModel>(responce.DataString);
				return serviceResponse;
			} catch (Exception exp) {
				throw exp;
			}
		}

		public async Task<ItemDataModel> GetItemPriceScrapping (string code,int storeid)
		{
			try {
				var url = AppSettings.GetItemPriceScrappingUrl (code, storeid);
				var responce = await HttpClientController.GetDocument (url);
				if (!responce.Response.IsSucceeded)
					return null;
				var serviceResponse = Newtonsoft.Json.JsonConvert.DeserializeObject<ItemDataModel>(responce.DataString);
				return serviceResponse;
			} catch (Exception exp) {
				throw exp;
			}
		}
		public async Task<string> GetItemGoogleSearchUrl (string code)
		{
			try {
				var url = AppSettings.GetItemGoogleSearchUrl (code);
				var responce = await HttpClientController.GetDocument (url);
				if (!responce.Response.IsSucceeded)
					return null;
				var serviceResponse = Newtonsoft.Json.JsonConvert.DeserializeObject<string>(responce.DataString);
				return serviceResponse;
			} catch (Exception exp) {
				throw exp;
			}
		}

		public async Task<UserDataModel> GetUserData (string user, string password)
		{
			try {
				var url = AppSettings.GetUserDataUrl (user,password);
				var responce = await HttpClientController.GetDocument (url);
				if (!responce.Response.IsSucceeded)
					return null;
				var serviceResponse = Newtonsoft.Json.JsonConvert.DeserializeObject<UserDataModel>(responce.DataString);
				return serviceResponse;
			} catch (Exception exp) {
				throw exp;
			}
		}


		public async Task<UserDataResponse>  GetUserNewPassword (string id){
			try {
				var model = new UserNewPasswordPostModel (){ gen_id = id };
				var url = AppSettings.GetUserNewPasswordUrl ();
				var jsonString = Newtonsoft.Json.JsonConvert.SerializeObject (model);
				StringContent queryString = new StringContent (jsonString);
				var responce = await HttpClientController.PostDocument (url, queryString);
				if (!responce.Response.IsSucceeded)
					return null;
				var serviceResponse = Newtonsoft.Json.JsonConvert.DeserializeObject<UserDataResponse> (responce.DataString);
				return serviceResponse;
			} catch (Exception exp) {
				throw exp;
			}
		}

		public async Task<UserDataResponse>  Logout (string id){
			try {
				var model = new UserNewPasswordPostModel (){ gen_id = id };
				var url = AppSettings.SetUserLogoutUrl (id);
				var jsonString = Newtonsoft.Json.JsonConvert.SerializeObject (model);
				StringContent queryString = new StringContent (jsonString);
				var responce = await HttpClientController.PostDocument (url, queryString);
				if (!responce.Response.IsSucceeded)
					return null;
				var serviceResponse = Newtonsoft.Json.JsonConvert.DeserializeObject<UserDataResponse> (responce.DataString);
				return serviceResponse;
			} catch (Exception exp) {
				throw exp;
			}
		}


		public async Task<StoreDataModel> GetStoreData (float lat, float lon, int range)
		{
			try {
				var url = AppSettings.GetStoreDataUrl (lat, lon, range,0);
				var responce = await HttpClientController.GetDocument (url);
				if (!responce.Response.IsSucceeded)
					return null;
				var serviceResponse = Newtonsoft.Json.JsonConvert.DeserializeObject<StoreDataModel>(responce.DataString);
				return serviceResponse;
			} catch (Exception exp) {
				throw exp;
			}
		}

		public async Task<SearchResultModel> GetItemsData  (string str_keywords,float flt_lat,float flt_lon, int int_store_id)
		{
			try {
				var url = AppSettings.GetItemsDataUrl (str_keywords,flt_lat, flt_lon, int_store_id);
				var responce = await HttpClientController.GetDocument (url);
				if (!responce.Response.IsSucceeded)
					return null;
				var serviceResponse = Newtonsoft.Json.JsonConvert.DeserializeObject<SearchResultModel>(responce.DataString);
				return serviceResponse;
			} catch (Exception exp) {
				throw exp;
			}
		}

		public async Task<UserDataResponse> SetUserData (int id, PostUserData data)
		{
			try {
				data.gen_id= id;
				var url = AppSettings.SetUserDataUrl ();
				var jsonString=Newtonsoft.Json.JsonConvert.SerializeObject(data);
				StringContent queryString = new StringContent(jsonString);
				var responce = await HttpClientController.PostDocument (url,queryString);
				if (!responce.Response.IsSucceeded)
					return null;
				var serviceResponse = Newtonsoft.Json.JsonConvert.DeserializeObject<UserDataResponse>(responce.DataString);
				return serviceResponse;
			} catch (Exception exp) {
				throw exp;
			}
			return null;
		}

		public async Task<UserDataResponse> CreateNewRecipe (NewRecipePostModel data)
		{
			try {
				var url = AppSettings.CreateNewRecipeUrl ();
				var jsonString=Newtonsoft.Json.JsonConvert.SerializeObject(data);
				StringContent queryString = new StringContent(jsonString);
				var responce = await HttpClientController.PostDocument (url,queryString);
				if (!responce.Response.IsSucceeded)
					return null;
				var serviceResponse = Newtonsoft.Json.JsonConvert.DeserializeObject<UserDataResponse>(responce.DataString);
				return serviceResponse;
			} catch (Exception exp) {
				throw exp;
			}
			return null;
		}

		public async Task<CurrentStoreModel> GetCurrentTrolleyDataUrl (string int_user_id, float flt_lat, float flt_lon)
		{
			try {
				var url = AppSettings.GetCurrentTrolleyDataUrl (int_user_id,flt_lat,flt_lon);
				var responce = await HttpClientController.GetDocument (url);
				if (!responce.Response.IsSucceeded)
					return null;
				var serviceResponse = Newtonsoft.Json.JsonConvert.DeserializeObject<CurrentStoreModel> (responce.DataString);
				return serviceResponse;
			} catch (Exception exp) {
				throw exp;
			}
		}

		public async Task<MyListResponse> GetMyListData (string int_user_id)
		{
			try {
				var url = AppSettings.GetMyListDataUrl (int_user_id);
				var responce = await HttpClientController.GetDocument (url);
				if (!responce.Response.IsSucceeded)
					return null;
				var serviceResponse = Newtonsoft.Json.JsonConvert.DeserializeObject<MyListResponse> (responce.DataString);
				return serviceResponse;
			} catch (Exception exp) {
				throw exp;
			}
		}


		public async Task<MyTicketsResponse> GetMyTicketsData (string int_user_id)
		{
			try {
				var url = AppSettings.GetMyTicketsDataUrl (int_user_id);
				var responce = await HttpClientController.GetDocument (url);
				if (!responce.Response.IsSucceeded)
					return null;
				var serviceResponse = Newtonsoft.Json.JsonConvert.DeserializeObject<MyTicketsResponse> (responce.DataString);
				return serviceResponse;
			} catch (Exception exp) {
				throw exp;
			}
		}

		public async Task<MyRecipesResponse> GetMyRecipesData (string int_user_id)
		{
			try {
				var url = AppSettings.GetMyRecipesDataUrl (int_user_id);
				var responce = await HttpClientController.GetDocument (url);
				if (!responce.Response.IsSucceeded)
					return null;
				var serviceResponse = Newtonsoft.Json.JsonConvert.DeserializeObject<MyRecipesResponse> (responce.DataString);
				return serviceResponse;
			} catch (Exception exp) {
				throw exp;
			}
		}

		public async Task<MyTicketDetailResponse> GetMyTicketDetailData (string int_user_id,string int_id)
		{
			try {
				var url = AppSettings.GetMyTicketDetailDataUrl (int_user_id,int_id);
				var responce = await HttpClientController.GetDocument (url);
				if (!responce.Response.IsSucceeded)
					return null;
				var serviceResponse = Newtonsoft.Json.JsonConvert.DeserializeObject<MyTicketDetailResponse> (responce.DataString);
				return serviceResponse;
			} catch (Exception exp) {
				throw exp;
			}
		}

		public async Task<MyRecipeDetailResponse> GetMyRecipeDetailData (string int_user_id, string int_id)
		{
			try {
				var url = AppSettings.GetMyRecipeDetailDataUrl (int_user_id,int_id);
				var responce = await HttpClientController.GetDocument (url);
				if (!responce.Response.IsSucceeded)
					return null;
				var serviceResponse = Newtonsoft.Json.JsonConvert.DeserializeObject<MyRecipeDetailResponse> (responce.DataString);
				return serviceResponse;
			} catch (Exception exp) {
				throw exp;
			}
		}

		public async Task<MyListDetailResponse> GetMyListDetailData (string int_user_id, string int_id)
		{
			try {
				var url = AppSettings.GetMyListDetailDataUrl (int_user_id,int_id);
				var responce = await HttpClientController.GetDocument (url);
				if (!responce.Response.IsSucceeded)
					return null;
				var serviceResponse = Newtonsoft.Json.JsonConvert.DeserializeObject<MyListDetailResponse> (responce.DataString);
				return serviceResponse;
			} catch (Exception exp) {
				throw exp;
			}
		}



		public async Task<UserDataResponse> SetRemoveList (RemoveItemModel item)
		{
			try {
				var url = AppSettings.SetRemoveListUrl ();
				var jsonString = Newtonsoft.Json.JsonConvert.SerializeObject (item);
				StringContent queryString = new StringContent (jsonString);
				var responce = await HttpClientController.PostDocument (url, queryString);
				if (!responce.Response.IsSucceeded)
					return null;
				var serviceResponse = Newtonsoft.Json.JsonConvert.DeserializeObject<UserDataResponse> (responce.DataString);
				return serviceResponse;
			} catch (Exception exp) {
				throw exp;
			}
		}


		public async Task<UserDataResponse> SetRemoveListItem (RemoveListItemModel item)
		{
			try {
				var url = AppSettings.SetRemoveListItemUrl ();
				var jsonString = Newtonsoft.Json.JsonConvert.SerializeObject (item);
				StringContent queryString = new StringContent (jsonString);
				var responce = await HttpClientController.PostDocument (url, queryString);
				if (!responce.Response.IsSucceeded)
					return null;
				var serviceResponse = Newtonsoft.Json.JsonConvert.DeserializeObject<UserDataResponse> (responce.DataString);
				return serviceResponse;
			} catch (Exception exp) {
				throw exp;
			}
		}

		public async Task<UserDataResponse> SetCurrentTrolleyStatus (PostCurrentTrolleyStatus item)
		{
			try {
				var url = AppSettings.SetCurrentTrolleyStatusUrl ();
				var jsonString = Newtonsoft.Json.JsonConvert.SerializeObject (item);
				StringContent queryString = new StringContent (jsonString);
				var responce = await HttpClientController.PostDocument (url, queryString);
				if (!responce.Response.IsSucceeded)
					return null;
				var serviceResponse = Newtonsoft.Json.JsonConvert.DeserializeObject<UserDataResponse> (responce.DataString);
				return serviceResponse;
			} catch (Exception exp) {
				throw exp;
			}
		}


	}
}

