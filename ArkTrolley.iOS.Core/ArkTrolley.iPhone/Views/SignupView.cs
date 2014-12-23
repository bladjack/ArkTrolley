
using System;
using System.Drawing;

using MonoTouch.Foundation;
using MonoTouch.UIKit;
using ArkTrolley.iOS.Core.ViewModels;
using ArkTrolley.iOS.Core.AppHelper;
using Cirrious.MvvmCross.Binding.BindingContext;
using ArkTrolley.iPhone.UserControls;
using ArkTrolley.Core.AppHelper;

namespace ArkTrolley.iPhone.Views
{
	public class SignupView : UIView
	{
		public float ScreenWidth{ get; set;}
		public float ScreenHeight{ get; set;}
		public string PageTitle{ get; set; }
		public int TopMargin{ get; set; }
		public int BottomMargin{ get; set;}

		UITextField UserNameTextField{ get; set;}
		UITextField UserEmailTextField{ get; set;}
		UITextField UserAddressTextField{ get; set;}
		UITextField UserPasswordTextField{ get; set;}
		UITextField UserResetPasswordTextField{ get; set;}

		UIImagePickerController imagePicker;
		UIImageView ipad1xBackgroundImageView;
		byte[] ipad1xBackgroundImageBytes;


		SignupViewModel viewModel;
		UINavigationController NavigationController;

		private LoadingOverlay Loading;

		private bool showProgressLoading;
		public bool ShowProgressLoading 
		{
			set{ 
				showProgressLoading = value;
				if (showProgressLoading) {
					var bounds = UIScreen.MainScreen.Bounds;
					Loading = LoadingOverlay.Show (bounds);
					this.Add (Loading);
				} else {
					if (Loading != null) {
						Loading.Hide ();
					}
				}
			}
		}


		public SignupView (RectangleF frame,UINavigationController navigationController ) : base (frame)
		{
			viewModel = new SignupViewModel ();
			imagePicker = new UIImagePickerController ();
			imagePicker.SourceType = UIImagePickerControllerSourceType.PhotoLibrary;
			imagePicker.MediaTypes = UIImagePickerController.AvailableMediaTypes (UIImagePickerControllerSourceType.PhotoLibrary);

			imagePicker.FinishedPickingMedia += Handle_FinishedPickingMedia;
			imagePicker.Canceled += Handle_Canceled;

			this.NavigationController = navigationController;
			StartView ();
			viewModel.PropertyChanged += PageViewModelPropertyChanged;
		}

		private void PageViewModelPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e) {

			if (e.PropertyName == "IsProgessRingVisible") {
				ShowProgressLoading = viewModel.IsProgessRingVisible;
			} 
		}
		public  void StartView ()
		{

			ipad1xBackgroundImageView = new UIImageView (UIImage.FromBundle ("Common/my_profile_button_regular.png"));


			TopMargin = TopMargin + 50;
			BottomMargin = BottomMargin + 10;

			var displayBound = UIScreen.MainScreen.Bounds;

			int width = (int)(displayBound.Height) - TopMargin;


			var backgroundImage = new UIImageView (UIImage.FromBundle ("Login/Box.png"));
			backgroundImage.Frame = new RectangleF (0, 0, 294, width );

			var labelImage = new UIImageView (UIImage.FromBundle ("Common/user_profile_title_picture_regular.png"));
			labelImage.Frame = new RectangleF (50, TopMargin, 200, 20);

			var lineimage = new UIImageView (UIImage.FromBundle ("PickStore/LineBlue.png"));
			lineimage.Frame = new RectangleF (35, TopMargin + 30, this.Frame.Width - 70, 2);


			var userNameView = GetUserNameView ();
			userNameView.Frame = new RectangleF (33, TopMargin + 40, 250, 30);

			var userEmailView = GetUserEmail ();
			userEmailView.Frame = new RectangleF (33, TopMargin + 80, 250, 30);

			var userAddressView = GetUserAddressView ();
			userAddressView.Frame = new RectangleF (33, TopMargin + 120, 250, 30);


			var passwordView = GetPasswordView ();
			passwordView.Frame = new RectangleF (33, TopMargin + 160, 250, 30);

			var resetpasswordView = GetResetPasswordView ();
			resetpasswordView.Frame = new RectangleF (33, TopMargin + 200, 250, 30);


			var loginButton = UIButton.FromType (UIButtonType.Custom);
			loginButton.SetImage (UIImage.FromFile ("Common/save_user_button_picture_regular.png").ImageWithRenderingMode (UIImageRenderingMode.AlwaysOriginal), UIControlState.Normal);


			var cancelButton = UIButton.FromType (UIButtonType.Custom);
			cancelButton.SetImage (UIImage.FromFile ("Common/cancel_button_picture_regular.png").ImageWithRenderingMode (UIImageRenderingMode.AlwaysOriginal), UIControlState.Normal);

			var saveUserButton = UIButton.FromType (UIButtonType.Custom);
			saveUserButton.SetImage (UIImage.FromFile ("Common/browse_picture_button_picture_regular.png").ImageWithRenderingMode (UIImageRenderingMode.AlwaysOriginal), UIControlState.Normal);

			saveUserButton.UserInteractionEnabled = true;

			saveUserButton.AddGestureRecognizer (new UITapGestureRecognizer (() => {
				NavigationController.PresentModalViewController(imagePicker, true);
			}));

			UpdateUserInfo ();

			ipad1xBackgroundImageView.Frame = new RectangleF (200, TopMargin + 250, 80, 80);

			saveUserButton.Frame = new RectangleF (40, TopMargin + 280, 100, 40);

			loginButton.Frame = new RectangleF (40, TopMargin + 350, 100, 40);
			cancelButton.Frame = new RectangleF (160, TopMargin + 350, 100, 40);

			this.AddSubview (backgroundImage);
			this.AddSubview (saveUserButton);
			this.AddSubview (labelImage);
			this.AddSubview (lineimage);
			this.AddSubview (ipad1xBackgroundImageView);
			this.AddSubview (userEmailView);
			this.AddSubview (userNameView);
			this.AddSubview (passwordView);
			this.AddSubview (userAddressView);
			this.AddSubview (resetpasswordView);
			this.AddSubview (loginButton);
			this.AddSubview (cancelButton);
			loginButton.UserInteractionEnabled = true;

			loginButton.AddGestureRecognizer (new UITapGestureRecognizer (() => {
				viewModel.CurrentUserData.str_name= UserNameTextField.Text;
				viewModel.CurrentUserData.str_email= UserEmailTextField.Text;
				viewModel.CurrentUserData.str_address= UserAddressTextField.Text;
				viewModel.CurrentUserData.str_password= UserPasswordTextField.Text;
				viewModel.CurrentUserData.enm_type="regular";
				viewModel.CurrentUserData.gen_id=0;
				if(ipad1xBackgroundImageView.Image!=null)
				{
					ipad1xBackgroundImageBytes = ArkTrolley.iOS.Core.AppHelper.UtilityMethods.ToNSData (ipad1xBackgroundImageView.Image);
					viewModel.CurrentUserData.byt_picture = ipad1xBackgroundImageBytes;
				}
				viewModel.SignUpUser(UserPasswordTextField.Text, UserResetPasswordTextField.Text);

			}));

			cancelButton.UserInteractionEnabled = true;

			cancelButton.AddGestureRecognizer (new UITapGestureRecognizer (() => {
				this.Hide ();
			}));
		}

		private async void UpdateUserInfo()
		{
			if (SharedData.CurrentLoginedUser != null) {
				UserNameTextField.Text = SharedData.CurrentLoginedUser.str_name;
				UserEmailTextField.Text = SharedData.CurrentLoginedUser.str_email;
				UserAddressTextField.Text = SharedData.CurrentLoginedUser.str_address;
				var downloadimage = await BaseViewController.LoadImage (SharedData.CurrentConfigurationData.str_web_service_root + SharedData.CurrentConfigurationData.str_users_pictures_path + SharedData.CurrentLoginedUser.int_id + ".jpg");
				if (downloadimage != null) {
					ipad1xBackgroundImageView.Image = downloadimage;
				}
			}
		}

		protected void Handle_FinishedPickingMedia (object sender, UIImagePickerMediaPickedEventArgs e)
		{
			bool isImage = false;
			switch (e.Info [UIImagePickerController.MediaType].ToString ()) {
			case "public.image":
				isImage = true;
				break;
			}
			if (isImage) {
				UIImage originalImage = e.Info [UIImagePickerController.OriginalImage] as UIImage;
				if (originalImage != null) {
					ipad1xBackgroundImageView.Image = originalImage;
				}
			}
			imagePicker.DismissViewController (true, null);
		}

		void Handle_Canceled (object sender, EventArgs e) {
			imagePicker.DismissViewController(true, null);
		}

		private UIImageView GetUserImage()
		{
			var userImage = new UIImageView (UIImage.FromBundle ("Common/my_profile_button_regular.png"));

			return userImage;
		}

		private UIView GetUserNameView()
		{
			var userNameView = new UIView ();

			userNameView.Layer.CornerRadius = 8.0f;
			userNameView.ClipsToBounds = true;
			userNameView.BackgroundColor = UIColor.White;

			UserNameTextField = new UITextField ();
			UserNameTextField.Frame = new RectangleF (0, 0, 250, 32);
			UserNameTextField.Placeholder = "Enter Name";
			UserNameTextField.KeyboardType = UIKeyboardType.EmailAddress;
			UserNameTextField.ReturnKeyType = UIReturnKeyType.Send;
			UserNameTextField.MinimumFontSize = 14f;
			UserNameTextField.AdjustsFontSizeToFitWidth = true;
			UserNameTextField.BackgroundColor = CustomUIColor.FromHexString ("#C3E9FF");

			userNameView.AddSubview (UserNameTextField);
			UserNameTextField.ShouldReturn += (textField) => { 
				UserNameTextField.ResignFirstResponder (); 
				return true;
			};
			return userNameView;

		}

		private UIView GetUserAddressView()
		{
			var userNameView = new UIView ();

			userNameView.Layer.CornerRadius = 8.0f;
			userNameView.ClipsToBounds = true;
			userNameView.BackgroundColor = UIColor.White;


			UserAddressTextField = new UITextField ();
			UserAddressTextField.Frame = new RectangleF (0, 0, 250, 32);
			UserAddressTextField.Placeholder = "Enter Address";
			UserAddressTextField.KeyboardType = UIKeyboardType.EmailAddress;
			UserAddressTextField.ReturnKeyType = UIReturnKeyType.Send;
			UserAddressTextField.MinimumFontSize = 12f;
			UserAddressTextField.AdjustsFontSizeToFitWidth = true;
			UserAddressTextField.BackgroundColor = CustomUIColor.FromHexString ("#C3E9FF");

			userNameView.AddSubview (UserAddressTextField);
			userNameView.AddSubview (UserNameTextField);
			UserAddressTextField.ShouldReturn += (textField) => { 
				UserAddressTextField.ResignFirstResponder (); 
				return true;
			};
			return userNameView;

		}

		private UIView GetUserEmail()
		{
			var userEmailView = new UIView ();
		

			userEmailView.Layer.CornerRadius = 8.0f;
			userEmailView.ClipsToBounds = true;
			userEmailView.BackgroundColor = UIColor.White;

			var paddingView = new UIView ();
			paddingView.Frame = new RectangleF (35, 0, 5, 32);
			paddingView.BackgroundColor = CustomUIColor.FromHexString ("#C3E9FF");



			UserEmailTextField= new UITextField ();
			UserEmailTextField.Frame = new RectangleF (0, 0, 250, 32);
			UserEmailTextField.Placeholder = "Enter email";
			UserEmailTextField.KeyboardType = UIKeyboardType.EmailAddress;
			UserEmailTextField.ReturnKeyType = UIReturnKeyType.Send;
			UserEmailTextField.MinimumFontSize = 12f;
			UserEmailTextField.AdjustsFontSizeToFitWidth = true;
			UserEmailTextField.BackgroundColor = CustomUIColor.FromHexString ("#C3E9FF");
			UserEmailTextField.ShouldReturn += (textField) => { 
				UserEmailTextField.ResignFirstResponder (); 
				return true;
			};
			userEmailView.AddSubview (UserEmailTextField);
			return userEmailView;
		}

		private UIView GetPasswordView()
		{
			var passwordView = new UIView ();

			passwordView.Layer.CornerRadius = 8.0f;
			passwordView.ClipsToBounds = true;
			passwordView.BackgroundColor = UIColor.White;

			var paddingView2 = new UIView ();
			paddingView2.Frame = new RectangleF (35, 0, 5, 32);
			paddingView2.BackgroundColor = CustomUIColor.FromHexString ("#C3E9FF");


			UserPasswordTextField = new UITextField ();
			UserPasswordTextField.Frame = new RectangleF (0, 0, 250, 32);
			UserPasswordTextField.Placeholder = "Enter Password";
			UserPasswordTextField.AutocorrectionType = UITextAutocorrectionType.No;
			UserPasswordTextField.KeyboardType = UIKeyboardType.Default;
			UserPasswordTextField.ReturnKeyType = UIReturnKeyType.Done;
			UserPasswordTextField.ClearButtonMode = UITextFieldViewMode.WhileEditing;
			UserPasswordTextField.MinimumFontSize = 12f;
			UserPasswordTextField.AdjustsFontSizeToFitWidth = true;

			UserPasswordTextField.SecureTextEntry = true;
			UserPasswordTextField.BackgroundColor = CustomUIColor.FromHexString ("#C3E9FF");


			UserPasswordTextField.ShouldReturn += (textField) => { 
				UserPasswordTextField.ResignFirstResponder (); 
				return true;
			};

			passwordView.AddSubview (UserPasswordTextField);

			return passwordView;
		}

		private UIView GetResetPasswordView()
		{
			var passwordView = new UIView ();

			passwordView.Layer.CornerRadius = 8.0f;
			passwordView.ClipsToBounds = true;
			passwordView.BackgroundColor = UIColor.White;

			UserResetPasswordTextField = new UITextField ();
			UserResetPasswordTextField.Frame = new RectangleF (0, 0, 250, 32);
			UserResetPasswordTextField.Placeholder = "Enter Reset Password";
			UserResetPasswordTextField.AutocorrectionType = UITextAutocorrectionType.No;
			UserResetPasswordTextField.KeyboardType = UIKeyboardType.Default;
			UserResetPasswordTextField.ReturnKeyType = UIReturnKeyType.Done;
			UserResetPasswordTextField.ClearButtonMode = UITextFieldViewMode.WhileEditing;
			UserResetPasswordTextField.MinimumFontSize = 12f;
			UserResetPasswordTextField.AdjustsFontSizeToFitWidth = true;

			UserResetPasswordTextField.SecureTextEntry = true;
			UserResetPasswordTextField.BackgroundColor = CustomUIColor.FromHexString ("#C3E9FF");


			UserResetPasswordTextField.ShouldReturn += (textField) => { 
				UserResetPasswordTextField.ResignFirstResponder (); 
				return true;
			};

			passwordView.AddSubview (UserResetPasswordTextField);

			return passwordView;
		}


		/// <summary>
		/// Fades out the control and then removes it from the super view
		/// </summary>
		public void Hide ()
		{
			UIView.Animate (
				0.5, // duration
				() => { Alpha = 0; },
				() => { RemoveFromSuperview(); }
			);
		}
	}
}

