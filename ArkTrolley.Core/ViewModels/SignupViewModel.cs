using System;
using ArkTrolley.Core.Models;
using System.Threading.Tasks;
using ArkTrolley.WebService.Models.PostModel;
using ArkTrolley.Core.AppHelper;

namespace ArkTrolley.Core.ViewModels
{
	public class SignupViewModel:BaseViewModel
	{
		private PostUserData  currentUserData;
		public PostUserData CurrentUserData
		{ 
			get { return currentUserData; }
			set { currentUserData = value; RaisePropertyChanged(() => CurrentUserData); }
		}

		public SignupViewModel ()
		{
			currentUserData = new PostUserData ();
		}


		public async Task<bool> SignUpUser(string password, string confirmpassword)
		{
			IsProgessRingVisible = true;
			if (string.IsNullOrEmpty (password)) {
				await AppMessageDialog.ShowAppDialog ("Enter Valid password!", "Invalid Password");
			} else if (password == confirmpassword) {
				var id = 0;
				if (SharedData.CurrentLoginedUser != null)
					id = Convert.ToInt32 (SharedData.CurrentLoginedUser.int_id);
				var response = await ClientHandler.SetUserData (id, currentUserData);
				await AppMessageDialog.ShowAppDialog (response.responseMessage, "ArkTrolley");	
			} else {
				await AppMessageDialog.ShowAppDialog ("Password and reset password not matched", "Invalid Password");	
			}
			IsProgessRingVisible = false;
			return true;
		}
	}
}

