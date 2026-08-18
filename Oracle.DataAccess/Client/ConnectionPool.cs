using System;
using System.Collections;
using System.EnterpriseServices;
using System.Security.Principal;
using System.Threading;

namespace Oracle.DataAccess.Client
{
	// Token: 0x02000134 RID: 308
	internal class ConnectionPool
	{
		// Token: 0x06000C4F RID: 3151 RVA: 0x0007978C File Offset: 0x0007878C
		public ConnectionPool(OpoConCtx opoConCtx, CPCtx cpCtx) : this(opoConCtx, cpCtx, null)
		{
		}

		// Token: 0x06000C50 RID: 3152 RVA: 0x00079798 File Offset: 0x00078798
		public unsafe ConnectionPool(OpoConCtx opoConCtx, CPCtx cpCtx, WindowsIdentity identity)
		{
			try
			{
				this.m_poolId = opoConCtx.conString.GetHashCode();
				if (opoConCtx.m_bSelfTuning)
				{
					this.m_stmtSamplesLimit = 1000;
					OracleTuningAgent.Register(opoConCtx.conString, opoConCtx.poolName, this.m_poolId, new OracleTuningAgent.UpdateRecommendations(this.UpdateAgentRecommendations), new OracleTuningAgent.IncrementStmtSamplesLimit(this.IncrementStmtSamplesLimit), out this.m_agentKey);
					if (-1 == this.m_agentKey)
					{
						opoConCtx.m_bSelfTuning = false;
					}
				}
			}
			catch (Exception ex)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.Trace(64U, new string[]
					{
						string.Concat(new string[]
						{
							"(ERROR) ConnectionPool::ConnectionPool(): Pool ",
							opoConCtx.conString,
							"; Exception: ",
							ex.ToString(),
							" \n"
						})
					});
				}
			}
			this.m_scsRecommendations = opoConCtx.pOpoConValCtx->StmtCacheSize;
			this.m_connections = Stack.Synchronized(new Stack(opoConCtx.minPoolSize));
			this.m_cpCtx = cpCtx;
			if (Environment.OSVersion.Version.Major <= 4)
			{
				opoConCtx.pOpoConValCtx->Enlist = 0;
			}
			else
			{
				this.m_oraResPool = new OracleResourcePool(new OracleResourcePool.TransactionEndDelegate(this.TransactionEnd));
				this.m_mtsConnections = new ResourcePool(new ResourcePool.TransactionEndDelegate(this.TransactionEnd));
			}
			try
			{
				this.m_semAvaNumOfCons = OpsCon.CreateSemaphore(IntPtr.Zero, 0, opoConCtx.maxPoolSize, "");
			}
			catch (Exception ex2)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.TraceExceptionInfo(ex2);
				}
				throw;
			}
			this.m_identity = null;
			try
			{
				if (opoConCtx.pOpoConValCtx->OSAuthent == 1 || opoConCtx.pOpoConValCtx->OSAuthent == 2)
				{
					if (identity != null)
					{
						this.m_identity = identity;
					}
					else
					{
						this.m_identity = WindowsIdentity.GetCurrent();
					}
				}
			}
			catch (Exception ex3)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.Trace(2U, new string[]
					{
						string.Concat(new string[]
						{
							" (POOL) (ERROR) ConnectionPool::ConnectionPool(): Pool: ",
							opoConCtx.poolName,
							"; Exception: ",
							ex3.ToString(),
							" \n"
						})
					});
				}
			}
			this.m_clonedCtx = (OpoConCtx)opoConCtx.Clone();
			if (opoConCtx.opoConRefCtx.password != null && opoConCtx.opoConRefCtx.password != string.Empty)
			{
				this.m_encryptedPwd = new EncryptedPassword(opoConCtx.opoConRefCtx.password);
			}
			if (opoConCtx.opoConRefCtx.proxyPassword != null && opoConCtx.opoConRefCtx.proxyPassword != string.Empty)
			{
				this.m_encryptedPxyPwd = new EncryptedPassword(opoConCtx.opoConRefCtx.proxyPassword);
			}
			this.m_clonedCtx.opoConRefCtx.password = null;
			this.m_clonedCtx.opoConRefCtx.proxyPassword = null;
			this.m_bGridRac = this.m_clonedCtx.bGridRac;
			this.m_clonedCtx.pOpoConValCtx->Pooling = opoConCtx.pOpoConValCtx->Pooling;
			this.m_clonedCtx.pOpoConValCtx->DBAPrivilege = opoConCtx.pOpoConValCtx->DBAPrivilege;
			this.m_clonedCtx.pOpoConValCtx->SetIntAndExtName = opoConCtx.pOpoConValCtx->SetIntAndExtName;
			if (this.m_cpCtx == null)
			{
				this.m_timer = new Timer(new TimerCallback(this.RegulateNumOfConsThreadFunc), null, opoConCtx.poolRegulator * 1000, opoConCtx.poolRegulator * 1000);
			}
			this.m_counter = new Counter(false);
			this.m_skipDecrement = true;
			if ((OraTrace.m_PerformanceCounters & PerfCounterLevel.NumberOfActiveConnectionPools) == PerfCounterLevel.NumberOfActiveConnectionPools)
			{
				OraclePerfCounterCollection.NumberOfActiveConnectionPools.Increment();
			}
		}

		// Token: 0x06000C51 RID: 3153 RVA: 0x00079B68 File Offset: 0x00078B68
		~ConnectionPool()
		{
			this.Dispose();
		}

		// Token: 0x06000C52 RID: 3154 RVA: 0x00079B94 File Offset: 0x00078B94
		public void ResetPasswords(string password, string proxyPassword)
		{
			if (this.m_encryptedPwd != null)
			{
				this.m_encryptedPwd.Dispose();
				this.m_encryptedPwd = null;
			}
			if (this.m_encryptedPxyPwd != null)
			{
				this.m_encryptedPxyPwd.Dispose();
				this.m_encryptedPxyPwd = null;
			}
			if (password != null && password != string.Empty)
			{
				this.m_encryptedPwd = new EncryptedPassword(password);
			}
			if (proxyPassword != null && proxyPassword != string.Empty)
			{
				this.m_encryptedPxyPwd = new EncryptedPassword(proxyPassword);
			}
		}

		// Token: 0x06000C53 RID: 3155 RVA: 0x00079C10 File Offset: 0x00078C10
		private void Dispose()
		{
			try
			{
				if (this.m_agentKey != -1)
				{
					OracleTuningAgent.Unregister(this.m_agentKey);
				}
			}
			catch (Exception ex)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.Trace(64U, new string[]
					{
						string.Concat(new object[]
						{
							"(ERROR) ConnectionPool::Dispose(): Pool Id: ",
							this.m_poolId,
							"; Exception: ",
							ex.ToString(),
							" \n"
						})
					});
				}
			}
			try
			{
				if (this.m_semAvaNumOfCons != IntPtr.Zero)
				{
					try
					{
						OpsCon.CloseHandle(this.m_semAvaNumOfCons);
					}
					catch (Exception ex2)
					{
						if (OraTrace.m_TraceLevel != 0U)
						{
							OraTrace.TraceExceptionInfo(ex2);
						}
					}
					this.m_semAvaNumOfCons = IntPtr.Zero;
				}
				if (this.m_encryptedPwd != null)
				{
					try
					{
						this.m_encryptedPwd.Dispose();
					}
					finally
					{
						this.m_encryptedPwd = null;
					}
				}
				if (this.m_encryptedPxyPwd != null)
				{
					try
					{
						this.m_encryptedPxyPwd.Dispose();
					}
					finally
					{
						this.m_encryptedPxyPwd = null;
					}
				}
				if (this.m_clonedCtx != null && this.m_clonedCtx.pOpoConValCtx != null)
				{
					try
					{
						OpsCon.FreeValCtx(ref this.m_clonedCtx.pOpoConValCtx);
					}
					catch (Exception ex3)
					{
						if (OraTrace.m_TraceLevel != 0U)
						{
							OraTrace.TraceExceptionInfo(ex3);
						}
					}
				}
				int count = this.m_connections.Count;
				while (this.m_connections.Count > 0)
				{
					PooledConCtx pooledConCtx = null;
					bool flag = this.m_cpCtx != null;
					if (flag)
					{
						Interlocked.Decrement(ref this.m_cpCtx.totalAvaliableConnections);
					}
					if (this.m_bSynchronizeStack)
					{
						lock (this.m_connections.SyncRoot)
						{
							pooledConCtx = (PooledConCtx)this.m_connections.Pop();
							goto IL_1B0;
						}
						goto IL_19E;
					}
					goto IL_19E;
					IL_1B0:
					pooledConCtx.m_conPooler = null;
					pooledConCtx.m_udtDescPoolerByName = null;
					pooledConCtx.m_udtDescPoolerByTDO = null;
					try
					{
						if (pooledConCtx.m_fetchArrayPooler != null)
						{
							pooledConCtx.m_fetchArrayPooler.Dispose();
							pooledConCtx.m_fetchArrayPooler = null;
						}
						OpsCon.Dispose(ref pooledConCtx.opsConCtx, ref pooledConCtx.opsErrCtx, ref pooledConCtx.pOpoConValCtx, pooledConCtx.opoConRefCtx);
					}
					catch (Exception ex4)
					{
						if (OraTrace.m_TraceLevel != 0U)
						{
							OraTrace.TraceExceptionInfo(ex4);
						}
					}
					if ((OraTrace.m_PerformanceCounters & PerfCounterLevel.HardDisconnectsPerSecond) == PerfCounterLevel.HardDisconnectsPerSecond)
					{
						OraclePerfCounterCollection.HardDisconnectsPerSecond.Increment();
						continue;
					}
					continue;
					IL_19E:
					pooledConCtx = (PooledConCtx)this.m_connections.Pop();
					goto IL_1B0;
				}
				if ((OraTrace.m_PerformanceCounters & PerfCounterLevel.NumberOfFreeConnections) == PerfCounterLevel.NumberOfFreeConnections)
				{
					OraclePerfCounterCollection.NumberOfFreeConnections.IncrementBy(-1 * count);
				}
			}
			catch
			{
			}
			if (this.m_inactive)
			{
				if ((OraTrace.m_PerformanceCounters & PerfCounterLevel.NumberOfInactiveConnectionPools) == PerfCounterLevel.NumberOfInactiveConnectionPools)
				{
					OraclePerfCounterCollection.NumberOfInactiveConnectionPools.Decrement();
				}
			}
			else if ((OraTrace.m_PerformanceCounters & PerfCounterLevel.NumberOfActiveConnectionPools) == PerfCounterLevel.NumberOfActiveConnectionPools)
			{
				OraclePerfCounterCollection.NumberOfActiveConnectionPools.Decrement();
			}
			GC.SuppressFinalize(this);
		}

		// Token: 0x06000C54 RID: 3156 RVA: 0x00079F88 File Offset: 0x00078F88
		public unsafe void PopulatePool(object state)
		{
			int num = 0;
			int num2 = (int)state;
			int num3 = 0;
			int num4 = 0;
			WindowsImpersonationContext windowsImpersonationContext = null;
			try
			{
				if (OraTrace.m_CPThreadPrioritization == 1)
				{
					Thread.CurrentThread.Priority = ThreadPriority.AboveNormal;
				}
			}
			catch
			{
				OraTrace.m_CPThreadPrioritization = 0;
			}
			try
			{
				if (this.m_identity != null)
				{
					try
					{
						if (WindowsIdentity.GetCurrent() != this.m_identity)
						{
							windowsImpersonationContext = this.m_identity.Impersonate();
						}
					}
					catch (Exception ex)
					{
						windowsImpersonationContext = null;
						if (OraTrace.m_TraceLevel != 0U)
						{
							OraTrace.Trace(2U, new string[]
							{
								string.Concat(new object[]
								{
									" (POOL) (ERROR) ConnectionPool::PopulatePool(): Pool (id: ",
									this.m_clonedCtx.conString.GetHashCode(),
									"); Exception: ",
									ex.ToString(),
									" \n"
								})
							});
						}
						throw;
					}
				}
				ConnectionPool connectionPool = this;
				if ((this.m_bGridRac && this.m_cpCtx.m_counter.total < this.m_clonedCtx.maxPoolSize) || (!this.m_bGridRac && this.m_counter.total < this.m_clonedCtx.maxPoolSize))
				{
					for (int i = 0; i < num2; i++)
					{
						lock (ConnectionPool.m_populationSyncObj)
						{
							if (this.m_bGridRac)
							{
								if (this.m_cpCtx.m_counter.total >= this.m_clonedCtx.maxPoolSize)
								{
									break;
								}
							}
							else if (this.m_counter.total >= this.m_clonedCtx.maxPoolSize)
							{
								break;
							}
							OpoConCtx opoConCtx = (OpoConCtx)this.m_clonedCtx.Clone();
							try
							{
								lock (this.m_passwordSyncObj)
								{
									if (this.m_encryptedPwd != null)
									{
										opoConCtx.opoConRefCtx.password = this.m_encryptedPwd.Password;
									}
									else
									{
										opoConCtx.opoConRefCtx.password = "";
									}
									if (this.m_encryptedPxyPwd != null)
									{
										opoConCtx.opoConRefCtx.proxyPassword = this.m_encryptedPxyPwd.Password;
									}
									else
									{
										opoConCtx.opoConRefCtx.proxyPassword = "";
									}
								}
								if (opoConCtx.m_bSelfTuning)
								{
									opoConCtx.pOpoConValCtx->StmtCacheSize = this.m_scsRecommendations;
									if (opoConCtx.pOpoConValCtx->StmtCacheSize > OraTrace.MaxStatementCacheSize)
									{
										opoConCtx.pOpoConValCtx->StmtCacheSize = OraTrace.MaxStatementCacheSize;
									}
								}
								num = OpsCon.Open(ref opoConCtx.opsConCtx, ref opoConCtx.opsErrCtx, opoConCtx.pOpoConValCtx, ref opoConCtx.opoConRefCtx);
								if (num == 0 && (OraTrace.m_PerformanceCounters & PerfCounterLevel.HardConnectsPerSecond) == PerfCounterLevel.HardConnectsPerSecond)
								{
									OraclePerfCounterCollection.HardConnectsPerSecond.Increment();
								}
								if (opoConCtx.metaPool == 1)
								{
									if (opoConCtx.m_bSelfTuning)
									{
										int maxElemsInPool = (opoConCtx.pOpoConValCtx->StmtCacheSize > ConPooler.DEFAULT_MAX_ELEMS_IN_POOL_TUNING_ON) ? opoConCtx.pOpoConValCtx->StmtCacheSize : ConPooler.DEFAULT_MAX_ELEMS_IN_POOL_TUNING_ON;
										opoConCtx.m_conPooler = new ConPooler(maxElemsInPool);
										opoConCtx.m_udtDescPoolerByName = new ConPooler(ConPooler.DEFAULT_MAX_ELEMS_IN_POOL_TUNING_ON);
									}
									else
									{
										opoConCtx.m_conPooler = new ConPooler(ConPooler.DEFAULT_MAX_ELEMS_IN_POOL_TUNING_OFF);
										opoConCtx.m_udtDescPoolerByName = new ConPooler(ConPooler.DEFAULT_MAX_ELEMS_IN_POOL_TUNING_OFF);
									}
								}
								if (opoConCtx.m_bSelfTuning)
								{
									opoConCtx.m_udtDescPoolerByTDO = new ConPooler(ConPooler.DEFAULT_MAX_ELEMS_IN_POOL_TUNING_ON);
								}
								else
								{
									opoConCtx.m_udtDescPoolerByTDO = new ConPooler(ConPooler.DEFAULT_MAX_ELEMS_IN_POOL_TUNING_OFF);
								}
								if (num == 0 && this.m_clonedCtx.opoConRefCtx.dbName == null)
								{
									this.m_clonedCtx.opoConRefCtx.dbName = opoConCtx.opoConRefCtx.dbName;
									this.m_clonedCtx.opoConRefCtx.hostName = opoConCtx.opoConRefCtx.hostName;
									this.m_clonedCtx.opoConRefCtx.serviceName = opoConCtx.opoConRefCtx.serviceName;
									this.m_clonedCtx.opoConRefCtx.dbDomainName = opoConCtx.opoConRefCtx.dbDomainName;
								}
								if (num == 0 && (num = ConnectionDispenser.RegisterCallbacks(opoConCtx)) != 0)
								{
									OpoConValCtx* ptr = null;
									try
									{
										OpsCon.Close(ref opoConCtx.opsConCtx, ref opoConCtx.opsErrCtx, opoConCtx.pOpoConValCtx, opoConCtx.opoConRefCtx);
									}
									catch (Exception ex2)
									{
										if (OraTrace.m_TraceLevel != 0U)
										{
											OraTrace.TraceExceptionInfo(ex2);
										}
									}
									if ((OraTrace.m_PerformanceCounters & PerfCounterLevel.HardDisconnectsPerSecond) == PerfCounterLevel.HardDisconnectsPerSecond)
									{
										OraclePerfCounterCollection.HardDisconnectsPerSecond.Increment();
									}
									try
									{
										if (opoConCtx.m_fetchArrayPooler != null)
										{
											opoConCtx.m_fetchArrayPooler.Dispose();
											opoConCtx.m_fetchArrayPooler = null;
										}
										OpsCon.Dispose(ref opoConCtx.opsConCtx, ref opoConCtx.opsErrCtx, ref ptr, opoConCtx.opoConRefCtx);
									}
									catch (Exception ex3)
									{
										if (OraTrace.m_TraceLevel != 0U)
										{
											OraTrace.TraceExceptionInfo(ex3);
										}
									}
								}
								if (num != 0)
								{
									if (opoConCtx.validateCon == 1)
									{
										goto IL_7ED;
									}
									lock (this.m_bGridRac ? this.m_cpCtx.m_counter : this.m_counter)
									{
										if ((this.m_bGridRac && this.m_cpCtx.m_counter.threadWait <= this.m_cpCtx.totalAvaliableConnections) || (!this.m_bGridRac && this.m_counter.threadWait <= this.m_counter.totalAvailable))
										{
											goto IL_7ED;
										}
									}
								}
								opoConCtx.creationTime = DateTime.Now;
								this.m_skipDecrement = true;
								if (this.m_cpCtx != null)
								{
									this.m_cpCtx.m_cpCtxSkipDecrement = true;
								}
								bool flag4 = false;
								if (opoConCtx.pOpoConValCtx->SessionBegin == 1)
								{
									flag4 = true;
								}
								int num5;
								if (this.m_bGridRac)
								{
									lock (this.m_cpCtx.htInstToCp.SyncRoot)
									{
										if (this.m_cpCtx.htInstToCp[opoConCtx.opoConRefCtx.instanceName] == null)
										{
											connectionPool = new ConnectionPool(opoConCtx, this.m_cpCtx, this.m_identity);
											this.m_cpCtx.htInstToCp[opoConCtx.opoConRefCtx.instanceName] = connectionPool;
											if (OraTrace.m_TraceLevel != 0U)
											{
												OraTrace.Trace(2U, new string[]
												{
													string.Concat(new object[]
													{
														" (POOL)  New CP created (CP id: ",
														connectionPool.GetHashCode(),
														"; CPCtx id: ",
														this.m_cpCtx.GetHashCode(),
														") for: [Instance=",
														opoConCtx.opoConRefCtx.instanceName,
														"]\n"
													})
												});
												OraTrace.Trace(2U, new string[]
												{
													string.Concat(new object[]
													{
														" (POOL)  Num of Inst CPs in (CPCtx id: ",
														this.m_cpCtx.GetHashCode(),
														") : ",
														this.m_cpCtx.htInstToCp.Count,
														"\n"
													})
												});
											}
										}
										else
										{
											connectionPool = (ConnectionPool)this.m_cpCtx.htInstToCp[opoConCtx.opoConRefCtx.instanceName];
										}
									}
									if (this.m_bSynchronizeStack)
									{
										lock (this.m_connections)
										{
											num5 = connectionPool.PutConnection(ref opoConCtx, true, false, true, 0);
											goto IL_769;
										}
									}
									num5 = connectionPool.PutConnection(ref opoConCtx, true, false, true, 0);
								}
								else
								{
									connectionPool = this;
									if (this.m_bSynchronizeStack)
									{
										lock (this.m_connections)
										{
											num5 = this.PutConnection(ref opoConCtx, true, false, false, 0);
											goto IL_769;
										}
									}
									num5 = this.PutConnection(ref opoConCtx, true, false, false, 0);
								}
								IL_769:
								if ((OraTrace.m_PerformanceCounters & PerfCounterLevel.SoftDisconnectsPerSecond) == PerfCounterLevel.SoftDisconnectsPerSecond)
								{
									OraclePerfCounterCollection.SoftDisconnectsPerSecond.Increment();
								}
								if (flag4 && num == 0 && num5 == 0)
								{
									connectionPool.UpdateTotalCount(1, false);
									if (connectionPool == this)
									{
										num3++;
									}
									else
									{
										connectionPool.m_counter.UpdatePotentialTotalCount(1);
									}
									num4++;
								}
							}
							catch (Exception ex4)
							{
								if (OraTrace.m_TraceLevel != 0U)
								{
									OraTrace.TraceExceptionInfo(ex4);
								}
							}
							finally
							{
								opoConCtx.opoConRefCtx.password = null;
								opoConCtx.opoConRefCtx.proxyPassword = null;
							}
						}
						IL_7ED:;
					}
				}
				if (this.m_bGridRac && num2 - num4 > 0)
				{
					this.m_cpCtx.m_counter.UpdatePotentialTotalCount(num4 - num2);
				}
				if (num2 - num3 > 0)
				{
					this.m_counter.UpdatePotentialTotalCount(num3 - num2);
				}
			}
			finally
			{
				if (windowsImpersonationContext != null)
				{
					try
					{
						windowsImpersonationContext.Undo();
					}
					catch (Exception ex5)
					{
						if (OraTrace.m_TraceLevel != 0U)
						{
							OraTrace.Trace(2U, new string[]
							{
								string.Concat(new object[]
								{
									" (POOL) (ERROR) ConnectionPool::PopulatePool(): Pool (id: ",
									this.m_clonedCtx.conString.GetHashCode(),
									"); Exception: ",
									ex5.ToString(),
									" \n"
								})
							});
						}
					}
					windowsImpersonationContext = null;
				}
			}
		}

		// Token: 0x06000C55 RID: 3157 RVA: 0x0007A99C File Offset: 0x0007999C
		public void UpdateTotalCount(int val, bool bForPotential)
		{
			if (this.m_clonedCtx.bGridRac)
			{
				this.m_cpCtx.m_counter.UpdateTotalCount(this, val, bForPotential);
			}
			this.m_counter.UpdateTotalCount(this, val, bForPotential);
		}

		// Token: 0x06000C56 RID: 3158 RVA: 0x0007A9CC File Offset: 0x000799CC
		public void UpdatePotentialTotalCount(int val)
		{
			if (this.m_clonedCtx.bGridRac)
			{
				this.m_cpCtx.m_counter.UpdatePotentialTotalCount(val);
			}
			this.m_counter.UpdatePotentialTotalCount(val);
		}

		// Token: 0x06000C57 RID: 3159 RVA: 0x0007A9F8 File Offset: 0x000799F8
		public void UpdateThreadWaitCount(int val)
		{
			this.m_counter.UpdateThreadWaitCount(this, val);
		}

		// Token: 0x06000C58 RID: 3160 RVA: 0x0007AA07 File Offset: 0x00079A07
		public void RegulateNumOfConsThreadFunc(object state)
		{
			this.RegulateNumOfCons(state);
		}

		// Token: 0x06000C59 RID: 3161 RVA: 0x0007AA14 File Offset: 0x00079A14
		public void GetDisposalInfo(int totDecrCount, ref ConnectionPool[] conPool, ref int[] consToClose)
		{
			conPool = null;
			consToClose = null;
			int[] array = null;
			int num = 0;
			int num2 = int.MaxValue;
			if (this.m_cpCtx != null && this.m_cpCtx.htInstToCp != null && this.m_cpCtx.htInstToCp.Count > 0)
			{
				int count = this.m_cpCtx.htInstToCp.Count;
				int num3 = this.m_cpCtx.m_random.Next(0, count);
				lock (this.m_cpCtx.htInstToCp)
				{
					IDictionaryEnumerator enumerator = this.m_cpCtx.htInstToCp.GetEnumerator();
					conPool = new ConnectionPool[count];
					array = new int[count];
					while (enumerator.MoveNext())
					{
						conPool[num3] = (ConnectionPool)enumerator.Value;
						array[num3] = conPool[num3].m_connections.Count;
						num += array[num3];
						if (array[num3] < num2)
						{
							num2 = array[num3];
						}
						num3 = (num3 + 1) % count;
					}
				}
				if (num <= totDecrCount)
				{
					consToClose = array;
					return;
				}
				int num4 = 0;
				consToClose = new int[count];
				if (num2 * count > totDecrCount)
				{
					num2 = totDecrCount / count;
					for (int i = 0; i < count; i++)
					{
						consToClose[i] = num2;
						array[i] -= num2;
						num4 += num2;
					}
				}
				for (int i = 0; i < count * totDecrCount; i++)
				{
					num3 = i % count;
					if (array[num3] > 0)
					{
						consToClose[num3]++;
						array[num3]--;
						num4++;
						if (num4 >= totDecrCount)
						{
							return;
						}
					}
				}
			}
		}

		// Token: 0x06000C5A RID: 3162 RVA: 0x0007ABD8 File Offset: 0x00079BD8
		public unsafe int RegulateNumOfCons(object state)
		{
			bool flag = false;
			int num = 0;
			bool flag2 = state == null;
			bool bGridRac = this.m_clonedCtx.bGridRac;
			bool flag3 = false;
			try
			{
				if (this.m_cpCtx != null)
				{
					Monitor.Enter(this.m_cpCtx);
				}
				int num2;
				int total;
				int potentialTotal;
				int num3;
				bool flag4;
				if (bGridRac)
				{
					num2 = this.m_cpCtx.totalAvaliableConnections;
					total = this.m_cpCtx.m_counter.total;
					potentialTotal = this.m_cpCtx.m_counter.potentialTotal;
					num3 = this.m_clonedCtx.origMinPoolSize;
					flag4 = this.m_cpCtx.m_cpCtxSkipDecrement;
				}
				else
				{
					num2 = this.m_connections.Count;
					total = this.m_counter.total;
					potentialTotal = this.m_counter.potentialTotal;
					num3 = this.m_clonedCtx.minPoolSize;
					flag4 = this.m_skipDecrement;
				}
				if (num2 > 0 && (total > num3 || this.m_rlbGravCounter > 0 || (state != null && (int)state == -1)) && (!flag4 || state != null))
				{
					int num4 = 0;
					if (state == null)
					{
						if (num2 < this.m_clonedCtx.poolDecSize)
						{
							num4 = num2;
						}
						else
						{
							num4 = this.m_clonedCtx.poolDecSize;
						}
						if (total - num4 < num3)
						{
							num4 = total - num3;
						}
					}
					else
					{
						lock (this)
						{
							if ((int)state == -1)
							{
								num4 = this.m_clonedCtx.maxPoolSize;
								flag3 = true;
							}
							else if (this.m_rlbGravCounter <= 0)
							{
								num4 = Math.Min((int)state, total - num3);
							}
							else if (this.m_rlbGravCounter > 0)
							{
								num4 = this.m_rlbGravCounter;
								this.m_rlbGravCounter = 0;
							}
						}
					}
					bool flag6 = this.m_cpCtx != null && this.m_cpCtx.m_rlbCtx.RLBMetricsList != null && this.m_cpCtx.m_rlbCtx.RLBMetricsList.Count != 0;
					if (bGridRac && flag2 && !flag6)
					{
						ConnectionPool[] array = null;
						int[] array2 = null;
						this.GetDisposalInfo(num4, ref array, ref array2);
						if (array != null)
						{
							for (int i = 0; i < array.Length; i++)
							{
								array[i].RegulateNumOfCons(array2[i]);
								array[i] = null;
							}
						}
					}
					else if (!bGridRac || !flag2 || !flag6)
					{
						for (int j = 0; j < num4; j++)
						{
							PooledConCtx pooledConCtx = null;
							try
							{
								int num5 = 0;
								if (bGridRac && this.m_cpCtx != null)
								{
									num5 = OpsCon.WaitForSingleObject(this.m_cpCtx.m_semCPCtxAvaNumOfCons, (int)this.m_clonedCtx.timeOut.TotalSeconds * 1000 * 2);
								}
								int num6 = OpsCon.WaitForSingleObject(this.m_semAvaNumOfCons, 0);
								if (num6 == 0 && num5 == 0)
								{
									if (bGridRac && this.m_cpCtx != null)
									{
										Interlocked.Decrement(ref this.m_cpCtx.totalAvaliableConnections);
									}
									Interlocked.Decrement(ref this.m_counter.totalAvailable);
									if (this.m_bSynchronizeStack)
									{
										lock (this.m_connections.SyncRoot)
										{
											pooledConCtx = (PooledConCtx)this.m_connections.Pop();
											goto IL_31B;
										}
									}
									pooledConCtx = (PooledConCtx)this.m_connections.Pop();
									IL_31B:
									if (pooledConCtx != null && (OraTrace.m_PerformanceCounters & PerfCounterLevel.NumberOfFreeConnections) == PerfCounterLevel.NumberOfFreeConnections)
									{
										OraclePerfCounterCollection.NumberOfFreeConnections.Decrement();
									}
									if (pooledConCtx.pOpoConValCtx->bTAFEnabled == 1)
									{
										ConnectionPool connectionPool = this;
										string instanceName = pooledConCtx.opoConRefCtx.instanceName;
										OpsCon.GetAttributes(pooledConCtx.opsConCtx, pooledConCtx.opsErrCtx, pooledConCtx.opoConRefCtx);
										if (instanceName != pooledConCtx.opoConRefCtx.instanceName)
										{
											OraTrace.Trace(16U, new string[]
											{
												string.Concat(new string[]
												{
													" (FO)    Failed over from ",
													instanceName,
													" to ",
													pooledConCtx.opoConRefCtx.instanceName,
													"\n"
												})
											});
											flag = true;
											connectionPool = (ConnectionPool)this.m_cpCtx.htInstToCp[pooledConCtx.opoConRefCtx.instanceName];
											if (connectionPool == null)
											{
												lock (this.m_cpCtx.htInstToCp.SyncRoot)
												{
													if (this.m_cpCtx.htInstToCp[pooledConCtx.opoConRefCtx.instanceName] == null)
													{
														OpoConCtx opoConCtx = new OpoConCtx();
														opoConCtx.pooledConCtx = pooledConCtx;
														opoConCtx.opsConCtx = pooledConCtx.opsConCtx;
														opoConCtx.opsErrCtx = pooledConCtx.opsErrCtx;
														opoConCtx.pOpoConValCtx = pooledConCtx.pOpoConValCtx;
														opoConCtx.opoConRefCtx = pooledConCtx.opoConRefCtx;
														connectionPool = new ConnectionPool(opoConCtx, this.m_cpCtx, this.m_identity);
														this.m_cpCtx.htInstToCp[opoConCtx.opoConRefCtx.instanceName] = connectionPool;
														if (OraTrace.m_TraceLevel != 0U)
														{
															OraTrace.Trace(2U, new string[]
															{
																string.Concat(new object[]
																{
																	" (POOL)  New CP created (CP id: ",
																	connectionPool.GetHashCode(),
																	"; CPCtx id: ",
																	connectionPool.m_cpCtx.GetHashCode(),
																	") for: [Instance=",
																	opoConCtx.opoConRefCtx.instanceName,
																	"]\n"
																})
															});
															OraTrace.Trace(2U, new string[]
															{
																string.Concat(new object[]
																{
																	" (POOL)  Num of Inst CPs in (CPCtx id: ",
																	connectionPool.m_cpCtx.GetHashCode(),
																	") : ",
																	connectionPool.m_cpCtx.htInstToCp.Count,
																	"\n"
																})
															});
														}
													}
												}
											}
											if (this.m_bSynchronizeStack)
											{
												lock (connectionPool.m_connections.SyncRoot)
												{
													connectionPool.m_connections.Push(pooledConCtx);
													goto IL_618;
												}
											}
											connectionPool.m_connections.Push(pooledConCtx);
											IL_618:
											if ((OraTrace.m_PerformanceCounters & PerfCounterLevel.NumberOfFreeConnections) == PerfCounterLevel.NumberOfFreeConnections)
											{
												OraclePerfCounterCollection.NumberOfFreeConnections.Increment();
											}
											if (bGridRac && connectionPool.m_cpCtx != null)
											{
												Interlocked.Increment(ref connectionPool.m_cpCtx.totalAvaliableConnections);
											}
											Interlocked.Increment(ref connectionPool.m_counter.totalAvailable);
											connectionPool.ReleaseSemaphore();
											Interlocked.Decrement(ref this.m_counter.total);
											Interlocked.Increment(ref connectionPool.m_counter.total);
										}
										else
										{
											num++;
											this.UpdateTotalCount(-1, true);
										}
									}
									else
									{
										num++;
										this.UpdateTotalCount(-1, true);
									}
								}
								else if (bGridRac && this.m_cpCtx != null && num5 == 0)
								{
									int num7 = 0;
									OpsCon.ReleaseSemaphore(this.m_cpCtx.m_semCPCtxAvaNumOfCons, 1, ref num7);
								}
								else if (num6 == 0)
								{
									int num8 = 0;
									OpsCon.ReleaseSemaphore(this.m_semAvaNumOfCons, 1, ref num8);
								}
							}
							catch (Exception ex)
							{
								if (OraTrace.m_TraceLevel != 0U)
								{
									OraTrace.TraceExceptionInfo(ex);
								}
							}
							if (pooledConCtx == null)
							{
								break;
							}
							if (!flag)
							{
								try
								{
									if (flag3)
									{
										pooledConCtx.pOpoConValCtx->HABasedConClose = 1;
									}
									if (pooledConCtx.m_fetchArrayPooler != null)
									{
										pooledConCtx.m_fetchArrayPooler.Dispose();
										pooledConCtx.m_fetchArrayPooler = null;
									}
									OpsCon.Dispose(ref pooledConCtx.opsConCtx, ref pooledConCtx.opsErrCtx, ref pooledConCtx.pOpoConValCtx, pooledConCtx.opoConRefCtx);
								}
								catch (Exception ex2)
								{
									if (OraTrace.m_TraceLevel != 0U)
									{
										OraTrace.TraceExceptionInfo(ex2);
									}
								}
								finally
								{
									if ((OraTrace.m_PerformanceCounters & PerfCounterLevel.HardDisconnectsPerSecond) == PerfCounterLevel.HardDisconnectsPerSecond)
									{
										OraclePerfCounterCollection.HardDisconnectsPerSecond.Increment();
									}
									pooledConCtx.opsConCtx = IntPtr.Zero;
									pooledConCtx.opsErrCtx = IntPtr.Zero;
									pooledConCtx.pOpoConValCtx = null;
									pooledConCtx.m_conPooler = null;
									pooledConCtx.m_udtDescPoolerByName = null;
									pooledConCtx.m_udtDescPoolerByTDO = null;
								}
							}
						}
					}
				}
				else
				{
					if (total == 0 && num2 == 0 && num3 == 0 && potentialTotal == 0 && flag2)
					{
						if (bGridRac)
						{
							lock (this.m_cpCtx.m_rlbCtx.htConToInst.SyncRoot)
							{
								if (this.m_cpCtx.m_counter.total == 0 && this.m_cpCtx.totalAvaliableConnections == 0 && this.m_clonedCtx.origMinPoolSize == 0 && this.m_cpCtx.m_counter.potentialTotal == 0)
								{
									this.m_cpCtx.m_rlbCtx.htConToInst.Remove(this.m_clonedCtx.conString);
								}
								DeriveParamInfo.m_pooler.RemovePool(this.m_clonedCtx.conString);
								this.m_cpCtx.m_timer.Dispose();
								goto IL_9C6;
							}
						}
						lock (ConnectionDispenser.s_lockObj)
						{
							if (this.m_counter.total == 0 && this.m_connections.Count == 0 && this.m_clonedCtx.minPoolSize == 0 && this.m_counter.potentialTotal == 0)
							{
								ConnectionPool connectionPool2 = (ConnectionPool)ConnectionDispenser.m_ConnectionPools[this.m_clonedCtx.conString];
								ConnectionDispenser.m_ConnectionPools.Remove(this.m_clonedCtx.conString);
								connectionPool2.Dispose();
								DeriveParamInfo.m_pooler.RemovePool(this.m_clonedCtx.conString);
								this.m_timer.Dispose();
							}
							goto IL_9C6;
						}
					}
					if (potentialTotal < num3)
					{
						int num9;
						if (potentialTotal + this.m_clonedCtx.poolIncSize > this.m_clonedCtx.maxPoolSize)
						{
							num9 = this.m_clonedCtx.maxPoolSize - potentialTotal;
						}
						else
						{
							num9 = this.m_clonedCtx.poolIncSize;
						}
						if (num9 > 0)
						{
							ThreadPool.QueueUserWorkItem(new WaitCallback(this.PopulatePool), num9);
							this.UpdatePotentialTotalCount(num9);
						}
					}
				}
				IL_9C6:;
			}
			catch
			{
			}
			finally
			{
				if (this.m_cpCtx != null)
				{
					Monitor.Exit(this.m_cpCtx);
				}
			}
			if (state == null)
			{
				this.m_skipDecrement = false;
			}
			if (this.m_cpCtx != null)
			{
				this.m_cpCtx.m_cpCtxSkipDecrement = false;
			}
			int num10 = bGridRac ? this.m_cpCtx.m_counter.total : this.m_counter.total;
			int num11 = bGridRac ? this.m_cpCtx.m_counter.totalAvailable : this.m_counter.totalAvailable;
			if (num10 == num11)
			{
				if (!this.m_inactive)
				{
					this.m_inactive = true;
					if ((OraTrace.m_PerformanceCounters & PerfCounterLevel.NumberOfActiveConnectionPools) == PerfCounterLevel.NumberOfActiveConnectionPools)
					{
						OraclePerfCounterCollection.NumberOfActiveConnectionPools.Decrement();
					}
					if ((OraTrace.m_PerformanceCounters & PerfCounterLevel.NumberOfInactiveConnectionPools) == PerfCounterLevel.NumberOfInactiveConnectionPools)
					{
						OraclePerfCounterCollection.NumberOfInactiveConnectionPools.Increment();
					}
				}
			}
			else if (this.m_inactive)
			{
				this.m_inactive = false;
				if ((OraTrace.m_PerformanceCounters & PerfCounterLevel.NumberOfActiveConnectionPools) == PerfCounterLevel.NumberOfActiveConnectionPools)
				{
					OraclePerfCounterCollection.NumberOfActiveConnectionPools.Increment();
				}
				if ((OraTrace.m_PerformanceCounters & PerfCounterLevel.NumberOfInactiveConnectionPools) == PerfCounterLevel.NumberOfInactiveConnectionPools)
				{
					OraclePerfCounterCollection.NumberOfInactiveConnectionPools.Decrement();
				}
			}
			return num;
		}

		// Token: 0x06000C5B RID: 3163 RVA: 0x0007B7B8 File Offset: 0x0007A7B8
		public unsafe int CheckLifeTimeAndStatus(ref OpoConCtx opoConCtx, int bDistTxnEnd, ref bool bClosed, int bFromPool, bool bCheckLifetimeOnly)
		{
			int result = 0;
			bool bGridRac = opoConCtx.bGridRac;
			bool flag = false;
			if (!bGridRac || bFromPool != 1)
			{
				int total;
				if (this.m_cpCtx != null)
				{
					total = this.m_cpCtx.m_counter.total;
				}
				else
				{
					total = this.m_counter.total;
				}
				lock (this)
				{
					if (this.m_rlbGravCounter > 0)
					{
						this.m_rlbGravCounter--;
						flag = true;
					}
				}
				if ((bFromPool != 0 || !(opoConCtx.lifeTime > TimeSpan.Zero) || total <= opoConCtx.minPoolSize || !(opoConCtx.lifeTime < DateTime.Now.Subtract(opoConCtx.creationTime))) && (!(this.m_clonedCtx.lifeTime == new TimeSpan(1L)) || !(this.m_clonedCtx.origLifeTime < DateTime.Now.Subtract(opoConCtx.creationTime))))
				{
					if (!flag)
					{
						goto IL_1DC;
					}
				}
				try
				{
					if (opoConCtx.m_fetchArrayPooler != null)
					{
						opoConCtx.m_fetchArrayPooler.Dispose();
						opoConCtx.m_fetchArrayPooler = null;
					}
					OpsCon.Dispose(ref opoConCtx.opsConCtx, ref opoConCtx.opsErrCtx, ref opoConCtx.pOpoConValCtx, opoConCtx.opoConRefCtx);
				}
				catch (Exception ex)
				{
					if (OraTrace.m_TraceLevel != 0U)
					{
						OraTrace.TraceExceptionInfo(ex);
					}
				}
				if ((OraTrace.m_PerformanceCounters & PerfCounterLevel.HardDisconnectsPerSecond) == PerfCounterLevel.HardDisconnectsPerSecond)
				{
					OraclePerfCounterCollection.HardDisconnectsPerSecond.Increment();
				}
				opoConCtx.pooledConCtx = null;
				opoConCtx.opsConCtx = IntPtr.Zero;
				opoConCtx.opsErrCtx = IntPtr.Zero;
				opoConCtx.m_conPooler = null;
				opoConCtx.m_udtDescPoolerByName = null;
				opoConCtx.m_udtDescPoolerByTDO = null;
				opoConCtx.m_systemTransaction = null;
				opoConCtx.m_txnType = TxnType.None;
				this.UpdateTotalCount(-1, true);
				bClosed = true;
				if (flag)
				{
					this.UpdatePotentialTotalCount(1);
					ThreadPool.QueueUserWorkItem(new WaitCallback(this.PopulatePool), 1);
				}
				return 0;
			}
			IL_1DC:
			if (bCheckLifetimeOnly)
			{
				bClosed = false;
				return 0;
			}
			try
			{
				if (bFromPool == 0 && (opoConCtx.pOpoConValCtx->InMtsTxn == 1 || (opoConCtx.opoConRefCtx.proxyUserId != null && opoConCtx.opoConRefCtx.proxyUserId.Length > 0) || opoConCtx.pOpoConValCtx->OSAuthent == 2))
				{
					OpsCon.CloseProxyAuthUserSession(opoConCtx.opsConCtx, opoConCtx.opsErrCtx, opoConCtx.pOpoConValCtx, opoConCtx.opoConRefCtx);
				}
			}
			catch (Exception ex2)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.TraceExceptionInfo(ex2);
				}
			}
			int num = 1;
			bool flag3 = false;
			try
			{
				if (bFromPool == 0 || opoConCtx.validateCon == 1)
				{
					num = 0;
					OpsCon.CheckConStatus(opoConCtx.opsConCtx, opoConCtx.opsErrCtx, bDistTxnEnd, ref num, bFromPool, opoConCtx.validateCon);
				}
			}
			catch (Exception ex3)
			{
				flag3 = true;
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.TraceExceptionInfo(ex3);
				}
			}
			if (num == 0)
			{
				OpoConValCtx* ptr = null;
				if (bFromPool == 1)
				{
					if (OraTrace.m_TraceLevel != 0U)
					{
						if (flag3)
						{
							OraTrace.Trace(1U, new string[]
							{
								" (VALID) Exception in OpsCon.CheckConStatus\n"
							});
						}
						else
						{
							OraTrace.Trace(1U, new string[]
							{
								" (VALID) Dead connection\n"
							});
						}
					}
					try
					{
						OpsCon.Close(ref opoConCtx.opsConCtx, ref opoConCtx.opsErrCtx, opoConCtx.pOpoConValCtx, opoConCtx.opoConRefCtx);
						goto IL_344;
					}
					catch (Exception ex4)
					{
						if (OraTrace.m_TraceLevel != 0U)
						{
							OraTrace.TraceExceptionInfo(ex4);
						}
						goto IL_344;
					}
				}
				ptr = opoConCtx.pOpoConValCtx;
				opoConCtx.pOpoConValCtx = null;
				try
				{
					IL_344:
					if (opoConCtx.m_fetchArrayPooler != null)
					{
						opoConCtx.m_fetchArrayPooler.Dispose();
						opoConCtx.m_fetchArrayPooler = null;
					}
					OpsCon.Dispose(ref opoConCtx.opsConCtx, ref opoConCtx.opsErrCtx, ref ptr, opoConCtx.opoConRefCtx);
				}
				catch (Exception ex5)
				{
					if (OraTrace.m_TraceLevel != 0U)
					{
						OraTrace.TraceExceptionInfo(ex5);
					}
				}
				if ((OraTrace.m_PerformanceCounters & PerfCounterLevel.HardDisconnectsPerSecond) == PerfCounterLevel.HardDisconnectsPerSecond)
				{
					OraclePerfCounterCollection.HardDisconnectsPerSecond.Increment();
				}
				opoConCtx.pooledConCtx = null;
				opoConCtx.opsConCtx = IntPtr.Zero;
				opoConCtx.opsErrCtx = IntPtr.Zero;
				opoConCtx.m_conPooler = null;
				opoConCtx.m_udtDescPoolerByName = null;
				opoConCtx.m_udtDescPoolerByTDO = null;
				opoConCtx.m_systemTransaction = null;
				opoConCtx.m_txnType = TxnType.None;
				this.UpdateTotalCount(-1, true);
				bClosed = true;
			}
			else
			{
				bClosed = false;
			}
			return result;
		}

		// Token: 0x06000C5C RID: 3164 RVA: 0x0007BC14 File Offset: 0x0007AC14
		private void GetEnlistedConnection(ref PooledConCtx pooledConCtx, OpoConCtx opoConCtx)
		{
			try
			{
				if (opoConCtx.m_txnType == TxnType.SystemTxn)
				{
					pooledConCtx = (PooledConCtx)this.m_oraResPool.GetResource(opoConCtx.m_systemTransaction.TransactionInformation.LocalIdentifier);
				}
				else if (opoConCtx.m_txnType == TxnType.COMPlus)
				{
					lock (this.m_mtsConnections)
					{
						pooledConCtx = (PooledConCtx)this.m_mtsConnections.GetResource();
					}
					if (pooledConCtx == null)
					{
						pooledConCtx = (PooledConCtx)this.m_oraResPool.GetResource(opoConCtx.m_systemTransaction.TransactionInformation.LocalIdentifier);
						if (pooledConCtx != null)
						{
							opoConCtx.m_txnType = TxnType.SystemTxn;
						}
					}
				}
				if (pooledConCtx != null && (OraTrace.m_PerformanceCounters & PerfCounterLevel.NumberOfStasisConnections) == PerfCounterLevel.NumberOfStasisConnections)
				{
					OraclePerfCounterCollection.NumberOfStasisConnections.Decrement();
				}
			}
			catch
			{
			}
		}

		// Token: 0x06000C5D RID: 3165 RVA: 0x0007BCFC File Offset: 0x0007ACFC
		private void GetRegularConnection(ref PooledConCtx pooledConCtx)
		{
			try
			{
				if (this.m_bSynchronizeStack)
				{
					lock (this.m_connections.SyncRoot)
					{
						pooledConCtx = (PooledConCtx)this.m_connections.Pop();
						goto IL_4E;
					}
				}
				pooledConCtx = (PooledConCtx)this.m_connections.Pop();
				IL_4E:
				if (pooledConCtx != null && (OraTrace.m_PerformanceCounters & PerfCounterLevel.NumberOfFreeConnections) == PerfCounterLevel.NumberOfFreeConnections)
				{
					OraclePerfCounterCollection.NumberOfFreeConnections.Decrement();
				}
			}
			catch
			{
			}
		}

		// Token: 0x06000C5E RID: 3166 RVA: 0x0007BD9C File Offset: 0x0007AD9C
		public unsafe int CopyPooledToCon(ref OpoConCtx opoConCtx, ref PooledConCtx pooledConCtx)
		{
			int num = 0;
			int num2 = 0;
			int sessionBegin = 1;
			opoConCtx.pooledConCtx = pooledConCtx;
			if (opoConCtx.opsConCtx != IntPtr.Zero && opoConCtx.opsErrCtx != IntPtr.Zero)
			{
				OpoConValCtx* ptr = null;
				try
				{
					if (opoConCtx.m_fetchArrayPooler != null)
					{
						opoConCtx.m_fetchArrayPooler.Dispose();
						opoConCtx.m_fetchArrayPooler = null;
					}
					OpsCon.Dispose(ref opoConCtx.opsConCtx, ref opoConCtx.opsErrCtx, ref ptr, null);
				}
				catch (Exception ex)
				{
					if (OraTrace.m_TraceLevel != 0U)
					{
						OraTrace.TraceExceptionInfo(ex);
					}
				}
				if ((OraTrace.m_PerformanceCounters & PerfCounterLevel.HardDisconnectsPerSecond) == PerfCounterLevel.HardDisconnectsPerSecond)
				{
					OraclePerfCounterCollection.HardDisconnectsPerSecond.Increment();
				}
			}
			opoConCtx.opsConCtx = pooledConCtx.opsConCtx;
			opoConCtx.opsErrCtx = pooledConCtx.opsErrCtx;
			opoConCtx.creationTime = pooledConCtx.creationTime;
			num = ConnectionDispenser.CopyPooledConCtx(ref opoConCtx.pOpoConValCtx, pooledConCtx.pOpoConValCtx);
			try
			{
				if (num == 0 && opoConCtx.pOpoConValCtx->SessionBegin == 1)
				{
					if (opoConCtx.opoConRefCtx.proxyUserId == null || opoConCtx.opoConRefCtx.proxyUserId.Length <= 0)
					{
						if (opoConCtx.pOpoConValCtx->OSAuthent != 2)
						{
							goto IL_163;
						}
					}
					try
					{
						num2 = OpsCon.OpenProxyAuthUserSession(opoConCtx.opsConCtx, opoConCtx.opsErrCtx, opoConCtx.pOpoConValCtx, opoConCtx.opoConRefCtx);
					}
					catch (Exception ex2)
					{
						if (OraTrace.m_TraceLevel != 0U)
						{
							OraTrace.TraceExceptionInfo(ex2);
						}
						num = ErrRes.INT_ERR;
						opoConCtx.exceptMsg = ex2.ToString();
					}
				}
				IL_163:;
			}
			finally
			{
				if (num2 != 0)
				{
					sessionBegin = opoConCtx.pOpoConValCtx->SessionBegin;
					opoConCtx.pOpoConValCtx->SessionBegin = 1;
					num = num2;
				}
				if (num != 0)
				{
					if (opoConCtx.pOpoConValCtx->Enlist == 1 && opoConCtx.m_systemTransaction != null && (opoConCtx.opoConRefCtx.proxyUserId == null || opoConCtx.opoConRefCtx.proxyUserId.Length == 0))
					{
						this.PushToResourcePool(opoConCtx, pooledConCtx);
						if ((OraTrace.m_PerformanceCounters & PerfCounterLevel.NumberOfStasisConnections) == PerfCounterLevel.NumberOfStasisConnections)
						{
							OraclePerfCounterCollection.NumberOfStasisConnections.Increment();
						}
					}
					else
					{
						if (this.m_bSynchronizeStack)
						{
							lock (this.m_connections)
							{
								this.PutConnection(ref opoConCtx, false, true, true, 0);
								goto IL_236;
							}
						}
						this.PutConnection(ref opoConCtx, false, true, true, 0);
					}
					IL_236:
					opoConCtx.pooledConCtx = null;
					opoConCtx.opsConCtx = IntPtr.Zero;
					opoConCtx.opsErrCtx = IntPtr.Zero;
					opoConCtx.m_conPooler = null;
					opoConCtx.m_udtDescPoolerByName = null;
					opoConCtx.m_udtDescPoolerByTDO = null;
				}
			}
			opoConCtx.opoConRefCtx.serverVersion = pooledConCtx.opoConRefCtx.serverVersion;
			opoConCtx.opoConRefCtx.dataSource = pooledConCtx.opoConRefCtx.dataSource;
			opoConCtx.opoConRefCtx.dbName = pooledConCtx.opoConRefCtx.dbName;
			opoConCtx.opoConRefCtx.hostName = pooledConCtx.opoConRefCtx.hostName;
			opoConCtx.opoConRefCtx.serviceName = pooledConCtx.opoConRefCtx.serviceName;
			opoConCtx.opoConRefCtx.instanceName = pooledConCtx.opoConRefCtx.instanceName;
			opoConCtx.opoConRefCtx.dbDomainName = pooledConCtx.opoConRefCtx.dbDomainName;
			opoConCtx.opoConRefCtx.ttOpsConOpenErrMssg = pooledConCtx.opoConRefCtx.ttOpsConOpenErrMssg;
			opoConCtx.m_conPooler = pooledConCtx.m_conPooler;
			opoConCtx.m_udtDescPoolerByName = pooledConCtx.m_udtDescPoolerByName;
			opoConCtx.m_udtDescPoolerByTDO = pooledConCtx.m_udtDescPoolerByTDO;
			opoConCtx.m_promotableTxnManager = pooledConCtx.m_promotableTxnManager;
			opoConCtx.m_fetchArrayPooler = pooledConCtx.m_fetchArrayPooler;
			opoConCtx.m_statementData = pooledConCtx.m_statementData;
			opoConCtx.m_totalDataAvailable = pooledConCtx.m_totalDataAvailable;
			if (num2 != 0)
			{
				opoConCtx.pOpoConValCtx->SessionBegin = sessionBegin;
			}
			return num;
		}

		// Token: 0x06000C5F RID: 3167 RVA: 0x0007C1B0 File Offset: 0x0007B1B0
		private void CreateMoreConnections()
		{
			int potentialTotal;
			if (this.m_cpCtx != null)
			{
				potentialTotal = this.m_cpCtx.m_counter.potentialTotal;
			}
			else
			{
				potentialTotal = this.m_counter.potentialTotal;
			}
			int num;
			if (potentialTotal + this.m_clonedCtx.poolIncSize > this.m_clonedCtx.maxPoolSize)
			{
				num = this.m_clonedCtx.maxPoolSize - potentialTotal;
			}
			else
			{
				num = this.m_clonedCtx.poolIncSize;
			}
			if (num > 0)
			{
				ThreadPool.QueueUserWorkItem(new WaitCallback(this.PopulatePool), num);
				this.UpdatePotentialTotalCount(num);
			}
		}

		// Token: 0x06000C60 RID: 3168 RVA: 0x0007C240 File Offset: 0x0007B240
		private int WaitForRegularConnection(ref OpoConCtx opoConCtx, ref PooledConCtx pooledConCtx)
		{
			int num = 0;
			int num2 = 0;
			this.UpdateThreadWaitCount(1);
			double num3;
			if (this.m_bGridRac)
			{
				num3 = 0.0;
			}
			else
			{
				num3 = opoConCtx.timeOut.TotalSeconds;
			}
			try
			{
				num = OpsCon.WaitForSingleObject(this.m_semAvaNumOfCons, (int)num3 * 1000);
			}
			catch (Exception ex)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.TraceExceptionInfo(ex);
				}
				num = ErrRes.INT_ERR;
				opoConCtx.exceptMsg = ex.ToString();
			}
			finally
			{
				this.UpdateThreadWaitCount(-1);
			}
			if (num == ErrRes.INT_ERR)
			{
				return num;
			}
			int num4 = num;
			if (num4 != 0)
			{
				if (num4 == 128 || num4 == 258)
				{
					if (this.m_bGridRac)
					{
						Interlocked.Increment(ref this.m_cpCtx.totalAvaliableConnections);
					}
					else
					{
						Interlocked.Increment(ref this.m_counter.totalAvailable);
					}
					return ErrRes.CON_TIMEOUT_EXCEEDED;
				}
			}
			else
			{
				this.GetRegularConnection(ref pooledConCtx);
				if (pooledConCtx == null)
				{
					num = ErrRes.CON_TIMEOUT_EXCEEDED;
					if (this.m_bGridRac)
					{
						Interlocked.Increment(ref this.m_cpCtx.totalAvaliableConnections);
					}
					else
					{
						Interlocked.Increment(ref this.m_counter.totalAvailable);
					}
					try
					{
						OpsCon.ReleaseSemaphore(this.m_semAvaNumOfCons, 1, ref num2);
					}
					catch (Exception ex2)
					{
						if (OraTrace.m_TraceLevel != 0U)
						{
							OraTrace.TraceExceptionInfo(ex2);
						}
					}
					return num;
				}
			}
			return 0;
		}

		// Token: 0x06000C61 RID: 3169 RVA: 0x0007C3A0 File Offset: 0x0007B3A0
		public unsafe int GetConnection(OpoConCtx opoConCtx)
		{
			int num = 0;
			PooledConCtx pooledConCtx = null;
			bool flag = false;
			bool flag2 = false;
			DateTime now = DateTime.Now;
			try
			{
				if (opoConCtx.pOpoConValCtx->Enlist == 1 && opoConCtx.m_systemTransaction != null && (opoConCtx.opoConRefCtx.proxyUserId == null || opoConCtx.opoConRefCtx.proxyUserId.Length == 0))
				{
					flag2 = true;
				}
				for (;;)
				{
					flag = false;
					pooledConCtx = null;
					int inMtsTxn = 0;
					if (flag2)
					{
						this.GetEnlistedConnection(ref pooledConCtx, opoConCtx);
						if (pooledConCtx != null)
						{
							inMtsTxn = 1;
							if (opoConCtx.bGridRac && this.m_cpCtx != null)
							{
								Interlocked.Increment(ref this.m_cpCtx.totalAvaliableConnections);
								Interlocked.Increment(ref this.m_counter.totalAvailable);
								int num2 = 0;
								OpsCon.ReleaseSemaphore(this.m_cpCtx.m_semCPCtxAvaNumOfCons, 1, ref num2);
							}
						}
					}
					if (pooledConCtx == null)
					{
						num = this.WaitForRegularConnection(ref opoConCtx, ref pooledConCtx);
						if (num != 0)
						{
							break;
						}
					}
					if (pooledConCtx != null)
					{
						num = this.CopyPooledToCon(ref opoConCtx, ref pooledConCtx);
						if (num != 0)
						{
							goto Block_16;
						}
					}
					if (opoConCtx.pOpoConValCtx->SessionBegin == 1 && opoConCtx.validateCon == 1)
					{
						this.CheckLifeTimeAndStatus(ref opoConCtx, 1, ref flag, 1, false);
					}
					if (!flag)
					{
						goto Block_19;
					}
					if (DateTime.Now - now >= opoConCtx.timeOut)
					{
						goto Block_21;
					}
				}
				return num;
				Block_16:
				return num;
				Block_19:
				if (flag2)
				{
					int inMtsTxn;
					opoConCtx.pOpoConValCtx->InMtsTxn = inMtsTxn;
				}
				if (opoConCtx.pOpoConValCtx->Enlist == 1 && opoConCtx.pOpoConValCtx->InMtsTxn == 0 && opoConCtx.pOpoConValCtx->SessionBegin == 1)
				{
					if (opoConCtx.opoConRefCtx.proxyUserId != null)
					{
						if (opoConCtx.opoConRefCtx.proxyUserId.Length != 0)
						{
							goto IL_232;
						}
					}
					try
					{
						num = OpsCon.Enlist(opoConCtx.opsConCtx, opoConCtx.pOpoConValCtx, opoConCtx.opoConRefCtx);
					}
					catch (Exception ex)
					{
						if (OraTrace.m_TraceLevel != 0U)
						{
							OraTrace.TraceExceptionInfo(ex);
						}
						num = ErrRes.INT_ERR;
						opoConCtx.exceptMsg = ex.ToString();
					}
					finally
					{
						if (num != 0)
						{
							opoConCtx.pOpoConValCtx->InMtsTxn = 0;
							if (this.m_bSynchronizeStack)
							{
								lock (this.m_connections)
								{
									this.PutConnection(ref opoConCtx, false, true, true, 0);
									goto IL_229;
								}
							}
							this.PutConnection(ref opoConCtx, false, true, true, 0);
						}
						IL_229:;
					}
					if (num != 0)
					{
						return num;
					}
				}
				IL_232:
				goto IL_259;
				Block_21:
				return ErrRes.CON_TIMEOUT_EXCEEDED;
			}
			catch (Exception ex2)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.TraceExceptionInfo(ex2);
				}
				num = ErrRes.INT_ERR;
				opoConCtx.exceptMsg = ex2.ToString();
			}
			IL_259:
			if (this.m_inactive)
			{
				this.m_inactive = false;
				if ((OraTrace.m_PerformanceCounters & PerfCounterLevel.NumberOfActiveConnectionPools) == PerfCounterLevel.NumberOfActiveConnectionPools)
				{
					OraclePerfCounterCollection.NumberOfActiveConnectionPools.Increment();
				}
				if ((OraTrace.m_PerformanceCounters & PerfCounterLevel.NumberOfInactiveConnectionPools) == PerfCounterLevel.NumberOfInactiveConnectionPools)
				{
					OraclePerfCounterCollection.NumberOfInactiveConnectionPools.Decrement();
				}
			}
			return num;
		}

		// Token: 0x06000C62 RID: 3170 RVA: 0x0007C6AC File Offset: 0x0007B6AC
		public int ReleaseSemaphore()
		{
			int num = 0;
			int num2 = 0;
			try
			{
				OpsCon.ReleaseSemaphore(this.m_semAvaNumOfCons, 1, ref num);
				if (this.m_cpCtx != null)
				{
					OpsCon.ReleaseSemaphore(this.m_cpCtx.m_semCPCtxAvaNumOfCons, 1, ref num2);
				}
			}
			catch (Exception ex)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.TraceExceptionInfo(ex);
				}
			}
			return 0;
		}

		// Token: 0x06000C63 RID: 3171 RVA: 0x0007C70C File Offset: 0x0007B70C
		public unsafe int PutConnection(ref OpoConCtx opoConCtx, bool bDoNotAllocValCtx, bool bCheckStatus, bool bCheckLifeTime, int bDistTxnEnd)
		{
			bool flag = false;
			int num = 0;
			bool flag2 = false;
			ConnectionPool connectionPool = this;
			if (opoConCtx.pOpoConValCtx->StmtCacheSize > 0 && opoConCtx.pOpoConValCtx->StmtCachePurge == 1)
			{
				try
				{
					num = OpsCon.PurgeStatementCache(opoConCtx.opsConCtx, opoConCtx.opsErrCtx, opoConCtx.pOpoConValCtx);
				}
				catch (Exception ex)
				{
					if (OraTrace.m_TraceLevel != 0U)
					{
						OraTrace.TraceExceptionInfo(ex);
					}
				}
			}
			if (((opoConCtx.pOpoConValCtx->InMtsTxn == 1 && opoConCtx.m_systemTransaction != null) || opoConCtx.m_promotableTxnManager != null) && (opoConCtx.opoConRefCtx.proxyUserId == null || opoConCtx.opoConRefCtx.proxyUserId.Length == 0))
			{
				if (opoConCtx.pooledConCtx != null)
				{
					opoConCtx.pooledConCtx.m_fetchArrayPooler = opoConCtx.m_fetchArrayPooler;
					opoConCtx.pooledConCtx.m_promotableTxnManager = opoConCtx.m_promotableTxnManager;
					opoConCtx.pooledConCtx.m_statementData = opoConCtx.m_statementData;
					opoConCtx.pooledConCtx.m_totalDataAvailable = opoConCtx.m_totalDataAvailable;
					this.PushToResourcePool(opoConCtx, opoConCtx.pooledConCtx);
					if ((OraTrace.m_PerformanceCounters & PerfCounterLevel.NumberOfStasisConnections) == PerfCounterLevel.NumberOfStasisConnections)
					{
						OraclePerfCounterCollection.NumberOfStasisConnections.Increment();
					}
					opoConCtx.pooledConCtx = null;
					opoConCtx.opsConCtx = IntPtr.Zero;
					opoConCtx.opsErrCtx = IntPtr.Zero;
					opoConCtx.m_conPooler = null;
					opoConCtx.m_udtDescPoolerByName = null;
					opoConCtx.m_udtDescPoolerByTDO = null;
				}
				else
				{
					PooledConCtx pooledConCtx = new PooledConCtx();
					pooledConCtx.opsConCtx = opoConCtx.opsConCtx;
					pooledConCtx.opsErrCtx = opoConCtx.opsErrCtx;
					pooledConCtx.conString = opoConCtx.conString;
					pooledConCtx.creationTime = opoConCtx.creationTime;
					if (bDoNotAllocValCtx)
					{
						pooledConCtx.pOpoConValCtx = opoConCtx.pOpoConValCtx;
						opoConCtx.pOpoConValCtx = null;
					}
					else
					{
						num = ConnectionDispenser.CopyPooledConCtx(ref pooledConCtx.pOpoConValCtx, opoConCtx.pOpoConValCtx);
						if (num != 0)
						{
							try
							{
								if (opoConCtx.m_fetchArrayPooler != null)
								{
									opoConCtx.m_fetchArrayPooler.Dispose();
									opoConCtx.m_fetchArrayPooler = null;
								}
								OpsCon.Dispose(ref opoConCtx.opsConCtx, ref opoConCtx.opsErrCtx, ref opoConCtx.pOpoConValCtx, opoConCtx.opoConRefCtx);
							}
							catch (Exception ex2)
							{
								if (OraTrace.m_TraceLevel != 0U)
								{
									OraTrace.TraceExceptionInfo(ex2);
								}
							}
							if ((OraTrace.m_PerformanceCounters & PerfCounterLevel.HardDisconnectsPerSecond) == PerfCounterLevel.HardDisconnectsPerSecond)
							{
								OraclePerfCounterCollection.HardDisconnectsPerSecond.Increment();
							}
							opoConCtx.pooledConCtx = null;
							opoConCtx.opsConCtx = IntPtr.Zero;
							opoConCtx.opsErrCtx = IntPtr.Zero;
							opoConCtx.m_conPooler = null;
							opoConCtx.m_udtDescPoolerByName = null;
							opoConCtx.m_udtDescPoolerByTDO = null;
							opoConCtx.m_systemTransaction = null;
							opoConCtx.m_txnType = TxnType.None;
							opoConCtx.m_promotableTxnManager = null;
							this.UpdateTotalCount(-1, true);
							return -1;
						}
					}
					pooledConCtx.opoConRefCtx = new OpoConRefCtx();
					pooledConCtx.opoConRefCtx.serverVersion = opoConCtx.opoConRefCtx.serverVersion;
					pooledConCtx.opoConRefCtx.dataSource = opoConCtx.opoConRefCtx.dataSource;
					pooledConCtx.opoConRefCtx.dbName = opoConCtx.opoConRefCtx.dbName;
					pooledConCtx.opoConRefCtx.hostName = opoConCtx.opoConRefCtx.hostName;
					pooledConCtx.opoConRefCtx.serviceName = opoConCtx.opoConRefCtx.serviceName;
					pooledConCtx.opoConRefCtx.instanceName = opoConCtx.opoConRefCtx.instanceName;
					pooledConCtx.opoConRefCtx.dbDomainName = opoConCtx.opoConRefCtx.dbDomainName;
					pooledConCtx.opoConRefCtx.ttOpsConOpenErrMssg = opoConCtx.opoConRefCtx.ttOpsConOpenErrMssg;
					pooledConCtx.m_conPooler = opoConCtx.m_conPooler;
					pooledConCtx.m_udtDescPoolerByName = opoConCtx.m_udtDescPoolerByName;
					pooledConCtx.m_udtDescPoolerByTDO = opoConCtx.m_udtDescPoolerByTDO;
					pooledConCtx.m_txnid = opoConCtx.m_txnid;
					pooledConCtx.m_promotableTxnManager = opoConCtx.m_promotableTxnManager;
					pooledConCtx.m_fetchArrayPooler = opoConCtx.m_fetchArrayPooler;
					pooledConCtx.m_statementData = opoConCtx.m_statementData;
					pooledConCtx.m_totalDataAvailable = opoConCtx.m_totalDataAvailable;
					this.PushToResourcePool(opoConCtx, pooledConCtx);
					if ((OraTrace.m_PerformanceCounters & PerfCounterLevel.NumberOfStasisConnections) == PerfCounterLevel.NumberOfStasisConnections)
					{
						OraclePerfCounterCollection.NumberOfStasisConnections.Increment();
					}
					opoConCtx.pooledConCtx = null;
					opoConCtx.opsConCtx = IntPtr.Zero;
					opoConCtx.opsErrCtx = IntPtr.Zero;
					opoConCtx.m_conPooler = null;
					opoConCtx.m_udtDescPoolerByName = null;
					opoConCtx.m_udtDescPoolerByTDO = null;
				}
				opoConCtx.pOpoConValCtx->InMtsTxn = 0;
				return num;
			}
			if (opoConCtx.pOpoConValCtx->InMtsTxn == 1 && !ContextUtil.IsInTransaction)
			{
				opoConCtx.opoConRefCtx.pITransaction = null;
				try
				{
					num = OpsCon.Enlist(opoConCtx.opsConCtx, opoConCtx.pOpoConValCtx, opoConCtx.opoConRefCtx);
				}
				catch (Exception ex3)
				{
					if (OraTrace.m_TraceLevel != 0U)
					{
						OraTrace.TraceExceptionInfo(ex3);
					}
					num = ErrRes.INT_ERR;
				}
				finally
				{
					if (num != 0)
					{
						try
						{
							if (opoConCtx.m_fetchArrayPooler != null)
							{
								opoConCtx.m_fetchArrayPooler.Dispose();
								opoConCtx.m_fetchArrayPooler = null;
							}
							OpsCon.Dispose(ref opoConCtx.opsConCtx, ref opoConCtx.opsErrCtx, ref opoConCtx.pOpoConValCtx, opoConCtx.opoConRefCtx);
						}
						catch (Exception ex4)
						{
							if (OraTrace.m_TraceLevel != 0U)
							{
								OraTrace.TraceExceptionInfo(ex4);
							}
						}
						if ((OraTrace.m_PerformanceCounters & PerfCounterLevel.HardDisconnectsPerSecond) == PerfCounterLevel.HardDisconnectsPerSecond)
						{
							OraclePerfCounterCollection.HardDisconnectsPerSecond.Increment();
						}
						opoConCtx.pooledConCtx = null;
						opoConCtx.opsConCtx = IntPtr.Zero;
						opoConCtx.opsErrCtx = IntPtr.Zero;
						opoConCtx.m_conPooler = null;
						opoConCtx.m_udtDescPoolerByName = null;
						opoConCtx.m_udtDescPoolerByTDO = null;
						opoConCtx.m_systemTransaction = null;
						opoConCtx.m_txnType = TxnType.None;
						this.UpdateTotalCount(-1, true);
					}
				}
				if (num != 0)
				{
					return -1;
				}
			}
			if (bCheckStatus || bCheckLifeTime)
			{
				bool bCheckLifetimeOnly = bCheckLifeTime && !bCheckStatus;
				this.CheckLifeTimeAndStatus(ref opoConCtx, bDistTxnEnd, ref flag, 0, bCheckLifetimeOnly);
				if (flag)
				{
					return -1;
				}
			}
			if (this.m_bClearPoolInProgress && opoConCtx.creationTime < this.m_clearRequestTimeStamp)
			{
				try
				{
					if (opoConCtx.m_fetchArrayPooler != null)
					{
						opoConCtx.m_fetchArrayPooler.Dispose();
						opoConCtx.m_fetchArrayPooler = null;
					}
					OpsCon.Dispose(ref opoConCtx.opsConCtx, ref opoConCtx.opsErrCtx, ref opoConCtx.pOpoConValCtx, opoConCtx.opoConRefCtx);
				}
				catch (Exception ex5)
				{
					if (OraTrace.m_TraceLevel != 0U)
					{
						OraTrace.TraceExceptionInfo(ex5);
					}
				}
				opoConCtx.pooledConCtx = null;
				opoConCtx.opsConCtx = IntPtr.Zero;
				opoConCtx.opsErrCtx = IntPtr.Zero;
				opoConCtx.m_conPooler = null;
				opoConCtx.m_udtDescPoolerByName = null;
				opoConCtx.m_udtDescPoolerByTDO = null;
				opoConCtx.m_systemTransaction = null;
				opoConCtx.m_txnType = TxnType.None;
				this.UpdateTotalCount(-1, true);
				if (this.m_bGridRac)
				{
					Interlocked.Decrement(ref this.m_cpCtx.m_consFromAppToClear);
					if (this.m_cpCtx.m_consFromAppToClear == 0)
					{
						this.m_bClearPoolInProgress = false;
					}
				}
				else
				{
					Interlocked.Decrement(ref this.m_consFromAppToClear);
					if (this.m_consFromAppToClear == 0)
					{
						this.m_bClearPoolInProgress = false;
					}
				}
			}
			else
			{
				if (opoConCtx.pooledConCtx != null)
				{
					if (bDoNotAllocValCtx)
					{
						opoConCtx.pooledConCtx.pOpoConValCtx = opoConCtx.pOpoConValCtx;
						opoConCtx.pOpoConValCtx = null;
					}
					else
					{
						num = ConnectionDispenser.CopyPooledConCtx(ref opoConCtx.pooledConCtx.pOpoConValCtx, opoConCtx.pOpoConValCtx);
						if (num != 0)
						{
							try
							{
								if (opoConCtx.m_fetchArrayPooler != null)
								{
									opoConCtx.m_fetchArrayPooler.Dispose();
									opoConCtx.m_fetchArrayPooler = null;
								}
								OpsCon.Dispose(ref opoConCtx.opsConCtx, ref opoConCtx.opsErrCtx, ref opoConCtx.pOpoConValCtx, opoConCtx.opoConRefCtx);
							}
							catch (Exception ex6)
							{
								if (OraTrace.m_TraceLevel != 0U)
								{
									OraTrace.TraceExceptionInfo(ex6);
								}
							}
							if ((OraTrace.m_PerformanceCounters & PerfCounterLevel.HardDisconnectsPerSecond) == PerfCounterLevel.HardDisconnectsPerSecond)
							{
								OraclePerfCounterCollection.HardDisconnectsPerSecond.Increment();
							}
							opoConCtx.pooledConCtx = null;
							opoConCtx.opsConCtx = IntPtr.Zero;
							opoConCtx.opsErrCtx = IntPtr.Zero;
							opoConCtx.m_conPooler = null;
							opoConCtx.m_udtDescPoolerByName = null;
							opoConCtx.m_udtDescPoolerByTDO = null;
							opoConCtx.m_systemTransaction = null;
							opoConCtx.m_txnType = TxnType.None;
							this.UpdateTotalCount(-1, true);
							return -1;
						}
					}
					if (opoConCtx.pooledConCtx.pOpoConValCtx->bTAFEnabled == 1)
					{
						string instanceName = opoConCtx.opoConRefCtx.instanceName;
						OpsCon.GetAttributes(opoConCtx.opsConCtx, opoConCtx.opsErrCtx, opoConCtx.opoConRefCtx);
						if (instanceName != opoConCtx.opoConRefCtx.instanceName)
						{
							OraTrace.Trace(16U, new string[]
							{
								string.Concat(new string[]
								{
									" (FO)    Failed over from ",
									instanceName,
									" to ",
									opoConCtx.opoConRefCtx.instanceName,
									"\n"
								})
							});
							flag2 = true;
							connectionPool = (ConnectionPool)this.m_cpCtx.htInstToCp[opoConCtx.opoConRefCtx.instanceName];
							if (connectionPool == null)
							{
								lock (this.m_cpCtx.htInstToCp.SyncRoot)
								{
									if (this.m_cpCtx.htInstToCp[opoConCtx.opoConRefCtx.instanceName] == null)
									{
										connectionPool = new ConnectionPool(opoConCtx, this.m_cpCtx, this.m_identity);
										this.m_cpCtx.htInstToCp[opoConCtx.opoConRefCtx.instanceName] = connectionPool;
										if (OraTrace.m_TraceLevel != 0U)
										{
											OraTrace.Trace(2U, new string[]
											{
												string.Concat(new object[]
												{
													" (POOL)  New CP created (CP id: ",
													connectionPool.GetHashCode(),
													"; CPCtx id: ",
													connectionPool.m_cpCtx.GetHashCode(),
													") for: [Instance=",
													opoConCtx.opoConRefCtx.instanceName,
													"]\n"
												})
											});
											OraTrace.Trace(2U, new string[]
											{
												string.Concat(new object[]
												{
													" (POOL)  Num of Inst CPs in (CPCtx id: ",
													connectionPool.m_cpCtx.GetHashCode(),
													") : ",
													connectionPool.m_cpCtx.htInstToCp.Count,
													"\n"
												})
											});
										}
									}
								}
							}
						}
					}
					opoConCtx.pooledConCtx.m_promotableTxnManager = opoConCtx.m_promotableTxnManager;
					opoConCtx.pooledConCtx.m_fetchArrayPooler = opoConCtx.m_fetchArrayPooler;
					opoConCtx.pooledConCtx.m_statementData = opoConCtx.m_statementData;
					opoConCtx.pooledConCtx.m_totalDataAvailable = opoConCtx.m_totalDataAvailable;
					if (this.m_bSynchronizeStack)
					{
						lock (connectionPool.m_connections.SyncRoot)
						{
							connectionPool.m_connections.Push(opoConCtx.pooledConCtx);
							goto IL_A9E;
						}
					}
					connectionPool.m_connections.Push(opoConCtx.pooledConCtx);
					IL_A9E:
					if ((OraTrace.m_PerformanceCounters & PerfCounterLevel.NumberOfFreeConnections) == PerfCounterLevel.NumberOfFreeConnections)
					{
						OraclePerfCounterCollection.NumberOfFreeConnections.Increment();
					}
					if (this.m_bGridRac && connectionPool.m_cpCtx != null)
					{
						Interlocked.Increment(ref connectionPool.m_cpCtx.totalAvaliableConnections);
					}
					Interlocked.Increment(ref connectionPool.m_counter.totalAvailable);
					connectionPool.ReleaseSemaphore();
					opoConCtx.pooledConCtx = null;
					opoConCtx.opsConCtx = IntPtr.Zero;
					opoConCtx.opsErrCtx = IntPtr.Zero;
					opoConCtx.m_conPooler = null;
					opoConCtx.m_udtDescPoolerByName = null;
					opoConCtx.m_udtDescPoolerByTDO = null;
					if (!flag2)
					{
						goto IL_11FB;
					}
					Interlocked.Decrement(ref this.m_counter.total);
					Interlocked.Increment(ref connectionPool.m_counter.total);
					try
					{
						lock (this.m_passwordSyncObj)
						{
							if (this.m_encryptedPwd != null)
							{
								opoConCtx.opoConRefCtx.password = this.m_encryptedPwd.Password;
							}
							else
							{
								opoConCtx.opoConRefCtx.password = "";
							}
							if (this.m_encryptedPxyPwd != null)
							{
								opoConCtx.opoConRefCtx.proxyPassword = this.m_encryptedPxyPwd.Password;
							}
							else
							{
								opoConCtx.opoConRefCtx.proxyPassword = "";
							}
						}
						if ((num = ConnectionDispenser.RegisterCallbacks(opoConCtx)) != 0)
						{
							OpoConValCtx* ptr = null;
							try
							{
								OpsCon.Close(ref opoConCtx.opsConCtx, ref opoConCtx.opsErrCtx, opoConCtx.pOpoConValCtx, opoConCtx.opoConRefCtx);
							}
							catch (Exception ex7)
							{
								if (OraTrace.m_TraceLevel != 0U)
								{
									OraTrace.TraceExceptionInfo(ex7);
								}
							}
							if ((OraTrace.m_PerformanceCounters & PerfCounterLevel.HardDisconnectsPerSecond) == PerfCounterLevel.HardDisconnectsPerSecond)
							{
								OraclePerfCounterCollection.HardDisconnectsPerSecond.Increment();
							}
							try
							{
								if (opoConCtx.m_fetchArrayPooler != null)
								{
									opoConCtx.m_fetchArrayPooler.Dispose();
									opoConCtx.m_fetchArrayPooler = null;
								}
								OpsCon.Dispose(ref opoConCtx.opsConCtx, ref opoConCtx.opsErrCtx, ref ptr, opoConCtx.opoConRefCtx);
							}
							catch (Exception ex8)
							{
								if (OraTrace.m_TraceLevel != 0U)
								{
									OraTrace.TraceExceptionInfo(ex8);
								}
							}
							return num;
						}
						goto IL_11FB;
					}
					finally
					{
						opoConCtx.opoConRefCtx.password = null;
						opoConCtx.opoConRefCtx.proxyPassword = null;
					}
				}
				PooledConCtx pooledConCtx2 = new PooledConCtx();
				pooledConCtx2.opsConCtx = opoConCtx.opsConCtx;
				pooledConCtx2.opsErrCtx = opoConCtx.opsErrCtx;
				pooledConCtx2.conString = opoConCtx.conString;
				pooledConCtx2.creationTime = opoConCtx.creationTime;
				if (bDoNotAllocValCtx)
				{
					pooledConCtx2.pOpoConValCtx = opoConCtx.pOpoConValCtx;
					opoConCtx.pOpoConValCtx = null;
				}
				else
				{
					num = ConnectionDispenser.CopyPooledConCtx(ref pooledConCtx2.pOpoConValCtx, opoConCtx.pOpoConValCtx);
					if (num != 0)
					{
						try
						{
							if (opoConCtx.m_fetchArrayPooler != null)
							{
								opoConCtx.m_fetchArrayPooler.Dispose();
								opoConCtx.m_fetchArrayPooler = null;
							}
							OpsCon.Dispose(ref opoConCtx.opsConCtx, ref opoConCtx.opsErrCtx, ref opoConCtx.pOpoConValCtx, opoConCtx.opoConRefCtx);
						}
						catch (Exception ex9)
						{
							if (OraTrace.m_TraceLevel != 0U)
							{
								OraTrace.TraceExceptionInfo(ex9);
							}
						}
						if ((OraTrace.m_PerformanceCounters & PerfCounterLevel.HardDisconnectsPerSecond) == PerfCounterLevel.HardDisconnectsPerSecond)
						{
							OraclePerfCounterCollection.HardDisconnectsPerSecond.Increment();
						}
						opoConCtx.pooledConCtx = null;
						opoConCtx.opsConCtx = IntPtr.Zero;
						opoConCtx.opsErrCtx = IntPtr.Zero;
						opoConCtx.m_conPooler = null;
						opoConCtx.m_udtDescPoolerByName = null;
						opoConCtx.m_udtDescPoolerByTDO = null;
						opoConCtx.m_systemTransaction = null;
						opoConCtx.m_txnType = TxnType.None;
						this.UpdateTotalCount(-1, true);
						return -1;
					}
				}
				if (pooledConCtx2.pOpoConValCtx->bTAFEnabled == 1)
				{
					string instanceName2 = opoConCtx.opoConRefCtx.instanceName;
					OpsCon.GetAttributes(opoConCtx.opsConCtx, opoConCtx.opsErrCtx, opoConCtx.opoConRefCtx);
					if (instanceName2 != opoConCtx.opoConRefCtx.instanceName)
					{
						OraTrace.Trace(16U, new string[]
						{
							string.Concat(new string[]
							{
								" (FO)    Failed over from ",
								instanceName2,
								" to ",
								opoConCtx.opoConRefCtx.instanceName,
								"\n"
							})
						});
						flag2 = true;
						connectionPool = (ConnectionPool)this.m_cpCtx.htInstToCp[opoConCtx.opoConRefCtx.instanceName];
						if (connectionPool == null)
						{
							lock (this.m_cpCtx.htInstToCp.SyncRoot)
							{
								if (this.m_cpCtx.htInstToCp[opoConCtx.opoConRefCtx.instanceName] == null)
								{
									connectionPool = new ConnectionPool(opoConCtx, this.m_cpCtx, this.m_identity);
									this.m_cpCtx.htInstToCp[opoConCtx.opoConRefCtx.instanceName] = connectionPool;
									if (OraTrace.m_TraceLevel != 0U)
									{
										OraTrace.Trace(2U, new string[]
										{
											string.Concat(new object[]
											{
												" (POOL)  New CP created (CP id: ",
												connectionPool.GetHashCode(),
												"; CPCtx id: ",
												connectionPool.m_cpCtx.GetHashCode(),
												") for: [Instance=",
												opoConCtx.opoConRefCtx.instanceName,
												"]\n"
											})
										});
										OraTrace.Trace(2U, new string[]
										{
											string.Concat(new object[]
											{
												" (POOL)  Num of Inst CPs in (CPCtx id: ",
												connectionPool.m_cpCtx.GetHashCode(),
												") : ",
												connectionPool.m_cpCtx.htInstToCp.Count,
												"\n"
											})
										});
									}
								}
							}
						}
					}
				}
				pooledConCtx2.opoConRefCtx = new OpoConRefCtx();
				pooledConCtx2.opoConRefCtx.serverVersion = opoConCtx.opoConRefCtx.serverVersion;
				pooledConCtx2.opoConRefCtx.dataSource = opoConCtx.opoConRefCtx.dataSource;
				pooledConCtx2.opoConRefCtx.dbName = opoConCtx.opoConRefCtx.dbName;
				pooledConCtx2.opoConRefCtx.hostName = opoConCtx.opoConRefCtx.hostName;
				pooledConCtx2.opoConRefCtx.serviceName = opoConCtx.opoConRefCtx.serviceName;
				pooledConCtx2.opoConRefCtx.instanceName = opoConCtx.opoConRefCtx.instanceName;
				pooledConCtx2.opoConRefCtx.dbDomainName = opoConCtx.opoConRefCtx.dbDomainName;
				pooledConCtx2.opoConRefCtx.ttOpsConOpenErrMssg = opoConCtx.opoConRefCtx.ttOpsConOpenErrMssg;
				pooledConCtx2.m_conPooler = opoConCtx.m_conPooler;
				pooledConCtx2.m_udtDescPoolerByName = opoConCtx.m_udtDescPoolerByName;
				pooledConCtx2.m_udtDescPoolerByTDO = opoConCtx.m_udtDescPoolerByTDO;
				pooledConCtx2.m_txnid = opoConCtx.m_txnid;
				pooledConCtx2.m_promotableTxnManager = opoConCtx.m_promotableTxnManager;
				pooledConCtx2.m_fetchArrayPooler = opoConCtx.m_fetchArrayPooler;
				pooledConCtx2.m_statementData = opoConCtx.m_statementData;
				pooledConCtx2.m_totalDataAvailable = opoConCtx.m_totalDataAvailable;
				if (this.m_bSynchronizeStack)
				{
					lock (connectionPool.m_connections.SyncRoot)
					{
						connectionPool.m_connections.Push(pooledConCtx2);
						goto IL_11A0;
					}
				}
				connectionPool.m_connections.Push(pooledConCtx2);
				IL_11A0:
				if ((OraTrace.m_PerformanceCounters & PerfCounterLevel.NumberOfFreeConnections) == PerfCounterLevel.NumberOfFreeConnections)
				{
					OraclePerfCounterCollection.NumberOfFreeConnections.Increment();
				}
				bool bGridRac = opoConCtx.bGridRac;
				if (bGridRac && connectionPool.m_cpCtx != null)
				{
					Interlocked.Increment(ref connectionPool.m_cpCtx.totalAvaliableConnections);
				}
				Interlocked.Increment(ref connectionPool.m_counter.totalAvailable);
				connectionPool.ReleaseSemaphore();
			}
			IL_11FB:
			opoConCtx.opsConCtx = IntPtr.Zero;
			opoConCtx.opsErrCtx = IntPtr.Zero;
			opoConCtx.m_conPooler = null;
			opoConCtx.m_udtDescPoolerByName = null;
			opoConCtx.m_udtDescPoolerByTDO = null;
			opoConCtx.m_systemTransaction = null;
			opoConCtx.m_txnType = TxnType.None;
			if (flag2)
			{
				Interlocked.Decrement(ref this.m_counter.total);
				Interlocked.Increment(ref connectionPool.m_counter.total);
			}
			return num;
		}

		// Token: 0x06000C64 RID: 3172 RVA: 0x0007DB04 File Offset: 0x0007CB04
		public void TransactionEnd(object obj)
		{
			int num = 0;
			PooledConCtx pooledConCtx = (PooledConCtx)obj;
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(4U, new string[]
				{
					" (MTS) ConnectionPool::TransactionEnd() txnid: (" + pooledConCtx.m_txnid + ")\n"
				});
			}
			if (this.m_cpCtx != null)
			{
				this.m_cpCtx.m_htTxnIdToIntance.Remove(pooledConCtx.m_txnid);
			}
			pooledConCtx.m_txnid = null;
			if (pooledConCtx != null && (OraTrace.m_PerformanceCounters & PerfCounterLevel.NumberOfStasisConnections) == PerfCounterLevel.NumberOfStasisConnections)
			{
				OraclePerfCounterCollection.NumberOfStasisConnections.Decrement();
			}
			if (pooledConCtx != null && pooledConCtx.m_promotableTxnManager != null)
			{
				if (pooledConCtx.m_promotableTxnManager.m_bLocalTxnPromoted)
				{
					try
					{
						OpsCon.DelistPromotedTxn(pooledConCtx.opsConCtx);
					}
					catch (Exception ex)
					{
						if (OraTrace.m_TraceLevel != 0U)
						{
							OraTrace.TraceExceptionInfo(ex);
						}
					}
				}
				string localTxnIdentifier = pooledConCtx.m_promotableTxnManager.m_localTxnIdentifier;
				if (!string.IsNullOrEmpty(localTxnIdentifier))
				{
					OracleConnection.m_pspePrimaryResourceEntry.Remove(localTxnIdentifier);
					pooledConCtx.m_promotableTxnManager = null;
				}
			}
			else
			{
				pooledConCtx.opoConRefCtx.pITransaction = null;
				try
				{
					num = OpsCon.Enlist(pooledConCtx.opsConCtx, pooledConCtx.pOpoConValCtx, pooledConCtx.opoConRefCtx);
				}
				catch (Exception ex2)
				{
					if (OraTrace.m_TraceLevel != 0U)
					{
						OraTrace.TraceExceptionInfo(ex2);
					}
					num = ErrRes.INT_ERR;
				}
				finally
				{
					if (num != 0)
					{
						if (OraTrace.m_TraceLevel != 0U)
						{
							OraTrace.Trace(4U, new string[]
							{
								" (MTS)  ConnectionPool::TransactionEnd(): delistment failure txnid:" + pooledConCtx.m_txnid + "\n"
							});
						}
						try
						{
							if (pooledConCtx.m_fetchArrayPooler != null)
							{
								pooledConCtx.m_fetchArrayPooler.Dispose();
								pooledConCtx.m_fetchArrayPooler = null;
							}
							OpsCon.Dispose(ref pooledConCtx.opsConCtx, ref pooledConCtx.opsErrCtx, ref pooledConCtx.pOpoConValCtx, pooledConCtx.opoConRefCtx);
						}
						catch (Exception ex3)
						{
							if (OraTrace.m_TraceLevel != 0U)
							{
								OraTrace.TraceExceptionInfo(ex3);
							}
						}
						if ((OraTrace.m_PerformanceCounters & PerfCounterLevel.HardDisconnectsPerSecond) == PerfCounterLevel.HardDisconnectsPerSecond)
						{
							OraclePerfCounterCollection.HardDisconnectsPerSecond.Increment();
						}
						this.UpdateTotalCount(-1, true);
					}
				}
				if (num != 0)
				{
					return;
				}
			}
			OpoConCtx opoConCtx = new OpoConCtx();
			opoConCtx.pooledConCtx = pooledConCtx;
			opoConCtx.opsConCtx = pooledConCtx.opsConCtx;
			opoConCtx.opsErrCtx = pooledConCtx.opsErrCtx;
			opoConCtx.pOpoConValCtx = pooledConCtx.pOpoConValCtx;
			opoConCtx.opoConRefCtx = new OpoConRefCtx();
			opoConCtx.opoConRefCtx.serverVersion = pooledConCtx.opoConRefCtx.serverVersion;
			opoConCtx.opoConRefCtx.dataSource = pooledConCtx.opoConRefCtx.dataSource;
			opoConCtx.opoConRefCtx.dbName = pooledConCtx.opoConRefCtx.dbName;
			opoConCtx.opoConRefCtx.hostName = pooledConCtx.opoConRefCtx.hostName;
			opoConCtx.opoConRefCtx.serviceName = pooledConCtx.opoConRefCtx.serviceName;
			opoConCtx.opoConRefCtx.instanceName = pooledConCtx.opoConRefCtx.instanceName;
			opoConCtx.opoConRefCtx.dbDomainName = pooledConCtx.opoConRefCtx.dbDomainName;
			opoConCtx.opoConRefCtx.ttOpsConOpenErrMssg = pooledConCtx.opoConRefCtx.ttOpsConOpenErrMssg;
			opoConCtx.m_conPooler = pooledConCtx.m_conPooler;
			opoConCtx.m_udtDescPoolerByName = pooledConCtx.m_udtDescPoolerByName;
			opoConCtx.m_udtDescPoolerByTDO = pooledConCtx.m_udtDescPoolerByTDO;
			opoConCtx.m_txnid = pooledConCtx.m_txnid;
			opoConCtx.m_systemTransaction = null;
			opoConCtx.m_txnType = TxnType.None;
			opoConCtx.m_promotableTxnManager = null;
			opoConCtx.pooledConCtx.m_promotableTxnManager = null;
			opoConCtx.m_fetchArrayPooler = pooledConCtx.m_fetchArrayPooler;
			opoConCtx.m_statementData = pooledConCtx.m_statementData;
			opoConCtx.m_totalDataAvailable = pooledConCtx.m_totalDataAvailable;
			if (this.m_bSynchronizeStack)
			{
				lock (this.m_connections)
				{
					this.PutConnection(ref opoConCtx, true, true, true, 1);
					goto IL_386;
				}
			}
			this.PutConnection(ref opoConCtx, true, true, true, 1);
			IL_386:
			opoConCtx = null;
		}

		// Token: 0x06000C65 RID: 3173 RVA: 0x0007DEDC File Offset: 0x0007CEDC
		public void ClearPool(bool bInvalidOnly, bool bRefresh)
		{
			int num = 0;
			int num2 = 0;
			Stack stack = null;
			int num3 = 0;
			Thread.Sleep(100);
			this.m_bSynchronizeStack = true;
			this.m_bClearPoolInProgress = true;
			try
			{
				lock (this.m_connections)
				{
					if (bInvalidOnly)
					{
						stack = Stack.Synchronized(new Stack());
					}
					else
					{
						this.m_clearRequestTimeStamp = DateTime.Now;
					}
					while (this.m_connections.Count > 0)
					{
						int num4 = 0;
						if (this.m_bGridRac && this.m_cpCtx != null)
						{
							try
							{
								num4 = OpsCon.WaitForSingleObject(this.m_cpCtx.m_semCPCtxAvaNumOfCons, 0);
							}
							catch (Exception ex)
							{
								if (OraTrace.m_TraceLevel != 0U)
								{
									OraTrace.TraceExceptionInfo(ex);
								}
								num4 = -1;
							}
							if (num4 != 0)
							{
								break;
							}
							Interlocked.Decrement(ref this.m_cpCtx.totalAvaliableConnections);
						}
						int num5 = 0;
						try
						{
							num5 = OpsCon.WaitForSingleObject(this.m_semAvaNumOfCons, 0);
						}
						catch (Exception ex2)
						{
							if (OraTrace.m_TraceLevel != 0U)
							{
								OraTrace.TraceExceptionInfo(ex2);
							}
							num5 = -1;
						}
						if (num5 == 0)
						{
							Interlocked.Decrement(ref this.m_counter.totalAvailable);
							PooledConCtx pooledConCtx = (PooledConCtx)this.m_connections.Pop();
							if (pooledConCtx != null && (OraTrace.m_PerformanceCounters & PerfCounterLevel.NumberOfFreeConnections) == PerfCounterLevel.NumberOfFreeConnections)
							{
								OraclePerfCounterCollection.NumberOfFreeConnections.Decrement();
							}
							try
							{
								if (bInvalidOnly)
								{
									OpsCon.CheckConStatus(pooledConCtx.opsConCtx, pooledConCtx.opsErrCtx, 0, ref num2, 1, 1);
								}
							}
							catch (Exception ex3)
							{
								if (OraTrace.m_TraceLevel != 0U)
								{
									OraTrace.TraceExceptionInfo(ex3);
								}
								num2 = 0;
							}
							finally
							{
								if (num2 == 0)
								{
									try
									{
										try
										{
											if (pooledConCtx.m_fetchArrayPooler != null)
											{
												pooledConCtx.m_fetchArrayPooler.Dispose();
												pooledConCtx.m_fetchArrayPooler = null;
											}
											OpsCon.Dispose(ref pooledConCtx.opsConCtx, ref pooledConCtx.opsErrCtx, ref pooledConCtx.pOpoConValCtx, pooledConCtx.opoConRefCtx);
										}
										catch (Exception ex4)
										{
											if (OraTrace.m_TraceLevel != 0U)
											{
												OraTrace.TraceExceptionInfo(ex4);
											}
										}
										continue;
									}
									finally
									{
										if ((OraTrace.m_PerformanceCounters & PerfCounterLevel.HardDisconnectsPerSecond) == PerfCounterLevel.HardDisconnectsPerSecond)
										{
											OraclePerfCounterCollection.HardDisconnectsPerSecond.Increment();
										}
										pooledConCtx.opsConCtx = IntPtr.Zero;
										pooledConCtx.opsErrCtx = IntPtr.Zero;
										pooledConCtx.pOpoConValCtx = null;
										pooledConCtx.m_conPooler = null;
										pooledConCtx.m_udtDescPoolerByName = null;
										pooledConCtx.m_udtDescPoolerByTDO = null;
										this.UpdateTotalCount(-1, true);
										num++;
									}
								}
								stack.Push(pooledConCtx);
							}
						}
						else
						{
							if (num4 == 0 && this.m_bGridRac && this.m_cpCtx != null)
							{
								try
								{
									OpsCon.ReleaseSemaphore(this.m_cpCtx.m_semCPCtxAvaNumOfCons, 1, ref num3);
								}
								catch (Exception ex5)
								{
									if (OraTrace.m_TraceLevel != 0U)
									{
										OraTrace.TraceExceptionInfo(ex5);
									}
								}
								Interlocked.Increment(ref this.m_cpCtx.totalAvaliableConnections);
								break;
							}
							break;
						}
					}
					if (stack != null && stack.Count > 0)
					{
						if ((OraTrace.m_PerformanceCounters & PerfCounterLevel.NumberOfFreeConnections) == PerfCounterLevel.NumberOfFreeConnections)
						{
							OraclePerfCounterCollection.NumberOfFreeConnections.IncrementBy(stack.Count - this.m_connections.Count);
						}
						this.m_connections = stack;
						for (int i = 0; i < stack.Count; i++)
						{
							if (this.m_bGridRac && this.m_cpCtx != null)
							{
								Interlocked.Increment(ref this.m_cpCtx.totalAvaliableConnections);
							}
							Interlocked.Increment(ref this.m_counter.totalAvailable);
							this.ReleaseSemaphore();
						}
					}
					if (this.m_bGridRac)
					{
						Interlocked.Add(ref this.m_cpCtx.m_consFromAppToClear, this.m_cpCtx.m_counter.total);
					}
					else
					{
						Interlocked.Add(ref this.m_consFromAppToClear, this.m_counter.total);
					}
				}
			}
			finally
			{
				this.m_bSynchronizeStack = false;
			}
			lock (this.m_counter)
			{
				if (!bRefresh && this.m_counter.total < this.m_clonedCtx.minPoolSize)
				{
					num = this.m_clonedCtx.minPoolSize - this.m_counter.total;
					bRefresh = true;
				}
				else if (bRefresh && num + this.m_counter.total < this.m_clonedCtx.minPoolSize)
				{
					num = this.m_clonedCtx.minPoolSize - this.m_counter.total;
				}
			}
			if (bRefresh && num > 0)
			{
				ThreadPool.QueueUserWorkItem(new WaitCallback(this.PopulatePool), num);
				this.UpdatePotentialTotalCount(num);
			}
		}

		// Token: 0x06000C66 RID: 3174 RVA: 0x0007E400 File Offset: 0x0007D400
		private void PushToResourcePool(OpoConCtx opoConCtx, PooledConCtx pooledConCtx)
		{
			pooledConCtx.m_txnid = opoConCtx.m_txnid;
			if (opoConCtx.m_txnType == TxnType.LocalTxnForSysTxn)
			{
				this.m_oraResPool.CacheResourceWithLocalTxn(opoConCtx.m_systemTransaction, pooledConCtx);
			}
			else if (opoConCtx.m_txnType == TxnType.SystemTxn)
			{
				this.m_oraResPool.PutResource(opoConCtx.m_systemTransaction, pooledConCtx);
			}
			else if (opoConCtx.m_txnType == TxnType.COMPlus)
			{
				try
				{
					if (ContextUtil.IsInTransaction)
					{
						lock (this.m_mtsConnections)
						{
							this.m_mtsConnections.PutResource(pooledConCtx);
							goto IL_88;
						}
					}
					this.TransactionEnd(pooledConCtx);
					IL_88:;
				}
				catch
				{
					this.TransactionEnd(pooledConCtx);
				}
			}
			opoConCtx.m_systemTransaction = null;
			opoConCtx.m_txnType = TxnType.None;
		}

		// Token: 0x06000C67 RID: 3175 RVA: 0x0007E4CC File Offset: 0x0007D4CC
		private void UpdateAgentRecommendations(OracleTuningAgent.RecommendationType recommendationType, object recommendation)
		{
			try
			{
				if (recommendationType == OracleTuningAgent.RecommendationType.SCS)
				{
					this.m_scsRecommendations = (int)recommendation;
					if (OraTrace.m_TraceLevel != 0U)
					{
						OraTrace.Trace(64U, new string[]
						{
							string.Concat(new object[]
							{
								" (TUNING) ConnectionPool::UpdateAgentRecommendations(): SCS recommendations for pool with Id: ",
								this.m_poolId,
								"; Change to ",
								this.m_scsRecommendations.ToString(),
								" \n"
							})
						});
					}
				}
			}
			catch (Exception ex)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.Trace(64U, new string[]
					{
						string.Concat(new object[]
						{
							" (ERROR) ConnectionPool::UpdateAgentRecommendations(): Pool Id: ",
							this.m_poolId,
							"; Exception: ",
							ex.ToString(),
							" \n"
						})
					});
				}
			}
		}

		// Token: 0x06000C68 RID: 3176 RVA: 0x0007E5BC File Offset: 0x0007D5BC
		private void IncrementStmtSamplesLimit()
		{
			this.m_stmtSamplesLimit += 100;
		}

		// Token: 0x040009AF RID: 2479
		internal const int DEFAULT_STMT_CACHE_SIZE_WITH_SELF_TUNING = 30;

		// Token: 0x040009B0 RID: 2480
		public const int WAIT_ABAONDONED = 128;

		// Token: 0x040009B1 RID: 2481
		public const int WAIT_OBJECT_0 = 0;

		// Token: 0x040009B2 RID: 2482
		public const int WAIT_TIMEOUT = 258;

		// Token: 0x040009B3 RID: 2483
		public const int WAIT_FAILED = -1;

		// Token: 0x040009B4 RID: 2484
		internal int m_agentKey = -1;

		// Token: 0x040009B5 RID: 2485
		internal int m_poolId = -1;

		// Token: 0x040009B6 RID: 2486
		internal int m_scsRecommendations = 30;

		// Token: 0x040009B7 RID: 2487
		internal int m_stmtSamplesLimit;

		// Token: 0x040009B8 RID: 2488
		public Stack m_connections;

		// Token: 0x040009B9 RID: 2489
		public ResourcePool m_mtsConnections;

		// Token: 0x040009BA RID: 2490
		public OracleResourcePool m_oraResPool;

		// Token: 0x040009BB RID: 2491
		public IntPtr m_semAvaNumOfCons;

		// Token: 0x040009BC RID: 2492
		public Timer m_timer;

		// Token: 0x040009BD RID: 2493
		public OpoConCtx m_clonedCtx;

		// Token: 0x040009BE RID: 2494
		public Counter m_counter;

		// Token: 0x040009BF RID: 2495
		private bool m_skipDecrement;

		// Token: 0x040009C0 RID: 2496
		public CPCtx m_cpCtx;

		// Token: 0x040009C1 RID: 2497
		public int m_rlbGravCounter;

		// Token: 0x040009C2 RID: 2498
		public float m_attemptedRequests;

		// Token: 0x040009C3 RID: 2499
		public bool m_bSynchronizeStack;

		// Token: 0x040009C4 RID: 2500
		public bool m_bClearPoolInProgress;

		// Token: 0x040009C5 RID: 2501
		public int m_consFromAppToClear;

		// Token: 0x040009C6 RID: 2502
		public bool m_bGridRac;

		// Token: 0x040009C7 RID: 2503
		public EncryptedPassword m_encryptedPwd;

		// Token: 0x040009C8 RID: 2504
		public EncryptedPassword m_encryptedPxyPwd;

		// Token: 0x040009C9 RID: 2505
		private bool m_inactive;

		// Token: 0x040009CA RID: 2506
		private static object m_populationSyncObj = new object();

		// Token: 0x040009CB RID: 2507
		public object m_passwordSyncObj = new object();

		// Token: 0x040009CC RID: 2508
		public DateTime m_clearRequestTimeStamp;

		// Token: 0x040009CD RID: 2509
		private WindowsIdentity m_identity;
	}
}
