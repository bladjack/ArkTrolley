using System;

namespace ArkTrolley.Core.ViewModels
{
	public class AppWebViewModel: BaseViewModel
	{
		private string url;
		public string Url {
			get{ return url; }
			set {
				url = value;
				RaisePropertyChanged (() => Url);
			}
		}

		public AppWebViewModel ()
		{
		}

		public async void Init(string  parameters)
		{
			Url = "http://198.38.82.51/~adminat/index.php/";
		}
	}
}

