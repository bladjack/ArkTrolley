using Cirrious.MvvmCross.ViewModels;
using ArkTrolley.WebService.Services;
using ArkTrolley.Core.AppHelper;
using System.Linq;

namespace ArkTrolley.Core.ViewModels
{
    public class FirstViewModel 
		: BaseViewModel
    {
		private string _hello = "Hello MvvmCross";
        public string Hello
		{ 
			get { return _hello; }
			set { _hello = value; RaisePropertyChanged(() => Hello); }
		}
		IDeviceInfoService deviceInfo;
		public FirstViewModel(IDeviceInfoService deviceInfo	)	{
			this.deviceInfo = deviceInfo;
			GetSampleData ();
		}

		private async void GetSampleData()
		{
			string alertMessage = "There is issue in services.";
			try {
				var response = await ClientHandler.ServerConfigManagement (ArkTrolley.WebService.DeviceSize.regular,
					              ArkTrolley.WebService.DeviceType.smarthphone, deviceInfo.DeviceModel);
				if (response.responseCode == 1) {
					SharedData.CurrentConfigurationData = response.responseData;
				} else {
					await AppMessageDialog.ShowAppDialog (response.responseMessage, "Alert");
				}
				alertMessage= response.responseMessage;
			} catch {
				AppMessageDialog.ShowAppDialog (alertMessage, "Alert");
			}

			//var response2 = await ClientHandler.GetUserData ("bladgack%40gmail.com", "1e68934c4b07f48d");
//
//			var res = await ClientHandler.GetUserNewPassword ("hakimalisami%40gmail.com");
//
//			var response4 = await ClientHandler.GetItemData ("300675033848", 1, 1);
//			var response1 = await ClientHandler.GetStoreData (153.130998, -27.633284, 1);
			//	var response3 = await ClientHandler.GetUserNewPassword (1);
		}

    }
}
