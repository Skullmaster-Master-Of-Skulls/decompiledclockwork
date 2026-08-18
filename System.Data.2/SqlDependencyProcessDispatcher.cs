using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.ProviderBase;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Security.Principal;
using System.Threading;
using System.Xml;

// Token: 0x0200002E RID: 46
internal class SqlDependencyProcessDispatcher : MarshalByRefObject
{
	// Token: 0x17000009 RID: 9
	// (get) Token: 0x06000117 RID: 279 RVA: 0x00037D60 File Offset: 0x00037160
	internal int ObjectID
	{
		get
		{
			return this._objectID;
		}
	}

	// Token: 0x06000118 RID: 280 RVA: 0x00037D74 File Offset: 0x00037174
	private SqlDependencyProcessDispatcher(object dummyVariable)
	{
		IntPtr intPtr;
		Bid.NotificationsScopeEnter(out intPtr, "<sc.SqlDependencyProcessDispatcher|DEP> %d#", this.ObjectID);
		try
		{
			this._connectionContainers = new Dictionary<SqlDependencyProcessDispatcher.SqlConnectionContainerHashHelper, SqlDependencyProcessDispatcher.SqlConnectionContainer>();
			this._sqlDependencyPerAppDomainDispatchers = new Dictionary<string, SqlDependencyPerAppDomainDispatcher>();
		}
		finally
		{
			Bid.ScopeLeave(ref intPtr);
		}
	}

	// Token: 0x06000119 RID: 281 RVA: 0x00037DE8 File Offset: 0x000371E8
	public SqlDependencyProcessDispatcher()
	{
		IntPtr intPtr;
		Bid.NotificationsScopeEnter(out intPtr, "<sc.SqlDependencyProcessDispatcher|DEP> %d#", this.ObjectID);
		try
		{
		}
		finally
		{
			Bid.ScopeLeave(ref intPtr);
		}
	}

	// Token: 0x1700000A RID: 10
	// (get) Token: 0x0600011A RID: 282 RVA: 0x00037E44 File Offset: 0x00037244
	internal SqlDependencyProcessDispatcher SingletonProcessDispatcher
	{
		get
		{
			return SqlDependencyProcessDispatcher._staticInstance;
		}
	}

	// Token: 0x0600011B RID: 283 RVA: 0x00037E58 File Offset: 0x00037258
	private static SqlDependencyProcessDispatcher.SqlConnectionContainerHashHelper GetHashHelper(string connectionString, out SqlConnectionStringBuilder connectionStringBuilder, out DbConnectionPoolIdentity identity, out string user, string queue)
	{
		IntPtr intPtr;
		Bid.NotificationsScopeEnter(out intPtr, "<sc.SqlDependencyProcessDispatcher.GetHashString|DEP> %d#, queue: %ls", SqlDependencyProcessDispatcher._staticInstance.ObjectID, queue);
		SqlDependencyProcessDispatcher.SqlConnectionContainerHashHelper result;
		try
		{
			connectionStringBuilder = new SqlConnectionStringBuilder(connectionString);
			connectionStringBuilder.AsynchronousProcessing = true;
			connectionStringBuilder.Pooling = false;
			connectionStringBuilder.Enlist = false;
			connectionStringBuilder.ConnectRetryCount = 0;
			if (queue != null)
			{
				connectionStringBuilder.ApplicationName = queue;
			}
			if (connectionStringBuilder.IntegratedSecurity)
			{
				identity = DbConnectionPoolIdentity.GetCurrent();
				user = null;
			}
			else
			{
				identity = null;
				user = connectionStringBuilder.UserID;
			}
			result = new SqlDependencyProcessDispatcher.SqlConnectionContainerHashHelper(identity, connectionStringBuilder.ConnectionString, queue, connectionStringBuilder);
		}
		finally
		{
			Bid.ScopeLeave(ref intPtr);
		}
		return result;
	}

	// Token: 0x0600011C RID: 284 RVA: 0x00037F10 File Offset: 0x00037310
	public override object InitializeLifetimeService()
	{
		return null;
	}

	// Token: 0x0600011D RID: 285 RVA: 0x00037F20 File Offset: 0x00037320
	private void Invalidate(string server, SqlNotification sqlNotification)
	{
		IntPtr intPtr;
		Bid.NotificationsScopeEnter(out intPtr, "<sc.SqlDependencyProcessDispatcher.Invalidate|DEP> %d#, server: %ls", this.ObjectID, server);
		try
		{
			Dictionary<string, SqlDependencyPerAppDomainDispatcher> sqlDependencyPerAppDomainDispatchers = this._sqlDependencyPerAppDomainDispatchers;
			lock (sqlDependencyPerAppDomainDispatchers)
			{
				foreach (KeyValuePair<string, SqlDependencyPerAppDomainDispatcher> keyValuePair in this._sqlDependencyPerAppDomainDispatchers)
				{
					SqlDependencyPerAppDomainDispatcher value = keyValuePair.Value;
					try
					{
						value.InvalidateServer(server, sqlNotification);
					}
					catch (Exception e)
					{
						if (!ADP.IsCatchableExceptionType(e))
						{
							throw;
						}
						ADP.TraceExceptionWithoutRethrow(e);
					}
				}
			}
		}
		finally
		{
			Bid.ScopeLeave(ref intPtr);
		}
	}

	// Token: 0x0600011E RID: 286 RVA: 0x00038024 File Offset: 0x00037424
	internal void QueueAppDomainUnloading(string appDomainKey)
	{
		ThreadPool.QueueUserWorkItem(new WaitCallback(this.AppDomainUnloading), appDomainKey);
	}

	// Token: 0x0600011F RID: 287 RVA: 0x00038044 File Offset: 0x00037444
	private void AppDomainUnloading(object state)
	{
		IntPtr intPtr;
		Bid.NotificationsScopeEnter(out intPtr, "<sc.SqlDependencyProcessDispatcher.AppDomainUnloading|DEP> %d#", this.ObjectID);
		try
		{
			string text = (string)state;
			Dictionary<SqlDependencyProcessDispatcher.SqlConnectionContainerHashHelper, SqlDependencyProcessDispatcher.SqlConnectionContainer> connectionContainers = this._connectionContainers;
			lock (connectionContainers)
			{
				List<SqlDependencyProcessDispatcher.SqlConnectionContainerHashHelper> list = new List<SqlDependencyProcessDispatcher.SqlConnectionContainerHashHelper>();
				foreach (KeyValuePair<SqlDependencyProcessDispatcher.SqlConnectionContainerHashHelper, SqlDependencyProcessDispatcher.SqlConnectionContainer> keyValuePair in this._connectionContainers)
				{
					SqlDependencyProcessDispatcher.SqlConnectionContainer value = keyValuePair.Value;
					if (value.AppDomainUnload(text))
					{
						list.Add(value.HashHelper);
					}
				}
				foreach (SqlDependencyProcessDispatcher.SqlConnectionContainerHashHelper key in list)
				{
					this._connectionContainers.Remove(key);
				}
			}
			Dictionary<string, SqlDependencyPerAppDomainDispatcher> sqlDependencyPerAppDomainDispatchers = this._sqlDependencyPerAppDomainDispatchers;
			lock (sqlDependencyPerAppDomainDispatchers)
			{
				this._sqlDependencyPerAppDomainDispatchers.Remove(text);
			}
		}
		finally
		{
			Bid.ScopeLeave(ref intPtr);
		}
	}

	// Token: 0x06000120 RID: 288 RVA: 0x000381CC File Offset: 0x000375CC
	internal bool StartWithDefault(string connectionString, out string server, out DbConnectionPoolIdentity identity, out string user, out string database, ref string service, string appDomainKey, SqlDependencyPerAppDomainDispatcher dispatcher, out bool errorOccurred, out bool appDomainStart)
	{
		return this.Start(connectionString, out server, out identity, out user, out database, ref service, appDomainKey, dispatcher, out errorOccurred, out appDomainStart, true);
	}

	// Token: 0x06000121 RID: 289 RVA: 0x000381F4 File Offset: 0x000375F4
	internal bool Start(string connectionString, string queue, string appDomainKey, SqlDependencyPerAppDomainDispatcher dispatcher)
	{
		string text = null;
		bool flag = false;
		DbConnectionPoolIdentity dbConnectionPoolIdentity = null;
		return this.Start(connectionString, out text, out dbConnectionPoolIdentity, out text, out text, ref queue, appDomainKey, dispatcher, out flag, out flag, false);
	}

	// Token: 0x06000122 RID: 290 RVA: 0x00038220 File Offset: 0x00037620
	private bool Start(string connectionString, out string server, out DbConnectionPoolIdentity identity, out string user, out string database, ref string queueService, string appDomainKey, SqlDependencyPerAppDomainDispatcher dispatcher, out bool errorOccurred, out bool appDomainStart, bool useDefaults)
	{
		IntPtr intPtr;
		Bid.NotificationsScopeEnter(out intPtr, "<sc.SqlDependencyProcessDispatcher.Start|DEP> %d#, queue: '%ls', appDomainKey: '%ls', perAppDomainDispatcher ID: '%d'", this.ObjectID, queueService, appDomainKey, dispatcher.ObjectID);
		bool result;
		try
		{
			server = null;
			identity = null;
			user = null;
			database = null;
			errorOccurred = false;
			appDomainStart = false;
			Dictionary<string, SqlDependencyPerAppDomainDispatcher> sqlDependencyPerAppDomainDispatchers = this._sqlDependencyPerAppDomainDispatchers;
			lock (sqlDependencyPerAppDomainDispatchers)
			{
				if (!this._sqlDependencyPerAppDomainDispatchers.ContainsKey(appDomainKey))
				{
					this._sqlDependencyPerAppDomainDispatchers[appDomainKey] = dispatcher;
				}
			}
			SqlConnectionStringBuilder sqlConnectionStringBuilder = null;
			SqlDependencyProcessDispatcher.SqlConnectionContainerHashHelper hashHelper = SqlDependencyProcessDispatcher.GetHashHelper(connectionString, out sqlConnectionStringBuilder, out identity, out user, queueService);
			bool flag2 = false;
			SqlDependencyProcessDispatcher.SqlConnectionContainer sqlConnectionContainer = null;
			Dictionary<SqlDependencyProcessDispatcher.SqlConnectionContainerHashHelper, SqlDependencyProcessDispatcher.SqlConnectionContainer> connectionContainers = this._connectionContainers;
			lock (connectionContainers)
			{
				if (!this._connectionContainers.ContainsKey(hashHelper))
				{
					Bid.NotificationsTrace("<sc.SqlDependencyProcessDispatcher.Start|DEP> %d#, hashtable miss, creating new container.\n", this.ObjectID);
					sqlConnectionContainer = new SqlDependencyProcessDispatcher.SqlConnectionContainer(hashHelper, appDomainKey, useDefaults);
					this._connectionContainers.Add(hashHelper, sqlConnectionContainer);
					flag2 = true;
					appDomainStart = true;
				}
				else
				{
					sqlConnectionContainer = this._connectionContainers[hashHelper];
					Bid.NotificationsTrace("<sc.SqlDependencyProcessDispatcher.Start|DEP> %d#, hashtable hit, container: %d\n", this.ObjectID, sqlConnectionContainer.ObjectID);
					if (sqlConnectionContainer.InErrorState)
					{
						Bid.NotificationsTrace("<sc.SqlDependencyProcessDispatcher.Start|DEP> %d#, container: %d is in error state!\n", this.ObjectID, sqlConnectionContainer.ObjectID);
						errorOccurred = true;
					}
					else
					{
						sqlConnectionContainer.IncrementStartCount(appDomainKey, out appDomainStart);
					}
				}
			}
			if (useDefaults && !errorOccurred)
			{
				server = sqlConnectionContainer.Server;
				database = sqlConnectionContainer.Database;
				queueService = sqlConnectionContainer.Queue;
				Bid.NotificationsTrace("<sc.SqlDependencyProcessDispatcher.Start|DEP> %d#, default service: '%ls', server: '%ls', database: '%ls'\n", this.ObjectID, queueService, server, database);
			}
			Bid.NotificationsTrace("<sc.SqlDependencyProcessDispatcher.Start|DEP> %d#, started: %d\n", this.ObjectID, flag2);
			result = flag2;
		}
		finally
		{
			Bid.ScopeLeave(ref intPtr);
		}
		return result;
	}

	// Token: 0x06000123 RID: 291 RVA: 0x0003840C File Offset: 0x0003780C
	internal bool Stop(string connectionString, out string server, out DbConnectionPoolIdentity identity, out string user, out string database, ref string queueService, string appDomainKey, out bool appDomainStop)
	{
		IntPtr intPtr;
		Bid.NotificationsScopeEnter(out intPtr, "<sc.SqlDependencyProcessDispatcher.Stop|DEP> %d#, queue: '%ls'", this.ObjectID, queueService);
		bool result;
		try
		{
			server = null;
			identity = null;
			user = null;
			database = null;
			appDomainStop = false;
			SqlConnectionStringBuilder sqlConnectionStringBuilder = null;
			SqlDependencyProcessDispatcher.SqlConnectionContainerHashHelper hashHelper = SqlDependencyProcessDispatcher.GetHashHelper(connectionString, out sqlConnectionStringBuilder, out identity, out user, queueService);
			bool flag = false;
			Dictionary<SqlDependencyProcessDispatcher.SqlConnectionContainerHashHelper, SqlDependencyProcessDispatcher.SqlConnectionContainer> connectionContainers = this._connectionContainers;
			lock (connectionContainers)
			{
				if (this._connectionContainers.ContainsKey(hashHelper))
				{
					SqlDependencyProcessDispatcher.SqlConnectionContainer sqlConnectionContainer = this._connectionContainers[hashHelper];
					Bid.NotificationsTrace("<sc.SqlDependencyProcessDispatcher.Stop|DEP> %d#, hashtable hit, container: %d\n", this.ObjectID, sqlConnectionContainer.ObjectID);
					server = sqlConnectionContainer.Server;
					database = sqlConnectionContainer.Database;
					queueService = sqlConnectionContainer.Queue;
					if (sqlConnectionContainer.Stop(appDomainKey, out appDomainStop))
					{
						flag = true;
						this._connectionContainers.Remove(hashHelper);
					}
				}
				else
				{
					Bid.NotificationsTrace("<sc.SqlDependencyProcessDispatcher.Stop|DEP> %d#, hashtable miss.\n", this.ObjectID);
				}
			}
			Bid.NotificationsTrace("<sc.SqlDependencyProcessDispatcher.Stop|DEP> %d#, stopped: %d\n", this.ObjectID, flag);
			result = flag;
		}
		finally
		{
			Bid.ScopeLeave(ref intPtr);
		}
		return result;
	}

	// Token: 0x040000BA RID: 186
	private static SqlDependencyProcessDispatcher _staticInstance = new SqlDependencyProcessDispatcher(null);

	// Token: 0x040000BB RID: 187
	private Dictionary<SqlDependencyProcessDispatcher.SqlConnectionContainerHashHelper, SqlDependencyProcessDispatcher.SqlConnectionContainer> _connectionContainers;

	// Token: 0x040000BC RID: 188
	private Dictionary<string, SqlDependencyPerAppDomainDispatcher> _sqlDependencyPerAppDomainDispatchers;

	// Token: 0x040000BD RID: 189
	private readonly int _objectID = Interlocked.Increment(ref SqlDependencyProcessDispatcher._objectTypeCount);

	// Token: 0x040000BE RID: 190
	private static int _objectTypeCount;

	// Token: 0x0200033D RID: 829
	private class SqlConnectionContainer
	{
		// Token: 0x17000830 RID: 2096
		// (get) Token: 0x060033C0 RID: 13248 RVA: 0x0013D990 File Offset: 0x0013CD90
		internal int ObjectID
		{
			get
			{
				return this._objectID;
			}
		}

		// Token: 0x060033C1 RID: 13249 RVA: 0x0013D9A4 File Offset: 0x0013CDA4
		internal SqlConnectionContainer(SqlDependencyProcessDispatcher.SqlConnectionContainerHashHelper hashHelper, string appDomainKey, bool useDefaults)
		{
			IntPtr intPtr;
			Bid.NotificationsScopeEnter(out intPtr, "<sc.SqlConnectionContainer|DEP> %d#, queue: '%ls'", this.ObjectID, hashHelper.Queue);
			bool flag = false;
			try
			{
				this._hashHelper = hashHelper;
				string str = null;
				if (useDefaults)
				{
					str = Guid.NewGuid().ToString();
					this._queue = "SqlQueryNotificationService-" + str;
					this._hashHelper.ConnectionStringBuilder.ApplicationName = this._queue;
				}
				else
				{
					this._queue = this._hashHelper.Queue;
				}
				this._con = new SqlConnection(this._hashHelper.ConnectionStringBuilder.ConnectionString);
				SqlConnectionString sqlConnectionString = (SqlConnectionString)this._con.ConnectionOptions;
				sqlConnectionString.CreatePermissionSet().Assert();
				if (sqlConnectionString.LocalDBInstance != null)
				{
					LocalDBAPI.AssertLocalDBPermissions();
				}
				this._con.Open();
				this._cachedServer = this._con.DataSource;
				if (!this._con.IsYukonOrNewer)
				{
					throw SQL.NotificationsRequireYukon();
				}
				if (hashHelper.Identity != null)
				{
					this._windowsIdentity = DbConnectionPoolIdentity.GetCurrentWindowsIdentity();
				}
				this._escapedQueueName = SqlConnection.FixupDatabaseTransactionName(this._queue);
				this._appDomainKeyHash = new Dictionary<string, int>();
				this._com = new SqlCommand();
				this._com.Connection = this._con;
				this._com.CommandText = "select is_broker_enabled from sys.databases where database_id=db_id()";
				if (!(bool)this._com.ExecuteScalar())
				{
					throw SQL.SqlDependencyDatabaseBrokerDisabled();
				}
				this._conversationGuidParam = new SqlParameter("@p1", SqlDbType.UniqueIdentifier);
				this._timeoutParam = new SqlParameter("@p2", SqlDbType.Int);
				this._timeoutParam.Value = 0;
				this._com.Parameters.Add(this._timeoutParam);
				flag = true;
				this._receiveQuery = "WAITFOR(RECEIVE TOP (1) message_type_name, conversation_handle, cast(message_body AS XML) as message_body from " + this._escapedQueueName + "), TIMEOUT @p2;";
				if (useDefaults)
				{
					this._sprocName = SqlConnection.FixupDatabaseTransactionName("SqlQueryNotificationStoredProcedure-" + str);
					this.CreateQueueAndService(false);
				}
				else
				{
					this._com.CommandText = this._receiveQuery;
					this._endConversationQuery = "END CONVERSATION @p1; ";
					this._concatQuery = this._endConversationQuery + this._receiveQuery;
				}
				bool flag2 = false;
				this.IncrementStartCount(appDomainKey, out flag2);
				this.SynchronouslyQueryServiceBrokerQueue();
				this._timeoutParam.Value = this._defaultWaitforTimeout;
				this.AsynchronouslyQueryServiceBrokerQueue();
			}
			catch (Exception e)
			{
				if (!ADP.IsCatchableExceptionType(e))
				{
					throw;
				}
				ADP.TraceExceptionWithoutRethrow(e);
				if (flag)
				{
					this.TearDownAndDispose();
				}
				else
				{
					if (this._com != null)
					{
						this._com.Dispose();
						this._com = null;
					}
					if (this._con != null)
					{
						this._con.Dispose();
						this._con = null;
					}
				}
				throw;
			}
			finally
			{
				Bid.ScopeLeave(ref intPtr);
			}
		}

		// Token: 0x17000831 RID: 2097
		// (get) Token: 0x060033C2 RID: 13250 RVA: 0x0013DCAC File Offset: 0x0013D0AC
		internal string Database
		{
			get
			{
				if (this._cachedDatabase == null)
				{
					this._cachedDatabase = this._con.Database;
				}
				return this._cachedDatabase;
			}
		}

		// Token: 0x17000832 RID: 2098
		// (get) Token: 0x060033C3 RID: 13251 RVA: 0x0013DCD8 File Offset: 0x0013D0D8
		internal SqlDependencyProcessDispatcher.SqlConnectionContainerHashHelper HashHelper
		{
			get
			{
				return this._hashHelper;
			}
		}

		// Token: 0x17000833 RID: 2099
		// (get) Token: 0x060033C4 RID: 13252 RVA: 0x0013DCEC File Offset: 0x0013D0EC
		internal bool InErrorState
		{
			get
			{
				return this._errorState;
			}
		}

		// Token: 0x17000834 RID: 2100
		// (get) Token: 0x060033C5 RID: 13253 RVA: 0x0013DD04 File Offset: 0x0013D104
		internal string Queue
		{
			get
			{
				return this._queue;
			}
		}

		// Token: 0x17000835 RID: 2101
		// (get) Token: 0x060033C6 RID: 13254 RVA: 0x0013DD18 File Offset: 0x0013D118
		internal string Server
		{
			get
			{
				return this._cachedServer;
			}
		}

		// Token: 0x060033C7 RID: 13255 RVA: 0x0013DD2C File Offset: 0x0013D12C
		internal bool AppDomainUnload(string appDomainKey)
		{
			IntPtr intPtr;
			Bid.NotificationsScopeEnter(out intPtr, "<sc.SqlConnectionContainer.AppDomainUnload|DEP> %d#, AppDomainKey: '%ls'", this.ObjectID, appDomainKey);
			bool stopped;
			try
			{
				Dictionary<string, int> appDomainKeyHash = this._appDomainKeyHash;
				lock (appDomainKeyHash)
				{
					if (this._appDomainKeyHash.ContainsKey(appDomainKey))
					{
						Bid.NotificationsTrace("<sc.SqlConnectionContainer.AppDomainUnload|DEP> _appDomainKeyHash contained AppDomainKey: '%ls'.\n", appDomainKey);
						int i = this._appDomainKeyHash[appDomainKey];
						Bid.NotificationsTrace("<sc.SqlConnectionContainer.AppDomainUnload|DEP> _appDomainKeyHash for AppDomainKey: '%ls' count: '%d'.\n", appDomainKey, i);
						bool flag2 = false;
						while (i > 0)
						{
							this.Stop(appDomainKey, out flag2);
							i--;
						}
						if (this._appDomainKeyHash.ContainsKey(appDomainKey))
						{
							Bid.NotificationsTrace("<sc.SqlConnectionContainer.AppDomainUnload|DEP|ERR> ERROR - after the Stop() loop, _appDomainKeyHash for AppDomainKey: '%ls' entry not removed from hash.  Count: %d'\n", appDomainKey, this._appDomainKeyHash[appDomainKey]);
						}
					}
					else
					{
						Bid.NotificationsTrace("<sc.SqlConnectionContainer.AppDomainUnload|DEP> _appDomainKeyHash did not contain AppDomainKey: '%ls'.\n", appDomainKey);
					}
				}
				Bid.NotificationsTrace("<sc.SqlConnectionContainer.AppDomainUnload|DEP> Exiting, _stopped: '%d'.\n", this._stopped);
				stopped = this._stopped;
			}
			finally
			{
				Bid.ScopeLeave(ref intPtr);
			}
			return stopped;
		}

		// Token: 0x060033C8 RID: 13256 RVA: 0x0013DE40 File Offset: 0x0013D240
		private void AsynchronouslyQueryServiceBrokerQueue()
		{
			IntPtr intPtr;
			Bid.NotificationsScopeEnter(out intPtr, "<sc.SqlConnectionContainer.AsynchronouslyQueryServiceBrokerQueue|DEP> %d#", this.ObjectID);
			try
			{
				AsyncCallback callback = new AsyncCallback(this.AsyncResultCallback);
				this._com.BeginExecuteReader(callback, null);
			}
			finally
			{
				Bid.ScopeLeave(ref intPtr);
			}
		}

		// Token: 0x060033C9 RID: 13257 RVA: 0x0013DEA0 File Offset: 0x0013D2A0
		private void AsyncResultCallback(IAsyncResult asyncResult)
		{
			IntPtr intPtr;
			Bid.NotificationsScopeEnter(out intPtr, "<sc.SqlConnectionContainer.AsyncResultCallback|DEP> %d#", this.ObjectID);
			try
			{
				using (SqlDataReader sqlDataReader = this._com.EndExecuteReader(asyncResult))
				{
					this.ProcessNotificationResults(sqlDataReader);
				}
				if (!this._stop)
				{
					this.AsynchronouslyQueryServiceBrokerQueue();
				}
				else
				{
					this.TearDownAndDispose();
				}
			}
			catch (Exception e)
			{
				if (!ADP.IsCatchableExceptionType(e))
				{
					this._errorState = true;
					throw;
				}
				Bid.NotificationsTrace("<sc.SqlConnectionContainer.AsyncResultCallback|DEP> Exception occurred.\n");
				if (!this._stop)
				{
					ADP.TraceExceptionWithoutRethrow(e);
				}
				if (this._stop)
				{
					this.TearDownAndDispose();
				}
				else
				{
					this._errorState = true;
					this.Restart(null);
				}
			}
			finally
			{
				Bid.ScopeLeave(ref intPtr);
			}
		}

		// Token: 0x060033CA RID: 13258 RVA: 0x0013DFA4 File Offset: 0x0013D3A4
		private void CreateQueueAndService(bool restart)
		{
			IntPtr intPtr;
			Bid.NotificationsScopeEnter(out intPtr, "<sc.SqlConnectionContainer.CreateQueueAndService|DEP> %d#", this.ObjectID);
			try
			{
				SqlCommand sqlCommand = new SqlCommand();
				sqlCommand.Connection = this._con;
				SqlTransaction sqlTransaction = null;
				try
				{
					sqlTransaction = this._con.BeginTransaction();
					sqlCommand.Transaction = sqlTransaction;
					string text = SqlServerEscapeHelper.MakeStringLiteral(this._queue);
					sqlCommand.CommandText = string.Concat(new string[]
					{
						"CREATE PROCEDURE ",
						this._sprocName,
						" AS BEGIN BEGIN TRANSACTION; RECEIVE TOP(0) conversation_handle FROM ",
						this._escapedQueueName,
						"; IF (SELECT COUNT(*) FROM ",
						this._escapedQueueName,
						" WHERE message_type_name = 'http://schemas.microsoft.com/SQL/ServiceBroker/DialogTimer') > 0 BEGIN if ((SELECT COUNT(*) FROM sys.services WHERE name = ",
						text,
						") > 0)   DROP SERVICE ",
						this._escapedQueueName,
						"; if (OBJECT_ID(",
						text,
						", 'SQ') IS NOT NULL)   DROP QUEUE ",
						this._escapedQueueName,
						"; DROP PROCEDURE ",
						this._sprocName,
						"; END COMMIT TRANSACTION; END"
					});
					if (!restart)
					{
						sqlCommand.ExecuteNonQuery();
					}
					else
					{
						try
						{
							sqlCommand.ExecuteNonQuery();
						}
						catch (Exception e)
						{
							if (!ADP.IsCatchableExceptionType(e))
							{
								throw;
							}
							ADP.TraceExceptionWithoutRethrow(e);
							try
							{
								if (sqlTransaction != null)
								{
									sqlTransaction.Rollback();
									sqlTransaction = null;
								}
							}
							catch (Exception e2)
							{
								if (!ADP.IsCatchableExceptionType(e2))
								{
									throw;
								}
								ADP.TraceExceptionWithoutRethrow(e2);
							}
						}
						if (sqlTransaction == null)
						{
							sqlTransaction = this._con.BeginTransaction();
							sqlCommand.Transaction = sqlTransaction;
						}
					}
					sqlCommand.CommandText = string.Concat(new string[]
					{
						"IF OBJECT_ID(",
						text,
						", 'SQ') IS NULL BEGIN CREATE QUEUE ",
						this._escapedQueueName,
						" WITH ACTIVATION (PROCEDURE_NAME=",
						this._sprocName,
						", MAX_QUEUE_READERS=1, EXECUTE AS OWNER); END; IF (SELECT COUNT(*) FROM sys.services WHERE NAME=",
						text,
						") = 0 BEGIN CREATE SERVICE ",
						this._escapedQueueName,
						" ON QUEUE ",
						this._escapedQueueName,
						" ([http://schemas.microsoft.com/SQL/Notifications/PostQueryNotification]); IF (SELECT COUNT(*) FROM sys.database_principals WHERE name='sql_dependency_subscriber' AND type='R') <> 0 BEGIN GRANT SEND ON SERVICE::",
						this._escapedQueueName,
						" TO sql_dependency_subscriber; END;  END; BEGIN DIALOG @dialog_handle FROM SERVICE ",
						this._escapedQueueName,
						" TO SERVICE ",
						text
					});
					SqlParameter sqlParameter = new SqlParameter();
					sqlParameter.ParameterName = "@dialog_handle";
					sqlParameter.DbType = DbType.Guid;
					sqlParameter.Direction = ParameterDirection.Output;
					sqlCommand.Parameters.Add(sqlParameter);
					sqlCommand.ExecuteNonQuery();
					this._dialogHandle = ((Guid)sqlParameter.Value).ToString();
					this._beginConversationQuery = "BEGIN CONVERSATION TIMER ('" + this._dialogHandle + "') TIMEOUT = 120; " + this._receiveQuery;
					this._com.CommandText = this._beginConversationQuery;
					this._endConversationQuery = "END CONVERSATION @p1; ";
					this._concatQuery = this._endConversationQuery + this._com.CommandText;
					sqlTransaction.Commit();
					sqlTransaction = null;
					this._serviceQueueCreated = true;
				}
				finally
				{
					if (sqlTransaction != null)
					{
						try
						{
							sqlTransaction.Rollback();
							sqlTransaction = null;
						}
						catch (Exception e3)
						{
							if (!ADP.IsCatchableExceptionType(e3))
							{
								throw;
							}
							ADP.TraceExceptionWithoutRethrow(e3);
						}
					}
				}
			}
			finally
			{
				Bid.ScopeLeave(ref intPtr);
			}
		}

		// Token: 0x060033CB RID: 13259 RVA: 0x0013E30C File Offset: 0x0013D70C
		internal void IncrementStartCount(string appDomainKey, out bool appDomainStart)
		{
			IntPtr intPtr;
			Bid.NotificationsScopeEnter(out intPtr, "<sc.SqlConnectionContainer.IncrementStartCount|DEP> %d#", this.ObjectID);
			try
			{
				appDomainStart = false;
				int a = Interlocked.Increment(ref this._startCount);
				Bid.NotificationsTrace("<sc.SqlConnectionContainer.IncrementStartCount|DEP> %d#, incremented _startCount: %d\n", SqlDependencyProcessDispatcher._staticInstance.ObjectID, a);
				Dictionary<string, int> appDomainKeyHash = this._appDomainKeyHash;
				lock (appDomainKeyHash)
				{
					if (this._appDomainKeyHash.ContainsKey(appDomainKey))
					{
						this._appDomainKeyHash[appDomainKey] = this._appDomainKeyHash[appDomainKey] + 1;
						Bid.NotificationsTrace("<sc.SqlConnectionContainer.IncrementStartCount|DEP> _appDomainKeyHash contained AppDomainKey: '%ls', incremented count: '%d'.\n", appDomainKey, this._appDomainKeyHash[appDomainKey]);
					}
					else
					{
						this._appDomainKeyHash[appDomainKey] = 1;
						appDomainStart = true;
						Bid.NotificationsTrace("<sc.SqlConnectionContainer.IncrementStartCount|DEP> _appDomainKeyHash did not contain AppDomainKey: '%ls', added to hashtable and value set to 1.\n", appDomainKey);
					}
				}
			}
			finally
			{
				Bid.ScopeLeave(ref intPtr);
			}
		}

		// Token: 0x060033CC RID: 13260 RVA: 0x0013E408 File Offset: 0x0013D808
		private void ProcessNotificationResults(SqlDataReader reader)
		{
			IntPtr intPtr;
			Bid.NotificationsScopeEnter(out intPtr, "<sc.SqlConnectionContainer.ProcessNotificationResults|DEP> %d#", this.ObjectID);
			try
			{
				Guid guid = Guid.Empty;
				try
				{
					if (!this._stop)
					{
						while (reader.Read())
						{
							Bid.NotificationsTrace("<sc.SqlConnectionContainer.ProcessNotificationResults|DEP> Row read.\n");
							string @string = reader.GetString(0);
							Bid.NotificationsTrace("<sc.SqlConnectionContainer.ProcessNotificationResults|DEP> msgType: '%ls'\n", @string);
							guid = reader.GetGuid(1);
							if (string.Compare(@string, "http://schemas.microsoft.com/SQL/Notifications/QueryNotification", StringComparison.OrdinalIgnoreCase) == 0)
							{
								SqlXml sqlXml = reader.GetSqlXml(2);
								if (sqlXml != null)
								{
									SqlNotification sqlNotification = SqlDependencyProcessDispatcher.SqlNotificationParser.ProcessMessage(sqlXml);
									if (sqlNotification != null)
									{
										string key = sqlNotification.Key;
										Bid.NotificationsTrace("<sc.SqlConnectionContainer.ProcessNotificationResults|DEP> Key: '%ls'\n", key);
										int num = key.IndexOf(';');
										if (num >= 0)
										{
											string key2 = key.Substring(0, num);
											Dictionary<string, SqlDependencyPerAppDomainDispatcher> sqlDependencyPerAppDomainDispatchers = SqlDependencyProcessDispatcher._staticInstance._sqlDependencyPerAppDomainDispatchers;
											SqlDependencyPerAppDomainDispatcher sqlDependencyPerAppDomainDispatcher;
											lock (sqlDependencyPerAppDomainDispatchers)
											{
												sqlDependencyPerAppDomainDispatcher = SqlDependencyProcessDispatcher._staticInstance._sqlDependencyPerAppDomainDispatchers[key2];
											}
											if (sqlDependencyPerAppDomainDispatcher != null)
											{
												try
												{
													sqlDependencyPerAppDomainDispatcher.InvalidateCommandID(sqlNotification);
													continue;
												}
												catch (Exception e)
												{
													if (!ADP.IsCatchableExceptionType(e))
													{
														throw;
													}
													ADP.TraceExceptionWithoutRethrow(e);
													continue;
												}
											}
											Bid.NotificationsTrace("<sc.SqlConnectionContainer.ProcessNotificationResults|DEP|ERR> Received notification but do not have an associated PerAppDomainDispatcher!\n");
										}
										else
										{
											Bid.NotificationsTrace("<sc.SqlConnectionContainer.ProcessNotificationResults|DEP|ERR> Unexpected ID format received!\n");
										}
									}
									else
									{
										Bid.NotificationsTrace("<sc.SqlConnectionContainer.ProcessNotificationResults|DEP|ERR> Null notification returned from ProcessMessage!\n");
									}
								}
								else
								{
									Bid.NotificationsTrace("<sc.SqlConnectionContainer.ProcessNotificationResults|DEP|ERR> Null payload for QN notification type!\n");
								}
							}
							else
							{
								guid = Guid.Empty;
								Bid.NotificationsTrace("<sc.SqlConnectionContainer.ProcessNotificationResults|DEP> Unexpected message format received!\n");
							}
						}
					}
				}
				finally
				{
					if (guid == Guid.Empty)
					{
						this._com.CommandText = ((this._beginConversationQuery != null) ? this._beginConversationQuery : this._receiveQuery);
						if (this._com.Parameters.Count > 1)
						{
							this._com.Parameters.Remove(this._conversationGuidParam);
						}
					}
					else
					{
						this._com.CommandText = this._concatQuery;
						this._conversationGuidParam.Value = guid;
						if (this._com.Parameters.Count == 1)
						{
							this._com.Parameters.Add(this._conversationGuidParam);
						}
					}
				}
			}
			finally
			{
				Bid.ScopeLeave(ref intPtr);
			}
		}

		// Token: 0x060033CD RID: 13261 RVA: 0x0013E680 File Offset: 0x0013DA80
		private void Restart(object unused)
		{
			IntPtr intPtr;
			Bid.NotificationsScopeEnter(out intPtr, "<sc.SqlConnectionContainer.Restart|DEP> %d#", this.ObjectID);
			try
			{
				lock (this)
				{
					if (!this._stop)
					{
						try
						{
							this._con.Close();
						}
						catch (Exception e)
						{
							if (!ADP.IsCatchableExceptionType(e))
							{
								throw;
							}
							ADP.TraceExceptionWithoutRethrow(e);
						}
					}
				}
				lock (this)
				{
					if (!this._stop)
					{
						if (this._hashHelper.Identity != null)
						{
							WindowsImpersonationContext windowsImpersonationContext = null;
							RuntimeHelpers.PrepareConstrainedRegions();
							try
							{
								windowsImpersonationContext = this._windowsIdentity.Impersonate();
								this._con.Open();
								goto IL_B7;
							}
							finally
							{
								if (windowsImpersonationContext != null)
								{
									windowsImpersonationContext.Undo();
								}
							}
						}
						this._con.Open();
					}
					IL_B7:;
				}
				lock (this)
				{
					if (!this._stop && this._serviceQueueCreated)
					{
						bool flag4 = false;
						try
						{
							this.CreateQueueAndService(true);
						}
						catch (Exception e2)
						{
							if (!ADP.IsCatchableExceptionType(e2))
							{
								throw;
							}
							ADP.TraceExceptionWithoutRethrow(e2);
							flag4 = true;
						}
						if (flag4)
						{
							SqlDependencyProcessDispatcher._staticInstance.Invalidate(this.Server, new SqlNotification(SqlNotificationInfo.Error, SqlNotificationSource.Client, SqlNotificationType.Change, null));
						}
					}
				}
				lock (this)
				{
					if (!this._stop)
					{
						this._timeoutParam.Value = 0;
						this.SynchronouslyQueryServiceBrokerQueue();
						this._timeoutParam.Value = this._defaultWaitforTimeout;
						this.AsynchronouslyQueryServiceBrokerQueue();
						this._errorState = false;
						this._retryTimer = null;
					}
				}
				if (this._stop)
				{
					this.TearDownAndDispose();
				}
			}
			catch (Exception e3)
			{
				if (!ADP.IsCatchableExceptionType(e3))
				{
					throw;
				}
				ADP.TraceExceptionWithoutRethrow(e3);
				try
				{
					SqlDependencyProcessDispatcher._staticInstance.Invalidate(this.Server, new SqlNotification(SqlNotificationInfo.Error, SqlNotificationSource.Client, SqlNotificationType.Change, null));
				}
				catch (Exception e4)
				{
					if (!ADP.IsCatchableExceptionType(e4))
					{
						throw;
					}
					ADP.TraceExceptionWithoutRethrow(e4);
				}
				try
				{
					this._con.Close();
				}
				catch (Exception e5)
				{
					if (!ADP.IsCatchableExceptionType(e5))
					{
						throw;
					}
					ADP.TraceExceptionWithoutRethrow(e5);
				}
				if (!this._stop)
				{
					this._retryTimer = new Timer(new TimerCallback(this.Restart), null, this._defaultWaitforTimeout, -1);
				}
			}
			finally
			{
				Bid.ScopeLeave(ref intPtr);
			}
		}

		// Token: 0x060033CE RID: 13262 RVA: 0x0013E9E8 File Offset: 0x0013DDE8
		internal bool Stop(string appDomainKey, out bool appDomainStop)
		{
			IntPtr intPtr;
			Bid.NotificationsScopeEnter(out intPtr, "<sc.SqlConnectionContainer.Stop|DEP> %d#", this.ObjectID);
			bool stopped;
			try
			{
				appDomainStop = false;
				if (appDomainKey != null)
				{
					Dictionary<string, int> appDomainKeyHash = this._appDomainKeyHash;
					lock (appDomainKeyHash)
					{
						if (this._appDomainKeyHash.ContainsKey(appDomainKey))
						{
							int num = this._appDomainKeyHash[appDomainKey];
							Bid.NotificationsTrace("<sc.SqlConnectionContainer.Stop|DEP> _appDomainKeyHash contained AppDomainKey: '%ls', pre-decrement Count: '%d'.\n", appDomainKey, num);
							if (num > 0)
							{
								this._appDomainKeyHash[appDomainKey] = num - 1;
							}
							else
							{
								Bid.NotificationsTrace("<sc.SqlConnectionContainer.Stop|DEP}ERR> ERROR pre-decremented count <= 0!\n");
							}
							if (1 == num)
							{
								this._appDomainKeyHash.Remove(appDomainKey);
								appDomainStop = true;
							}
						}
						else
						{
							Bid.NotificationsTrace("<sc.SqlConnectionContainer.Stop|DEP|ERR> ERROR appDomainKey not null and not found in hash!\n");
						}
					}
				}
				if (Interlocked.Decrement(ref this._startCount) == 0)
				{
					Bid.NotificationsTrace("<sc.SqlConnectionContainer.Stop|DEP> Reached 0 count, cancelling and waiting.\n");
					lock (this)
					{
						try
						{
							this._com.Cancel();
						}
						catch (Exception e)
						{
							if (!ADP.IsCatchableExceptionType(e))
							{
								throw;
							}
							ADP.TraceExceptionWithoutRethrow(e);
						}
						this._stop = true;
					}
					Stopwatch stopwatch = Stopwatch.StartNew();
					for (;;)
					{
						lock (this)
						{
							if (this._stopped)
							{
								break;
							}
							if (this._errorState || stopwatch.Elapsed.Seconds >= 30)
							{
								Bid.NotificationsTrace("<sc.SqlConnectionContainer.Stop|DEP|ERR> forcing cleanup. elapsedSeconds: '%d', _errorState: '%d'.\n", stopwatch.Elapsed.Seconds, this._errorState);
								Timer retryTimer = this._retryTimer;
								this._retryTimer = null;
								if (retryTimer != null)
								{
									retryTimer.Dispose();
								}
								this.TearDownAndDispose();
								break;
							}
						}
						Thread.Sleep(1);
					}
				}
				else
				{
					Bid.NotificationsTrace("<sc.SqlConnectionContainer.Stop|DEP> _startCount not 0 after decrement.  _startCount: '%d'.\n", this._startCount);
				}
				stopped = this._stopped;
			}
			finally
			{
				Bid.ScopeLeave(ref intPtr);
			}
			return stopped;
		}

		// Token: 0x060033CF RID: 13263 RVA: 0x0013EC38 File Offset: 0x0013E038
		private void SynchronouslyQueryServiceBrokerQueue()
		{
			IntPtr intPtr;
			Bid.NotificationsScopeEnter(out intPtr, "<sc.SqlConnectionContainer.SynchronouslyQueryServiceBrokerQueue|DEP> %d#", this.ObjectID);
			try
			{
				using (SqlDataReader sqlDataReader = this._com.ExecuteReader())
				{
					this.ProcessNotificationResults(sqlDataReader);
				}
			}
			finally
			{
				Bid.ScopeLeave(ref intPtr);
			}
		}

		// Token: 0x060033D0 RID: 13264 RVA: 0x0013ECB4 File Offset: 0x0013E0B4
		private void TearDownAndDispose()
		{
			IntPtr intPtr;
			Bid.NotificationsScopeEnter(out intPtr, "<sc.SqlConnectionContainer.TearDownAndDispose|DEP> %d#", this.ObjectID);
			try
			{
				lock (this)
				{
					try
					{
						if (this._con.State != ConnectionState.Closed && ConnectionState.Broken != this._con.State)
						{
							if (this._com.Parameters.Count > 1)
							{
								try
								{
									this._com.CommandText = this._endConversationQuery;
									this._com.Parameters.Remove(this._timeoutParam);
									this._com.ExecuteNonQuery();
								}
								catch (Exception e)
								{
									if (!ADP.IsCatchableExceptionType(e))
									{
										throw;
									}
									ADP.TraceExceptionWithoutRethrow(e);
								}
							}
							if (this._serviceQueueCreated && !this._errorState)
							{
								this._com.CommandText = string.Concat(new string[]
								{
									"BEGIN TRANSACTION; DROP SERVICE ",
									this._escapedQueueName,
									"; DROP QUEUE ",
									this._escapedQueueName,
									"; DROP PROCEDURE ",
									this._sprocName,
									"; COMMIT TRANSACTION;"
								});
								try
								{
									this._com.ExecuteNonQuery();
								}
								catch (Exception e2)
								{
									if (!ADP.IsCatchableExceptionType(e2))
									{
										throw;
									}
									ADP.TraceExceptionWithoutRethrow(e2);
								}
							}
						}
					}
					finally
					{
						this._stopped = true;
						this._con.Dispose();
						if (this._windowsIdentity != null)
						{
							this._windowsIdentity.Dispose();
						}
					}
				}
			}
			finally
			{
				Bid.ScopeLeave(ref intPtr);
			}
		}

		// Token: 0x04001E5B RID: 7771
		private SqlConnection _con;

		// Token: 0x04001E5C RID: 7772
		private SqlCommand _com;

		// Token: 0x04001E5D RID: 7773
		private SqlParameter _conversationGuidParam;

		// Token: 0x04001E5E RID: 7774
		private SqlParameter _timeoutParam;

		// Token: 0x04001E5F RID: 7775
		private SqlDependencyProcessDispatcher.SqlConnectionContainerHashHelper _hashHelper;

		// Token: 0x04001E60 RID: 7776
		private WindowsIdentity _windowsIdentity;

		// Token: 0x04001E61 RID: 7777
		private string _queue;

		// Token: 0x04001E62 RID: 7778
		private string _receiveQuery;

		// Token: 0x04001E63 RID: 7779
		private string _beginConversationQuery;

		// Token: 0x04001E64 RID: 7780
		private string _endConversationQuery;

		// Token: 0x04001E65 RID: 7781
		private string _concatQuery;

		// Token: 0x04001E66 RID: 7782
		private readonly int _defaultWaitforTimeout = 60000;

		// Token: 0x04001E67 RID: 7783
		private string _escapedQueueName;

		// Token: 0x04001E68 RID: 7784
		private string _sprocName;

		// Token: 0x04001E69 RID: 7785
		private string _dialogHandle;

		// Token: 0x04001E6A RID: 7786
		private string _cachedServer;

		// Token: 0x04001E6B RID: 7787
		private string _cachedDatabase;

		// Token: 0x04001E6C RID: 7788
		private volatile bool _errorState;

		// Token: 0x04001E6D RID: 7789
		private volatile bool _stop;

		// Token: 0x04001E6E RID: 7790
		private volatile bool _stopped;

		// Token: 0x04001E6F RID: 7791
		private volatile bool _serviceQueueCreated;

		// Token: 0x04001E70 RID: 7792
		private int _startCount;

		// Token: 0x04001E71 RID: 7793
		private Timer _retryTimer;

		// Token: 0x04001E72 RID: 7794
		private Dictionary<string, int> _appDomainKeyHash;

		// Token: 0x04001E73 RID: 7795
		private readonly int _objectID = Interlocked.Increment(ref SqlDependencyProcessDispatcher.SqlConnectionContainer._objectTypeCount);

		// Token: 0x04001E74 RID: 7796
		private static int _objectTypeCount;
	}

	// Token: 0x0200033E RID: 830
	private class SqlNotificationParser
	{
		// Token: 0x060033D1 RID: 13265 RVA: 0x0013EEA4 File Offset: 0x0013E2A4
		internal static SqlNotification ProcessMessage(SqlXml xmlMessage)
		{
			SqlNotification result;
			using (XmlReader xmlReader = xmlMessage.CreateReader())
			{
				string empty = string.Empty;
				SqlDependencyProcessDispatcher.SqlNotificationParser.MessageAttributes messageAttributes = SqlDependencyProcessDispatcher.SqlNotificationParser.MessageAttributes.None;
				SqlNotificationType type = SqlNotificationType.Unknown;
				SqlNotificationInfo info = SqlNotificationInfo.Unknown;
				SqlNotificationSource source = SqlNotificationSource.Unknown;
				string key = string.Empty;
				xmlReader.Read();
				if (XmlNodeType.Element == xmlReader.NodeType && "QueryNotification" == xmlReader.LocalName && 3 <= xmlReader.AttributeCount)
				{
					while (SqlDependencyProcessDispatcher.SqlNotificationParser.MessageAttributes.All != messageAttributes && xmlReader.MoveToNextAttribute())
					{
						try
						{
							string localName = xmlReader.LocalName;
							if (!(localName == "type"))
							{
								if (!(localName == "source"))
								{
									if (localName == "info")
									{
										try
										{
											string value = xmlReader.Value;
											if (!(value == "set options"))
											{
												if (!(value == "previous invalid"))
												{
													if (!(value == "query template limit"))
													{
														SqlNotificationInfo sqlNotificationInfo = (SqlNotificationInfo)Enum.Parse(typeof(SqlNotificationInfo), value, true);
														if (Enum.IsDefined(typeof(SqlNotificationInfo), sqlNotificationInfo))
														{
															info = sqlNotificationInfo;
														}
													}
													else
													{
														info = SqlNotificationInfo.TemplateLimit;
													}
												}
												else
												{
													info = SqlNotificationInfo.PreviousFire;
												}
											}
											else
											{
												info = SqlNotificationInfo.Options;
											}
										}
										catch (Exception e)
										{
											if (!ADP.IsCatchableExceptionType(e))
											{
												throw;
											}
											ADP.TraceExceptionWithoutRethrow(e);
										}
										messageAttributes |= SqlDependencyProcessDispatcher.SqlNotificationParser.MessageAttributes.Info;
									}
								}
								else
								{
									try
									{
										SqlNotificationSource sqlNotificationSource = (SqlNotificationSource)Enum.Parse(typeof(SqlNotificationSource), xmlReader.Value, true);
										if (Enum.IsDefined(typeof(SqlNotificationSource), sqlNotificationSource))
										{
											source = sqlNotificationSource;
										}
									}
									catch (Exception e2)
									{
										if (!ADP.IsCatchableExceptionType(e2))
										{
											throw;
										}
										ADP.TraceExceptionWithoutRethrow(e2);
									}
									messageAttributes |= SqlDependencyProcessDispatcher.SqlNotificationParser.MessageAttributes.Source;
								}
							}
							else
							{
								try
								{
									SqlNotificationType sqlNotificationType = (SqlNotificationType)Enum.Parse(typeof(SqlNotificationType), xmlReader.Value, true);
									if (Enum.IsDefined(typeof(SqlNotificationType), sqlNotificationType))
									{
										type = sqlNotificationType;
									}
								}
								catch (Exception e3)
								{
									if (!ADP.IsCatchableExceptionType(e3))
									{
										throw;
									}
									ADP.TraceExceptionWithoutRethrow(e3);
								}
								messageAttributes |= SqlDependencyProcessDispatcher.SqlNotificationParser.MessageAttributes.Type;
							}
						}
						catch (ArgumentException e4)
						{
							ADP.TraceExceptionWithoutRethrow(e4);
							Bid.Trace("<sc.SqlDependencyProcessDispatcher.ProcessMessage|DEP|ERR> Exception thrown - Enum.Parse failed to parse the value '%ls' of the attribute '%ls'.\n", xmlReader.Value, xmlReader.LocalName);
							return null;
						}
					}
					if (SqlDependencyProcessDispatcher.SqlNotificationParser.MessageAttributes.All != messageAttributes)
					{
						Bid.Trace("<sc.SqlDependencyProcessDispatcher.ProcessMessage|DEP|ERR> Not all expected attributes in Message; messageAttributes = '%d'.\n", (int)messageAttributes);
						result = null;
					}
					else if (!xmlReader.Read())
					{
						Bid.Trace("<sc.SqlDependencyProcessDispatcher.ProcessMessage|DEP|ERR> unexpected Read failure on xml or unexpected structure of xml.\n");
						result = null;
					}
					else if (XmlNodeType.Element != xmlReader.NodeType || string.Compare(xmlReader.LocalName, "Message", StringComparison.OrdinalIgnoreCase) != 0)
					{
						Bid.Trace("<sc.SqlDependencyProcessDispatcher.ProcessMessage|DEP|ERR> unexpected Read failure on xml or unexpected structure of xml.\n");
						result = null;
					}
					else if (!xmlReader.Read())
					{
						Bid.Trace("<sc.SqlDependencyProcessDispatcher.ProcessMessage|DEP|ERR> unexpected Read failure on xml or unexpected structure of xml.\n");
						result = null;
					}
					else if (xmlReader.NodeType != XmlNodeType.Text)
					{
						Bid.Trace("<sc.SqlDependencyProcessDispatcher.ProcessMessage|DEP|ERR> unexpected Read failure on xml or unexpected structure of xml.\n");
						result = null;
					}
					else
					{
						using (XmlTextReader xmlTextReader = new XmlTextReader(xmlReader.Value, XmlNodeType.Element, null))
						{
							if (!xmlTextReader.Read())
							{
								Bid.Trace("<sc.SqlDependencyProcessDispatcher.ProcessMessage|DEP|ERR> unexpected Read failure on xml or unexpected structure of xml.\n");
								return null;
							}
							if (xmlTextReader.NodeType != XmlNodeType.Text)
							{
								Bid.Trace("<sc.SqlDependencyProcessDispatcher.ProcessMessage|DEP|ERR> unexpected Read failure on xml or unexpected structure of xml.\n");
								return null;
							}
							key = xmlTextReader.Value;
							xmlTextReader.Close();
						}
						result = new SqlNotification(info, source, type, key);
					}
				}
				else
				{
					Bid.Trace("<sc.SqlDependencyProcessDispatcher.ProcessMessage|DEP|ERR> unexpected Read failure on xml or unexpected structure of xml.\n");
					result = null;
				}
			}
			return result;
		}

		// Token: 0x04001E75 RID: 7797
		private const string RootNode = "QueryNotification";

		// Token: 0x04001E76 RID: 7798
		private const string MessageNode = "Message";

		// Token: 0x04001E77 RID: 7799
		private const string InfoAttribute = "info";

		// Token: 0x04001E78 RID: 7800
		private const string SourceAttribute = "source";

		// Token: 0x04001E79 RID: 7801
		private const string TypeAttribute = "type";

		// Token: 0x02000471 RID: 1137
		[Flags]
		private enum MessageAttributes
		{
			// Token: 0x04002382 RID: 9090
			None = 0,
			// Token: 0x04002383 RID: 9091
			Type = 1,
			// Token: 0x04002384 RID: 9092
			Source = 2,
			// Token: 0x04002385 RID: 9093
			Info = 4,
			// Token: 0x04002386 RID: 9094
			All = 7
		}
	}

	// Token: 0x0200033F RID: 831
	private class SqlConnectionContainerHashHelper
	{
		// Token: 0x060033D3 RID: 13267 RVA: 0x0013F280 File Offset: 0x0013E680
		internal SqlConnectionContainerHashHelper(DbConnectionPoolIdentity identity, string connectionString, string queue, SqlConnectionStringBuilder connectionStringBuilder)
		{
			this._identity = identity;
			this._connectionString = connectionString;
			this._queue = queue;
			this._connectionStringBuilder = connectionStringBuilder;
		}

		// Token: 0x17000836 RID: 2102
		// (get) Token: 0x060033D4 RID: 13268 RVA: 0x0013F2B0 File Offset: 0x0013E6B0
		internal SqlConnectionStringBuilder ConnectionStringBuilder
		{
			get
			{
				return this._connectionStringBuilder;
			}
		}

		// Token: 0x17000837 RID: 2103
		// (get) Token: 0x060033D5 RID: 13269 RVA: 0x0013F2C4 File Offset: 0x0013E6C4
		internal DbConnectionPoolIdentity Identity
		{
			get
			{
				return this._identity;
			}
		}

		// Token: 0x17000838 RID: 2104
		// (get) Token: 0x060033D6 RID: 13270 RVA: 0x0013F2D8 File Offset: 0x0013E6D8
		internal string Queue
		{
			get
			{
				return this._queue;
			}
		}

		// Token: 0x060033D7 RID: 13271 RVA: 0x0013F2EC File Offset: 0x0013E6EC
		public override bool Equals(object value)
		{
			SqlDependencyProcessDispatcher.SqlConnectionContainerHashHelper sqlConnectionContainerHashHelper = (SqlDependencyProcessDispatcher.SqlConnectionContainerHashHelper)value;
			bool result;
			if (sqlConnectionContainerHashHelper == null)
			{
				result = false;
			}
			else if (this == sqlConnectionContainerHashHelper)
			{
				result = true;
			}
			else if ((this._identity != null && sqlConnectionContainerHashHelper._identity == null) || (this._identity == null && sqlConnectionContainerHashHelper._identity != null))
			{
				result = false;
			}
			else if (this._identity == null && sqlConnectionContainerHashHelper._identity == null)
			{
				result = (sqlConnectionContainerHashHelper._connectionString == this._connectionString && string.Equals(sqlConnectionContainerHashHelper._queue, this._queue, StringComparison.OrdinalIgnoreCase));
			}
			else
			{
				result = (sqlConnectionContainerHashHelper._identity.Equals(this._identity) && sqlConnectionContainerHashHelper._connectionString == this._connectionString && string.Equals(sqlConnectionContainerHashHelper._queue, this._queue, StringComparison.OrdinalIgnoreCase));
			}
			return result;
		}

		// Token: 0x060033D8 RID: 13272 RVA: 0x0013F3C0 File Offset: 0x0013E7C0
		public override int GetHashCode()
		{
			int num = 0;
			if (this._identity != null)
			{
				num = this._identity.GetHashCode();
			}
			if (this._queue != null)
			{
				num = this._connectionString.GetHashCode() + this._queue.GetHashCode() + num;
			}
			else
			{
				num = this._connectionString.GetHashCode() + num;
			}
			return num;
		}

		// Token: 0x04001E7A RID: 7802
		private DbConnectionPoolIdentity _identity;

		// Token: 0x04001E7B RID: 7803
		private string _connectionString;

		// Token: 0x04001E7C RID: 7804
		private string _queue;

		// Token: 0x04001E7D RID: 7805
		private SqlConnectionStringBuilder _connectionStringBuilder;
	}
}
