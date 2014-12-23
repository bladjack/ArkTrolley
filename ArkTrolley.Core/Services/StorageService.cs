using System;
using Cirrious.MvvmCross.Plugins.Sqlite;
using System.Collections.Generic;
using System.Linq;
using ArkTrolley.Core.Models;

namespace ArkTrolley.Core.Services
{
	public class StorageService:IStorageService
	{
		private readonly ISQLiteConnection _connection;

		public StorageService(ISQLiteConnectionFactory factory)
		{
			_connection = factory.Create("arktrolley.sql");
			_connection.CreateTable<UserData>();
			_connection.CreateTable<StoreDataItem>();
		}

		public List<T> All<T>() where T : new()
		{
			return _connection
				.Table<T>()
				.ToList();
		}

		public T Latest<T>() where T : new()
		{

			return _connection
				.Table<T>()
				.FirstOrDefault();
		}

		public void Add<T>(T item)
		{
			_connection.Insert(item);
		}

		public void Delete<T>(T item)
		{
			_connection.Delete(item);
		}

		public void Update<T>(T item)
		{
			_connection.Update(item);
		}

	}
}

