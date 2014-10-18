using System;
using Cirrious.MvvmCross.ViewModels;
using System.Collections.Generic;

namespace ArkTrolley.Core.ViewModels
{
	public class MainViewModel:MvxViewModel
	{
		private List<LocalUserClass> previousLoginUser;

		public List<LocalUserClass>  PreviousLoginUser
		{
			get{ return previousLoginUser;}
			set{ 
				previousLoginUser = value;
				RaisePropertyChanged(() => PreviousLoginUser); 
			}
		}

		public MainViewModel ()
		{
			PreviousLoginUser = new List<LocalUserClass> ();

			PreviousLoginUser = getLocalUsers ();
		}

		public List<LocalUserClass> getLocalUsers()
		{

			//var db = new SQLiteConnection (DatabaseFilePath);
			//var localUsers = db.Table<LocalUserClass>();

			List<LocalUserClass> arr_LocalUsers = new List<LocalUserClass> (); 

			LocalUserClass obj_User = new LocalUserClass();
			obj_User.dte_last_login = new DateTime(2014, 10, 17);
			obj_User.int_id = 1;
			obj_User.str_email = "bladjack@hotmail.com";
			obj_User.str_name = "Jose Luna";

			arr_LocalUsers.Add(obj_User);

			obj_User = new LocalUserClass();
			obj_User.dte_last_login = new DateTime(2014, 10, 5);
			obj_User.int_id = 2;
			obj_User.str_email = "jluna@blad-net.com";
			obj_User.str_name = "LCC Jose Luna";
			arr_LocalUsers.Add(obj_User);

			obj_User = new LocalUserClass();
			obj_User.dte_last_login = new DateTime(2014, 9, 20);
			obj_User.int_id = 3;
			obj_User.str_email = "bladgack@gmail.com";
			obj_User.str_name = "Jose L. Luna E.";
			arr_LocalUsers.Add(obj_User);

			obj_User = new LocalUserClass();
			obj_User.dte_last_login = new DateTime(2014, 9, 20);
			obj_User.int_id = 4;
			obj_User.str_email = "hakim@gmail.com";
			obj_User.str_name = "Hakim Ali";
			arr_LocalUsers.Add(obj_User);

			obj_User = new LocalUserClass();
			obj_User.dte_last_login = new DateTime(2014, 9, 20);
			obj_User.int_id = 4;
			obj_User.str_email = "hakim@gmail.com";
			obj_User.str_name = "Hakim Ali";
			arr_LocalUsers.Add(obj_User);

			obj_User = new LocalUserClass();
			obj_User.dte_last_login = new DateTime(2014, 9, 20);
			obj_User.int_id = 4;
			obj_User.str_email = "hakim@gmail.com";
			obj_User.str_name = "Hakim Ali";
			arr_LocalUsers.Add(obj_User);


			/*
			foreach (var localUser in localUsers){
				LocalUserClass obj_User = new LocalUserClass();
				obj_User.dte_last_login = localUser.dte_last_login;
				obj_User.int_id = localUser.int_id;
				obj_User.str_email = localUser.str_email;
				obj_User.str_name = localUser.str_name;
				arr_LocalUsers.SetValue(obj_User, arr_LocalUsers.Length);
			}
			*/
			return arr_LocalUsers;
		}



	}


	public class LocalUserClass
	{
		public int int_id { get; set; }
		public DateTime dte_last_login { get; set; }
		public string str_name { get; set; }
		public string str_email { get; set; }

		public LocalUserClass ()
		{
		}
	}
}

