
using System;
using System.Drawing;

using MonoTouch.Foundation;
using MonoTouch.UIKit;
using MonoTouch.CoreAnimation;
using MonoTouch.ObjCRuntime;

namespace ArkTrolley.iPhone.Views
{
	public partial class FirstView : BaseViewController
	{
		public FirstView () : base ("FirstView", "ArkTrolley",null)
		{

		}
		PointF pt;
		public override void StartView ()
		{
			AddLogos ();


			var pt= Logo.Center;
			Logo.Center = new PointF (Logo.Center.X , UIScreen.MainScreen.Bounds.Bottom- Logo.Center.Y- 50 );


			UIView.BeginAnimations ("slideAnimation");

			UIView.SetAnimationDuration (5);
			UIView.SetAnimationCurve (UIViewAnimationCurve.EaseIn);
			UIView.SetAnimationRepeatCount (0);
			UIView.SetAnimationRepeatAutoreverses (false);

			UIView.SetAnimationDelegate (this);

			Logo.Center = new PointF (pt.X , pt.Y);
			BackImage.Center = new PointF (pt.X, -170);
			UIView.CommitAnimations ();
		}


	}
}

