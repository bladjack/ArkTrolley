using System;
using Cirrious.MvvmCross.Plugins.Location;
using Cirrious.MvvmCross.Plugins.Messenger;
using Cirrious.CrossCore;

namespace ArkTrolley.Core.Services
{
	public class LocationService :ILocationService
	{
		private readonly IMvxGeoLocationWatcher _watcher;
		private readonly IMvxMessenger _messenger;

		public LocationService(IMvxGeoLocationWatcher watcher, IMvxMessenger messenger)
		{
			_watcher = watcher;
			_messenger = messenger;
			_watcher.Start(new MvxGeoLocationOptions(), OnLocation, OnError);

		}

		private void OnLocation(MvxGeoLocation location)
		{
			var message = new LocationMessage(this,location);

			_messenger.Publish(message);
		}

		private void OnError(MvxLocationError error)
		{
			Mvx.Error("Seen location error {0}", error.Code);
		}

	}
}

