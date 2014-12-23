using System;
using System.Collections.Generic;

namespace ArkTrolley.Core
{
	public interface IStorageService
		{
			List<T> All<T>() where T : new();
			T Latest<T>() where T : new();
			void Add<T>(T item);
			void Delete<T>(T item);
			void Update<T>(T item);
	}
}
