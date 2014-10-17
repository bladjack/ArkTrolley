
using System;
using System.Drawing;

using MonoTouch.Foundation;
using MonoTouch.UIKit;
using MonoTouch.CoreAnimation;

namespace ArkTrolley.iPhone.Views
{
	public partial class FirstView : BaseViewController
	{
		public FirstView () : base ("FirstView", "ArkTrolley",null)
		{

		}


		public override void StartView ()
		{
			AddLogos ();
		}


	}
}

