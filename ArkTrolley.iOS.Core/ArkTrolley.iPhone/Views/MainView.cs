
using System;
using System.Drawing;

using MonoTouch.Foundation;
using MonoTouch.UIKit;
using ArkTrolley.iOS.Core.UserControls;
using ArkTrolley.iOS.Core.AppHelper;
using ArkTrolley.iOS.Core.ViewModels;

namespace ArkTrolley.iPhone.Views
{
	public partial class MainView : BaseViewController
	{
		private UIView AnotherUser;
		private UIView NewAccount;

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

		public override void StartView ()
		{
			scrollView = new UIScrollView {
				Frame = new RectangleF (0, TopMargin + 120, View.Frame.Width, 200),
				BackgroundColor = CustomUIColor.FromHexString ("#00ffffff"),
				AutoresizingMask = UIViewAutoresizing.FlexibleWidth,
				AlwaysBounceVertical = false,
				ShowsVerticalScrollIndicator = false
			};


			AddLogos ();

			AddNewUser ();

			var maxHeight = AddPreviousUser ();

			scrollView.ContentSize = new SizeF (0, maxHeight+ 80);

			View.AddSubview (scrollView);
		}

		private void AddNewUser()
		{
			var newUser = UserInfoHub (
				UIImage.FromBundle ("Common/Ark_lg.png"), 
				"Another user",
				"Didn't login on this device before."
			);	

			newUser.Frame = new RectangleF (10, TopMargin + 25, 300, 100);
			View.AddSubview (newUser);
		}

		private int AddPreviousUser()
		{
			int i = 0;
			int topMargin = 0;
			foreach (var item in PageViewModel.PreviousLoginUser) {

				var newUser1 = UserInfoHub  (
					UIImage.FromBundle ("Common/Ark_lg.png"), 
					item.str_name,
					item.str_email
				);	
				topMargin= 10 + 105*i;
				newUser1.Frame = new RectangleF (10, topMargin, 300, 100);
				scrollView.AddSubview (newUser1);
				i++;
			}


//			var newUser1 = UserInfoHub  (
//				UIImage.FromBundle ("Common/Ark_lg.png"), 
//				"Another user",
//				"Didn't login on this device before."
//			);	
//
//			newUser1.Frame = new RectangleF (10, TopMargin + 130, 300, 100);
//			View.AddSubview (newUser1);
//
//			var newUser2 = UserInfoHub  (
//				UIImage.FromBundle ("Common/Ark_lg.png"), 
//				"Another user",
//				"Didn't login on this device before."
//			);	
//
//			newUser2.Frame = new RectangleF (10, TopMargin + 235, 300, 100);
//			View.AddSubview (newUser2);
//
			var newUser3 = UserInfoHub2  ();	
				
			newUser3.Frame = new RectangleF (10, TopMargin + 340, 300, 100);
			View.AddSubview (newUser3);

			return topMargin;
		}

		private UIView UserInfoHub(UIImage userImage, string userName, string email)
		{
			var userView = new UIView ();

			var backGroundImage = new UIImageView (UIImage.FromBundle ("Common/Boton1.png"));
			backGroundImage.Frame = new RectangleF (0, 0, 300, 81);

			var userViewRectangle = new UIImageView (UIImage.FromBundle ("Common/Rect.png"));
			userViewRectangle.Frame = new RectangleF (86, 10, 200, 55);

			var userImageView = new UIImageView (userImage); 
				//new UIImageView (UIImage.FromBundle ("Common/Ark_lg.png"));
			userImageView.Frame = new RectangleF (20, 7, 60, 61);



			var userNameLabel = CreateLebel (userName, 20.0f);
			userNameLabel.Frame = new RectangleF (95, 15, 180, 25);
		

			var emailLabel = CreateLebel (email, 12.0f);
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



			var email = CreateLebel ("Didn't login on this device beforekajdhkjahsdkjahskjh", 12.0f);
			email.Frame = new RectangleF (95, 38, 180, 25);


			userView.AddSubview (backGroundImage);
			userView.AddSubview (logoImage);
			userView.AddSubview (userViewBoxLeft);
			userView.AddSubview (userViewBoxRight);
			userView.AddSubview (storeImage);
			userView.AddSubview (createNewAccount);
			userView.AddSubview (registerNewStore);


			//userView.AddSubview (userName);
			//userView.AddSubview (email);


			return userView;
		}
	}
}

