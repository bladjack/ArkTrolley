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
using System.Linq;

namespace ArkTrolley.iPhone.UserControls
{
	public class ItemsComparisionView: UIView {
		// control declarations

		private static ItemsComparisionView SharedInstance;
		UITextView commentsEditText;

		UIScrollView scrollView;// new UIScrollView ();
		ProfileViewModel pageViewModel;
		private ItemsComparisionView (RectangleF frame, ProfileViewModel pageViewModel) : base (frame)
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
		}

		private async Task AddBackGroundAndTitle()
		{
			var backgroundImage = new UIImageView (UIImage.FromBundle ("Login/Box.png"));
			backgroundImage.Frame = this.Frame;
			backgroundImage.Frame = new RectangleF (0, 0, this.Frame.Width, this.Frame.Height);

			var labelImage = new UIImageView ();
			UIImage image = null;// await ArkTrolley.iOS.Core.AppHelper.UtilityMethods.LoadImage (ConfigurationSettings.GetSearchItemTitleImagePath);
			if (image != null) {
				labelImage.Image = image;
			} else {
				labelImage.Image = UIImage.FromBundle ("Common/items_comparison_title_regular.png");
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
			var topmargin = 60;
			var bottommargin = this.Frame.Height;


			AddStoreList (topmargin, bottommargin);
			AddButtons (bottommargin);
		}

		private void AddButtons(float topMargin)
		{
			var buttonTopMargin = topMargin-60;

			var finishButton = UIButton.FromType (UIButtonType.Custom);
			finishButton.SetImage (UIImage.FromFile ("Common/add_button_picture_regular.png").ImageWithRenderingMode (UIImageRenderingMode.AlwaysOriginal), UIControlState.Normal);
		
			var cancelButton = UIButton.FromType (UIButtonType.Custom);
			cancelButton.SetImage (UIImage.FromFile ("Common/cancel_button_picture_regular.png").ImageWithRenderingMode (UIImageRenderingMode.AlwaysOriginal), UIControlState.Normal);


			finishButton.Frame = new RectangleF (50, buttonTopMargin, 90, 30);
			cancelButton.Frame = new RectangleF (160, buttonTopMargin, 90, 30);

			finishButton.UserInteractionEnabled = true;

			finishButton.AddGestureRecognizer (new UITapGestureRecognizer (() => {
				//SubmitCurrentTrolleyStatus(CurrentTrolleyStatusType.mytickets);
			}));

			cancelButton.UserInteractionEnabled = true;

			cancelButton.AddGestureRecognizer (new UITapGestureRecognizer (() => {
				this.Hide();
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

		private async void AddStoreList(float topmargin, float bottomargin)
		{

			scrollView.Frame = new RectangleF (120, topmargin, this.Frame.Width - 160, 240);

			scrollView.BackgroundColor = CustomUIColor.FromHexString ("#00ffffff");
			scrollView.AutoresizingMask = UIViewAutoresizing.FlexibleWidth;
			scrollView.AlwaysBounceVertical = false;
			scrollView.ShowsVerticalScrollIndicator = false;

			var leftImage = new UIImageView (UIImage.FromBundle ("Common/left_arrow_icon_picture_regular.png"));
			var rightImage = new UIImageView (UIImage.FromBundle ("Common/right_arrow_icon_picture_regular.png"));
			var leftImage1 = new UIImageView (UIImage.FromBundle ("Common/left_arrow_icon_picture_regular.png"));
			var rightImage1 = new UIImageView (UIImage.FromBundle ("Common/right_arrow_icon_picture_regular.png"));


			int i = 0;
			var leftMargin = 0;

			for (int j = 1; j < pageViewModel.SearchResultData.responseData.Count; j++) {
				var searchItem = await StoreDataItem (pageViewModel.SearchResultData.responseData [j]);
				leftMargin = i * 80;
				searchItem.Frame = new RectangleF (leftMargin, 0, 70, 230);
				scrollView.AddSubview (searchItem);
				i++;
			}

			var firstItem = await StoreDataItem (pageViewModel.SearchResultData.responseData [0]);
			firstItem.Frame = new RectangleF (20, topmargin, 75, 240);
			firstItem.BackgroundColor = UIColor.Gray;

			leftImage.Frame = new RectangleF (100, topmargin + 20, 20, 30);
			leftImage1.Frame = new RectangleF (100, topmargin + 120, 20, 30);
			rightImage.Frame = new RectangleF (this.Frame.Width - 40, topmargin + 20, 20, 30);
			rightImage1.Frame = new RectangleF (this.Frame.Width - 40, topmargin + 120, 20, 30);

			scrollView.ContentSize = new SizeF (i * 80, 230);

			var storeNameLabel = new UILabel {
				Text = "NOTE: You can click another store item to create/update another pending list.",
				TextColor = CustomUIColor.FromHexString ("#000000"),
				Font = UIFont.FromName ("Apple SD Gothic Neo", 10.0f),
				AdjustsFontSizeToFitWidth = false,
				LineBreakMode = UILineBreakMode.TailTruncation,	
				Lines = 2,
				TextAlignment = UITextAlignment.Center	
			};
			storeNameLabel.Frame = new RectangleF (10, 350, this.Bounds.Width - 60, 30);

			this.AddSubviews (firstItem, scrollView, leftImage, leftImage1, rightImage, rightImage1, storeNameLabel);
		}

		private async Task<UIView> StoreDataItem(List<SearchItem> searchItem)
		{
			var itemview = new UIView ();
			var firstItem = searchItem.FirstOrDefault ();
			var storeView = await StoreView (firstItem.int_chain, firstItem.str_store_name);
			storeView.Layer.CornerRadius = 5.0f;
			storeView.Frame = new RectangleF (0, 0, 70, 60);

			var upImage = new UIImageView (UIImage.FromBundle ("Common/up_arrow_icon_picture_regular.png"));
			upImage.Frame = new RectangleF (20, 65 , 30, 20);

			var downImage = new UIImageView (UIImage.FromBundle ("Common/down_arrow_icon_picture_regular.png"));
			downImage.Frame = new RectangleF (20, 225, 30, 20);

			var storeItemScroll = new UIScrollView ();
			storeItemScroll.Frame = new RectangleF (0, 90 , 100, 130);

			storeItemScroll.BackgroundColor = CustomUIColor.FromHexString ("#00ffffff");
			storeItemScroll.AutoresizingMask = UIViewAutoresizing.FlexibleWidth;
			storeItemScroll.AlwaysBounceVertical = false;
			storeItemScroll.ShowsVerticalScrollIndicator = false;
			storeItemScroll.AlwaysBounceHorizontal = false;
			storeItemScroll.ShowsHorizontalScrollIndicator = false;


			var scrollContentWidth = 0;
			int i = 0;
			foreach (var item in searchItem) {
				var storeItem = await StoreItemView (item.int_id, item.str_name, item.flt_price);
				storeItem.Layer.CornerRadius = 5.0f;
				int topMargin = i * 130;
				storeItem.Frame = new RectangleF (0, topMargin, 70, 125);
				storeItemScroll.AddSubview (storeItem);
				i++;
			}

			storeItemScroll.ContentSize = new SizeF (100, i * 130);
			itemview.AddSubviews (storeView, storeItemScroll, upImage, downImage);
			return itemview;
		}







		private async Task<UIView> StoreView(string storeId, string storeName)
		{

			var storeView = new UIView ();
			storeView.Frame = new RectangleF (0, 0, 70, 60);
			storeView.BackgroundColor = UIColor.White;
		
			var downloadimage = await ArkTrolley.iOS.Core.AppHelper.UtilityMethods.LoadImage (
				                   SharedData.CurrentConfigurationData.str_web_service_root +
				                   SharedData.CurrentConfigurationData.str_store_chains_pictures_path + storeId + ".jpg");

			var userImageView = new UIImageView ();
			if (downloadimage == null) {
				userImageView.Image = UIImage.FromBundle ("Common/Ark_lg.png");
			} else {
				userImageView.Image = downloadimage;
			}
			//new UIImageView (UIImage.FromBundle ("Common/Ark_lg.png"));
			userImageView.Frame = new RectangleF (5, 0, 60, 35);

			var storeNameLabel = new UILabel {
				Text = storeName,
				TextColor = CustomUIColor.FromHexString ("#000000"),
				Font = UIFont.FromName ("Apple SD Gothic Neo", 10.0f),
				AdjustsFontSizeToFitWidth = false,
				LineBreakMode = UILineBreakMode.TailTruncation,	
				Lines = 2,
				TextAlignment = UITextAlignment.Center	
			};

			storeNameLabel.Frame = new RectangleF (0, 35, 60, 25);


			storeView.AddSubview (userImageView);
			storeView.AddSubview (storeNameLabel);
			return storeView;
		}

		private async Task<UIView> StoreItemView(string storeItemId, string storeItemName,string price)
		{

			var storeItemView = new UIView ();
			storeItemView.Frame = new RectangleF (0, 0, 70, 120);
			storeItemView.BackgroundColor = UIColor.White;
			var downloadimage = await ArkTrolley.iOS.Core.AppHelper.UtilityMethods.LoadImage (
				                    SharedData.CurrentConfigurationData.str_web_service_root +
				SharedData.CurrentConfigurationData.str_items_pictures_path + storeItemId + ".jpg");

			var userImageView = new UIImageView ();
			if (downloadimage == null) {
				userImageView.Image = UIImage.FromBundle ("Common/Ark_lg.png");
			} else {
				userImageView.Image = downloadimage;
			}
			//new UIImageView (UIImage.FromBundle ("Common/Ark_lg.png"));
			userImageView.Frame = new RectangleF (10, 5, 50, 50);

			var storeItemNameLabel = new UILabel {
				Text = storeItemName,
				TextColor = CustomUIColor.FromHexString ("#000000"),
				Font = UIFont.FromName ("Apple SD Gothic Neo", 10.0f),
				AdjustsFontSizeToFitWidth = false,
				LineBreakMode = UILineBreakMode.TailTruncation,
				Lines = 2,
				TextAlignment = UITextAlignment.Center
			};

			storeItemNameLabel.Frame = new RectangleF (0, 60, 60, 30);

			var storeItemPriceLabel = new UILabel {
				Text = price + " USD",
				TextColor = CustomUIColor.FromHexString ("#000000"),
				Font = UIFont.FromName ("Apple SD Gothic Neo", 10.0f),
				AdjustsFontSizeToFitWidth = false,
				LineBreakMode = UILineBreakMode.TailTruncation,
				Lines = 1,
				TextAlignment = UITextAlignment.Center
			};

			storeItemPriceLabel.Frame = new RectangleF (5, 95, 70, 20);



			storeItemView.AddSubview (userImageView);
			storeItemView.AddSubview (storeItemNameLabel);
			storeItemView.AddSubview (storeItemPriceLabel);

			return storeItemView;
		}

		public static ItemsComparisionView Show(RectangleF bounds, ProfileViewModel pageViewModel)
		{
			SharedInstance = new ItemsComparisionView (bounds,pageViewModel);
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

