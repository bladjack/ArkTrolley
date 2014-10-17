using System;
using System.Windows.Input;
using MonoTouch.UIKit;

namespace ArkTrolley.iOS.Core.UserControls
{
	public class CustomTapped
	{
		public ICommand TappedCommand { get;set; }

		public CustomTapped(UIView view)
		{
			var tap = new UITapGestureRecognizer(() => 
				{
					var command = TappedCommand;
					if (command != null)
						command.Execute(null);
				});
			view.AddGestureRecognizer(tap);
		}
	}

	public static class BehaviourExtensions
	{
		public static CustomTapped Tapped(this UIView view)
		{
			return new CustomTapped(view);
		}
	}
}

