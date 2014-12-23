using System;
using Cirrious.MvvmCross.ViewModels;
using ArkTrolley.Core.Models;
using ArkTrolley.WebService.Models;
using System.Threading.Tasks;
using ArkTrolley.Core.AppHelper;
using System.Linq;

namespace ArkTrolley.Core.ViewModels
{
	public  class LoginViewModel:BaseViewModel
	{
		private string  userName;
		public string UserName
		{ 
			get { return userName; }
			set { userName = value; RaisePropertyChanged(() => UserName); }
		}

		private string  userEmail;
		public string UserEmail
		{ 
			get { return userEmail; }
			set { userEmail = value; RaisePropertyChanged(() => UserEmail); }
		}

		private string  userId;
		public string UserId
		{ 
			get { return userId; }
			set { userId = value; RaisePropertyChanged(() => UserId); }
		}

		private string  userPassword;
		public string UserPassword
		{ 
			get { return userPassword; }
			set { userPassword = value; RaisePropertyChanged(() => UserPassword); }
		}

		private bool  rememberMe;
		public bool RememberMe
		{ 
			get { return rememberMe; }
			set { rememberMe = value; RaisePropertyChanged(() => RememberMe); }
		}

		public LoginViewModel ()
		{
			RememberMe = true;
		}

		public async void Init(UserData parameters)
		{
			if (SharedData.CurrentLoginedUser != null) {
				UserName = SharedData.CurrentLoginedUser.str_name;
				UserEmail = SharedData.CurrentLoginedUser.str_email;
				UserId = SharedData.CurrentLoginedUser.int_id;
			}
		}

		public void GoBack()
		{
			this.Close (this);
		}

		protected async Task<UserDataModel> UserLogin()
		{
			if (string.IsNullOrEmpty (UserEmail) || !UtilityMethods.IsValidEmail (UserEmail)) {
				await AppMessageDialog.ShowAppDialog ("Invalid Email !", "Alert");
			} else if (string.IsNullOrEmpty (UserPassword)) {
				await AppMessageDialog.ShowAppDialog ("Enter Password", "Alert");
			} else {

				IsProgessRingVisible = true;
				try {
					var response = await ClientHandler.GetUserData (UserEmail.Replace ("@", "%40"), UserPassword);
					if (response.responseCode == 1) {
						if (RememberMe) {
							AddToDataBase (response.responseData);
						}
						SharedData.CurrentSelectedStore = new StoreDataItem ();
						SharedData.CurrentLoginedUser = MapperToLocalDataBase (response.responseData);
						IsProgessRingVisible = false;
						return response;
					} 
					return response;
				} catch (Exception ex) {
				}
				IsProgessRingVisible = false;
			}
			return null;
		}

		private void AddToDataBase(UserDataItem item )
		{
			var response=StorageHelper.All<UserData> ().FirstOrDefault (x => x.str_email==item.str_email);
			if (response == null) {
				StorageHelper.Add<UserData> (MapperToLocalDataBase (item));
			}
		}

		private UserData MapperToLocalDataBase(UserDataItem item )
		{
			UserData userdata = new UserData ();
			userdata.dte_last_login = item.dte_last_login;
			userdata.dte_register = item.dte_register;
			userdata.enm_type = item.enm_type;
			userdata.int_active = item.int_active;
			userdata.int_id = item.int_id;
			userdata.int_points = item.int_points;
			userdata.str_address = item.str_address;
			userdata.str_email = item.str_email;
			userdata.str_name = item.str_name;
			return userdata;
		}

		public async void ResetPassword()
		{
			IsProgessRingVisible = true;
			if (string.IsNullOrEmpty (UserEmail)) {
				await AppMessageDialog.ShowAppDialog ("Please enter your email id.", "ArkTrolley");

			} else {
				var response3 = await ClientHandler.GetUserNewPassword (UserEmail.Replace ("@", "%40"));
				if (response3 == null) {
					await AppMessageDialog.ShowAppDialog ("We unable to reset password, please try later", "ArkTrolley");
				} else {
					await AppMessageDialog.ShowAppDialog (response3.responseMessage, "Password !");
				} 
			}
			IsProgessRingVisible = false;
		}
	}
}

