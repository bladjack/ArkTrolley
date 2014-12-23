
using System;
using System.Drawing;

using MonoTouch.Foundation;
using MonoTouch.UIKit;
using ArkTrolley.iOS.Core.ViewModels;
using ArkTrolley.iOS.Core.AppHelper;
using ArkTrolley.iPhone.UserControls;
using ArkTrolley.Core.AppHelper;

namespace ArkTrolley.iPhone.Views
{
	public partial class ProfileView : BaseViewController
	{
		protected ProfileViewModel PageViewModel {
			get {
				return (ProfileViewModel)base.ViewModel;
			}
			set { 
				base.ViewModel = value;
			}
		}
		public ProfileView () : base ("ProfileView",  "ArkTrolley",null)
		{

		}
		public override void StartView ()
		{
			AddLogos ();
			TopBarButtons ();

			if (UIScreen.MainScreen.Bounds.Height > 500) {
				TopMargin = TopMargin + 20;
				BottomMargin = BottomMargin + 20;
			} else {
				TopMargin = TopMargin + 10;
				BottomMargin = BottomMargin + 10;
			}

			MenuList ();


			var botttomView = UserInfoHub2  ();	
			if (UIScreen.MainScreen.Bounds.Height > 500) {
				botttomView.Frame = new RectangleF (10, TopMargin + 340, 300, 100);
			} else {
				botttomView.Frame = new RectangleF (10, TopMargin + 260, 300, 100);
			}
			View.AddSubview (botttomView);
			//ShowProgressLoading = true;

			PageViewModel.PropertyChanged += PageViewModelPropertyChanged;
		}

		private void PageViewModelPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e) {

			if (e.PropertyName == "IsProgessRingVisible") {
				ShowProgressLoading = PageViewModel.IsProgessRingVisible;
			} else if (e.PropertyName == "CurrentTrolleyData") {
				var displayBound = UIScreen.MainScreen.Bounds;
				int width = (int)(displayBound.Height) - TopMargin - BottomMargin;
				if (PageViewModel.CurrentTrolleyData.responseCode == -2 || PageViewModel.CurrentTrolleyData.responseCode == -4) {
					var trolleyView = FinishedCurrentTrolleyView.Show (new RectangleF (0, TopMargin + 30, View.Frame.Width, width - 20), SharedData.CurrentLoginedUser.str_name, SharedData.CurrentSelectedStore.str_name, PageViewModel);
					this.View.Add (trolleyView);
				} else if (PageViewModel.CurrentTrolleyData.responseCode > 0) {
					var trolleyView = CurrentTrolleyView.Show (new RectangleF (0, TopMargin + 30, View.Frame.Width, width - 20), SharedData.CurrentLoginedUser.str_name, SharedData.CurrentSelectedStore.str_name, PageViewModel.CurrentTrolleyData, PageViewModel);
					this.View.Add (trolleyView);
				} else if (PageViewModel.CurrentTrolleyData.responseCode == -5) {
					PageViewModel.SetCurrentTrolleyStatus (ArkTrolley.Core.AppHelper.AppEnums.CurrentTrolleyStatusType.currenttrolley, string.Empty);
				}
			} else if (e.PropertyName == "MyListData") {
				var displayBound = UIScreen.MainScreen.Bounds;
				int width = (int)(displayBound.Height) - TopMargin - BottomMargin;
				if (PageViewModel.MyListData.Count > 0) {
					var trolleyView = MyListView.Show (new RectangleF (0, TopMargin + 30, View.Frame.Width, width - 20), SharedData.CurrentLoginedUser.str_name, SharedData.CurrentSelectedStore.str_name, PageViewModel.MyListData, PageViewModel);
					this.View.Add (trolleyView);
				} 
			} else if (e.PropertyName == "MyTicketsData") {
				var displayBound = UIScreen.MainScreen.Bounds;
				int width = (int)(displayBound.Height) - TopMargin - BottomMargin;
				if (PageViewModel.MyTicketsData.Count > 0) {
					var trolleyView = MyTicketView.Show (new RectangleF (0, TopMargin + 30, View.Frame.Width, width - 20), SharedData.CurrentLoginedUser.str_name, SharedData.CurrentSelectedStore.str_name, PageViewModel.MyTicketsData, PageViewModel);
					this.View.Add (trolleyView);
				} 
			} else if (e.PropertyName == "MyListDetailData") {
				var displayBound = UIScreen.MainScreen.Bounds;
				int width = (int)(displayBound.Height) - TopMargin - BottomMargin;
				if (PageViewModel.MyListDetailData.Count > 0) {
					var trolleyView = MySelectedList.Show (new RectangleF (0, TopMargin + 30, View.Frame.Width, width - 20), SharedData.CurrentLoginedUser.str_name, SharedData.CurrentSelectedStore.str_name, PageViewModel.MyListDetailData, PageViewModel);
					this.View.Add (trolleyView);
				} 
			} else if (e.PropertyName == "MyTicketDetailData") {
				var displayBound = UIScreen.MainScreen.Bounds;
				int width = (int)(displayBound.Height) - TopMargin - BottomMargin;
				if (PageViewModel.MyTicketDetailData.Count > 0) {
					var trolleyView = MySelectedTicket.Show (new RectangleF (0, TopMargin + 30, View.Frame.Width, width - 20), SharedData.CurrentLoginedUser.str_name, SharedData.CurrentSelectedStore.str_name, PageViewModel.MyTicketDetailData, PageViewModel);
					this.View.Add (trolleyView);
				} 
			} else if (e.PropertyName == "MyRecipesData") {
				var displayBound = UIScreen.MainScreen.Bounds;
				int width = (int)(displayBound.Height) - TopMargin - BottomMargin;
				if (PageViewModel.MyRecipesData.Count > 0) {
					var trolleyView = MyRecipesView.Show (new RectangleF (0, TopMargin + 30, View.Frame.Width, width - 20), SharedData.CurrentLoginedUser.str_name, SharedData.CurrentSelectedStore.str_name, PageViewModel.MyRecipesData, PageViewModel);
					this.View.Add (trolleyView);
				} 
			} else if (e.PropertyName == "MyRecipeDetailData") {
				var displayBound = UIScreen.MainScreen.Bounds;
				int width = (int)(displayBound.Height) - TopMargin - BottomMargin;
				if (PageViewModel.MyRecipeDetailData.arr_items.Count > 0) {
					var trolleyView = MySelectedRecipes.Show (new RectangleF (0, TopMargin + 30, View.Frame.Width, width - 20), SharedData.CurrentLoginedUser.str_name, SharedData.CurrentSelectedStore.str_name, PageViewModel.MyRecipeDetailData, PageViewModel);
					this.View.Add (trolleyView);
				} 
			} else if (e.PropertyName == "SearchResultData") {
				var displayBound = UIScreen.MainScreen.Bounds;
				int width = (int)(displayBound.Height) - TopMargin - BottomMargin;
				if (PageViewModel.SearchResultData.responseData.Count > 0) {
					var trolleyView = ItemsComparisionView.Show (new RectangleF (0, TopMargin + 30, View.Frame.Width, width - 20), PageViewModel);
					this.View.Add (trolleyView);
				} 
			} else if (e.PropertyName == "MyUpdatedRecipesData") {
				var displayBound = UIScreen.MainScreen.Bounds;
				int width = (int)(displayBound.Height) - TopMargin - BottomMargin;
				var createRecipeView = CreateRecipesView.Show (new RectangleF (0, TopMargin + 30, View.Frame.Width, width - 20), PageViewModel.MyUpdatedRecipesData, PageViewModel, this.NavigationController);
				this.View.Add (createRecipeView);
			}

		}

		private void TopBarButtons()
		{
			var selectedStore = CreateLebel ("No store selected.", 12.0f);
			selectedStore.Text = SharedData.CurrentSelectedStore.str_name;
			selectedStore.Frame = new RectangleF (5, TopMargin + 10, View.Frame.Width/2- 50, 20);

			var updateButton = UIButton.FromType (UIButtonType.Custom);
			updateButton.SetImage (UIImage.FromFile ("PickStore/UpdateBut.png").ImageWithRenderingMode (UIImageRenderingMode.AlwaysOriginal), UIControlState.Normal);
			updateButton.SetImage (UIImage.FromFile ("PickStore/UpdateButOv.png").ImageWithRenderingMode (UIImageRenderingMode.AlwaysOriginal), UIControlState.Highlighted);
			updateButton.SetImage (UIImage.FromFile ("PickStore/UpdateButOv.png").ImageWithRenderingMode (UIImageRenderingMode.AlwaysOriginal), UIControlState.Disabled);
			updateButton.Frame = new RectangleF (View.Frame.Width/2 - 38, TopMargin + 10, 43, 20);


			var userName = CreateLebel ("Jose", 12.0f);
			userName.Text = SharedData.CurrentLoginedUser.str_name;

			userName.Frame = new RectangleF (View.Frame.Width/2+20, TopMargin + 10,View.Frame.Width/2 - 50, 20);

			var logoutButton = UIButton.FromType (UIButtonType.Custom);
			logoutButton.SetImage (UIImage.FromFile ("PickStore/LogOutBut.png").ImageWithRenderingMode (UIImageRenderingMode.AlwaysOriginal), UIControlState.Normal);
			logoutButton.SetImage (UIImage.FromFile ("PickStore/LogOutBut.png").ImageWithRenderingMode (UIImageRenderingMode.AlwaysOriginal), UIControlState.Highlighted);
			logoutButton.SetImage (UIImage.FromFile ("PickStore/LogOutBut.png").ImageWithRenderingMode (UIImageRenderingMode.AlwaysOriginal), UIControlState.Disabled);
			logoutButton.Frame = new RectangleF (View.Frame.Width - 50, TopMargin + 10, 43, 20);

			var lineimage = new UIImageView (UIImage.FromBundle ("PickStore/LineBlue.png"));
			lineimage.Frame = new RectangleF (0, TopMargin + 35, View.Frame.Width, 2);


			View.AddSubview (selectedStore);
			View.AddSubview (updateButton);
			View.AddSubview (userName);
			View.AddSubview (logoutButton);
			View.AddSubview (lineimage);

			updateButton.UserInteractionEnabled=true;

			updateButton.AddGestureRecognizer (new UITapGestureRecognizer (() => {
				PageViewModel.GoBack();
			}));

			logoutButton.UserInteractionEnabled=true;

			logoutButton.AddGestureRecognizer (new UITapGestureRecognizer (() => {
				PageViewModel.NavigatedToLoginPage();
			}));

		}



		private UIView UserInfoHub2()
		{
			var userView = new UIView ();

			var backGroundImage = new UIImageView (UIImage.FromBundle ("Profile/Rect.png"));
			backGroundImage.Frame = new RectangleF (0, 0, 300, 81);

			var BackMenuBut = new UIImageView (UIImage.FromBundle ("Profile/BackMenuBut.png"));
			BackMenuBut.Frame = new RectangleF (20, 10, 55 , 47);

			var ArkBut = new UIImageView (UIImage.FromBundle ("Profile/ArkBut.png"));
			ArkBut.Frame = new RectangleF (120, 10, 55 , 47);

			var BarCodeBut = new UIImageView (UIImage.FromBundle ("Profile/BarCodeBut.png"));
			BarCodeBut.Frame = new RectangleF (220, 7, 60, 47);

			userView.AddSubview (backGroundImage);
			userView.AddSubview (BarCodeBut);
			userView.AddSubview (BackMenuBut);
			userView.AddSubview (ArkBut);

			return userView;
		}


		private void MenuList()
		{
			var myProfile = UIMenuItem ("Profile/ProfileBut.png", "MY PROFILE");
			var myList = UIMenuItem ("Profile/ListBut.png", "MY LIST");
			var myRecipes = UIMenuItem ("Profile/RecipesBut.png", "MY RECIPES");
			var searchItems = UIMenuItem ("Profile/SearchBut.png", "SEARCH ITEM");
			var ticketsItems = UIMenuItem ("Profile/my_tickets_button_regular.png", "MY TICKETS");


			myProfile.Frame = new RectangleF (10, TopMargin + 50, 82, 120);
			myList.Frame = new RectangleF (115, TopMargin + 50, 82, 120);
			myRecipes.Frame = new RectangleF (220, TopMargin + 50, 82, 120);
			searchItems.Frame = new RectangleF (10, TopMargin + 160, 82, 120);
			ticketsItems.Frame = new RectangleF (115, TopMargin + 160, 82, 120);



			var displayBound = UIScreen.MainScreen.Bounds;

			int width = (int)(displayBound.Height) - TopMargin - BottomMargin;


			myList.UserInteractionEnabled = true;

			myList.AddGestureRecognizer (new UITapGestureRecognizer (() => {
				PageViewModel.GetMyListData ();
			}));

			ticketsItems.UserInteractionEnabled = true;

			ticketsItems.AddGestureRecognizer (new UITapGestureRecognizer (() => {
				PageViewModel.GetMyTicketsData ();
			}));

			myRecipes.UserInteractionEnabled = true;

			myRecipes.AddGestureRecognizer (new UITapGestureRecognizer (() => {
				PageViewModel.GetMyRecipesData ();
			}));

			myProfile.UserInteractionEnabled = true;

			myProfile.AddGestureRecognizer (new UITapGestureRecognizer (() => {
				var signUpBound = UIScreen.MainScreen.Bounds;
				int height = (int)(signUpBound.Height) - TopMargin - BottomMargin;

				var signUpView = new SignupView (new RectangleF (10, TopMargin, View.Frame.Width, height - 20), this.NavigationController);
				this.View.Add (signUpView);

			}));

			searchItems.UserInteractionEnabled = true;

			searchItems.AddGestureRecognizer (new UITapGestureRecognizer (() => {
				var signUpBound = UIScreen.MainScreen.Bounds;
				int height = (int)(signUpBound.Height) - TopMargin - BottomMargin;

				var searchItemView = SearchView.Show (new RectangleF (0, TopMargin + 30, View.Frame.Width, width - 20), PageViewModel);
				this.View.Add (searchItemView);
			}));


			View.AddSubview (myProfile);
			View.AddSubview (myList);
			View.AddSubview (myRecipes);
			View.AddSubview (searchItems);
			View.AddSubview (ticketsItems);
		}

		private UIView UIMenuItem(string imagePath, string title)
		{
			var menu = new UIView ();
			var menuImage = new UIImageView (UIImage.FromBundle (imagePath));
			menuImage.Frame = new RectangleF (0, 0, 79, 68);

			var menuName = CreateLebel (title, 10.0f);
			menuName.Frame = new RectangleF (0, 75, 85, 32);
			menuName.TextAlignment = UITextAlignment.Center;

			menu.Add (menuImage);
			menu.Add (menuName);

			return menu;
		}
	}
}

