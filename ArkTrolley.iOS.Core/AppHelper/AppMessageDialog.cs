using System;
using System.Threading.Tasks;
using ArkTrolley.Core.AppHelper.AppEnums;
using MonoTouch.UIKit;

namespace ArkTrolley.iOS.Core.AppHelper
{
	public class AppMessageDialog: ArkTrolley.Core.AppHelper.IAppMessageDialog
	{
		public async Task<DialogButton> ShowAppDialog(string message, string title, string button1Text = "OK")
		{
			await ShowAlert (title, message, button1Text);
			return DialogButton.Button1;
		}

		public async Task<DialogButton> ShowAppDialog(string message, string title, string button1Text,
			string button2Text){
			int clickedButtonIndex = await ShowAlert (title, message, button1Text, button2Text);
			if (clickedButtonIndex == 0)
				return DialogButton.Button1;
			else
				return DialogButton.Button2;
		}

		public async Task<DialogButton> ShowAppDialog(string message, string title, string button1Text,
			string button2Text, string button3Text){
			int clickedButtonIndex = await ShowAlert (title, message, button1Text, button2Text, button3Text);
			if (clickedButtonIndex == 0)
				return DialogButton.Button1;
			else if (clickedButtonIndex == 1)
				return DialogButton.Button2;
			else
				return DialogButton.Button3;
		}

		// Displays a UIAlertView and returns the index of the button pressed.
		private static async Task<int> ShowAlert (string title, string message, params string [] buttons)
		{
			var tcs = new TaskCompletionSource<int> ();
			var alert = new UIAlertView {
				Title = title,
				Message = message
			};
			foreach (var button in buttons)
				alert.AddButton (button);
			alert.Show ();
			alert.Clicked += (s, e) => tcs.SetResult (e.ButtonIndex);
			return await tcs.Task;
		}
	}
}

