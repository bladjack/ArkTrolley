using System;

namespace ArkTrolley.iOS.Core.ViewModels
{
	public class ProfileViewModel:ArkTrolley.Core.ViewModels.ProfileViewModel
	{
		public ProfileViewModel ()
		{
		}

		public void GoBack()
		{
			this.Close (this);
		}

		public async void NavigatedToLoginPage()
		{
			if (await Logout ()) {
				this.ShowViewModel<MainViewModel> ();
			}
		}
	}
}

