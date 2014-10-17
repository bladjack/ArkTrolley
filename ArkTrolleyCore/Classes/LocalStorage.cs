using System;
using SQLite;
using System.IO;
using System.Collections;

namespace ArkTrolleyCore
{
	public class LocalStorage
	{
		public LocalStorage ()
		{
		}

		public static string DatabaseFilePath {
			get { 
				var filename = "arkTrolleyDB.sdb3";
				#if SILVERLIGHT
					var path = filename;
				#else
					#if __ANDROID__
						string libraryPath = Environment.GetFolderPath (Environment.SpecialFolder.Personal);
						;
					#else
						// we need to put in /Library/ on iOS5.1 to meet Apple's iCloud terms
						// (they don't want non-user-generated data in Documents)
						string documentsPath = Environment.GetFolderPath (Environment.SpecialFolder.Personal); // Documents folder
						string libraryPath = Path.Combine (documentsPath, "..", "Library");
					#endif
					var path = Path.Combine (libraryPath, filename);
				#endif
				return path;
			}
		}

		public LocalUserClass[] getLocalUsers()
		{

			//var db = new SQLiteConnection (DatabaseFilePath);
			//var localUsers = db.Table<LocalUserClass>();

			LocalUserClass[] arr_LocalUsers = new LocalUserClass[]{};

			LocalUserClass obj_User = new LocalUserClass();
			obj_User.dte_last_login = new DateTime(2014, 10, 17);
			obj_User.int_id = 1;
			obj_User.str_email = "bladjack@hotmail.com";
			obj_User.str_name = "Jose Luna";
			arr_LocalUsers.SetValue(obj_User, arr_LocalUsers.Length);

			obj_User = new LocalUserClass();
			obj_User.dte_last_login = new DateTime(2014, 10, 5);
			obj_User.int_id = 2;
			obj_User.str_email = "jluna@blad-net.com";
			obj_User.str_name = "LCC Jose Luna";
			arr_LocalUsers.SetValue(obj_User, arr_LocalUsers.Length);

			obj_User = new LocalUserClass();
			obj_User.dte_last_login = new DateTime(2014, 9, 20);
			obj_User.int_id = 3;
			obj_User.str_email = "bladgack@gmail.com";
			obj_User.str_name = "Jose L. Luna E.";
			arr_LocalUsers.SetValue(obj_User, arr_LocalUsers.Length);

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

