
using System;
using System.Drawing;

using MonoTouch.Foundation;
using MonoTouch.UIKit;
using ArkTrolley.iOS.Core.UserControls;
using ArkTrolley.iOS.Core.AppHelper;
using ArkTrolley.iOS.Core.ViewModels;
using ArkTrolley.Core.AppHelper;
using System.Threading.Tasks;

namespace ArkTrolley.iPhone.Views
{
	public partial class MainView : BaseViewController
	{
		private UIScrollView scrollView;

		protected MainViewModel PageViewModel {
			get {
				return (MainViewModel)base.ViewModel;
			}
			set { 
				base.ViewModel = value;
			}
		}


		public MainView () : base ("MainView", "ArkTrolley",null)
		{

		}

		public async override void StartView ()
		{
			scrollView = new UIScrollView ();
			if (UIScreen.MainScreen.Bounds.Height > 500) {
				scrollView.Frame = new RectangleF (0, TopMargin + 120, ScreenWidth, ScreenHeight - (TopMargin + 120+ 150));
			}else {
				scrollView.Frame = new RectangleF (0, TopMargin + 100, ScreenWidth, ScreenHeight - (TopMargin + 100+150));
			}
			scrollView.BackgroundColor = CustomUIColor.FromHexString ("#00ffffff");
			scrollView.AutoresizingMask = UIViewAutoresizing.FlexibleWidth;
			scrollView.AlwaysBounceVertical = false;
			scrollView.ShowsVerticalScrollIndicator = false;
			

			AddLogos ();

			AddNewUser ();

			var maxHeight = await AddPreviousUser ();

			scrollView.ContentSize = new SizeF (0, maxHeight+ 80);

			View.AddSubview (scrollView);
		}

		private async void AddNewUser()
		{
			var newUser = await UserInfoHub (
				UIImage.FromBundle ("Common/Ark_lg.png"), 
				"Another user",
				"Didn't login on this device before.",
				string.Empty
			);	
			if (UIScreen.MainScreen.Bounds.Height > 500) {
				newUser.Frame = new RectangleF (10, TopMargin + 25, ScreenWidth-20, 100);
			} else {
				newUser.Frame = new RectangleF (10, TopMargin + 15, ScreenWidth-20, 100);
			}

			newUser.UserInteractionEnabled=true;

			newUser.AddGestureRecognizer (new UITapGestureRecognizer (() => {
				PageViewModel.NavigateToLoginScreen();
			}));

			View.AddSubview (newUser);
		}

		private async Task<int> AddPreviousUser()
		{
			int i = 0;
			int topMargin = 0;
			foreach (var item in PageViewModel.PreviousLoginUser) {

				var newUser1 = await UserInfoHub (
					               UIImage.FromBundle ("Common/Ark_lg.png"), 
					item.str_name,
					               item.str_email,
					item.int_id );	
				if (UIScreen.MainScreen.Bounds.Height > 500) {
					topMargin = 10 + 105 * i;
				} else {
					topMargin = 10 + 90 * i;
				}

				newUser1.Frame = new RectangleF (10, topMargin, 300, 100);
				scrollView.AddSubview (newUser1);

				newUser1.UserInteractionEnabled = true;

				newUser1.AddGestureRecognizer (new UITapGestureRecognizer (() => {
					PageViewModel.NavigatedToStorePickList(item);
				}));
				i++;
			}


			var newUser3 = UserInfoHub2 ();	
			if (UIScreen.MainScreen.Bounds.Height > 500) {
				newUser3.Frame = new RectangleF (10, ScreenHeight - 120, ScreenWidth - 20, 100);
			} else {
				newUser3.Frame = new RectangleF (10, ScreenHeight - 110, ScreenWidth - 20, 100);
			}

			View.AddSubview (newUser3);

			return topMargin;
		}

		private async Task<UIView> UserInfoHub(UIImage userImage, string userName, string email, string id)
		{
			var userView = new UIView ();

			var backGroundImage = new UIImageView (UIImage.FromBundle ("Common/Boton1.png"));
			backGroundImage.Frame = new RectangleF (0, 0, 300, 81);

			var userViewRectangle = new UIImageView (UIImage.FromBundle ("Common/Rect.png"));
			userViewRectangle.Frame = new RectangleF (86, 10, 200, 55);

			var userImageView = new UIImageView (); 
				//new UIImageView (UIImage.FromBundle ("Common/Ark_lg.png"));
			UIImage downloadimage=null;
			try{
				downloadimage= await BaseViewController.LoadImage(SharedData.CurrentConfigurationData.str_web_service_root+ SharedData.CurrentConfigurationData.str_users_pictures_path+ id+".jpg");
			}catch{
			}
			if (downloadimage == null) {
				userImageView.Image = userImage;
			} else {
				userImageView.Image = downloadimage;
			}

			userImageView.Frame = new RectangleF (20, 7, 60, 61);
			var userNameLabel = CreateLebel (userName, 12.0f);
			userNameLabel.Frame = new RectangleF (95, 14, 180, 25);
		

			var emailLabel = CreateLebel (email, 10.0f);
			emailLabel.Frame = new RectangleF (95, 38, 180, 25);
		

			userView.AddSubview (backGroundImage);
			userView.AddSubview (userImageView);
			userView.AddSubview (userViewRectangle);
			userView.AddSubview (userNameLabel);
			userView.AddSubview (emailLabel);


			return userView;
		}

		private UIView UserInfoHub2()
		{
			var userView = new UIView ();

			var backGroundImage = new UIImageView (UIImage.FromBundle ("Common/Boton1.png"));
			backGroundImage.Frame = new RectangleF (0, 0, 300, 81);

			var userViewBoxLeft = new UIImageView (UIImage.FromBundle ("Common/Box.png"));
			userViewBoxLeft.Frame = new RectangleF (86, 10, 55 , 55);

			var userViewBoxRight = new UIImageView (UIImage.FromBundle ("Common/Box.png"));
			userViewBoxRight.Frame = new RectangleF (159, 10, 55 , 55);

			var logoImage = new UIImageView (UIImage.FromBundle ("Common/Ark_lg.png"));
			logoImage.Frame = new RectangleF (20, 7, 60, 61);

			var storeImage = new UIImageView (UIImage.FromBundle ("Common/Store.png"));
			storeImage.Frame = new RectangleF (220 , 7, 60, 61);


			//var userName = CreateLebel ("Another User", 20.0f);
			var createNewAccount = new UILabel
			{
				Text = "Create new account",
				TextColor = CustomUIColor.FromHexString("#000000"),
				Font = UIFont.FromName("Apple SD Gothic Neo", 12.0f),
				LineBreakMode = UILineBreakMode.WordWrap,
				TextAlignment = UITextAlignment.Center,
				Lines = 3
			};
			createNewAccount.Frame = new RectangleF (90 , 15, 45, 45);

			var registerNewStore = new UILabel
			{
				Text = "Register your store",
				TextColor = CustomUIColor.FromHexString("#000000"),
				Font = UIFont.FromName("Apple SD Gothic Neo", 12.0f),
				LineBreakMode = UILineBreakMode.WordWrap,
				TextAlignment = UITextAlignment.Center,
				Lines = 3
			};
			registerNewStore.Frame = new RectangleF (163 , 15, 45, 45);



			var email = CreateLebel ("Didn't login on this device before", 12.0f);
			email.Frame = new RectangleF (95, 38, 180, 25);


			userView.AddSubview (backGroundImage);
			userView.AddSubview (logoImage);
			userView.AddSubview (userViewBoxLeft);
			userView.AddSubview (userViewBoxRight);
			userView.AddSubview (storeImage);
			userView.AddSubview (createNewAccount);
			userView.AddSubview (registerNewStore);

			createNewAccount.UserInteractionEnabled = true;

			createNewAccount.AddGestureRecognizer (new UITapGestureRecognizer (() => {
				var displayBound = UIScreen.MainScreen.Bounds;
				int height = (int)(displayBound.Height) - TopMargin;
				var signUpView = new SignupView(new RectangleF(displayBound.X,TopMargin, displayBound.Width, height), this.NavigationController);
				this.View.Add (signUpView);
			}));

			registerNewStore.UserInteractionEnabled = true;

			registerNewStore.AddGestureRecognizer (new UITapGestureRecognizer (() => {
				PageViewModel.NavigatedAppWebView();
			}));
				
			//userView.AddSubview (userName);
			//userView.AddSubview (email);


			return userView;
		}
	}
}

