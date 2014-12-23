
using System;
using System.Drawing;

using MonoTouch.Foundation;
using MonoTouch.UIKit;
using ArkTrolley.iOS.Core.ViewModels;
using ArkTrolley.iOS.Core.AppHelper;

namespace ArkTrolley.iPhone.Views
{
	public partial class AppWebView : BaseViewController
	{
		public AppWebViewModel PageViewModel
		{
			get{ return (AppWebViewModel)ViewModel;}
		}

		public AppWebView () : base ("AppWebView", "ArkTrolley",null)
		{

		}
		UIWebView webView;
		public async override void StartView ()
		{
			AddLogos ();
			webView = new UIWebView(View.Bounds);
			webView.LoadFinished += WebViewLoadCompleted;

			webView.LoadRequest (new NSUrlRequest (new NSUrl (PageViewModel.Url)));


			webView.Frame = new RectangleF (0, TopMargin+20, View.Frame.Width, View.Frame.Height); 

			View.AddSubview(webView);
			ShowProgressLoading = true;

			webView.ScrollView.Bounces = false;
			webView.ScalesPageToFit = true;
			CustomBottombar ();
		}

		public void CustomBottombar()
		{
			this.NavigationController.ToolbarHidden = false;
			var buttons = new UIBarButtonItem[] {
				new UIBarButtonItem (UIBarButtonSystemItem.Cancel, (sender, args) => {
					PageViewModel.GoBack ();
				})
			};

			this.SetToolbarItems(buttons, true);
		}

		public override void ViewWillDisappear (bool animated)
		{
			base.ViewWillDisappear (animated);
			this.NavigationController.ToolbarHidden = true;
		}

		private void WebViewLoadCompleted(object sender, EventArgs e)
		{
			webView.Hidden = false;
			ShowProgressLoading = false;
		}


	}
}