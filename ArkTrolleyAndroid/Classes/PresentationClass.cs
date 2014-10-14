using System;
using ArkTrolleyCore;
using Android.Widget;
using Android.App;

namespace ArkTrolleyAndroid
{
	public class PresentationClass
	{
		public Activity activity;
		public PresentationClass (Activity _activity)
		{
			this.activity = _activity;
		}

		public void buildUsersMenu(LocalUserClass[] arr_LocalUsers)
		{
			foreach(LocalUserClass user in arr_LocalUsers){
				Console.WriteLine (user.int_id + " " + user.str_name);
			}
		/*
			LinearLayout layout = (LinearLayout) this.activity.FindViewById(Resource.Id.lstAccountsMenuButtons);
			Button btnTag = new Button(this);
			btnTag.setLayoutParams(new Android.Widget.LinearLayout.LayoutParams(LayoutParams.WRAP_CONTENT, LayoutParams.WRAP_CONTENT));
			btnTag.setText ("Button");
			btnTag.setId(some_random_id);

			//add button to the layout
			layout.addView(btnTag);
		*/
		}
	}
}

