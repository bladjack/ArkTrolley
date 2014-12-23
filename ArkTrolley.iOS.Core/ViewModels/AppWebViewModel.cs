using System;

namespace ArkTrolley.iOS.Core.ViewModels
{
	public class AppWebViewModel:ArkTrolley.Core.ViewModels.AppWebViewModel
	{
		public AppWebViewModel ()
		{
		}

		public void GoBack()
		{
			this.Close (this);
		}
	}
}

