using System;
using System.Collections;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;

namespace Oracle.DataAccess.Client
{
	// Token: 0x02000135 RID: 309
	internal class ConnectionDispenser
	{
		// Token: 0x06000C6A RID: 3178 RVA: 0x0007E5DC File Offset: 0x0007D5DC
		static ConnectionDispenser()
		{
			ConnectionDispenser.m_random = new Random(DateTime.Today.Millisecond);
			ConnectionDispenser.m_htHAOpoConCtx = Hashtable.Synchronized(new Hashtable());
			ConnectionDispenser.m_htRLBOpoConCtx = Hashtable.Synchronized(new Hashtable());
			ConnectionDispenser.m_HACallback = new OraHACallbackFuncPtr(ConnectionDispenser.OnHACallback);
			ConnectionDispenser.m_RLBCallback = new OraRLBCallbackFuncPtr(ConnectionDispenser.OnRLBCallback);
			ConnectionDispenser.m_pspePrimaryResources = Hashtable.Synchronized(new Hashtable());
		}

		// Token: 0x06000C6B RID: 3179 RVA: 0x0007E684 File Offset: 0x0007D684
		public unsafe static int Enlist(OpoConCtx opoConCtx)
		{
			int num = 0;
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
			}
			return num;
		}

		// Token: 0x06000C6C RID: 3180 RVA: 0x0007E7A0 File Offset: 0x0007D7A0
		public unsafe static int Open(OpoConCtx opoConCtx)
		{
			int num = 0;
			if (opoConCtx.pOpoConValCtx->Pooling == 0)
			{
				try
				{
					num = OpsCon.Open(ref opoConCtx.opsConCtx, ref opoConCtx.opsErrCtx, opoConCtx.pOpoConValCtx, ref opoConCtx.opoConRefCtx);
					if (num == 0 && (OraTrace.m_PerformanceCounters & PerfCounterLevel.HardConnectsPerSecond) == PerfCounterLevel.HardConnectsPerSecond)
					{
						OraclePerfCounterCollection.HardConnectsPerSecond.Increment();
					}
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
				if (num != 0)
				{
					return num;
				}
				opoConCtx.creationTime = DateTime.Now;
				if (opoConCtx.pOpoConValCtx->Enlist == 1 && opoConCtx.pOpoConValCtx->InMtsTxn == 0 && (opoConCtx.opoConRefCtx.proxyUserId == null || opoConCtx.opoConRefCtx.proxyUserId.Length == 0))
				{
					num = ConnectionDispenser.Enlist(opoConCtx);
				}
				if (opoConCtx.metaPool == 1)
				{
					if (opoConCtx.m_bSelfTuning)
					{
						opoConCtx.m_conPooler = new ConPooler(ConPooler.DEFAULT_MAX_ELEMS_IN_POOL_TUNING_ON);
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
				if (num == 0)
				{
					if ((OraTrace.m_PerformanceCounters & PerfCounterLevel.NumberOfNonPooledConnections) == PerfCounterLevel.NumberOfNonPooledConnections)
					{
						OraclePerfCounterCollection.NumberOfNonPooledConnections.Increment();
					}
					if ((OraTrace.m_PerformanceCounters & PerfCounterLevel.NumberOfActiveConnections) == PerfCounterLevel.NumberOfActiveConnections)
					{
						OraclePerfCounterCollection.NumberOfActiveConnections.Increment();
					}
				}
				return num;
			}
			else
			{
				bool flag = false;
				num = ConnectionDispenser.GetConnectionPool(ref opoConCtx, ref flag);
				if (num != 0)
				{
					return num;
				}
				if (opoConCtx.pool != null && !flag)
				{
					int num2 = opoConCtx.AuthenticateUser();
					if (num2 != 0 || opoConCtx.opoConRefCtx.newPassword != "")
					{
						num = OpsCon.Open(ref opoConCtx.opsConCtx, ref opoConCtx.opsErrCtx, opoConCtx.pOpoConValCtx, ref opoConCtx.opoConRefCtx);
						if (num == 0)
						{
							if (opoConCtx.opoConRefCtx.newPassword != "")
							{
								opoConCtx.opoConRefCtx.password = opoConCtx.opoConRefCtx.newPassword;
								opoConCtx.opoConRefCtx.newPassword = "";
							}
							lock (opoConCtx.pool.m_passwordSyncObj)
							{
								opoConCtx.pool.ResetPasswords(opoConCtx.opoConRefCtx.password, opoConCtx.opoConRefCtx.proxyPassword);
								opoConCtx.pool.ClearPool(false, false);
							}
							OpsCon.Close(ref opoConCtx.opsConCtx, ref opoConCtx.opsErrCtx, opoConCtx.pOpoConValCtx, opoConCtx.opoConRefCtx);
						}
						else
						{
							OracleException.HandleError(num2, null, IntPtr.Zero, null);
						}
					}
				}
				if (opoConCtx.pool.m_cpCtx != null && !flag)
				{
					num = opoConCtx.pool.m_cpCtx.GetConnection(opoConCtx);
					if (num != 0)
					{
						return num;
					}
				}
				else if (opoConCtx.pool != null && !flag)
				{
					num = opoConCtx.pool.GetConnection(opoConCtx);
					if (num != 0)
					{
						return num;
					}
				}
				if (opoConCtx.m_bSelfTuning && opoConCtx.pool != null)
				{
					int stmtCacheSize = opoConCtx.pOpoConValCtx->StmtCacheSize;
					if (opoConCtx.pool.m_scsRecommendations <= OraTrace.MaxStatementCacheSize)
					{
						if (opoConCtx.pool.m_scsRecommendations >= opoConCtx.pOpoConValCtx->StmtCacheSize || opoConCtx.pool.m_scsRecommendations <= (int)((float)opoConCtx.pOpoConValCtx->StmtCacheSize * 0.9f))
						{
							opoConCtx.pOpoConValCtx->StmtCacheSize = opoConCtx.pool.m_scsRecommendations;
						}
					}
					else if (opoConCtx.pOpoConValCtx->StmtCacheSize > OraTrace.MaxStatementCacheSize)
					{
						opoConCtx.pOpoConValCtx->StmtCacheSize = OraTrace.MaxStatementCacheSize;
					}
					if (stmtCacheSize != opoConCtx.pOpoConValCtx->StmtCacheSize)
					{
						try
						{
							if (OraTrace.m_TraceLevel != 0U)
							{
								OraTrace.Trace(64U, new string[]
								{
									string.Concat(new object[]
									{
										" (TUNING) ConnectionDispenser::Open(): Setting stmt cache size for connection (Pool Id: ",
										opoConCtx.pool.m_poolId,
										") to ",
										opoConCtx.pOpoConValCtx->StmtCacheSize.ToString(),
										" \n"
									})
								});
							}
							num = OpsCon.SetStatementCacheSize(opoConCtx.opsConCtx, ref opoConCtx.opsErrCtx, opoConCtx.pOpoConValCtx);
							if (opoConCtx.m_conPooler != null)
							{
								opoConCtx.m_conPooler.ModifyConPoolerSize(opoConCtx.pOpoConValCtx->StmtCacheSize);
							}
						}
						catch (Exception ex2)
						{
							if (OraTrace.m_TraceLevel != 0U)
							{
								OraTrace.Trace(64U, new string[]
								{
									string.Concat(new object[]
									{
										"(ERROR) ConnectionPool::ConnectionDispenser(): Open (Pool Id: ",
										opoConCtx.pool.m_poolId,
										"); Exception: ",
										ex2.ToString(),
										" \n"
									})
								});
							}
						}
					}
				}
				if (num == 0 && opoConCtx.pOpoConValCtx->SessionBegin == 1)
				{
					if ((OraTrace.m_PerformanceCounters & PerfCounterLevel.SoftConnectsPerSecond) == PerfCounterLevel.SoftConnectsPerSecond)
					{
						OraclePerfCounterCollection.SoftConnectsPerSecond.Increment();
					}
					if ((OraTrace.m_PerformanceCounters & PerfCounterLevel.NumberOfPooledConnections) == PerfCounterLevel.NumberOfPooledConnections)
					{
						OraclePerfCounterCollection.NumberOfPooledConnections.Increment();
					}
					if ((OraTrace.m_PerformanceCounters & PerfCounterLevel.NumberOfActiveConnections) == PerfCounterLevel.NumberOfActiveConnections)
					{
						OraclePerfCounterCollection.NumberOfActiveConnections.Increment();
					}
				}
				return num;
			}
		}

		// Token: 0x06000C6D RID: 3181 RVA: 0x0007ECD8 File Offset: 0x0007DCD8
		public unsafe static int Close(ref OpoConCtx opoConCtx, bool isContextConnection)
		{
			int result = 0;
			if (opoConCtx.pOpoConValCtx->Pooling == 0)
			{
				if (opoConCtx.m_fetchArrayPooler != null)
				{
					opoConCtx.m_fetchArrayPooler.Dispose();
				}
				if (TxnType.LocalTxnForSysTxn == opoConCtx.m_txnType && opoConCtx.m_promotableTxnManager != null && !string.IsNullOrEmpty(opoConCtx.m_promotableTxnManager.m_localTxnIdentifier))
				{
					ConnectionDispenser.m_pspePrimaryResources.Add(opoConCtx.m_promotableTxnManager.m_localTxnIdentifier, opoConCtx);
					opoConCtx = null;
				}
				else
				{
					try
					{
						result = OpsCon.Close(ref opoConCtx.opsConCtx, ref opoConCtx.opsErrCtx, opoConCtx.pOpoConValCtx, opoConCtx.opoConRefCtx);
					}
					catch (Exception ex)
					{
						if (OraTrace.m_TraceLevel != 0U)
						{
							OraTrace.TraceExceptionInfo(ex);
						}
					}
					if ((OraTrace.m_PerformanceCounters & PerfCounterLevel.HardDisconnectsPerSecond) == PerfCounterLevel.HardDisconnectsPerSecond && !isContextConnection)
					{
						OraclePerfCounterCollection.HardDisconnectsPerSecond.Increment();
					}
					if ((OraTrace.m_PerformanceCounters & PerfCounterLevel.NumberOfNonPooledConnections) == PerfCounterLevel.NumberOfNonPooledConnections && !isContextConnection)
					{
						OraclePerfCounterCollection.NumberOfNonPooledConnections.Decrement();
					}
					if ((OraTrace.m_PerformanceCounters & PerfCounterLevel.NumberOfActiveConnections) == PerfCounterLevel.NumberOfActiveConnections && !isContextConnection)
					{
						OraclePerfCounterCollection.NumberOfActiveConnections.Decrement();
					}
					opoConCtx.m_conPooler = null;
					opoConCtx.m_udtDescPoolerByName = null;
					opoConCtx.m_udtDescPoolerByTDO = null;
					opoConCtx.m_systemTransaction = null;
					opoConCtx.m_txnType = TxnType.None;
					opoConCtx.m_promotableTxnManager = null;
				}
			}
			else if (opoConCtx.pool != null)
			{
				if (OracleTuningAgent.bHighMemoryAlertFlag && opoConCtx.m_fetchArrayPooler != null)
				{
					opoConCtx.m_fetchArrayPooler.ReSizeFetchArrayPooler(1);
				}
				if (opoConCtx.pool.m_bSynchronizeStack)
				{
					lock (opoConCtx.pool.m_connections)
					{
						if (opoConCtx.pOpoConValCtx->InMtsTxn == 1)
						{
							result = opoConCtx.pool.PutConnection(ref opoConCtx, false, true, true, 1);
						}
						else
						{
							result = opoConCtx.pool.PutConnection(ref opoConCtx, false, true, true, 0);
						}
						goto IL_1ED;
					}
				}
				if (opoConCtx.pOpoConValCtx->InMtsTxn == 1)
				{
					result = opoConCtx.pool.PutConnection(ref opoConCtx, false, true, true, 1);
				}
				else
				{
					result = opoConCtx.pool.PutConnection(ref opoConCtx, false, true, true, 0);
				}
				IL_1ED:
				if ((OraTrace.m_PerformanceCounters & PerfCounterLevel.SoftDisconnectsPerSecond) == PerfCounterLevel.SoftDisconnectsPerSecond)
				{
					OraclePerfCounterCollection.SoftDisconnectsPerSecond.Increment();
				}
				if ((OraTrace.m_PerformanceCounters & PerfCounterLevel.NumberOfPooledConnections) == PerfCounterLevel.NumberOfPooledConnections)
				{
					OraclePerfCounterCollection.NumberOfPooledConnections.Decrement();
				}
				if ((OraTrace.m_PerformanceCounters & PerfCounterLevel.NumberOfActiveConnections) == PerfCounterLevel.NumberOfActiveConnections)
				{
					OraclePerfCounterCollection.NumberOfActiveConnections.Decrement();
				}
			}
			else
			{
				ConnectionDispenser.Dispose(ref opoConCtx);
				if ((OraTrace.m_PerformanceCounters & PerfCounterLevel.NumberOfPooledConnections) == PerfCounterLevel.NumberOfPooledConnections)
				{
					OraclePerfCounterCollection.NumberOfPooledConnections.Decrement();
				}
				if ((OraTrace.m_PerformanceCounters & PerfCounterLevel.NumberOfActiveConnections) == PerfCounterLevel.NumberOfActiveConnections)
				{
					OraclePerfCounterCollection.NumberOfActiveConnections.Decrement();
				}
			}
			return result;
		}

		// Token: 0x06000C6E RID: 3182 RVA: 0x0007EF78 File Offset: 0x0007DF78
		public static int Dispose(ref OpoConCtx opoConCtx)
		{
			bool flag = opoConCtx.opsConCtx == IntPtr.Zero;
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
			if (!flag && (OraTrace.m_PerformanceCounters & PerfCounterLevel.HardDisconnectsPerSecond) == PerfCounterLevel.HardDisconnectsPerSecond)
			{
				OraclePerfCounterCollection.HardDisconnectsPerSecond.Increment();
			}
			opoConCtx.opsConCtx = IntPtr.Zero;
			opoConCtx.opsErrCtx = IntPtr.Zero;
			opoConCtx.pOpoConValCtx = null;
			opoConCtx.opoConRefCtx = null;
			opoConCtx.pooledConCtx = null;
			opoConCtx.m_conPooler = null;
			opoConCtx.m_udtDescPoolerByName = null;
			opoConCtx.m_udtDescPoolerByTDO = null;
			opoConCtx.m_systemTransaction = null;
			opoConCtx.m_txnType = TxnType.None;
			return 0;
		}

		// Token: 0x06000C6F RID: 3183 RVA: 0x0007F06C File Offset: 0x0007E06C
		public static int GetConnectionPool(ref OpoConCtx opoConCtx, ref bool bConObtained)
		{
			int num = 0;
			bool flag = false;
			bool flag2 = false;
			bool flag3 = false;
			RLBCtx rlbctx = null;
			CPCtx cpctx = null;
			opoConCtx.pool = null;
			int result;
			try
			{
				if (opoConCtx.bGridRac)
				{
					string text = null;
					if (ConnectionDispenser.m_htTnsToSvc == null && ConnectionDispenser.m_htSvcToRLB == null)
					{
						lock (ConnectionDispenser.s_lockObj)
						{
							if (ConnectionDispenser.m_htTnsToSvc == null)
							{
								ConnectionDispenser.m_htTnsToSvc = Hashtable.Synchronized(new Hashtable());
							}
							if (ConnectionDispenser.m_htSvcToRLB == null)
							{
								ConnectionDispenser.m_htSvcToRLB = Hashtable.Synchronized(new Hashtable());
							}
						}
					}
					text = (string)ConnectionDispenser.m_htTnsToSvc[opoConCtx.opoConRefCtx.dataSource];
					if (text == null)
					{
						lock (ConnectionDispenser.m_htTnsToSvc.SyncRoot)
						{
							if (ConnectionDispenser.m_htTnsToSvc[opoConCtx.opoConRefCtx.dataSource] == null)
							{
								num = ConnectionDispenser.CreateConnectionPool(ref opoConCtx);
								if (num != 0)
								{
									return num;
								}
								bConObtained = true;
								text = opoConCtx.opoConRefCtx.serviceName;
								ConnectionDispenser.m_htTnsToSvc[opoConCtx.opoConRefCtx.dataSource] = text;
							}
							else
							{
								text = (string)ConnectionDispenser.m_htTnsToSvc[opoConCtx.opoConRefCtx.dataSource];
							}
						}
					}
					rlbctx = (RLBCtx)ConnectionDispenser.m_htSvcToRLB[text];
					if (rlbctx == null)
					{
						lock (ConnectionDispenser.m_htSvcToRLB.SyncRoot)
						{
							if ((RLBCtx)ConnectionDispenser.m_htSvcToRLB[text] == null)
							{
								num = ConnectionDispenser.CreateConnectionPool(ref opoConCtx);
								if (num != 0)
								{
									return num;
								}
								bConObtained = true;
							}
							rlbctx = (RLBCtx)ConnectionDispenser.m_htSvcToRLB[text];
						}
					}
					cpctx = (CPCtx)rlbctx.htConToInst[opoConCtx.conString];
					if (cpctx == null)
					{
						lock (rlbctx)
						{
							lock (rlbctx.htConToInst.SyncRoot)
							{
								if (rlbctx.htConToInst[opoConCtx.conString] == null)
								{
									num = ConnectionDispenser.CreateConnectionPool(ref opoConCtx);
									if (num != 0)
									{
										return num;
									}
									bConObtained = true;
								}
								cpctx = (CPCtx)rlbctx.htConToInst[opoConCtx.conString];
							}
						}
					}
					if (opoConCtx.pool == null)
					{
						if (opoConCtx.m_systemTransaction != null && opoConCtx.bGridRac)
						{
							if (opoConCtx.m_txnid == null)
							{
								opoConCtx.m_txnid = opoConCtx.m_systemTransaction.TransactionInformation.LocalIdentifier;
							}
							opoConCtx.affinityInstanceName = (string)cpctx.m_htTxnIdToIntance[opoConCtx.m_txnid];
							if (opoConCtx.affinityInstanceName != null)
							{
								opoConCtx.pool = (ConnectionPool)cpctx.htInstToCp[opoConCtx.affinityInstanceName];
								opoConCtx.instanceConCount = opoConCtx.pool.m_connections.Count;
								return 0;
							}
						}
						else
						{
							opoConCtx.m_txnid = null;
						}
						if (opoConCtx.gridRLB == 1 && rlbctx.RLBMetricsList != null && rlbctx.RLBMetricsList.Count > 0)
						{
							lock (rlbctx)
							{
								if (rlbctx.RLBMetricsList != null && rlbctx.RLBMetricsList.Count > 0)
								{
									int i = 0;
									bool flag10 = false;
									while (!flag10)
									{
										int num2 = int.MaxValue;
										for (i = 0; i < rlbctx.RLBMetricsList.Count; i++)
										{
											if (((RLBMetrics)rlbctx.RLBMetricsList[i]).CurDistribFreq == 0)
											{
												flag10 = true;
												break;
											}
											num2 = Math.Min(num2, ((RLBMetrics)rlbctx.RLBMetricsList[i]).CurDistribFreq);
										}
										if (!flag10)
										{
											for (i = 0; i < rlbctx.RLBMetricsList.Count; i++)
											{
												((RLBMetrics)rlbctx.RLBMetricsList[i]).CurDistribFreq -= num2;
											}
										}
									}
									opoConCtx.pool = (ConnectionPool)cpctx.htInstToCp[((RLBMetrics)rlbctx.RLBMetricsList[i]).InstanceName];
									if (opoConCtx.pool == null)
									{
										lock (cpctx.htInstToCp.SyncRoot)
										{
											if (cpctx.htInstToCp[((RLBMetrics)rlbctx.RLBMetricsList[i]).InstanceName] == null)
											{
												opoConCtx.opoConRefCtx.serviceName = rlbctx.ServiceName;
												opoConCtx.opoConRefCtx.instanceName = ((RLBMetrics)rlbctx.RLBMetricsList[i]).InstanceName;
												opoConCtx.pool = new ConnectionPool(opoConCtx, cpctx);
												cpctx.htInstToCp[((RLBMetrics)rlbctx.RLBMetricsList[i]).InstanceName] = opoConCtx.pool;
												opoConCtx.pool.UpdatePotentialTotalCount(opoConCtx.pool.m_clonedCtx.poolIncSize);
												ThreadPool.QueueUserWorkItem(new WaitCallback(opoConCtx.pool.PopulatePool), opoConCtx.pool.m_clonedCtx.poolIncSize);
											}
											else
											{
												opoConCtx.pool = (ConnectionPool)cpctx.htInstToCp[((RLBMetrics)rlbctx.RLBMetricsList[i]).InstanceName];
											}
										}
									}
									if (bConObtained && (OraTrace.m_TraceLevel & 32U) == 32U)
									{
										StringBuilder stringBuilder = new StringBuilder();
										stringBuilder.Append(" (GRID) (RLB) (DISP) (inst=");
										stringBuilder.Append(opoConCtx.opoConRefCtx.instanceName);
										stringBuilder.Append(") ");
										for (int j = 0; j < rlbctx.RLBMetricsList.Count; j++)
										{
											stringBuilder.Append("(");
											stringBuilder.Append(((RLBMetrics)rlbctx.RLBMetricsList[j]).InstanceName);
											ConnectionPool connectionPool = (ConnectionPool)cpctx.htInstToCp[((RLBMetrics)rlbctx.RLBMetricsList[j]).InstanceName];
											if (connectionPool != null)
											{
												stringBuilder.Append(": used=");
												stringBuilder.Append(connectionPool.m_counter.total - connectionPool.m_connections.Count);
												stringBuilder.Append("; idle=");
												stringBuilder.Append(connectionPool.m_connections.Count);
												stringBuilder.Append("; tot=");
												stringBuilder.Append(connectionPool.m_counter.total);
											}
											else
											{
												stringBuilder.Append(": N/A");
											}
											stringBuilder.Append("; counter=");
											stringBuilder.Append(((RLBMetrics)rlbctx.RLBMetricsList[j]).CurDistribFreq);
											stringBuilder.Append("/");
											stringBuilder.Append(((RLBMetrics)rlbctx.RLBMetricsList[j]).MaxDistribFreq);
											stringBuilder.Append(") ");
										}
										stringBuilder.Append(")\n");
										OraTrace.Trace(32U, new string[]
										{
											stringBuilder.ToString()
										});
									}
									if (opoConCtx.pool != null)
									{
										return num;
									}
								}
							}
						}
						if (opoConCtx.pool != null)
						{
							if (!flag3)
							{
								Monitor.Enter(cpctx.htInstToCp.SyncRoot);
								flag3 = true;
							}
							cpctx.htInstToCp[opoConCtx.opoConRefCtx.instanceName] = opoConCtx.pool;
						}
						else
						{
							if (!flag3)
							{
								Monitor.Enter(cpctx.htInstToCp.SyncRoot);
								flag3 = true;
							}
							if (cpctx.htInstToCp.Count == 0)
							{
								if (opoConCtx.pool == null)
								{
									num = ConnectionDispenser.CreateConnectionPool(ref opoConCtx);
									if (num != 0)
									{
										return num;
									}
								}
								bConObtained = true;
								return num;
							}
							int num3 = Interlocked.Increment(ref ConnectionDispenser.m_iteration) % cpctx.htInstToCp.Count;
							if (ConnectionDispenser.m_iteration > 1073741823)
							{
								ConnectionDispenser.m_iteration = 0;
							}
							int num4 = 0;
							IDictionaryEnumerator enumerator = cpctx.htInstToCp.GetEnumerator();
							while (enumerator.MoveNext() && num4 != num3)
							{
								num4++;
							}
							opoConCtx.pool = (ConnectionPool)enumerator.Value;
						}
					}
					result = num;
				}
				else
				{
					if (ConnectionDispenser.m_ConnectionPools == null)
					{
						lock (ConnectionDispenser.s_lockObj)
						{
							if (ConnectionDispenser.m_ConnectionPools == null)
							{
								num = ConnectionDispenser.CreateConnectionPool(ref opoConCtx);
								if (num != 0)
								{
									return num;
								}
								bConObtained = true;
								Hashtable hashtable = Hashtable.Synchronized(new Hashtable());
								hashtable[opoConCtx.conString] = opoConCtx.pool;
								ConnectionDispenser.m_ConnectionPools = hashtable;
							}
							goto IL_91A;
						}
					}
					opoConCtx.pool = (ConnectionPool)ConnectionDispenser.m_ConnectionPools[opoConCtx.conString];
					IL_91A:
					if (opoConCtx.pool == null)
					{
						lock (ConnectionDispenser.s_lockObj)
						{
							opoConCtx.pool = (ConnectionPool)ConnectionDispenser.m_ConnectionPools[opoConCtx.conString];
							if (opoConCtx.pool == null)
							{
								num = ConnectionDispenser.CreateConnectionPool(ref opoConCtx);
								if (num == 0)
								{
									bConObtained = true;
									ConnectionDispenser.m_ConnectionPools[opoConCtx.conString] = opoConCtx.pool;
								}
							}
						}
					}
					result = num;
				}
			}
			finally
			{
				if (flag)
				{
					Monitor.Exit(ConnectionDispenser.m_htSvcToRLB.SyncRoot);
				}
				if (flag2)
				{
					Monitor.Exit(rlbctx.htConToInst.SyncRoot);
				}
				if (flag3)
				{
					Monitor.Exit(cpctx.htInstToCp.SyncRoot);
				}
			}
			return result;
		}

		// Token: 0x06000C70 RID: 3184 RVA: 0x0007FB40 File Offset: 0x0007EB40
		public unsafe static int CreateConnectionPool(ref OpoConCtx opoConCtx)
		{
			int num = 0;
			bool flag = false;
			bool flag2 = false;
			try
			{
				num = OpsCon.Open(ref opoConCtx.opsConCtx, ref opoConCtx.opsErrCtx, opoConCtx.pOpoConValCtx, ref opoConCtx.opoConRefCtx);
				if (num == 0 && (OraTrace.m_PerformanceCounters & PerfCounterLevel.HardConnectsPerSecond) == PerfCounterLevel.HardConnectsPerSecond)
				{
					OraclePerfCounterCollection.HardConnectsPerSecond.Increment();
				}
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
			if (num != 0)
			{
				return num;
			}
			if (opoConCtx.opoConRefCtx.proxyUserId == null || opoConCtx.opoConRefCtx.proxyUserId.Length <= 0)
			{
				if (opoConCtx.pOpoConValCtx->OSAuthent != 2)
				{
					goto IL_196;
				}
			}
			try
			{
				num = OpsCon.OpenProxyAuthUserSession(opoConCtx.opsConCtx, opoConCtx.opsErrCtx, opoConCtx.pOpoConValCtx, opoConCtx.opoConRefCtx);
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
			finally
			{
				if (num != 0)
				{
					OpoConValCtx* ptr = null;
					try
					{
						OpsCon.Close(ref opoConCtx.opsConCtx, ref opoConCtx.opsErrCtx, opoConCtx.pOpoConValCtx, opoConCtx.opoConRefCtx);
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
					try
					{
						if (opoConCtx.m_fetchArrayPooler != null)
						{
							opoConCtx.m_fetchArrayPooler.Dispose();
							opoConCtx.m_fetchArrayPooler = null;
						}
						OpsCon.Dispose(ref opoConCtx.opsConCtx, ref opoConCtx.opsErrCtx, ref ptr, opoConCtx.opoConRefCtx);
					}
					catch (Exception ex4)
					{
						if (OraTrace.m_TraceLevel != 0U)
						{
							OraTrace.TraceExceptionInfo(ex4);
						}
					}
				}
			}
			if (num != 0)
			{
				return num;
			}
			IL_196:
			if ((num = ConnectionDispenser.RegisterCallbacks(opoConCtx)) != 0)
			{
				OpoConValCtx* ptr2 = null;
				try
				{
					OpsCon.Close(ref opoConCtx.opsConCtx, ref opoConCtx.opsErrCtx, opoConCtx.pOpoConValCtx, opoConCtx.opoConRefCtx);
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
				try
				{
					if (opoConCtx.m_fetchArrayPooler != null)
					{
						opoConCtx.m_fetchArrayPooler.Dispose();
						opoConCtx.m_fetchArrayPooler = null;
					}
					OpsCon.Dispose(ref opoConCtx.opsConCtx, ref opoConCtx.opsErrCtx, ref ptr2, opoConCtx.opoConRefCtx);
				}
				catch (Exception ex6)
				{
					if (OraTrace.m_TraceLevel != 0U)
					{
						OraTrace.TraceExceptionInfo(ex6);
					}
				}
				return num;
			}
			opoConCtx.creationTime = DateTime.Now;
			opoConCtx.pool = null;
			RLBCtx rlbctx = null;
			CPCtx cpctx = null;
			if (opoConCtx.bGridRac)
			{
				rlbctx = (RLBCtx)ConnectionDispenser.m_htSvcToRLB[opoConCtx.opoConRefCtx.serviceName];
				if (rlbctx != null)
				{
					cpctx = (CPCtx)rlbctx.htConToInst[opoConCtx.conString];
					if (cpctx != null && cpctx.htInstToCp != null)
					{
						opoConCtx.pool = (ConnectionPool)cpctx.htInstToCp[opoConCtx.opoConRefCtx.instanceName];
						if (opoConCtx.pool != null && opoConCtx.pool.m_clonedCtx.opoConRefCtx.dbName == null)
						{
							opoConCtx.pool.m_clonedCtx.opoConRefCtx.dbName = opoConCtx.opoConRefCtx.dbName;
							opoConCtx.pool.m_clonedCtx.opoConRefCtx.hostName = opoConCtx.opoConRefCtx.hostName;
							opoConCtx.pool.m_clonedCtx.opoConRefCtx.instanceName = opoConCtx.opoConRefCtx.instanceName;
							opoConCtx.pool.m_clonedCtx.opoConRefCtx.dbDomainName = opoConCtx.opoConRefCtx.dbDomainName;
						}
					}
				}
				else
				{
					lock (ConnectionDispenser.m_htSvcToRLB.SyncRoot)
					{
						if (ConnectionDispenser.m_htSvcToRLB[opoConCtx.opoConRefCtx.serviceName] == null)
						{
							rlbctx = new RLBCtx(opoConCtx.opoConRefCtx.serviceName);
							ConnectionDispenser.m_htSvcToRLB[opoConCtx.opoConRefCtx.serviceName] = rlbctx;
						}
					}
				}
				cpctx = (CPCtx)rlbctx.htConToInst[opoConCtx.conString];
				if (cpctx == null)
				{
					lock (rlbctx.htConToInst.SyncRoot)
					{
						if (rlbctx.htConToInst[opoConCtx.conString] == null)
						{
							try
							{
								cpctx = new CPCtx(opoConCtx.maxPoolSize, rlbctx, opoConCtx.poolRegulator);
								flag = true;
							}
							catch
							{
								num = -1;
							}
							if (opoConCtx.m_systemTransaction != null)
							{
								opoConCtx.m_txnid = opoConCtx.m_systemTransaction.TransactionInformation.LocalIdentifier;
								cpctx.m_htTxnIdToIntance[opoConCtx.m_txnid] = opoConCtx.opoConRefCtx.instanceName;
							}
							if (num != -1)
							{
								rlbctx.htConToInst[opoConCtx.conString] = cpctx;
							}
						}
					}
				}
				if (num != -1)
				{
					opoConCtx.pool = (ConnectionPool)cpctx.htInstToCp[opoConCtx.opoConRefCtx.instanceName];
				}
				if (opoConCtx.pool == null && num != -1)
				{
					lock (cpctx.htInstToCp.SyncRoot)
					{
						if (cpctx.htInstToCp[opoConCtx.opoConRefCtx.instanceName] == null)
						{
							try
							{
								opoConCtx.pool = new ConnectionPool(opoConCtx, cpctx);
								flag2 = true;
							}
							catch
							{
								num = -1;
							}
							if (num != -1)
							{
								cpctx.htInstToCp[opoConCtx.opoConRefCtx.instanceName] = opoConCtx.pool;
							}
						}
					}
				}
				if (num != 0)
				{
					OpoConValCtx* ptr3 = null;
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
						OpsCon.Dispose(ref opoConCtx.opsConCtx, ref opoConCtx.opsErrCtx, ref ptr3, opoConCtx.opoConRefCtx);
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
				ConnectionDispenser.m_totalNumberOfConnectionPools++;
				OraTrace.Trace(2U, new string[]
				{
					" (POOL) (AFFINITY) (Dispensed con for " + opoConCtx.pool.m_clonedCtx.opoConRefCtx.instanceName + "; no affinity specified\n"
				});
				if (OraTrace.m_TraceLevel != 0U)
				{
					if (flag)
					{
						OraTrace.Trace(2U, new string[]
						{
							string.Concat(new object[]
							{
								" (POOL)  New CPCtx created (CPCtx id: ",
								cpctx.GetHashCode(),
								") for: [Attributes=\"",
								opoConCtx.poolName,
								"\"] [Database=",
								opoConCtx.opoConRefCtx.dbName,
								";Service=",
								opoConCtx.opoConRefCtx.serviceName,
								";Host=",
								opoConCtx.opoConRefCtx.hostName,
								"] (Inst CP id: ",
								opoConCtx.pool.GetHashCode(),
								")\n"
							})
						});
					}
					if (flag2)
					{
						OraTrace.Trace(2U, new string[]
						{
							string.Concat(new object[]
							{
								" (POOL)  New CP created (CP id: ",
								opoConCtx.pool.GetHashCode(),
								"; CPCtx id: ",
								cpctx.GetHashCode(),
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
								cpctx.GetHashCode(),
								") : ",
								cpctx.htInstToCp.Count,
								"\n"
							})
						});
					}
				}
			}
			else
			{
				try
				{
					opoConCtx.pool = new ConnectionPool(opoConCtx, null);
				}
				catch (Exception ex9)
				{
					if (OraTrace.m_TraceLevel != 0U)
					{
						OraTrace.TraceExceptionInfo(ex9);
					}
					num = ErrRes.INT_ERR;
					opoConCtx.exceptMsg = ex9.ToString();
				}
				finally
				{
					if (num != 0)
					{
						OpoConValCtx* ptr4 = null;
						try
						{
							OpsCon.Close(ref opoConCtx.opsConCtx, ref opoConCtx.opsErrCtx, opoConCtx.pOpoConValCtx, opoConCtx.opoConRefCtx);
						}
						catch (Exception ex10)
						{
							if (OraTrace.m_TraceLevel != 0U)
							{
								OraTrace.TraceExceptionInfo(ex10);
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
							OpsCon.Dispose(ref opoConCtx.opsConCtx, ref opoConCtx.opsErrCtx, ref ptr4, opoConCtx.opoConRefCtx);
						}
						catch (Exception ex11)
						{
							if (OraTrace.m_TraceLevel != 0U)
							{
								OraTrace.TraceExceptionInfo(ex11);
							}
						}
					}
				}
				if (num != 0)
				{
					return num;
				}
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.Trace(2U, new string[]
					{
						string.Concat(new object[]
						{
							" (POOL)  New connection pool created for: \"",
							opoConCtx.poolName,
							"\" (id: ",
							opoConCtx.conString.GetHashCode(),
							")\n"
						})
					});
					int num2;
					if (ConnectionDispenser.m_ConnectionPools == null)
					{
						num2 = 1;
					}
					else
					{
						num2 = ConnectionDispenser.m_ConnectionPools.Count;
					}
					OraTrace.Trace(2U, new string[]
					{
						" (POOL)  Total number of connection pools: " + num2.ToString() + "\n"
					});
				}
			}
			opoConCtx.pool.UpdateTotalCount(1, true);
			int num3 = 1;
			if (opoConCtx.bGridRac)
			{
				if (opoConCtx.minPoolSize > opoConCtx.pool.m_cpCtx.m_counter.potentialTotal)
				{
					num3 = opoConCtx.minPoolSize - opoConCtx.pool.m_cpCtx.m_counter.potentialTotal;
				}
			}
			else
			{
				num3 = opoConCtx.minPoolSize - opoConCtx.pool.m_counter.potentialTotal;
			}
			if (num3 > 0)
			{
				opoConCtx.pool.UpdatePotentialTotalCount(num3);
				ThreadPool.QueueUserWorkItem(new WaitCallback(opoConCtx.pool.PopulatePool), num3);
			}
			if (opoConCtx.metaPool == 1)
			{
				if (opoConCtx.m_bSelfTuning)
				{
					opoConCtx.m_conPooler = new ConPooler(ConPooler.DEFAULT_MAX_ELEMS_IN_POOL_TUNING_ON);
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
			return num;
		}

		// Token: 0x06000C71 RID: 3185 RVA: 0x0008071C File Offset: 0x0007F71C
		public unsafe static int CopyPooledConCtx(ref OpoConValCtx* dst, OpoConValCtx* src)
		{
			int num = 0;
			try
			{
				if (dst == (IntPtr)((UIntPtr)0))
				{
					num = OpsCon.AllocValCtx(ref dst);
				}
			}
			catch (Exception ex)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.TraceExceptionInfo(ex);
				}
				num = ErrRes.INT_ERR;
			}
			if (num != 0)
			{
				return num;
			}
			dst.InMtsTxn = src->InMtsTxn;
			dst.OSAuthent = src->OSAuthent;
			dst.Pooling = src->Pooling;
			dst.ServerAttach = src->ServerAttach;
			dst.SessionBegin = src->SessionBegin;
			dst.TxnHndAllocated = src->TxnHndAllocated;
			dst.SetIntAndExtName = src->SetIntAndExtName;
			dst.DBAPrivilege = src->DBAPrivilege;
			dst.registerHA = src->registerHA;
			dst.registerRLB = src->registerRLB;
			dst.bTAFEnabled = src->bTAFEnabled;
			dst.StmtCachePurge = src->StmtCachePurge;
			dst.StmtCacheSize = src->StmtCacheSize;
			dst.PSPE = src->PSPE;
			dst.MajorVersion = src->MajorVersion;
			dst.MinorVersion = src->MinorVersion;
			dst.PatchSetVersion = src->PatchSetVersion;
			dst.DbNtfPort = src->DbNtfPort;
			dst.ConSignature = src->ConSignature;
			dst.bIsTimesTen = src->bIsTimesTen;
			return num;
		}

		// Token: 0x06000C72 RID: 3186 RVA: 0x0008086C File Offset: 0x0007F86C
		public unsafe static int RegisterCallbacks(OpoConCtx opoConCtx)
		{
			int num = 0;
			bool flag = false;
			bool flag2 = false;
			if (!opoConCtx.bGridRac)
			{
				return 0;
			}
			string serverVersion = opoConCtx.opoConRefCtx.serverVersion;
			int num2 = serverVersion.IndexOf('.');
			int num3 = int.Parse(serverVersion.Substring(0, num2));
			if (num3 <= 10)
			{
				if (num3 != 10)
				{
					return 0;
				}
				int num4 = serverVersion.IndexOf('.', num2 + 1);
				int num5 = int.Parse(serverVersion.Substring(num2 + 1, num4 - (num2 + 1)));
				if (num5 < 2)
				{
					return 0;
				}
			}
			if (!ConnectionDispenser.m_bIsGlobalOCIEnvExists)
			{
				lock (ConnectionDispenser.s_lockObj)
				{
					if (!ConnectionDispenser.m_bIsGlobalOCIEnvExists)
					{
						try
						{
							num = OpsCon.InitSubscrEnv(ConnectionDispenser.m_HACallback, ConnectionDispenser.m_RLBCallback);
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
						if (num == 0)
						{
							ConnectionDispenser.m_bIsGlobalOCIEnvExists = true;
						}
					}
				}
			}
			if (!ConnectionDispenser.m_bIsGlobalOCIEnvExists)
			{
				return num;
			}
			if (opoConCtx.gridCR == 1 && ConnectionDispenser.m_htHAOpoConCtx[opoConCtx.opoConRefCtx.dbName] == null)
			{
				flag = true;
			}
			if (opoConCtx.gridRLB == 1 && ConnectionDispenser.m_htRLBOpoConCtx[opoConCtx.opoConRefCtx.serviceName] == null)
			{
				flag2 = true;
			}
			if (flag || flag2)
			{
				opoConCtx.pOpoConValCtx->registerHA = 0;
				opoConCtx.pOpoConValCtx->registerRLB = 0;
				OpoConCtx opoConCtx2 = (OpoConCtx)opoConCtx.Clone();
				if (flag)
				{
					opoConCtx2.pOpoConValCtx->registerHA = 1;
				}
				if (flag2)
				{
					opoConCtx2.pOpoConValCtx->registerRLB = 1;
				}
				opoConCtx2.pOpoConValCtx->Enlist = 0;
				opoConCtx2.pOpoConValCtx->SetIntAndExtName = 0;
				opoConCtx2.pOpoConValCtx->Pooling = 0;
				try
				{
					num = OpsCon.RegisterCallbacks(ref opoConCtx2.opsConCtx, ref opoConCtx2.opsErrCtx, opoConCtx2.pOpoConValCtx, ref opoConCtx2.opoConRefCtx);
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
				opoConCtx2.opoConRefCtx.dbName = opoConCtx.opoConRefCtx.dbName;
				opoConCtx2.opoConRefCtx.serviceName = opoConCtx.opoConRefCtx.serviceName;
				if (num == 0)
				{
					CallbackHashCtx callbackHashCtx = null;
					if (opoConCtx2.pOpoConValCtx->HASubscrHnd != IntPtr.Zero || opoConCtx2.pOpoConValCtx->RLBSubscrHnd != IntPtr.Zero)
					{
						callbackHashCtx = new CallbackHashCtx(opoConCtx2);
					}
					if (flag && opoConCtx2.pOpoConValCtx->HASubscrHnd == IntPtr.Zero)
					{
						opoConCtx.pOpoConValCtx->SessionBegin = opoConCtx2.pOpoConValCtx->reRegHAFailed;
						return 0;
					}
					opoConCtx2.pOpoConValCtx->reRegHAFailed = 0;
					if (flag2 && opoConCtx2.pOpoConValCtx->RLBSubscrHnd == IntPtr.Zero)
					{
						opoConCtx.pOpoConValCtx->SessionBegin = opoConCtx2.pOpoConValCtx->reRegRLBFailed;
						return 0;
					}
					opoConCtx2.pOpoConValCtx->reRegRLBFailed = 0;
					if (flag)
					{
						ConnectionDispenser.m_htHAOpoConCtx[opoConCtx.opoConRefCtx.dbName] = callbackHashCtx;
					}
					if (flag2)
					{
						ConnectionDispenser.m_htRLBOpoConCtx[opoConCtx.opoConRefCtx.serviceName] = callbackHashCtx;
					}
					if (flag && flag2)
					{
						callbackHashCtx.m_shared = true;
					}
				}
			}
			return num;
		}

		// Token: 0x06000C73 RID: 3187 RVA: 0x00080BC0 File Offset: 0x0007FBC0
		public unsafe static void ReRegisterCallbacks(object state)
		{
			int num = 0;
			try
			{
				IDictionaryEnumerator enumerator = ConnectionDispenser.m_htHAOpoConCtx.GetEnumerator();
				while (enumerator.MoveNext())
				{
					if (enumerator.Value != null)
					{
						OpoConCtx opoConCtxReg = ((CallbackHashCtx)enumerator.Value).m_opoConCtxReg;
						opoConCtxReg.pOpoConValCtx->registerHA = 1;
						if (((CallbackHashCtx)enumerator.Value).m_shared)
						{
							opoConCtxReg.pOpoConValCtx->registerRLB = 1;
						}
						else
						{
							opoConCtxReg.pOpoConValCtx->registerRLB = 0;
						}
						try
						{
							num = OpsCon.ReRegisterCallbacks(ref opoConCtxReg.opsConCtx, ref opoConCtxReg.opsErrCtx, opoConCtxReg.pOpoConValCtx, ref opoConCtxReg.opoConRefCtx);
						}
						catch (Exception ex)
						{
							if (OraTrace.m_TraceLevel != 0U)
							{
								OraTrace.TraceExceptionInfo(ex);
							}
							num = ErrRes.INT_ERR;
						}
						if (num == 0)
						{
							if (opoConCtxReg.pOpoConValCtx->HASubscrHnd == IntPtr.Zero)
							{
								opoConCtxReg.pOpoConValCtx->reRegHAFailed = 1;
							}
							else
							{
								opoConCtxReg.pOpoConValCtx->reRegHAFailed = 0;
							}
							if (opoConCtxReg.pOpoConValCtx->registerRLB == 1)
							{
								if (opoConCtxReg.pOpoConValCtx->RLBSubscrHnd == IntPtr.Zero)
								{
									opoConCtxReg.pOpoConValCtx->reRegRLBFailed = 1;
								}
								else
								{
									opoConCtxReg.pOpoConValCtx->reRegRLBFailed = 0;
								}
							}
						}
					}
				}
			}
			catch
			{
			}
			try
			{
				IDictionaryEnumerator enumerator2 = ConnectionDispenser.m_htRLBOpoConCtx.GetEnumerator();
				while (enumerator2.MoveNext())
				{
					if (enumerator2.Value != null)
					{
						OpoConCtx opoConCtxReg2 = ((CallbackHashCtx)enumerator2.Value).m_opoConCtxReg;
						opoConCtxReg2.pOpoConValCtx->registerHA = 0;
						if (!((CallbackHashCtx)enumerator2.Value).m_shared)
						{
							opoConCtxReg2.pOpoConValCtx->registerRLB = 1;
							try
							{
								num = OpsCon.ReRegisterCallbacks(ref opoConCtxReg2.opsConCtx, ref opoConCtxReg2.opsErrCtx, opoConCtxReg2.pOpoConValCtx, ref opoConCtxReg2.opoConRefCtx);
							}
							catch (Exception ex2)
							{
								if (OraTrace.m_TraceLevel != 0U)
								{
									OraTrace.TraceExceptionInfo(ex2);
								}
								num = ErrRes.INT_ERR;
							}
							if (num == 0)
							{
								if (opoConCtxReg2.pOpoConValCtx->RLBSubscrHnd == IntPtr.Zero)
								{
									opoConCtxReg2.pOpoConValCtx->reRegRLBFailed = 1;
								}
								else
								{
									opoConCtxReg2.pOpoConValCtx->reRegRLBFailed = 0;
								}
							}
						}
					}
				}
			}
			catch
			{
			}
		}

		// Token: 0x06000C74 RID: 3188 RVA: 0x00080E34 File Offset: 0x0007FE34
		public static int OnHACallback(OpoHACtx opoHACtx)
		{
			if (opoHACtx != null)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.Trace(16U, new string[]
					{
						string.Concat(new object[]
						{
							" (HA)    Event=",
							opoHACtx.eventType,
							";Database=",
							opoHACtx.dbName,
							";Service=",
							opoHACtx.serviceName,
							";Instance=",
							opoHACtx.instName,
							";Host=",
							opoHACtx.hostName,
							"\n"
						})
					});
				}
				Thread thread = new Thread(new ThreadStart(opoHACtx.Process));
				thread.Start();
			}
			return 0;
		}

		// Token: 0x06000C75 RID: 3189 RVA: 0x00080EF0 File Offset: 0x0007FEF0
		public static void HACallbackProcessing(object state)
		{
			int num = 0;
			ArrayList arrayList = new ArrayList(16);
			ConnectionPool connectionPool = null;
			OpoHACtx opoHACtx = (OpoHACtx)state;
			OracleHAEventArgs parameter = new OracleHAEventArgs(opoHACtx);
			Thread thread = new Thread(new ParameterizedThreadStart(OracleConnection.OnHAEvent));
			thread.Start(parameter);
			try
			{
				lock (ConnectionDispenser.s_haEventObj)
				{
					if (opoHACtx.eventType == HAEventType.ServiceDown)
					{
						RLBCtx rlbctx = null;
						lock (ConnectionDispenser.m_htSvcToRLB.SyncRoot)
						{
							rlbctx = (RLBCtx)ConnectionDispenser.m_htSvcToRLB[opoHACtx.serviceName];
							if (rlbctx != null)
							{
								ConnectionDispenser.m_htSvcToRLB.Remove(opoHACtx.serviceName);
							}
						}
						CPCtx[] array = null;
						int i = 0;
						if (rlbctx != null)
						{
							lock (rlbctx.htConToInst.SyncRoot)
							{
								IDictionaryEnumerator enumerator = rlbctx.htConToInst.GetEnumerator();
								array = new CPCtx[rlbctx.htConToInst.Count];
								while (enumerator.MoveNext())
								{
									array[i] = (CPCtx)enumerator.Value;
									array[i].m_timer.Dispose();
									i++;
								}
							}
						}
						if (array != null)
						{
							for (i = 0; i < array.Length; i++)
							{
								lock (array[i].htInstToCp.SyncRoot)
								{
									IDictionaryEnumerator enumerator2 = array[i].htInstToCp.GetEnumerator();
									while (enumerator2.MoveNext())
									{
										connectionPool = (ConnectionPool)enumerator2.Value;
										connectionPool.m_clonedCtx.lifeTime = new TimeSpan(1L);
										connectionPool.m_clonedCtx.minPoolSize = 0;
										connectionPool.m_clonedCtx.poolDecSize = connectionPool.m_clonedCtx.maxPoolSize;
										arrayList.Insert(num++, connectionPool);
									}
								}
							}
						}
					}
					else if (opoHACtx.eventType == HAEventType.ServiceMemberDown)
					{
						RLBCtx rlbctx2 = (RLBCtx)ConnectionDispenser.m_htSvcToRLB[opoHACtx.serviceName];
						if (rlbctx2 != null)
						{
							lock (rlbctx2)
							{
								rlbctx2.RLBMetricsList = null;
							}
							CPCtx[] array2 = null;
							int j = 0;
							lock (rlbctx2.htConToInst.SyncRoot)
							{
								IDictionaryEnumerator enumerator3 = rlbctx2.htConToInst.GetEnumerator();
								array2 = new CPCtx[rlbctx2.htConToInst.Count];
								while (enumerator3.MoveNext())
								{
									array2[j] = (CPCtx)enumerator3.Value;
									j++;
								}
							}
							if (array2 != null)
							{
								for (j = 0; j < array2.Length; j++)
								{
									connectionPool = (ConnectionPool)array2[j].htInstToCp[opoHACtx.instName];
									if (connectionPool != null)
									{
										lock (array2[j].htInstToCp.SyncRoot)
										{
											connectionPool = (ConnectionPool)array2[j].htInstToCp[opoHACtx.instName];
											if (connectionPool != null)
											{
												array2[j].htInstToCp.Remove(opoHACtx.instName);
											}
										}
										if (connectionPool != null)
										{
											connectionPool.m_clonedCtx.lifeTime = new TimeSpan(1L);
											connectionPool.m_clonedCtx.minPoolSize = 0;
											connectionPool.m_clonedCtx.poolDecSize = connectionPool.m_clonedCtx.maxPoolSize;
											arrayList.Insert(num++, connectionPool);
										}
									}
								}
							}
						}
					}
					else if (opoHACtx.eventType == HAEventType.NodeDown)
					{
						RLBCtx[] array3 = null;
						int k = 0;
						CPCtx[] array4 = null;
						int l = 0;
						lock (ConnectionDispenser.m_htSvcToRLB.SyncRoot)
						{
							IDictionaryEnumerator enumerator4 = ConnectionDispenser.m_htSvcToRLB.GetEnumerator();
							array3 = new RLBCtx[ConnectionDispenser.m_htSvcToRLB.Count];
							bool[] array5 = new bool[ConnectionDispenser.m_htSvcToRLB.Count];
							while (enumerator4.MoveNext())
							{
								array5[k] = false;
								array3[k] = (RLBCtx)enumerator4.Value;
								k++;
							}
						}
						if (array3 != null)
						{
							for (k = 0; k < array3.Length; k++)
							{
								lock (array3[k].htConToInst.SyncRoot)
								{
									IDictionaryEnumerator enumerator5 = array3[k].htConToInst.GetEnumerator();
									array4 = new CPCtx[array3[k].htConToInst.Count];
									l = 0;
									while (enumerator5.MoveNext())
									{
										array4[l] = (CPCtx)enumerator5.Value;
										l++;
									}
								}
								if (array4 != null)
								{
									for (l = 0; l < array4.Length; l++)
									{
										lock (array4[l].htInstToCp.SyncRoot)
										{
											ArrayList arrayList2 = new ArrayList();
											IDictionaryEnumerator enumerator6 = array4[l].htInstToCp.GetEnumerator();
											while (enumerator6.MoveNext())
											{
												connectionPool = (ConnectionPool)enumerator6.Value;
												if (connectionPool.m_clonedCtx.opoConRefCtx.hostName == opoHACtx.hostName)
												{
													arrayList2.Add(enumerator6.Key);
													connectionPool.m_clonedCtx.lifeTime = new TimeSpan(1L);
													connectionPool.m_clonedCtx.minPoolSize = 0;
													connectionPool.m_clonedCtx.poolDecSize = connectionPool.m_clonedCtx.maxPoolSize;
													arrayList.Insert(num++, connectionPool);
												}
											}
											if (arrayList2.Count > 0)
											{
												for (int m = 0; m < arrayList2.Count; m++)
												{
													array4[l].htInstToCp.Remove(arrayList2[m]);
												}
											}
										}
										if (array4[l].m_rlbCtx != null)
										{
											lock (array4[l].m_rlbCtx)
											{
												array4[l].m_rlbCtx.RLBMetricsList = null;
											}
										}
									}
								}
							}
						}
					}
					else if (opoHACtx.eventType == HAEventType.DatabaseDown)
					{
						RLBCtx[] array6 = null;
						int n = 0;
						CPCtx[] array7 = null;
						int num2 = 0;
						lock (ConnectionDispenser.m_htSvcToRLB.SyncRoot)
						{
							IDictionaryEnumerator enumerator7 = ConnectionDispenser.m_htSvcToRLB.GetEnumerator();
							array6 = new RLBCtx[ConnectionDispenser.m_htSvcToRLB.Count];
							bool[] array8 = new bool[ConnectionDispenser.m_htSvcToRLB.Count];
							while (enumerator7.MoveNext())
							{
								array8[n] = false;
								array6[n] = (RLBCtx)enumerator7.Value;
								n++;
							}
						}
						if (array6 != null)
						{
							for (n = 0; n < array6.Length; n++)
							{
								lock (array6[n].htConToInst.SyncRoot)
								{
									IDictionaryEnumerator enumerator8 = array6[n].htConToInst.GetEnumerator();
									array7 = new CPCtx[array6[n].htConToInst.Count];
									num2 = 0;
									while (enumerator8.MoveNext())
									{
										array7[num2] = (CPCtx)enumerator8.Value;
										num2++;
									}
								}
								if (array7 != null)
								{
									for (num2 = 0; num2 < array7.Length; num2++)
									{
										lock (array7[num2].htInstToCp.SyncRoot)
										{
											ArrayList arrayList3 = new ArrayList();
											IDictionaryEnumerator enumerator9 = array7[num2].htInstToCp.GetEnumerator();
											while (enumerator9.MoveNext())
											{
												connectionPool = (ConnectionPool)enumerator9.Value;
												if (connectionPool.m_clonedCtx.opoConRefCtx.dbName == opoHACtx.dbName)
												{
													arrayList3.Add(enumerator9.Key);
													connectionPool.m_clonedCtx.lifeTime = new TimeSpan(1L);
													connectionPool.m_clonedCtx.minPoolSize = 0;
													connectionPool.m_clonedCtx.poolDecSize = connectionPool.m_clonedCtx.maxPoolSize;
													arrayList.Insert(num++, connectionPool);
												}
											}
											if (arrayList3.Count > 0)
											{
												for (int num3 = 0; num3 < arrayList3.Count; num3++)
												{
													array7[num2].htInstToCp.Remove(arrayList3[num3]);
												}
											}
										}
										if (array7[num2].m_rlbCtx != null)
										{
											lock (array7[num2].m_rlbCtx)
											{
												array7[num2].m_rlbCtx.RLBMetricsList = null;
											}
										}
									}
								}
							}
						}
					}
				}
				if (arrayList != null)
				{
					num = 0;
					while (num < arrayList.Count && arrayList[num] != null)
					{
						((ConnectionPool)arrayList[num]).RegulateNumOfCons(-1);
						num++;
					}
				}
			}
			catch
			{
			}
		}

		// Token: 0x06000C76 RID: 3190 RVA: 0x00081974 File Offset: 0x00080974
		public static int OnRLBCallback(OpoRLBCtx opoRLBCtx)
		{
			int result = 0;
			string text;
			CaptureCollection captureCollection;
			CaptureCollection captureCollection2;
			CaptureCollection captureCollection3;
			string text2;
			RLBMsgStatus rlbmsgStatus = ConnectionDispenser.ParseRLBMessage(opoRLBCtx.metrics, out text, out captureCollection, out captureCollection2, out captureCollection3, out text2);
			if (OraTrace.m_TraceLevel != 0U)
			{
				OraTrace.Trace(32U, new string[]
				{
					" (RLB)   Message Received=" + opoRLBCtx.metrics + "\n"
				});
				OraTrace.Trace(32U, new string[]
				{
					" (RLB)   Message Status=" + rlbmsgStatus + "\n"
				});
			}
			lock (ConnectionDispenser.m_htSvcToRLB.SyncRoot)
			{
				RLBCtx rlbctx = null;
				if (text != null)
				{
					rlbctx = (RLBCtx)ConnectionDispenser.m_htSvcToRLB[text];
				}
				else
				{
					OraTrace.Trace(32U, new string[]
					{
						" (RLB)   Message Not Processed=" + opoRLBCtx.metrics + "\n"
					});
				}
				if (rlbctx != null && rlbmsgStatus == RLBMsgStatus.GOOD && string.Compare(text2, rlbctx.timeStamp) > 0)
				{
					if (OraTrace.m_TraceLevel != 0U)
					{
						OraTrace.Trace(32U, new string[]
						{
							string.Concat(new string[]
							{
								" (RLB)   Service=",
								text,
								";Timestamp=",
								text2,
								"\n"
							})
						});
						for (int i = 0; i < captureCollection.Count; i++)
						{
							OraTrace.Trace(32U, new string[]
							{
								string.Concat(new string[]
								{
									" (RLB)   Instance=",
									captureCollection[i].Value.ToLower(),
									";Percentage=",
									captureCollection2[i].Value,
									";Flag=",
									captureCollection3[i].Value,
									"\n"
								})
							});
						}
					}
					rlbctx.timeStamp = text2;
					ArrayList arrayList = new ArrayList();
					double num = 0.0;
					double num2 = 0.0;
					double val = 0.0;
					for (int j = 0; j < captureCollection.Count; j++)
					{
						RLBMetricsFlag metricsEnum = ConnectionDispenser.GetMetricsEnum(captureCollection3[j].Value);
						double num3 = Convert.ToDouble(captureCollection2[j].Value);
						arrayList.Add(new RLBMetrics(captureCollection[j].Value.ToLower(), num3, (num3 == 0.0) ? 1073741822 : ((int)(1000.0 / num3)), metricsEnum));
						val = Math.Max(val, num3);
						num += num3;
						if (captureCollection.Count > 2)
						{
							num2 += Math.Pow(num3 - (double)(100 / captureCollection.Count), 2.0);
						}
					}
					if (num < 105.0 && num > 95.0 && rlbctx.ServiceName == text)
					{
						arrayList.Sort(ConnectionDispenser.s_metricsComparer);
						if (rlbctx.RLBMetricsList != null && arrayList.Count < rlbctx.RLBMetricsList.Count)
						{
							for (int k = 0; k < rlbctx.RLBMetricsList.Count; k++)
							{
								bool flag2 = false;
								for (int l = 0; l < arrayList.Count; l++)
								{
									if (((RLBMetrics)arrayList[l]).InstanceName == ((RLBMetrics)rlbctx.RLBMetricsList[k]).InstanceName)
									{
										flag2 = true;
										break;
									}
								}
								if (!flag2)
								{
									OraTrace.Trace(32U, new string[]
									{
										" (GRID) (RLB) (GRAV) (gravitation due to change in # of entries in RLB msg"
									});
									ConnectionDispenser.RLBGravitate(rlbctx.ServiceName, ((RLBMetrics)rlbctx.RLBMetricsList[k]).InstanceName);
								}
							}
						}
						if (rlbctx.RLBMetricsList != null)
						{
							lock (rlbctx)
							{
								rlbctx.RLBMetricsList = arrayList;
								goto IL_426;
							}
						}
						rlbctx.RLBMetricsList = arrayList;
						IL_426:
						rlbctx.bNeedNormalization = true;
					}
					else
					{
						lock (rlbctx)
						{
							rlbctx.RLBMetricsList = null;
						}
					}
				}
			}
			return result;
		}

		// Token: 0x06000C77 RID: 3191 RVA: 0x00081E34 File Offset: 0x00080E34
		public static void RLBGravitate(string ServiceName)
		{
			if (ConnectionDispenser.m_htSvcToRLB[ServiceName] != null)
			{
				int num = 0;
				RLBCtx rlbctx = (RLBCtx)ConnectionDispenser.m_htSvcToRLB[ServiceName];
				lock (rlbctx.htConToInst.SyncRoot)
				{
					IDictionaryEnumerator enumerator = rlbctx.htConToInst.GetEnumerator();
					while (enumerator.MoveNext())
					{
						CPCtx cpctx = (CPCtx)enumerator.Value;
						num += (int)(0.15f * (float)cpctx.m_counter.total);
						ConnectionDispenser.RLBGravitate(ServiceName, true, num);
					}
				}
			}
		}

		// Token: 0x06000C78 RID: 3192 RVA: 0x00081ED8 File Offset: 0x00080ED8
		public static void RLBGravitate(string ServiceName, string InstanceName)
		{
			int num = 0;
			ConnectionPool connectionPool = null;
			RLBCtx rlbctx = (RLBCtx)ConnectionDispenser.m_htSvcToRLB[ServiceName];
			if (rlbctx != null)
			{
				lock (rlbctx.htConToInst.SyncRoot)
				{
					IDictionaryEnumerator enumerator = rlbctx.htConToInst.GetEnumerator();
					while (enumerator.MoveNext())
					{
						CPCtx cpctx = (CPCtx)enumerator.Value;
						connectionPool = (ConnectionPool)cpctx.htInstToCp[InstanceName];
						if (connectionPool != null)
						{
							num = (int)(0.25f * (float)connectionPool.m_cpCtx.m_counter.total);
							if (num > 0)
							{
								lock (connectionPool)
								{
									connectionPool.m_rlbGravCounter += num;
								}
								int num2 = connectionPool.RegulateNumOfCons(num);
								connectionPool.UpdatePotentialTotalCount(num2);
								ThreadPool.QueueUserWorkItem(new WaitCallback(connectionPool.PopulatePool), num2);
								if ((OraTrace.m_TraceLevel & 32U) == 32U)
								{
									StringBuilder stringBuilder = new StringBuilder();
									stringBuilder.Append(" (GRID) (RLB) (GRAV) (inst=");
									stringBuilder.Append(InstanceName);
									stringBuilder.Append(") (rebalancing ");
									stringBuilder.Append(num2);
									stringBuilder.Append(" connections)\n");
									OraTrace.Trace(32U, new string[]
									{
										stringBuilder.ToString()
									});
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x06000C79 RID: 3193 RVA: 0x00082080 File Offset: 0x00081080
		public static void RLBGravitate(string ServiceName, bool incrementPool, int decrSize)
		{
			if (decrSize == 0)
			{
				return;
			}
			int num = 0;
			RLBCtx rlbctx = (RLBCtx)ConnectionDispenser.m_htSvcToRLB[ServiceName];
			if (rlbctx != null)
			{
				lock (rlbctx)
				{
					if (rlbctx.RLBMetricsList != null && rlbctx.RLBMetricsList.Count > 0)
					{
						lock (rlbctx.htConToInst.SyncRoot)
						{
							RLBMetrics rlbmetrics = null;
							int[] array = new int[rlbctx.RLBMetricsList.Count];
							int[] array2 = new int[rlbctx.RLBMetricsList.Count];
							ConnectionPool[] array3 = new ConnectionPool[rlbctx.RLBMetricsList.Count];
							int[] array4 = new int[rlbctx.RLBMetricsList.Count];
							IDictionaryEnumerator enumerator = rlbctx.htConToInst.GetEnumerator();
							while (enumerator.MoveNext())
							{
								ConnectionPool connectionPool = null;
								int num2 = 0;
								int num3 = 0;
								int num4 = 0;
								CPCtx cpctx = (CPCtx)enumerator.Value;
								lock (cpctx.htInstToCp.SyncRoot)
								{
									IDictionaryEnumerator enumerator2 = cpctx.htInstToCp.GetEnumerator();
									while (enumerator2.MoveNext())
									{
										connectionPool = (ConnectionPool)enumerator2.Value;
										int i = 0;
										while (i < rlbctx.RLBMetricsList.Count && !(((RLBMetrics)rlbctx.RLBMetricsList[i]).InstanceName == connectionPool.m_clonedCtx.opoConRefCtx.instanceName))
										{
											i++;
										}
										if (i >= rlbctx.RLBMetricsList.Count)
										{
											return;
										}
										array3[i] = connectionPool;
										array2[i] = Math.Min(connectionPool.m_connections.Count, connectionPool.m_counter.total - connectionPool.m_clonedCtx.minPoolSize);
										num2 += array2[i];
										num3 += ((RLBMetrics)rlbctx.RLBMetricsList[i]).MaxDistribFreq;
										array4[i] = array3[i].m_connections.Count;
										num4 += array4[i];
										i++;
									}
									int num5 = 0;
									int num6 = 0;
									int num7 = 0;
									for (int i = 0; i < rlbctx.RLBMetricsList.Count; i++)
									{
										rlbmetrics = (RLBMetrics)rlbctx.RLBMetricsList[i];
										if (rlbmetrics.Flag == RLBMetricsFlag.BLOCKED || rlbmetrics.Flag == RLBMetricsFlag.VIOLATING)
										{
											num5 += array2[i];
											num6++;
										}
									}
									if (num5 >= decrSize)
									{
										int num8 = 0;
										for (int i = 0; i < rlbctx.RLBMetricsList.Count; i++)
										{
											array[i] = 0;
											if (rlbmetrics.Flag == RLBMetricsFlag.BLOCKED || rlbmetrics.Flag == RLBMetricsFlag.VIOLATING)
											{
												num8++;
											}
										}
										for (int i = 0; i < rlbctx.RLBMetricsList.Count; i++)
										{
											rlbmetrics = (RLBMetrics)rlbctx.RLBMetricsList[i];
											if (rlbmetrics.Flag == RLBMetricsFlag.BLOCKED || rlbmetrics.Flag == RLBMetricsFlag.VIOLATING)
											{
												if (array2[i] <= array3[i].m_clonedCtx.poolIncSize)
												{
													array[i] = 0;
												}
												else
												{
													array[i] = Math.Min(array2[i], num5 / num8);
												}
											}
										}
										for (int i = 0; i < rlbctx.RLBMetricsList.Count; i++)
										{
											if (array3[i] != null && array[i] > 0)
											{
												array3[i].RegulateNumOfCons(array[i]);
												num7 += array2[i];
											}
										}
									}
									else
									{
										for (int i = 0; i < rlbctx.RLBMetricsList.Count; i++)
										{
											array[i] = 0;
											rlbmetrics = (RLBMetrics)rlbctx.RLBMetricsList[i];
											if (rlbmetrics.Flag == RLBMetricsFlag.BLOCKED || rlbmetrics.Flag == RLBMetricsFlag.VIOLATING)
											{
												array[i] = array2[i];
												num7 += array2[i];
											}
										}
										for (int i = 0; i < rlbctx.RLBMetricsList.Count; i++)
										{
											rlbmetrics = (RLBMetrics)rlbctx.RLBMetricsList[i];
											if (rlbmetrics.Flag != RLBMetricsFlag.BLOCKED && rlbmetrics.Flag != RLBMetricsFlag.VIOLATING)
											{
												if (array3[i] == null)
												{
													array[i] = 0;
												}
												else if (array2[i] <= array3[i].m_clonedCtx.poolIncSize)
												{
													array[i] = 0;
												}
												else if (Math.Abs((double)(array4[i] * 100 / num4) - rlbmetrics.Percentage) <= 5.0)
												{
													array[i] = 0;
												}
												else
												{
													array[i] = (int)Math.Min((double)(decrSize - num7), (double)(decrSize * rlbmetrics.MaxDistribFreq) / (double)num3);
												}
											}
										}
										for (int i = 0; i < rlbctx.RLBMetricsList.Count; i++)
										{
											if (array3[i] != null && array[i] > 0)
											{
												num += array3[i].RegulateNumOfCons(array[i]);
												num7 += array[i];
											}
										}
									}
								}
								if (num > 0)
								{
									if (incrementPool)
									{
										connectionPool.UpdatePotentialTotalCount(num);
										connectionPool.PopulatePool(num);
									}
									if ((OraTrace.m_TraceLevel & 32U) == 32U)
									{
										StringBuilder stringBuilder = new StringBuilder();
										stringBuilder.Append(" (GRID) (RLB) (GRAV) (svc=");
										stringBuilder.Append(ServiceName);
										stringBuilder.Append(") (rebalanced ");
										stringBuilder.Append(num);
										stringBuilder.Append(" connections)\n");
										OraTrace.Trace(32U, new string[]
										{
											stringBuilder.ToString()
										});
									}
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x06000C7A RID: 3194 RVA: 0x00082668 File Offset: 0x00081668
		private static RLBMsgStatus ParseRLBMessage(string message, out string service, out CaptureCollection instColl, out CaptureCollection percColl, out CaptureCollection flagColl, out string timestamp)
		{
			RLBMsgStatus result = RLBMsgStatus.GOOD;
			if (message != null && message.Length != 0)
			{
				Regex regex = new Regex(ConnectionDispenser.s_pattern, RegexOptions.IgnoreCase | RegexOptions.Singleline);
				Match match = regex.Match(message);
				service = match.Groups["svc"].Value.ToLower();
				instColl = match.Groups["inst"].Captures;
				percColl = match.Groups["perc"].Captures;
				flagColl = match.Groups["flag"].Captures;
				timestamp = match.Groups["ts"].Value;
				if (instColl.Count == 0 || percColl.Count == 0 || flagColl.Count == 0 || match.Groups["svc"].Captures.Count == 0)
				{
					result = RLBMsgStatus.BAD_FORMAT;
				}
				else if (instColl.Count != percColl.Count || percColl.Count != flagColl.Count)
				{
					result = RLBMsgStatus.MISSING_PAIR;
				}
				else if (service != null && service.Length == 0)
				{
					result = RLBMsgStatus.MISSING_SVC;
				}
				else if (timestamp != null && timestamp.Length == 0)
				{
					timestamp = "Unknown";
				}
			}
			else
			{
				result = RLBMsgStatus.EMPTY;
				service = null;
				instColl = null;
				percColl = null;
				flagColl = null;
				timestamp = null;
			}
			return result;
		}

		// Token: 0x06000C7B RID: 3195 RVA: 0x000827B8 File Offset: 0x000817B8
		internal static RLBMetricsFlag GetMetricsEnum(string s)
		{
			string[] names = Enum.GetNames(typeof(RLBMetricsFlag));
			RLBMetricsFlag result = RLBMetricsFlag.UNKNOWN;
			for (int i = 0; i < names.Length; i++)
			{
				if (string.Compare(s, names[i]) == 0)
				{
					result = (RLBMetricsFlag)i;
				}
			}
			return result;
		}

		// Token: 0x06000C7C RID: 3196 RVA: 0x000827F4 File Offset: 0x000817F4
		public static void ClearPool(OpoConCtx opoConCtx, bool bInvalidOnly, bool bRefresh)
		{
			if (opoConCtx.bGridRac)
			{
				string key = (string)ConnectionDispenser.m_htTnsToSvc[opoConCtx.dataSrc];
				RLBCtx rlbctx = (RLBCtx)ConnectionDispenser.m_htSvcToRLB[key];
				CPCtx cpctx = (CPCtx)rlbctx.htConToInst[opoConCtx.conString];
				lock (cpctx.htInstToCp.SyncRoot)
				{
					IDictionaryEnumerator enumerator = cpctx.htInstToCp.GetEnumerator();
					while (enumerator.MoveNext())
					{
						ConnectionPool connectionPool = (ConnectionPool)enumerator.Value;
						connectionPool.ClearPool(bInvalidOnly, bRefresh);
					}
					return;
				}
			}
			opoConCtx.pool.ClearPool(bInvalidOnly, bRefresh);
		}

		// Token: 0x06000C7D RID: 3197 RVA: 0x000828C0 File Offset: 0x000818C0
		public static void ClearAllPools()
		{
			if (ConnectionDispenser.m_htSvcToRLB != null && 0 < ConnectionDispenser.m_htSvcToRLB.Count)
			{
				lock (ConnectionDispenser.m_htSvcToRLB.SyncRoot)
				{
					IDictionaryEnumerator enumerator = ConnectionDispenser.m_htSvcToRLB.GetEnumerator();
					while (enumerator.MoveNext())
					{
						RLBCtx rlbctx = (RLBCtx)enumerator.Value;
						lock (rlbctx.htConToInst.SyncRoot)
						{
							IDictionaryEnumerator enumerator2 = rlbctx.htConToInst.GetEnumerator();
							while (enumerator2.MoveNext())
							{
								CPCtx cpctx = (CPCtx)enumerator2.Value;
								lock (cpctx.htInstToCp.SyncRoot)
								{
									IDictionaryEnumerator enumerator3 = cpctx.htInstToCp.GetEnumerator();
									while (enumerator3.MoveNext())
									{
										ConnectionPool connectionPool = (ConnectionPool)enumerator3.Value;
										connectionPool.ClearPool(false, false);
									}
								}
							}
						}
					}
				}
			}
			if (ConnectionDispenser.m_ConnectionPools != null && ConnectionDispenser.m_ConnectionPools.Count > 0)
			{
				IDictionaryEnumerator enumerator4 = ConnectionDispenser.m_ConnectionPools.GetEnumerator();
				while (enumerator4.MoveNext())
				{
					ConnectionPool connectionPool2 = (ConnectionPool)enumerator4.Value;
					if (connectionPool2 != null)
					{
						connectionPool2.ClearPool(false, false);
					}
				}
			}
		}

		// Token: 0x040009CE RID: 2510
		internal static Hashtable m_ConnectionPools;

		// Token: 0x040009CF RID: 2511
		internal static Hashtable m_pspePrimaryResources;

		// Token: 0x040009D0 RID: 2512
		internal static Hashtable m_htTnsToSvc;

		// Token: 0x040009D1 RID: 2513
		internal static Hashtable m_htSvcToRLB;

		// Token: 0x040009D2 RID: 2514
		internal static int m_totalNumberOfConnectionPools;

		// Token: 0x040009D3 RID: 2515
		internal static Random m_random;

		// Token: 0x040009D4 RID: 2516
		internal static Hashtable m_htHAOpoConCtx;

		// Token: 0x040009D5 RID: 2517
		internal static Hashtable m_htRLBOpoConCtx;

		// Token: 0x040009D6 RID: 2518
		internal static int m_iteration;

		// Token: 0x040009D7 RID: 2519
		internal static OraHACallbackFuncPtr m_HACallback;

		// Token: 0x040009D8 RID: 2520
		internal static OraRLBCallbackFuncPtr m_RLBCallback;

		// Token: 0x040009D9 RID: 2521
		internal static bool m_bIsGlobalOCIEnvExists;

		// Token: 0x040009DA RID: 2522
		internal static string s_pattern = "service\\s*=\\s*(?<svc>.*?)\\s*{\\s*(\\s*{\\s*instance\\s*=\\s*(?<inst>.*?)\\s+percent\\s*=\\s*(?<perc>.*?)\\s+flag\\s*=\\s*(?<flag>.*?)\\s*}\\s*)*\\s*}\\s*timestamp\\s*=\\s*(?<ts>.*?)\\s*\\Z";

		// Token: 0x040009DB RID: 2523
		internal static RLBMetricsComparer s_metricsComparer = new RLBMetricsComparer();

		// Token: 0x040009DC RID: 2524
		internal static int REQ_ATTEMPTS_FOR_GRAV_PER_INSTANCE = 1000;

		// Token: 0x040009DD RID: 2525
		internal static object s_lockObj = new object();

		// Token: 0x040009DE RID: 2526
		internal static object s_haEventObj = new object();
	}
}
