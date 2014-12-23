using System;
using Cirrious.MvvmCross.Touch.Views;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using ArkTrolley.iOS.Core.AppHelper;
using System.Drawing;
using ArkTrolley.iOS.Core.UserControls;
using ArkTrolley.iPhone.UserControls;
using System.Threading.Tasks;
using System.Net.Http;

namespace ArkTrolley.iPhone.Views
{
	public abstract class BaseViewController : MvxViewController
	{
		public float ScreenWidth{ get; set;}
		public float ScreenHeight{ get; set;}
		public string PageTitle{ get; set; }
		public int TopMargin{ get; set; }
		public int BottomMargin{ get; set; }
		public UIView Logo{ get; set;}
		public UIView Flood{ get; set;}
		public UIImageView BackImage{ get; set;}

		private LoadingOverlay Loading;

		private bool showProgressLoading;
		public bool ShowProgressLoading 
		{
			set{ 
				showProgressLoading = value;
				if (showProgressLoading) {
					var bounds = UIScreen.MainScreen.Bounds;
					Loading = LoadingOverlay.Show (bounds);
					this.View.Add (Loading);
				} else {
					if (Loading != null) {
						Loading.Hide ();
					}
				}
			}
		}

		private UIView activeview;
		private float scroll_amount = 0.0f;
		private float bottom=0.0f;
		private float offset = 500.0f;
		private bool moveViewUp=false;

		public BaseViewController (string viewName,string title,NSBundle bundle) : base (viewName, bundle)
		{
			this.PageTitle = title;

		}

		public override void DidReceiveMemoryWarning ()
		{
			// Releases the view if it doesn't have a superview.
			base.DidReceiveMemoryWarning ();
			// Release any cached data, images, etc that aren't in use.
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			Initilized ();
			this.Title = PageTitle;
			//this.View.BackgroundColor=UIColor.FromPatternImage (UIImage.FromFile ("Images/Common/InfowareBG.png"));
			this.View.BackgroundColor = UIColor.White;
			NavigationController.NavigationBar.Opaque = true;
			NavigationController.NavigationBar.Translucent = false;
			NavigationController.NavigationBarHidden = true;

			NavigationController.NavigationBar.TintColor = UIColor.Black;
			//Bindings and View Specific Code
			StartView ();

			NSNotificationCenter.DefaultCenter.AddObserver
			(UIKeyboard.DidShowNotification,KeyBoardUpNotification);

			// Keyboard Down
			NSNotificationCenter.DefaultCenter.AddObserver
			(UIKeyboard.WillHideNotification,KeyBoardDownNotification);

		}

		private UIView FindFirstResponder(UIView view)
		{
			if (view.IsFirstResponder) {
				return view;
			}
			foreach (var item in view.Subviews) {
				var responder = FindFirstResponder (item);
				if (responder != null)
					return responder;
			}
			return null;
		}

		public void KeyBoardUpNotification(NSNotification notification)
		{
			// get the keyboard size
			RectangleF r = UIKeyboard.BoundsFromNotification (notification);

			// Find what opened the keyboard
			activeview = FindFirstResponder (this.View);
			if (activeview == null)
				return;

			offset = UIScreen.MainScreen.Bounds.Height - 100;
			// Bottom of the controller = initial position + height + offset      
			bottom = (activeview.Frame.Y + activeview.Frame.Height + offset);
			moveViewUp = false;
			ScrollTheView (moveViewUp);
			// Calculate how far we need to scroll
			scroll_amount = (r.Height - (View.Frame.Size.Height - bottom)) ;

			// Perform the scfabrolling
			if (scroll_amount > 0) {
				moveViewUp = true;
				ScrollTheView (moveViewUp);
			} else {
				moveViewUp = false;
			}

		}

		public void KeyBoardDownNotification(NSNotification notification)
		{
			if(moveViewUp){ScrollTheView(false);}
		}

		private void ScrollTheView(bool move)
		{
			// scroll the view up or down
			UIView.BeginAnimations (string.Empty, System.IntPtr.Zero);
			UIView.SetAnimationDuration (0.3);

			RectangleF frame = View.Frame;

			if (move) {
				frame.Y -= scroll_amount;
			} else {
				frame.Y += scroll_amount;
				scroll_amount = 0;
			}

			View.Frame = frame;
			UIView.CommitAnimations();
		}


		public void Initilized()
		{
			TopMargin = 100;
			BottomMargin = 0;//26;
			Logo = new UIView ();

			ScreenWidth = UIScreen.MainScreen.Bounds.Width;
			ScreenHeight = UIScreen.MainScreen.Bounds.Height;

			Logo.Frame = new RectangleF (0, 20, ScreenWidth, (135/2));
			Logo.BackgroundColor = CustomUIColor.FromHexString ("#AFE2FF");

			var logoImage = new UIImageView (UIImage.FromBundle ("Common/ArkTrolley.png"));
			var logoTides = new UIImageView (UIImage.FromBundle ("Common/Ola.png"));

			logoImage.BackgroundColor = CustomUIColor.FromHexString ("#AFE2FF");
		
			logoImage.Frame = new RectangleF (0, 0, ScreenWidth, 135/2);
			logoTides.Frame = new RectangleF (0, 135/2, ScreenWidth, 46/2);
			logoTides.BackgroundColor= CustomUIColor.FromHexString ("#AFE2FF");

			var whitelayer = new UIImageView ();
			whitelayer.BackgroundColor = UIColor.White;
			whitelayer.Frame = new RectangleF(0, 135 / 2 + 40 / 2, ScreenWidth, 5);


			Logo.Add (logoImage);
			Logo.Add (logoTides);
			Logo.Add (whitelayer);

			var floodImage = new UIImageView (UIImage.FromBundle ("Common/Flood.png"));
			floodImage.BackgroundColor= CustomUIColor.FromHexString ("#AFE2FF");
			floodImage.Frame = new RectangleF ((View.Frame.Width -300)/2, 6, 300, 24);
			floodImage.ContentMode= UIViewContentMode.ScaleAspectFill;

			 
			var bounds=UIScreen.MainScreen.Bounds;
			Flood = new UIView ();
			Flood.Frame = new RectangleF (0 , ScreenHeight - 36 , ScreenWidth, 36);
			Flood.BackgroundColor= CustomUIColor.FromHexString ("#AFE2FF");
			Flood.AddSubview (floodImage);

			BackImage = new UIImageView ();
			BackImage.Frame = new RectangleF (0, 00, View.Frame.Width, UIScreen.MainScreen.Bounds.Bottom- Logo.Center.Y- 50 );
			BackImage.BackgroundColor = CustomUIColor.FromHexString ("#AFE2FF");
		}

		public void AddLogos()
		{

			View.AddSubview (Logo);
			//View.AddSubview (Flood);
		}

		public abstract void StartView ();

		public UILabel CreateLebel(string name, float fontSize, string colorCode= "#000000" ,string fontName="Verdana")
		{
			var customLabel = new UILabel
			{
				Text = name,
				TextColor = CustomUIColor.FromHexString(colorCode),
				Font = UIFont.FromName(fontName, fontSize),
				AdjustsFontSizeToFitWidth = false,
				LineBreakMode = UILineBreakMode.TailTruncation,
				Lines = 1
			};
			return customLabel;
		}

		public UILabel MultiLinesLabel(string fontName,string text)
		{
			var label = new UILabel {
				Font = AppCustomFont (fontName, 18),
				TextColor = UIColor.FromRGB (0.41f, 0.41f, 0.41f),
				TextAlignment = UITextAlignment.Justified,
				Lines = 100
			};
			var height = string.IsNullOrEmpty (text)
				? 0f
				: ((NSString)text).StringSize (label.Font, new SizeF (626, 630),
					UILineBreakMode.WordWrap).Height;
			label.Frame = new RectangleF (new PointF (390, 125), new SizeF (610, (height + 25)));
			return label;
		}

		public UIFont AppCustomFont(string fontName,float size)
		{
			return UIFont.FromName(fontName, size);

		}

		protected ConfizTile GetConfizTile(string imageUrl)
		{
			var tile = new ConfizTile (new System.Drawing.RectangleF (), imageUrl);
			return tile;
		}

		public UITextField GetTextField(string placeholder, bool secureTextEntry= true, UIKeyboardType type= UIKeyboardType.Default)
		{
			var uitext=new UITextField {
				BorderStyle = UITextBorderStyle.RoundedRect,
				SecureTextEntry = secureTextEntry,
				Placeholder = placeholder,
				AutocorrectionType = UITextAutocorrectionType.No,
				KeyboardType = type,
				ReturnKeyType = UIReturnKeyType.Done,
				ClearButtonMode = UITextFieldViewMode.WhileEditing,
			};


			uitext.Layer.BorderColor = UIColor.Red.CGColor;
			//uitext.Layer.CornerRadius = 5;

			uitext.ShouldReturn += (textField) => { 
				uitext.ResignFirstResponder(); 
				return true;
			};

			return uitext;
		}

		public static async Task<UIImage> LoadImage (string imageUrl)
		{
			try {
				var httpClient = new HttpClient ();

				Task<byte[]> contentsTask = httpClient.GetByteArrayAsync (imageUrl);

				// await! control returns to the caller and the task continues to run on another thread
				var contents = await contentsTask;

				// load from bytes
				return UIImage.LoadFromData (NSData.FromArray (contents));
			} catch {
				return null;
			}
		}

	}
}

