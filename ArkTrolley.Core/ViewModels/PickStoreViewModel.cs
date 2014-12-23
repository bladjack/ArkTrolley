using System;
using Cirrious.MvvmCross.ViewModels;
using System.Collections.Generic;
using ArkTrolley.Core.Models;
using ArkTrolley.WebService.Models;
using ArkTrolley.Core.AppHelper;

namespace ArkTrolley.Core.ViewModels
{
	public class PickStoreViewModel:BaseViewModel
	{
		private List<StoreDataItem> storelist;

		public List<StoreDataItem>  Storelist
		{
			get{ return storelist;}
			set{ 
				storelist = value;
				RaisePropertyChanged(() => Storelist); 
			}
		}

		public PickStoreViewModel ()
		{
			storelist = new List<StoreDataItem> ();
			UpdateStoreData ();
		}



		public async void UpdateStoreData ()
		{			IsProgessRingVisible = true;
			try {
				if (Coordinates == null) {
					Coordinates = new Cirrious.MvvmCross.Plugins.Location.MvxCoordinates ();
					Coordinates.Latitude = -35.237418300000;
					Coordinates.Longitude = 149.067644300000;
				}
				var response = await ClientHandler.GetStoreData ((float)Coordinates.Latitude,(float) Coordinates.Longitude, 1);

				if (response.responseCode == 1) {
					Storelist = MapperTodatabaseModel (response.responseData);
				}
			} catch {
			}
			IsProgessRingVisible = false;
		}

		private List<StoreDataItem> MapperTodatabaseModel(List<StoreItem> stores)
		{
			var items = new List<StoreDataItem> ();

			foreach (var item in stores) {
				var store = new StoreDataItem();
				store.dte_register = item.dte_register;
				store.enm_type = item.enm_type;
				store.int_active = item.int_active;
				store.int_chain = item.int_chain;
				store.int_id = item.int_id;
				store.str_address = item.str_address;
				store.str_lat = item.str_lat;
				store.str_lon = item.str_lon;
				store.str_name = item.str_name;

				items.Add (store);
			}
			return items;
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

}

