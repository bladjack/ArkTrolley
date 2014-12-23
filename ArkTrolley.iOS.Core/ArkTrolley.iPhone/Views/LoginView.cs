
using System;
using System.Drawing;

using MonoTouch.Foundation;
using MonoTouch.UIKit;
using ArkTrolley.iOS.Core.AppHelper;
using ArkTrolley.iOS.Core.UserControls;
using Cirrious.MvvmCross.Binding.BindingContext;
using ArkTrolley.iOS.Core.ViewModels;
using ArkTrolley.Core.AppHelper;

namespace ArkTrolley.iPhone.Views
{
	public partial class LoginView : BaseViewController
	{
		protected LoginViewModel PageViewModel {
			get {
				return (LoginViewModel)base.ViewModel;
			}
			set { 
				base.ViewModel = value;
			}
		}
		public LoginView () : base ("LoginView",  "ArkTrolley",null)
		{

		}
		private void PageViewModelPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e) {

			if (e.PropertyName == "IsProgessRingVisible") {
				ShowProgressLoading = PageViewModel.IsProgessRingVisible;
			}
		}
		public async override void StartView ()
		{
			AddLogos ();
			PageViewModel.PropertyChanged += PageViewModelPropertyChanged;


			if (UIScreen.MainScreen.Bounds.Height > 500) {
				TopMargin = TopMargin + 20;
				BottomMargin = BottomMargin + 20;
			} else {
				TopMargin = TopMargin + 10;
				BottomMargin = BottomMargin + 10;
			}


			var displayBound = UIScreen.MainScreen.Bounds;

			int width = (int)(displayBound.Height) - TopMargin - BottomMargin;


			var backgroundImage = new UIImageView (UIImage.FromBundle ("Login/Box.png"));
			backgroundImage.Frame = new RectangleF (8, TopMargin, 294, width);
//
//			var downloadimage= await BaseViewController.LoadImage(SharedData.CurrentConfigurationData.str_web_service_root+ SharedData.CurrentConfigurationData.str_items_pictures_path+ item.int_id+".jpg");
//			var userImageView=new UIImageView();
//			if (downloadimage == null) {
//				userImageView.Image = userImage;
//			} else {
//				userImageView.Image = downloadimage;
//			}
			var userImageView = new UIImageView (UIImage.FromBundle ("Common/Ark_lg.png"));
			if (PageViewModel.UserId != null) {
				var downloadimage = await BaseViewController.LoadImage (SharedData.CurrentConfigurationData.str_web_service_root + SharedData.CurrentConfigurationData.str_users_pictures_path + PageViewModel.UserId + ".jpg");
				if (downloadimage != null) {
					userImageView.Image = downloadimage;
				}
			}


			if (UIScreen.MainScreen.Bounds.Height > 500) {
				userImageView.Frame = new RectangleF ((displayBound.Width / 2) - (90 / 2 - 8), TopMargin + 20, 90, 120);
			} else {
				userImageView.Frame = new RectangleF ((displayBound.Width / 2) - (70 / 2), TopMargin + 20, 70, 90);
			}
			var userNameLabel = CreateLebel ("Add New user", 20.0f);
			if (UIScreen.MainScreen.Bounds.Height > 500) {
				userNameLabel.Frame = new RectangleF (33, TopMargin + 160, 250, 32);
			} else {
				userNameLabel.Frame = new RectangleF (33, TopMargin + 120, 250, 32);
			}
			userNameLabel.TextAlignment = UITextAlignment.Center;
			userNameLabel.Layer.CornerRadius = 8.0f;
			userNameLabel.ClipsToBounds = true;
			userNameLabel.BackgroundColor = CustomUIColor.FromHexString ("#C3E9FF");


			var userNameView = new UIView ();
			if (UIScreen.MainScreen.Bounds.Height > 500) {
				userNameView.Frame = new RectangleF (33, TopMargin + 210, 250, 32);
			} else {
				userNameView.Frame = new RectangleF (33, TopMargin + 162, 250, 32);
			}

			userNameView.Layer.CornerRadius = 8.0f;
			userNameView.ClipsToBounds = true;
			userNameView.BackgroundColor = UIColor.White;

			var Arroba = GetConfizTile ("Login/Arroba.png");
			Arroba.Frame = new RectangleF (0, 0, 32, 32);
			Arroba.Image.Frame = new RectangleF (7, 7, 18, 18);
			Arroba.BackgroundColor = CustomUIColor.FromHexString ("#C3E9FF");

			var paddingView = new UIView ();
			paddingView.Frame = new RectangleF (35, 0, 5, 32);
			paddingView.BackgroundColor = CustomUIColor.FromHexString ("#C3E9FF");



			var userEmailField = new UITextField ();
			userEmailField.Frame = new RectangleF (40, 0, 216, 32);
			userEmailField.Placeholder = "Enter email";
			userEmailField.KeyboardType = UIKeyboardType.EmailAddress;
			userEmailField.ReturnKeyType = UIReturnKeyType.Send;
			userEmailField.MinimumFontSize = 14f;
			userEmailField.AdjustsFontSizeToFitWidth = true;
			userEmailField.BackgroundColor = CustomUIColor.FromHexString ("#C3E9FF");

			userNameView.AddSubview (Arroba);
			userNameView.AddSubview (paddingView);
			userNameView.AddSubview (userEmailField);

			userEmailField.ShouldReturn += (textField) => { 
				userEmailField.ResignFirstResponder (); 
				return true;
			};

			var passwordView = new UIView ();
			if (UIScreen.MainScreen.Bounds.Height > 500) {
				passwordView.Frame = new RectangleF (33, TopMargin + 260, 250, 32);
			} else {
				passwordView.Frame = new RectangleF (33, TopMargin + 204, 250, 32);
			}
			passwordView.Layer.CornerRadius = 8.0f;
			passwordView.ClipsToBounds = true;
			passwordView.BackgroundColor = UIColor.White;

			var Prestas = GetConfizTile ("Login/Prestas.png");
			Prestas.Frame = new RectangleF (0, 0, 32, 32);
			Prestas.Image.Frame = new RectangleF (7, 7, 18, 18);
			Prestas.BackgroundColor = CustomUIColor.FromHexString ("#C3E9FF");

			var paddingView1 = new UIView ();
			paddingView1.Frame = new RectangleF (35, 0, 5, 32);
			paddingView1.BackgroundColor = CustomUIColor.FromHexString ("#C3E9FF");


			var userPasswordField = new UITextField ();
			userPasswordField.Frame = new RectangleF (40, 0, 216, 32);
			userPasswordField.Placeholder = "Enter Password";
			userPasswordField.AutocorrectionType = UITextAutocorrectionType.No;
			userPasswordField.KeyboardType = UIKeyboardType.Default;
			userPasswordField.ReturnKeyType = UIReturnKeyType.Done;
			userPasswordField.ClearButtonMode = UITextFieldViewMode.WhileEditing;
			userPasswordField.MinimumFontSize = 14f;
			userPasswordField.AdjustsFontSizeToFitWidth = true;

			userPasswordField.SecureTextEntry = true;
			userPasswordField.BackgroundColor = CustomUIColor.FromHexString ("#C3E9FF");

			passwordView.AddSubview (Prestas);
			passwordView.AddSubview (paddingView1);
			passwordView.AddSubview (userPasswordField);

			userPasswordField.ShouldReturn += (textField) => { 
				userPasswordField.ResignFirstResponder (); 
				return true;
			};


			//	var checkBox= new CheckBox(new RectangleF (120 , TopMargin + 310 , 18  , 18 ));

			var checkBoxTopMargin = TopMargin;

			if (UIScreen.MainScreen.Bounds.Height > 500) {
				checkBoxTopMargin = checkBoxTopMargin + 310;
			} else {
				checkBoxTopMargin = checkBoxTopMargin + 246;
			}

			var checkBoxSelectedButton = new UIImageView (UIImage.FromBundle ("Login/selectedCheckBox.png"));
			checkBoxSelectedButton.Frame = new RectangleF (110, checkBoxTopMargin, 18, 18);
			checkBoxSelectedButton.UserInteractionEnabled = true;

			var checkBoxUnSelectedButton = new UIImageView (UIImage.FromBundle ("Login/unselectedbox.png"));
			checkBoxUnSelectedButton.Frame = new RectangleF (110, checkBoxTopMargin, 18, 18);
			checkBoxUnSelectedButton.UserInteractionEnabled = true;

			var checkBoxText = CreateLebel ("Remember me", 14.0f);
			checkBoxText.Frame = new RectangleF (132, checkBoxTopMargin - 2, 120, 25);
			checkBoxText.UserInteractionEnabled = true;

			checkBoxText.AddGestureRecognizer (new UITapGestureRecognizer (() => {
				if (checkBoxSelectedButton.Hidden == true) {
					PageViewModel.RememberMe= true;
					checkBoxSelectedButton.Hidden = false;
					checkBoxUnSelectedButton.Hidden = true;
				} else {
					PageViewModel.RememberMe= false;
					checkBoxSelectedButton.Hidden = true;
					checkBoxUnSelectedButton.Hidden = false;
				}
			}));


			checkBoxSelectedButton.AddGestureRecognizer (new UITapGestureRecognizer (() => {
				PageViewModel.RememberMe= true;
				checkBoxSelectedButton.Hidden = true;
				checkBoxUnSelectedButton.Hidden = false;
			}));

			checkBoxUnSelectedButton.AddGestureRecognizer (new UITapGestureRecognizer (() => {
				PageViewModel.RememberMe= false;
				checkBoxSelectedButton.Hidden = false;
				checkBoxUnSelectedButton.Hidden = true;
			}));

			var buttonTopMargin = TopMargin;

			if (UIScreen.MainScreen.Bounds.Height > 500) {
				buttonTopMargin = buttonTopMargin + 345;
			} else {
				buttonTopMargin = buttonTopMargin + 280;
			}


			var loginButton = UIButton.FromType (UIButtonType.Custom);
			loginButton.SetImage (UIImage.FromFile ("Login/LoginBut.png").ImageWithRenderingMode (UIImageRenderingMode.AlwaysOriginal), UIControlState.Normal);
			loginButton.SetImage (UIImage.FromFile ("Login/LoginButOv.png").ImageWithRenderingMode (UIImageRenderingMode.AlwaysOriginal), UIControlState.Highlighted);
			loginButton.SetImage (UIImage.FromFile ("Login/LoginButOv.png").ImageWithRenderingMode (UIImageRenderingMode.AlwaysOriginal), UIControlState.Disabled);


			var cancelButton = UIButton.FromType (UIButtonType.Custom);
			cancelButton.SetImage (UIImage.FromFile ("Login/CancelBut.png").ImageWithRenderingMode (UIImageRenderingMode.AlwaysOriginal), UIControlState.Normal);
			cancelButton.SetImage (UIImage.FromFile ("Login/CancelButOv.png").ImageWithRenderingMode (UIImageRenderingMode.AlwaysOriginal), UIControlState.Highlighted);
			cancelButton.SetImage (UIImage.FromFile ("Login/CancelButOv.png").ImageWithRenderingMode (UIImageRenderingMode.AlwaysOriginal), UIControlState.Disabled);


			//var loginButton = new UIImageView (UIImage.FromBundle ("Login/button.png"));
			//var cancelButton = new UIImageView (UIImage.FromBundle ("Login/button.png"));


			loginButton.Frame = new RectangleF (65, buttonTopMargin, 80, 26);
			cancelButton.Frame = new RectangleF (180, buttonTopMargin, 80, 26);


			var forgotPasswordText = CreateLebel ("I can't access my account ", 14.0f);
			forgotPasswordText.TextColor = UIColor.Blue;

			forgotPasswordText.Frame = new RectangleF (90, buttonTopMargin+ 35, 220, 25);
			forgotPasswordText.UserInteractionEnabled = true;

			forgotPasswordText.AddGestureRecognizer (new UITapGestureRecognizer (() => {
				PageViewModel.ResetPassword();
			}));


			View.AddSubview (backgroundImage);
			View.AddSubview (userImageView);
			View.AddSubview (userNameLabel);
			View.AddSubview (userNameView);
			View.AddSubview (passwordView);
			View.AddSubview (checkBoxSelectedButton);
			View.AddSubview (checkBoxUnSelectedButton);
			View.AddSubview (checkBoxText);
			View.AddSubview (loginButton);
			View.AddSubview (cancelButton);
			View.AddSubview (forgotPasswordText);

			loginButton.UserInteractionEnabled=true;

			loginButton.AddGestureRecognizer (new UITapGestureRecognizer (() => {
			
				PageViewModel.NavigatedToPickerView();
			}));

			cancelButton.UserInteractionEnabled=true;

			cancelButton.AddGestureRecognizer (new UITapGestureRecognizer (() => {
				PageViewModel.GoBack();
			}));

			var set = this.CreateBindingSet<LoginView, LoginViewModel> ();
			set.Bind (userEmailField).To (vm => vm.UserEmail);
			set.Bind (userPasswordField).To (vm => vm.UserPassword);
			set.Bind (userNameLabel).To (vm => vm.UserName);

			set.Apply ();

//			userEmailField.Text = PageViewModel.Currentuser.str_email;
//		//	userPasswordField.Text = PageViewModel.Currentuser.str_password;
//
//
//
//			//View.AddSubview (checkBox);
		}
	}
}

