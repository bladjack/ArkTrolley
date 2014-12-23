using System;
using MonoTouch.Foundation;
using MonoTouch.Security;
using System.Runtime.InteropServices;
using MonoTouch.UIKit;

namespace ArkTrolley.iOS.Core.AppHelper
{
	public class DeviceInfoService: ArkTrolley.Core.AppHelper.IDeviceInfoService
	{
		public DeviceInfoService ()
		{
		}

		public string DeviceModel {
			get {
				return UIDevice.CurrentDevice.Model;
			}
		}

		public string AppVersion {
			get {
				try {
					return NSBundle.MainBundle.InfoDictionary ["CFBundleVersion"].ToString();
				} catch {
					return null;
				}
			}
		}

		public string UniqueID {
			get {
				var query = new SecRecord(SecKind.GenericPassword);
				query.Service = NSBundle.MainBundle.BundleIdentifier;
				query.Account = "UniqueID";

				NSData uniqueId = SecKeyChain.QueryAsData(query);
				if(uniqueId == null) {
					query.ValueData = NSData.FromString(System.Guid.NewGuid().ToString());
					var err = SecKeyChain.Add (query);
					if (err != SecStatusCode.Success && err != SecStatusCode.DuplicateItem)
						throw new Exception("Cannot store Unique ID");

					return query.ValueData.ToString();
				}
				else {
					return uniqueId.ToString();
				}
			}
		}
	}
}

