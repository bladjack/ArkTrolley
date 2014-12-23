using System;
using MonoTouch.UIKit;
using System.Drawing;
using ArkTrolley.iOS.Core.AppHelper;
using ArkTrolley.Core.ViewModels;
using System.Collections.Generic;
using ArkTrolley.WebService;
using ArkTrolley.Core.AppHelper;
using ArkTrolley.Core.AppHelper.AppEnums;

namespace ArkTrolley.iPhone.UserControls
{
	public class FinishedCurrentTrolleyView: UIView {
		// control declarations

		private static FinishedCurrentTrolleyView SharedInstance;
		private  string LoginUserName;
		private  string StoreName;
		UITextView commentsEditText;

		UIScrollView scrollView;// new UIScrollView ();
		ProfileViewModel pageViewModel;
		private FinishedCurrentTrolleyView (RectangleF frame, string user, string store,ProfileViewModel pageViewModel) : base (frame)
		{
			// configurable bits

			this.pageViewModel = pageViewModel;
			LoginUserName = user;
			StoreName = store;
			scrollView =new UIScrollView ();


			this.Layer.CornerRadius = 5.0f;

//			BackgroundColor = UIColor.Gray;
//			Alpha = 0.75f;
			AutoresizingMask = UIViewAutoresizing.FlexibleDimensions;
		
			AddBackGroundAndTitle ();

			AddUserAndStore ();
		}

		private void AddBackGroundAndTitle()
		{
			var backgroundImage = new UIImageView (UIImage.FromBundle ("Login/Box.png"));
			backgroundImage.Frame = this.Frame;
			backgroundImage.Frame = new RectangleF (0, 0, this.Frame.Width, this.Frame.Height);

			var labelImage = new UIImageView (UIImage.FromBundle ("CurrentTrolley/CurrentTrolleyTitle.png"));
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
				Text = DateTime.Now.ToString ("mm/dd/yyyy"),
				TextColor = CustomUIColor.FromHexString ("#000000"),
				Font = UIFont.FromName ("Apple SD Gothic Neo", 16.0f),
				AdjustsFontSizeToFitWidth = false,
				LineBreakMode = UILineBreakMode.TailTruncation,
				Lines = 1,
				TextAlignment = UITextAlignment.Center
			};

			var storeLabel = new UILabel {
				Text = StoreName,
				TextColor = CustomUIColor.FromHexString ("#000000"),
				Font = UIFont.FromName ("Apple SD Gothic Neo", 16.0f),
				AdjustsFontSizeToFitWidth = false,
				LineBreakMode = UILineBreakMode.TailTruncation,
				Lines = 1,
				TextAlignment = UITextAlignment.Center
			};

			userLabel.Frame = new RectangleF (0, 50, this.Frame.Width, 20);
			storeLabel.Frame = new RectangleF (0, 75, this.Frame.Width, 20);
			Add (userLabel);
			Add (storeLabel);
		}

		private void AddButtons(float topMargin)
		{
			var buttonTopMargin = topMargin-60;

			var finishButton = UIButton.FromType (UIButtonType.Custom);
			finishButton.SetImage (UIImage.FromFile ("CurrentTrolley/finish_current_trolley_paid_button_picture_regular.png").ImageWithRenderingMode (UIImageRenderingMode.AlwaysOriginal), UIControlState.Normal);
		
			var cancelButton = UIButton.FromType (UIButtonType.Custom);
			cancelButton.SetImage (UIImage.FromFile ("CurrentTrolley/finish_current_trolley_canceled_button_picture_regular.png").ImageWithRenderingMode (UIImageRenderingMode.AlwaysOriginal), UIControlState.Normal);


			finishButton.Frame = new RectangleF (50, buttonTopMargin, 90, 30);
			cancelButton.Frame = new RectangleF (160, buttonTopMargin, 90, 30);

			finishButton.UserInteractionEnabled = true;

			finishButton.AddGestureRecognizer (new UITapGestureRecognizer (() => {
				SubmitCurrentTrolleyStatus(CurrentTrolleyStatusType.mytickets);
			}));

			cancelButton.UserInteractionEnabled = true;

			cancelButton.AddGestureRecognizer (new UITapGestureRecognizer (() => {
				SubmitCurrentTrolleyStatus(CurrentTrolleyStatusType.canceled);
			}));

			AddSubview (finishButton);
			AddSubview (cancelButton);
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

			var commentsLabel = new UILabel {
				Text = SharedData.CurrentConfigurationData.str_finish_current_trolley_text,
				TextColor = CustomUIColor.FromHexString ("#000000"),
				Font = UIFont.FromName ("Apple SD Gothic Neo", 16.0f),
				AdjustsFontSizeToFitWidth = false,
				LineBreakMode = UILineBreakMode.TailTruncation,
				Lines = 2,
				TextAlignment = UITextAlignment.Center
			};

			var belowCommentLabel = new UILabel {
				Text = SharedData.CurrentConfigurationData.str_finish_current_trolley_question,
				TextColor = CustomUIColor.FromHexString ("#000000"),
				Font = UIFont.FromName ("Apple SD Gothic Neo", 16.0f),
				AdjustsFontSizeToFitWidth = false,
				LineBreakMode = UILineBreakMode.TailTruncation,
				Lines = 3,
				TextAlignment = UITextAlignment.Center
			};

			commentsEditText = new UITextView {
				Text = "Optional",
				TextColor = CustomUIColor.FromHexString ("#000000"),
				Font = UIFont.FromName ("Apple SD Gothic Neo", 16.0f),
				TextAlignment = UITextAlignment.Left,
				BackgroundColor = UIColor.White
			};

			commentsLabel.Frame = new RectangleF (10, 0, this.Frame.Width - 40, 30);
			commentsEditText.Frame = new RectangleF (35, 40, this.Frame.Width - 70, 160);
			belowCommentLabel.Frame = new RectangleF (20, 210, this.Frame.Width - 40, 60);

			scrollView.ContentSize = new SizeF (0, bottomargin - topmargin - 20);
			scrollView.AddSubviews (commentsLabel, commentsEditText, belowCommentLabel);
			this.AddSubview (scrollView);
		}

		public static FinishedCurrentTrolleyView Show(RectangleF bounds, string user, string store,ProfileViewModel pageViewModel)
		{
			SharedInstance = new FinishedCurrentTrolleyView (bounds,user,store,pageViewModel);
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

