using System;
using ArkTrolley.Core.Models;
using ArkTrolley.WebService;

namespace ArkTrolley.Core.AppHelper
{
	public class SharedData
	{
		public static UserData CurrentLoginedUser{ get; set;}
		public static StoreDataItem CurrentSelectedStore{ get; set;}
		public static ConfigurationData CurrentConfigurationData { get; set;}
	}
}

