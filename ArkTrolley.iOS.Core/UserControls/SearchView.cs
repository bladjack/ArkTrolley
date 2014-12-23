using System;
using MonoTouch.UIKit;
using System.Drawing;
using ArkTrolley.iOS.Core.AppHelper;
using ArkTrolley.Core.ViewModels;
using System.Collections.Generic;
using ArkTrolley.WebService;
using ArkTrolley.Core.AppHelper;
using ArkTrolley.Core.AppHelper.AppEnums;
using ArkTrolley.Core;
using System.Threading.Tasks;

namespace ArkTrolley.iPhone.UserControls
{
	public class SearchView: UIView {
		// control declarations

		private static SearchView SharedInstance;
		UITextView commentsEditText;

		UIScrollView scrollView;// new UIScrollView ();
		ProfileViewModel pageViewModel;
		private SearchView (RectangleF frame, ProfileViewModel pageViewModel) : base (frame)
		{
			// configurable bits

			this.pageViewModel = pageViewModel;
			scrollView =new UIScrollView ();


			this.Layer.CornerRadius = 5.0f;

//			BackgroundColor = UIColor.Gray;
//			Alpha = 0.75f;
			AutoresizingMask = UIViewAutoresizing.FlexibleDimensions;

			UpdateView ();
		
		}

		private async void UpdateView()
		{
			await AddBackGroundAndTitle ();
			AddUserAndStore ();
		}

		private async Task AddBackGroundAndTitle()
		{
			var backgroundImage = new UIImageView (UIImage.FromBundle ("Login/Box.png"));
			backgroundImage.Frame = this.Frame;
			backgroundImage.Frame = new RectangleF (0, 0, this.Frame.Width, this.Frame.Height);

			var labelImage = new UIImageView ();
			var image = await ArkTrolley.iOS.Core.AppHelper.UtilityMethods.LoadImage (ConfigurationSettings.GetSearchItemTitleImagePath);
			if (image != null) {
				labelImage.Image = image;
			} else {
				labelImage.Image = UIImage.FromBundle ("Common/search_item_title_picture_regular.png");
			}
			labelImage.Frame = new RectangleF (80, 20, 160, 20);

			var lineimage = new UIImageView (UIImage.FromBundle ("PickStore/LineBlue.png"));
			lineimage.Frame = new RectangleF (35, 45, this.Frame.Width - 70, 2);

			AddSubview (backgroundImage);
			AddSubview (labelImage);
			AddSubview (lineimage);

			var bottommargin = this.Frame.Height;

			AddListView ();
		}

		private void AddListView()
		{
			var topmargin = 85;
			var bottommargin = this.Frame.Height;


			AddStoreList (topmargin, bottommargin);
			AddButtons (bottommargin);
		}

		private void AddUserAndStore()
		{
			var userLabel = new UILabel {
				Text = "Please enter the keyword to search for into our database. You can also enter a barcode value here. Be aware this can take many seconds so please be patience.",
				TextColor = CustomUIColor.FromHexString ("#000000"),
				Font = UIFont.FromName ("Apple SD Gothic Neo", 15.0f),
				AdjustsFontSizeToFitWidth = false,
				LineBreakMode = UILineBreakMode.TailTruncation,
				Lines = 4,
				TextAlignment = UITextAlignment.Center
			};



			userLabel.Frame = new RectangleF (20, 50, this.Frame.Width-40, 100);
			Add (userLabel);
		}

		private async void AddButtons(float topMargin)
		{
			var buttonTopMargin = topMargin-60;

			var finishButton = UIButton.FromType (UIButtonType.Custom);
			finishButton.SetImage (UIImage.FromFile ("Common/search_button_picture_regular.png").ImageWithRenderingMode (UIImageRenderingMode.AlwaysOriginal), UIControlState.Normal);
		
			var cancelButton = UIButton.FromType (UIButtonType.Custom);
			cancelButton.SetImage (UIImage.FromFile ("Common/cancel_button_picture_regular.png").ImageWithRenderingMode (UIImageRenderingMode.AlwaysOriginal), UIControlState.Normal);


			finishButton.Frame = new RectangleF (50, buttonTopMargin, 90, 30);
			cancelButton.Frame = new RectangleF (160, buttonTopMargin, 90, 30);

			finishButton.UserInteractionEnabled = true;

			finishButton.AddGestureRecognizer (new UITapGestureRecognizer (() => {
				ShowSearchItemsData();
				//SubmitCurrentTrolleyStatus(CurrentTrolleyStatusType.mytickets);
			}));

			cancelButton.UserInteractionEnabled = true;

			cancelButton.AddGestureRecognizer (new UITapGestureRecognizer (() => {
				Hide();
			}));

			AddSubview (finishButton);
			AddSubview (cancelButton);
		}

		private async void ShowSearchItemsData()
		{
			var view = await pageViewModel.GetSearchItemsData (commentsEditText.Text);
			this.Hide ();
		}

		private async void SubmitCurrentTrolleyStatus(CurrentTrolleyStatusType type)
		{
			var response = await pageViewModel.SetCurrentTrolleyStatus (type, commentsEditText.Text);
			await pageViewModel.AppMessageDialog.ShowAppDialog (response.responseMessage, "Alert");
			this.Hide ();
		}

		private void AddStoreList(float topmargin, float bottomargin)
		{

			scrollView.Frame = new RectangleF (0, topmargin, this.Frame.Width, bottomargin - topmargin - 20);

			scrollView.BackgroundColor = CustomUIColor.FromHexString ("#00ffffff");
			scrollView.AutoresizingMask = UIViewAutoresizing.FlexibleWidth;
			scrollView.AlwaysBounceVertical = false;
			scrollView.ShowsVerticalScrollIndicator = false;


			commentsEditText = new UITextView {
				Text = "Optional",
				TextColor = CustomUIColor.FromHexString ("#000000"),
				Font = UIFont.FromName ("Apple SD Gothic Neo", 16.0f),
				TextAlignment = UITextAlignment.Left,
				BackgroundColor = UIColor.White
			};

			commentsEditText.Frame = new RectangleF (35, 70, this.Frame.Width - 70, 140);
			scrollView.AddSubview (commentsEditText);
			scrollView.ContentSize = new SizeF (0, bottomargin - topmargin - 20);
			this.AddSubview (scrollView);
		}

		public static SearchView Show(RectangleF bounds, ProfileViewModel pageViewModel)
		{
			SharedInstance = new SearchView (bounds,pageViewModel);
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

