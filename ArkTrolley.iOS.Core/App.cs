using Cirrious.CrossCore;
using MonoTouch.UIKit;
using Cirrious.CrossCore.Platform;
using Cirrious.MvvmCross.ViewModels;
using Cirrious.MvvmCross.Touch.Platform;
using System.Collections.Generic;
using Cirrious.CrossCore.IoC;
using Cirrious.MvvmCross.Plugins.Sqlite;
using ArkTrolley.Core;
using ArkTrolley.Core.Services;
using Cirrious.MvvmCross.Plugins.Location;
using Cirrious.MvvmCross.Plugins.Messenger;

namespace ArkTrolley.iOS.Core
{
    public class App : Cirrious.MvvmCross.ViewModels.MvxApplication
    {
        public override void Initialize()
        {
			CreatableTypes()
				.EndingWith("StorageHelper")
				.AsInterfaces()
				.RegisterAsLazySingleton();

            CreatableTypes()
                .EndingWith("Service")
                .AsInterfaces()
                .RegisterAsLazySingleton();

			Mvx.Resolve<ISQLiteConnectionFactory> ();
			Mvx.RegisterSingleton (typeof(IStorageService), new StorageService (Mvx.Resolve<ISQLiteConnectionFactory> ()));

			Mvx.RegisterSingleton (typeof(ArkTrolley.WebService.Services.IClientHandler), new ArkTrolley.WebService.Services.ClientHandler());
			Mvx.RegisterSingleton (typeof(ArkTrolley.Core.AppHelper.IAppMessageDialog), new ArkTrolley.iOS.Core.AppHelper.AppMessageDialog());
			Mvx.RegisterSingleton (typeof(ArkTrolley.Core.Services.ILocationService), new ArkTrolley.Core.Services.LocationService(Mvx.Resolve<IMvxGeoLocationWatcher>(),Mvx.Resolve<IMvxMessenger>()));
			Mvx.RegisterSingleton (typeof(ArkTrolley.Core.AppHelper.IDeviceInfoService), new ArkTrolley.iOS.Core.AppHelper.DeviceInfoService());


			RegisterAppStart<ViewModels.FirstViewModel>();
        }
    }
}