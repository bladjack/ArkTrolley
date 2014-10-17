using System;
using MonoTouch.UIKit;
using MonoTouch.Foundation;

namespace ArkTrolley.iOS.Core.AppHelper
{
	public static class BytesToImageConverter
	{

		public static UIImage ToImage(this byte[] data)
		{
			if (data==null) {
				return null;
			}
			UIImage image = null;
			try {

				image = new UIImage(NSData.FromArray(data));
				data = null;
			} catch (Exception ) {
				return null;
			}
			return image;
		}
		public static byte[] ToNSData(this UIImage image){

			if (image == null) {
				return null;
			}
			NSData data = null;

			try {
				data = image.AsPNG();
				return data.ToArray ();
			} catch (Exception ) {
				return null;
			}
			finally
			{
				if (image != null) {
					image.Dispose ();
					image = null;
				}
				if (data != null) {
					data.Dispose ();
					data = null;
				}
			}
		}
	}
}

