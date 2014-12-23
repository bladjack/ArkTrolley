using System;

namespace ArkTrolley.iOS.Core.ViewModels
{
	public class LoginViewModel: ArkTrolley.Core.ViewModels.LoginViewModel
	{
		public LoginViewModel ()
		{
		}

		public async void NavigatedToPickerView()
		{
			var status = await UserLogin ();
			if (status == null) {
				//UserEmail = string.Empty;
				UserPassword = string.Empty;
				await AppMessageDialog.ShowAppDialog ("Invalid ID or password.", "Alert");
			} else if (status.responseCode == 1) {
				this.ShowViewModel<PickStoreViewModel> ();
			} else {
				//UserEmail = string.Empty;
				UserPassword = string.Empty;
				await AppMessageDialog.ShowAppDialog (status.responseMessage, "Alert");
			}
		}
	}
}

