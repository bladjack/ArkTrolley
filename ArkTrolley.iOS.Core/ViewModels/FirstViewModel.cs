using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using MonoTouch.Foundation;
using MonoTouch.UIKit;
using ArkTrolley.WebService.Services;

namespace ArkTrolley.iOS.Core.ViewModels
{
    public class FirstViewModel: ArkTrolley.Core.ViewModels.FirstViewModel
    {

		public FirstViewModel(ArkTrolley.Core.AppHelper.IDeviceInfoService deviceInfo): base(deviceInfo)
		{
		}

		public void NavigatToMainScreen()
		{
			ShowViewModel<MainViewModel> ();
		}
    }


}