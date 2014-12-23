using System;
using Cirrious.MvvmCross.ViewModels;
using System.Collections.Generic;
using ArkTrolley.Core.Models;
using System.Linq;

namespace ArkTrolley.Core.ViewModels
{
	public class MainViewModel:BaseViewModel
	{
		private List<UserData> previousLoginUser;

		public List<UserData>  PreviousLoginUser
		{
			get{ return previousLoginUser;}
			set{ 
				previousLoginUser = value;
				RaisePropertyChanged(() => PreviousLoginUser); 
			}
		}

		public MainViewModel ()
		{
			PreviousLoginUser = new List<UserData> ();

			GetLocalUsers ();
		}




		public void GetLocalUsers()
		{
			try{
				PreviousLoginUser= StorageHelper.All<UserData> ();
//				var item= StorageHelper.All<UserData> ().FirstOrDefault();
//				var temList= new List<UserData>();
//				temList.Add(item);
//				PreviousLoginUser= temList;


			}catch{
			}
		}
	}


	public class LocalUserClass
	{
		public int int_id { get; set; }
		public DateTime dte_last_login { get; set; }
		public string str_name { get; set; }
		public string str_email { get; set; }
		public string str_password{ get; set;}

		public LocalUserClass ()
		{
		}
	}
}

