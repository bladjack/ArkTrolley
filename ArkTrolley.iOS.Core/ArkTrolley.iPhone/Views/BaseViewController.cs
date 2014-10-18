using System;
using Cirrious.MvvmCross.Touch.Views;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using ArkTrolley.iOS.Core.AppHelper;
using System.Drawing;
using ArkTrolley.iOS.Core.UserControls;

namespace ArkTrolley.iPhone.Views
{
	public abstract class BaseViewController : MvxViewController
	{
		public string PageTitle{ get; set; }
		public int TopMargin{ get; set; }
		public UIView Logo{ get; set;}
		public UIView Flood{ get; set;}
		public UIImageView BackImage{ get; set;}

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

		}

		public void Initilized()
		{
			TopMargin = 100;
			Logo = new UIView ();

			Logo.Frame = new RectangleF (0, 20, View.Frame.Width, 135/2);

			var logoImage = new UIImageView (UIImage.FromBundle ("Common/ArkTrolley.png"));
			var logoTides = new UIImageView (UIImage.FromBundle ("Common/Ola.png"));

			Logo.BackgroundColor = CustomUIColor.FromHexString ("#AFE2FF");
		
			logoImage.Frame = new RectangleF (0, 0, View.Frame.Width, 135/2);
			logoTides.Frame = new RectangleF (0, 135/2, View.Frame.Width, 46/2);
			logoTides.BackgroundColor= CustomUIColor.FromHexString ("#AFE2FF");




			Logo.Add (logoImage);
			Logo.Add (logoTides);

			var floodImage = new UIImageView (UIImage.FromBundle ("Common/Flood.png"));
			floodImage.BackgroundColor= CustomUIColor.FromHexString ("#AFE2FF");
			floodImage.Frame = new RectangleF ((View.Frame.Width -300)/2, 6, 300, 24);
			floodImage.ContentMode= UIViewContentMode.ScaleAspectFill;

			 
			var bounds=UIScreen.MainScreen.Bounds;
			Flood = new UIView ();
			Flood.Frame = new RectangleF (0 , View.Frame.Height - 36 , View.Frame.Width, 36);
			Flood.BackgroundColor= CustomUIColor.FromHexString ("#AFE2FF");
			Flood.AddSubview (floodImage);
		}

		public void AddLogos()
		{
			BackImage = new UIImageView ();
			BackImage.Frame = new RectangleF (0, 00, View.Frame.Width, UIScreen.MainScreen.Bounds.Bottom- Logo.Center.Y- 50 );
			BackImage.BackgroundColor = CustomUIColor.FromHexString ("#AFE2FF");

			View.AddSubview (BackImage);
			View.AddSubview (Logo);
			View.AddSubview (Flood);
		}

		public abstract void StartView ();

		public UILabel CreateLebel(string name, float fontSize, string colorCode= "#000000" ,string fontName="Apple SD Gothic Neo")
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

	}
}

