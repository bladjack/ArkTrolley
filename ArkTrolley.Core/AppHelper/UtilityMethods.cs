using System;
using System.Text.RegularExpressions;

namespace ArkTrolley.Core.AppHelper
{
	public class UtilityMethods
	{
		public static bool IsValidEmail(string strIn)
		{
			// Return true if strIn is in valid e-mail format. 
			try {
				return Regex.IsMatch(strIn,
					@"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
					@"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$",
					RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
			}
			catch (RegexMatchTimeoutException) {
				return false;
			}
		}

		public static bool IsNumeric(string input) {
			double test;
			return double.TryParse(input, out test);
		}


	}
}

