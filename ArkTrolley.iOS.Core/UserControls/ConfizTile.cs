using System;
using System.Drawing;
using MonoTouch.UIKit;

namespace ArkTrolley.iOS.Core.UserControls
{
	public class ConfizTile: UIView
	{
		public CustomTapped Tapped{ get; set;}
		public UIImageView Image{ get; set; }

		public ConfizTile (RectangleF frame, string imageUrl) : base (frame)
		{
			Image = new UIImageView (UIImage.FromBundle (imageUrl));
			this.UserInteractionEnabled = true;
			Tapped=BehaviourExtensions.Tapped (this);

			AddSubview (Image);
		}
	}
}

