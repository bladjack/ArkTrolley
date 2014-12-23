

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
	public class NewUserView: UIView {
		// control declarations
		ProfileViewModel pageViewModel;
		private static NewUserView SharedInstance;
		UIScrollView scrollView;// new UIScrollView ();

		private double totalCost=0;

		private NewUserView (RectangleF frame, ProfileViewModel pageViewModel) : base (frame)
		{
			// configurable bits

			this.pageViewModel = pageViewModel;
			scrollView =new UIScrollView ();

			this.BackgroundColor=UIColor.FromPatternImage (UIImage.FromFile ("CurrentTrolley/OverBackMenu.png"));

			this.Layer.CornerRadius = 10.0f;

			//BackgroundColor = UIColor.Gray;
			Alpha = 1.0f;
			AutoresizingMask = UIViewAutoresizing.FlexibleDimensions;

			ShowTheData ();

		}

		private async void ShowTheData()
		{
			await AddBackGroundAndTitle ();

			AddUserAndStore ();
		}

		private async Task AddBackGroundAndTitle()
		{
			var backgroundImage = new UIImageView (UIImage.FromBundle ("Login/Box.png"));
			backgroundImage.Frame = new RectangleF (8, 30, this.Frame.Width-20, this.Frame.Height-40);
			var image = await ArkTrolley.iOS.Core.AppHelper.UtilityMethods.LoadImage (ConfigurationSettings.GetMyTicketsTitleImagePath);
			UIImageView labelImage = new UIImageView ();
			if (image != null) {
				labelImage.Image = image;
			} else {
				labelImage.Image = UIImage.FromBundle ("CurrentTrolley/CurrentTrolleyTitle.png");
			}
			labelImage.Frame = new RectangleF (80, 50, 160, 20);

			var lineimage = new UIImageView (UIImage.FromBundle ("PickStore/LineBlue.png"));
			lineimage.Frame = new RectangleF (35, 80, this.Frame.Width - 70, 2);

			AddSubview (backgroundImage);
			AddSubview (labelImage);
			AddSubview (lineimage);

			var bottommargin = this.Frame.Height - 80;

			AddButtons (bottommargin);
			AddListView ();
		}

		private void AddListView()
		{
			var topmargin = 140;
			var bottommargin = this.Frame.Height - 80;

			AddButtons (bottommargin);
			AddStoreList (topmargin, bottommargin);
		}

		private void AddUserAndStore()
		{
			var userLabel = new UILabel {
				Text = SharedData.CurrentConfigurationData.str_my_lists_text,
				TextColor = CustomUIColor.FromHexString ("#000000"),
				Font = UIFont.FromName ("Apple SD Gothic Neo", 16.0f),
				AdjustsFontSizeToFitWidth = false,
				LineBreakMode = UILineBreakMode.TailTruncation,
				Lines = 4,
				TextAlignment= UITextAlignment.Center
			};


			userLabel.Frame = new RectangleF (30, 85, this.Frame.Width-60, 50);

			Add (userLabel);
		}

		private void AddButtons(float topMargin)
		{
			var buttonTopMargin = topMargin;

			var finishButton = UIButton.FromType (UIButtonType.Custom);
			finishButton.SetImage (UIImage.FromFile ("CurrentTrolley/FinishBut.png").ImageWithRenderingMode (UIImageRenderingMode.AlwaysOriginal), UIControlState.Normal);
			finishButton.SetImage (UIImage.FromFile ("CurrentTrolley/FinishButOv.png").ImageWithRenderingMode (UIImageRenderingMode.AlwaysOriginal), UIControlState.Highlighted);
			finishButton.SetImage (UIImage.FromFile ("CurrentTrolley/FinishButOv.png").ImageWithRenderingMode (UIImageRenderingMode.AlwaysOriginal), UIControlState.Disabled);


			var cancelButton = UIButton.FromType (UIButtonType.Custom);
			cancelButton.SetImage (UIImage.FromFile ("CurrentTrolley/CancelBut.png").ImageWithRenderingMode (UIImageRenderingMode.AlwaysOriginal), UIControlState.Normal);
			cancelButton.SetImage (UIImage.FromFile ("CurrentTrolley/CancelButOv.png").ImageWithRenderingMode (UIImageRenderingMode.AlwaysOriginal), UIControlState.Highlighted);
			cancelButton.SetImage (UIImage.FromFile ("CurrentTrolley/CancelButOv.png").ImageWithRenderingMode (UIImageRenderingMode.AlwaysOriginal), UIControlState.Disabled);


			//var loginButton = new UIImageView (UIImage.FromBundle ("Login/button.png"));
			//var cancelButton = new UIImageView (UIImage.FromBundle ("Login/button.png"));


			finishButton.Frame = new RectangleF (65, buttonTopMargin+10, 90, 30);
			cancelButton.Frame = new RectangleF (180, buttonTopMargin+10, 90, 30);

			finishButton.UserInteractionEnabled = true;

			finishButton.AddGestureRecognizer (new UITapGestureRecognizer (() => {
				this.Hide();
			}));

			cancelButton.UserInteractionEnabled = true;

			cancelButton.AddGestureRecognizer (new UITapGestureRecognizer (() => {
				this.Hide();
			}));

			//	AddSubview (finishButton);
			AddSubview (cancelButton);
		}


		private async void AddStoreList(float topmargin, float bottomargin)
		{

			scrollView.Frame = new RectangleF (0, topmargin, this.Frame.Width, bottomargin - topmargin-20);
			scrollView.BackgroundColor = CustomUIColor.FromHexString ("#00ffffff");
			scrollView.AutoresizingMask = UIViewAutoresizing.FlexibleWidth;
			scrollView.AlwaysBounceVertical = false;
			scrollView.ShowsVerticalScrollIndicator = false;
			int i = 0;
			topmargin = 0;


			topmargin = 90 * (i);
			scrollView.ContentSize = new SizeF (0, topmargin);
			this.AddSubview (scrollView);
		}

		private async Task<UIView> StoreHub(UIImage userImage, MyTicketsModel item)
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
				Text = Convert.ToDateTime(item.dte_closed).ToString("yyyy-dd-mm")+"\n"+ "$ " +item.flt_total_cost ,
				TextColor = CustomUIColor.FromHexString ("#000000"),
				Font = UIFont.FromName ("Apple SD Gothic Neo", 14.0f),
				AdjustsFontSizeToFitWidth = false,
				LineBreakMode = UILineBreakMode.TailTruncation,
				Lines = 3,
				TextAlignment= UITextAlignment.Center
			};
			descriptionLabel.Frame = new RectangleF (65, 5, 100, 70);

			var crossImageView = new UIImageView (UIImage.FromBundle("CurrentTrolley/RedX")); 
			//new UIImageView (UIImage.FromBundle ("Common/Ark_lg.png"));
			crossImageView.Frame = new RectangleF (this.Frame.Width- 110 , 20, 35, 30);

			crossImageView.UserInteractionEnabled = true;

			crossImageView.AddGestureRecognizer (new UITapGestureRecognizer (() => {
				DeleteItemFromList(item);
			}));

			userView.AddSubview (backGroundImage);
			userView.AddSubview (userImageView);
			userView.AddSubview (descriptionLabel);
			userView.AddSubview (crossImageView);

			return userView;
		}

		private void DeleteItemFromList(MyTicketsModel item)
		{
			FlushScrollViewerData ();
			AddListView ();
		}

		private void FlushScrollViewerData()
		{
			foreach (var sub in scrollView.Subviews) {
				sub.RemoveFromSuperview ();
			}
		}



		public static NewUserView Show(RectangleF bounds, string user, string store, List<MyTicketsModel> data, ProfileViewModel viewModel)
		{
			//SharedInstance = new NewUserView (bounds,user,store,data,viewModel);
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



