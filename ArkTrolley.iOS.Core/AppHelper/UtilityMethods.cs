using System;
using MonoTouch.UIKit;
using System.Threading.Tasks;
using System.Net.Http;
using MonoTouch.Foundation;

namespace ArkTrolley.iOS.Core.AppHelper
{
	public class UtilityMethods
	{
		public UtilityMethods ()
		{
		}

		public static async Task<UIImage> LoadImage (string imageUrl)
		{
			try {
				var httpClient = new HttpClient ();

				Task<byte[]> contentsTask = httpClient.GetByteArrayAsync (imageUrl);

				// await! control returns to the caller and the task continues to run on another thread
				var contents = await contentsTask;

				// load from bytes
				return UIImage.LoadFromData (NSData.FromArray (contents));
			} catch {
				return null;
			}
		}

		public static byte[] ToNSData( UIImage image)
		{

			if (image == null)
			{
				return null;
			}
			NSData data = null;

			try
			{
				data = image.AsPNG();
				return data.ToArray();
			}
			catch (Exception)
			{
				return null;
			}
			finally
			{
				image.Dispose();
				if (data != null)
				{
					data.Dispose();
				}
			}
		}
	}
}

