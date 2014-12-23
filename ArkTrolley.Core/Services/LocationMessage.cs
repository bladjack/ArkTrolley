using System;
using Cirrious.MvvmCross.Plugins.Messenger;
using Cirrious.MvvmCross.Plugins.Location;

namespace ArkTrolley.Core.Services
{
	public class LocationMessage
		: MvxMessage
	{
		public MvxCoordinates Coordinates { get; private set;}
		public MvxGeoLocation GeoLocation { get; private set; }
		public LocationMessage(object sender, MvxGeoLocation location) 
			: base(sender)
		{
			Coordinates = new MvxCoordinates ();
			GeoLocation = location;
			Coordinates= location.Coordinates;
		}
	}
}

