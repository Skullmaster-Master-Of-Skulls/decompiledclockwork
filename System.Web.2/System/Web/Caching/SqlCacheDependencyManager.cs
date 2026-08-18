using System;
using System.Collections;
using System.Configuration;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Threading;
using System.Web.Configuration;
using System.Web.DataAccess;

namespace System.Web.Caching
{
	// Token: 0x02000892 RID: 2194
	internal static class SqlCacheDependencyManager
	{
		// Token: 0x06006705 RID: 26373 RVA: 0x0016B0C8 File Offset: 0x001692C8
		internal static string GetMoniterKey(string database, string table)
		{
			if (database.IndexOf(':') != -1)
			{
				database = database.Replace(":", "\\:");
			}
			if (table.IndexOf(':') != -1)
			{
				table = table.Replace(":", "\\:");
			}
			return "b" + database + ":" + table;
		}

		// Token: 0x06006706 RID: 26374 RVA: 0x0016B120 File Offset: 0x00169320
		internal static void Dispose(int waitTimeoutMs)
		{
			try
			{
				DateTime t = DateTime.UtcNow.AddMilliseconds((double)waitTimeoutMs);
				SqlCacheDependencyManager.s_shutdown = true;
				if (SqlCacheDependencyManager.s_DatabaseNotifStates != null && SqlCacheDependencyManager.s_DatabaseNotifStates.Count > 0)
				{
					Hashtable obj = SqlCacheDependencyManager.s_DatabaseNotifStates;
					lock (obj)
					{
						foreach (object obj2 in SqlCacheDependencyManager.s_DatabaseNotifStates)
						{
							object value = ((DictionaryEntry)obj2).Value;
							if (value != null)
							{
								((DatabaseNotifState)value).Dispose();
							}
						}
					}
					while (SqlCacheDependencyManager.s_activePolling != 0)
					{
						Thread.Sleep(250);
						if (!Debugger.IsAttached && DateTime.UtcNow > t)
						{
							break;
						}
					}
				}
			}
			catch
			{
			}
		}

		// Token: 0x06006707 RID: 26375 RVA: 0x0016B220 File Offset: 0x00169420
		internal static SqlCacheDependencyDatabase GetDatabaseConfig(string database)
		{
			SqlCacheDependencySection sqlCacheDependency = RuntimeConfig.GetAppConfig().SqlCacheDependency;
			object obj = sqlCacheDependency.Databases[database];
			if (obj == null)
			{
				throw new HttpException(SR.GetString("Database_not_found", new object[]
				{
					database
				}));
			}
			return (SqlCacheDependencyDatabase)obj;
		}

		// Token: 0x06006708 RID: 26376 RVA: 0x0016B268 File Offset: 0x00169468
		internal static void InitPolling(string database)
		{
			SqlCacheDependencySection sqlCacheDependency = RuntimeConfig.GetAppConfig().SqlCacheDependency;
			if (!sqlCacheDependency.Enabled)
			{
				throw new ConfigurationErrorsException(SR.GetString("Polling_not_enabled_for_sql_cache"), sqlCacheDependency.ElementInformation.Properties["enabled"].Source, sqlCacheDependency.ElementInformation.Properties["enabled"].LineNumber);
			}
			SqlCacheDependencyDatabase databaseConfig = SqlCacheDependencyManager.GetDatabaseConfig(database);
			if (databaseConfig.PollTime == 0)
			{
				throw new ConfigurationErrorsException(SR.GetString("Polltime_zero_for_database_sql_cache", new object[]
				{
					database
				}), databaseConfig.ElementInformation.Properties["pollTime"].Source, databaseConfig.ElementInformation.Properties["pollTime"].LineNumber);
			}
			if (SqlCacheDependencyManager.s_DatabaseNotifStates.ContainsKey(database))
			{
				return;
			}
			string connectionString = SqlConnectionHelper.GetConnectionString(databaseConfig.ConnectionStringName, true, true);
			if (connectionString == null || connectionString.Length < 1)
			{
				throw new ConfigurationErrorsException(SR.GetString("Connection_string_not_found", new object[]
				{
					databaseConfig.ConnectionStringName
				}), databaseConfig.ElementInformation.Properties["connectionStringName"].Source, databaseConfig.ElementInformation.Properties["connectionStringName"].LineNumber);
			}
			Hashtable obj = SqlCacheDependencyManager.s_DatabaseNotifStates;
			lock (obj)
			{
				if (!SqlCacheDependencyManager.s_DatabaseNotifStates.ContainsKey(database))
				{
					DatabaseNotifState databaseNotifState = new DatabaseNotifState(database, connectionString, databaseConfig.PollTime);
					databaseNotifState._timer = new Timer(SqlCacheDependencyManager.s_timerCallback, databaseNotifState, 0, databaseConfig.PollTime);
					SqlCacheDependencyManager.s_DatabaseNotifStates.Add(database, databaseNotifState);
				}
			}
		}

		// Token: 0x06006709 RID: 26377 RVA: 0x0016B418 File Offset: 0x00169618
		private static void PollCallback(object state)
		{
			using (new ApplicationImpersonationContext())
			{
				SqlCacheDependencyManager.PollDatabaseForChanges((DatabaseNotifState)state, true);
			}
		}

		// Token: 0x0600670A RID: 26378 RVA: 0x0016B454 File Offset: 0x00169654
		internal static void PollDatabaseForChanges(DatabaseNotifState dbState, bool fromTimer)
		{
			SqlDataReader sqlDataReader = null;
			SqlConnection sqlConnection = null;
			SqlCommand sqlCommand = null;
			CacheStoreProvider internalCache = HttpRuntime.Cache.InternalCache;
			bool flag = false;
			Exception ex = null;
			if (SqlCacheDependencyManager.s_shutdown)
			{
				return;
			}
			if (dbState._refCount == 0 && fromTimer && dbState._init)
			{
				return;
			}
			if (Interlocked.CompareExchange(ref dbState._rqInCallback, 1, 0) != 0)
			{
				if (fromTimer)
				{
					return;
				}
				HttpContext httpContext = HttpContext.Current;
				int seconds;
				if (httpContext == null)
				{
					seconds = 30;
				}
				else
				{
					seconds = Math.Max(httpContext.Timeout.Seconds / 3, 30);
				}
				DateTime t = DateTime.UtcNow.Add(new TimeSpan(0, 0, seconds));
				while (Interlocked.CompareExchange(ref dbState._rqInCallback, 1, 0) != 0)
				{
					Thread.Sleep(250);
					if (SqlCacheDependencyManager.s_shutdown)
					{
						return;
					}
					if (!Debugger.IsAttached && DateTime.UtcNow > t)
					{
						throw new HttpException(SR.GetString("Cant_connect_sql_cache_dep_database_polling", new object[]
						{
							dbState._database
						}));
					}
				}
			}
			try
			{
				try
				{
					Interlocked.Increment(ref SqlCacheDependencyManager.s_activePolling);
					dbState.GetConnection(out sqlConnection, out sqlCommand);
					sqlDataReader = sqlCommand.ExecuteReader();
					if (!SqlCacheDependencyManager.s_shutdown)
					{
						flag = true;
						Hashtable hashtable = (Hashtable)dbState._tables.Clone();
						while (sqlDataReader.Read())
						{
							string @string = sqlDataReader.GetString(0);
							int @int = sqlDataReader.GetInt32(1);
							string moniterKey = SqlCacheDependencyManager.GetMoniterKey(dbState._database, @string);
							object obj = internalCache.Get(moniterKey);
							if (obj == null)
							{
								internalCache.Add(moniterKey, @int, new CacheInsertOptions
								{
									Priority = CacheItemPriority.NotRemovable
								});
								dbState._tables.Add(@string, null);
							}
							else if (@int != (int)obj)
							{
								internalCache.Insert(moniterKey, @int, new CacheInsertOptions
								{
									Priority = CacheItemPriority.NotRemovable
								});
							}
							hashtable.Remove(@string);
						}
						foreach (object obj2 in hashtable.Keys)
						{
							dbState._tables.Remove((string)obj2);
							internalCache.Remove(SqlCacheDependencyManager.GetMoniterKey(dbState._database, (string)obj2));
						}
						if (dbState._pollSqlError != 0)
						{
							dbState._pollSqlError = 0;
						}
					}
				}
				catch (Exception ex2)
				{
					ex = ex2;
					SqlException ex3 = ex2 as SqlException;
					if (ex3 != null)
					{
						dbState._pollSqlError = ex3.Number;
					}
					else
					{
						dbState._pollSqlError = 0;
					}
				}
				finally
				{
					try
					{
						if (sqlDataReader != null)
						{
							sqlDataReader.Close();
						}
						dbState.ReleaseConnection(ref sqlConnection, ref sqlCommand, ex != null);
					}
					catch
					{
					}
					lock (dbState)
					{
						dbState._pollExpt = ex;
						if (dbState._notifEnabled && !flag && ex != null && dbState._pollSqlError == 2812)
						{
							foreach (object obj3 in dbState._tables.Keys)
							{
								try
								{
									internalCache.Remove(SqlCacheDependencyManager.GetMoniterKey(dbState._database, (string)obj3));
								}
								catch
								{
								}
							}
							dbState._tables.Clear();
						}
						dbState._notifEnabled = flag;
						dbState._utcTablesUpdated = DateTime.UtcNow;
					}
					if (!dbState._init)
					{
						dbState._init = true;
					}
					Interlocked.Decrement(ref SqlCacheDependencyManager.s_activePolling);
					Interlocked.Exchange(ref dbState._rqInCallback, 0);
				}
			}
			catch
			{
				throw;
			}
		}

		// Token: 0x0600670B RID: 26379 RVA: 0x0016B898 File Offset: 0x00169A98
		internal static void EnsureTableIsRegisteredAndPolled(string database, string table)
		{
			bool flag = false;
			if (HttpRuntime.Cache.InternalCache.Get(SqlCacheDependencyManager.GetMoniterKey(database, table)) != null)
			{
				return;
			}
			SqlCacheDependencyManager.InitPolling(database);
			DatabaseNotifState databaseNotifState = (DatabaseNotifState)SqlCacheDependencyManager.s_DatabaseNotifStates[database];
			if (!databaseNotifState._init)
			{
				HttpContext httpContext = HttpContext.Current;
				int seconds;
				if (httpContext == null)
				{
					seconds = 30;
				}
				else
				{
					seconds = Math.Max(httpContext.Timeout.Seconds / 3, 30);
				}
				DateTime t = DateTime.UtcNow.Add(new TimeSpan(0, 0, seconds));
				while (!databaseNotifState._init)
				{
					Thread.Sleep(250);
					if (!Debugger.IsAttached && DateTime.UtcNow > t)
					{
						throw new HttpException(SR.GetString("Cant_connect_sql_cache_dep_database_polling", new object[]
						{
							database
						}));
					}
				}
			}
			int num;
			Exception ex;
			bool notifEnabled;
			for (;;)
			{
				num = 0;
				DatabaseNotifState obj = databaseNotifState;
				DateTime utcTablesUpdated;
				lock (obj)
				{
					ex = databaseNotifState._pollExpt;
					if (ex != null)
					{
						num = databaseNotifState._pollSqlError;
					}
					utcTablesUpdated = databaseNotifState._utcTablesUpdated;
					notifEnabled = databaseNotifState._notifEnabled;
				}
				if (ex == null && notifEnabled && databaseNotifState._tables.ContainsKey(table))
				{
					break;
				}
				if (flag || !(DateTime.UtcNow - utcTablesUpdated >= SqlCacheDependencyManager.OneSec))
				{
					goto IL_142;
				}
				SqlCacheDependencyManager.UpdateDatabaseNotifState(database);
				flag = true;
			}
			return;
			IL_142:
			if (num == 2812)
			{
				ex = null;
			}
			if (ex != null)
			{
				string name;
				if (num == 229 || num == 262)
				{
					name = "Permission_denied_database_polling";
				}
				else
				{
					name = "Cant_connect_sql_cache_dep_database_polling";
				}
				HttpException ex2 = new HttpException(SR.GetString(name, new object[]
				{
					database
				}), ex);
				ex2.SetFormatter(new UseLastUnhandledErrorFormatter(ex2));
				throw ex2;
			}
			if (!notifEnabled)
			{
				throw new DatabaseNotEnabledForNotificationException(SR.GetString("Database_not_enabled_for_notification", new object[]
				{
					database
				}));
			}
			throw new TableNotEnabledForNotificationException(SR.GetString("Table_not_enabled_for_notification", new object[]
			{
				table,
				database
			}));
		}

		// Token: 0x0600670C RID: 26380 RVA: 0x0016BA90 File Offset: 0x00169C90
		internal static void UpdateDatabaseNotifState(string database)
		{
			using (new ApplicationImpersonationContext())
			{
				SqlCacheDependencyManager.InitPolling(database);
				SqlCacheDependencyManager.PollDatabaseForChanges((DatabaseNotifState)SqlCacheDependencyManager.s_DatabaseNotifStates[database], false);
			}
		}

		// Token: 0x0600670D RID: 26381 RVA: 0x0016BADC File Offset: 0x00169CDC
		internal static void UpdateAllDatabaseNotifState()
		{
			Hashtable obj = SqlCacheDependencyManager.s_DatabaseNotifStates;
			lock (obj)
			{
				foreach (object obj2 in SqlCacheDependencyManager.s_DatabaseNotifStates)
				{
					DictionaryEntry dictionaryEntry = (DictionaryEntry)obj2;
					DatabaseNotifState databaseNotifState = (DatabaseNotifState)dictionaryEntry.Value;
					if (databaseNotifState._init)
					{
						SqlCacheDependencyManager.UpdateDatabaseNotifState((string)dictionaryEntry.Key);
					}
				}
			}
		}

		// Token: 0x0600670E RID: 26382 RVA: 0x0016BB80 File Offset: 0x00169D80
		internal static DatabaseNotifState AddRef(string database)
		{
			DatabaseNotifState databaseNotifState = (DatabaseNotifState)SqlCacheDependencyManager.s_DatabaseNotifStates[database];
			Interlocked.Increment(ref databaseNotifState._refCount);
			return databaseNotifState;
		}

		// Token: 0x0600670F RID: 26383 RVA: 0x0016BBAB File Offset: 0x00169DAB
		internal static void Release(DatabaseNotifState dbState)
		{
			Interlocked.Decrement(ref dbState._refCount);
		}

		// Token: 0x0400351C RID: 13596
		internal const bool ENABLED_DEFAULT = true;

		// Token: 0x0400351D RID: 13597
		internal const int POLLTIME_DEFAULT = 60000;

		// Token: 0x0400351E RID: 13598
		internal const int TABLE_NAME_LENGTH = 128;

		// Token: 0x0400351F RID: 13599
		internal const int SQL_EXCEPTION_SP_NOT_FOUND = 2812;

		// Token: 0x04003520 RID: 13600
		internal const int SQL_EXCEPTION_PERMISSION_DENIED_ON_OBJECT = 229;

		// Token: 0x04003521 RID: 13601
		internal const int SQL_EXCEPTION_PERMISSION_DENIED_ON_DATABASE = 262;

		// Token: 0x04003522 RID: 13602
		internal const int SQL_EXCEPTION_PERMISSION_DENIED_ON_USER = 2760;

		// Token: 0x04003523 RID: 13603
		internal const int SQL_EXCEPTION_NO_GRANT_PERMISSION = 4613;

		// Token: 0x04003524 RID: 13604
		internal const int SQL_EXCEPTION_ADHOC = 50000;

		// Token: 0x04003525 RID: 13605
		private const char CacheKeySeparatorChar = ':';

		// Token: 0x04003526 RID: 13606
		private const string CacheKeySeparator = ":";

		// Token: 0x04003527 RID: 13607
		private const string CacheKeySeparatorEscaped = "\\:";

		// Token: 0x04003528 RID: 13608
		internal const string SQL_CUSTOM_ERROR_TABLE_NOT_FOUND = "00000001";

		// Token: 0x04003529 RID: 13609
		internal const string SQL_NOTIF_TABLE = "AspNet_SqlCacheTablesForChangeNotification";

		// Token: 0x0400352A RID: 13610
		internal const string SQL_POLLING_SP = "AspNet_SqlCachePollingStoredProcedure";

		// Token: 0x0400352B RID: 13611
		internal const string SQL_POLLING_SP_DBO = "dbo.AspNet_SqlCachePollingStoredProcedure";

		// Token: 0x0400352C RID: 13612
		internal static TimeSpan OneSec = new TimeSpan(0, 0, 1);

		// Token: 0x0400352D RID: 13613
		internal static Hashtable s_DatabaseNotifStates = new Hashtable();

		// Token: 0x0400352E RID: 13614
		private static TimerCallback s_timerCallback = new TimerCallback(SqlCacheDependencyManager.PollCallback);

		// Token: 0x0400352F RID: 13615
		private static int s_activePolling = 0;

		// Token: 0x04003530 RID: 13616
		private static bool s_shutdown = false;
	}
}
