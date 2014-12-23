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
using System.Linq;

namespace ArkTrolley.iPhone.UserControls
{
	public class MySelectedTicket: UIView {
		// control declarations

		private static MySelectedTicket SharedInstance;
		private  string LoginUserName;
		private  string StoreName;
		private List<MyTicketDetailModel> myselectedlist;
		UIScrollView scrollView;// new UIScrollView ();


		ProfileViewModel pageViewModel;


		private double totalCost=0;
	
		private MySelectedTicket (RectangleF frame, string user, string store, List<MyTicketDetailModel> data,ProfileViewModel viewModel) : base (frame)
		{
			// configurable bits

			this.pageViewModel = viewModel;
			LoginUserName = user;
			StoreName = store;
			myselectedlist = data;
			scrollView =new UIScrollView ();

			AutoresizingMask = UIViewAutoresizing.FlexibleDimensions;
			pageViewModel.Myselectedlist = new List<MyTicketDetailModel> ();
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
			backgroundImage.Frame = new RectangleF (0, 00, this.Frame.Width, this.Frame.Height);

			var image = await ArkTrolley.iOS.Core.AppHelper.UtilityMethods.LoadImage (ConfigurationSettings.GetSelectedTicketTitleImagePath);
			UIImageView labelImage = new UIImageView ();
			if (image != null) {
				labelImage.Image = image;
			} else {
				labelImage.Image = UIImage.FromBundle ("Common/my_lists_title_regular.png");
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
			var topmargin = 110;
			var bottommargin = this.Frame.Height - 80;

			AddButtons (bottommargin);
			AddStoreList (topmargin, bottommargin);
		}

		private void AddUserAndStore()
		{
			var userLabel = new UILabel {
				Text = LoginUserName + " at " + StoreName ,
				TextColor = CustomUIColor.FromHexString ("#000000"),
				Font = UIFont.FromName ("Apple SD Gothic Neo", 12.0f),
				AdjustsFontSizeToFitWidth = false,
				LineBreakMode = UILineBreakMode.TailTruncation,
				Lines = 1,
				TextAlignment= UITextAlignment.Center
			};

			var storeLabel = new UILabel {
				Text = SharedData.CurrentConfigurationData.str_selected_ticket_note,
				TextColor = CustomUIColor.FromHexString ("#000000"),
				Font = UIFont.FromName ("Apple SD Gothic Neo", 12.0f),
				AdjustsFontSizeToFitWidth = false,
				LineBreakMode = UILineBreakMode.TailTruncation,
				Lines = 3,
				TextAlignment= UITextAlignment.Center
			};

			userLabel.Frame = new RectangleF (0, 50, this.Frame.Width, 30);
			storeLabel.Frame = new RectangleF (30, 85, this.Frame.Width-60, 30);
			Add (userLabel);
			Add (storeLabel);
		}

		private  void AddButtons(float topMargin)
		{
			var buttonTopMargin = topMargin;

			var finishButton = UIButton.FromType (UIButtonType.Custom);
			finishButton.SetImage (UIImage.FromFile ("CurrentTrolley/save_recipe_button_picture_regular.png").ImageWithRenderingMode (UIImageRenderingMode.AlwaysOriginal), UIControlState.Normal);
		

			var cancelButton = UIButton.FromType (UIButtonType.Custom);
			cancelButton.SetImage (UIImage.FromFile ("CurrentTrolley/CancelBut.png").ImageWithRenderingMode (UIImageRenderingMode.AlwaysOriginal), UIControlState.Normal);
		

			//var loginButton = new UIImageView (UIImage.FromBundle ("Login/button.png"));
			//var cancelButton = new UIImageView (UIImage.FromBundle ("Login/button.png"));


			finishButton.Frame = new RectangleF (65, buttonTopMargin + 10, 90, 30);
			cancelButton.Frame = new RectangleF (180, buttonTopMargin + 10, 90, 30);

			finishButton.UserInteractionEnabled = true;

			finishButton.AddGestureRecognizer (new UITapGestureRecognizer (() => {
				SaveRecipe ();
			}));

			cancelButton.UserInteractionEnabled = true;

			cancelButton.AddGestureRecognizer (new UITapGestureRecognizer (() => {
				this.Hide ();
			}));

			AddSubview (finishButton);
			AddSubview (cancelButton);
		}

		private async void SaveRecipe()
		{
			if (pageViewModel.Myselectedlist.Count > 0) {
				await pageViewModel.SaveRecipesData ();
				this.Hide ();
			} else {
				pageViewModel.AppMessageDialog.ShowAppDialog ("You need to select at least one item.", "ArkTrolley");
			}
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
			foreach (var item in myselectedlist) {
					var newUser1 = await StoreHub (
					UIImage.FromBundle ("Common/Ark_lg.png"), 
					item
				);	
				topmargin = 10*i + 70 * i;
				totalCost += Convert.ToDouble(item.flt_price) * Convert.ToDouble( item.flt_quantity);
				newUser1.Frame = new RectangleF (35, topmargin, this.Frame.Width - 70, 70);
				scrollView.AddSubview (newUser1);

				newUser1.UserInteractionEnabled = true;

				newUser1.AddGestureRecognizer (new UITapGestureRecognizer (() => {
					//PageViewModel.NavigateToLoginScreen(item);
				}));
				i++;
			}

			topmargin = 90 * (i);
			scrollView.ContentSize = new SizeF (0, topmargin);
			this.AddSubview (scrollView);

			var totalCostLabel = new UILabel {
				Text = SharedData.CurrentConfigurationData.str_selected_ticket_text +" "+ totalCost.ToString() ,
				TextColor = CustomUIColor.FromHexString ("#000000"),
				Font = UIFont.FromName ("Apple SD Gothic Neo", 14.0f),
				AdjustsFontSizeToFitWidth = false,
				LineBreakMode = UILineBreakMode.TailTruncation,
				Lines = 1,
				TextAlignment= UITextAlignment.Center
			};
			totalCostLabel.Frame = new RectangleF (0, bottomargin- 10, this.Frame.Width, 20);

			this.AddSubview (totalCostLabel);

		}

		private async Task<UIView> StoreHub(UIImage userImage, MyTicketDetailModel item)
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
				Text = item.str_name+"\n"+ "$ " +item.flt_price+" Qty." + item.flt_quantity ,
				TextColor = CustomUIColor.FromHexString ("#000000"),
				Font = UIFont.FromName ("Apple SD Gothic Neo", 14.0f),
				AdjustsFontSizeToFitWidth = false,
				LineBreakMode = UILineBreakMode.TailTruncation,
				Lines = 3,
				TextAlignment= UITextAlignment.Center
			};
			descriptionLabel.Frame = new RectangleF (65, 5, 100, 70);

			var checkBoxSelectedButton = new UIImageView (UIImage.FromBundle ("Login/selectedCheckBox.png"));
			checkBoxSelectedButton.Frame = new RectangleF (this.Frame.Width- 110, 20, 18, 18);
			checkBoxSelectedButton.UserInteractionEnabled = true;

			var checkBoxUnSelectedButton = new UIImageView (UIImage.FromBundle ("Login/unselectedbox.png"));
			checkBoxUnSelectedButton.Frame = new RectangleF (this.Frame.Width- 110, 20, 18, 18);
			checkBoxUnSelectedButton.UserInteractionEnabled = true;

			checkBoxSelectedButton.Hidden = true;
			checkBoxUnSelectedButton.Hidden = false;


			checkBoxSelectedButton.UserInteractionEnabled = true;

			checkBoxSelectedButton.AddGestureRecognizer (new UITapGestureRecognizer (() => {
				checkBoxSelectedButton.Hidden = true;
				checkBoxUnSelectedButton.Hidden = false;
				pageViewModel.Myselectedlist.Remove(item);

			}));

			checkBoxUnSelectedButton.UserInteractionEnabled = true;

			checkBoxUnSelectedButton.AddGestureRecognizer (new UITapGestureRecognizer (() => {
				checkBoxSelectedButton.Hidden = false;
				checkBoxUnSelectedButton.Hidden = true;

				if(pageViewModel.Myselectedlist.FirstOrDefault(x=>x.int_id==item.int_id)==null)
				{
					pageViewModel.Myselectedlist.Add(item);
				}
			}));


			userView.AddSubview (backGroundImage);
			userView.AddSubview (userImageView);
			userView.AddSubview (descriptionLabel);
			userView.AddSubview (checkBoxUnSelectedButton);
			userView.AddSubview (checkBoxSelectedButton);


			return userView;
		}

		private void DeleteItemFromList(MyTicketDetailModel item)
		{
			myselectedlist.Remove (item);
			FlushScrollViewerData ();
			AddListView ();
		}

		private void FlushScrollViewerData()
		{
			foreach (var sub in scrollView.Subviews) {
				sub.RemoveFromSuperview ();
			}
		}



		public static MySelectedTicket Show(RectangleF bounds, string user, string store, List<MyTicketDetailModel> data,ProfileViewModel viewModel)
		{
			SharedInstance = new MySelectedTicket (bounds,user,store,data,viewModel);
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

