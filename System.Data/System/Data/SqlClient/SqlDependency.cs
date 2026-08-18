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
	// Token: 0x020002E4 RID: 740
	public sealed class SqlDependency
	{
		// Token: 0x17000610 RID: 1552
		// (get) Token: 0x06002685 RID: 9861 RVA: 0x002A3998 File Offset: 0x002A2D98
		internal int ObjectID
		{
			get
			{
				return this._objectID;
			}
		}

		// Token: 0x06002686 RID: 9862 RVA: 0x002A39B8 File Offset: 0x002A2DB8
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public SqlDependency() : this(null, null, 0)
		{
		}

		// Token: 0x06002687 RID: 9863 RVA: 0x002A39D8 File Offset: 0x002A2DD8
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public SqlDependency(SqlCommand command) : this(command, null, 0)
		{
		}

		// Token: 0x06002688 RID: 9864 RVA: 0x002A39F8 File Offset: 0x002A2DF8
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

		// Token: 0x17000611 RID: 1553
		// (get) Token: 0x06002689 RID: 9865 RVA: 0x002A3AE8 File Offset: 0x002A2EE8
		[ResDescription("SqlDependency_HasChanges")]
		[ResCategory("DataCategory_Data")]
		public bool HasChanges
		{
			get
			{
				return this._dependencyFired;
			}
		}

		// Token: 0x17000612 RID: 1554
		// (get) Token: 0x0600268A RID: 9866 RVA: 0x002A3B08 File Offset: 0x002A2F08
		[ResCategory("DataCategory_Data")]
		[ResDescription("SqlDependency_Id")]
		public string Id
		{
			get
			{
				return this._id;
			}
		}

		// Token: 0x17000613 RID: 1555
		// (get) Token: 0x0600268B RID: 9867 RVA: 0x002A3B28 File Offset: 0x002A2F28
		internal static string AppDomainKey
		{
			get
			{
				return SqlDependency._appDomainKey;
			}
		}

		// Token: 0x17000614 RID: 1556
		// (get) Token: 0x0600268C RID: 9868 RVA: 0x002A3B48 File Offset: 0x002A2F48
		internal DateTime ExpirationTime
		{
			get
			{
				return this._expirationTime;
			}
		}

		// Token: 0x17000615 RID: 1557
		// (get) Token: 0x0600268D RID: 9869 RVA: 0x002A3B68 File Offset: 0x002A2F68
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

		// Token: 0x17000616 RID: 1558
		// (get) Token: 0x0600268E RID: 9870 RVA: 0x002A3B88 File Offset: 0x002A2F88
		internal static SqlDependencyProcessDispatcher ProcessDispatcher
		{
			get
			{
				return SqlDependency._processDispatcher;
			}
		}

		// Token: 0x17000617 RID: 1559
		// (get) Token: 0x0600268F RID: 9871 RVA: 0x002A3BA8 File Offset: 0x002A2FA8
		internal List<string> ServerList
		{
			get
			{
				return this._serverList;
			}
		}

		// Token: 0x17000618 RID: 1560
		// (get) Token: 0x06002690 RID: 9872 RVA: 0x002A3BC8 File Offset: 0x002A2FC8
		internal int Timeout
		{
			get
			{
				return this._timeout;
			}
		}

		// Token: 0x1400002E RID: 46
		// (add) Token: 0x06002691 RID: 9873 RVA: 0x002A3BE8 File Offset: 0x002A2FE8
		// (remove) Token: 0x06002692 RID: 9874 RVA: 0x002A3CC8 File Offset: 0x002A30C8
		[ResCategory("DataCategory_Data")]
		[ResDescription("SqlDependency_OnChange")]
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
						lock (this._eventHandlerLock)
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
						lock (this._eventHandlerLock)
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

		// Token: 0x06002693 RID: 9875 RVA: 0x002A3D68 File Offset: 0x002A3168
		[ResDescription("SqlDependency_AddCommandDependency")]
		[ResCategory("DataCategory_Data")]
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

		// Token: 0x06002694 RID: 9876 RVA: 0x002A3DC8 File Offset: 0x002A31C8
		[ReflectionPermission(SecurityAction.Assert, MemberAccess = true)]
		private static ObjectHandle CreateProcessDispatcher(_AppDomain masterDomain)
		{
			return masterDomain.CreateInstance(SqlDependency._assemblyName, SqlDependency._typeName);
		}

		// Token: 0x06002695 RID: 9877 RVA: 0x002A3DE8 File Offset: 0x002A31E8
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

		// Token: 0x06002696 RID: 9878 RVA: 0x002A3EE8 File Offset: 0x002A32E8
		[SecurityPermission(SecurityAction.Assert, Flags = SecurityPermissionFlag.RemotingConfiguration)]
		private static ObjRef GetObjRef(SqlDependencyProcessDispatcher _processDispatcher)
		{
			return RemotingServices.Marshal(_processDispatcher);
		}

		// Token: 0x06002697 RID: 9879 RVA: 0x002A3F08 File Offset: 0x002A3308
		[SecurityPermission(SecurityAction.Assert, Flags = SecurityPermissionFlag.SerializationFormatter)]
		private static void GetSerializedObject(ObjRef objRef, BinaryFormatter formatter, MemoryStream stream)
		{
			formatter.Serialize(stream, objRef);
		}

		// Token: 0x06002698 RID: 9880 RVA: 0x002A3F28 File Offset: 0x002A3328
		[SecurityPermission(SecurityAction.Assert, Flags = SecurityPermissionFlag.SerializationFormatter)]
		private static SqlDependencyProcessDispatcher GetDeserializedObject(BinaryFormatter formatter, MemoryStream stream)
		{
			object obj = formatter.Deserialize(stream);
			return (SqlDependencyProcessDispatcher)obj;
		}

		// Token: 0x06002699 RID: 9881 RVA: 0x002A3F48 File Offset: 0x002A3348
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public static bool Start(string connectionString)
		{
			return SqlDependency.Start(connectionString, null, true);
		}

		// Token: 0x0600269A RID: 9882 RVA: 0x002A3F68 File Offset: 0x002A3368
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public static bool Start(string connectionString, string queue)
		{
			return SqlDependency.Start(connectionString, queue, false);
		}

		// Token: 0x0600269B RID: 9883 RVA: 0x002A3F88 File Offset: 0x002A3388
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
					lock (SqlDependency._startStopLock)
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
								bool flag3 = false;
								RuntimeHelpers.PrepareConstrainedRegions();
								try
								{
									flag2 = SqlDependency._processDispatcher.StartWithDefault(connectionString, out text, out identity, out userName, out text2, ref text3, SqlDependency._appDomainKey, SqlDependencyPerAppDomainDispatcher.SingletonInstance, out flag, out flag3);
									Bid.NotificationsTrace("<sc.SqlDependency.Start|DEP> Start (defaults) returned: '%d', with service: '%ls', server: '%ls', database: '%ls'\n", flag2, text3, text, text2);
									goto IL_15D;
								}
								finally
								{
									if (flag3 && !flag)
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
							IL_15D:;
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

		// Token: 0x0600269C RID: 9884 RVA: 0x002A41A8 File Offset: 0x002A35A8
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public static bool Stop(string connectionString)
		{
			return SqlDependency.Stop(connectionString, null, true, false);
		}

		// Token: 0x0600269D RID: 9885 RVA: 0x002A41C8 File Offset: 0x002A35C8
		[HostProtection(SecurityAction.LinkDemand, ExternalThreading = true)]
		public static bool Stop(string connectionString, string queue)
		{
			return SqlDependency.Stop(connectionString, queue, false, false);
		}

		// Token: 0x0600269E RID: 9886 RVA: 0x002A41E8 File Offset: 0x002A35E8
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
					lock (SqlDependency._startStopLock)
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
									bool flag2 = false;
									RuntimeHelpers.PrepareConstrainedRegions();
									try
									{
										flag = SqlDependency._processDispatcher.Stop(connectionString, out server, out identity, out userName, out database, ref service, SqlDependency._appDomainKey, out flag2);
										goto IL_106;
									}
									finally
									{
										if (flag2 && !startFailed)
										{
											SqlDependency.IdentityUserNamePair identityUser = new SqlDependency.IdentityUserNamePair(identity, userName);
											SqlDependency.DatabaseServicePair databaseService = new SqlDependency.DatabaseServicePair(database, service);
											SqlDependency.RemoveFromServerUserHash(server, identityUser, databaseService);
										}
									}
								}
								bool flag3 = false;
								flag = SqlDependency._processDispatcher.Stop(connectionString, out server, out identity, out userName, out database, ref queue, SqlDependency._appDomainKey, out flag3);
								IL_106:;
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

		// Token: 0x0600269F RID: 9887 RVA: 0x002A4398 File Offset: 0x002A3798
		private static bool AddToServerUserHash(string server, SqlDependency.IdentityUserNamePair identityUser, SqlDependency.DatabaseServicePair databaseService)
		{
			IntPtr intPtr;
			Bid.NotificationsScopeEnter(out intPtr, "<sc.SqlDependency.AddToServerUserHash|DEP> server: '%ls', database: '%ls', service: '%ls'", server, databaseService.Database, databaseService.Service);
			bool result;
			try
			{
				bool flag = false;
				lock (SqlDependency._serverUserHash)
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

		// Token: 0x060026A0 RID: 9888 RVA: 0x002A44A8 File Offset: 0x002A38A8
		private static void RemoveFromServerUserHash(string server, SqlDependency.IdentityUserNamePair identityUser, SqlDependency.DatabaseServicePair databaseService)
		{
			IntPtr intPtr;
			Bid.NotificationsScopeEnter(out intPtr, "<sc.SqlDependency.RemoveFromServerUserHash|DEP> server: '%ls', database: '%ls', service: '%ls'", server, databaseService.Database, databaseService.Service);
			try
			{
				lock (SqlDependency._serverUserHash)
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

		// Token: 0x060026A1 RID: 9889 RVA: 0x002A45C8 File Offset: 0x002A39C8
		internal static string GetDefaultComposedOptions(string server, string failoverServer, SqlDependency.IdentityUserNamePair identityUser, string database)
		{
			IntPtr intPtr;
			Bid.NotificationsScopeEnter(out intPtr, "<sc.SqlDependency.GetDefaultComposedOptions|DEP> server: '%ls', failoverServer: '%ls', database: '%ls'", server, failoverServer, database);
			string result;
			try
			{
				string text;
				lock (SqlDependency._serverUserHash)
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
							if (enumerator.MoveNext())
							{
								KeyValuePair<SqlDependency.IdentityUserNamePair, List<SqlDependency.DatabaseServicePair>> keyValuePair = enumerator.Current;
								list = keyValuePair.Value;
							}
							goto IL_E7;
						}
					}
					list = dictionary[identityUser];
					IL_E7:
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
						databaseServicePair = (SqlDependency.DatabaseServicePair)array[0];
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

		// Token: 0x060026A2 RID: 9890 RVA: 0x002A47E8 File Offset: 0x002A3BE8
		internal void AddToServerList(string server)
		{
			IntPtr intPtr;
			Bid.NotificationsScopeEnter(out intPtr, "<sc.SqlDependency.AddToServerList|DEP> %d#, server: '%ls'", this.ObjectID, server);
			try
			{
				lock (this._serverList)
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

		// Token: 0x060026A3 RID: 9891 RVA: 0x002A4898 File Offset: 0x002A3C98
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

		// Token: 0x060026A4 RID: 9892 RVA: 0x002A4918 File Offset: 0x002A3D18
		internal void Invalidate(SqlNotificationType type, SqlNotificationInfo info, SqlNotificationSource source)
		{
			IntPtr intPtr;
			Bid.NotificationsScopeEnter(out intPtr, "<sc.SqlDependency.Invalidate|DEP> %d#", this.ObjectID);
			try
			{
				List<SqlDependency.EventContextPair> list = null;
				lock (this._eventHandlerLock)
				{
					if (this._dependencyFired && SqlNotificationInfo.AlreadyChanged != info && SqlNotificationSource.Client != source)
					{
						Bid.NotificationsTrace("<sc.SqlDependency.Invalidate|DEP|ERR> ERROR - notification received twice - we should never enter this state!");
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

		// Token: 0x060026A5 RID: 9893 RVA: 0x002A4A28 File Offset: 0x002A3E28
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

		// Token: 0x060026A6 RID: 9894 RVA: 0x002A4AE8 File Offset: 0x002A3EE8
		private void AddCommandInternal(SqlCommand cmd)
		{
			if (cmd != null)
			{
				IntPtr intPtr;
				Bid.NotificationsScopeEnter(out intPtr, "<sc.SqlDependency.AddCommandInternal|DEP> %d#, SqlCommand: %d#", this.ObjectID, cmd.ObjectID);
				try
				{
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
						lock (this._eventHandlerLock)
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

		// Token: 0x060026A7 RID: 9895 RVA: 0x002A4C08 File Offset: 0x002A4008
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

		// Token: 0x060026A8 RID: 9896 RVA: 0x002A4D98 File Offset: 0x002A4198
		internal static string FixupServiceOrDatabaseName(string name)
		{
			if (!ADP.IsEmpty(name))
			{
				return "\"" + name.Replace("\"", "\"\"") + "\"";
			}
			return name;
		}

		// Token: 0x04001855 RID: 6229
		internal const Bid.ApiGroup NotificationsTracePoints = Bid.ApiGroup.Dependency;

		// Token: 0x04001856 RID: 6230
		private readonly string _id = Guid.NewGuid().ToString() + ";" + SqlDependency._appDomainKey;

		// Token: 0x04001857 RID: 6231
		private string _options;

		// Token: 0x04001858 RID: 6232
		private int _timeout;

		// Token: 0x04001859 RID: 6233
		private bool _dependencyFired;

		// Token: 0x0400185A RID: 6234
		private List<SqlDependency.EventContextPair> _eventList = new List<SqlDependency.EventContextPair>();

		// Token: 0x0400185B RID: 6235
		private object _eventHandlerLock = new object();

		// Token: 0x0400185C RID: 6236
		private DateTime _expirationTime = DateTime.MaxValue;

		// Token: 0x0400185D RID: 6237
		private List<string> _serverList = new List<string>();

		// Token: 0x0400185E RID: 6238
		private static object _startStopLock = new object();

		// Token: 0x0400185F RID: 6239
		private static readonly string _appDomainKey = Guid.NewGuid().ToString();

		// Token: 0x04001860 RID: 6240
		private static Dictionary<string, Dictionary<SqlDependency.IdentityUserNamePair, List<SqlDependency.DatabaseServicePair>>> _serverUserHash = new Dictionary<string, Dictionary<SqlDependency.IdentityUserNamePair, List<SqlDependency.DatabaseServicePair>>>(StringComparer.OrdinalIgnoreCase);

		// Token: 0x04001861 RID: 6241
		private static SqlDependencyProcessDispatcher _processDispatcher = null;

		// Token: 0x04001862 RID: 6242
		private static readonly string _assemblyName = typeof(SqlDependencyProcessDispatcher).Assembly.FullName;

		// Token: 0x04001863 RID: 6243
		private static readonly string _typeName = typeof(SqlDependencyProcessDispatcher).FullName;

		// Token: 0x04001864 RID: 6244
		private readonly int _objectID = Interlocked.Increment(ref SqlDependency._objectTypeCount);

		// Token: 0x04001865 RID: 6245
		private static int _objectTypeCount;

		// Token: 0x020002E5 RID: 741
		internal class IdentityUserNamePair
		{
			// Token: 0x060026AA RID: 9898 RVA: 0x002A4E58 File Offset: 0x002A4258
			internal IdentityUserNamePair(DbConnectionPoolIdentity identity, string userName)
			{
				this._identity = identity;
				this._userName = userName;
			}

			// Token: 0x17000619 RID: 1561
			// (get) Token: 0x060026AB RID: 9899 RVA: 0x002A4E88 File Offset: 0x002A4288
			internal DbConnectionPoolIdentity Identity
			{
				get
				{
					return this._identity;
				}
			}

			// Token: 0x1700061A RID: 1562
			// (get) Token: 0x060026AC RID: 9900 RVA: 0x002A4EA8 File Offset: 0x002A42A8
			internal string UserName
			{
				get
				{
					return this._userName;
				}
			}

			// Token: 0x060026AD RID: 9901 RVA: 0x002A4EC8 File Offset: 0x002A42C8
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

			// Token: 0x060026AE RID: 9902 RVA: 0x002A4F28 File Offset: 0x002A4328
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

			// Token: 0x04001866 RID: 6246
			private DbConnectionPoolIdentity _identity;

			// Token: 0x04001867 RID: 6247
			private string _userName;
		}

		// Token: 0x020002E6 RID: 742
		private class DatabaseServicePair
		{
			// Token: 0x060026AF RID: 9903 RVA: 0x002A4F68 File Offset: 0x002A4368
			internal DatabaseServicePair(string database, string service)
			{
				this._database = database;
				this._service = service;
			}

			// Token: 0x1700061B RID: 1563
			// (get) Token: 0x060026B0 RID: 9904 RVA: 0x002A4F98 File Offset: 0x002A4398
			internal string Database
			{
				get
				{
					return this._database;
				}
			}

			// Token: 0x1700061C RID: 1564
			// (get) Token: 0x060026B1 RID: 9905 RVA: 0x002A4FB8 File Offset: 0x002A43B8
			internal string Service
			{
				get
				{
					return this._service;
				}
			}

			// Token: 0x060026B2 RID: 9906 RVA: 0x002A4FD8 File Offset: 0x002A43D8
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

			// Token: 0x060026B3 RID: 9907 RVA: 0x002A5018 File Offset: 0x002A4418
			public override int GetHashCode()
			{
				return this._database.GetHashCode();
			}

			// Token: 0x04001868 RID: 6248
			private string _database;

			// Token: 0x04001869 RID: 6249
			private string _service;
		}

		// Token: 0x020002E7 RID: 743
		internal class EventContextPair
		{
			// Token: 0x060026B4 RID: 9908 RVA: 0x002A5038 File Offset: 0x002A4438
			internal EventContextPair(OnChangeEventHandler eventHandler, SqlDependency dependency)
			{
				this._eventHandler = eventHandler;
				this._context = ExecutionContext.Capture();
				this._dependency = dependency;
			}

			// Token: 0x060026B5 RID: 9909 RVA: 0x002A5068 File Offset: 0x002A4468
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

			// Token: 0x060026B6 RID: 9910 RVA: 0x002A50A8 File Offset: 0x002A44A8
			public override int GetHashCode()
			{
				return this._eventHandler.GetHashCode();
			}

			// Token: 0x060026B7 RID: 9911 RVA: 0x002A50C8 File Offset: 0x002A44C8
			internal void Invoke(SqlNotificationEventArgs args)
			{
				this._args = args;
				ExecutionContext.Run(this._context, SqlDependency.EventContextPair._contextCallback, this);
			}

			// Token: 0x060026B8 RID: 9912 RVA: 0x002A50F8 File Offset: 0x002A44F8
			private static void InvokeCallback(object eventContextPair)
			{
				SqlDependency.EventContextPair eventContextPair2 = (SqlDependency.EventContextPair)eventContextPair;
				eventContextPair2._eventHandler(eventContextPair2._dependency, eventContextPair2._args);
			}

			// Token: 0x0400186A RID: 6250
			private OnChangeEventHandler _eventHandler;

			// Token: 0x0400186B RID: 6251
			private ExecutionContext _context;

			// Token: 0x0400186C RID: 6252
			private SqlDependency _dependency;

			// Token: 0x0400186D RID: 6253
			private SqlNotificationEventArgs _args;

			// Token: 0x0400186E RID: 6254
			private static ContextCallback _contextCallback = new ContextCallback(SqlDependency.EventContextPair.InvokeCallback);
		}
	}
}
