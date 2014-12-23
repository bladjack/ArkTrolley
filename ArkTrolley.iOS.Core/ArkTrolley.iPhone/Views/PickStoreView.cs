
using System;
using System.Drawing;

using MonoTouch.Foundation;
using MonoTouch.UIKit;
using ArkTrolley.iOS.Core.ViewModels;
using ArkTrolley.iOS.Core.AppHelper;
using Cirrious.MvvmCross.Binding.BindingContext;
using ArkTrolley.Core.Models;
using System.Collections.Generic;
using ArkTrolley.Core.AppHelper;
using System.Threading.Tasks;

namespace ArkTrolley.iPhone.Views
{
	public partial class PickStoreView : BaseViewController
	{
		protected PickStoreViewModel PageViewModel {
			get {
				return (PickStoreViewModel)base.ViewModel;
			}
			set { 
				base.ViewModel = value;
			}
		}
		UIScrollView scrollView ;

		public PickStoreView () : base ("PickStoreView",  "ArkTrolley",null)
		{

		}
		public override void StartView ()
		{
			scrollView = new UIScrollView ();
			AddLogos ();
			TopBarButtons ();

			if (UIScreen.MainScreen.Bounds.Height > 500) {
				TopMargin = TopMargin + 20;
				BottomMargin = BottomMargin + 20;
			} else {
				TopMargin = TopMargin + 10;
				BottomMargin = BottomMargin + 10;
			}


			AddBackGroundAndTitle ();

			AddButtons (TopMargin);
			ShowProgressLoading = true;
			PageViewModel.PropertyChanged += PageViewModelPropertyChanged;
		}

		private void PageViewModelPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e) {

			if (e.PropertyName == "IsProgessRingVisible") {
				ShowProgressLoading = PageViewModel.IsProgessRingVisible;
			} else if (e.PropertyName == "Storelist") {
				AddStoreList( PageViewModel.Storelist,TopMargin+ 100 , BottomMargin);
			}
		}

		private void AddBackGroundAndTitle()
		{
			var displayBound = UIScreen.MainScreen.Bounds;

			int width = (int)(displayBound.Height) - TopMargin - BottomMargin;


			var backgroundImage = new UIImageView (UIImage.FromBundle ("Login/Box.png"));
			backgroundImage.Frame = new RectangleF (8, TopMargin + 30, 294, width);

			var labelImage = new UIImageView (UIImage.FromBundle ("PickStore/PickStore.png"));
			labelImage.Frame = new RectangleF (80, TopMargin + 50, 160, 20);

			var lineimage = new UIImageView (UIImage.FromBundle ("PickStore/LineBlue.png"));
			lineimage.Frame = new RectangleF (35, TopMargin + 80, View.Frame.Width- 70, 2);



			View.AddSubview (backgroundImage);
			View.AddSubview (labelImage);
			View.AddSubview (lineimage);
		}

		private void FlushScrollViewerData()
		{
			foreach (var sub in scrollView.Subviews)
			{
				sub.RemoveFromSuperview();
			}
		}

		private async void AddStoreList(List<StoreDataItem> storeList, int topmargin, int bottomargin)
		{
			FlushScrollViewerData ();
			scrollView.Frame = new RectangleF (0, topmargin, View.Frame.Width, bottomargin - topmargin-20);

			scrollView.BackgroundColor = CustomUIColor.FromHexString ("#00ffffff");
			scrollView.AutoresizingMask = UIViewAutoresizing.FlexibleWidth;
			scrollView.AlwaysBounceVertical = false;
			scrollView.ShowsVerticalScrollIndicator = false;

			int i = 0;
			topmargin = 0;
			foreach (var item in storeList) {

				var newUser1 = await StoreHub (
					               UIImage.FromBundle ("Common/Ark_lg.png"), 
					               item.str_name,
					               item.str_address,
					item.int_chain);
				topmargin = 10*i + 70 * i;

				newUser1.Frame = new RectangleF (35, topmargin, View.Frame.Width - 70, 70);
				scrollView.AddSubview (newUser1);

				newUser1.UserInteractionEnabled = true;

				newUser1.AddGestureRecognizer (new UITapGestureRecognizer (() => {
					SharedData.CurrentSelectedStore= item;
					PageViewModel.NavigatedToProfilePage();
				}));
				i++;
			}

			topmargin = 90 * (i);

			scrollView.ContentSize = new SizeF (0, topmargin);

			View.AddSubview (scrollView);

		}

		private void TopBarButtons()
		{
			var selectedStore = CreateLebel ("No store selected.", 12.0f);
			selectedStore.Frame = new RectangleF (5, TopMargin + 10, View.Frame.Width - 50, 20);

			var updateButton = UIButton.FromType (UIButtonType.Custom);
			updateButton.SetImage (UIImage.FromFile ("PickStore/UpdateBut.png").ImageWithRenderingMode (UIImageRenderingMode.AlwaysOriginal), UIControlState.Normal);
			updateButton.SetImage (UIImage.FromFile ("PickStore/UpdateButOv.png").ImageWithRenderingMode (UIImageRenderingMode.AlwaysOriginal), UIControlState.Highlighted);
			updateButton.SetImage (UIImage.FromFile ("PickStore/UpdateButOv.png").ImageWithRenderingMode (UIImageRenderingMode.AlwaysOriginal), UIControlState.Disabled);
			updateButton.Frame = new RectangleF (View.Frame.Width/2 -20 , TopMargin + 10, 43, 20);


			var userName = CreateLebel ("Jose", 12.0f);
			userName.Text = SharedData.CurrentLoginedUser.str_name;

			userName.Frame = new RectangleF (View.Frame.Width/2+30, TopMargin + 10,View.Frame.Width/2 - 60, 20);

			var logoutButton = UIButton.FromType (UIButtonType.Custom);
			logoutButton.SetImage (UIImage.FromFile ("PickStore/LogOutBut.png").ImageWithRenderingMode (UIImageRenderingMode.AlwaysOriginal), UIControlState.Normal);
			logoutButton.SetImage (UIImage.FromFile ("PickStore/LogOutBut.png").ImageWithRenderingMode (UIImageRenderingMode.AlwaysOriginal), UIControlState.Highlighted);
			logoutButton.SetImage (UIImage.FromFile ("PickStore/LogOutBut.png").ImageWithRenderingMode (UIImageRenderingMode.AlwaysOriginal), UIControlState.Disabled);
			logoutButton.Frame = new RectangleF (View.Frame.Width - 50, TopMargin + 10, 43, 20);

			var lineimage = new UIImageView (UIImage.FromBundle ("PickStore/LineBlue.png"));
			lineimage.Frame = new RectangleF (0, TopMargin + 35, View.Frame.Width, 2);

			logoutButton.UserInteractionEnabled=true;

			logoutButton.AddGestureRecognizer (new UITapGestureRecognizer (() => {
				PageViewModel.NavigatedToLoginPage();
			}));

			View.AddSubview (selectedStore);
			View.AddSubview (updateButton);
			View.AddSubview (userName);
			View.AddSubview (logoutButton);
			View.AddSubview (lineimage);
		}

		private void AddButtons(int topMargin)
		{

			var buttonTopMargin = topMargin;

			if (UIScreen.MainScreen.Bounds.Height > 500) {
				buttonTopMargin = buttonTopMargin + 400;
			} else {
				buttonTopMargin = buttonTopMargin + 330;
			}


			var refreshButton = UIButton.FromType (UIButtonType.Custom);
			refreshButton.SetImage (UIImage.FromFile ("PickStore/RefreshBut.png").ImageWithRenderingMode (UIImageRenderingMode.AlwaysOriginal), UIControlState.Normal);
			refreshButton.SetImage (UIImage.FromFile ("PickStore/RefreshButOv.png").ImageWithRenderingMode (UIImageRenderingMode.AlwaysOriginal), UIControlState.Highlighted);
			refreshButton.SetImage (UIImage.FromFile ("PickStore/RefreshButOv.png").ImageWithRenderingMode (UIImageRenderingMode.AlwaysOriginal), UIControlState.Disabled);


			var cancelButton = UIButton.FromType (UIButtonType.Custom);
			cancelButton.SetImage (UIImage.FromFile ("PickStore/CancelBut.png").ImageWithRenderingMode (UIImageRenderingMode.AlwaysOriginal), UIControlState.Normal);
			cancelButton.SetImage (UIImage.FromFile ("PickStore/CancelButOv.png").ImageWithRenderingMode (UIImageRenderingMode.AlwaysOriginal), UIControlState.Highlighted);
			cancelButton.SetImage (UIImage.FromFile ("PickStore/CancelButOv.png").ImageWithRenderingMode (UIImageRenderingMode.AlwaysOriginal), UIControlState.Disabled);


			//var loginButton = new UIImageView (UIImage.FromBundle ("Login/button.png"));
			//var cancelButton = new UIImageView (UIImage.FromBundle ("Login/button.png"));


			refreshButton.Frame = new RectangleF (65, buttonTopMargin, 80, 26);
			cancelButton.Frame = new RectangleF (180, buttonTopMargin, 80, 26);

			BottomMargin = buttonTopMargin;

			View.AddSubview (refreshButton);
			View.AddSubview (cancelButton);

			refreshButton.UserInteractionEnabled=true;

			refreshButton.AddGestureRecognizer (new UITapGestureRecognizer (() => {

				PageViewModel.RefreshData();
			}));

			cancelButton.UserInteractionEnabled=true;

			cancelButton.AddGestureRecognizer (new UITapGestureRecognizer (() => {
				PageViewModel.GoBack();
			}));

		}

		private async Task<UIView> StoreHub(UIImage userImage, string userName, string address, int id)
		{
			var userView = new UIView ();

			var backGroundImage = new UIImageView (UIImage.FromBundle ("PickStore/BoxStore.png"));
			backGroundImage.Frame = new RectangleF (0, 0, View.Frame.Width- 70, 70);

			//var userViewRectangle = new UIImageView (UIImage.FromBundle ("Common/Rect.png"));
			//userViewRectangle.Frame = new RectangleF (86, 10, 200, 55);

			var downloadimage= await BaseViewController.LoadImage(SharedData.CurrentConfigurationData.str_web_service_root+ SharedData.CurrentConfigurationData.str_store_chains_pictures_path+ id+".jpg");
			var userImageView=new UIImageView();
			if (downloadimage == null) {
				userImageView.Image = userImage;
			} else {
				userImageView.Image = downloadimage;
			}
			//new UIImageView (UIImage.FromBundle ("Common/Ark_lg.png"));
			userImageView.Frame = new RectangleF (4, 7, 73, 56);



			var userNameLabel = CreateLebel (userName, 12.0f);
			userNameLabel.Frame = new RectangleF (90, 10,170, 25);


			var emailLabel = CreateLebel (address, 10.0f);
			emailLabel.Frame = new RectangleF (90, 36, 170, 25);


			userView.AddSubview (backGroundImage);
			userView.AddSubview (userImageView);
			//userView.AddSubview (userViewRectangle);
			userView.AddSubview (userNameLabel);
			userView.AddSubview (emailLabel);


			return userView;
		}
	}
}

