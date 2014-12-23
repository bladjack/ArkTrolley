using System;

namespace ArkTrolley.WebService.HttpClientHandler
{
	public class DataBytesDataObject : BaseDataObject
	{
		public byte[] DataBytes { get; set; }
		public string DataString { get; set; }

	}

	public class BaseDataObject
	{
		public ResponseDataObject Response { get; set; }
	}

	/// <summary>
	/// Response Object to give proper information of any process or service success or failure with error code and reason.
	/// </summary>
	public class ResponseDataObject
	{
		/// <summary>
		/// Set true if process or service is succeeded, else false.
		/// </summary>
		public bool IsSucceeded;

		/// <summary>
		/// If there is any error then its code must be placed in this field.
		/// </summary>
		public int ErrorNo;

		/// <summary>
		/// If there is any error then its reason must be placed in this field.
		/// </summary>
		public string ErrorReason;
	}
}

