using System;

namespace ArkTrolley.iOS.Core.ViewModels
{
	public class PickStoreViewModel: ArkTrolley.Core.ViewModels.PickStoreViewModel
	{
		public PickStoreViewModel ()
		{
		}


		public void RefreshData()
		{
			UpdateStoreData ();
		}

		public void GoBack()
		{
			this.Close (this);
		}

		public void NavigatedToProfilePage()				
		{
			ShowViewModel<ProfileViewModel> ();
		}	

		public async void NavigatedToLoginPage()
		{
			if (await Logout ()) {
				this.ShowViewModel<MainViewModel> ();
			}
		}
	}
}

