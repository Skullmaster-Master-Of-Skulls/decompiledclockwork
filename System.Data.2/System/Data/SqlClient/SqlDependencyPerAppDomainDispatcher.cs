using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Globalization;
using System.Threading;

namespace System.Data.SqlClient
{
	// Token: 0x020001C9 RID: 457
	internal class SqlDependencyPerAppDomainDispatcher : MarshalByRefObject
	{
		// Token: 0x17000468 RID: 1128
		// (get) Token: 0x06001CDB RID: 7387 RVA: 0x000CB9CC File Offset: 0x000CADCC
		internal int ObjectID
		{
			get
			{
				return this._objectID;
			}
		}

		// Token: 0x06001CDC RID: 7388 RVA: 0x000CB9E0 File Offset: 0x000CADE0
		private SqlDependencyPerAppDomainDispatcher()
		{
			IntPtr intPtr;
			Bid.NotificationsScopeEnter(out intPtr, "<sc.SqlDependencyPerAppDomainDispatcher|DEP> %d#", this.ObjectID);
			try
			{
				this._dependencyIdToDependencyHash = new Dictionary<string, SqlDependency>();
				this._notificationIdToDependenciesHash = new Dictionary<string, SqlDependencyPerAppDomainDispatcher.DependencyList>();
				this._commandHashToNotificationId = new Dictionary<string, string>();
				this._timeoutTimer = new Timer(new TimerCallback(SqlDependencyPerAppDomainDispatcher.TimeoutTimerCallback), null, -1, -1);
				AppDomain.CurrentDomain.DomainUnload += this.UnloadEventHandler;
			}
			finally
			{
				Bid.ScopeLeave(ref intPtr);
			}
		}

		// Token: 0x06001CDD RID: 7389 RVA: 0x000CBA8C File Offset: 0x000CAE8C
		public override object InitializeLifetimeService()
		{
			return null;
		}

		// Token: 0x06001CDE RID: 7390 RVA: 0x000CBA9C File Offset: 0x000CAE9C
		private void UnloadEventHandler(object sender, EventArgs e)
		{
			IntPtr intPtr;
			Bid.NotificationsScopeEnter(out intPtr, "<sc.SqlDependencyPerAppDomainDispatcher.UnloadEventHandler|DEP> %d#", this.ObjectID);
			try
			{
				SqlDependencyProcessDispatcher processDispatcher = SqlDependency.ProcessDispatcher;
				if (processDispatcher != null)
				{
					processDispatcher.QueueAppDomainUnloading(SqlDependency.AppDomainKey);
				}
			}
			finally
			{
				Bid.ScopeLeave(ref intPtr);
			}
		}

		// Token: 0x06001CDF RID: 7391 RVA: 0x000CBAF8 File Offset: 0x000CAEF8
		internal void AddDependencyEntry(SqlDependency dep)
		{
			IntPtr intPtr;
			Bid.NotificationsScopeEnter(out intPtr, "<sc.SqlDependencyPerAppDomainDispatcher.AddDependencyEntry|DEP> %d#, SqlDependency: %d#", this.ObjectID, dep.ObjectID);
			try
			{
				lock (this)
				{
					this._dependencyIdToDependencyHash.Add(dep.Id, dep);
				}
			}
			finally
			{
				Bid.ScopeLeave(ref intPtr);
			}
		}

		// Token: 0x06001CE0 RID: 7392 RVA: 0x000CBB88 File Offset: 0x000CAF88
		internal string AddCommandEntry(string commandHash, SqlDependency dep)
		{
			string text = string.Empty;
			IntPtr intPtr;
			Bid.NotificationsScopeEnter(out intPtr, "<sc.SqlDependencyPerAppDomainDispatcher.AddCommandEntry|DEP> %d#, commandHash: '%ls', SqlDependency: %d#", this.ObjectID, commandHash, dep.ObjectID);
			try
			{
				lock (this)
				{
					if (!this._dependencyIdToDependencyHash.ContainsKey(dep.Id))
					{
						Bid.NotificationsTrace("<sc.SqlDependencyPerAppDomainDispatcher.AddCommandEntry|DEP> Dependency not present in depId->dep hash, must have been invalidated.\n");
					}
					else if (this._commandHashToNotificationId.TryGetValue(commandHash, out text))
					{
						SqlDependencyPerAppDomainDispatcher.DependencyList dependencyList = null;
						if (!this._notificationIdToDependenciesHash.TryGetValue(text, out dependencyList))
						{
							throw ADP.InternalError(ADP.InternalErrorCode.SqlDependencyCommandHashIsNotAssociatedWithNotification);
						}
						if (!dependencyList.Contains(dep))
						{
							Bid.NotificationsTrace("<sc.SqlDependencyPerAppDomainDispatcher.AddCommandEntry|DEP> Dependency not present for commandHash, adding.\n");
							dependencyList.Add(dep);
						}
						else
						{
							Bid.NotificationsTrace("<sc.SqlDependencyPerAppDomainDispatcher.AddCommandEntry|DEP> Dependency already present for commandHash.\n");
						}
					}
					else
					{
						text = string.Format(CultureInfo.InvariantCulture, "{0};{1}", new object[]
						{
							SqlDependency.AppDomainKey,
							Guid.NewGuid().ToString("D", CultureInfo.InvariantCulture)
						});
						Bid.NotificationsTrace("<sc.SqlDependencyPerAppDomainDispatcher.AddCommandEntry|DEP> Creating new Dependencies list for commandHash.\n");
						SqlDependencyPerAppDomainDispatcher.DependencyList dependencyList2 = new SqlDependencyPerAppDomainDispatcher.DependencyList(commandHash);
						dependencyList2.Add(dep);
						try
						{
						}
						finally
						{
							this._commandHashToNotificationId.Add(commandHash, text);
							this._notificationIdToDependenciesHash.Add(text, dependencyList2);
						}
					}
				}
			}
			finally
			{
				Bid.ScopeLeave(ref intPtr);
			}
			return text;
		}

		// Token: 0x06001CE1 RID: 7393 RVA: 0x000CBD10 File Offset: 0x000CB110
		internal void InvalidateCommandID(SqlNotification sqlNotification)
		{
			IntPtr intPtr;
			Bid.NotificationsScopeEnter(out intPtr, "<sc.SqlDependencyPerAppDomainDispatcher.InvalidateCommandID|DEP> %d#, commandHash: '%ls'", this.ObjectID, sqlNotification.Key);
			try
			{
				List<SqlDependency> list = null;
				lock (this)
				{
					list = this.LookupCommandEntryWithRemove(sqlNotification.Key);
					if (list != null)
					{
						Bid.NotificationsTrace("<sc.SqlDependencyPerAppDomainDispatcher.InvalidateCommandID|DEP> commandHash found in hashtable.\n");
						using (List<SqlDependency>.Enumerator enumerator = list.GetEnumerator())
						{
							while (enumerator.MoveNext())
							{
								SqlDependency sqlDependency = enumerator.Current;
								this.LookupDependencyEntryWithRemove(sqlDependency.Id);
								this.RemoveDependencyFromCommandToDependenciesHash(sqlDependency);
							}
							goto IL_96;
						}
					}
					Bid.NotificationsTrace("<sc.SqlDependencyPerAppDomainDispatcher.InvalidateCommandID|DEP> commandHash NOT found in hashtable.\n");
				}
				IL_96:
				if (list != null)
				{
					foreach (SqlDependency sqlDependency2 in list)
					{
						Bid.NotificationsTrace("<sc.SqlDependencyPerAppDomainDispatcher.InvalidateCommandID|DEP> Dependency found in commandHash dependency ArrayList - calling invalidate.\n");
						try
						{
							sqlDependency2.Invalidate(sqlNotification.Type, sqlNotification.Info, sqlNotification.Source);
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

		// Token: 0x06001CE2 RID: 7394 RVA: 0x000CBEA0 File Offset: 0x000CB2A0
		internal void InvalidateServer(string server, SqlNotification sqlNotification)
		{
			IntPtr intPtr;
			Bid.NotificationsScopeEnter(out intPtr, "<sc.SqlDependencyPerAppDomainDispatcher.Invalidate|DEP> %d#, server: '%ls'", this.ObjectID, server);
			try
			{
				List<SqlDependency> list = new List<SqlDependency>();
				lock (this)
				{
					foreach (KeyValuePair<string, SqlDependency> keyValuePair in this._dependencyIdToDependencyHash)
					{
						SqlDependency value = keyValuePair.Value;
						if (value.ContainsServer(server))
						{
							list.Add(value);
						}
					}
					foreach (SqlDependency sqlDependency in list)
					{
						this.LookupDependencyEntryWithRemove(sqlDependency.Id);
						this.RemoveDependencyFromCommandToDependenciesHash(sqlDependency);
					}
				}
				foreach (SqlDependency sqlDependency2 in list)
				{
					try
					{
						sqlDependency2.Invalidate(sqlNotification.Type, sqlNotification.Info, sqlNotification.Source);
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
			finally
			{
				Bid.ScopeLeave(ref intPtr);
			}
		}

		// Token: 0x06001CE3 RID: 7395 RVA: 0x000CC064 File Offset: 0x000CB464
		internal SqlDependency LookupDependencyEntry(string id)
		{
			IntPtr intPtr;
			Bid.NotificationsScopeEnter(out intPtr, "<sc.SqlDependencyPerAppDomainDispatcher.LookupDependencyEntry|DEP> %d#, Key: '%ls'", this.ObjectID, id);
			SqlDependency result;
			try
			{
				if (id == null)
				{
					throw ADP.ArgumentNull("id");
				}
				if (ADP.IsEmpty(id))
				{
					throw SQL.SqlDependencyIdMismatch();
				}
				SqlDependency sqlDependency = null;
				lock (this)
				{
					if (this._dependencyIdToDependencyHash.ContainsKey(id))
					{
						sqlDependency = this._dependencyIdToDependencyHash[id];
					}
					else
					{
						Bid.NotificationsTrace("<sc.SqlDependencyPerAppDomainDispatcher.LookupDependencyEntry|DEP|ERR> ERROR - dependency ID mismatch - not throwing.\n");
					}
				}
				result = sqlDependency;
			}
			finally
			{
				Bid.ScopeLeave(ref intPtr);
			}
			return result;
		}

		// Token: 0x06001CE4 RID: 7396 RVA: 0x000CC124 File Offset: 0x000CB524
		private void LookupDependencyEntryWithRemove(string id)
		{
			IntPtr intPtr;
			Bid.NotificationsScopeEnter(out intPtr, "<sc.SqlDependencyPerAppDomainDispatcher.LookupDependencyEntryWithRemove|DEP> %d#, id: '%ls'", this.ObjectID, id);
			try
			{
				lock (this)
				{
					if (this._dependencyIdToDependencyHash.ContainsKey(id))
					{
						Bid.NotificationsTrace("<sc.SqlDependencyPerAppDomainDispatcher.LookupDependencyEntryWithRemove|DEP> Entry found in hashtable - removing.\n");
						this._dependencyIdToDependencyHash.Remove(id);
						if (this._dependencyIdToDependencyHash.Count == 0)
						{
							this._timeoutTimer.Change(-1, -1);
							this._SqlDependencyTimeOutTimerStarted = false;
						}
					}
					else
					{
						Bid.NotificationsTrace("<sc.SqlDependencyPerAppDomainDispatcher.LookupDependencyEntryWithRemove|DEP> Entry NOT found in hashtable.\n");
					}
				}
			}
			finally
			{
				Bid.ScopeLeave(ref intPtr);
			}
		}

		// Token: 0x06001CE5 RID: 7397 RVA: 0x000CC1F0 File Offset: 0x000CB5F0
		private List<SqlDependency> LookupCommandEntryWithRemove(string notificationId)
		{
			IntPtr intPtr;
			Bid.NotificationsScopeEnter(out intPtr, "<sc.SqlDependencyPerAppDomainDispatcher.LookupCommandEntryWithRemove|DEP> %d#, commandHash: '%ls'", this.ObjectID, notificationId);
			List<SqlDependency> result;
			try
			{
				SqlDependencyPerAppDomainDispatcher.DependencyList dependencyList = null;
				lock (this)
				{
					if (this._notificationIdToDependenciesHash.TryGetValue(notificationId, out dependencyList))
					{
						Bid.NotificationsTrace("<sc.SqlDependencyPerAppDomainDispatcher.LookupDependencyEntriesWithRemove|DEP> Entries found in hashtable - removing.\n");
						try
						{
							goto IL_73;
						}
						finally
						{
							this._notificationIdToDependenciesHash.Remove(notificationId);
							this._commandHashToNotificationId.Remove(dependencyList.CommandHash);
						}
					}
					Bid.NotificationsTrace("<sc.SqlDependencyPerAppDomainDispatcher.LookupDependencyEntriesWithRemove|DEP> Entries NOT found in hashtable.\n");
				}
				IL_73:
				result = dependencyList;
			}
			finally
			{
				Bid.ScopeLeave(ref intPtr);
			}
			return result;
		}

		// Token: 0x06001CE6 RID: 7398 RVA: 0x000CC2CC File Offset: 0x000CB6CC
		private void RemoveDependencyFromCommandToDependenciesHash(SqlDependency dependency)
		{
			IntPtr intPtr;
			Bid.NotificationsScopeEnter(out intPtr, "<sc.SqlDependencyPerAppDomainDispatcher.RemoveDependencyFromCommandToDependenciesHash|DEP> %d#, SqlDependency: %d#", this.ObjectID, dependency.ObjectID);
			try
			{
				lock (this)
				{
					List<string> list = new List<string>();
					List<string> list2 = new List<string>();
					foreach (KeyValuePair<string, SqlDependencyPerAppDomainDispatcher.DependencyList> keyValuePair in this._notificationIdToDependenciesHash)
					{
						SqlDependencyPerAppDomainDispatcher.DependencyList value = keyValuePair.Value;
						if (value.Remove(dependency))
						{
							Bid.NotificationsTrace("<sc.SqlDependencyPerAppDomainDispatcher.RemoveDependencyFromCommandToDependenciesHash|DEP> Removed SqlDependency: %d#, with ID: '%ls'.\n", dependency.ObjectID, dependency.Id);
							if (value.Count == 0)
							{
								list.Add(keyValuePair.Key);
								list2.Add(keyValuePair.Value.CommandHash);
							}
						}
					}
					for (int i = 0; i < list.Count; i++)
					{
						try
						{
						}
						finally
						{
							this._notificationIdToDependenciesHash.Remove(list[i]);
							this._commandHashToNotificationId.Remove(list2[i]);
						}
					}
				}
			}
			finally
			{
				Bid.ScopeLeave(ref intPtr);
			}
		}

		// Token: 0x06001CE7 RID: 7399 RVA: 0x000CC444 File Offset: 0x000CB844
		internal void StartTimer(SqlDependency dep)
		{
			IntPtr intPtr;
			Bid.NotificationsScopeEnter(out intPtr, "<sc.SqlDependencyPerAppDomainDispatcher.StartTimer|DEP> %d#, SqlDependency: %d#", this.ObjectID, dep.ObjectID);
			try
			{
				lock (this)
				{
					if (!this._SqlDependencyTimeOutTimerStarted)
					{
						Bid.NotificationsTrace("<sc.SqlDependencyPerAppDomainDispatcher.StartTimer|DEP> Timer not yet started, starting.\n");
						this._timeoutTimer.Change(15000, 15000);
						this._nextTimeout = dep.ExpirationTime;
						this._SqlDependencyTimeOutTimerStarted = true;
					}
					else if (this._nextTimeout > dep.ExpirationTime)
					{
						Bid.NotificationsTrace("<sc.SqlDependencyPerAppDomainDispatcher.StartTimer|DEP> Timer already started, resetting time.\n");
						this._nextTimeout = dep.ExpirationTime;
					}
				}
			}
			finally
			{
				Bid.ScopeLeave(ref intPtr);
			}
		}

		// Token: 0x06001CE8 RID: 7400 RVA: 0x000CC528 File Offset: 0x000CB928
		private static void TimeoutTimerCallback(object state)
		{
			IntPtr intPtr;
			Bid.NotificationsScopeEnter(out intPtr, "<sc.SqlDependencyPerAppDomainDispatcher.TimeoutTimerCallback|DEP> AppDomainKey: '%ls'", SqlDependency.AppDomainKey);
			try
			{
				SqlDependencyPerAppDomainDispatcher singletonInstance = SqlDependencyPerAppDomainDispatcher.SingletonInstance;
				SqlDependency[] array;
				lock (singletonInstance)
				{
					if (SqlDependencyPerAppDomainDispatcher.SingletonInstance._dependencyIdToDependencyHash.Count == 0)
					{
						Bid.NotificationsTrace("<sc.SqlDependencyPerAppDomainDispatcher.TimeoutTimerCallback|DEP> No dependencies, exiting.\n");
						return;
					}
					if (SqlDependencyPerAppDomainDispatcher.SingletonInstance._nextTimeout > DateTime.UtcNow)
					{
						Bid.NotificationsTrace("<sc.SqlDependencyPerAppDomainDispatcher.TimeoutTimerCallback|DEP> No timeouts expired, exiting.\n");
						return;
					}
					array = new SqlDependency[SqlDependencyPerAppDomainDispatcher.SingletonInstance._dependencyIdToDependencyHash.Count];
					SqlDependencyPerAppDomainDispatcher.SingletonInstance._dependencyIdToDependencyHash.Values.CopyTo(array, 0);
				}
				DateTime utcNow = DateTime.UtcNow;
				DateTime dateTime = DateTime.MaxValue;
				int i = 0;
				while (i < array.Length)
				{
					if (array[i].ExpirationTime <= utcNow)
					{
						try
						{
							array[i].Invalidate(SqlNotificationType.Change, SqlNotificationInfo.Error, SqlNotificationSource.Timeout);
							goto IL_104;
						}
						catch (Exception e)
						{
							if (!ADP.IsCatchableExceptionType(e))
							{
								throw;
							}
							ADP.TraceExceptionWithoutRethrow(e);
							goto IL_104;
						}
						goto IL_E7;
					}
					goto IL_E7;
					IL_104:
					i++;
					continue;
					IL_E7:
					if (array[i].ExpirationTime < dateTime)
					{
						dateTime = array[i].ExpirationTime;
					}
					array[i] = null;
					goto IL_104;
				}
				SqlDependencyPerAppDomainDispatcher singletonInstance2 = SqlDependencyPerAppDomainDispatcher.SingletonInstance;
				lock (singletonInstance2)
				{
					for (int j = 0; j < array.Length; j++)
					{
						if (array[j] != null)
						{
							SqlDependencyPerAppDomainDispatcher.SingletonInstance._dependencyIdToDependencyHash.Remove(array[j].Id);
						}
					}
					if (dateTime < SqlDependencyPerAppDomainDispatcher.SingletonInstance._nextTimeout)
					{
						SqlDependencyPerAppDomainDispatcher.SingletonInstance._nextTimeout = dateTime;
					}
				}
			}
			finally
			{
				Bid.ScopeLeave(ref intPtr);
			}
		}

		// Token: 0x0400105F RID: 4191
		internal static readonly SqlDependencyPerAppDomainDispatcher SingletonInstance = new SqlDependencyPerAppDomainDispatcher();

		// Token: 0x04001060 RID: 4192
		private Dictionary<string, SqlDependency> _dependencyIdToDependencyHash;

		// Token: 0x04001061 RID: 4193
		private Dictionary<string, SqlDependencyPerAppDomainDispatcher.DependencyList> _notificationIdToDependenciesHash;

		// Token: 0x04001062 RID: 4194
		private Dictionary<string, string> _commandHashToNotificationId;

		// Token: 0x04001063 RID: 4195
		private bool _SqlDependencyTimeOutTimerStarted;

		// Token: 0x04001064 RID: 4196
		private DateTime _nextTimeout;

		// Token: 0x04001065 RID: 4197
		private Timer _timeoutTimer;

		// Token: 0x04001066 RID: 4198
		private readonly int _objectID = Interlocked.Increment(ref SqlDependencyPerAppDomainDispatcher._objectTypeCount);

		// Token: 0x04001067 RID: 4199
		private static int _objectTypeCount;

		// Token: 0x020003C3 RID: 963
		private sealed class DependencyList : List<SqlDependency>
		{
			// Token: 0x06003522 RID: 13602 RVA: 0x00143F34 File Offset: 0x00143334
			internal DependencyList(string commandHash)
			{
				this.CommandHash = commandHash;
			}

			// Token: 0x040020CF RID: 8399
			public readonly string CommandHash;
		}
	}
}
