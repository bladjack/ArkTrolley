using System;
using Cirrious.MvvmCross.ViewModels;
using ArkTrolley.WebService.Services;
using Cirrious.CrossCore;
using ArkTrolley.Core.AppHelper;
using Cirrious.MvvmCross.Plugins.Messenger;
using ArkTrolley.Core.Services;
using Cirrious.MvvmCross.Plugins.Location;
using System.Threading.Tasks;

namespace ArkTrolley.Core.ViewModels
{
	public class BaseViewModel:MvxViewModel
	{
		public IClientHandler ClientHandler{ get; set;}
		public IStorageService StorageHelper { get; set;}
		public IAppMessageDialog AppMessageDialog { get;set;}
		public MvxCoordinates Coordinates { get; set;}
		private readonly MvxSubscriptionToken token;

		private bool isProgessRingVisible;

		public bool IsProgessRingVisible {
			get{ return isProgessRingVisible; }
			set {
				isProgessRingVisible = value;
				RaisePropertyChanged (() => IsProgessRingVisible);
			}
		}


		public BaseViewModel ()
		{
			this.ClientHandler = Mvx.Resolve<IClientHandler>();
			this.StorageHelper= Mvx.Resolve<IStorageService>();
			this.AppMessageDialog = Mvx.Resolve<IAppMessageDialog> ();
			var messenger = Mvx.Resolve<IMvxMessenger> ();
			token = messenger.Subscribe<LocationMessage>(OnLocationMessage);
		}

		private void OnLocationMessage(LocationMessage locationMessage)
		{
			Coordinates = locationMessage.Coordinates;
		}

		public async Task<bool> Logout()
		{
			IsProgessRingVisible = true;
			var response3 = await ClientHandler.Logout (SharedData.CurrentLoginedUser.str_email.Replace ("@", "%40"));
			if (response3 == null) {
				await AppMessageDialog.ShowAppDialog (response3.responseMessage, "ArkTrolley");
				IsProgessRingVisible = false;
				return false;
			} else {
				SharedData.CurrentLoginedUser = null;

			} 
			IsProgessRingVisible = false;
			return true;
		}
	}
}

