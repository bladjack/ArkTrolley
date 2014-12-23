using System;
using Cirrious.MvvmCross.ViewModels;
using System.Collections.Generic;
using ArkTrolley.WebService.Services;
using ArkTrolley.Core.AppHelper;
using ArkTrolley.WebService;
using System.Threading.Tasks;
using ArkTrolley.WebService.Models.PostModel;
using ArkTrolley.Core.AppHelper.AppEnums;

namespace ArkTrolley.Core.ViewModels
{
	public class ProfileViewModel:BaseViewModel
	{
		private CurrentStoreModel currentTrolleyData;

		public CurrentStoreModel CurrentTrolleyData
		{
			get{ return currentTrolleyData;}
			set{ 
				currentTrolleyData = value;
				RaisePropertyChanged(() => CurrentTrolleyData); 
			}
		}
		private List<MyListModel> myListData;

		public List<MyListModel> MyListData
		{
			get{ return myListData;}
			set{ 
				myListData = value;
				RaisePropertyChanged(() => MyListData); 
			}
		}

		private List<MyTicketsModel> myTicketsData;

		public List<MyTicketsModel> MyTicketsData
		{
			get{ return myTicketsData;}
			set{ 
				myTicketsData = value;
				RaisePropertyChanged(() => MyTicketsData); 
			}
		}

		private List<MyRecipesModel> myRecipesData;

		public List<MyRecipesModel> MyRecipesData
		{
			get{ return myRecipesData;}
			set{ 
				myRecipesData = value;
				RaisePropertyChanged(() => MyRecipesData); 
			}
		}

		private List<MyRecipesModel> myUpdatedRecipesData;

		public List<MyRecipesModel> MyUpdatedRecipesData
		{
			get{ return myUpdatedRecipesData;}
			set{ 
				myUpdatedRecipesData = value;
				RaisePropertyChanged(() => MyUpdatedRecipesData); 
			}
		}

		private List<MyListDetailModel> myListDetailData;

		public List<MyListDetailModel> MyListDetailData
		{
			get{ return myListDetailData;}
			set{ 
				myListDetailData = value;
				RaisePropertyChanged(() => MyListDetailData); 
			}
		}

		private List<MyTicketDetailModel> myTicketDetailData;

		public List<MyTicketDetailModel> MyTicketDetailData {
			get{ return myTicketDetailData; }
			set { 
				myTicketDetailData = value;
				RaisePropertyChanged (() => MyTicketDetailData); 
			}
		}

		private MyRecipeDetailResponseData myRecipeDetailData;

		public MyRecipeDetailResponseData MyRecipeDetailData {
			get{ return myRecipeDetailData; }
			set { 
				myRecipeDetailData = value;
				RaisePropertyChanged (() => MyRecipeDetailData); 
			}
		}

		private SearchResultModel searchResultData;

		public SearchResultModel SearchResultData {
			get{ return searchResultData; }
			set { 
				searchResultData = value;
				RaisePropertyChanged (() => SearchResultData); 
			}
		}

		private List<MyTicketDetailModel> myselectedlist;
		public List<MyTicketDetailModel> Myselectedlist {
			get{ return myselectedlist; }
			set { 
				myselectedlist = value;
				RaisePropertyChanged (() => Myselectedlist); 
			}
		}

		public ProfileViewModel ()
		{
			CurrentTrolleyData = new CurrentStoreModel ();

			GetCurrentTrolleyData ();
		}

		private async void GetCurrentTrolleyData()
		{
			IsProgessRingVisible = true;
			try {

				var response = await ClientHandler.GetCurrentTrolleyDataUrl (SharedData.CurrentLoginedUser.int_id, -35.237418300000f, 149.067644300000f);
				CurrentTrolleyData = response;

			} catch {
			}
			IsProgessRingVisible = false;
		}
		public async Task<bool> GetSearchItemsData(string searchText)
		{
			IsProgessRingVisible = true;
			try {

				var response = await ClientHandler.GetItemsData (searchText, -35.237418300000f, 149.067644300000f, SharedData.CurrentSelectedStore.int_id);
				SearchResultData = response;
				IsProgessRingVisible = false;
				return true;

			} catch {
			}
			IsProgessRingVisible = false;
			return false;
		}

		public async void GetMyListData()
		{
			IsProgessRingVisible = true;
			try {
				var response = await ClientHandler.GetMyListData (SharedData.CurrentLoginedUser.int_id);
				MyListData = response.responseData;

			} catch {
			}
			IsProgessRingVisible = false;
		}

		public async void GetMyTicketsData()
		{
			IsProgessRingVisible = true;
			try {
				var response = await ClientHandler.GetMyTicketsData (SharedData.CurrentLoginedUser.int_id);
				MyTicketsData = response.responseData;

			} catch {
			}
			IsProgessRingVisible = false;
		}

		public async void GetMyRecipesData()
		{
			IsProgessRingVisible = true;
			try {
				var response = await ClientHandler.GetMyRecipesData (SharedData.CurrentLoginedUser.int_id);
				MyRecipesData = response.responseData;

			} catch {
			}
			IsProgessRingVisible = false;
		}

		public async void GetMyListDetailData(string int_id)
		{
			IsProgessRingVisible = true;
			try {
				var response = await ClientHandler.GetMyListDetailData (SharedData.CurrentLoginedUser.int_id,int_id);
				MyListDetailData = response.responseData.arr_items;

			} catch {
			}
			IsProgessRingVisible = false;
		}

		public async Task<bool> GetMyTicketDetailData(string int_id)
		{
			IsProgessRingVisible = true;
			try {
				var response = await ClientHandler.GetMyTicketDetailData (SharedData.CurrentLoginedUser.int_id,int_id);
				MyTicketDetailData = response.responseData.arr_items;

			} catch {
			}
			IsProgessRingVisible = false;
			return true;
		}

		public async void GetMyRecipesDetailData(string int_id)
		{
			IsProgessRingVisible = true;
			try {
				var response = await ClientHandler.GetMyRecipeDetailData (SharedData.CurrentLoginedUser.int_id,int_id);
				MyRecipeDetailData = response.responseData;

			} catch {
			}
			IsProgessRingVisible = false;
		}


		public async Task<UserDataResponse> SetRemoveList(string int_list_id, ListType enm_type)
		{
			IsProgessRingVisible = true;
			try {
				RemoveItemModel model = new RemoveItemModel ();
				model.enm_type = enm_type.ToString ();
				model.int_user_id = Convert.ToInt32 (SharedData.CurrentLoginedUser.int_id);
				model.int_list_id = Convert.ToInt32 (int_list_id);
				var response = await ClientHandler.SetRemoveList (model);
				IsProgessRingVisible = false;
				return response;

			} catch {
			}
			IsProgessRingVisible = false;
			return null;
		}

		public async Task<UserDataResponse> SetRemoveListItem(string int_list_id, ListType enm_type, string item_id)
		{
			IsProgessRingVisible = true;
			try {
				RemoveListItemModel model = new RemoveListItemModel ();
				model.enm_type = enm_type.ToString ();
				model.int_user_id = Convert.ToInt32 (SharedData.CurrentLoginedUser.int_id);
				model.int_list_id = Convert.ToInt32 (int_list_id);
				model.int_id= Convert.ToInt32(item_id);
				var response = await ClientHandler.SetRemoveListItem (model);
				IsProgessRingVisible = false;
				return response;

			} catch {
			}
			IsProgessRingVisible = false;
			return null;
		}

		public async Task<UserDataResponse> SetCurrentTrolleyStatus (CurrentTrolleyStatusType enm_status, string str_comments)
		{
			IsProgessRingVisible = true;
			try {
				PostCurrentTrolleyStatus model = new PostCurrentTrolleyStatus ();
				model.enm_status = enm_status.ToString ();
				model.int_user_id = Convert.ToInt32 (SharedData.CurrentLoginedUser.int_id);
				if (enm_status == CurrentTrolleyStatusType.mytickets) {
					model.int_list_id = CurrentTrolleyData.responseCode;
				} else {
					model.int_list_id = 0;
				}
				model.str_comments = str_comments;
				var response = await ClientHandler.SetCurrentTrolleyStatus (model);
				IsProgessRingVisible = false;
				return response;

			} catch {
			}
			IsProgessRingVisible = false;
			return null;
		}

		public async Task SaveRecipesData ()
		{
			IsProgessRingVisible = true;
			try {
				var response = await ClientHandler.GetMyRecipesData (SharedData.CurrentLoginedUser.int_id);
				MyUpdatedRecipesData = response.responseData;

			} catch {
			}
			IsProgessRingVisible = false;
		}

		public async Task<bool> CreateNewRecipe(int recipeid,string name, string description,byte[] picture)
		{
			IsProgessRingVisible = true;
			if (string.IsNullOrEmpty (name)) {
				await AppMessageDialog.ShowAppDialog ("Please enter the recipe name", "ArkTrolley");
			} else if (string.IsNullOrEmpty (description)) {
				await AppMessageDialog.ShowAppDialog ("Please enter the description", "ArkTrolley");
			}else{
				var id = 0;
				if (SharedData.CurrentLoginedUser != null)
					id = Convert.ToInt32 (SharedData.CurrentLoginedUser.int_id);
				NewRecipePostModel data = new NewRecipePostModel (){ 
					int_user_id=Convert.ToInt32(  SharedData.CurrentLoginedUser.int_id),
					int_store_id= SharedData.CurrentSelectedStore.int_id,
					int_id= recipeid,
					arr_items= GetItemIds(),
					str_recipe_name= name,
					str_recipe_description=description,
					byt_picture= picture
				};
				var response = await ClientHandler.CreateNewRecipe (data);
				await AppMessageDialog.ShowAppDialog (response.responseMessage, "ArkTrolley");	
			} 
			IsProgessRingVisible = false;
			return true;
		}

		private string GetItemIds()
		{
			string ids = string.Empty;
			foreach (var item in Myselectedlist) {
				ids = ids + item.int_id + "::";
			}

			return ids;
		}
	}
}

