using System;
using ArkTrolley.Core.AppHelper.AppEnums;
using System.Threading.Tasks;

namespace ArkTrolley.Core.AppHelper
{
	public interface IAppMessageDialog
	{
		Task<DialogButton> ShowAppDialog(string message, string title, string button1Text = "OK");

		Task<DialogButton> ShowAppDialog(string message, string title, string button1Text,
			string button2Text);

		Task<DialogButton> ShowAppDialog(string message, string title, string button1Text,
			string button2Text, string button3Text);
	}
}

