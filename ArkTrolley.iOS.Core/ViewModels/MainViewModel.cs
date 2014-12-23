using System;
using ArkTrolley.Core.ViewModels;
using ArkTrolley.Core.Models;
using ArkTrolley.Core.AppHelper;
using ZXing.Mobile;

namespace ArkTrolley.iOS.Core.ViewModels
{
	public class MainViewModel: ArkTrolley.Core.ViewModels.MainViewModel
	{
		public MainViewModel ()
		{

		}

		public void NavigateToLoginScreenForNewUser()
		{
		}

		public void NavigateToLoginScreen()
		{
			UserData parameters = new UserData ();
			parameters.str_name="Add New User";
			parameters.str_email = string.Empty;
			SharedData.CurrentLoginedUser = parameters;
			this.ShowViewModel<LoginViewModel>(parameters);
		}

		public void NavigatedToStorePickList(UserData parameters)
		{
			SharedData.CurrentLoginedUser = parameters;
			this.ShowViewModel<LoginViewModel>(parameters);
			//this.ShowViewModel<PickStoreViewModel> (parameter);
		}

		public void NavigatedSignUp()
		{
			//BarCodeScanner ();
			this.ShowViewModel<SignupViewModel> ();

		}

		public void NavigatedAppWebView()
		{
			string parameters = SharedData.CurrentConfigurationData.str_web_service_root;
			this.ShowViewModel<AppWebViewModel> (parameters);
		}

		public async void BarCodeScanner()
		{
			var scanner = new MobileBarcodeScanner();
			var result = await scanner.Scan();

			if (result != null)
				Console.WriteLine("Scanned Barcode: " + result.Text);
		}
	}
}

