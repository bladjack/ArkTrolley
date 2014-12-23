using System;
using MonoTouch.UIKit;
using System.Drawing;

namespace ArkTrolley.iOS.Core.UserControls
{
	public class CheckBox:UIView
	{
		public bool IsChecked { get; private set;}
		public CheckBox (RectangleF frame):base(frame)
		{
			IsChecked = true;
			var checkBoxSelectedButton = new UIImageView(UIImage.FromBundle("Login/selectedCheckBox.png"));
			checkBoxSelectedButton.Frame= new RectangleF (0 , 0 , frame.Width  , frame.Height );
			checkBoxSelectedButton.UserInteractionEnabled=true;

			var checkBoxUnSelectedButton = new UIImageView(UIImage.FromBundle("Login/unselectedbox.png"));
			checkBoxUnSelectedButton.Frame= new RectangleF (0 , 0 , frame.Width ,frame.Height );
			checkBoxUnSelectedButton.UserInteractionEnabled=true;

			var checkBoxText =new UILabel
			{
				Text = "Remember me",
				TextColor = UIColor.Black,
				Font = UIFont.FromName("Apple SD Gothic Neo", 14.0f),
				AdjustsFontSizeToFitWidth = false,
				LineBreakMode = UILineBreakMode.TailTruncation,
				Lines = 1
			};
			checkBoxText.Frame= new RectangleF (22 , -2 , 100  , 25 );
			checkBoxText.UserInteractionEnabled=true;

			checkBoxText.AddGestureRecognizer (new UITapGestureRecognizer (() => {
				if(checkBoxSelectedButton.Hidden== true)
				{
					checkBoxSelectedButton.Hidden= false;
					checkBoxUnSelectedButton.Hidden= true;
				}else{
					checkBoxSelectedButton.Hidden= true;
					checkBoxUnSelectedButton.Hidden= false;
				}
			}));


			checkBoxSelectedButton.AddGestureRecognizer (new UITapGestureRecognizer (() => {
				checkBoxSelectedButton.Hidden= true;
				checkBoxUnSelectedButton.Hidden= false;
				IsChecked= true;
			}));

			checkBoxUnSelectedButton.AddGestureRecognizer (new UITapGestureRecognizer (() => {
				checkBoxSelectedButton.Hidden= false;
				checkBoxUnSelectedButton.Hidden= true;
				IsChecked= false;
			}));
			this.AddSubview (checkBoxSelectedButton);
			this.AddSubview (checkBoxUnSelectedButton);

			this.AddSubview (checkBoxText);
		}
	}
}

