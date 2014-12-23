using System;
using System.Threading.Tasks;
using ArkTrolley.WebService.Models;
using ArkTrolley.WebService.Models.PostModel;

namespace ArkTrolley.WebService.Services
{
	public interface IClientHandler
	{
		Task<ItemDataModel> GetItemData (string code,int storeid, int range);
		Task<ItemDataModel> GetItemPriceScrapping (string code, int storeid);
		Task<string> GetItemGoogleSearchUrl (string code);
		Task<UserDataModel> GetUserData (string user, string password);

		Task<StoreDataModel> GetStoreData (float lat, float lon, int range);
		Task<UserDataResponse>  GetUserNewPassword (string id);
		Task<UserDataResponse>  Logout (string id);
		Task<UserDataResponse> SetUserData (int id, PostUserData data);

		Task<ConfigurationModel> ServerConfigManagement (DeviceSize size, DeviceType type, string model);

		Task<CurrentStoreModel> GetCurrentTrolleyDataUrl (string int_user_id, float flt_lat, float flt_lon);

		Task<MyListResponse> GetMyListData (string int_user_id);

		Task<SearchResultModel> GetItemsData  (string str_keywords,float flt_lat,float flt_lon, int int_store_id);



		Task<MyTicketsResponse> GetMyTicketsData (string int_user_id);

		Task<MyRecipesResponse> GetMyRecipesData (string int_user_id);

		Task<MyTicketDetailResponse> GetMyTicketDetailData (string int_user_id, string int_id);

		Task<MyRecipeDetailResponse> GetMyRecipeDetailData (string int_user_id, string int_id);

		Task<MyListDetailResponse> GetMyListDetailData (string int_user_id, string int_id);

		Task<UserDataResponse> SetRemoveList (RemoveItemModel item);

		Task<UserDataResponse> SetRemoveListItem (RemoveListItemModel item);

		Task<UserDataResponse> SetCurrentTrolleyStatus (PostCurrentTrolleyStatus item);

		Task<UserDataResponse> CreateNewRecipe (NewRecipePostModel data);


	}
}

