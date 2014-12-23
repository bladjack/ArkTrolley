using System;
using MonoTouch.UIKit;
using System.Drawing;
using ArkTrolley.iOS.Core.AppHelper;
using ArkTrolley.Core.ViewModels;
using System.Collections.Generic;
using ArkTrolley.WebService;
using ArkTrolley.Core.AppHelper;
using ArkTrolley.Core;
using System.Threading.Tasks;

namespace ArkTrolley.iPhone.UserControls
{
	public class CreateRecipesView: UIView {
		// control declarations
		ProfileViewModel pageViewModel;
		private static CreateRecipesView SharedInstance;
		private  string LoginUserName;
		private  string StoreName;
		private List<MyRecipesModel> myListData;
		UIScrollView scrollView;// new UIScrollView ();

		UIButton finishButton;
		UIButton cancelButton;

		UIView newRecipesView;
		UITextField NameTextField;
		UITextView DescriptionTextView;

		UIImagePickerController imagePicker;
		UIImageView RecipeImage;
		byte[] RecipeImageBytes;

		UINavigationController NavigationController;

		private int currentRecipeid=0;
		private double totalCost=0;
		UISegmentedControl segmentControl;
	
		private CreateRecipesView (RectangleF frame, List<MyRecipesModel> data, ProfileViewModel pageViewModel,UINavigationController navigationController) : base (frame)
		{
			// configurable bits

			imagePicker = new UIImagePickerController ();
			imagePicker.SourceType = UIImagePickerControllerSourceType.PhotoLibrary;
			imagePicker.MediaTypes = UIImagePickerController.AvailableMediaTypes (UIImagePickerControllerSourceType.PhotoLibrary);

			imagePicker.FinishedPickingMedia += Handle_FinishedPickingMedia;
			imagePicker.Canceled += Handle_Canceled;

			this.NavigationController = navigationController;


			this.pageViewModel = pageViewModel;
			myListData = data;
			scrollView =new UIScrollView ();

			AutoresizingMask = UIViewAutoresizing.FlexibleDimensions;
			RecipeImage= new UIImageView (UIImage.FromBundle ("Common/Ark_lg.png"));
			newRecipesView = new UIView ();
			ShowTheData ();

			//SegmentView(true);

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
					RecipeImage.Image = originalImage;
				}
			}
			imagePicker.DismissViewController (true, null);
		}

		void Handle_Canceled (object sender, EventArgs e) {
			imagePicker.DismissViewController(true, null);
		}


		private async void ShowTheData()
		{
			await AddBackGroundAndTitle ();
			GetNewRecipesView ();
			AddUserAndStore ();
		}

		private async Task AddBackGroundAndTitle()
		{
			var backgroundImage = new UIImageView (UIImage.FromBundle ("Login/Box.png"));
			backgroundImage.Frame = new RectangleF (0, 0, this.Frame.Width, this.Frame.Height);
			var image = await ArkTrolley.iOS.Core.AppHelper.UtilityMethods.LoadImage (ConfigurationSettings.GetMyRecipesTitleImagePath);
			UIImageView labelImage = new UIImageView ();
			if (image != null) {
				labelImage.Image = image;
			} else {
				labelImage.Image = UIImage.FromBundle ("CurrentTrolley/CurrentTrolleyTitle.png");
			}
			labelImage.Frame = new RectangleF (80, 20, 160, 20);

			var lineimage = new UIImageView (UIImage.FromBundle ("PickStore/LineBlue.png"));
			lineimage.Frame = new RectangleF (35, 45, this.Frame.Width - 70, 2);

			AddSubview (backgroundImage);
			AddSubview (labelImage);
			AddSubview (lineimage);

			AddListView ();
		}

		private void AddListView()
		{
			var topmargin = 100;
			var bottommargin = this.Frame.Height - 70;

			AddButtons (bottommargin);
			AddStoreList (topmargin, bottommargin);
		}

		private void AddUserAndStore()
		{
			var userLabel = new UILabel {
				Text = "You can create new recipes or update exisitng ones selecting items from your tickets",
				TextColor = CustomUIColor.FromHexString ("#000000"),
				Font = UIFont.FromName ("Apple SD Gothic Neo", 14.0f),
				AdjustsFontSizeToFitWidth = false,
				LineBreakMode = UILineBreakMode.TailTruncation,
				Lines = 4,
				TextAlignment= UITextAlignment.Center
			};


			userLabel.Frame = new RectangleF (30, 50, this.Frame.Width-40, 60);

			Add (userLabel);
		}

		private void AddButtons(float topMargin)
		{
			var buttonTopMargin = topMargin;

			finishButton = UIButton.FromType (UIButtonType.Custom);
			finishButton.SetImage (UIImage.FromFile ("CurrentTrolley/save_recipe_button_picture_regular.png").ImageWithRenderingMode (UIImageRenderingMode.AlwaysOriginal), UIControlState.Normal);


			cancelButton = UIButton.FromType (UIButtonType.Custom);
			cancelButton.SetImage (UIImage.FromFile ("CurrentTrolley/CancelBut.png").ImageWithRenderingMode (UIImageRenderingMode.AlwaysOriginal), UIControlState.Normal);


			//var loginButton = new UIImageView (UIImage.FromBundle ("Login/button.png"));
			//var cancelButton = new UIImageView (UIImage.FromBundle ("Login/button.png"));


			finishButton.Frame = new RectangleF (65, buttonTopMargin, 90, 30);
			cancelButton.Frame = new RectangleF (180, buttonTopMargin, 90, 30);

			finishButton.UserInteractionEnabled = true;

			finishButton.AddGestureRecognizer (new UITapGestureRecognizer (() => {
				if (RecipeImage.Image != null) {
					RecipeImageBytes = ArkTrolley.iOS.Core.AppHelper.UtilityMethods.ToNSData (RecipeImage.Image);
				}

				pageViewModel.CreateNewRecipe (currentRecipeid, NameTextField.Text, DescriptionTextView.Text, RecipeImageBytes);
			}));

			cancelButton.UserInteractionEnabled = true;

			cancelButton.AddGestureRecognizer (new UITapGestureRecognizer (() => {
				this.Hide ();
			}));

			//	AddSubview (finishButton);
			AddSubview (cancelButton);
		}

		private UISegmentedControl AddSegmentControl()
		{
			segmentControl = new UISegmentedControl ();

			segmentControl.Frame = new RectangleF (40, 5, 230, 30);
			segmentControl.InsertSegment ("Existing Recipes", 0, false);
			segmentControl.InsertSegment ("New Recipe", 1, false);
			segmentControl.TintColor = UIColor.White;
			segmentControl.BackgroundColor = CustomUIColor.FromHexString ("#6a6a6a");
			segmentControl.Layer.CornerRadius = 10.0f;
			segmentControl.Layer.BorderWidth = 1;
			segmentControl.Layer.BorderColor = CustomUIColor.FromHexString ("#000000").CGColor;



			segmentControl.SelectedSegment = 0; 

			segmentControl.ControlStyle = UISegmentedControlStyle.Bordered;
			segmentControl.UserInteractionEnabled = true;
			segmentControl.ValueChanged += (sender, e) => {
				var selectedSegmentId = (sender as UISegmentedControl).SelectedSegment;

				if (selectedSegmentId == 0) {
					SegmentView(false);
				} else {
					currentRecipeid=0;
					SegmentView(true);
				}
				segmentControl.SelectedSegment = selectedSegmentId; 
			};

			return segmentControl;
		}


		private void GetNewRecipesView()
		{
			var userNameView = GetUserNameView ();
			userNameView.Frame = new RectangleF (35, 0, 250, 30);

			var descriptionLabel = new UILabel {
				Text = "Description",
				TextColor = CustomUIColor.FromHexString ("#000000"),
				Font = UIFont.FromName ("Apple SD Gothic Neo", 16.0f),
				AdjustsFontSizeToFitWidth = false,
				LineBreakMode = UILineBreakMode.TailTruncation,
				Lines = 3,
				TextAlignment = UITextAlignment.Left
			};

			DescriptionTextView = new UITextView {
				TextColor = CustomUIColor.FromHexString ("#000000"),
				Font = UIFont.FromName ("Apple SD Gothic Neo", 16.0f),
				TextAlignment = UITextAlignment.Left,
				BackgroundColor = UIColor.White
			};

			var browserButton = UIButton.FromType (UIButtonType.Custom);
			browserButton.SetImage (UIImage.FromFile ("Common/browse_picture_button_picture_regular.png").ImageWithRenderingMode (UIImageRenderingMode.AlwaysOriginal), UIControlState.Normal);

			browserButton.UserInteractionEnabled = true;

			browserButton.AddGestureRecognizer (new UITapGestureRecognizer (() => {
				NavigationController.PresentModalViewController(imagePicker, true);
			}));



			descriptionLabel.Frame = new RectangleF (35, 40, this.Frame.Width / 2, 30);
			browserButton.Frame = new RectangleF (this.Bounds.Width - 120, 40, 100, 30);
			DescriptionTextView.Frame = new RectangleF (35, 70, this.Frame.Width / 2, 120);
			RecipeImage.Frame = new RectangleF (this.Bounds.Width - 120, 70, 80, 80);

			newRecipesView.AddSubviews (userNameView, descriptionLabel, DescriptionTextView, browserButton, RecipeImage);

		}

		private UIView GetUserNameView()
		{
			var userNameView = new UIView ();

			userNameView.Layer.CornerRadius = 8.0f;
			userNameView.ClipsToBounds = true;
			userNameView.BackgroundColor = UIColor.White;

			NameTextField = new UITextField ();
			NameTextField.Frame = new RectangleF (0, 0, 250, 32);
			NameTextField.Placeholder = "Enter Name";
			NameTextField.KeyboardType = UIKeyboardType.EmailAddress;
			NameTextField.ReturnKeyType = UIReturnKeyType.Send;
			NameTextField.MinimumFontSize = 14f;
			NameTextField.AdjustsFontSizeToFitWidth = true;
			NameTextField.BackgroundColor = CustomUIColor.FromHexString ("#C3E9FF");

			NameTextField.ShouldReturn += (textField) => { 
				NameTextField.ResignFirstResponder (); 
				return true;
			};


			userNameView.AddSubview (NameTextField);

			return userNameView;

		}

		private void SegmentView(bool newRecipe)
		{
			if (newRecipe) {
				this.Add (newRecipesView);
				this.Add (finishButton);
				scrollView.RemoveFromSuperview ();

//				newRecipesView.Hidden = false;
//				scrollView.Hidden = true;
//				finishButton.Hidden = false;
//				cancelButton.Hidden = false;
			} else {

				newRecipesView.RemoveFromSuperview ();
				this.Add (scrollView);
				finishButton.RemoveFromSuperview ();
				this.Add (cancelButton);

//				newRecipesView.Hidden = true;
//				scrollView.Hidden = false;
//				finishButton.Hidden = true;
//				cancelButton.Hidden = false;
			}
		}


		private async void AddStoreList(float topmargin, float bottomargin)
		{
			var segmant = AddSegmentControl ();
			segmant.Frame = new RectangleF (30, topmargin , 230, 30);

			newRecipesView.Frame= new RectangleF (0, topmargin+ 40, this.Frame.Width, bottomargin - topmargin-60);

			scrollView.Frame = new RectangleF (0, topmargin+ 40, this.Frame.Width, bottomargin - topmargin-60);
			scrollView.BackgroundColor = CustomUIColor.FromHexString ("#00ffffff");
			scrollView.AutoresizingMask = UIViewAutoresizing.FlexibleWidth;
			scrollView.AlwaysBounceVertical = false;
			scrollView.ShowsVerticalScrollIndicator = false;
			int i = 0;
			topmargin = 0;
			foreach (var item in myListData) {
					var newUser1 = await StoreHub (
					UIImage.FromBundle ("Common/Ark_lg.png"), 
					item
				);	
				topmargin = 10*i + 70 * i;
				newUser1.Frame = new RectangleF (35, topmargin, this.Frame.Width - 70, 70);
				scrollView.AddSubview (newUser1);

				newUser1.UserInteractionEnabled = true;

				newUser1.AddGestureRecognizer (new UITapGestureRecognizer (() => {
					currentRecipeid= Convert.ToInt32( item.int_id);
					NameTextField.Text= item.str_recipe_name;
					DescriptionTextView.Text= item.str_recipe_description;
					foreach (var itemview in newUser1.Subviews) {
						if(itemview is UIImageView)
						{
							RecipeImage= (UIImageView)itemview;
						}
					}

					segmentControl.SelectedSegment=1;
					SegmentView(true);

				}));
				i++;
			}

			topmargin = 90 * (i);
			scrollView.ContentSize = new SizeF (0, topmargin);
			this.AddSubviews (scrollView,segmant);
		}

		private async Task<UIView> StoreHub(UIImage userImage, MyRecipesModel item)
		{
			var userView = new UIView ();

			var backGroundImage = new UIImageView (UIImage.FromBundle ("CurrentTrolley/RectBox.png"));
			backGroundImage.Frame = new RectangleF (0, 0, this.Frame.Width- 70, 70);

			//var userViewRectangle = new UIImageView (UIImage.FromBundle ("Common/Rect.png"));
			//userViewRectangle.Frame = new RectangleF (86, 10, 200, 55);
			var downloadimage= await ArkTrolley.iOS.Core.AppHelper.UtilityMethods.LoadImage(SharedData.CurrentConfigurationData.str_web_service_root+ SharedData.CurrentConfigurationData.str_items_pictures_path+ item.int_id+".jpg");
			var userImageView=new UIImageView();
			if (downloadimage == null) {
				userImageView.Image = userImage;
			} else {
				userImageView.Image = downloadimage;
			}
			//new UIImageView (UIImage.FromBundle ("Common/Ark_lg.png"));
			userImageView.Frame = new RectangleF (4, 7, 45, 56);


			var descriptionLabel = new UILabel {
				Text = Convert.ToDateTime(item.dte_register).ToString("yyyy-dd-mm")+"\n"+ "$ " +item.str_recipe_name ,
				TextColor = CustomUIColor.FromHexString ("#000000"),
				Font = UIFont.FromName ("Apple SD Gothic Neo", 14.0f),
				AdjustsFontSizeToFitWidth = false,
				LineBreakMode = UILineBreakMode.TailTruncation,
				Lines = 3,
				TextAlignment= UITextAlignment.Center
			};
			descriptionLabel.Frame = new RectangleF (65, 5, 100, 70);

		


//			var crossImageView = new UIImageView (UIImage.FromBundle("CurrentTrolley/RedX")); 
//			//new UIImageView (UIImage.FromBundle ("Common/Ark_lg.png"));
//			crossImageView.Frame = new RectangleF (this.Frame.Width- 110 , 20, 35, 30);
//
//			crossImageView.UserInteractionEnabled = true;
//
//			crossImageView.AddGestureRecognizer (new UITapGestureRecognizer (() => {
//				DeleteItemFromList(item);
//			}));

			userView.AddSubview (backGroundImage);
			userView.AddSubview (userImageView);
			userView.AddSubview (descriptionLabel);
		//	userView.AddSubview (crossImageView);

			return userView;
		}

		private async void DeleteItemFromList(MyRecipesModel item)
		{
			var button = await pageViewModel.AppMessageDialog.ShowAppDialog ("Are you sure you want to delete list", "Confirm", "OK", "Cancel");
			if (button == ArkTrolley.Core.AppHelper.AppEnums.DialogButton.Button1) {
				var response = await pageViewModel.SetRemoveList (item.int_id, ListType.myrecipes);
				if (response.responseCode == 1) {
					myListData.Remove (item);
					FlushScrollViewerData ();
					AddListView ();
				}
				await pageViewModel.AppMessageDialog.ShowAppDialog (response.responseMessage, "Alert");
			}
		}

		private void FlushScrollViewerData()
		{
			foreach (var sub in scrollView.Subviews) {
				sub.RemoveFromSuperview ();
			}
		}



		public static CreateRecipesView Show(RectangleF bounds, List<MyRecipesModel> data, ProfileViewModel viewModel,UINavigationController navigation)
		{
			SharedInstance = new CreateRecipesView (bounds,data, viewModel, navigation);
			return SharedInstance;
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
	};
}

