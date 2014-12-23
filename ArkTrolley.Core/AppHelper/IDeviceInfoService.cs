using System;

namespace ArkTrolley.Core.AppHelper
{
	public interface IDeviceInfoService
	{
		string AppVersion{ get;}
		string UniqueID {
			get ;
		}

		string DeviceModel {
			get;
		}
	}
}

