using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;

namespace System.Data.ProviderBase
{
	// Token: 0x020002C1 RID: 705
	internal sealed class DbConnectionPool
	{
		// Token: 0x06002AB3 RID: 10931 RVA: 0x00118800 File Offset: 0x00117C00
		internal DbConnectionPool(DbConnectionFactory connectionFactory, DbConnectionPoolGroup connectionPoolGroup, DbConnectionPoolIdentity identity, DbConnectionPoolProviderInfo connectionPoolProviderInfo)
		{
			if (identity != null && identity.IsRestricted)
			{
				throw ADP.InternalError(ADP.InternalErrorCode.AttemptingToPoolOnRestrictedToken);
			}
			this._state = DbConnectionPool.State.Initializing;
			Random random = DbConnectionPool._random;
			lock (random)
			{
				this._cleanupWait = DbConnectionPool._random.Next(12, 24) * 10 * 1000;
			}
			this._connectionFactory = connectionFactory;
			this._connectionPoolGroup = connectionPoolGroup;
			this._connectionPoolGroupOptions = connectionPoolGroup.PoolGroupOptions;
			this._connectionPoolProviderInfo = connectionPoolProviderInfo;
			this._identity = identity;
			this._waitHandles = new DbConnectionPool.PoolWaitHandles();
			this._errorWait = 5000;
			this._errorTimer = null;
			this._objectList = new List<DbConnectionInternal>(this.MaxPoolSize);
			this._pooledDbAuthenticationContexts = new ConcurrentDictionary<DbConnectionPoolAuthenticationContextKey, DbConnectionPoolAuthenticationContext>(4 * Environment.ProcessorCount, 2);
			if (ADP.IsPlatformNT5)
			{
				this._transactedConnectionPool = new DbConnectionPool.TransactedConnectionPool(this);
			}
			this._poolCreateRequest = new WaitCallback(this.PoolCreateRequest);
			this._state = DbConnectionPool.State.Running;
			Bid.PoolerTrace("<prov.DbConnectionPool.DbConnectionPool|RES|CPOOL> %d#, Constructed.\n", this.ObjectID);
		}

		// Token: 0x170006F5 RID: 1781
		// (get) Token: 0x06002AB4 RID: 10932 RVA: 0x00118958 File Offset: 0x00117D58
		private int CreationTimeout
		{
			get
			{
				return this.PoolGroupOptions.CreationTimeout;
			}
		}

		// Token: 0x170006F6 RID: 1782
		// (get) Token: 0x06002AB5 RID: 10933 RVA: 0x00118970 File Offset: 0x00117D70
		internal int Count
		{
			get
			{
				return this._totalObjects;
			}
		}

		// Token: 0x170006F7 RID: 1783
		// (get) Token: 0x06002AB6 RID: 10934 RVA: 0x00118984 File Offset: 0x00117D84
		internal DbConnectionFactory ConnectionFactory
		{
			get
			{
				return this._connectionFactory;
			}
		}

		// Token: 0x170006F8 RID: 1784
		// (get) Token: 0x06002AB7 RID: 10935 RVA: 0x00118998 File Offset: 0x00117D98
		internal bool ErrorOccurred
		{
			get
			{
				return this._errorOccurred;
			}
		}

		// Token: 0x170006F9 RID: 1785
		// (get) Token: 0x06002AB8 RID: 10936 RVA: 0x001189B0 File Offset: 0x00117DB0
		private bool HasTransactionAffinity
		{
			get
			{
				return this.PoolGroupOptions.HasTransactionAffinity;
			}
		}

		// Token: 0x170006FA RID: 1786
		// (get) Token: 0x06002AB9 RID: 10937 RVA: 0x001189C8 File Offset: 0x00117DC8
		internal TimeSpan LoadBalanceTimeout
		{
			get
			{
				return this.PoolGroupOptions.LoadBalanceTimeout;
			}
		}

		// Token: 0x170006FB RID: 1787
		// (get) Token: 0x06002ABA RID: 10938 RVA: 0x001189E0 File Offset: 0x00117DE0
		private bool NeedToReplenish
		{
			get
			{
				if (DbConnectionPool.State.Running != this._state)
				{
					return false;
				}
				int count = this.Count;
				if (count >= this.MaxPoolSize)
				{
					return false;
				}
				if (count < this.MinPoolSize)
				{
					return true;
				}
				int num = this._stackNew.Count + this._stackOld.Count;
				int waitCount = this._waitCount;
				return num < waitCount || (num == waitCount && count > 1);
			}
		}

		// Token: 0x170006FC RID: 1788
		// (get) Token: 0x06002ABB RID: 10939 RVA: 0x00118A48 File Offset: 0x00117E48
		internal DbConnectionPoolIdentity Identity
		{
			get
			{
				return this._identity;
			}
		}

		// Token: 0x170006FD RID: 1789
		// (get) Token: 0x06002ABC RID: 10940 RVA: 0x00118A5C File Offset: 0x00117E5C
		internal bool IsRunning
		{
			get
			{
				return DbConnectionPool.State.Running == this._state;
			}
		}

		// Token: 0x170006FE RID: 1790
		// (get) Token: 0x06002ABD RID: 10941 RVA: 0x00118A74 File Offset: 0x00117E74
		private int MaxPoolSize
		{
			get
			{
				return this.PoolGroupOptions.MaxPoolSize;
			}
		}

		// Token: 0x170006FF RID: 1791
		// (get) Token: 0x06002ABE RID: 10942 RVA: 0x00118A8C File Offset: 0x00117E8C
		private int MinPoolSize
		{
			get
			{
				return this.PoolGroupOptions.MinPoolSize;
			}
		}

		// Token: 0x17000700 RID: 1792
		// (get) Token: 0x06002ABF RID: 10943 RVA: 0x00118AA4 File Offset: 0x00117EA4
		internal int ObjectID
		{
			get
			{
				return this._objectID;
			}
		}

		// Token: 0x17000701 RID: 1793
		// (get) Token: 0x06002AC0 RID: 10944 RVA: 0x00118AB8 File Offset: 0x00117EB8
		internal DbConnectionPoolCounters PerformanceCounters
		{
			get
			{
				return this._connectionFactory.PerformanceCounters;
			}
		}

		// Token: 0x17000702 RID: 1794
		// (get) Token: 0x06002AC1 RID: 10945 RVA: 0x00118AD0 File Offset: 0x00117ED0
		internal DbConnectionPoolGroup PoolGroup
		{
			get
			{
				return this._connectionPoolGroup;
			}
		}

		// Token: 0x17000703 RID: 1795
		// (get) Token: 0x06002AC2 RID: 10946 RVA: 0x00118AE4 File Offset: 0x00117EE4
		internal DbConnectionPoolGroupOptions PoolGroupOptions
		{
			get
			{
				return this._connectionPoolGroupOptions;
			}
		}

		// Token: 0x17000704 RID: 1796
		// (get) Token: 0x06002AC3 RID: 10947 RVA: 0x00118AF8 File Offset: 0x00117EF8
		internal DbConnectionPoolProviderInfo ProviderInfo
		{
			get
			{
				return this._connectionPoolProviderInfo;
			}
		}

		// Token: 0x17000705 RID: 1797
		// (get) Token: 0x06002AC4 RID: 10948 RVA: 0x00118B0C File Offset: 0x00117F0C
		internal ConcurrentDictionary<DbConnectionPoolAuthenticationContextKey, DbConnectionPoolAuthenticationContext> AuthenticationContexts
		{
			get
			{
				return this._pooledDbAuthenticationContexts;
			}
		}

		// Token: 0x17000706 RID: 1798
		// (get) Token: 0x06002AC5 RID: 10949 RVA: 0x00118B20 File Offset: 0x00117F20
		internal bool UseLoadBalancing
		{
			get
			{
				return this.PoolGroupOptions.UseLoadBalancing;
			}
		}

		// Token: 0x17000707 RID: 1799
		// (get) Token: 0x06002AC6 RID: 10950 RVA: 0x00118B38 File Offset: 0x00117F38
		private bool UsingIntegrateSecurity
		{
			get
			{
				return this._identity != null && DbConnectionPoolIdentity.NoIdentity != this._identity;
			}
		}

		// Token: 0x06002AC7 RID: 10951 RVA: 0x00118B60 File Offset: 0x00117F60
		private void CleanupCallback(object state)
		{
			Bid.PoolerTrace("<prov.DbConnectionPool.CleanupCallback|RES|INFO|CPOOL> %d#\n", this.ObjectID);
			while (this.Count > this.MinPoolSize && this._waitHandles.PoolSemaphore.WaitOne(0, false))
			{
				DbConnectionInternal dbConnectionInternal;
				if (!this._stackOld.TryPop(out dbConnectionInternal))
				{
					this._waitHandles.PoolSemaphore.Release(1);
					break;
				}
				this.PerformanceCounters.NumberOfFreeConnections.Decrement();
				bool flag = true;
				DbConnectionInternal obj = dbConnectionInternal;
				lock (obj)
				{
					if (dbConnectionInternal.IsTransactionRoot)
					{
						flag = false;
					}
				}
				if (flag)
				{
					this.DestroyObject(dbConnectionInternal);
				}
				else
				{
					dbConnectionInternal.SetInStasis();
				}
			}
			if (this._waitHandles.PoolSemaphore.WaitOne(0, false))
			{
				DbConnectionInternal dbConnectionInternal2;
				while (this._stackNew.TryPop(out dbConnectionInternal2))
				{
					Bid.PoolerTrace("<prov.DbConnectionPool.CleanupCallback|RES|INFO|CPOOL> %d#, ChangeStacks=%d#\n", this.ObjectID, dbConnectionInternal2.ObjectID);
					this._stackOld.Push(dbConnectionInternal2);
				}
				this._waitHandles.PoolSemaphore.Release(1);
			}
			this.QueuePoolCreateRequest();
		}

		// Token: 0x06002AC8 RID: 10952 RVA: 0x00118C94 File Offset: 0x00118094
		internal void Clear()
		{
			Bid.PoolerTrace("<prov.DbConnectionPool.Clear|RES|CPOOL> %d#, Clearing.\n", this.ObjectID);
			List<DbConnectionInternal> objectList = this._objectList;
			DbConnectionInternal dbConnectionInternal;
			lock (objectList)
			{
				int count = this._objectList.Count;
				for (int i = 0; i < count; i++)
				{
					dbConnectionInternal = this._objectList[i];
					if (dbConnectionInternal != null)
					{
						dbConnectionInternal.DoNotPoolThisConnection();
					}
				}
				goto IL_74;
			}
			IL_5D:
			this.PerformanceCounters.NumberOfFreeConnections.Decrement();
			this.DestroyObject(dbConnectionInternal);
			IL_74:
			if (!this._stackNew.TryPop(out dbConnectionInternal))
			{
				while (this._stackOld.TryPop(out dbConnectionInternal))
				{
					this.PerformanceCounters.NumberOfFreeConnections.Decrement();
					this.DestroyObject(dbConnectionInternal);
				}
				this.ReclaimEmancipatedObjects();
				Bid.PoolerTrace("<prov.DbConnectionPool.Clear|RES|CPOOL> %d#, Cleared.\n", this.ObjectID);
				return;
			}
			goto IL_5D;
		}

		// Token: 0x06002AC9 RID: 10953 RVA: 0x00118D80 File Offset: 0x00118180
		private Timer CreateCleanupTimer()
		{
			return new Timer(new TimerCallback(this.CleanupCallback), null, this._cleanupWait, this._cleanupWait);
		}

		// Token: 0x06002ACA RID: 10954 RVA: 0x00118DAC File Offset: 0x001181AC
		private bool IsBlockingPeriodEnabled()
		{
			SqlConnectionString sqlConnectionString = this._connectionPoolGroup.ConnectionOptions as SqlConnectionString;
			if (sqlConnectionString == null)
			{
				return true;
			}
			switch (sqlConnectionString.PoolBlockingPeriod)
			{
			case PoolBlockingPeriod.Auto:
				return !ADP.IsAzureSqlServerEndpoint(sqlConnectionString.DataSource);
			case PoolBlockingPeriod.AlwaysBlock:
				return true;
			case PoolBlockingPeriod.NeverBlock:
				return false;
			default:
				return true;
			}
		}

		// Token: 0x06002ACB RID: 10955 RVA: 0x00118E00 File Offset: 0x00118200
		private DbConnectionInternal CreateObject(DbConnection owningObject, DbConnectionOptions userOptions, DbConnectionInternal oldConnection)
		{
			DbConnectionInternal dbConnectionInternal = null;
			try
			{
				dbConnectionInternal = this._connectionFactory.CreatePooledConnection(this, owningObject, this._connectionPoolGroup.ConnectionOptions, this._connectionPoolGroup.PoolKey, userOptions);
				if (dbConnectionInternal == null)
				{
					throw ADP.InternalError(ADP.InternalErrorCode.CreateObjectReturnedNull);
				}
				if (!dbConnectionInternal.CanBePooled)
				{
					throw ADP.InternalError(ADP.InternalErrorCode.NewObjectCannotBePooled);
				}
				dbConnectionInternal.PrePush(null);
				List<DbConnectionInternal> objectList = this._objectList;
				lock (objectList)
				{
					if (oldConnection != null && oldConnection.Pool == this)
					{
						this._objectList.Remove(oldConnection);
					}
					this._objectList.Add(dbConnectionInternal);
					this._totalObjects = this._objectList.Count;
					this.PerformanceCounters.NumberOfPooledConnections.Increment();
				}
				if (oldConnection != null)
				{
					DbConnectionPool pool = oldConnection.Pool;
					if (pool != null && pool != this)
					{
						List<DbConnectionInternal> objectList2 = pool._objectList;
						lock (objectList2)
						{
							pool._objectList.Remove(oldConnection);
							pool._totalObjects = pool._objectList.Count;
						}
					}
				}
				Bid.PoolerTrace("<prov.DbConnectionPool.CreateObject|RES|CPOOL> %d#, Connection %d#, Added to pool.\n", this.ObjectID, dbConnectionInternal.ObjectID);
				this._errorWait = 5000;
			}
			catch (Exception ex)
			{
				if (!ADP.IsCatchableExceptionType(ex))
				{
					throw;
				}
				ADP.TraceExceptionForCapture(ex);
				if (!this.IsBlockingPeriodEnabled())
				{
					throw;
				}
				if (!LocalAppContextSwitches.DisablePooledConnectionResetOnTransientError && dbConnectionInternal != null && dbConnectionInternal.IsConnectionAlive(false))
				{
					dbConnectionInternal.Dispose();
				}
				dbConnectionInternal = null;
				this._resError = ex;
				Timer timer = new Timer(new TimerCallback(this.ErrorCallback), null, -1, -1);
				RuntimeHelpers.PrepareConstrainedRegions();
				try
				{
				}
				finally
				{
					this._waitHandles.ErrorEvent.Set();
					this._errorOccurred = true;
					this._errorTimer = timer;
					bool flag3 = timer.Change(this._errorWait, this._errorWait);
				}
				if (30000 < this._errorWait)
				{
					this._errorWait = 60000;
				}
				else
				{
					this._errorWait *= 2;
				}
				throw;
			}
			return dbConnectionInternal;
		}

		// Token: 0x06002ACC RID: 10956 RVA: 0x00119054 File Offset: 0x00118454
		private void DeactivateObject(DbConnectionInternal obj)
		{
			Bid.PoolerTrace("<prov.DbConnectionPool.DeactivateObject|RES|CPOOL> %d#, Connection %d#, Deactivating.\n", this.ObjectID, obj.ObjectID);
			obj.DeactivateConnection();
			bool flag = false;
			bool flag2 = false;
			if (obj.IsConnectionDoomed)
			{
				flag2 = true;
			}
			else
			{
				lock (obj)
				{
					if (this._state == DbConnectionPool.State.ShuttingDown)
					{
						if (obj.IsTransactionRoot)
						{
							obj.SetInStasis();
						}
						else
						{
							flag2 = true;
						}
					}
					else if (obj.IsNonPoolableTransactionRoot)
					{
						obj.SetInStasis();
					}
					else if (obj.CanBePooled)
					{
						Transaction enlistedTransaction = obj.EnlistedTransaction;
						if (null != enlistedTransaction)
						{
							this._transactedConnectionPool.PutTransactedObject(enlistedTransaction, obj);
						}
						else
						{
							flag = true;
						}
					}
					else if (obj.IsTransactionRoot && !obj.IsConnectionDoomed)
					{
						obj.SetInStasis();
					}
					else
					{
						flag2 = true;
					}
				}
			}
			if (flag)
			{
				this.PutNewObject(obj);
				return;
			}
			if (flag2)
			{
				this.DestroyObject(obj);
				this.QueuePoolCreateRequest();
			}
		}

		// Token: 0x06002ACD RID: 10957 RVA: 0x00119160 File Offset: 0x00118560
		internal void DestroyObject(DbConnectionInternal obj)
		{
			if (obj.IsTxRootWaitingForTxEnd)
			{
				Bid.PoolerTrace("<prov.DbConnectionPool.DestroyObject|RES|CPOOL> %d#, Connection %d#, Has Delegated Transaction, waiting to Dispose.\n", this.ObjectID, obj.ObjectID);
				return;
			}
			Bid.PoolerTrace("<prov.DbConnectionPool.DestroyObject|RES|CPOOL> %d#, Connection %d#, Removing from pool.\n", this.ObjectID, obj.ObjectID);
			bool flag = false;
			List<DbConnectionInternal> objectList = this._objectList;
			lock (objectList)
			{
				flag = this._objectList.Remove(obj);
				this._totalObjects = this._objectList.Count;
			}
			if (flag)
			{
				Bid.PoolerTrace("<prov.DbConnectionPool.DestroyObject|RES|CPOOL> %d#, Connection %d#, Removed from pool.\n", this.ObjectID, obj.ObjectID);
				this.PerformanceCounters.NumberOfPooledConnections.Decrement();
			}
			obj.Dispose();
			Bid.PoolerTrace("<prov.DbConnectionPool.DestroyObject|RES|CPOOL> %d#, Connection %d#, Disposed.\n", this.ObjectID, obj.ObjectID);
			this.PerformanceCounters.HardDisconnectsPerSecond.Increment();
		}

		// Token: 0x06002ACE RID: 10958 RVA: 0x00119250 File Offset: 0x00118650
		private void ErrorCallback(object state)
		{
			Bid.PoolerTrace("<prov.DbConnectionPool.ErrorCallback|RES|CPOOL> %d#, Resetting Error handling.\n", this.ObjectID);
			this._errorOccurred = false;
			this._waitHandles.ErrorEvent.Reset();
			Timer errorTimer = this._errorTimer;
			this._errorTimer = null;
			if (errorTimer != null)
			{
				errorTimer.Dispose();
			}
		}

		// Token: 0x06002ACF RID: 10959 RVA: 0x001192A0 File Offset: 0x001186A0
		private Exception TryCloneCachedException()
		{
			if (this._resError == null)
			{
				return null;
			}
			if (this._resError.GetType() == typeof(SqlException))
			{
				return ((SqlException)this._resError).InternalClone();
			}
			return this._resError;
		}

		// Token: 0x06002AD0 RID: 10960 RVA: 0x001192EC File Offset: 0x001186EC
		private void WaitForPendingOpen()
		{
			DbConnectionPool.PendingGetConnection pendingGetConnection;
			do
			{
				bool flag = false;
				RuntimeHelpers.PrepareConstrainedRegions();
				try
				{
					RuntimeHelpers.PrepareConstrainedRegions();
					try
					{
					}
					finally
					{
						flag = (Interlocked.CompareExchange(ref this._pendingOpensWaiting, 1, 0) == 0);
					}
					if (!flag)
					{
						break;
					}
					while (this._pendingOpens.TryDequeue(out pendingGetConnection))
					{
						if (!pendingGetConnection.Completion.Task.IsCompleted)
						{
							uint waitForMultipleObjectsTimeout;
							if (pendingGetConnection.DueTime == -1L)
							{
								waitForMultipleObjectsTimeout = uint.MaxValue;
							}
							else
							{
								waitForMultipleObjectsTimeout = (uint)Math.Max(ADP.TimerRemainingMilliseconds(pendingGetConnection.DueTime), 0L);
							}
							DbConnectionInternal dbConnectionInternal = null;
							bool flag2 = false;
							Exception ex = null;
							RuntimeHelpers.PrepareConstrainedRegions();
							try
							{
								bool allowCreate = true;
								bool onlyOneCheckConnection = false;
								ADP.SetCurrentTransaction(pendingGetConnection.Completion.Task.AsyncState as Transaction);
								flag2 = !this.TryGetConnection(pendingGetConnection.Owner, waitForMultipleObjectsTimeout, allowCreate, onlyOneCheckConnection, pendingGetConnection.UserOptions, out dbConnectionInternal);
							}
							catch (OutOfMemoryException)
							{
								if (dbConnectionInternal != null)
								{
									dbConnectionInternal.DoomThisConnection();
								}
								throw;
							}
							catch (StackOverflowException)
							{
								if (dbConnectionInternal != null)
								{
									dbConnectionInternal.DoomThisConnection();
								}
								throw;
							}
							catch (ThreadAbortException)
							{
								if (dbConnectionInternal != null)
								{
									dbConnectionInternal.DoomThisConnection();
								}
								throw;
							}
							catch (Exception ex2)
							{
								ex = ex2;
							}
							if (ex != null)
							{
								pendingGetConnection.Completion.TrySetException(ex);
							}
							else if (flag2)
							{
								pendingGetConnection.Completion.TrySetException(ADP.ExceptionWithStackTrace(ADP.PooledOpenTimeout()));
							}
							else if (!pendingGetConnection.Completion.TrySetResult(dbConnectionInternal))
							{
								this.PutObject(dbConnectionInternal, pendingGetConnection.Owner);
							}
						}
					}
				}
				finally
				{
					if (flag)
					{
						Interlocked.Exchange(ref this._pendingOpensWaiting, 0);
					}
				}
			}
			while (this._pendingOpens.TryPeek(out pendingGetConnection));
		}

		// Token: 0x06002AD1 RID: 10961 RVA: 0x001194F0 File Offset: 0x001188F0
		internal bool TryGetConnection(DbConnection owningObject, TaskCompletionSource<DbConnectionInternal> retry, DbConnectionOptions userOptions, out DbConnectionInternal connection)
		{
			uint num = 0U;
			bool allowCreate = false;
			if (retry == null)
			{
				num = (uint)this.CreationTimeout;
				if (num == 0U)
				{
					num = uint.MaxValue;
				}
				allowCreate = true;
			}
			if (this._state != DbConnectionPool.State.Running)
			{
				Bid.PoolerTrace("<prov.DbConnectionPool.GetConnection|RES|CPOOL> %d#, DbConnectionInternal State != Running.\n", this.ObjectID);
				connection = null;
				return true;
			}
			bool onlyOneCheckConnection = true;
			if (this.TryGetConnection(owningObject, num, allowCreate, onlyOneCheckConnection, userOptions, out connection))
			{
				return true;
			}
			if (retry == null)
			{
				return true;
			}
			DbConnectionPool.PendingGetConnection item = new DbConnectionPool.PendingGetConnection((this.CreationTimeout == 0) ? -1L : (ADP.TimerCurrent() + ADP.TimerFromSeconds(this.CreationTimeout / 1000)), owningObject, retry, userOptions);
			this._pendingOpens.Enqueue(item);
			if (this._pendingOpensWaiting == 0)
			{
				new Thread(new ThreadStart(this.WaitForPendingOpen))
				{
					IsBackground = true
				}.Start();
			}
			connection = null;
			return false;
		}

		// Token: 0x06002AD2 RID: 10962 RVA: 0x001195B0 File Offset: 0x001189B0
		private bool TryGetConnection(DbConnection owningObject, uint waitForMultipleObjectsTimeout, bool allowCreate, bool onlyOneCheckConnection, DbConnectionOptions userOptions, out DbConnectionInternal connection)
		{
			DbConnectionInternal dbConnectionInternal = null;
			Transaction transaction = null;
			this.PerformanceCounters.SoftConnectsPerSecond.Increment();
			Bid.PoolerTrace("<prov.DbConnectionPool.GetConnection|RES|CPOOL> %d#, Getting connection.\n", this.ObjectID);
			if (this.HasTransactionAffinity)
			{
				dbConnectionInternal = this.GetFromTransactedPool(out transaction);
			}
			if (dbConnectionInternal == null)
			{
				Interlocked.Increment(ref this._waitCount);
				uint nCount = allowCreate ? 3U : 2U;
				for (;;)
				{
					int num = 3;
					int num2 = 0;
					bool flag = false;
					int errorCode = 0;
					RuntimeHelpers.PrepareConstrainedRegions();
					try
					{
						this._waitHandles.DangerousAddRef(ref flag);
						RuntimeHelpers.PrepareConstrainedRegions();
						try
						{
						}
						finally
						{
							num = SafeNativeMethods.WaitForMultipleObjectsEx(nCount, this._waitHandles.DangerousGetHandle(), false, waitForMultipleObjectsTimeout, false);
							if (num == -1)
							{
								errorCode = Marshal.GetHRForLastWin32Error();
							}
						}
						switch (num)
						{
						case -1:
							Bid.PoolerTrace("<prov.DbConnectionPool.GetConnection|RES|CPOOL> %d#, Wait failed.\n", this.ObjectID);
							Interlocked.Decrement(ref this._waitCount);
							Marshal.ThrowExceptionForHR(errorCode);
							break;
						case 0:
							Interlocked.Decrement(ref this._waitCount);
							dbConnectionInternal = this.GetFromGeneralPool();
							if (dbConnectionInternal == null || dbConnectionInternal.IsConnectionAlive(false))
							{
								goto IL_31B;
							}
							Bid.PoolerTrace("<prov.DbConnectionPool.GetConnection|RES|CPOOL> %d#, Connection %d#, found dead and removed.\n", this.ObjectID, dbConnectionInternal.ObjectID);
							this.DestroyObject(dbConnectionInternal);
							dbConnectionInternal = null;
							if (onlyOneCheckConnection)
							{
								if (this._waitHandles.CreationSemaphore.WaitOne((int)waitForMultipleObjectsTimeout))
								{
									RuntimeHelpers.PrepareConstrainedRegions();
									try
									{
										Bid.PoolerTrace("<prov.DbConnectionPool.GetConnection|RES|CPOOL> %d#, Creating new connection.\n", this.ObjectID);
										dbConnectionInternal = this.UserCreateRequest(owningObject, userOptions, null);
										goto IL_358;
									}
									finally
									{
										this._waitHandles.CreationSemaphore.Release(1);
									}
								}
								Bid.PoolerTrace("<prov.DbConnectionPool.GetConnection|RES|CPOOL> %d#, Wait timed out.\n", this.ObjectID);
								connection = null;
								return false;
							}
							goto IL_31B;
						case 1:
							Bid.PoolerTrace("<prov.DbConnectionPool.GetConnection|RES|CPOOL> %d#, Errors are set.\n", this.ObjectID);
							Interlocked.Decrement(ref this._waitCount);
							throw this.TryCloneCachedException();
						case 2:
							Bid.PoolerTrace("<prov.DbConnectionPool.GetConnection|RES|CPOOL> %d#, Creating new connection.\n", this.ObjectID);
							try
							{
								dbConnectionInternal = this.UserCreateRequest(owningObject, userOptions, null);
							}
							catch
							{
								if (dbConnectionInternal == null)
								{
									Interlocked.Decrement(ref this._waitCount);
								}
								throw;
							}
							finally
							{
								if (dbConnectionInternal != null)
								{
									Interlocked.Decrement(ref this._waitCount);
								}
							}
							if (dbConnectionInternal == null && this.Count >= this.MaxPoolSize && this.MaxPoolSize != 0 && !this.ReclaimEmancipatedObjects())
							{
								nCount = 2U;
								goto IL_358;
							}
							goto IL_31B;
						default:
							switch (num)
							{
							case 128:
								Bid.PoolerTrace("<prov.DbConnectionPool.GetConnection|RES|CPOOL> %d#, Semaphore handle abandonded.\n", this.ObjectID);
								Interlocked.Decrement(ref this._waitCount);
								throw new AbandonedMutexException(0, this._waitHandles.PoolSemaphore);
							case 129:
								Bid.PoolerTrace("<prov.DbConnectionPool.GetConnection|RES|CPOOL> %d#, Error handle abandonded.\n", this.ObjectID);
								Interlocked.Decrement(ref this._waitCount);
								throw new AbandonedMutexException(1, this._waitHandles.ErrorEvent);
							case 130:
								Bid.PoolerTrace("<prov.DbConnectionPool.GetConnection|RES|CPOOL> %d#, Creation handle abandoned.\n", this.ObjectID);
								Interlocked.Decrement(ref this._waitCount);
								throw new AbandonedMutexException(2, this._waitHandles.CreationSemaphore);
							default:
								if (num == 258)
								{
									Bid.PoolerTrace("<prov.DbConnectionPool.GetConnection|RES|CPOOL> %d#, Wait timed out.\n", this.ObjectID);
									Interlocked.Decrement(ref this._waitCount);
									connection = null;
									return false;
								}
								break;
							}
							break;
						}
						Bid.PoolerTrace("<prov.DbConnectionPool.GetConnection|RES|CPOOL> %d#, WaitForMultipleObjects=%d\n", this.ObjectID, num);
						Interlocked.Decrement(ref this._waitCount);
						throw ADP.InternalError(ADP.InternalErrorCode.UnexpectedWaitAnyResult);
						IL_31B:;
					}
					finally
					{
						if (2 == num && SafeNativeMethods.ReleaseSemaphore(this._waitHandles.CreationHandle.DangerousGetHandle(), 1, IntPtr.Zero) == 0)
						{
							num2 = Marshal.GetHRForLastWin32Error();
						}
						if (flag)
						{
							this._waitHandles.DangerousRelease();
						}
					}
					IL_358:
					if (num2 != 0)
					{
						Marshal.ThrowExceptionForHR(num2);
					}
					if (dbConnectionInternal != null)
					{
						goto IL_367;
					}
				}
				bool result;
				return result;
			}
			IL_367:
			if (dbConnectionInternal != null)
			{
				this.PrepareConnection(owningObject, dbConnectionInternal, transaction);
			}
			connection = dbConnectionInternal;
			return true;
		}

		// Token: 0x06002AD3 RID: 10963 RVA: 0x001199B4 File Offset: 0x00118DB4
		private void PrepareConnection(DbConnection owningObject, DbConnectionInternal obj, Transaction transaction)
		{
			lock (obj)
			{
				obj.PostPop(owningObject);
			}
			try
			{
				obj.ActivateConnection(transaction);
			}
			catch
			{
				this.PutObject(obj, owningObject);
				throw;
			}
		}

		// Token: 0x06002AD4 RID: 10964 RVA: 0x00119A2C File Offset: 0x00118E2C
		internal DbConnectionInternal ReplaceConnection(DbConnection owningObject, DbConnectionOptions userOptions, DbConnectionInternal oldConnection)
		{
			this.PerformanceCounters.SoftConnectsPerSecond.Increment();
			Bid.PoolerTrace("<prov.DbConnectionPool.ReplaceConnection|RES|CPOOL> %d#, replacing connection.\n", this.ObjectID);
			DbConnectionInternal dbConnectionInternal = this.UserCreateRequest(owningObject, userOptions, oldConnection);
			if (dbConnectionInternal != null)
			{
				this.PrepareConnection(owningObject, dbConnectionInternal, oldConnection.EnlistedTransaction);
				oldConnection.PrepareForReplaceConnection();
				oldConnection.DeactivateConnection();
				oldConnection.Dispose();
			}
			return dbConnectionInternal;
		}

		// Token: 0x06002AD5 RID: 10965 RVA: 0x00119A88 File Offset: 0x00118E88
		private DbConnectionInternal GetFromGeneralPool()
		{
			DbConnectionInternal dbConnectionInternal = null;
			if (!this._stackNew.TryPop(out dbConnectionInternal) && !this._stackOld.TryPop(out dbConnectionInternal))
			{
				dbConnectionInternal = null;
			}
			if (dbConnectionInternal != null)
			{
				Bid.PoolerTrace("<prov.DbConnectionPool.GetFromGeneralPool|RES|CPOOL> %d#, Connection %d#, Popped from general pool.\n", this.ObjectID, dbConnectionInternal.ObjectID);
				this.PerformanceCounters.NumberOfFreeConnections.Decrement();
			}
			return dbConnectionInternal;
		}

		// Token: 0x06002AD6 RID: 10966 RVA: 0x00119AE4 File Offset: 0x00118EE4
		private DbConnectionInternal GetFromTransactedPool(out Transaction transaction)
		{
			transaction = ADP.GetCurrentTransaction();
			DbConnectionInternal dbConnectionInternal = null;
			if (null != transaction && this._transactedConnectionPool != null)
			{
				dbConnectionInternal = this._transactedConnectionPool.GetTransactedObject(transaction);
				if (dbConnectionInternal != null)
				{
					Bid.PoolerTrace("<prov.DbConnectionPool.GetFromTransactedPool|RES|CPOOL> %d#, Connection %d#, Popped from transacted pool.\n", this.ObjectID, dbConnectionInternal.ObjectID);
					this.PerformanceCounters.NumberOfFreeConnections.Decrement();
					if (dbConnectionInternal.IsTransactionRoot)
					{
						try
						{
							dbConnectionInternal.IsConnectionAlive(true);
							return dbConnectionInternal;
						}
						catch
						{
							Bid.PoolerTrace("<prov.DbConnectionPool.GetFromTransactedPool|RES|CPOOL> %d#, Connection %d#, found dead and removed.\n", this.ObjectID, dbConnectionInternal.ObjectID);
							this.DestroyObject(dbConnectionInternal);
							throw;
						}
					}
					if (!dbConnectionInternal.IsConnectionAlive(false))
					{
						Bid.PoolerTrace("<prov.DbConnectionPool.GetFromTransactedPool|RES|CPOOL> %d#, Connection %d#, found dead and removed.\n", this.ObjectID, dbConnectionInternal.ObjectID);
						this.DestroyObject(dbConnectionInternal);
						dbConnectionInternal = null;
					}
				}
			}
			return dbConnectionInternal;
		}

		// Token: 0x06002AD7 RID: 10967 RVA: 0x00119BC8 File Offset: 0x00118FC8
		private void PoolCreateRequest(object state)
		{
			IntPtr intPtr;
			Bid.PoolerScopeEnter(out intPtr, "<prov.DbConnectionPool.PoolCreateRequest|RES|INFO|CPOOL> %d#\n", this.ObjectID);
			try
			{
				if (DbConnectionPool.State.Running == this._state)
				{
					if (!this._pendingOpens.IsEmpty && this._pendingOpensWaiting == 0)
					{
						new Thread(new ThreadStart(this.WaitForPendingOpen))
						{
							IsBackground = true
						}.Start();
					}
					this.ReclaimEmancipatedObjects();
					if (!this.ErrorOccurred && this.NeedToReplenish)
					{
						if (!this.UsingIntegrateSecurity || this._identity.Equals(DbConnectionPoolIdentity.GetCurrent()))
						{
							bool flag = false;
							int num = 3;
							uint creationTimeout = (uint)this.CreationTimeout;
							RuntimeHelpers.PrepareConstrainedRegions();
							try
							{
								this._waitHandles.DangerousAddRef(ref flag);
								RuntimeHelpers.PrepareConstrainedRegions();
								try
								{
								}
								finally
								{
									num = SafeNativeMethods.WaitForSingleObjectEx(this._waitHandles.CreationHandle.DangerousGetHandle(), creationTimeout, false);
								}
								if (num == 0)
								{
									if (!this.ErrorOccurred)
									{
										while (this.NeedToReplenish)
										{
											DbConnectionInternal dbConnectionInternal = this.CreateObject(null, null, null);
											if (dbConnectionInternal == null)
											{
												break;
											}
											this.PutNewObject(dbConnectionInternal);
										}
									}
								}
								else if (258 == num)
								{
									this.QueuePoolCreateRequest();
								}
								else
								{
									Bid.PoolerTrace("<prov.DbConnectionPool.PoolCreateRequest|RES|CPOOL> %d#, PoolCreateRequest called WaitForSingleObject failed %d", this.ObjectID, num);
								}
							}
							catch (Exception ex)
							{
								if (!ADP.IsCatchableExceptionType(ex))
								{
									throw;
								}
								Bid.PoolerTrace("<prov.DbConnectionPool.PoolCreateRequest|RES|CPOOL> %d#, PoolCreateRequest called CreateConnection which threw an exception: %ls", this.ObjectID, ex);
							}
							finally
							{
								if (num == 0)
								{
									num = SafeNativeMethods.ReleaseSemaphore(this._waitHandles.CreationHandle.DangerousGetHandle(), 1, IntPtr.Zero);
								}
								if (flag)
								{
									this._waitHandles.DangerousRelease();
								}
							}
						}
					}
				}
			}
			finally
			{
				Bid.ScopeLeave(ref intPtr);
			}
		}

		// Token: 0x06002AD8 RID: 10968 RVA: 0x00119DB0 File Offset: 0x001191B0
		internal void PutNewObject(DbConnectionInternal obj)
		{
			Bid.PoolerTrace("<prov.DbConnectionPool.PutNewObject|RES|CPOOL> %d#, Connection %d#, Pushing to general pool.\n", this.ObjectID, obj.ObjectID);
			this._stackNew.Push(obj);
			this._waitHandles.PoolSemaphore.Release(1);
			this.PerformanceCounters.NumberOfFreeConnections.Increment();
		}

		// Token: 0x06002AD9 RID: 10969 RVA: 0x00119E04 File Offset: 0x00119204
		internal void PutObject(DbConnectionInternal obj, object owningObject)
		{
			this.PerformanceCounters.SoftDisconnectsPerSecond.Increment();
			lock (obj)
			{
				obj.PrePush(owningObject);
			}
			this.DeactivateObject(obj);
		}

		// Token: 0x06002ADA RID: 10970 RVA: 0x00119E64 File Offset: 0x00119264
		internal void PutObjectFromTransactedPool(DbConnectionInternal obj)
		{
			Bid.PoolerTrace("<prov.DbConnectionPool.PutObjectFromTransactedPool|RES|CPOOL> %d#, Connection %d#, Transaction has ended.\n", this.ObjectID, obj.ObjectID);
			if (this._state == DbConnectionPool.State.Running && obj.CanBePooled)
			{
				this.PutNewObject(obj);
				return;
			}
			this.DestroyObject(obj);
			this.QueuePoolCreateRequest();
		}

		// Token: 0x06002ADB RID: 10971 RVA: 0x00119EB0 File Offset: 0x001192B0
		private void QueuePoolCreateRequest()
		{
			if (DbConnectionPool.State.Running == this._state)
			{
				ThreadPool.QueueUserWorkItem(this._poolCreateRequest);
			}
		}

		// Token: 0x06002ADC RID: 10972 RVA: 0x00119ED4 File Offset: 0x001192D4
		private bool ReclaimEmancipatedObjects()
		{
			bool result = false;
			Bid.PoolerTrace("<prov.DbConnectionPool.ReclaimEmancipatedObjects|RES|CPOOL> %d#\n", this.ObjectID);
			List<DbConnectionInternal> list = new List<DbConnectionInternal>();
			List<DbConnectionInternal> objectList = this._objectList;
			int count;
			lock (objectList)
			{
				count = this._objectList.Count;
				for (int i = 0; i < count; i++)
				{
					DbConnectionInternal dbConnectionInternal = this._objectList[i];
					if (dbConnectionInternal != null)
					{
						bool flag2 = false;
						try
						{
							Monitor.TryEnter(dbConnectionInternal, ref flag2);
							if (flag2 && dbConnectionInternal.IsEmancipated)
							{
								dbConnectionInternal.PrePush(null);
								list.Add(dbConnectionInternal);
							}
						}
						finally
						{
							if (flag2)
							{
								Monitor.Exit(dbConnectionInternal);
							}
						}
					}
				}
			}
			count = list.Count;
			for (int j = 0; j < count; j++)
			{
				DbConnectionInternal dbConnectionInternal2 = list[j];
				Bid.PoolerTrace("<prov.DbConnectionPool.ReclaimEmancipatedObjects|RES|CPOOL> %d#, Connection %d#, Reclaiming.\n", this.ObjectID, dbConnectionInternal2.ObjectID);
				this.PerformanceCounters.NumberOfReclaimedConnections.Increment();
				result = true;
				dbConnectionInternal2.DetachCurrentTransactionIfEnded();
				this.DeactivateObject(dbConnectionInternal2);
			}
			return result;
		}

		// Token: 0x06002ADD RID: 10973 RVA: 0x0011A008 File Offset: 0x00119408
		internal void Startup()
		{
			Bid.PoolerTrace("<prov.DbConnectionPool.Startup|RES|INFO|CPOOL> %d#, CleanupWait=%d\n", this.ObjectID, this._cleanupWait);
			this._cleanupTimer = this.CreateCleanupTimer();
			if (this.NeedToReplenish)
			{
				this.QueuePoolCreateRequest();
			}
		}

		// Token: 0x06002ADE RID: 10974 RVA: 0x0011A048 File Offset: 0x00119448
		internal void Shutdown()
		{
			Bid.PoolerTrace("<prov.DbConnectionPool.Shutdown|RES|INFO|CPOOL> %d#\n", this.ObjectID);
			this._state = DbConnectionPool.State.ShuttingDown;
			Timer cleanupTimer = this._cleanupTimer;
			this._cleanupTimer = null;
			if (cleanupTimer != null)
			{
				cleanupTimer.Dispose();
			}
		}

		// Token: 0x06002ADF RID: 10975 RVA: 0x0011A084 File Offset: 0x00119484
		internal void TransactionEnded(Transaction transaction, DbConnectionInternal transactedObject)
		{
			Bid.PoolerTrace("<prov.DbConnectionPool.TransactionEnded|RES|CPOOL> %d#, Transaction %d#, Connection %d#, Transaction Completed\n", this.ObjectID, transaction.GetHashCode(), transactedObject.ObjectID);
			DbConnectionPool.TransactedConnectionPool transactedConnectionPool = this._transactedConnectionPool;
			if (transactedConnectionPool != null)
			{
				transactedConnectionPool.TransactionEnded(transaction, transactedObject);
			}
		}

		// Token: 0x06002AE0 RID: 10976 RVA: 0x0011A0C0 File Offset: 0x001194C0
		private DbConnectionInternal UserCreateRequest(DbConnection owningObject, DbConnectionOptions userOptions, DbConnectionInternal oldConnection = null)
		{
			DbConnectionInternal result = null;
			if (this.ErrorOccurred)
			{
				throw this.TryCloneCachedException();
			}
			if ((oldConnection != null || this.Count < this.MaxPoolSize || this.MaxPoolSize == 0) && (oldConnection != null || (this.Count & 1) == 1 || !this.ReclaimEmancipatedObjects()))
			{
				result = this.CreateObject(owningObject, userOptions, oldConnection);
			}
			return result;
		}

		// Token: 0x04001B3A RID: 6970
		internal const Bid.ApiGroup PoolerTracePoints = Bid.ApiGroup.Pooling;

		// Token: 0x04001B3B RID: 6971
		private const int MAX_Q_SIZE = 1048576;

		// Token: 0x04001B3C RID: 6972
		private const int SEMAPHORE_HANDLE = 0;

		// Token: 0x04001B3D RID: 6973
		private const int ERROR_HANDLE = 1;

		// Token: 0x04001B3E RID: 6974
		private const int CREATION_HANDLE = 2;

		// Token: 0x04001B3F RID: 6975
		private const int BOGUS_HANDLE = 3;

		// Token: 0x04001B40 RID: 6976
		private const int WAIT_OBJECT_0 = 0;

		// Token: 0x04001B41 RID: 6977
		private const int WAIT_TIMEOUT = 258;

		// Token: 0x04001B42 RID: 6978
		private const int WAIT_ABANDONED = 128;

		// Token: 0x04001B43 RID: 6979
		private const int WAIT_FAILED = -1;

		// Token: 0x04001B44 RID: 6980
		private const int ERROR_WAIT_DEFAULT = 5000;

		// Token: 0x04001B45 RID: 6981
		private static readonly Random _random = new Random(5101977);

		// Token: 0x04001B46 RID: 6982
		private readonly int _cleanupWait;

		// Token: 0x04001B47 RID: 6983
		private readonly DbConnectionPoolIdentity _identity;

		// Token: 0x04001B48 RID: 6984
		private readonly DbConnectionFactory _connectionFactory;

		// Token: 0x04001B49 RID: 6985
		private readonly DbConnectionPoolGroup _connectionPoolGroup;

		// Token: 0x04001B4A RID: 6986
		private readonly DbConnectionPoolGroupOptions _connectionPoolGroupOptions;

		// Token: 0x04001B4B RID: 6987
		private DbConnectionPoolProviderInfo _connectionPoolProviderInfo;

		// Token: 0x04001B4C RID: 6988
		private readonly ConcurrentDictionary<DbConnectionPoolAuthenticationContextKey, DbConnectionPoolAuthenticationContext> _pooledDbAuthenticationContexts;

		// Token: 0x04001B4D RID: 6989
		private DbConnectionPool.State _state;

		// Token: 0x04001B4E RID: 6990
		private readonly ConcurrentStack<DbConnectionInternal> _stackOld = new ConcurrentStack<DbConnectionInternal>();

		// Token: 0x04001B4F RID: 6991
		private readonly ConcurrentStack<DbConnectionInternal> _stackNew = new ConcurrentStack<DbConnectionInternal>();

		// Token: 0x04001B50 RID: 6992
		private readonly ConcurrentQueue<DbConnectionPool.PendingGetConnection> _pendingOpens = new ConcurrentQueue<DbConnectionPool.PendingGetConnection>();

		// Token: 0x04001B51 RID: 6993
		private int _pendingOpensWaiting;

		// Token: 0x04001B52 RID: 6994
		private readonly WaitCallback _poolCreateRequest;

		// Token: 0x04001B53 RID: 6995
		private int _waitCount;

		// Token: 0x04001B54 RID: 6996
		private readonly DbConnectionPool.PoolWaitHandles _waitHandles;

		// Token: 0x04001B55 RID: 6997
		private Exception _resError;

		// Token: 0x04001B56 RID: 6998
		private volatile bool _errorOccurred;

		// Token: 0x04001B57 RID: 6999
		private int _errorWait;

		// Token: 0x04001B58 RID: 7000
		private Timer _errorTimer;

		// Token: 0x04001B59 RID: 7001
		private Timer _cleanupTimer;

		// Token: 0x04001B5A RID: 7002
		private readonly DbConnectionPool.TransactedConnectionPool _transactedConnectionPool;

		// Token: 0x04001B5B RID: 7003
		private readonly List<DbConnectionInternal> _objectList;

		// Token: 0x04001B5C RID: 7004
		private int _totalObjects;

		// Token: 0x04001B5D RID: 7005
		private static int _objectTypeCount;

		// Token: 0x04001B5E RID: 7006
		internal readonly int _objectID = Interlocked.Increment(ref DbConnectionPool._objectTypeCount);

		// Token: 0x02000429 RID: 1065
		private enum State
		{
			// Token: 0x040022E5 RID: 8933
			Initializing,
			// Token: 0x040022E6 RID: 8934
			Running,
			// Token: 0x040022E7 RID: 8935
			ShuttingDown
		}

		// Token: 0x0200042A RID: 1066
		private sealed class TransactedConnectionList : List<DbConnectionInternal>
		{
			// Token: 0x06003608 RID: 13832 RVA: 0x001484CC File Offset: 0x001478CC
			internal TransactedConnectionList(int initialAllocation, Transaction tx) : base(initialAllocation)
			{
				this._transaction = tx;
			}

			// Token: 0x06003609 RID: 13833 RVA: 0x001484E8 File Offset: 0x001478E8
			internal void Dispose()
			{
				if (null != this._transaction)
				{
					this._transaction.Dispose();
				}
			}

			// Token: 0x040022E8 RID: 8936
			private Transaction _transaction;
		}

		// Token: 0x0200042B RID: 1067
		private sealed class PendingGetConnection
		{
			// Token: 0x0600360A RID: 13834 RVA: 0x00148510 File Offset: 0x00147910
			public PendingGetConnection(long dueTime, DbConnection owner, TaskCompletionSource<DbConnectionInternal> completion, DbConnectionOptions userOptions)
			{
				this.DueTime = dueTime;
				this.Owner = owner;
				this.Completion = completion;
			}

			// Token: 0x1700086E RID: 2158
			// (get) Token: 0x0600360B RID: 13835 RVA: 0x00148538 File Offset: 0x00147938
			// (set) Token: 0x0600360C RID: 13836 RVA: 0x0014854C File Offset: 0x0014794C
			public long DueTime { get; private set; }

			// Token: 0x1700086F RID: 2159
			// (get) Token: 0x0600360D RID: 13837 RVA: 0x00148560 File Offset: 0x00147960
			// (set) Token: 0x0600360E RID: 13838 RVA: 0x00148574 File Offset: 0x00147974
			public DbConnection Owner { get; private set; }

			// Token: 0x17000870 RID: 2160
			// (get) Token: 0x0600360F RID: 13839 RVA: 0x00148588 File Offset: 0x00147988
			// (set) Token: 0x06003610 RID: 13840 RVA: 0x0014859C File Offset: 0x0014799C
			public TaskCompletionSource<DbConnectionInternal> Completion { get; private set; }

			// Token: 0x17000871 RID: 2161
			// (get) Token: 0x06003611 RID: 13841 RVA: 0x001485B0 File Offset: 0x001479B0
			// (set) Token: 0x06003612 RID: 13842 RVA: 0x001485C4 File Offset: 0x001479C4
			public DbConnectionOptions UserOptions { get; private set; }
		}

		// Token: 0x0200042C RID: 1068
		private sealed class TransactedConnectionPool
		{
			// Token: 0x06003613 RID: 13843 RVA: 0x001485D8 File Offset: 0x001479D8
			internal TransactedConnectionPool(DbConnectionPool pool)
			{
				this._pool = pool;
				this._transactedCxns = new Dictionary<Transaction, DbConnectionPool.TransactedConnectionList>();
				Bid.PoolerTrace("<prov.DbConnectionPool.TransactedConnectionPool.TransactedConnectionPool|RES|CPOOL> %d#, Constructed for connection pool %d#\n", this.ObjectID, this._pool.ObjectID);
			}

			// Token: 0x17000872 RID: 2162
			// (get) Token: 0x06003614 RID: 13844 RVA: 0x00148628 File Offset: 0x00147A28
			internal int ObjectID
			{
				get
				{
					return this._objectID;
				}
			}

			// Token: 0x17000873 RID: 2163
			// (get) Token: 0x06003615 RID: 13845 RVA: 0x0014863C File Offset: 0x00147A3C
			internal DbConnectionPool Pool
			{
				get
				{
					return this._pool;
				}
			}

			// Token: 0x06003616 RID: 13846 RVA: 0x00148650 File Offset: 0x00147A50
			internal DbConnectionInternal GetTransactedObject(Transaction transaction)
			{
				DbConnectionInternal dbConnectionInternal = null;
				bool flag = false;
				Dictionary<Transaction, DbConnectionPool.TransactedConnectionList> transactedCxns = this._transactedCxns;
				DbConnectionPool.TransactedConnectionList transactedConnectionList;
				lock (transactedCxns)
				{
					flag = this._transactedCxns.TryGetValue(transaction, out transactedConnectionList);
				}
				if (flag)
				{
					DbConnectionPool.TransactedConnectionList obj = transactedConnectionList;
					lock (obj)
					{
						int num = transactedConnectionList.Count - 1;
						if (0 <= num)
						{
							dbConnectionInternal = transactedConnectionList[num];
							transactedConnectionList.RemoveAt(num);
						}
					}
				}
				if (dbConnectionInternal != null)
				{
					Bid.PoolerTrace("<prov.DbConnectionPool.TransactedConnectionPool.GetTransactedObject|RES|CPOOL> %d#, Transaction %d#, Connection %d#, Popped.\n", this.ObjectID, transaction.GetHashCode(), dbConnectionInternal.ObjectID);
				}
				return dbConnectionInternal;
			}

			// Token: 0x06003617 RID: 13847 RVA: 0x00148724 File Offset: 0x00147B24
			internal void PutTransactedObject(Transaction transaction, DbConnectionInternal transactedObject)
			{
				bool flag = false;
				Dictionary<Transaction, DbConnectionPool.TransactedConnectionList> transactedCxns = this._transactedCxns;
				lock (transactedCxns)
				{
					DbConnectionPool.TransactedConnectionList transactedConnectionList;
					if (flag = this._transactedCxns.TryGetValue(transaction, out transactedConnectionList))
					{
						DbConnectionPool.TransactedConnectionList obj = transactedConnectionList;
						lock (obj)
						{
							Bid.PoolerTrace("<prov.DbConnectionPool.TransactedConnectionPool.PutTransactedObject|RES|CPOOL> %d#, Transaction %d#, Connection %d#, Pushing.\n", this.ObjectID, transaction.GetHashCode(), transactedObject.ObjectID);
							transactedConnectionList.Add(transactedObject);
						}
					}
				}
				if (!flag)
				{
					Transaction transaction2 = null;
					DbConnectionPool.TransactedConnectionList transactedConnectionList2 = null;
					try
					{
						transaction2 = transaction.Clone();
						transactedConnectionList2 = new DbConnectionPool.TransactedConnectionList(2, transaction2);
						Dictionary<Transaction, DbConnectionPool.TransactedConnectionList> transactedCxns2 = this._transactedCxns;
						lock (transactedCxns2)
						{
							DbConnectionPool.TransactedConnectionList transactedConnectionList;
							if (flag = this._transactedCxns.TryGetValue(transaction, out transactedConnectionList))
							{
								DbConnectionPool.TransactedConnectionList obj2 = transactedConnectionList;
								lock (obj2)
								{
									Bid.PoolerTrace("<prov.DbConnectionPool.TransactedConnectionPool.PutTransactedObject|RES|CPOOL> %d#, Transaction %d#, Connection %d#, Pushing.\n", this.ObjectID, transaction.GetHashCode(), transactedObject.ObjectID);
									transactedConnectionList.Add(transactedObject);
									goto IL_152;
								}
							}
							Bid.PoolerTrace("<prov.DbConnectionPool.TransactedConnectionPool.PutTransactedObject|RES|CPOOL> %d#, Transaction %d#, Connection %d#, Adding List to transacted pool.\n", this.ObjectID, transaction.GetHashCode(), transactedObject.ObjectID);
							transactedConnectionList2.Add(transactedObject);
							this._transactedCxns.Add(transaction2, transactedConnectionList2);
							transaction2 = null;
						}
					}
					finally
					{
						if (null != transaction2)
						{
							if (transactedConnectionList2 != null)
							{
								transactedConnectionList2.Dispose();
							}
							else
							{
								transaction2.Dispose();
							}
						}
					}
					IL_152:
					Bid.PoolerTrace("<prov.DbConnectionPool.TransactedConnectionPool.PutTransactedObject|RES|CPOOL> %d#, Transaction %d#, Connection %d#, Added.\n", this.ObjectID, transaction.GetHashCode(), transactedObject.ObjectID);
				}
				this.Pool.PerformanceCounters.NumberOfFreeConnections.Increment();
			}

			// Token: 0x06003618 RID: 13848 RVA: 0x00148930 File Offset: 0x00147D30
			internal void TransactionEnded(Transaction transaction, DbConnectionInternal transactedObject)
			{
				Bid.PoolerTrace("<prov.DbConnectionPool.TransactedConnectionPool.TransactionEnded|RES|CPOOL> %d#, Transaction %d#, Connection %d#, Transaction Completed\n", this.ObjectID, transaction.GetHashCode(), transactedObject.ObjectID);
				int num = -1;
				Dictionary<Transaction, DbConnectionPool.TransactedConnectionList> transactedCxns = this._transactedCxns;
				lock (transactedCxns)
				{
					DbConnectionPool.TransactedConnectionList transactedConnectionList;
					if (this._transactedCxns.TryGetValue(transaction, out transactedConnectionList))
					{
						bool flag2 = false;
						DbConnectionPool.TransactedConnectionList obj = transactedConnectionList;
						lock (obj)
						{
							num = transactedConnectionList.IndexOf(transactedObject);
							if (num >= 0)
							{
								transactedConnectionList.RemoveAt(num);
							}
							if (0 >= transactedConnectionList.Count)
							{
								Bid.PoolerTrace("<prov.DbConnectionPool.TransactedConnectionPool.TransactionEnded|RES|CPOOL> %d#, Transaction %d#, Removing List from transacted pool.\n", this.ObjectID, transaction.GetHashCode());
								this._transactedCxns.Remove(transaction);
								flag2 = true;
							}
						}
						if (flag2)
						{
							transactedConnectionList.Dispose();
						}
					}
					else
					{
						Bid.PoolerTrace("<prov.DbConnectionPool.TransactedConnectionPool.TransactionEnded|RES|CPOOL> %d#, Transaction %d#, Connection %d#, Transacted pool not yet created prior to transaction completing. Connection may be leaked.\n", this.ObjectID, transaction.GetHashCode(), transactedObject.ObjectID);
					}
				}
				if (0 <= num)
				{
					this.Pool.PerformanceCounters.NumberOfFreeConnections.Decrement();
					this.Pool.PutObjectFromTransactedPool(transactedObject);
				}
			}

			// Token: 0x040022ED RID: 8941
			private Dictionary<Transaction, DbConnectionPool.TransactedConnectionList> _transactedCxns;

			// Token: 0x040022EE RID: 8942
			private DbConnectionPool _pool;

			// Token: 0x040022EF RID: 8943
			private static int _objectTypeCount;

			// Token: 0x040022F0 RID: 8944
			internal readonly int _objectID = Interlocked.Increment(ref DbConnectionPool.TransactedConnectionPool._objectTypeCount);
		}

		// Token: 0x0200042D RID: 1069
		private sealed class PoolWaitHandles : DbBuffer
		{
			// Token: 0x06003619 RID: 13849 RVA: 0x00148A6C File Offset: 0x00147E6C
			internal PoolWaitHandles() : base(3 * IntPtr.Size)
			{
				bool flag = false;
				bool flag2 = false;
				bool flag3 = false;
				this._poolSemaphore = new Semaphore(0, 1048576);
				this._errorEvent = new ManualResetEvent(false);
				this._creationSemaphore = new Semaphore(1, 1);
				RuntimeHelpers.PrepareConstrainedRegions();
				try
				{
					this._poolHandle = this._poolSemaphore.SafeWaitHandle;
					this._errorHandle = this._errorEvent.SafeWaitHandle;
					this._creationHandle = this._creationSemaphore.SafeWaitHandle;
					this._poolHandle.DangerousAddRef(ref flag);
					this._errorHandle.DangerousAddRef(ref flag2);
					this._creationHandle.DangerousAddRef(ref flag3);
					int size = IntPtr.Size;
					base.WriteIntPtr(0, this._poolHandle.DangerousGetHandle());
					base.WriteIntPtr(IntPtr.Size, this._errorHandle.DangerousGetHandle());
					base.WriteIntPtr(2 * IntPtr.Size, this._creationHandle.DangerousGetHandle());
				}
				finally
				{
					if (flag)
					{
						this._releaseFlags |= 1;
					}
					if (flag2)
					{
						this._releaseFlags |= 2;
					}
					if (flag3)
					{
						this._releaseFlags |= 4;
					}
				}
			}

			// Token: 0x17000874 RID: 2164
			// (get) Token: 0x0600361A RID: 13850 RVA: 0x00148BB0 File Offset: 0x00147FB0
			internal SafeHandle CreationHandle
			{
				[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
				get
				{
					return this._creationHandle;
				}
			}

			// Token: 0x17000875 RID: 2165
			// (get) Token: 0x0600361B RID: 13851 RVA: 0x00148BC4 File Offset: 0x00147FC4
			internal Semaphore CreationSemaphore
			{
				get
				{
					return this._creationSemaphore;
				}
			}

			// Token: 0x17000876 RID: 2166
			// (get) Token: 0x0600361C RID: 13852 RVA: 0x00148BD8 File Offset: 0x00147FD8
			internal ManualResetEvent ErrorEvent
			{
				get
				{
					return this._errorEvent;
				}
			}

			// Token: 0x17000877 RID: 2167
			// (get) Token: 0x0600361D RID: 13853 RVA: 0x00148BEC File Offset: 0x00147FEC
			internal Semaphore PoolSemaphore
			{
				get
				{
					return this._poolSemaphore;
				}
			}

			// Token: 0x0600361E RID: 13854 RVA: 0x00148C00 File Offset: 0x00148000
			protected override bool ReleaseHandle()
			{
				if ((1 & this._releaseFlags) != 0)
				{
					this._poolHandle.DangerousRelease();
				}
				if ((2 & this._releaseFlags) != 0)
				{
					this._errorHandle.DangerousRelease();
				}
				if ((4 & this._releaseFlags) != 0)
				{
					this._creationHandle.DangerousRelease();
				}
				return base.ReleaseHandle();
			}

			// Token: 0x040022F1 RID: 8945
			private readonly Semaphore _poolSemaphore;

			// Token: 0x040022F2 RID: 8946
			private readonly ManualResetEvent _errorEvent;

			// Token: 0x040022F3 RID: 8947
			private readonly Semaphore _creationSemaphore;

			// Token: 0x040022F4 RID: 8948
			private readonly SafeHandle _poolHandle;

			// Token: 0x040022F5 RID: 8949
			private readonly SafeHandle _errorHandle;

			// Token: 0x040022F6 RID: 8950
			private readonly SafeHandle _creationHandle;

			// Token: 0x040022F7 RID: 8951
			private readonly int _releaseFlags;
		}
	}
}
