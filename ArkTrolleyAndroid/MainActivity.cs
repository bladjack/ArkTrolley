using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Animation;
using Android.Views.Animations;
using Android.Util;
using Android.Content.PM;

namespace ArkTrolleyAndroid
{
	[Activity (Label = "The Ark Trolley for Android: Loading", MainLauncher = true, Icon = "@drawable/arkIcon", ScreenOrientation = ScreenOrientation.Portrait)]
	public class MainActivity : Activity
	{//The back button will kill the current activity, but the home button only change the state of the current activity and send it to the background.

		protected override void OnCreate (Bundle bundle)
		{ //Method for Startup initializations: 
			//Creating views, Initializing variables & Binding static data to lists among others.
			//bundle param contains the state between activities or it is null if just starting the app.

			base.OnCreate (bundle);
			Log.Debug (GetType().FullName, "Log Debug: MainActivity OnCreate");
			//ActionBar bar = getActionBar();    
			//bar.hide();
			//RequestWindowFeature(Window.FEATURE_NO_TITLE);
			// Set our view from the "LoadingScreen" layout resource
			SetContentView (Resource.Layout.LoadingScreen);

		}

		protected override void OnRestoreInstanceState(Bundle savedState)
		{//This is called after the OnCreate method is finished, and provides another opportunity for an Activity to restore its state after initialization is complete.
			Log.Debug (GetType().FullName, "Log Debug: MainActivity Log Debug: OnRestoreInstanceState");
		}

		protected override void  OnStart ()
		{//Method to perform any specific tasks right before an activity becomes visible such as refreshing current values of views within the activity
			base.OnStart();
			Log.Debug (GetType().FullName, "Log Debug: MainActivity OnStart");
		}

		protected override void OnResume()
		{//The system calls this method when the Activity is ready to start interacting with the user
			//Ramping up frame rates (a common task in game building)
			//Starting animations, Listening for GPS updates
			//Display any relevant alerts or dialogs or Wire up external event handlers
			//any operation that is done in OnPause should be un-done in OnResume

			base.OnResume(); // Always call the superclass first.
			Log.Debug (GetType().FullName, "Log Debug: MainActivity OnResume");
			var metrics = Resources.DisplayMetrics;

			FrameLayout whiteSea = FindViewById<FrameLayout> (Resource.Id.WhiteSea);
			ImageView tide = FindViewById<ImageView> (Resource.Id.Tide);
			whiteSea.SetMinimumHeight(30);
			tide.SetX(metrics.WidthPixels - tide.Width);

			//start the animations
			ObjectAnimator whitesea_animator = ObjectAnimator.OfInt(whiteSea, "minimumHeight", new int[] {30, metrics.HeightPixels - 120} );
			whitesea_animator.SetDuration(3000);

			ObjectAnimator tide_animator = ObjectAnimator.OfFloat(tide, "x", new float[] {-(tide.Width - metrics.WidthPixels), 1f});
			tide_animator.SetDuration(3000);

			AnimatorSet animationSet = new AnimatorSet ();
			animationSet.SetInterpolator (new AccelerateDecelerateInterpolator ());
			animationSet.Play (tide_animator).With(whitesea_animator);
			animationSet.AnimationEnd += (sender, e) => {
				StartActivity (typeof(AccountsMenuScreen));
			};
			animationSet.Start ();
			//whiteSea.SetMinimumHeight(metrics.HeightPixels - 120);
		}

		protected override void OnPause()
		{//Called when the system is about to put the activity into the background or when the activity becomes partially obscured
			//Commit unsaved changes to persistent data
			//Destroy or clean up other objects consuming resources
			//Ramp down frame rates and pausing animations
			//Unregister external event handlers or notification handlers
			//Likewise, if the Activity has displayed any dialogs or alerts, they must be cleaned up with the .Dismiss() method.
			//from this activity state the app can only go to:
			//1.- OnResume will be called if the Activity is to be returned to the foreground.
			//2.- OnStop will be called if the Activity is being placed in the background.
			base.OnPause(); // Always call the superclass first.
			Log.Debug (GetType().FullName, "Log Debug: MainActivity OnPause");
		}

		protected override void OnStop()
		{//This method is called when the activity is no longer visible to the user.
			//A new activity is being started and is covering up this activity.
			//An existing activity is being brought to the foreground.
			//The activity is being destroyed.
			//OnStop may not always be called in low-memory situations, for app destruction is better to relay on OnPause method
			base.OnStop(); // Always call the superclass first.
			Log.Debug (GetType().FullName, "Log Debug: MainActivity OnStop");
		}

		protected override void OnRestart()
		{//This method is called after your activity has been stopped, prior to it being started again.
			//There are no general guidelines for what kind of logic should be implemented in OnRestart. 
			//This is because OnStart is always invoked regardless of whether the Activity is being created or being restarted, 
			//so any resources required by the Activity should be initialized in OnStart, rather than OnRestart.
			//The next lifecycle method called after OnRestart will be OnStart.
			base.OnRestart(); // Always call the superclass first.
			Log.Debug (GetType().FullName, "Log Debug: MainActivity OnRestart");
		}

		protected override void OnSaveInstanceState (Bundle outState)
		{//This is invoked by Android when the activity is being destroyed. Activities can implement this method if they need to persist any key/value state items.
			base.OnSaveInstanceState(outState); // Always call the superclass first.
			Log.Debug (GetType().FullName, "Log Debug: MainActivity OnSaveInstanceState");
		}

		protected override void OnDestroy()
		{//This is the final method that is called on an Activity instance before it’s destroyed and completely removed from memory.
			//but Android could kill the app without destroying the current Activity, so this method is not always called.
			//almost all cleaning is done in the OnPause method.
			//A new activity is being started and is covering up this activity.
			//An existing activity is being brought to the foreground.
			//The activity is being destroyed.
			//OnStop may not always be called in low-memory situations, for app destruction is better to relay on OnDestroy method
			base.OnDestroy(); // Always call the superclass first.
			Log.Debug (GetType().FullName, "Log Debug: MainActivity OnDestroy");
		}
	}
}