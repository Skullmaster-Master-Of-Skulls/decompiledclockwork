using System;
using System.Collections;
using System.Text;
using System.Threading;

namespace Oracle.DataAccess.Client
{
	// Token: 0x0200013B RID: 315
	internal class CPCtx
	{
		// Token: 0x170001F8 RID: 504
		// (get) Token: 0x06000CA0 RID: 3232 RVA: 0x00082E58 File Offset: 0x00081E58
		// (set) Token: 0x06000CA1 RID: 3233 RVA: 0x00082E60 File Offset: 0x00081E60
		public Hashtable htInstToCp
		{
			get
			{
				return this.m_htInstToCp;
			}
			set
			{
				this.m_htInstToCp = value;
			}
		}

		// Token: 0x06000CA2 RID: 3234 RVA: 0x00082E6C File Offset: 0x00081E6C
		public CPCtx(int maxPoolSize, RLBCtx rlbCtx, int poolRegulator)
		{
			this.m_htInstToCp = Hashtable.Synchronized(new Hashtable());
			this.m_counter = new Counter(true);
			this.m_rlbCtx = rlbCtx;
			this.m_random = new Random();
			this.m_htTxnIdToIntance = Hashtable.Synchronized(new Hashtable());
			this.m_cpCtxSkipDecrement = true;
			this.m_timer = new Timer(new TimerCallback(this.RegulateNumOfConsThreadFunc), null, poolRegulator * 1000, poolRegulator * 1000);
			try
			{
				this.m_semCPCtxAvaNumOfCons = OpsCon.CreateSemaphore(IntPtr.Zero, 0, maxPoolSize, "");
			}
			catch (Exception ex)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.TraceExceptionInfo(ex);
				}
				throw;
			}
		}

		// Token: 0x06000CA3 RID: 3235 RVA: 0x00082F24 File Offset: 0x00081F24
		protected override void Finalize()
		{
			try
			{
				try
				{
					if (this.m_semCPCtxAvaNumOfCons != IntPtr.Zero)
					{
						try
						{
							OpsCon.CloseHandle(this.m_semCPCtxAvaNumOfCons);
						}
						catch (Exception ex)
						{
							if (OraTrace.m_TraceLevel != 0U)
							{
								OraTrace.TraceExceptionInfo(ex);
							}
						}
						this.m_semCPCtxAvaNumOfCons = IntPtr.Zero;
					}
				}
				catch
				{
				}
				if (this.m_timer != null)
				{
					this.m_timer.Dispose();
					this.m_timer = null;
				}
			}
			finally
			{
				base.Finalize();
			}
		}

		// Token: 0x06000CA4 RID: 3236 RVA: 0x00082FBC File Offset: 0x00081FBC
		public void RegulateNumOfConsThreadFunc(object state)
		{
			if (this.m_htInstToCp != null && this.m_htInstToCp.Count > 0)
			{
				lock (this.m_htInstToCp.SyncRoot)
				{
					if (this.m_htInstToCp.Count > 0)
					{
						int num = this.m_random.Next(1, this.htInstToCp.Count + 1);
						int num2 = 0;
						IDictionaryEnumerator enumerator = this.m_htInstToCp.GetEnumerator();
						while (enumerator.MoveNext())
						{
							num2++;
							if (num2 == num)
							{
								break;
							}
						}
						ConnectionPool connectionPool = (ConnectionPool)enumerator.Value;
						connectionPool.RegulateNumOfCons(state);
					}
				}
			}
		}

		// Token: 0x06000CA5 RID: 3237 RVA: 0x0008307C File Offset: 0x0008207C
		public int GetConnection(OpoConCtx opoConCtx)
		{
			DateTime now = DateTime.Now;
			int num = 0;
			bool flag = false;
			bool flag2 = false;
			bool flag3 = false;
			string key = (string)ConnectionDispenser.m_htTnsToSvc[opoConCtx.opoConRefCtx.dataSource];
			RLBCtx rlbctx = (RLBCtx)ConnectionDispenser.m_htSvcToRLB[key];
			int result;
			try
			{
				bool flag4 = false;
				if (opoConCtx.gridRLB == 1 && opoConCtx.pool != null && rlbctx != null && rlbctx.RLBMetricsList != null && rlbctx.RLBMetricsList.Count > 0)
				{
					lock (rlbctx)
					{
						if (rlbctx.RLBMetricsList == null || rlbctx.RLBMetricsList.Count <= 0)
						{
							flag4 = true;
							goto IL_A31;
						}
						int i = 0;
						bool flag6 = false;
						if (!rlbctx.bNeedNormalization)
						{
							goto IL_2A2;
						}
						CPCtx cpctx = (CPCtx)rlbctx.htConToInst[opoConCtx.conString];
						rlbctx.NormalizeCounters(rlbctx, cpctx);
						if (rlbctx.RLBMetricsList.Count < 2)
						{
							goto IL_2A2;
						}
						int num2 = 0;
						float num3 = 0f;
						int count = rlbctx.RLBMetricsList.Count;
						float[] array = new float[count];
						ConnectionPool[] array2 = new ConnectionPool[count];
						lock (rlbctx.htConToInst.SyncRoot)
						{
							IDictionaryEnumerator enumerator = rlbctx.htConToInst.GetEnumerator();
							while (enumerator.MoveNext())
							{
								cpctx = (CPCtx)enumerator.Value;
								lock (cpctx.htInstToCp.SyncRoot)
								{
									IDictionaryEnumerator enumerator2 = cpctx.htInstToCp.GetEnumerator();
									while (enumerator2.MoveNext())
									{
										if (num2 < count)
										{
											array2[num2] = (ConnectionPool)enumerator2.Value;
											num3 += array2[num2].m_attemptedRequests;
											array[num2] = array2[num2].m_attemptedRequests;
											num2++;
										}
									}
								}
							}
							goto IL_2A2;
						}
						IL_1F0:
						int num4 = int.MaxValue;
						for (i = 0; i < rlbctx.RLBMetricsList.Count; i++)
						{
							if (((RLBMetrics)rlbctx.RLBMetricsList[i]).CurDistribFreq == 0)
							{
								flag6 = true;
								break;
							}
							if (i < rlbctx.RLBMetricsList.Count)
							{
								num4 = Math.Min(num4, ((RLBMetrics)rlbctx.RLBMetricsList[i]).CurDistribFreq);
							}
						}
						if (!flag6)
						{
							for (i = 0; i < rlbctx.RLBMetricsList.Count; i++)
							{
								((RLBMetrics)rlbctx.RLBMetricsList[i]).CurDistribFreq -= num4;
							}
						}
						IL_2A2:
						if (flag6)
						{
							DateTime now2 = DateTime.Now;
							bool flag9 = true;
							bool[] array3 = new bool[rlbctx.RLBMetricsList.Count];
							for (;;)
							{
								if (i >= rlbctx.RLBMetricsList.Count)
								{
									i = this.m_random.Next(0, rlbctx.RLBMetricsList.Count);
								}
								opoConCtx.pool = (ConnectionPool)this.htInstToCp[((RLBMetrics)rlbctx.RLBMetricsList[i]).InstanceName];
								if (opoConCtx.pool != null && opoConCtx.pool.m_counter.potentialTotal != 0)
								{
									TimeSpan timeSpan = opoConCtx.timeOut - (DateTime.Now - now);
									if (timeSpan.TotalSeconds > 0.0)
									{
										this.m_counter.UpdateThreadWaitCount(opoConCtx.pool, 1);
										num = OpsCon.WaitForSingleObject(this.m_semCPCtxAvaNumOfCons, (int)timeSpan.TotalSeconds * 1000);
										this.m_counter.UpdateThreadWaitCount(opoConCtx.pool, -1);
									}
									else
									{
										num = -1;
									}
									if (num != 0)
									{
										break;
									}
									num = opoConCtx.pool.GetConnection(opoConCtx);
									if (num == 0)
									{
										goto Block_49;
									}
									int num5 = 0;
									OpsCon.ReleaseSemaphore(this.m_semCPCtxAvaNumOfCons, 1, ref num5);
								}
								if (flag9 && i != 0)
								{
									i = rlbctx.RLBMetricsList.Count - 1;
									flag9 = false;
								}
								else
								{
									array3[i] = true;
									int maxValue = int.MaxValue;
									int num6 = rlbctx.RLBMetricsList.Count;
									for (int j = 0; j < rlbctx.RLBMetricsList.Count; j++)
									{
										if (!array3[j])
										{
											if (((RLBMetrics)rlbctx.RLBMetricsList[j]).CurDistribFreq == 0)
											{
												num6 = j;
												break;
											}
											if (((RLBMetrics)rlbctx.RLBMetricsList[j]).CurDistribFreq < maxValue)
											{
												num6 = j;
											}
										}
									}
									i = num6;
								}
								if (DateTime.Now - now >= opoConCtx.timeOut)
								{
									goto Block_73;
								}
							}
							return ErrRes.CON_TIMEOUT_EXCEEDED;
							Block_49:
							opoConCtx.pool.m_attemptedRequests += 1f;
							int num7 = 0;
							while (num7 < rlbctx.RLBMetricsList.Count && !(((RLBMetrics)rlbctx.RLBMetricsList[num7]).InstanceName == opoConCtx.opoConRefCtx.instanceName))
							{
								num7++;
							}
							ConnectionPool connectionPool = null;
							StringBuilder stringBuilder = null;
							if ((OraTrace.m_TraceLevel & 32U) == 32U)
							{
								stringBuilder = new StringBuilder();
								stringBuilder.Append(" (GRID) (RLB) (DISP) (inst=");
								stringBuilder.Append(opoConCtx.opoConRefCtx.instanceName);
								stringBuilder.Append(") ");
								for (int k = 0; k < rlbctx.RLBMetricsList.Count; k++)
								{
									stringBuilder.Append("(");
									stringBuilder.Append(((RLBMetrics)rlbctx.RLBMetricsList[k]).InstanceName);
									connectionPool = (ConnectionPool)this.htInstToCp[((RLBMetrics)rlbctx.RLBMetricsList[k]).InstanceName];
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
									stringBuilder.Append(((RLBMetrics)rlbctx.RLBMetricsList[k]).CurDistribFreq);
									stringBuilder.Append("/");
									stringBuilder.Append(((RLBMetrics)rlbctx.RLBMetricsList[k]).MaxDistribFreq);
									stringBuilder.Append(") ");
								}
								stringBuilder.Append(") ");
							}
							if (num7 < rlbctx.RLBMetricsList.Count)
							{
								if (((RLBMetrics)rlbctx.RLBMetricsList[num7]).CurDistribFreq != 0)
								{
									connectionPool = (ConnectionPool)this.htInstToCp[((RLBMetrics)rlbctx.RLBMetricsList[num7]).InstanceName];
									connectionPool.m_cpCtx.m_dispMiss++;
									connectionPool.UpdatePotentialTotalCount(connectionPool.m_clonedCtx.poolIncSize);
									ThreadPool.QueueUserWorkItem(new WaitCallback(connectionPool.PopulatePool), connectionPool.m_clonedCtx.poolIncSize);
									string value = null;
									float num8 = float.MinValue;
									ConnectionPool connectionPool2 = null;
									if (connectionPool.m_cpCtx.m_dispMiss >= CPCtx.MAX_MISS_COUNT)
									{
										connectionPool.m_cpCtx.m_dispMiss = 0;
										int total = connectionPool.m_cpCtx.m_counter.total;
										if (total > 0)
										{
											for (int l = 0; l < rlbctx.RLBMetricsList.Count; l++)
											{
												string instanceName = ((RLBMetrics)rlbctx.RLBMetricsList[l]).InstanceName;
												int num9 = (int)((RLBMetrics)rlbctx.RLBMetricsList[l]).Percentage;
												ConnectionPool connectionPool3 = connectionPool.m_cpCtx.htInstToCp[instanceName] as ConnectionPool;
												if (connectionPool3 != null)
												{
													int num10 = connectionPool3.m_counter.total * 100 / total;
													float num11 = (float)(num10 - num9);
													if (num11 > num8)
													{
														value = instanceName;
														num8 = num11;
														connectionPool2 = connectionPool3;
													}
												}
											}
											if (connectionPool2 != null)
											{
												int total2 = connectionPool2.m_cpCtx.m_counter.total;
												if (total2 > 0)
												{
													int num12 = (int)(num8 / (float)(CPCtx.GRAV_FACTOR * 100) * (float)total2);
													if (num12 >= 1)
													{
														lock (connectionPool2)
														{
															connectionPool2.m_rlbGravCounter += num12;
														}
														ThreadPool.QueueUserWorkItem(new WaitCallback(connectionPool2.RegulateNumOfConsThreadFunc), num12);
														connectionPool2.UpdatePotentialTotalCount(num12);
														ThreadPool.QueueUserWorkItem(new WaitCallback(connectionPool.PopulatePool), num12);
														if ((OraTrace.m_TraceLevel & 32U) == 32U)
														{
															stringBuilder.Append("(GRAV) (");
															stringBuilder.Append(value);
															stringBuilder.Append(" = ");
															stringBuilder.Append(num12);
															stringBuilder.Append(" cons gravitating due to misses)");
														}
													}
												}
											}
										}
									}
								}
								if ((OraTrace.m_TraceLevel & 32U) == 32U)
								{
									stringBuilder.Append("\n");
									OraTrace.Trace(32U, new string[]
									{
										stringBuilder.ToString()
									});
								}
								((RLBMetrics)rlbctx.RLBMetricsList[num7]).CurDistribFreq += ((RLBMetrics)rlbctx.RLBMetricsList[num7]).MaxDistribFreq;
								if (((RLBMetrics)rlbctx.RLBMetricsList[num7]).CurDistribFreq >= 1073741822)
								{
									((RLBMetrics)rlbctx.RLBMetricsList[num7]).CurDistribFreq = 1073741822;
								}
							}
							else
							{
								stringBuilder.Append("\n");
								OraTrace.Trace(32U, new string[]
								{
									stringBuilder.ToString()
								});
							}
							return num;
							Block_73:
							return ErrRes.CON_TIMEOUT_EXCEEDED;
						}
						goto IL_1F0;
					}
				}
				flag4 = true;
				IL_A31:
				if (flag4)
				{
					if (!flag3)
					{
						Monitor.Enter(this.htInstToCp.SyncRoot);
						flag3 = true;
					}
					if (this.htInstToCp.Count == 0 && opoConCtx.pool == null)
					{
						num = ConnectionDispenser.CreateConnectionPool(ref opoConCtx);
						if (num != 0)
						{
							return num;
						}
					}
					for (;;)
					{
						if (opoConCtx.pool == null)
						{
							int num13 = Interlocked.Increment(ref this.m_iteration) % this.htInstToCp.Count;
							if (this.m_iteration > 1073741823)
							{
								this.m_iteration = 0;
							}
							int num14 = 0;
							if (this.htInstToCp.Count > 0)
							{
								IDictionaryEnumerator enumerator3 = this.htInstToCp.GetEnumerator();
								while (enumerator3.MoveNext() && num14 != num13)
								{
									num14++;
								}
								opoConCtx.pool = (ConnectionPool)enumerator3.Value;
							}
							else
							{
								opoConCtx.pool = null;
							}
						}
						if (flag3)
						{
							Monitor.Exit(this.htInstToCp.SyncRoot);
							flag3 = false;
						}
						if (opoConCtx.pool != null)
						{
							this.m_counter.UpdateThreadWaitCount(opoConCtx.pool, 1);
							num = OpsCon.WaitForSingleObject(this.m_semCPCtxAvaNumOfCons, (int)opoConCtx.timeOut.TotalSeconds * 1000);
							this.m_counter.UpdateThreadWaitCount(opoConCtx.pool, -1);
							if (num != 0)
							{
								goto IL_102C;
							}
							num = opoConCtx.pool.GetConnection(opoConCtx);
							if (num == 0)
							{
								break;
							}
							int num15 = 0;
							OpsCon.ReleaseSemaphore(this.m_semCPCtxAvaNumOfCons, 1, ref num15);
						}
						if (DateTime.Now - now >= opoConCtx.timeOut)
						{
							goto Block_33;
						}
						opoConCtx.pool = null;
					}
					if ((OraTrace.m_PerformanceCounters & PerfCounterLevel.SoftConnectsPerSecond) == PerfCounterLevel.SoftConnectsPerSecond)
					{
						OraclePerfCounterCollection.SoftConnectsPerSecond.Increment();
					}
					if (OraTrace.m_TraceLevel != 0U)
					{
						OraTrace.Trace(32U, new string[]
						{
							" (GRID) (NON-RLB) (DISP) (" + opoConCtx.opoConRefCtx.instanceName + ")\n"
						});
					}
					if (opoConCtx.m_txnid != null && opoConCtx.pool != null && opoConCtx.pool.m_cpCtx.m_htTxnIdToIntance[opoConCtx.m_txnid] == null)
					{
						opoConCtx.pool.m_cpCtx.m_htTxnIdToIntance[opoConCtx.m_txnid] = opoConCtx.pool.m_clonedCtx.opoConRefCtx.instanceName;
					}
					if (opoConCtx.affinityInstanceName != null && opoConCtx.affinityInstanceName != opoConCtx.pool.m_clonedCtx.opoConRefCtx.instanceName)
					{
						OraTrace.Trace(2U, new string[]
						{
							string.Concat(new object[]
							{
								" (POOL) (AFFINITY) (Dispensed con for ",
								opoConCtx.pool.m_clonedCtx.opoConRefCtx.instanceName,
								" instead of ",
								opoConCtx.affinityInstanceName,
								" [",
								opoConCtx.instanceConCount,
								"])\n"
							})
						});
						ConnectionPool connectionPool4 = (ConnectionPool)this.htInstToCp[opoConCtx.affinityInstanceName];
						StringBuilder stringBuilder2 = new StringBuilder();
						if (connectionPool4 != null)
						{
							stringBuilder2.Append(" (POOL) (AFFINITY) (inst=");
							stringBuilder2.Append(opoConCtx.affinityInstanceName);
							stringBuilder2.Append(": tot=");
							stringBuilder2.Append(connectionPool4.m_counter.total);
							stringBuilder2.Append("; used=");
							stringBuilder2.Append(connectionPool4.m_counter.total - connectionPool4.m_connections.Count);
							stringBuilder2.Append("; idle=");
							stringBuilder2.Append(connectionPool4.m_connections.Count);
							stringBuilder2.Append(")");
						}
						OraTrace.Trace(2U, new string[]
						{
							stringBuilder2.ToString()
						});
						connectionPool4 = (ConnectionPool)this.htInstToCp[opoConCtx.pool.m_clonedCtx.opoConRefCtx.instanceName];
						StringBuilder stringBuilder3 = new StringBuilder();
						if (connectionPool4 != null)
						{
							stringBuilder3.Append(" (POOL) (AFFINITY) (inst=");
							stringBuilder3.Append(opoConCtx.pool.m_clonedCtx.opoConRefCtx.instanceName);
							stringBuilder3.Append(": tot=");
							stringBuilder3.Append(connectionPool4.m_counter.total);
							stringBuilder3.Append("; used=");
							stringBuilder3.Append(connectionPool4.m_counter.total - connectionPool4.m_connections.Count);
							stringBuilder3.Append("; idle=");
							stringBuilder3.Append(connectionPool4.m_connections.Count);
							stringBuilder3.Append(")");
						}
						OraTrace.Trace(2U, new string[]
						{
							stringBuilder3.ToString()
						});
					}
					else
					{
						if (opoConCtx.affinityInstanceName != null)
						{
							OraTrace.Trace(2U, new string[]
							{
								" (POOL) (AFFINITY) (Dispensed con for " + opoConCtx.pool.m_clonedCtx.opoConRefCtx.instanceName + " appropriately, which honors affinity.\n"
							});
						}
						else
						{
							OraTrace.Trace(2U, new string[]
							{
								" (POOL) (AFFINITY) (Dispensed con for " + opoConCtx.pool.m_clonedCtx.opoConRefCtx.instanceName + "; no affinity specified\n"
							});
						}
						ConnectionPool connectionPool5 = (ConnectionPool)this.htInstToCp[opoConCtx.pool.m_clonedCtx.opoConRefCtx.instanceName];
						StringBuilder stringBuilder4 = new StringBuilder();
						if (connectionPool5 != null)
						{
							stringBuilder4.Append(" (POOL) (AFFINITY) (inst=");
							stringBuilder4.Append(opoConCtx.pool.m_clonedCtx.opoConRefCtx.instanceName);
							stringBuilder4.Append(": tot=");
							stringBuilder4.Append(connectionPool5.m_counter.total);
							stringBuilder4.Append("; used=");
							stringBuilder4.Append(connectionPool5.m_counter.total - connectionPool5.m_connections.Count);
							stringBuilder4.Append("; idle=");
							stringBuilder4.Append(connectionPool5.m_connections.Count);
							stringBuilder4.Append(")");
						}
						OraTrace.Trace(2U, new string[]
						{
							stringBuilder4.ToString()
						});
					}
					return num;
					IL_102C:
					return ErrRes.CON_TIMEOUT_EXCEEDED;
					Block_33:
					result = ErrRes.CON_TIMEOUT_EXCEEDED;
				}
				else
				{
					result = 0;
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
					Monitor.Exit(this.htInstToCp.SyncRoot);
				}
			}
			return result;
		}

		// Token: 0x040009EA RID: 2538
		private Hashtable m_htInstToCp;

		// Token: 0x040009EB RID: 2539
		public Counter m_counter;

		// Token: 0x040009EC RID: 2540
		public IntPtr m_semCPCtxAvaNumOfCons;

		// Token: 0x040009ED RID: 2541
		public RLBCtx m_rlbCtx;

		// Token: 0x040009EE RID: 2542
		internal Random m_random;

		// Token: 0x040009EF RID: 2543
		public int m_iteration;

		// Token: 0x040009F0 RID: 2544
		public int totalAvaliableConnections;

		// Token: 0x040009F1 RID: 2545
		public Timer m_timer;

		// Token: 0x040009F2 RID: 2546
		public bool m_cpCtxSkipDecrement;

		// Token: 0x040009F3 RID: 2547
		public int m_dispMiss;

		// Token: 0x040009F4 RID: 2548
		private static int MAX_MISS_COUNT = 2000;

		// Token: 0x040009F5 RID: 2549
		private static int GRAV_FACTOR = 6;

		// Token: 0x040009F6 RID: 2550
		public int m_consFromAppToClear;

		// Token: 0x040009F7 RID: 2551
		public Hashtable m_htTxnIdToIntance;
	}
}
