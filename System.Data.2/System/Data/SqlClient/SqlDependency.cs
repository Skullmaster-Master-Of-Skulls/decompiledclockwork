using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.ProviderBase;
using System.Data.Sql;
using System.Globalization;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.Remoting;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Permissions;
using System.Text;
using System.Threading;
using System.Xml;

namespace System.Data.SqlClient
{
	// Token: 0x020001C8 RID: 456
	public sealed class SqlDependency
	{
		// Token: 0x17000460 RID: 1120
		// (get) Token: 0x06001CB6 RID: 7350 RVA: 0x000CA554 File Offset: 0x000C9954
		internal int ObjectID
		{
			get
			{
				return this._objectID;
			}
		}

		// Token: 0x06001CB7 RID: 7351 RVA: 0x000CA568 File Offset: 0x000C9968
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public SqlDependency() : this(null, null, 0)
		{
		}

		// Token: 0x06001CB8 RID: 7352 RVA: 0x000CA580 File Offset: 0x000C9980
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public SqlDependency(SqlCommand command) : this(command, null, 0)
		{
		}

		// Token: 0x06001CB9 RID: 7353 RVA: 0x000CA598 File Offset: 0x000C9998
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public SqlDependency(SqlCommand command, string options, int timeout)
		{
			IntPtr intPtr;
			Bid.NotificationsScopeEnter(out intPtr, "<sc.SqlDependency|DEP> %d#, options: '%ls', timeout: '%d'", this.ObjectID, options, timeout);
			try
			{
				if (InOutOfProcHelper.InProc)
				{
					throw SQL.SqlDepCannotBeCreatedInProc();
				}
				if (timeout < 0)
				{
					throw SQL.InvalidSqlDependencyTimeout("timeout");
				}
				this._timeout = timeout;
				if (options != null)
				{
					this._options = options;
				}
				this.AddCommandInternal(command);
				SqlDependencyPerAppDomainDispatcher.SingletonInstance.AddDependencyEntry(this);
			}
			finally
			{
				Bid.ScopeLeave(ref intPtr);
			}
		}

		// Token: 0x17000461 RID: 1121
		// (get) Token: 0x06001CBA RID: 7354 RVA: 0x000CA688 File Offset: 0x000C9A88
		[ResCategory("DataCategory_Data")]
		[ResDescription("SqlDependency_HasChanges")]
		public bool HasChanges
		{
			get
			{
				return this._dependencyFired;
			}
		}

		// Token: 0x17000462 RID: 1122
		// (get) Token: 0x06001CBB RID: 7355 RVA: 0x000CA69C File Offset: 0x000C9A9C
		[ResCategory("DataCategory_Data")]
		[ResDescription("SqlDependency_Id")]
		public string Id
		{
			get
			{
				return this._id;
			}
		}

		// Token: 0x17000463 RID: 1123
		// (get) Token: 0x06001CBC RID: 7356 RVA: 0x000CA6B0 File Offset: 0x000C9AB0
		internal static string AppDomainKey
		{
			get
			{
				return SqlDependency._appDomainKey;
			}
		}

		// Token: 0x17000464 RID: 1124
		// (get) Token: 0x06001CBD RID: 7357 RVA: 0x000CA6C4 File Offset: 0x000C9AC4
		internal DateTime ExpirationTime
		{
			get
			{
				return this._expirationTime;
			}
		}

		// Token: 0x17000465 RID: 1125
		// (get) Token: 0x06001CBE RID: 7358 RVA: 0x000CA6D8 File Offset: 0x000C9AD8
		internal string Options
		{
			get
			{
				string result = null;
				if (this._options != null)
				{
					result = this._options;
				}
				return result;
			}
		}

		// Token: 0x17000466 RID: 1126
		// (get) Token: 0x06001CBF RID: 7359 RVA: 0x000CA6F8 File Offset: 0x000C9AF8
		internal static SqlDependencyProcessDispatcher ProcessDispatcher
		{
			get
			{
				return SqlDependency._processDispatcher;
			}
		}

		// Token: 0x17000467 RID: 1127
		// (get) Token: 0x06001CC0 RID: 7360 RVA: 0x000CA70C File Offset: 0x000C9B0C
		internal int Timeout
		{
			get
			{
				return this._timeout;
			}
		}

		// Token: 0x14000025 RID: 37
		// (add) Token: 0x06001CC1 RID: 7361 RVA: 0x000CA720 File Offset: 0x000C9B20
		// (remove) Token: 0x06001CC2 RID: 7362 RVA: 0x000CA800 File Offset: 0x000C9C00
		[ResDescription("SqlDependency_OnChange")]
		[ResCategory("DataCategory_Data")]
		public event OnChangeEventHandler OnChange
		{
			add
			{
				IntPtr intPtr;
				Bid.NotificationsScopeEnter(out intPtr, "<sc.SqlDependency.OnChange-Add|DEP> %d#", this.ObjectID);
				try
				{
					if (value != null)
					{
						SqlNotificationEventArgs sqlNotificationEventArgs = null;
						object eventHandlerLock = this._eventHandlerLock;
						lock (eventHandlerLock)
						{
							if (this._dependencyFired)
							{
								Bid.NotificationsTrace("<sc.SqlDependency.OnChange-Add|DEP> Dependency already fired, firing new event.\n");
								sqlNotificationEventArgs = new SqlNotificationEventArgs(SqlNotificationType.Subscribe, SqlNotificationInfo.AlreadyChanged, SqlNotificationSource.Client);
							}
							else
							{
								Bid.NotificationsTrace("<sc.SqlDependency.OnChange-Add|DEP> Dependency has not fired, adding new event.\n");
								SqlDependency.EventContextPair item = new SqlDependency.EventContextPair(value, this);
								if (this._eventList.Contains(item))
								{
									throw SQL.SqlDependencyEventNoDuplicate();
								}
								this._eventList.Add(item);
							}
						}
						if (sqlNotificationEventArgs != null)
						{
							value(this, sqlNotificationEventArgs);
						}
					}
				}
				finally
				{
					Bid.ScopeLeave(ref intPtr);
				}
			}
			remove
			{
				IntPtr intPtr;
				Bid.NotificationsScopeEnter(out intPtr, "<sc.SqlDependency.OnChange-Remove|DEP> %d#", this.ObjectID);
				try
				{
					if (value != null)
					{
						SqlDependency.EventContextPair item = new SqlDependency.EventContextPair(value, this);
						object eventHandlerLock = this._eventHandlerLock;
						lock (eventHandlerLock)
						{
							int num = this._eventList.IndexOf(item);
							if (0 <= num)
							{
								this._eventList.RemoveAt(num);
							}
						}
					}
				}
				finally
				{
					Bid.ScopeLeave(ref intPtr);
				}
			}
		}

		// Token: 0x06001CC3 RID: 7363 RVA: 0x000CA8A4 File Offset: 0x000C9CA4
		[ResCategory("DataCategory_Data")]
		[ResDescription("SqlDependency_AddCommandDependency")]
		public void AddCommandDependency(SqlCommand command)
		{
			IntPtr intPtr;
			Bid.NotificationsScopeEnter(out intPtr, "<sc.SqlDependency.AddCommandDependency|DEP> %d#", this.ObjectID);
			try
			{
				if (command == null)
				{
					throw ADP.ArgumentNull("command");
				}
				this.AddCommandInternal(command);
			}
			finally
			{
				Bid.ScopeLeave(ref intPtr);
			}
		}

		// Token: 0x06001CC4 RID: 7364 RVA: 0x000CA900 File Offset: 0x000C9D00
		[ReflectionPermission(SecurityAction.Assert, MemberAccess = true)]
		private static ObjectHandle CreateProcessDispatcher(_AppDomain masterDomain)
		{
			return masterDomain.CreateInstance(SqlDependency._assemblyName, SqlDependency._typeName);
		}

		// Token: 0x06001CC5 RID: 7365 RVA: 0x000CA920 File Offset: 0x000C9D20
		private static void ObtainProcessDispatcher()
		{
			byte[] data = SNINativeMethodWrapper.GetData();
			if (data != null)
			{
				Bid.NotificationsTrace("<sc.SqlDependency.ObtainProcessDispatcher|DEP> nativeStorage not null, obtaining existing dispatcher AppDomain and ProcessDispatcher.\n");
				BinaryFormatter formatter = new BinaryFormatter();
				MemoryStream stream = new MemoryStream(data);
				SqlDependency._processDispatcher = SqlDependency.GetDeserializedObject(formatter, stream);
				Bid.NotificationsTrace("<sc.SqlDependency.ObtainProcessDispatcher|DEP> processDispatcher obtained, ID: %d\n", SqlDependency._processDispatcher.ObjectID);
				return;
			}
			Bid.NotificationsTrace("<sc.SqlDependency.ObtainProcessDispatcher|DEP> nativeStorage null, obtaining dispatcher AppDomain and creating ProcessDispatcher.\n");
			_AppDomain defaultAppDomain = SNINativeMethodWrapper.GetDefaultAppDomain();
			if (defaultAppDomain == null)
			{
				Bid.NotificationsTrace("<sc.SqlDependency.ObtainProcessDispatcher|DEP|ERR> ERROR - unable to obtain default AppDomain!\n");
				throw ADP.InternalError(ADP.InternalErrorCode.SqlDependencyProcessDispatcherFailureAppDomain);
			}
			ObjectHandle objectHandle = SqlDependency.CreateProcessDispatcher(defaultAppDomain);
			if (objectHandle == null)
			{
				Bid.NotificationsTrace("<sc.SqlDependency.ObtainProcessDispatcher|DEP|ERR> ERROR - AppDomain.CreateInstance returned null!\n");
				throw ADP.InternalError(ADP.InternalErrorCode.SqlDependencyProcessDispatcherFailureCreateInstance);
			}
			SqlDependencyProcessDispatcher sqlDependencyProcessDispatcher = (SqlDependencyProcessDispatcher)objectHandle.Unwrap();
			if (sqlDependencyProcessDispatcher != null)
			{
				SqlDependency._processDispatcher = sqlDependencyProcessDispatcher.SingletonProcessDispatcher;
				ObjRef objRef = SqlDependency.GetObjRef(SqlDependency._processDispatcher);
				BinaryFormatter formatter2 = new BinaryFormatter();
				MemoryStream memoryStream = new MemoryStream();
				SqlDependency.GetSerializedObject(objRef, formatter2, memoryStream);
				SNINativeMethodWrapper.SetData(memoryStream.GetBuffer());
				return;
			}
			Bid.NotificationsTrace("<sc.SqlDependency.ObtainProcessDispatcher|DEP|ERR> ERROR - ObjectHandle.Unwrap returned null!\n");
			throw ADP.InternalError(ADP.InternalErrorCode.SqlDependencyObtainProcessDispatcherFailureObjectHandle);
		}

		// Token: 0x06001CC6 RID: 7366 RVA: 0x000CAA18 File Offset: 0x000C9E18
		[SecurityPermission(SecurityAction.Assert, Flags = SecurityPermissionFlag.RemotingConfiguration)]
		private static ObjRef GetObjRef(SqlDependencyProcessDispatcher _processDispatcher)
		{
			return RemotingServices.Marshal(_processDispatcher);
		}

		// Token: 0x06001CC7 RID: 7367 RVA: 0x000CAA2C File Offset: 0x000C9E2C
		[SecurityPermission(SecurityAction.Assert, Flags = SecurityPermissionFlag.SerializationFormatter)]
		private static void GetSerializedObject(ObjRef objRef, BinaryFormatter formatter, MemoryStream stream)
		{
			formatter.Serialize(stream, objRef);
		}

		// Token: 0x06001CC8 RID: 7368 RVA: 0x000CAA44 File Offset: 0x000C9E44
		[SecurityPermission(SecurityAction.Assert, Flags = SecurityPermissionFlag.SerializationFormatter)]
		private static SqlDependencyProcessDispatcher GetDeserializedObject(BinaryFormatter formatter, MemoryStream stream)
		{
			object obj = formatter.Deserialize(stream);
			return (SqlDependencyProcessDispatcher)obj;
		}

		// Token: 0x06001CC9 RID: 7369 RVA: 0x000CAA60 File Offset: 0x000C9E60
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public static bool Start(string connectionString)
		{
			return SqlDependency.Start(connectionString, null, true);
		}

		// Token: 0x06001CCA RID: 7370 RVA: 0x000CAA78 File Offset: 0x000C9E78
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public static bool Start(string connectionString, string queue)
		{
			return SqlDependency.Start(connectionString, queue, false);
		}

		// Token: 0x06001CCB RID: 7371 RVA: 0x000CAA90 File Offset: 0x000C9E90
		internal static bool Start(string connectionString, string queue, bool useDefaults)
		{
			IntPtr intPtr;
			Bid.NotificationsScopeEnter(out intPtr, "<sc.SqlDependency.Start|DEP> AppDomainKey: '%ls', queue: '%ls'", SqlDependency.AppDomainKey, queue);
			bool result;
			try
			{
				if (InOutOfProcHelper.InProc)
				{
					throw SQL.SqlDepCannotBeCreatedInProc();
				}
				if (ADP.IsEmpty(connectionString))
				{
					if (connectionString == null)
					{
						throw ADP.ArgumentNull("connectionString");
					}
					throw ADP.Argument("connectionString");
				}
				else
				{
					if (!useDefaults && ADP.IsEmpty(queue))
					{
						useDefaults = true;
						queue = null;
					}
					SqlConnectionString sqlConnectionString = new SqlConnectionString(connectionString);
					sqlConnectionString.DemandPermission();
					if (sqlConnectionString.LocalDBInstance != null)
					{
						LocalDBAPI.DemandLocalDBPermissions();
					}
					bool flag = false;
					bool flag2 = false;
					object startStopLock = SqlDependency._startStopLock;
					lock (startStopLock)
					{
						try
						{
							if (SqlDependency._processDispatcher == null)
							{
								SqlDependency.ObtainProcessDispatcher();
							}
							if (useDefaults)
							{
								string text = null;
								DbConnectionPoolIdentity identity = null;
								string userName = null;
								string text2 = null;
								string text3 = null;
								bool flag4 = false;
								RuntimeHelpers.PrepareConstrainedRegions();
								try
								{
									flag2 = SqlDependency._processDispatcher.StartWithDefault(connectionString, out text, out identity, out userName, out text2, ref text3, SqlDependency._appDomainKey, SqlDependencyPerAppDomainDispatcher.SingletonInstance, out flag, out flag4);
									Bid.NotificationsTrace("<sc.SqlDependency.Start|DEP> Start (defaults) returned: '%d', with service: '%ls', server: '%ls', database: '%ls'\n", flag2, text3, text, text2);
									goto IL_163;
								}
								finally
								{
									if (flag4 && !flag)
									{
										SqlDependency.IdentityUserNamePair identityUser = new SqlDependency.IdentityUserNamePair(identity, userName);
										SqlDependency.DatabaseServicePair databaseService = new SqlDependency.DatabaseServicePair(text2, text3);
										if (!SqlDependency.AddToServerUserHash(text, identityUser, databaseService))
										{
											try
											{
												SqlDependency.Stop(connectionString, queue, useDefaults, true);
											}
											catch (Exception e)
											{
												if (!ADP.IsCatchableExceptionType(e))
												{
													throw;
												}
												ADP.TraceExceptionWithoutRethrow(e);
												Bid.NotificationsTrace("<sc.SqlDependency.Start|DEP|ERR> Exception occurred from Stop() after duplicate was found on Start().\n");
											}
											throw SQL.SqlDependencyDuplicateStart();
										}
									}
								}
							}
							flag2 = SqlDependency._processDispatcher.Start(connectionString, queue, SqlDependency._appDomainKey, SqlDependencyPerAppDomainDispatcher.SingletonInstance);
							Bid.NotificationsTrace("<sc.SqlDependency.Start|DEP> Start (user provided queue) returned: '%d'\n", flag2);
							IL_163:;
						}
						catch (Exception e2)
						{
							if (!ADP.IsCatchableExceptionType(e2))
							{
								throw;
							}
							ADP.TraceExceptionWithoutRethrow(e2);
							Bid.NotificationsTrace("<sc.SqlDependency.Start|DEP|ERR> Exception occurred from _processDispatcher.Start(...), calling Invalidate(...).\n");
							throw;
						}
					}
					result = flag2;
				}
			}
			finally
			{
				Bid.ScopeLeave(ref intPtr);
			}
			return result;
		}

		// Token: 0x06001CCC RID: 7372 RVA: 0x000CACBC File Offset: 0x000CA0BC
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public static bool Stop(string connectionString)
		{
			return SqlDependency.Stop(connectionString, null, true, false);
		}

		// Token: 0x06001CCD RID: 7373 RVA: 0x000CACD4 File Offset: 0x000CA0D4
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public static bool Stop(string connectionString, string queue)
		{
			return SqlDependency.Stop(connectionString, queue, false, false);
		}

		// Token: 0x06001CCE RID: 7374 RVA: 0x000CACEC File Offset: 0x000CA0EC
		internal static bool Stop(string connectionString, string queue, bool useDefaults, bool startFailed)
		{
			IntPtr intPtr;
			Bid.NotificationsScopeEnter(out intPtr, "<sc.SqlDependency.Stop|DEP> AppDomainKey: '%ls', queue: '%ls'", SqlDependency.AppDomainKey, queue);
			bool result;
			try
			{
				if (InOutOfProcHelper.InProc)
				{
					throw SQL.SqlDepCannotBeCreatedInProc();
				}
				if (ADP.IsEmpty(connectionString))
				{
					if (connectionString == null)
					{
						throw ADP.ArgumentNull("connectionString");
					}
					throw ADP.Argument("connectionString");
				}
				else
				{
					if (!useDefaults && ADP.IsEmpty(queue))
					{
						useDefaults = true;
						queue = null;
					}
					SqlConnectionString sqlConnectionString = new SqlConnectionString(connectionString);
					sqlConnectionString.DemandPermission();
					if (sqlConnectionString.LocalDBInstance != null)
					{
						LocalDBAPI.DemandLocalDBPermissions();
					}
					bool flag = false;
					object startStopLock = SqlDependency._startStopLock;
					lock (startStopLock)
					{
						if (SqlDependency._processDispatcher != null)
						{
							try
							{
								string server = null;
								DbConnectionPoolIdentity identity = null;
								string userName = null;
								string database = null;
								string service = null;
								if (useDefaults)
								{
									bool flag3 = false;
									RuntimeHelpers.PrepareConstrainedRegions();
									try
									{
										flag = SqlDependency._processDispatcher.Stop(connectionString, out server, out identity, out userName, out database, ref service, SqlDependency._appDomainKey, out flag3);
										goto IL_10A;
									}
									finally
									{
										if (flag3 && !startFailed)
										{
											SqlDependency.IdentityUserNamePair identityUser = new SqlDependency.IdentityUserNamePair(identity, userName);
											SqlDependency.DatabaseServicePair databaseService = new SqlDependency.DatabaseServicePair(database, service);
											SqlDependency.RemoveFromServerUserHash(server, identityUser, databaseService);
										}
									}
								}
								bool flag4 = false;
								flag = SqlDependency._processDispatcher.Stop(connectionString, out server, out identity, out userName, out database, ref queue, SqlDependency._appDomainKey, out flag4);
								IL_10A:;
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
					result = flag;
				}
			}
			finally
			{
				Bid.ScopeLeave(ref intPtr);
			}
			return result;
		}

		// Token: 0x06001CCF RID: 7375 RVA: 0x000CAE98 File Offset: 0x000CA298
		private static bool AddToServerUserHash(string server, SqlDependency.IdentityUserNamePair identityUser, SqlDependency.DatabaseServicePair databaseService)
		{
			IntPtr intPtr;
			Bid.NotificationsScopeEnter(out intPtr, "<sc.SqlDependency.AddToServerUserHash|DEP> server: '%ls', database: '%ls', service: '%ls'", server, databaseService.Database, databaseService.Service);
			bool result;
			try
			{
				bool flag = false;
				Dictionary<string, Dictionary<SqlDependency.IdentityUserNamePair, List<SqlDependency.DatabaseServicePair>>> serverUserHash = SqlDependency._serverUserHash;
				lock (serverUserHash)
				{
					Dictionary<SqlDependency.IdentityUserNamePair, List<SqlDependency.DatabaseServicePair>> dictionary;
					if (!SqlDependency._serverUserHash.ContainsKey(server))
					{
						Bid.NotificationsTrace("<sc.SqlDependency.AddToServerUserHash|DEP> Hash did not contain server, adding.\n");
						dictionary = new Dictionary<SqlDependency.IdentityUserNamePair, List<SqlDependency.DatabaseServicePair>>();
						SqlDependency._serverUserHash.Add(server, dictionary);
					}
					else
					{
						dictionary = SqlDependency._serverUserHash[server];
					}
					List<SqlDependency.DatabaseServicePair> list;
					if (!dictionary.ContainsKey(identityUser))
					{
						Bid.NotificationsTrace("<sc.SqlDependency.AddToServerUserHash|DEP> Hash contained server but not user, adding user.\n");
						list = new List<SqlDependency.DatabaseServicePair>();
						dictionary.Add(identityUser, list);
					}
					else
					{
						list = dictionary[identityUser];
					}
					if (!list.Contains(databaseService))
					{
						Bid.NotificationsTrace("<sc.SqlDependency.AddToServerUserHash|DEP> Adding database.\n");
						list.Add(databaseService);
						flag = true;
					}
					else
					{
						Bid.NotificationsTrace("<sc.SqlDependency.AddToServerUserHash|DEP|ERR> ERROR - hash already contained server, user, and database - we will throw!.\n");
					}
				}
				result = flag;
			}
			finally
			{
				Bid.ScopeLeave(ref intPtr);
			}
			return result;
		}

		// Token: 0x06001CD0 RID: 7376 RVA: 0x000CAFAC File Offset: 0x000CA3AC
		private static void RemoveFromServerUserHash(string server, SqlDependency.IdentityUserNamePair identityUser, SqlDependency.DatabaseServicePair databaseService)
		{
			IntPtr intPtr;
			Bid.NotificationsScopeEnter(out intPtr, "<sc.SqlDependency.RemoveFromServerUserHash|DEP> server: '%ls', database: '%ls', service: '%ls'", server, databaseService.Database, databaseService.Service);
			try
			{
				Dictionary<string, Dictionary<SqlDependency.IdentityUserNamePair, List<SqlDependency.DatabaseServicePair>>> serverUserHash = SqlDependency._serverUserHash;
				lock (serverUserHash)
				{
					if (SqlDependency._serverUserHash.ContainsKey(server))
					{
						Dictionary<SqlDependency.IdentityUserNamePair, List<SqlDependency.DatabaseServicePair>> dictionary = SqlDependency._serverUserHash[server];
						if (dictionary.ContainsKey(identityUser))
						{
							List<SqlDependency.DatabaseServicePair> list = dictionary[identityUser];
							int num = list.IndexOf(databaseService);
							if (num >= 0)
							{
								Bid.NotificationsTrace("<sc.SqlDependency.RemoveFromServerUserHash|DEP> Hash contained server, user, and database - removing database.\n");
								list.RemoveAt(num);
								if (list.Count == 0)
								{
									Bid.NotificationsTrace("<sc.SqlDependency.RemoveFromServerUserHash|DEP> databaseServiceList count 0, removing the list for this server and user.\n");
									dictionary.Remove(identityUser);
									if (dictionary.Count == 0)
									{
										Bid.NotificationsTrace("<sc.SqlDependency.RemoveFromServerUserHash|DEP> identityDatabaseHash count 0, removing the hash for this server.\n");
										SqlDependency._serverUserHash.Remove(server);
									}
								}
							}
							else
							{
								Bid.NotificationsTrace("<sc.SqlDependency.RemoveFromServerUserHash|DEP|ERR> ERROR - hash contained server and user but not database!\n");
							}
						}
						else
						{
							Bid.NotificationsTrace("<sc.SqlDependency.RemoveFromServerUserHash|DEP|ERR> ERROR - hash contained server but not user!\n");
						}
					}
					else
					{
						Bid.NotificationsTrace("<sc.SqlDependency.RemoveFromServerUserHash|DEP|ERR> ERROR - hash did not contain server!\n");
					}
				}
			}
			finally
			{
				Bid.ScopeLeave(ref intPtr);
			}
		}

		// Token: 0x06001CD1 RID: 7377 RVA: 0x000CB0D4 File Offset: 0x000CA4D4
		internal static string GetDefaultComposedOptions(string server, string failoverServer, SqlDependency.IdentityUserNamePair identityUser, string database)
		{
			IntPtr intPtr;
			Bid.NotificationsScopeEnter(out intPtr, "<sc.SqlDependency.GetDefaultComposedOptions|DEP> server: '%ls', failoverServer: '%ls', database: '%ls'", server, failoverServer, database);
			string result;
			try
			{
				Dictionary<string, Dictionary<SqlDependency.IdentityUserNamePair, List<SqlDependency.DatabaseServicePair>>> serverUserHash = SqlDependency._serverUserHash;
				string text;
				lock (serverUserHash)
				{
					if (!SqlDependency._serverUserHash.ContainsKey(server))
					{
						if (SqlDependency._serverUserHash.Count == 0)
						{
							Bid.NotificationsTrace("<sc.SqlDependency.GetDefaultComposedOptions|DEP|ERR> ERROR - no start calls have been made, about to throw.\n");
							throw SQL.SqlDepDefaultOptionsButNoStart();
						}
						if (ADP.IsEmpty(failoverServer) || !SqlDependency._serverUserHash.ContainsKey(failoverServer))
						{
							Bid.NotificationsTrace("<sc.SqlDependency.GetDefaultComposedOptions|DEP|ERR> ERROR - not listening to this server, about to throw.\n");
							throw SQL.SqlDependencyNoMatchingServerStart();
						}
						Bid.NotificationsTrace("<sc.SqlDependency.GetDefaultComposedOptions|DEP> using failover server instead\n");
						server = failoverServer;
					}
					Dictionary<SqlDependency.IdentityUserNamePair, List<SqlDependency.DatabaseServicePair>> dictionary = SqlDependency._serverUserHash[server];
					List<SqlDependency.DatabaseServicePair> list = null;
					if (!dictionary.ContainsKey(identityUser))
					{
						if (dictionary.Count > 1)
						{
							Bid.NotificationsTrace("<sc.SqlDependency.GetDefaultComposedOptions|DEP|ERR> ERROR - not listening for this user, but listening to more than one other user, about to throw.\n");
							throw SQL.SqlDependencyNoMatchingServerStart();
						}
						using (Dictionary<SqlDependency.IdentityUserNamePair, List<SqlDependency.DatabaseServicePair>>.Enumerator enumerator = dictionary.GetEnumerator())
						{
							if (!enumerator.MoveNext())
							{
								goto IL_ED;
							}
							KeyValuePair<SqlDependency.IdentityUserNamePair, List<SqlDependency.DatabaseServicePair>> keyValuePair = enumerator.Current;
							list = keyValuePair.Value;
							goto IL_ED;
						}
					}
					list = dictionary[identityUser];
					IL_ED:
					SqlDependency.DatabaseServicePair item = new SqlDependency.DatabaseServicePair(database, null);
					SqlDependency.DatabaseServicePair databaseServicePair = null;
					int num = list.IndexOf(item);
					if (num != -1)
					{
						databaseServicePair = list[num];
					}
					if (databaseServicePair != null)
					{
						database = SqlDependency.FixupServiceOrDatabaseName(databaseServicePair.Database);
						string str = SqlDependency.FixupServiceOrDatabaseName(databaseServicePair.Service);
						text = "Service=" + str + ";Local Database=" + database;
					}
					else
					{
						if (list.Count != 1)
						{
							Bid.NotificationsTrace("<sc.SqlDependency.GetDefaultComposedOptions|DEP|ERR> ERROR - SqlDependency.Start called multiple times for this server/user, but no matching database.\n");
							throw SQL.SqlDependencyNoMatchingServerDatabaseStart();
						}
						object[] array = list.ToArray();
						object[] array2 = array;
						databaseServicePair = (SqlDependency.DatabaseServicePair)array2[0];
						string str2 = SqlDependency.FixupServiceOrDatabaseName(databaseServicePair.Database);
						string str3 = SqlDependency.FixupServiceOrDatabaseName(databaseServicePair.Service);
						text = "Service=" + str3 + ";Local Database=" + str2;
					}
				}
				Bid.NotificationsTrace("<sc.SqlDependency.GetDefaultComposedOptions|DEP> resulting options: '%ls'.\n", text);
				result = text;
			}
			finally
			{
				Bid.ScopeLeave(ref intPtr);
			}
			return result;
		}

		// Token: 0x06001CD2 RID: 7378 RVA: 0x000CB2F8 File Offset: 0x000CA6F8
		internal void AddToServerList(string server)
		{
			IntPtr intPtr;
			Bid.NotificationsScopeEnter(out intPtr, "<sc.SqlDependency.AddToServerList|DEP> %d#, server: '%ls'", this.ObjectID, server);
			try
			{
				List<string> serverList = this._serverList;
				lock (serverList)
				{
					int num = this._serverList.BinarySearch(server, StringComparer.OrdinalIgnoreCase);
					if (0 > num)
					{
						Bid.NotificationsTrace("<sc.SqlDependency.AddToServerList|DEP> Server not present in hashtable, adding server: '%ls'.\n", server);
						num = ~num;
						this._serverList.Insert(num, server);
					}
				}
			}
			finally
			{
				Bid.ScopeLeave(ref intPtr);
			}
		}

		// Token: 0x06001CD3 RID: 7379 RVA: 0x000CB3A4 File Offset: 0x000CA7A4
		internal bool ContainsServer(string server)
		{
			List<string> serverList = this._serverList;
			bool result;
			lock (serverList)
			{
				result = this._serverList.Contains(server);
			}
			return result;
		}

		// Token: 0x06001CD4 RID: 7380 RVA: 0x000CB3F8 File Offset: 0x000CA7F8
		internal string ComputeHashAndAddToDispatcher(SqlCommand command)
		{
			IntPtr intPtr;
			Bid.NotificationsScopeEnter(out intPtr, "<sc.SqlDependency.ComputeHashAndAddToDispatcher|DEP> %d#, SqlCommand: %d#", this.ObjectID, command.ObjectID);
			string result;
			try
			{
				string commandHash = this.ComputeCommandHash(command.Connection.ConnectionString, command);
				string text = SqlDependencyPerAppDomainDispatcher.SingletonInstance.AddCommandEntry(commandHash, this);
				Bid.NotificationsTrace("<sc.SqlDependency.ComputeHashAndAddToDispatcher|DEP> computed id string: '%ls'.\n", text);
				result = text;
			}
			finally
			{
				Bid.ScopeLeave(ref intPtr);
			}
			return result;
		}

		// Token: 0x06001CD5 RID: 7381 RVA: 0x000CB474 File Offset: 0x000CA874
		internal void Invalidate(SqlNotificationType type, SqlNotificationInfo info, SqlNotificationSource source)
		{
			IntPtr intPtr;
			Bid.NotificationsScopeEnter(out intPtr, "<sc.SqlDependency.Invalidate|DEP> %d#", this.ObjectID);
			try
			{
				List<SqlDependency.EventContextPair> list = null;
				object eventHandlerLock = this._eventHandlerLock;
				lock (eventHandlerLock)
				{
					if (this._dependencyFired && SqlNotificationInfo.AlreadyChanged != info && SqlNotificationSource.Client != source)
					{
						if (this.ExpirationTime < DateTime.UtcNow)
						{
							Bid.NotificationsTrace("<sc.SqlDependency.Invalidate|DEP> ignore notification received after timeout!");
						}
						else
						{
							Bid.NotificationsTrace("<sc.SqlDependency.Invalidate|DEP|ERR> ERROR - notification received twice - we should never enter this state!");
						}
					}
					else
					{
						this._dependencyFired = true;
						list = this._eventList;
						this._eventList = new List<SqlDependency.EventContextPair>();
					}
				}
				if (list != null)
				{
					Bid.NotificationsTrace("<sc.SqlDependency.Invalidate|DEP> Firing events.\n");
					foreach (SqlDependency.EventContextPair eventContextPair in list)
					{
						eventContextPair.Invoke(new SqlNotificationEventArgs(type, info, source));
					}
				}
			}
			finally
			{
				Bid.ScopeLeave(ref intPtr);
			}
		}

		// Token: 0x06001CD6 RID: 7382 RVA: 0x000CB5A4 File Offset: 0x000CA9A4
		internal void StartTimer(SqlNotificationRequest notificationRequest)
		{
			IntPtr intPtr;
			Bid.NotificationsScopeEnter(out intPtr, "<sc.SqlDependency.StartTimer|DEP> %d#", this.ObjectID);
			try
			{
				if (this._expirationTime == DateTime.MaxValue)
				{
					Bid.NotificationsTrace("<sc.SqlDependency.StartTimer|DEP> We've timed out, executing logic.\n");
					int num = 432000;
					if (this._timeout != 0)
					{
						num = this._timeout;
					}
					if (notificationRequest != null && notificationRequest.Timeout < num && notificationRequest.Timeout != 0)
					{
						num = notificationRequest.Timeout;
					}
					this._expirationTime = DateTime.UtcNow.AddSeconds((double)num);
					SqlDependencyPerAppDomainDispatcher.SingletonInstance.StartTimer(this);
				}
			}
			finally
			{
				Bid.ScopeLeave(ref intPtr);
			}
		}

		// Token: 0x06001CD7 RID: 7383 RVA: 0x000CB658 File Offset: 0x000CAA58
		private void AddCommandInternal(SqlCommand cmd)
		{
			if (cmd != null)
			{
				IntPtr intPtr;
				Bid.NotificationsScopeEnter(out intPtr, "<sc.SqlDependency.AddCommandInternal|DEP> %d#, SqlCommand: %d#", this.ObjectID, cmd.ObjectID);
				try
				{
					SqlConnection connection = cmd.Connection;
					if (cmd.Notification != null)
					{
						if (cmd._sqlDep == null || cmd._sqlDep != this)
						{
							Bid.NotificationsTrace("<sc.SqlDependency.AddCommandInternal|DEP|ERR> ERROR - throwing command has existing SqlNotificationRequest exception.\n");
							throw SQL.SqlCommandHasExistingSqlNotificationRequest();
						}
					}
					else
					{
						bool flag = false;
						object eventHandlerLock = this._eventHandlerLock;
						lock (eventHandlerLock)
						{
							if (!this._dependencyFired)
							{
								cmd.Notification = new SqlNotificationRequest();
								cmd.Notification.Timeout = this._timeout;
								if (this._options != null)
								{
									cmd.Notification.Options = this._options;
								}
								cmd._sqlDep = this;
							}
							else if (this._eventList.Count == 0)
							{
								Bid.NotificationsTrace("<sc.SqlDependency.AddCommandInternal|DEP|ERR> ERROR - firing events, though it is unexpected we have events at this point.\n");
								flag = true;
							}
						}
						if (flag)
						{
							this.Invalidate(SqlNotificationType.Subscribe, SqlNotificationInfo.AlreadyChanged, SqlNotificationSource.Client);
						}
					}
				}
				finally
				{
					Bid.ScopeLeave(ref intPtr);
				}
			}
		}

		// Token: 0x06001CD8 RID: 7384 RVA: 0x000CB780 File Offset: 0x000CAB80
		private string ComputeCommandHash(string connectionString, SqlCommand command)
		{
			IntPtr intPtr;
			Bid.NotificationsScopeEnter(out intPtr, "<sc.SqlDependency.ComputeCommandHash|DEP> %d#, SqlCommand: %d#", this.ObjectID, command.ObjectID);
			string result;
			try
			{
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.AppendFormat("{0};{1}", connectionString, command.CommandText);
				for (int i = 0; i < command.Parameters.Count; i++)
				{
					object value = command.Parameters[i].Value;
					if (value == null || value == DBNull.Value)
					{
						stringBuilder.Append("; NULL");
					}
					else
					{
						Type type = value.GetType();
						if (type == typeof(byte[]))
						{
							stringBuilder.Append(";");
							byte[] array = (byte[])value;
							for (int j = 0; j < array.Length; j++)
							{
								stringBuilder.Append(array[j].ToString("x2", CultureInfo.InvariantCulture));
							}
						}
						else if (type == typeof(char[]))
						{
							stringBuilder.Append((char[])value);
						}
						else if (type == typeof(XmlReader))
						{
							stringBuilder.Append(";");
							stringBuilder.Append(Guid.NewGuid().ToString());
						}
						else
						{
							stringBuilder.Append(";");
							stringBuilder.Append(value.ToString());
						}
					}
				}
				string text = stringBuilder.ToString();
				Bid.NotificationsTrace("<sc.SqlDependency.ComputeCommandHash|DEP> ComputeCommandHash result: '%ls'.\n", text);
				result = text;
			}
			finally
			{
				Bid.ScopeLeave(ref intPtr);
			}
			return result;
		}

		// Token: 0x06001CD9 RID: 7385 RVA: 0x000CB920 File Offset: 0x000CAD20
		internal static string FixupServiceOrDatabaseName(string name)
		{
			if (!ADP.IsEmpty(name))
			{
				return "\"" + name.Replace("\"", "\"\"") + "\"";
			}
			return name;
		}

		// Token: 0x0400104E RID: 4174
		private readonly string _id = Guid.NewGuid().ToString() + ";" + SqlDependency._appDomainKey;

		// Token: 0x0400104F RID: 4175
		private string _options;

		// Token: 0x04001050 RID: 4176
		private int _timeout;

		// Token: 0x04001051 RID: 4177
		private bool _dependencyFired;

		// Token: 0x04001052 RID: 4178
		private List<SqlDependency.EventContextPair> _eventList = new List<SqlDependency.EventContextPair>();

		// Token: 0x04001053 RID: 4179
		private object _eventHandlerLock = new object();

		// Token: 0x04001054 RID: 4180
		private DateTime _expirationTime = DateTime.MaxValue;

		// Token: 0x04001055 RID: 4181
		private List<string> _serverList = new List<string>();

		// Token: 0x04001056 RID: 4182
		private static object _startStopLock = new object();

		// Token: 0x04001057 RID: 4183
		private static readonly string _appDomainKey = Guid.NewGuid().ToString();

		// Token: 0x04001058 RID: 4184
		private static Dictionary<string, Dictionary<SqlDependency.IdentityUserNamePair, List<SqlDependency.DatabaseServicePair>>> _serverUserHash = new Dictionary<string, Dictionary<SqlDependency.IdentityUserNamePair, List<SqlDependency.DatabaseServicePair>>>(StringComparer.OrdinalIgnoreCase);

		// Token: 0x04001059 RID: 4185
		private static SqlDependencyProcessDispatcher _processDispatcher = null;

		// Token: 0x0400105A RID: 4186
		private static readonly string _assemblyName = typeof(SqlDependencyProcessDispatcher).Assembly.FullName;

		// Token: 0x0400105B RID: 4187
		private static readonly string _typeName = typeof(SqlDependencyProcessDispatcher).FullName;

		// Token: 0x0400105C RID: 4188
		internal const Bid.ApiGroup NotificationsTracePoints = Bid.ApiGroup.Dependency;

		// Token: 0x0400105D RID: 4189
		private readonly int _objectID = Interlocked.Increment(ref SqlDependency._objectTypeCount);

		// Token: 0x0400105E RID: 4190
		private static int _objectTypeCount;

		// Token: 0x020003C0 RID: 960
		internal class IdentityUserNamePair
		{
			// Token: 0x06003512 RID: 13586 RVA: 0x00143CC4 File Offset: 0x001430C4
			internal IdentityUserNamePair(DbConnectionPoolIdentity identity, string userName)
			{
				this._identity = identity;
				this._userName = userName;
			}

			// Token: 0x17000855 RID: 2133
			// (get) Token: 0x06003513 RID: 13587 RVA: 0x00143CE8 File Offset: 0x001430E8
			internal DbConnectionPoolIdentity Identity
			{
				get
				{
					return this._identity;
				}
			}

			// Token: 0x17000856 RID: 2134
			// (get) Token: 0x06003514 RID: 13588 RVA: 0x00143CFC File Offset: 0x001430FC
			internal string UserName
			{
				get
				{
					return this._userName;
				}
			}

			// Token: 0x06003515 RID: 13589 RVA: 0x00143D10 File Offset: 0x00143110
			public override bool Equals(object value)
			{
				SqlDependency.IdentityUserNamePair identityUserNamePair = (SqlDependency.IdentityUserNamePair)value;
				bool result = false;
				if (identityUserNamePair == null)
				{
					result = false;
				}
				else if (this == identityUserNamePair)
				{
					result = true;
				}
				else if (this._identity != null)
				{
					if (this._identity.Equals(identityUserNamePair._identity))
					{
						result = true;
					}
				}
				else if (this._userName == identityUserNamePair._userName)
				{
					result = true;
				}
				return result;
			}

			// Token: 0x06003516 RID: 13590 RVA: 0x00143D6C File Offset: 0x0014316C
			public override int GetHashCode()
			{
				int hashCode;
				if (this._identity != null)
				{
					hashCode = this._identity.GetHashCode();
				}
				else
				{
					hashCode = this._userName.GetHashCode();
				}
				return hashCode;
			}

			// Token: 0x040020C6 RID: 8390
			private DbConnectionPoolIdentity _identity;

			// Token: 0x040020C7 RID: 8391
			private string _userName;
		}

		// Token: 0x020003C1 RID: 961
		private class DatabaseServicePair
		{
			// Token: 0x06003517 RID: 13591 RVA: 0x00143DA0 File Offset: 0x001431A0
			internal DatabaseServicePair(string database, string service)
			{
				this._database = database;
				this._service = service;
			}

			// Token: 0x17000857 RID: 2135
			// (get) Token: 0x06003518 RID: 13592 RVA: 0x00143DC4 File Offset: 0x001431C4
			internal string Database
			{
				get
				{
					return this._database;
				}
			}

			// Token: 0x17000858 RID: 2136
			// (get) Token: 0x06003519 RID: 13593 RVA: 0x00143DD8 File Offset: 0x001431D8
			internal string Service
			{
				get
				{
					return this._service;
				}
			}

			// Token: 0x0600351A RID: 13594 RVA: 0x00143DEC File Offset: 0x001431EC
			public override bool Equals(object value)
			{
				SqlDependency.DatabaseServicePair databaseServicePair = (SqlDependency.DatabaseServicePair)value;
				bool result = false;
				if (databaseServicePair == null)
				{
					result = false;
				}
				else if (this == databaseServicePair)
				{
					result = true;
				}
				else if (this._database == databaseServicePair._database)
				{
					result = true;
				}
				return result;
			}

			// Token: 0x0600351B RID: 13595 RVA: 0x00143E28 File Offset: 0x00143228
			public override int GetHashCode()
			{
				return this._database.GetHashCode();
			}

			// Token: 0x040020C8 RID: 8392
			private string _database;

			// Token: 0x040020C9 RID: 8393
			private string _service;
		}

		// Token: 0x020003C2 RID: 962
		internal class EventContextPair
		{
			// Token: 0x0600351C RID: 13596 RVA: 0x00143E40 File Offset: 0x00143240
			internal EventContextPair(OnChangeEventHandler eventHandler, SqlDependency dependency)
			{
				this._eventHandler = eventHandler;
				this._context = ExecutionContext.Capture();
				this._dependency = dependency;
			}

			// Token: 0x0600351D RID: 13597 RVA: 0x00143E6C File Offset: 0x0014326C
			public override bool Equals(object value)
			{
				SqlDependency.EventContextPair eventContextPair = (SqlDependency.EventContextPair)value;
				bool result = false;
				if (eventContextPair == null)
				{
					result = false;
				}
				else if (this == eventContextPair)
				{
					result = true;
				}
				else if (this._eventHandler == eventContextPair._eventHandler)
				{
					result = true;
				}
				return result;
			}

			// Token: 0x0600351E RID: 13598 RVA: 0x00143EA8 File Offset: 0x001432A8
			public override int GetHashCode()
			{
				return this._eventHandler.GetHashCode();
			}

			// Token: 0x0600351F RID: 13599 RVA: 0x00143EC0 File Offset: 0x001432C0
			internal void Invoke(SqlNotificationEventArgs args)
			{
				this._args = args;
				ExecutionContext.Run(this._context, SqlDependency.EventContextPair._contextCallback, this);
			}

			// Token: 0x06003520 RID: 13600 RVA: 0x00143EE8 File Offset: 0x001432E8
			private static void InvokeCallback(object eventContextPair)
			{
				SqlDependency.EventContextPair eventContextPair2 = (SqlDependency.EventContextPair)eventContextPair;
				eventContextPair2._eventHandler(eventContextPair2._dependency, eventContextPair2._args);
			}

			// Token: 0x040020CA RID: 8394
			private OnChangeEventHandler _eventHandler;

			// Token: 0x040020CB RID: 8395
			private ExecutionContext _context;

			// Token: 0x040020CC RID: 8396
			private SqlDependency _dependency;

			// Token: 0x040020CD RID: 8397
			private SqlNotificationEventArgs _args;

			// Token: 0x040020CE RID: 8398
			private static ContextCallback _contextCallback = new ContextCallback(SqlDependency.EventContextPair.InvokeCallback);
		}
	}
}
