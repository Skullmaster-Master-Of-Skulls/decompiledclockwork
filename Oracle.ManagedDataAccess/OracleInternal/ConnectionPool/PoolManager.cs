using System;
using System.Collections.Generic;
using System.Security;
using System.Security.Principal;
using System.Threading;
using System.Transactions;
using Oracle.ManagedDataAccess.Client;
using OracleInternal.Common;
using OracleInternal.MTS;
using OracleInternal.Network;
using OracleInternal.ServiceObjects;

namespace OracleInternal.ConnectionPool
{
	// Token: 0x020000C7 RID: 199
	internal abstract class PoolManager<PM, CP, PR> where PM : PoolManager<PM, CP, PR>, new() where CP : Pool<PM, CP, PR>, new() where PR : PoolResource<PM, CP, PR>, new()
	{
		// Token: 0x060007AF RID: 1967 RVA: 0x0004BDB8 File Offset: 0x00049FB8
		static PoolManager()
		{
			int num = 2;
			PoolManager<PM, CP, PR>.m_criteriaToCritTypeArray = new int[num];
			PoolManager<PM, CP, PR>.m_criteriaToCritTypeArray[0] = 0;
			PoolManager<PM, CP, PR>.m_criteriaToCritTypeArray[1] = 1;
			PoolManager<PM, CP, PR>.m_criteriaToCritTypeForDTXN = new int[num];
			PoolManager<PM, CP, PR>.m_criteriaToCritTypeForDTXN[0] = 0;
			PoolManager<PM, CP, PR>.m_criteriaToCritTypeForDTXN[1] = 0;
			PoolManager<PM, CP, PR>.m_criteriaArray = new string[num];
			PoolManager<PM, CP, PR>.m_criteriaArray[0] = "ConnectionClass";
			PoolManager<PM, CP, PR>.m_criteriaArray[1] = "Edition";
		}

		// Token: 0x060007B0 RID: 1968 RVA: 0x0004BE40 File Offset: 0x0004A040
		public PoolManager()
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)2304, new string[0]);
			}
			try
			{
				this.m_dictDictCP = new SyncDictionary<string, SyncDictionary<string, CP>>();
				this.m_pmListCP = new SyncQueueList<CP>(int.MaxValue);
				this.m_pmListPR = new SyncQueueList<PR>(int.MaxValue);
				this.m_pmListTxnCtx = new SyncQueueList<TransactionContext<PM, CP, PR>>(int.MaxValue);
				this.m_dictDictTxnCtx = new SyncDictionary<string, SyncDictionary<string, TransactionContext<PM, CP, PR>>>();
				this.m_dictSvcCtx = new SyncDictionary<string, ServiceCtx>();
				this.m_creationSync = new object();
				this.m_syncPRClose = new object();
				this.m_txnAffinityLock = new object();
				this.m_syncTxnCtx = new object();
				this.m_id = this.GetHashCode().ToString();
				this.m_criteriaMapper = new CriteriaMapper();
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)268437504, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)2560, new string[0]);
				}
			}
		}

		// Token: 0x060007B1 RID: 1969 RVA: 0x0004BF9C File Offset: 0x0004A19C
		internal virtual void ResolveTnsAlias(ConnectionString cs)
		{
			if (!string.IsNullOrEmpty(cs.m_dataSource) && this.m_cs.m_pooling)
			{
				object syncObjForGetDataSources = OracleConnectionDispenser<OraclePoolManager, OraclePool, OracleConnectionImpl>.m_syncObjForGetDataSources;
				lock (syncObjForGetDataSources)
				{
					if (OracleConnectionDispenser<PM, CP, PR>.m_listDataSources != null)
					{
						OracleConnectionDispenser<PM, CP, PR>.m_listDataSources.Add(cs.m_dataSource);
					}
					string text = OracleCommunication.Resolve(cs.m_dataSource);
					if (text != null && text.TrimStart(HelperClass.WHITE_SPACE_DELIMS).StartsWith("("))
					{
						this.m_fullDescriptor = text;
					}
				}
			}
		}

		// Token: 0x060007B2 RID: 1970 RVA: 0x0004C038 File Offset: 0x0004A238
		internal virtual void Initialize(ConnectionString cs)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)2304, new string[]
				{
					"(constr=" + cs.m_constring + ")"
				});
			}
			try
			{
				this.m_cs = cs;
				string password = cs.Password;
				string newPassword = cs.m_newPassword;
				cs.Secure();
				this.m_semPoolPopulation = new Semaphore(1, 1);
				this.m_IdleAndIncrHandles = new WaitHandle[2];
				this.m_semIdleResource = new Semaphore(0, this.m_cs.m_maxPoolSize);
				this.m_IdleAndIncrHandles[0] = this.m_semIdleResource;
				this.m_semIncrPoolSize = new Semaphore(this.m_cs.m_incrPoolSize, this.m_cs.m_incrPoolSize);
				this.m_IdleAndIncrHandles[1] = this.m_semIncrPoolSize;
				this.m_semMaxPoolSize = new Semaphore(this.m_cs.m_maxPoolSize, this.m_cs.m_maxPoolSize);
				this.m_maxPoolSize = this.m_cs.m_maxPoolSize;
				this.m_IdleAndMaxHandles = new WaitHandle[2];
				this.m_IdleAndMaxHandles[0] = this.m_semIdleResource;
				this.m_IdleAndMaxHandles[1] = this.m_semMaxPoolSize;
				if (this.m_cs.m_poolRegulator > 0)
				{
					TimerCallback callback = new TimerCallback(this.RLBGravitateThreadFunc);
					object[] array = new object[3];
					array[0] = this.m_cs.m_decrPoolSize;
					this.m_timer = new Timer(callback, array, this.m_cs.m_poolRegulator * 1000, this.m_cs.m_poolRegulator * 1000);
				}
				if (this.m_cs.m_connectionTimeout > 2147483)
				{
					this.m_timeoutValue = TimeSpan.FromSeconds(2147483.0);
				}
				else
				{
					this.m_timeoutValue = TimeSpan.FromSeconds((double)this.m_cs.m_connectionTimeout);
				}
				this.m_pmListTxnCtx.m_max = cs.m_maxPoolSize;
				this.m_pmListTxnCtx.m_bMaxExplicitlySet = true;
				this.ResolveTnsAlias(cs);
				this.m_currentMaxSemCount = this.m_cs.m_maxPoolSize;
				this.m_currentIdleSemCount = this.m_cs.m_incrPoolSize;
				int num = cs.m_haEvents ? 1 : 0;
				if (!cs.m_haEventsPresentInConnString)
				{
					num = Convert.ToInt32(ConfigBaseClass.m_haEvents);
				}
				this.m_bHAEnabled = (num == 1);
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)268437504, ex, null);
				throw;
			}
			finally
			{
				this.m_bResolveTnsAlias = false;
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)2560, new string[]
					{
						string.Concat(new string[]
						{
							"(pmid=",
							this.m_id,
							") (constr=",
							cs.m_constring,
							")"
						})
					});
				}
			}
		}

		// Token: 0x060007B3 RID: 1971 RVA: 0x0004C31C File Offset: 0x0004A51C
		internal virtual void InitializeSelfTuning()
		{
		}

		// Token: 0x170001DB RID: 475
		// (get) Token: 0x060007B4 RID: 1972 RVA: 0x0004C320 File Offset: 0x0004A520
		public ConnectionString ConnectionString
		{
			get
			{
				return this.m_cs;
			}
		}

		// Token: 0x060007B5 RID: 1973 RVA: 0x0004C328 File Offset: 0x0004A528
		public int GetNumberOfIdleConnections()
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)2304, new string[0]);
			}
			int result;
			try
			{
				int num = 0;
				List<CP> list = this.m_pmListCP.GetList();
				for (int i = 0; i < list.Count; i++)
				{
					num += list[i].m_cpQueuePR.Count;
				}
				result = num;
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)268437504, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)2560, new string[0]);
				}
			}
			return result;
		}

		// Token: 0x060007B6 RID: 1974 RVA: 0x0004C3D4 File Offset: 0x0004A5D4
		public void UnPopulatePoolThreadFunc(object state)
		{
			this.UnPopulatePool(state);
		}

		// Token: 0x060007B7 RID: 1975 RVA: 0x0004C3E0 File Offset: 0x0004A5E0
		public int UnPopulatePool(object state)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)2304, new string[0]);
			}
			int num = 0;
			int num2 = 0;
			int num3 = 0;
			int num4 = 0;
			string text = string.Empty;
			string text2 = string.Empty;
			try
			{
				UnPopulatePoolArgs unPopulatePoolArgs = (UnPopulatePoolArgs)state;
				num4 = unPopulatePoolArgs.m_decrementCount;
				text = unPopulatePoolArgs.m_instanceName;
				text2 = unPopulatePoolArgs.m_serviceName;
				List<PR> list;
				if (text != null && text2 != null)
				{
					list = this.m_dictDictCP[text2][text].m_cpListPR.GetList();
				}
				else
				{
					list = this.m_pmListPR.GetList();
				}
				if (this.m_pmListCP.Count == 1)
				{
					num3 = Math.Min(num4, this.m_pmListCP[0].m_cpQueuePR.Count);
				}
				else
				{
					num2 = this.GetNumberOfIdleConnections();
					num3 = Math.Max(0, Math.Min(num4, num2 - this.m_cs.m_minPoolSize));
				}
				int num5 = 0;
				while (num5 < list.Count && num < num3)
				{
					try
					{
						PR pr = list[num5];
						if ((text == null || text2 == null || (text2 != null && pr.ServiceName == text2 && text != null && pr.m_instanceName == text)) && !pr.m_bCheckedOutByApp && !pr.m_bCheckedOutByDTC && pr.m_bPutCompleted && (pr.m_mtsTxnCtx == null || (pr.m_mtsTxnCtx != null && pr.m_mtsTxnCtx.m_txnType == MTSTxnType.None)))
						{
							lock (pr)
							{
								if (!pr.m_bCheckedOutByApp && !pr.m_bCheckedOutByDTC && pr.m_bPutCompleted && (pr.m_mtsTxnCtx == null || (pr.m_mtsTxnCtx != null && pr.m_mtsTxnCtx.m_txnType == MTSTxnType.None)))
								{
									if (pr.m_deletionRequestor != DeletionRequestor.HA)
									{
										pr.m_deletionRequestor = DeletionRequestor.PoolRegulator;
									}
									if (pr.m_pm.Close(pr, null))
									{
										num++;
									}
									else if (pr.m_deletionRequestor != DeletionRequestor.HA)
									{
										pr.m_deletionRequestor = DeletionRequestor.None;
									}
								}
							}
						}
					}
					catch (Exception ex)
					{
						if (ProviderConfig.m_bTraceLevelPrivate)
						{
							Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)268437504, new string[]
							{
								ex.ToString()
							});
						}
					}
					num5++;
				}
			}
			catch (Exception ex2)
			{
				OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)268437504, ex2, null);
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, OracleTraceTag.CP, new string[]
					{
						string.Concat(new object[]
						{
							"(RLB) UnPopulatePool(instance=",
							text,
							"; request to close=",
							num4,
							"; attempt to close=",
							num3,
							"; idle=",
							num2,
							"; min=",
							this.m_cs.m_minPoolSize,
							"; closed=",
							num,
							")"
						})
					});
				}
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)2560, new string[0]);
				}
			}
			return num;
		}

		// Token: 0x060007B8 RID: 1976 RVA: 0x0004C800 File Offset: 0x0004AA00
		public void PopulatePoolThreadFunc(object state)
		{
			this.PopulatePool(state);
		}

		// Token: 0x060007B9 RID: 1977 RVA: 0x0004C80C File Offset: 0x0004AA0C
		public int PopulatePool(object state)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)2304, new string[]
				{
					string.Concat(new object[]
					{
						"(count=",
						((PoolPopulationOption)state).m_requestCount,
						")(target=",
						((PoolPopulationOption)state).m_targetCount,
						")"
					})
				});
			}
			int num = 0;
			PoolPopulationOption poolPopulationOption = (PoolPopulationOption)state;
			CriteriaCtx criteriaCtx = null;
			if (poolPopulationOption.m_connectionClass != null)
			{
				criteriaCtx = new CriteriaCtx();
				criteriaCtx.m_connectionClass = poolPopulationOption.m_connectionClass;
			}
			try
			{
				WindowsImpersonationContext windowsImpersonationContext = null;
				try
				{
					if (this.m_cs.m_osUser != null)
					{
						windowsImpersonationContext = this.m_cs.m_osUser.Impersonate();
					}
					for (int i = 0; i < poolPopulationOption.m_requestCount; i++)
					{
						bool flag = false;
						bool flag2 = false;
						if (this.m_pmListPR.Count >= poolPopulationOption.m_targetCount)
						{
							return num;
						}
						flag = this.m_semMaxPoolSize.WaitOne(0);
						if (!flag)
						{
							return num;
						}
						PR pr = default(PR);
						try
						{
							if (poolPopulationOption.m_ignoreIncrPoolSize)
							{
								pr = this.CreateNewPR(1, true, null, criteriaCtx, null, null);
							}
							else
							{
								flag2 = this.m_semIncrPoolSize.WaitOne(0);
								if (flag2)
								{
									pr = this.CreateNewPR(1, true, null, criteriaCtx, null, null);
								}
							}
						}
						catch
						{
							break;
						}
						finally
						{
							if (flag2)
							{
								try
								{
									this.m_currentIncrSemCount = this.m_semIncrPoolSize.Release();
								}
								catch
								{
									throw new Exception("m_semIncrPoolSize.Release2() threw exception with count = " + this.m_currentIncrSemCount);
								}
								flag2 = false;
							}
							if (pr != null)
							{
								num++;
							}
							else if (flag)
							{
								this.m_currentMaxSemCount = this.m_semMaxPoolSize.Release();
								flag = false;
							}
						}
					}
				}
				catch (Exception ex)
				{
					if (ProviderConfig.m_bTraceLevelPrivate)
					{
						Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)268437504, new string[]
						{
							ex.ToString()
						});
					}
				}
				finally
				{
					try
					{
						if (windowsImpersonationContext != null)
						{
							windowsImpersonationContext.Undo();
						}
					}
					catch (Exception ex2)
					{
						if (ProviderConfig.m_bTraceLevelPrivate)
						{
							Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)268437504, new string[]
							{
								ex2.ToString()
							});
						}
					}
				}
			}
			catch (Exception ex3)
			{
				OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)268437504, ex3, null);
			}
			finally
			{
				if (poolPopulationOption.m_semPoolPopulation != null)
				{
					poolPopulationOption.m_semPoolPopulation.Release();
				}
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, OracleTraceTag.CP, new string[]
					{
						string.Concat(new object[]
						{
							"PopulatePoolThreadFunc(created=",
							num,
							"; max=",
							this.m_cs.m_maxPoolSize,
							"; total=",
							this.m_pmListPR.Count,
							")"
						})
					});
				}
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)2560, new string[0]);
				}
			}
			return num;
		}

		// Token: 0x060007BA RID: 1978 RVA: 0x0004CBC4 File Offset: 0x0004ADC4
		public virtual void PutCP(PR pr, CP cp)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)2304, new string[0]);
			}
			try
			{
				cp.m_pm = (PM)((object)this);
				if (cp.m_instanceName == null || cp.m_serviceName == null)
				{
					cp.m_instanceName = pr.m_instanceName;
					cp.m_serviceName = pr.ServiceName;
				}
				this.m_dictDictCP[pr.ServiceName][pr.m_instanceName] = cp;
				lock (this.m_creationSync)
				{
					this.m_pmListCP.Add(cp);
				}
				if (OraclePool.m_bPerfNumberOfInactiveConnectionPools)
				{
					OraclePool.PerformanceCounterIncrement(OraclePerfParams.CounterIndex.NumberOfInactiveConnectionPools, pr as OracleConnectionImpl, cp as OraclePool);
				}
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)268437504, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)2560, new string[]
					{
						"(total={0})",
						this.m_pmListCP.Count.ToString()
					});
				}
			}
		}

		// Token: 0x060007BB RID: 1979 RVA: 0x0004CD30 File Offset: 0x0004AF30
		public virtual void PutNewPR(PR pr, bool bForPoolPopulation)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)2304, new string[]
				{
					Trace.GetCPInfo(pr as OracleConnectionImpl, null, null, null, false, false)
				});
			}
			try
			{
				if (bForPoolPopulation)
				{
					pr.m_bPutCompleted = true;
				}
				else
				{
					pr.m_bPutCompleted = false;
				}
				this.m_pmListPR.Add(pr);
				pr.m_pm = (PM)((object)this);
				string serviceName = pr.ServiceName;
				this.CreateServiceCtx(pr);
				if (this.m_cs.m_pooling)
				{
					bool migratePR = false;
					this.AddPRToPool(pr, bForPoolPopulation, migratePR);
				}
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)268437504, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)2560, new string[]
					{
						Trace.GetCPInfo(pr as OracleConnectionImpl, null, null, null, false, false)
					});
				}
			}
		}

		// Token: 0x060007BC RID: 1980 RVA: 0x0004CE3C File Offset: 0x0004B03C
		public void SetServiceForDS(string dataSource, string serviceName)
		{
			if (!PoolManager<PM, CP, PR>.m_serviceDSDict.ContainsKey(dataSource))
			{
				try
				{
					PoolManager<PM, CP, PR>.m_serviceDomainDictLocker.EnterWriteLock();
					if (!PoolManager<PM, CP, PR>.m_serviceDSDict.ContainsKey(dataSource))
					{
						PoolManager<PM, CP, PR>.m_serviceDSDict[dataSource] = serviceName;
					}
				}
				finally
				{
					PoolManager<PM, CP, PR>.m_serviceDomainDictLocker.ExitWriteLock();
				}
			}
		}

		// Token: 0x060007BD RID: 1981 RVA: 0x0004CE98 File Offset: 0x0004B098
		public string GetServiceForDS(string dataSource)
		{
			string result = null;
			try
			{
				PoolManager<PM, CP, PR>.m_serviceDomainDictLocker.EnterReadLock();
				if (PoolManager<PM, CP, PR>.m_serviceDSDict.ContainsKey(dataSource))
				{
					result = PoolManager<PM, CP, PR>.m_serviceDSDict[dataSource];
				}
			}
			finally
			{
				PoolManager<PM, CP, PR>.m_serviceDomainDictLocker.ExitReadLock();
			}
			return result;
		}

		// Token: 0x060007BE RID: 1982 RVA: 0x0004CEE8 File Offset: 0x0004B0E8
		public void SetDomainForDS(string dataSource, string domainName)
		{
			if (!PoolManager<PM, CP, PR>.m_domainDSDict.ContainsKey(dataSource))
			{
				try
				{
					PoolManager<PM, CP, PR>.m_serviceDomainDictLocker.EnterWriteLock();
					if (!PoolManager<PM, CP, PR>.m_domainDSDict.ContainsKey(dataSource))
					{
						PoolManager<PM, CP, PR>.m_domainDSDict[dataSource] = domainName;
					}
				}
				finally
				{
					PoolManager<PM, CP, PR>.m_serviceDomainDictLocker.ExitWriteLock();
				}
			}
		}

		// Token: 0x060007BF RID: 1983 RVA: 0x0004CF44 File Offset: 0x0004B144
		public string GetDomainForDS(string dataSource)
		{
			string result = null;
			try
			{
				PoolManager<PM, CP, PR>.m_serviceDomainDictLocker.EnterReadLock();
				if (PoolManager<PM, CP, PR>.m_domainDSDict.ContainsKey(dataSource))
				{
					result = PoolManager<PM, CP, PR>.m_domainDSDict[dataSource];
				}
			}
			finally
			{
				PoolManager<PM, CP, PR>.m_serviceDomainDictLocker.ExitReadLock();
			}
			return result;
		}

		// Token: 0x060007C0 RID: 1984 RVA: 0x0004CF94 File Offset: 0x0004B194
		private void AddPRToPool(PR pr, bool bForPoolPopulation, bool migratePR)
		{
			string serviceName = pr.ServiceName;
			if (migratePR && pr.m_cp != null)
			{
				pr.m_cp.m_cpListPR.Remove(pr);
			}
			if (this.m_dictDictCP[serviceName] == null)
			{
				lock (this.m_dictDictCP)
				{
					if (this.m_dictDictCP[serviceName] == null)
					{
						this.m_dictDictCP[serviceName] = new SyncDictionary<string, CP>();
					}
				}
			}
			if (!this.m_dictDictCP[serviceName].ContainsKey(pr.m_instanceName))
			{
				lock (this.m_dictDictCP)
				{
					if (!this.m_dictDictCP[serviceName].ContainsKey(pr.m_instanceName))
					{
						CP cp = Activator.CreateInstance<CP>();
						this.PutCP(pr, cp);
						cp.PutNewPR(pr, bForPoolPopulation);
						this.m_dictSvcCtx[serviceName].m_roundRobin.SetMax(this.m_dictDictCP[serviceName].Count);
					}
					else
					{
						pr.m_cp = this.m_dictDictCP[serviceName][pr.m_instanceName];
						pr.m_cp.PutNewPR(pr, bForPoolPopulation);
					}
					goto IL_1AD;
				}
			}
			pr.m_cp = this.m_dictDictCP[serviceName][pr.m_instanceName];
			pr.m_cp.PutNewPR(pr, bForPoolPopulation);
			IL_1AD:
			if (bForPoolPopulation)
			{
				this.m_currentIdleSemCount = this.m_semIdleResource.Release();
				Trace.Write(OracleTraceLevel.Private, OracleTraceTag.CP, new string[]
				{
					string.Format("PM.PutNewPR() : Idle Sempahore count={0}", this.m_currentIdleSemCount)
				});
			}
		}

		// Token: 0x060007C1 RID: 1985 RVA: 0x0004D1AC File Offset: 0x0004B3AC
		private void CreateServiceCtx(PR pr)
		{
			string serviceName = pr.ServiceName;
			if (!this.m_dictSvcCtx.ContainsKey(serviceName))
			{
				lock (this.m_dictSvcCtx)
				{
					if (!this.m_dictSvcCtx.ContainsKey(serviceName))
					{
						this.m_dictSvcCtx[serviceName] = new ServiceCtx(serviceName);
						this.m_dictSvcCtx[serviceName].m_databaseName = pr.m_databaseName;
					}
				}
			}
		}

		// Token: 0x060007C2 RID: 1986 RVA: 0x0004D240 File Offset: 0x0004B440
		public void CreateNewPRThreadFunc(object state)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)2304, new string[0]);
			}
			try
			{
				WindowsImpersonationContext windowsImpersonationContext = null;
				object[] array = state as object[];
				PR pr = (PR)((object)array[0]);
				CriteriaCtx criteriaCtx = (CriteriaCtx)array[1];
				if (pr != null && ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)2304, new string[]
					{
						"(initiated by TID:" + pr.requestingThreadId + ")"
					});
				}
				ManualResetEventSlim eventConCreated = pr.m_eventConCreated;
				ManualResetEventSlim eventConTimeout = pr.m_eventConTimeout;
				try
				{
					if (this.m_cs.m_osUser != null)
					{
						windowsImpersonationContext = this.m_cs.m_osUser.Impersonate();
					}
					if (pr.m_cs == null)
					{
						pr.Connect(this.m_cs, true, criteriaCtx, pr.m_affinityInstance);
					}
					else
					{
						pr.Connect(pr.m_cs, true, criteriaCtx, pr.m_affinityInstance);
					}
				}
				catch (Exception exception)
				{
					pr.m_exception = exception;
				}
				finally
				{
					try
					{
						pr.m_newPassword = null;
						if (windowsImpersonationContext != null)
						{
							windowsImpersonationContext.Undo();
						}
					}
					catch
					{
					}
				}
				try
				{
					if (pr.m_bTimedOut && pr.m_exception == null)
					{
						string pmId = this.m_cs.m_pmId;
						SyncDictionary<string, PM> htPM = OracleConnectionDispenser<PM, CP, PR>.m_htPM;
						if (this.m_cs.m_pooling && this == htPM[pmId] && this.m_pmListCP.Count > 0)
						{
							if (this.m_semMaxPoolSize.WaitOne(0))
							{
								this.PutNewPR(pr, true);
							}
						}
						else
						{
							try
							{
								pr.DisConnect(null);
							}
							catch
							{
							}
						}
					}
				}
				finally
				{
					try
					{
						eventConCreated.Set();
					}
					catch
					{
					}
					try
					{
						eventConTimeout.Wait(pr.m_conTimeout);
					}
					catch
					{
					}
					try
					{
						eventConTimeout.Dispose();
					}
					catch
					{
					}
					try
					{
						eventConCreated.Dispose();
					}
					catch
					{
					}
				}
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)268437504, ex, null);
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)2560, new string[0]);
				}
			}
		}

		// Token: 0x060007C3 RID: 1987 RVA: 0x0004D584 File Offset: 0x0004B784
		internal PR ConnectWithNoTimeout(PR pr, string instanceName, ConnectionString csWithDiffOrNewPwd, bool bForPoolPopulation, CriteriaCtx criteriaCtx)
		{
			bool bForPoolPopulation2 = !bForPoolPopulation;
			if (csWithDiffOrNewPwd == null)
			{
				pr.Connect(this.m_cs, bForPoolPopulation2, criteriaCtx, instanceName);
			}
			else
			{
				pr.Connect(csWithDiffOrNewPwd, bForPoolPopulation2, criteriaCtx, instanceName);
			}
			this.ProcessCriteriaCtxAndAlterSessionIfReqd(criteriaCtx, pr);
			this.PutNewPR(pr, bForPoolPopulation);
			return pr;
		}

		// Token: 0x060007C4 RID: 1988 RVA: 0x0004D5D8 File Offset: 0x0004B7D8
		public virtual PR CreateNewPR(int reqCount, bool bForPoolPopulation, ConnectionString csWithDiffOrNewPwd, CriteriaCtx criteriaCtx, string instanceName = null, List<string> switchFailedInstNames = null)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)2304, new string[]
				{
					Trace.GetCPInfo(null, null, instanceName, null, false, false)
				});
			}
			PR pr = default(PR);
			PR result;
			try
			{
				pr = Activator.CreateInstance<PR>();
				pr.m_pm = (PM)((object)this);
				if (this.m_bResolveTnsAlias)
				{
					if (csWithDiffOrNewPwd == null)
					{
						this.ResolveTnsAlias(this.m_cs);
					}
					else
					{
						this.ResolveTnsAlias(csWithDiffOrNewPwd);
					}
				}
				int num;
				if (csWithDiffOrNewPwd != null)
				{
					num = csWithDiffOrNewPwd.m_connectionTimeout;
				}
				else if (this.m_cs.m_connectionTimeout > 2147483)
				{
					num = 2147483;
				}
				else
				{
					num = this.m_cs.m_connectionTimeout;
				}
				int num2 = num * 1000;
				bool flag = false;
				int num3 = 0;
				while (!flag)
				{
					if (!string.IsNullOrEmpty(this.m_conStrServiceName) && this.m_dictSvcCtx[this.m_conStrServiceName] != null && !this.m_dictSvcCtx[this.m_conStrServiceName].m_serviceUpEvent.IsSet)
					{
						lock (this.m_syncObjForDefaultSvcRelocWait)
						{
							if (this.m_dictSvcCtx[this.m_conStrServiceName] != null && !this.m_dictSvcCtx[this.m_conStrServiceName].m_serviceUpEvent.IsSet && !this.m_dictSvcCtx[this.m_conStrServiceName].m_bWaitedForSvcReloc)
							{
								if (ProviderConfig.m_bTraceLevelPrivate)
								{
									Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)2304, new string[]
									{
										"Waiting for Service up event in connection pool " + this.GetHashCode()
									});
								}
								TimeSpan timeSpan = DateTime.Now - this.m_dictSvcCtx[this.m_conStrServiceName].m_serviceDownTime;
								TimeSpan timeSpan2 = TimeSpan.Zero;
								if (ConfigBaseClass.s_bFromConfigSRCT)
								{
									if (ConfigBaseClass.s_bDrainTimeoutInSRCT)
									{
										timeSpan2 = TimeSpan.FromSeconds((double)(this.m_drain_timeout + ConfigBaseClass.srctOffset));
									}
									else
									{
										timeSpan2 = TimeSpan.FromSeconds((double)ConfigBaseClass.srctOffset);
									}
								}
								else if (this.m_drain_timeout != 0)
								{
									timeSpan2 = TimeSpan.FromSeconds((double)this.m_drain_timeout);
								}
								else
								{
									timeSpan2 = TimeSpan.FromSeconds((double)Convert.ToInt32(ConfigBaseClass.m_serviceRelocationTimeout));
								}
								int num4 = 0;
								if (timeSpan > TimeSpan.Zero && timeSpan < timeSpan2)
								{
									num4 = (int)(timeSpan2 - timeSpan).TotalSeconds;
									if (num4 < 0)
									{
										num4 = 2147483;
									}
								}
								this.m_dictSvcCtx[this.m_conStrServiceName].m_serviceUpEvent.Wait(num4 * 1000);
								this.m_dictSvcCtx[this.m_conStrServiceName].m_bWaitedForSvcReloc = true;
								if (ProviderConfig.m_bTraceLevelPrivate)
								{
									Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)2304, new string[]
									{
										"Finished waiting for Service up event in connection pool " + this.GetHashCode()
									});
								}
							}
						}
					}
					if (num2 == 0 || bForPoolPopulation)
					{
						PR pr2 = this.ConnectWithNoTimeout(pr, instanceName, csWithDiffOrNewPwd, bForPoolPopulation, criteriaCtx);
						flag = true;
						if (pr2 != null)
						{
							return pr2;
						}
					}
					else
					{
						pr.m_eventConCreated = new ManualResetEventSlim();
						pr.m_eventConTimeout = new ManualResetEventSlim();
						pr.m_conTimeout = num2;
						pr.m_cs = csWithDiffOrNewPwd;
						pr.m_affinityInstance = instanceName;
						if (csWithDiffOrNewPwd != null)
						{
							pr.m_password = csWithDiffOrNewPwd.Password;
							pr.m_proxyPassword = csWithDiffOrNewPwd.ProxyPassword;
							pr.m_newPassword = csWithDiffOrNewPwd.m_newPassword;
						}
						else
						{
							pr.m_password = this.Password;
							pr.m_proxyPassword = this.ProxyPassword;
						}
						pr.requestingThreadId = Thread.CurrentThread.ManagedThreadId;
						ThreadPool.QueueUserWorkItem(new WaitCallback(this.CreateNewPRThreadFunc), new object[]
						{
							pr,
							criteriaCtx
						});
						try
						{
							pr.m_bTimedOut = !pr.m_eventConCreated.Wait(num2);
						}
						finally
						{
							pr.m_eventConTimeout.Set();
						}
						if (pr.m_bTimedOut)
						{
							if (ProviderConfig.m_bTraceLevelPrivate)
							{
								Trace.Write(OracleTraceLevel.Private, OracleTraceTag.CP, new string[]
								{
									OracleConnection.Dump()
								});
							}
							throw new OracleException(ResourceStringConstants.CON_TIMEOUT_EXCEEDED, string.Empty, string.Empty, string.Empty);
						}
						if (pr.m_exception != null)
						{
							string message = pr.m_exception.Message;
							bool flag3 = message.StartsWith("ORA-12514", StringComparison.InvariantCulture);
							if (flag3)
							{
								if (ProviderConfig.m_bTraceLevelPrivate)
								{
									Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)2304, new string[]
									{
										"ORA-12514 in connection pool " + this.GetHashCode()
									});
								}
								if (!string.IsNullOrEmpty(this.m_conStrServiceName) && this.m_dictSvcCtx[this.m_conStrServiceName] != null && !this.m_dictSvcCtx[this.m_conStrServiceName].m_serviceUpEvent.IsSet)
								{
									if (this.m_dictSvcCtx[this.m_conStrServiceName].m_bWaitedForSvcReloc)
									{
										if (ProviderConfig.m_bTraceLevelPrivate)
										{
											Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)2304, new string[]
											{
												"Already WAITED for ServiceRelocationConnectionTimeout, propagate exception now for connection pool (id: " + this.GetHashCode() + ");\n"
											});
										}
										throw pr.m_exception;
									}
									pr.m_exception = null;
									continue;
								}
								else
								{
									num3++;
									if (num3 < 4)
									{
										pr.m_exception = null;
										if (num3 > 1)
										{
											if (ProviderConfig.m_bTraceLevelPrivate)
											{
												Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)2304, new string[]
												{
													"ORA-12514 : Sleep for a sec in connection pool " + this.GetHashCode()
												});
											}
											Thread.Sleep(1000);
											continue;
										}
										continue;
									}
								}
							}
							throw pr.m_exception;
						}
						try
						{
							this.ProcessCriteriaCtxAndAlterSessionIfReqd(criteriaCtx, pr);
							flag = true;
							this.PutNewPR(pr, bForPoolPopulation);
						}
						catch (OracleException)
						{
							if (pr != null)
							{
								if (switchFailedInstNames != null && !switchFailedInstNames.Contains(pr.m_instanceName))
								{
									switchFailedInstNames.Add(pr.m_instanceName);
								}
								this.PutNewPR(pr, true);
							}
							throw;
						}
					}
				}
				int count = this.m_pmListPR.Count;
				if (this.m_cs.m_pooling && this.m_cs.m_minPoolSize > 1 && count < this.m_cs.m_minPoolSize && count < this.m_cs.m_maxPoolSize)
				{
					if (this.m_semPoolPopulation.WaitOne(0))
					{
						if (this.m_pmListPR.Count < this.m_cs.m_minPoolSize)
						{
							int num5 = this.m_cs.m_minPoolSize - this.m_pmListPR.Count;
							if (num5 > 0)
							{
								string connectionClass = null;
								if (criteriaCtx != null)
								{
									connectionClass = criteriaCtx.m_connectionClass;
								}
								PoolPopulationOption state = new PoolPopulationOption(num5, this.m_cs.m_minPoolSize, this.m_semPoolPopulation, true, connectionClass);
								ThreadPool.QueueUserWorkItem(new WaitCallback(this.PopulatePoolThreadFunc), state);
							}
							else
							{
								this.m_semPoolPopulation.Release();
							}
						}
						else
						{
							this.m_semPoolPopulation.Release();
						}
					}
				}
				else if (this.m_cs.m_pooling && this.m_cs.m_incrPoolSize > 1 && count > this.m_cs.m_minPoolSize && count < this.m_cs.m_maxPoolSize && this.m_semPoolPopulation.WaitOne(0))
				{
					if (this.m_pmListPR.Count < this.m_cs.m_maxPoolSize)
					{
						int num6 = this.m_pmListPR.Count + this.m_cs.m_incrPoolSize;
						int num7 = this.m_cs.m_incrPoolSize;
						if (num6 > this.m_cs.m_maxPoolSize)
						{
							int num8 = num6 - this.m_cs.m_maxPoolSize;
							num7 -= num8;
						}
						if (num7 > 0)
						{
							string connectionClass2 = null;
							if (criteriaCtx != null)
							{
								connectionClass2 = criteriaCtx.m_connectionClass;
							}
							PoolPopulationOption state2 = new PoolPopulationOption(num7, num7 + this.m_pmListPR.Count, this.m_semPoolPopulation, false, connectionClass2);
							ThreadPool.QueueUserWorkItem(new WaitCallback(this.PopulatePoolThreadFunc), state2);
						}
						else
						{
							this.m_semPoolPopulation.Release();
						}
					}
					else
					{
						this.m_semPoolPopulation.Release();
					}
				}
				result = pr;
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)268437504, ex, null);
				if (ex is OracleException)
				{
					if (((OracleException)ex).Number == 38802 || ((OracleException)ex).Number == 2248)
					{
						this.m_criteriaMapper.RemoveId(criteriaCtx.m_edition, 2);
					}
					if (((OracleException)ex).Number == 38802)
					{
						OracleException ex2 = new OracleException(-7505, string.Empty, string.Empty, OracleStringResourceManager.GetErrorMesg(-7505, new string[0]), ex);
						throw ex2;
					}
				}
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)2560, new string[]
					{
						Trace.GetCPInfo(pr as OracleConnectionImpl, null, instanceName, null, false, false)
					});
				}
			}
			return result;
		}

		// Token: 0x060007C5 RID: 1989 RVA: 0x0004DF98 File Offset: 0x0004C198
		public virtual PR GetUsingDiffPassword(ConnectionString csWithDiffOrNewPwd, CriteriaCtx criteriaCtx)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)2304, new string[]
				{
					Trace.GetCPInfo(null, null, null, null, false, false)
				});
			}
			PR pr = default(PR);
			PR result;
			try
			{
				pr = this.CreateNewPR(1, false, csWithDiffOrNewPwd, criteriaCtx, null, null);
				pr.m_deletionRequestor = DeletionRequestor.None;
				pr.m_bPutCompleted = false;
				if (pr != null && this.m_semMaxPoolSize.WaitOne(0))
				{
					if (ProviderConfig.m_bTraceLevelPrivate)
					{
						Trace.Write(OracleTraceLevel.Private, OracleTraceTag.CP, new string[]
						{
							Trace.GetCPInfo(pr as OracleConnectionImpl, null, null, "new1", false, false)
						});
					}
					result = pr;
				}
				else
				{
					if (pr != null)
					{
						pr.DisConnect(null);
					}
					result = this.Get(csWithDiffOrNewPwd, true, criteriaCtx, null, false);
				}
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)268437504, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)2560, new string[]
					{
						Trace.GetCPInfo(pr as OracleConnectionImpl, null, null, null, false, false)
					});
				}
			}
			return result;
		}

		// Token: 0x060007C6 RID: 1990 RVA: 0x0004E0D4 File Offset: 0x0004C2D4
		public PR GetIdleConnectionToKill(TimeSpan ts, List<string> instancesToSkip = null)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)2304, new string[]
				{
					Trace.GetCPInfo(null, null, null, null, false, false)
				});
			}
			PR pr = default(PR);
			PR result;
			try
			{
				List<CP> list = this.m_pmListCP.GetList();
				list.Sort(CPComparer.s_cpComparer);
				DateTime now = DateTime.Now;
				while (pr == null)
				{
					for (int i = 0; i < list.Count; i++)
					{
						CP cp = list[i];
						if (instancesToSkip == null || !instancesToSkip.Contains(cp.m_instanceName))
						{
							pr = cp.Get(null);
							if (pr != null)
							{
								return pr;
							}
						}
					}
					Thread.Sleep(500);
					if (DateTime.Now - now > ts)
					{
						if (ProviderConfig.m_bTraceLevelPrivate)
						{
							Trace.Write(OracleTraceLevel.Private, OracleTraceTag.CP, new string[]
							{
								OracleConnection.Dump()
							});
						}
						throw new OracleException(ResourceStringConstants.CON_TIMEOUT_EXCEEDED, string.Empty, string.Empty, string.Empty);
					}
				}
				result = pr;
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)268437504, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)2560, new string[]
					{
						Trace.GetCPInfo(pr as OracleConnectionImpl, null, null, null, false, false)
					});
				}
			}
			return result;
		}

		// Token: 0x060007C7 RID: 1991 RVA: 0x0004E258 File Offset: 0x0004C458
		public PR GetPRWithMatchingServiceOrInstance(ConnectionString csWithDiffOrNewPwd, List<string> services, string serviceName, string instanceName, CriteriaCtx criteriaCtx, List<string> instancesToSkip = null)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)2304, new string[0]);
			}
			PR pr = default(PR);
			CP cp = default(CP);
			bool flag = false;
			PR result;
			try
			{
				if (this.m_dictDictCP[serviceName] != null)
				{
					cp = this.m_dictDictCP[serviceName][instanceName];
					if (cp != null)
					{
						if (!cp.m_bInstanceDown && (instancesToSkip == null || !instancesToSkip.Contains(cp.m_instanceName)))
						{
							CP cp2 = cp;
							if (cp2 != null)
							{
								if (!this.m_cs.m_bProxyUserIdSet)
								{
									pr = cp2.Get(criteriaCtx);
								}
								else
								{
									pr = cp2.GetProxy(csWithDiffOrNewPwd, criteriaCtx);
								}
							}
							if (pr != null)
							{
								return pr;
							}
						}
						else
						{
							if (ProviderConfig.m_bTraceLevelPrivate)
							{
								Trace.Write(OracleTraceLevel.Private, OracleTraceTag.CP, new string[]
								{
									string.Format("Service {0} is DOWN on Instance {1}. Not dispensing connections from this pool.", serviceName, instanceName)
								});
							}
							flag = true;
						}
					}
				}
				int num = services.IndexOf(serviceName);
				int num2 = (num > 0) ? num : 0;
				for (int i = 0; i < services.Count; i++)
				{
					if (this.m_dictDictCP[services[num2]] != null)
					{
						if (i == 0 && num >= 0)
						{
							List<CP> list = (this.m_dictDictCP[services[num2]] != null) ? this.m_dictDictCP[services[num2]].GetValues() : null;
							if (list != null)
							{
								for (int j = 0; j < list.Count; j++)
								{
									CP cp3 = list[j];
									if (cp3 != cp && !cp3.m_bInstanceDown && (instancesToSkip == null || !instancesToSkip.Contains(cp3.m_instanceName)))
									{
										if (cp3 != null)
										{
											if (!this.m_cs.m_bProxyUserIdSet)
											{
												pr = cp3.Get(criteriaCtx);
											}
											else
											{
												pr = cp3.GetProxy(csWithDiffOrNewPwd, criteriaCtx);
											}
										}
										if (pr != null)
										{
											return pr;
										}
									}
								}
							}
						}
						else
						{
							if (flag)
							{
								break;
							}
							CP cp4 = this.m_dictDictCP[services[num2]][instanceName];
							if (cp4 != null)
							{
								if (!this.m_cs.m_bProxyUserIdSet)
								{
									pr = cp4.Get(criteriaCtx);
								}
								else
								{
									pr = cp4.GetProxy(csWithDiffOrNewPwd, criteriaCtx);
								}
							}
						}
						if (pr != null)
						{
							return pr;
						}
						num2++;
						if (num2 == services.Count)
						{
							num2 = 0;
						}
					}
				}
				result = pr;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, OracleTraceTag.CP, new string[]
					{
						Trace.GetCPInfo(pr as OracleConnectionImpl, null, null, (pr != null) ? string.Format("Multitenant: Found a connection. ServiceName: {0}, InstanceName: {1}", pr.ServiceName, pr.m_instanceName) : "pr not found", false, false)
					});
				}
			}
			return result;
		}

		// Token: 0x060007C8 RID: 1992 RVA: 0x0004E59C File Offset: 0x0004C79C
		private PR DestoryAndReplace(ConnectionString csWithDiffOrNewPwd, CriteriaCtx criteriaCtx, string affinityInstanceName, List<string> switchFailedInstNames = null)
		{
			PR pr = default(PR);
			for (int i = 0; i < this.m_pmListCP.Count; i++)
			{
				CP cp = this.m_pmListCP[i];
				if (cp != null)
				{
					if (!this.m_cs.m_bProxyUserIdSet)
					{
						pr = cp.Get(criteriaCtx);
					}
					else
					{
						pr = cp.GetProxy(csWithDiffOrNewPwd, criteriaCtx);
					}
				}
				if (pr != null)
				{
					break;
				}
			}
			PR pr2 = pr;
			pr = this.CreateNewPR(1, false, csWithDiffOrNewPwd, criteriaCtx, affinityInstanceName, switchFailedInstNames);
			if (pr != null)
			{
				pr2.m_deletionRequestor = DeletionRequestor.HA;
				pr2.m_bClosedWithReplacement = true;
				this.Put(pr2, null);
				pr2.m_bClosedWithReplacement = false;
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, OracleTraceTag.CP, new string[]
					{
						Trace.GetCPInfo(pr as OracleConnectionImpl, null, affinityInstanceName, string.Format("pm:get:killed(sessid{0}:{1};inst={2})", pr2.m_endUserSessionId.ToString(), pr2.m_endUserSerialNum.ToString(), pr2.m_instanceName), false, false)
					});
				}
			}
			return pr;
		}

		// Token: 0x060007C9 RID: 1993 RVA: 0x0004E6C4 File Offset: 0x0004C8C4
		public virtual PR Get(ConnectionString csWithDiffOrNewPwd, bool bGetForApp, CriteriaCtx criteriaCtx, string affinityInstanceName = null, bool bForceMatch = false)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)2304, new string[]
				{
					Trace.GetCPInfo(null, null, affinityInstanceName, null, false, false) + "(bForceMatch={0})",
					bForceMatch.ToString().Substring(0, 1)
				});
			}
			PR pr = default(PR);
			bool flag = false;
			bool flag2 = false;
			bool flag3 = false;
			bool flag4 = false;
			bool flag5 = false;
			bool flag6 = false;
			bool flag7 = false;
			TimeSpan timeSpan = this.m_timeoutValue;
			DateTime now = DateTime.Now;
			int num = 0;
			List<string> list = null;
			if (criteriaCtx != null && !string.IsNullOrEmpty(criteriaCtx.m_serviceName))
			{
				list = new List<string>();
			}
			PR result;
			try
			{
				if (this.m_cs.m_drcpEnabled == DrcpType.True && !string.IsNullOrEmpty(this.m_cs.m_proxyUserId) && !string.IsNullOrEmpty(this.m_cs.Password))
				{
					throw new InvalidOperationException(OracleStringResourceManager.GetErrorMesg(28150, new string[0]));
				}
				if (criteriaCtx != null)
				{
					if (this.m_cs.m_drcpEnabled == DrcpType.False && !string.IsNullOrEmpty(criteriaCtx.m_connectionClass))
					{
						throw new InvalidOperationException(OracleStringResourceManager.GetErrorMesg(-7601, new string[]
						{
							"DRCPConnectionClass",
							"DRCP"
						}));
					}
					this.m_criteriaMapper.AssignId(criteriaCtx);
				}
				while (pr == null && num <= 1)
				{
					if (ProviderConfig.m_bTraceLevelPrivate)
					{
						Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)2304, new string[]
						{
							"MultiTenant : Searching for a idle connection, retryCountWithoutAffinity: " + num
						});
					}
					try
					{
						flag = false;
						flag2 = false;
						flag3 = false;
						flag4 = false;
						flag5 = false;
						int num2;
						if (!flag6)
						{
							num2 = WaitHandle.WaitAny(this.m_IdleAndIncrHandles, timeSpan);
							if (num2 == 0)
							{
								flag = true;
							}
							else if (num2 == 1)
							{
								flag2 = true;
							}
						}
						else
						{
							num2 = WaitHandle.WaitAny(this.m_IdleAndMaxHandles, timeSpan);
							if (num2 == 0)
							{
								flag = true;
							}
							else if (num2 == 1)
							{
								flag3 = true;
							}
						}
						this.ProcessCriteriaCtx_NonEnlistedConnection(ref criteriaCtx);
						if (num2 == 0)
						{
							try
							{
								string text = this.m_conStrServiceName;
								if (criteriaCtx != null && !string.IsNullOrEmpty(criteriaCtx.m_serviceName))
								{
									text = criteriaCtx.m_serviceName;
								}
								if (ProviderConfig.m_bTraceLevelPrivate)
								{
									Trace.Write(OracleTraceLevel.Private, OracleTraceTag.CP, new string[]
									{
										"MultiTenant : Searching for a idle connection. Service Name: " + text
									});
								}
								if (affinityInstanceName != null)
								{
									bool flag8 = false;
									bool flag9 = false;
									if (!string.IsNullOrEmpty(text))
									{
										if (this.m_dictSvcCtx[text] != null && this.m_dictSvcCtx[text].m_serviceDown)
										{
											if (ProviderConfig.m_bTraceLevelPrivate)
											{
												Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)2304, new string[]
												{
													"MultiTenant : Service DOWN: " + text
												});
											}
											flag8 = true;
										}
										if (this.m_dictSvcCtx[text] != null && this.m_dictSvcCtx[text].m_serviceMemberDownInstNames.IndexOf(affinityInstanceName) >= 0)
										{
											if (ProviderConfig.m_bTraceLevelPrivate)
											{
												Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)2304, new string[]
												{
													string.Format("MultiTenant : Service Member DOWN {0} on instance {1} (1)", text, affinityInstanceName)
												});
											}
											flag9 = true;
										}
										if (pr == null && !flag8)
										{
											List<string> keys = this.m_dictDictCP.GetKeys();
											pr = this.GetPRWithMatchingServiceOrInstance(csWithDiffOrNewPwd, keys, text, affinityInstanceName, criteriaCtx, list);
										}
										if (pr == null && !flag8)
										{
											int i = 0;
											while (i < this.m_pmListCP.Count)
											{
												CP cp = this.m_pmListCP[i];
												if (cp == null)
												{
													goto IL_42E;
												}
												if (cp.m_serviceName == text || cp.m_instanceName == affinityInstanceName || cp.m_bInstanceDown || (this.m_dictSvcCtx[text] != null && this.m_dictSvcCtx[text].m_serviceMemberDownInstNames.IndexOf(cp.m_instanceName) >= 0) || (list != null && list.Contains(cp.m_instanceName)))
												{
													if (ProviderConfig.m_bTraceLevelPrivate)
													{
														Trace.Write(OracleTraceLevel.Private, OracleTraceTag.CP, new string[]
														{
															string.Format("Multitenant : Skipping pool with service: {0}, instance: {1} (2)", cp.m_serviceName, cp.m_instanceName)
														});
													}
												}
												else
												{
													if (!this.m_cs.m_bProxyUserIdSet)
													{
														pr = cp.Get(criteriaCtx);
														goto IL_42E;
													}
													pr = cp.GetProxy(csWithDiffOrNewPwd, criteriaCtx);
													goto IL_42E;
												}
												IL_436:
												i++;
												continue;
												IL_42E:
												if (pr == null)
												{
													goto IL_436;
												}
												break;
											}
										}
										if (pr != null)
										{
											if (pr.m_instanceName != affinityInstanceName && !flag9)
											{
												PR pr2 = pr;
												pr = default(PR);
												try
												{
													pr = this.CreateNewPR(1, false, csWithDiffOrNewPwd, criteriaCtx, affinityInstanceName, list);
													if (pr != null)
													{
														pr2.m_deletionRequestor = DeletionRequestor.HA;
														pr2.m_bClosedWithReplacement = true;
														this.Put(pr2, null);
														pr2.m_bClosedWithReplacement = false;
														if (ProviderConfig.m_bTraceLevelPrivate)
														{
															Trace.Write(OracleTraceLevel.Private, OracleTraceTag.CP, new string[]
															{
																Trace.GetCPInfo(pr as OracleConnectionImpl, null, affinityInstanceName, string.Format("pm:get:killed(sessid{0}:{1};inst={2})", pr2.m_endUserSessionId.ToString(), pr2.m_endUserSerialNum.ToString(), pr2.m_instanceName), false, false)
															});
														}
													}
												}
												catch (Exception ex)
												{
													if (ex.InnerException == null || !(ex.InnerException is OracleException) || ((OracleException)ex.InnerException).Number != 44787)
													{
														throw;
													}
													if (ProviderConfig.m_bTraceLevelPrivate)
													{
														Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)2304, new string[]
														{
															"MultiTenant : ORA error 44787. Lets swallow this error and use the idle connection instead - 1\n" + ex.ToString()
														});
													}
													pr = pr2;
													pr2 = default(PR);
												}
											}
											if (pr != null && ProviderConfig.m_bTraceLevelPrivate)
											{
												Trace.Write(OracleTraceLevel.Private, OracleTraceTag.CP, new string[]
												{
													Trace.GetCPInfo(pr as OracleConnectionImpl, null, affinityInstanceName, "pm:get:reg1", false, false)
												});
											}
										}
										if (pr == null && ProviderConfig.m_bTraceLevelPrivate)
										{
											Trace.Write(OracleTraceLevel.Private, OracleTraceTag.CP, new string[]
											{
												Trace.GetCPInfo(null, null, affinityInstanceName, "pm.get:reg1:nothing", false, false)
											});
										}
									}
									if (pr == null)
									{
										string text2;
										if (!flag9 && ((!string.IsNullOrEmpty(affinityInstanceName) && !string.IsNullOrEmpty(criteriaCtx.m_serviceName)) || string.IsNullOrEmpty(criteriaCtx.m_serviceName)))
										{
											text2 = affinityInstanceName;
										}
										else
										{
											bool flag10;
											text2 = this.GetInstanceNameToConnect(text, list, out flag10);
											if (string.IsNullOrEmpty(text2) && criteriaCtx.m_serviceSwitchRequested)
											{
												throw new OracleException(-7505, string.Empty, string.Empty, OracleStringResourceManager.GetErrorMesg(-7505, new string[0]));
											}
										}
										pr = this.GetNew(csWithDiffOrNewPwd, criteriaCtx, text2, list);
										if (pr != null)
										{
											if (ProviderConfig.m_bTraceLevelPrivate)
											{
												Trace.Write(OracleTraceLevel.Private, OracleTraceTag.CP, new string[]
												{
													Trace.GetCPInfo(pr as OracleConnectionImpl, null, affinityInstanceName, "pm:get:new2", false, false)
												});
											}
											flag5 = true;
										}
										else if (ProviderConfig.m_bTraceLevelPrivate)
										{
											Trace.Write(OracleTraceLevel.Private, OracleTraceTag.CP, new string[]
											{
												Trace.GetCPInfo(null, null, affinityInstanceName, "pm:get:new2:nothing", false, false)
											});
										}
										if (pr == null)
										{
											PR pr3 = default(PR);
											timeSpan = this.m_timeoutValue - (DateTime.Now - now);
											pr3 = this.GetIdleConnectionToKill(timeSpan, list);
											if (pr3 != null)
											{
												if (flag9)
												{
													pr = pr3;
													pr3 = default(PR);
													goto IL_8E2;
												}
												try
												{
													pr = this.CreateNewPR(1, false, csWithDiffOrNewPwd, criteriaCtx, text2, list);
													if (pr != null)
													{
														pr3.m_deletionRequestor = DeletionRequestor.HA;
														pr3.m_bClosedWithReplacement = true;
														this.Put(pr3, null);
														pr3.m_bClosedWithReplacement = false;
														if (ProviderConfig.m_bTraceLevelPrivate)
														{
															Trace.Write(OracleTraceLevel.Private, OracleTraceTag.CP, new string[]
															{
																Trace.GetCPInfo(pr as OracleConnectionImpl, null, affinityInstanceName, string.Format("pm:get:killed(sessid{0}:{1};inst={2})", pr3.m_endUserSessionId.ToString(), pr3.m_endUserSerialNum.ToString(), pr3.m_instanceName), false, false)
															});
														}
													}
													goto IL_8E2;
												}
												catch (Exception ex2)
												{
													if (ex2.InnerException != null && ex2.InnerException is OracleException && ((OracleException)ex2.InnerException).Number == 44787)
													{
														if (ProviderConfig.m_bTraceLevelPrivate)
														{
															Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)2304, new string[]
															{
																"MultiTenant : ORA error 44787. Lets swallow this error and use the idle connection instead - 2\n" + ex2.ToString()
															});
														}
														pr = pr3;
														pr3 = default(PR);
														goto IL_8E2;
													}
													throw;
												}
											}
											if (ProviderConfig.m_bTraceLevelPrivate)
											{
												Trace.Write(OracleTraceLevel.Private, OracleTraceTag.CP, new string[]
												{
													Trace.GetCPInfo(null, null, affinityInstanceName, "pm:get:kill:nothing", false, false)
												});
											}
										}
									}
								}
								IL_8E2:
								if (pr == null)
								{
									bool flag11 = false;
									bool flag12 = false;
									List<string> keys2 = this.m_dictDictCP.GetKeys();
									int num3 = 0;
									if (!string.IsNullOrEmpty(text))
									{
										num3 = keys2.IndexOf(text);
									}
									int num4 = (num3 >= 0) ? num3 : 0;
									int num5 = 0;
									RLB rlb = null;
									if (!string.IsNullOrEmpty(text))
									{
										if (this.m_dictSvcCtx[text] != null && this.m_dictSvcCtx[text].m_serviceDown)
										{
											if (ProviderConfig.m_bTraceLevelPrivate)
											{
												Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)2304, new string[]
												{
													"MultiTenant : Service DOWN: " + text
												});
											}
											flag12 = true;
										}
										if (!string.IsNullOrEmpty(affinityInstanceName) && this.m_dictSvcCtx[text] != null && this.m_dictSvcCtx[text].m_serviceMemberDownInstNames.IndexOf(affinityInstanceName) >= 0)
										{
											if (ProviderConfig.m_bTraceLevelPrivate)
											{
												Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)2304, new string[]
												{
													string.Format("MultiTenant : Service Member DOWN {0} on instance {1}", text, affinityInstanceName)
												});
											}
											flag11 = true;
										}
									}
									while (pr == null)
									{
										rlb = null;
										CP cp2 = default(CP);
										if (this.m_timeoutValue < DateTime.Now - now)
										{
											if (ProviderConfig.m_bTraceLevelPrivate)
											{
												Trace.Write(OracleTraceLevel.Private, OracleTraceTag.CP, new string[]
												{
													OracleConnection.Dump()
												});
											}
											throw new OracleException((this.m_pmListPR.Count >= this.m_maxPoolSize) ? ResourceStringConstants.CON_POOLED_TIMEOUT_EXCEEDED : ResourceStringConstants.CON_TIMEOUT_EXCEEDED, string.Empty, string.Empty, string.Empty);
										}
										string text3 = keys2[num4];
										if (!string.IsNullOrEmpty(text3) && !flag12)
										{
											string text4 = null;
											if (this.m_dictSvcCtx[text3] != null)
											{
												text4 = (this.m_dictSvcCtx[text3].m_databaseName + "|" + text3).ToLowerInvariant();
											}
											if (text4 != null)
											{
												rlb = RLBManager.Get(text4);
											}
											int num6 = this.m_dictSvcCtx[keys2[num4]].m_roundRobin.NextValue();
											int index = num6;
											bool flag13 = false;
											int num7 = 0;
											if (rlb != null)
											{
												int j = 0;
												while (j < rlb.m_instances.Length)
												{
													string text5;
													int indexToBeRemoved;
													if (j == 0 || flag13)
													{
														text5 = rlb.GetInstanceName(out num6);
														indexToBeRemoved = num6;
													}
													else
													{
														if (num7 >= rlb.m_instances.Length)
														{
															num7 = 0;
														}
														text5 = rlb.m_instances[num7];
														indexToBeRemoved = num7;
													}
													if (!string.IsNullOrEmpty(text5) && this.m_dictDictCP[keys2[num4]] != null)
													{
														cp2 = this.m_dictDictCP[keys2[num4]][text5];
														if (cp2 != null && !cp2.m_bInstanceDown && (this.m_dictSvcCtx[text3] == null || this.m_dictSvcCtx[text3].m_serviceMemberDownInstNames.IndexOf(cp2.m_instanceName) == -1) && (list == null || !list.Contains(cp2.m_instanceName)))
														{
															if (!this.m_cs.m_bProxyUserIdSet || flag7)
															{
																pr = cp2.Get(criteriaCtx);
															}
															else
															{
																pr = cp2.GetProxy(csWithDiffOrNewPwd, criteriaCtx);
															}
														}
														else if (ProviderConfig.m_bTraceLevelPrivate && cp2 != null)
														{
															Trace.Write(OracleTraceLevel.Private, OracleTraceTag.CP, new string[]
															{
																string.Format("MultiTenant : Skipping pool with service: {0}, instance: {1} (3)", cp2.m_serviceName, cp2.m_instanceName)
															});
														}
													}
													if (pr != null)
													{
														if (text5 == null || !(pr.m_instanceName != text5))
														{
															break;
														}
														this.m_rlbMissCount++;
														if (this.m_rlbMissCount >= 2000)
														{
															string text6 = null;
															if (criteriaCtx != null)
															{
																text6 = criteriaCtx.m_connectionClass;
															}
															ThreadPool.QueueUserWorkItem(new WaitCallback(this.RLBGravitateThreadFunc), new object[]
															{
																0,
																pr.ServiceName,
																text6
															});
															this.m_rlbMissCount = 0;
															break;
														}
														break;
													}
													else
													{
														if (cp2 != null && !cp2.HasValidConnections)
														{
															this.RemoveAndRedistribute(rlb, cp2, indexToBeRemoved);
															flag13 = true;
														}
														else if (j == 0)
														{
															if (num6 == 0)
															{
																num7++;
															}
															else
															{
																num7 = 0;
															}
														}
														else
														{
															num7++;
															if (num7 == num6)
															{
																num7++;
															}
														}
														j++;
													}
												}
											}
											else
											{
												List<CP> values = this.m_dictDictCP[keys2[num4]].GetValues();
												for (int k = 0; k < values.Count; k++)
												{
													cp2 = values[index];
													if (cp2 != null && !cp2.m_bInstanceDown && (this.m_dictSvcCtx[text3] == null || this.m_dictSvcCtx[text3].m_serviceMemberDownInstNames.IndexOf(cp2.m_instanceName) == -1) && (list == null || !list.Contains(cp2.m_instanceName)))
													{
														if (!this.m_cs.m_bProxyUserIdSet || flag7)
														{
															pr = cp2.Get(criteriaCtx);
														}
														else
														{
															pr = cp2.GetProxy(csWithDiffOrNewPwd, criteriaCtx);
														}
													}
													else if (ProviderConfig.m_bTraceLevelPrivate && cp2 != null)
													{
														Trace.Write(OracleTraceLevel.Private, OracleTraceTag.CP, new string[]
														{
															string.Format("MultiTenant : Skipping pool with service: {0}, instance: {1}", cp2.m_serviceName, cp2.m_instanceName)
														});
													}
													if (pr != null)
													{
														break;
													}
													index = this.m_dictSvcCtx[keys2[num4]].m_roundRobin.NextValue();
												}
											}
											if (pr != null && ProviderConfig.m_bTraceLevelPrivate)
											{
												Trace.Write(OracleTraceLevel.Private, OracleTraceTag.CP, new string[]
												{
													Trace.GetCPInfo(pr as OracleConnectionImpl, null, affinityInstanceName, (rlb == null) ? "pm:get:rro1" : "get:rlb1", false, false)
												});
											}
										}
										if (pr == null)
										{
											num4++;
											num5++;
											if (num4 == keys2.Count)
											{
												num4 = 0;
											}
											if (num5 == keys2.Count)
											{
												string text7;
												if (!flag11 && ((!string.IsNullOrEmpty(affinityInstanceName) && !string.IsNullOrEmpty(criteriaCtx.m_serviceName)) || string.IsNullOrEmpty(criteriaCtx.m_serviceName)))
												{
													text7 = affinityInstanceName;
												}
												else
												{
													bool flag14;
													text7 = this.GetInstanceNameToConnect(text, list, out flag14);
													if (string.IsNullOrEmpty(text7) && criteriaCtx.m_serviceSwitchRequested)
													{
														throw new OracleException(-7505, string.Empty, string.Empty, OracleStringResourceManager.GetErrorMesg(-7505, new string[0]));
													}
												}
												pr = this.GetNew(csWithDiffOrNewPwd, criteriaCtx, text7, list);
												if (pr != null)
												{
													flag5 = true;
												}
												else
												{
													PR pr4 = default(PR);
													timeSpan = this.m_timeoutValue - (DateTime.Now - now);
													pr4 = this.GetIdleConnectionToKill(timeSpan, list);
													if (pr4 != null)
													{
														if (flag11)
														{
															pr = pr4;
															pr4 = default(PR);
														}
														else
														{
															pr4.m_deletionRequestor = DeletionRequestor.HA;
															pr4.m_bClosedWithReplacement = true;
															this.Put(pr4, null);
															pr = this.CreateNewPR(1, false, csWithDiffOrNewPwd, criteriaCtx, text7, list);
															pr4.m_bClosedWithReplacement = false;
														}
														if (pr != null)
														{
															break;
														}
													}
												}
												flag7 = true;
											}
										}
									}
									if (pr != null && ProviderConfig.m_bTraceLevelPrivate)
									{
										Trace.Write(OracleTraceLevel.Private, OracleTraceTag.CP, new string[]
										{
											Trace.GetCPInfo(pr as OracleConnectionImpl, null, affinityInstanceName, (rlb == null) ? "pm:get:rro2" : "pm:get:rlb2", false, false)
										});
									}
								}
								continue;
							}
							finally
							{
								if (flag && (pr == null || flag4 || flag5))
								{
									this.m_currentIdleSemCount = this.m_semIdleResource.Release();
									Trace.Write(OracleTraceLevel.Private, OracleTraceTag.CP, new string[]
									{
										string.Format("PM.Get() : Idle Sempahore count={0}", this.m_currentIdleSemCount)
									});
									flag = false;
								}
							}
						}
						if (num2 == 1)
						{
							try
							{
								if (!flag3)
								{
									flag3 = this.m_semMaxPoolSize.WaitOne(0);
								}
								if (!flag2)
								{
									flag2 = this.m_semIncrPoolSize.WaitOne(0);
								}
								if (flag3 && flag2)
								{
									bool flag15 = false;
									string text8 = this.m_conStrServiceName;
									if (criteriaCtx != null && !string.IsNullOrEmpty(criteriaCtx.m_serviceName))
									{
										text8 = criteriaCtx.m_serviceName;
									}
									if (!string.IsNullOrEmpty(affinityInstanceName) && !string.IsNullOrEmpty(text8) && this.m_dictSvcCtx[text8] != null && this.m_dictSvcCtx[text8].m_serviceMemberDownInstNames.IndexOf(affinityInstanceName) >= 0)
									{
										if (ProviderConfig.m_bTraceLevelPrivate)
										{
											Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)2304, new string[]
											{
												string.Format("MultiTenant : Service Member DOWN {0} on affinity instance {1}", text8, affinityInstanceName)
											});
										}
										flag15 = true;
									}
									string text9;
									if (!flag15 && ((!string.IsNullOrEmpty(affinityInstanceName) && !string.IsNullOrEmpty(criteriaCtx.m_serviceName)) || string.IsNullOrEmpty(criteriaCtx.m_serviceName)))
									{
										text9 = affinityInstanceName;
										pr = this.CreateNewPR(1, false, csWithDiffOrNewPwd, criteriaCtx, text9, list);
									}
									else
									{
										bool flag16 = false;
										text9 = this.GetInstanceNameToConnect(text8, list, out flag16);
										if (string.IsNullOrEmpty(text9) && !flag16 && criteriaCtx.m_serviceSwitchRequested)
										{
											throw new OracleException(-7505, string.Empty, string.Empty, OracleStringResourceManager.GetErrorMesg(-7505, new string[0]));
										}
										pr = this.CreateNewPR(1, false, csWithDiffOrNewPwd, criteriaCtx, text9, list);
									}
									if (ProviderConfig.m_bTraceLevelPrivate)
									{
										Trace.Write(OracleTraceLevel.Private, OracleTraceTag.CP, new string[]
										{
											Trace.GetCPInfo(pr as OracleConnectionImpl, null, text9, "pm:get:new3", false, false)
										});
									}
									if (pr != null)
									{
									}
								}
								else
								{
									flag6 = true;
									timeSpan = this.m_timeoutValue - (DateTime.Now - now);
								}
								continue;
							}
							finally
							{
								if (flag2)
								{
									try
									{
										this.m_currentIncrSemCount = this.m_semIncrPoolSize.Release();
									}
									catch
									{
										throw new Exception("m_semIncrPoolSize.Release1() threw exception with count = " + this.m_currentIncrSemCount);
									}
									flag2 = false;
								}
								if ((pr == null || flag4) && flag3)
								{
									try
									{
										this.m_currentMaxSemCount = this.m_semMaxPoolSize.Release();
									}
									catch
									{
										throw new Exception("m_semMaxPoolSize.Release() threw exception with count = " + this.m_currentMaxSemCount);
									}
									flag3 = false;
								}
							}
						}
						if (num2 == 258)
						{
							if (ProviderConfig.m_bTraceLevelPrivate)
							{
								Trace.Write(OracleTraceLevel.Private, OracleTraceTag.CP, new string[]
								{
									OracleConnection.Dump()
								});
							}
							throw new OracleException((this.m_pmListPR.Count >= this.m_maxPoolSize) ? ResourceStringConstants.CON_POOLED_TIMEOUT_EXCEEDED : ResourceStringConstants.CON_TIMEOUT_EXCEEDED, string.Empty, string.Empty, string.Empty);
						}
					}
					catch (OracleException ex3)
					{
						if (ex3.Number == 12521)
						{
							affinityInstanceName = null;
							num++;
							if (num > 1)
							{
								if (ProviderConfig.m_bTraceLevelPrivate)
								{
									string text10 = string.Format("(ERROR)(ERROR=12521)(RETRY_COUNT={0})", num);
									Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Error, new string[]
									{
										text10
									});
								}
								throw ex3;
							}
							if (ProviderConfig.m_bTraceLevelPrivate)
							{
								string text11 = string.Format("(RETRY)(ERROR=12521)(RETRY_COUNT={0})", num);
								Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Error, new string[]
								{
									text11
								});
							}
						}
						else
						{
							if (ex3.Number == 38802 || ex3.Number == 2248)
							{
								this.m_criteriaMapper.RemoveId(criteriaCtx.m_edition, 2);
								throw ex3;
							}
							if (ex3.InnerException == null || !(ex3.InnerException is OracleException) || ((OracleException)ex3.InnerException).Number != 44787)
							{
								throw ex3;
							}
							if (ProviderConfig.m_bTraceLevelPrivate)
							{
								Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)2304, new string[]
								{
									"MultiTenant : ORA error 44787. Try to get another connection - 1\n" + ex3.ToString()
								});
							}
							if (DateTime.Now - now > this.m_timeoutValue)
							{
								if (ProviderConfig.m_bTraceLevelPrivate)
								{
									Trace.Write(OracleTraceLevel.Private, OracleTraceTag.CP, new string[]
									{
										OracleConnection.Dump()
									});
								}
								throw new OracleException(ResourceStringConstants.CON_TIMEOUT_EXCEEDED, string.Empty, string.Empty, string.Empty);
							}
							timeSpan = this.m_timeoutValue - (DateTime.Now - now);
							if (ProviderConfig.m_bTraceLevelPrivate)
							{
								Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)2304, new string[]
								{
									"MultiTenant : Time Remaining to find a connection - 1: " + timeSpan.ToString()
								});
							}
						}
					}
					finally
					{
						if (pr != null)
						{
							lock (pr)
							{
								if (bGetForApp)
								{
									pr.m_bCheckedOutByApp = true;
								}
								else
								{
									pr.m_bCheckedOutByDTC = true;
								}
								pr.m_bPutCompleted = false;
								if (pr.m_pm.m_cs.m_drcpEnabled == DrcpType.True)
								{
									bool bDRCPServerProcessAttached = pr.bDRCPServerProcessAttached;
								}
							}
							string serviceName = pr.ServiceName;
							if (criteriaCtx != null && pr.m_bCheckIfAlterSessionReqd)
							{
								try
								{
									bool[] array = pr.ProcessCriteriaCtx(criteriaCtx);
									if (array != null && (array[0] || array[1]))
									{
										lock (pr)
										{
											pr.AlterSession(array, criteriaCtx);
										}
									}
								}
								catch (Exception ex4)
								{
									if (ex4.InnerException == null || !(ex4.InnerException is OracleException) || ((OracleException)ex4.InnerException).Number != 44787)
									{
										throw;
									}
									if (ProviderConfig.m_bTraceLevelPrivate)
									{
										Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)2304, new string[]
										{
											"MultiTenant : ORA error 44787. Try to get another connection - 2.\n" + ex4.ToString()
										});
									}
									if (pr != null)
									{
										if (!string.IsNullOrEmpty(pr.m_instanceName) && list != null && !list.Contains(pr.m_instanceName))
										{
											list.Add(pr.m_instanceName);
										}
										if (ProviderConfig.m_bTraceLevelPrivate)
										{
											Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)2304, new string[]
											{
												string.Format("MultiTenant : Placing the connection back. Conn ID = {0} , Service Name = {1} , PDB Name = {2} , Edition Name = {3} , DBInst = {4}", new object[]
												{
													pr.m_endUserSessionId,
													pr.ServiceName,
													pr.PdbName,
													pr.EditionName,
													pr.m_instanceName
												})
											});
										}
										this.Put(pr, criteriaCtx);
										pr = default(PR);
									}
									if (DateTime.Now - now > this.m_timeoutValue)
									{
										if (ProviderConfig.m_bTraceLevelPrivate)
										{
											Trace.Write(OracleTraceLevel.Private, OracleTraceTag.CP, new string[]
											{
												OracleConnection.Dump()
											});
										}
										throw new OracleException(ResourceStringConstants.CON_TIMEOUT_EXCEEDED, string.Empty, string.Empty, string.Empty);
									}
									timeSpan = this.m_timeoutValue - (DateTime.Now - now);
									if (ProviderConfig.m_bTraceLevelPrivate)
									{
										Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)2304, new string[]
										{
											"MultiTenant : Time Remaining to find a connection - 2: " + timeSpan.ToString()
										});
									}
								}
								if (pr != null)
								{
									string serviceName2 = pr.ServiceName;
									if (!serviceName2.Equals(serviceName, StringComparison.InvariantCultureIgnoreCase))
									{
										this.CreateServiceCtx(pr);
										bool bForPoolPopulation = false;
										bool migratePR = true;
										this.AddPRToPool(pr, bForPoolPopulation, migratePR);
									}
								}
							}
						}
					}
				}
				result = pr;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					string text12 = "null";
					if (pr != null)
					{
						if (pr.m_instanceName == affinityInstanceName)
						{
							text12 = "bMatchFound=T";
						}
						else
						{
							text12 = "bMatchFound=F";
						}
					}
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)2560, new string[]
					{
						Trace.GetCPInfo(pr as OracleConnectionImpl, null, affinityInstanceName, null, false, false) + "PM.Get(aff={0};force={1}) returning ({2})",
						affinityInstanceName,
						bForceMatch.ToString().Substring(0, 1),
						text12
					});
				}
			}
			return result;
		}

		// Token: 0x060007CA RID: 1994 RVA: 0x0005018C File Offset: 0x0004E38C
		internal string GetInstanceNameToConnect(string requestedServiceName, List<string> instancesToSkip, out bool firstConn)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)2304, new string[0]);
			}
			RLB rlb = null;
			string text = null;
			string text2 = null;
			int num = 0;
			int num2 = 0;
			firstConn = false;
			if (!string.IsNullOrEmpty(requestedServiceName) && this.m_dictSvcCtx[requestedServiceName] != null)
			{
				text = (this.m_dictSvcCtx[requestedServiceName].m_databaseName + "|" + this.m_dictSvcCtx[requestedServiceName].m_serviceName).ToLowerInvariant();
			}
			if (text != null)
			{
				rlb = RLBManager.Get(text);
			}
			if (rlb != null)
			{
				for (int i = 0; i < rlb.m_instances.Length; i++)
				{
					if (i == 0)
					{
						num = 0;
						num2 = 0;
						text2 = rlb.GetInstanceName(out num);
					}
					else
					{
						if (num2 >= rlb.m_instances.Length)
						{
							num2 = 0;
						}
						text2 = rlb.m_instances[num2];
					}
					if (!this.m_dictSvcCtx[requestedServiceName].m_serviceMemberDownInstNames.GetList().Contains(text2) && (instancesToSkip == null || !instancesToSkip.Contains(text2)))
					{
						break;
					}
					text2 = null;
					if (i == 0)
					{
						if (num == 0)
						{
							num2++;
						}
						else
						{
							num2 = 0;
						}
					}
					else
					{
						num2++;
						if (num2 == num)
						{
							num2++;
						}
					}
				}
			}
			else if (this.m_pmListCP != null && this.m_pmListCP.Count > 0)
			{
				for (int j = 0; j < this.m_pmListCP.Count; j++)
				{
					text2 = this.m_pmListCP[j].m_instanceName;
					if ((this.m_dictSvcCtx[requestedServiceName] == null || (this.m_dictSvcCtx[requestedServiceName] != null && !this.m_dictSvcCtx[requestedServiceName].m_serviceMemberDownInstNames.GetList().Contains(text2))) && (instancesToSkip == null || !instancesToSkip.Contains(text2)))
					{
						break;
					}
					text2 = null;
				}
			}
			else if (instancesToSkip != null && instancesToSkip.Count > 0)
			{
				firstConn = false;
			}
			else
			{
				firstConn = true;
			}
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)2560, new string[]
				{
					string.Format("MultiTenant : Returning inst name: {0}, firstConn: {1}", text2, firstConn)
				});
			}
			return text2;
		}

		// Token: 0x060007CB RID: 1995 RVA: 0x0005039C File Offset: 0x0004E59C
		private List<string> GetInstNames()
		{
			List<CP> list = this.m_pmListCP.GetList();
			List<string> list2 = new List<string>();
			for (int i = 0; i < list.Count; i++)
			{
				if (!list2.Contains(list[i].m_instanceName))
				{
					list2.Add(list[i].m_instanceName);
				}
			}
			return list2;
		}

		// Token: 0x060007CC RID: 1996 RVA: 0x00050400 File Offset: 0x0004E600
		internal void ProcessCriteriaCtx_EnlistedConnection(ref CriteriaCtx criteriaCtx)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Entry, new string[0]);
			}
			try
			{
				if (criteriaCtx == null || (string.IsNullOrEmpty(criteriaCtx.m_serviceName) && string.IsNullOrEmpty(criteriaCtx.m_pdbName)))
				{
					if (criteriaCtx == null)
					{
						criteriaCtx = new CriteriaCtx();
					}
					if (!string.IsNullOrEmpty(this.m_conStrServiceName))
					{
						criteriaCtx.m_serviceName = this.m_conStrServiceName;
					}
					else
					{
						string serviceForDS = this.GetServiceForDS(this.m_cs.ServerID);
						if (!string.IsNullOrEmpty(serviceForDS))
						{
							criteriaCtx.m_serviceName = serviceForDS;
						}
					}
				}
				else if (string.IsNullOrEmpty(criteriaCtx.m_serviceName) && !string.IsNullOrEmpty(criteriaCtx.m_pdbName))
				{
					if (criteriaCtx.m_pdbName.Equals("cdb$root", StringComparison.InvariantCultureIgnoreCase) || criteriaCtx.m_pdbName.Equals("pdb$seed", StringComparison.InvariantCultureIgnoreCase))
					{
						criteriaCtx.m_serviceName = "SYS$USERS";
					}
					else if (!string.IsNullOrEmpty(this.m_databaseDomainName))
					{
						criteriaCtx.m_serviceName = criteriaCtx.m_pdbName + "." + this.m_databaseDomainName;
					}
					else
					{
						string domainForDS = this.GetDomainForDS(this.m_cs.ServerID);
						if (!string.IsNullOrEmpty(domainForDS))
						{
							criteriaCtx.m_serviceName = criteriaCtx.m_pdbName + "." + domainForDS;
						}
					}
				}
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Exit, new string[0]);
				}
			}
		}

		// Token: 0x060007CD RID: 1997 RVA: 0x00050588 File Offset: 0x0004E788
		internal void ProcessCriteriaCtx_NonEnlistedConnection(ref CriteriaCtx criteriaCtx)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Entry, new string[0]);
			}
			try
			{
				if (criteriaCtx != null && string.IsNullOrEmpty(criteriaCtx.m_serviceName) && !string.IsNullOrEmpty(criteriaCtx.m_pdbName))
				{
					if (criteriaCtx.m_pdbName.Equals("cdb$root", StringComparison.InvariantCultureIgnoreCase) || criteriaCtx.m_pdbName.Equals("pdb$seed", StringComparison.InvariantCultureIgnoreCase))
					{
						criteriaCtx.m_serviceName = "SYS$USERS";
					}
					else if (!string.IsNullOrEmpty(this.m_databaseDomainName))
					{
						criteriaCtx.m_serviceName = criteriaCtx.m_pdbName + "." + this.m_databaseDomainName;
					}
					else
					{
						string domainForDS = this.GetDomainForDS(this.m_cs.ServerID);
						if (!string.IsNullOrEmpty(domainForDS))
						{
							criteriaCtx.m_serviceName = criteriaCtx.m_pdbName + "." + domainForDS;
						}
					}
				}
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, OracleTraceTag.Exit, new string[0]);
				}
			}
		}

		// Token: 0x060007CE RID: 1998 RVA: 0x00050698 File Offset: 0x0004E898
		internal virtual void ProcessCriteriaCtxAndAlterSessionIfReqd(CriteriaCtx criteriaCtx, PR pr)
		{
			if (criteriaCtx != null && pr.m_bCheckIfAlterSessionReqd)
			{
				bool[] array = pr.ProcessCriteriaCtx(criteriaCtx);
				if (array != null && (array[0] || array[1]))
				{
					pr.AlterSession(array, criteriaCtx);
				}
			}
		}

		// Token: 0x060007CF RID: 1999 RVA: 0x000506E4 File Offset: 0x0004E8E4
		internal PR GetNew(ConnectionString cs, CriteriaCtx criteriaCtx, string instanceName = null, List<string> switchFailedInstNames = null)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)2304, new string[]
				{
					Trace.GetCPInfo(null, null, instanceName, null, false, false)
				});
			}
			PR pr = default(PR);
			PR result;
			try
			{
				if (this.m_semMaxPoolSize.WaitOne(0))
				{
					try
					{
						pr = this.CreateNewPR(1, false, cs, criteriaCtx, instanceName, switchFailedInstNames);
					}
					finally
					{
						if (pr == null)
						{
							this.m_currentMaxSemCount = this.m_semMaxPoolSize.Release();
						}
					}
				}
				result = pr;
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)268437504, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)2560, new string[]
					{
						Trace.GetCPInfo(pr as OracleConnectionImpl, null, instanceName, null, false, false)
					});
				}
			}
			return result;
		}

		// Token: 0x060007D0 RID: 2000 RVA: 0x000507CC File Offset: 0x0004E9CC
		public virtual bool RemoveCheckedInPR(PR pr, bool bForce)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)2304, new string[0]);
			}
			bool result;
			try
			{
				bool flag = false;
				if (bForce)
				{
					Trace.Write(OracleTraceLevel.Private, OracleTraceTag.CP, new string[]
					{
						"PM.RemoveCheckedInPR() : Get Idle Sempahore "
					});
					if (this.m_semIdleResource.WaitOne(0))
					{
						Trace.Write(OracleTraceLevel.Private, OracleTraceTag.CP, new string[]
						{
							"PM.RemoveCheckedInPR() : Received Idle Sempahore "
						});
						try
						{
							if (pr.m_cp.m_cpQueuePR.Remove(pr))
							{
								pr.m_pm.m_pmListPR.Remove(pr);
								if (pr.m_cp != null)
								{
									pr.m_cp.m_cpListPR.Remove(pr);
								}
								flag = true;
							}
							goto IL_24F;
						}
						finally
						{
							if (!flag)
							{
								this.m_currentIdleSemCount = this.m_semIdleResource.Release();
								Trace.Write(OracleTraceLevel.Private, OracleTraceTag.CP, new string[]
								{
									string.Format("RemoveCheckedInPR() : Idle Sempahore count={0}", this.m_currentIdleSemCount)
								});
							}
						}
					}
					flag = false;
				}
				else
				{
					flag = false;
					if (this.m_pmListPR.Remove(pr, this.m_cs.m_minPoolSize))
					{
						Trace.Write(OracleTraceLevel.Private, OracleTraceTag.CP, new string[]
						{
							"PM.RemoveCheckedInPR() : Get Idle Sempahore "
						});
						if (this.m_semIdleResource.WaitOne(0))
						{
							Trace.Write(OracleTraceLevel.Private, OracleTraceTag.CP, new string[]
							{
								"PM.RemoveCheckedInPR() : Received Idle Sempahore "
							});
							try
							{
								if (pr.m_cp == null || pr.m_cp.m_cpQueuePR.Remove(pr))
								{
									if (pr.m_cp != null)
									{
										pr.m_cp.m_cpListPR.Remove(pr);
									}
									flag = true;
								}
								goto IL_24F;
							}
							finally
							{
								if (!flag)
								{
									this.m_currentIdleSemCount = this.m_semIdleResource.Release();
									Trace.Write(OracleTraceLevel.Private, OracleTraceTag.CP, new string[]
									{
										string.Format("RemoveCheckedInPR2() : Idle Sempahore count={0}", this.m_currentIdleSemCount)
									});
									this.m_pmListPR.Add(pr);
								}
							}
						}
						this.m_pmListPR.Add(pr);
						flag = false;
					}
					else
					{
						flag = false;
					}
				}
				IL_24F:
				result = flag;
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)268437504, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)2560, new string[]
					{
						Trace.GetCPInfo(pr as OracleConnectionImpl, null, null, null, false, false)
					});
				}
			}
			return result;
		}

		// Token: 0x060007D1 RID: 2001 RVA: 0x00050AD8 File Offset: 0x0004ECD8
		public virtual bool RemoveCheckedOutPR(PR pr, bool bForce)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)2304, new string[]
				{
					Trace.GetCPInfo(pr as OracleConnectionImpl, null, null, null, false, false)
				});
			}
			bool result;
			try
			{
				bool flag;
				if (bForce)
				{
					flag = this.m_pmListPR.Remove(pr);
				}
				else
				{
					flag = this.m_pmListPR.Remove(pr, this.m_cs.m_minPoolSize);
				}
				if (flag)
				{
					if (pr.m_cp != null)
					{
						pr.m_cp.m_cpListPR.Remove(pr);
					}
					result = true;
				}
				else
				{
					result = false;
				}
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)268437504, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)2560, new string[]
					{
						Trace.GetCPInfo(pr as OracleConnectionImpl, null, null, null, false, false)
					});
				}
			}
			return result;
		}

		// Token: 0x060007D2 RID: 2002 RVA: 0x00050BE0 File Offset: 0x0004EDE0
		internal virtual bool Close(PR pr, CriteriaCtx criteriaCtx)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)2304, new string[]
				{
					Trace.GetCPInfo(pr as OracleConnectionImpl, null, null, null, false, false)
				});
			}
			bool flag = false;
			bool result;
			try
			{
				if ((!pr.m_bCheckedOutByApp && !pr.m_bCheckedOutByDTC) || pr.m_deletionRequestor == DeletionRequestor.HA)
				{
					lock (pr)
					{
						if ((!pr.m_bCheckedOutByApp && !pr.m_bCheckedOutByDTC) || pr.m_deletionRequestor == DeletionRequestor.HA)
						{
							lock (this.m_syncPRClose)
							{
								if (!pr.m_cs.m_pooling)
								{
									flag = this.RemoveCheckedOutPR(pr, true);
								}
								else if (pr.m_deletionRequestor == DeletionRequestor.ConnectionLifetime)
								{
									flag = this.RemoveCheckedOutPR(pr, true);
								}
								else if (pr.m_deletionRequestor == DeletionRequestor.Put)
								{
									flag = this.RemoveCheckedOutPR(pr, true);
								}
								else if (pr.m_deletionRequestor == DeletionRequestor.PoolRegulator && pr.m_bPutCompleted)
								{
									flag = this.RemoveCheckedInPR(pr, false);
								}
								else if (pr.m_deletionRequestor == DeletionRequestor.ClearPool && pr.m_bPutCompleted)
								{
									flag = this.RemoveCheckedInPR(pr, true);
								}
								else if (pr.m_deletionRequestor == DeletionRequestor.HA)
								{
									flag = this.RemoveCheckedInPR(pr, true);
									bool flag5;
									bool flag4 = flag5 = flag;
									if (!flag)
									{
										flag = this.RemoveCheckedOutPR(pr, true);
										flag5 = true;
									}
									bool flag6 = false;
									if (pr != null)
									{
										flag6 = pr.IsTAFEnabled();
									}
									if (flag6)
									{
										Trace.Write(OracleTraceLevel.Private, OracleTraceTag.CP, new string[]
										{
											"PM.Close: TAF IS ENABLED"
										});
										if (pr.m_failoverOccured)
										{
											string preFailoverInstName = pr.m_preFailoverInstName;
											pr.m_preFailoverInstName = null;
											pr.m_failoverOccured = false;
											Trace.Write(OracleTraceLevel.Private, OracleTraceTag.CP, new string[]
											{
												string.Format("INSTANCE NAMES DO NOT MATCH{0}={1}", pr.m_instanceName, preFailoverInstName)
											});
											pr.m_cp = this.m_dictDictCP[pr.ServiceName][pr.m_instanceName];
											flag = false;
											if (flag5 && flag4)
											{
												pr.m_cp.PutNewPR(pr, true);
											}
											else if (flag5)
											{
												pr.m_cp.PutNewPR(pr, false);
											}
										}
									}
									if (pr.m_cp == null)
									{
										Trace.Write(OracleTraceLevel.Private, OracleTraceTag.CP, new string[]
										{
											"pr.m_cp is NULL"
										});
									}
								}
								if (flag)
								{
									pr.m_bPutCompleted = true;
								}
							}
						}
						if (flag)
						{
							if (pr.m_mtsTxnCtx != null)
							{
								try
								{
									(pr as OracleConnectionImpl).ResetMTSTxnCtx();
								}
								catch
								{
								}
							}
							pr.DisConnect(criteriaCtx);
						}
					}
				}
				result = flag;
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)268437504, ex, null);
				throw;
			}
			finally
			{
				if (flag && !pr.m_bClosedWithReplacement && this.m_cs.m_pooling)
				{
					this.m_currentMaxSemCount = this.m_semMaxPoolSize.Release();
				}
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)2560, new string[]
					{
						Trace.GetCPInfo(pr as OracleConnectionImpl, null, null, null, false, false) + "(pmListPR.count={0})",
						this.m_pmListPR.Count.ToString()
					});
				}
			}
			return result;
		}

		// Token: 0x060007D3 RID: 2003 RVA: 0x00051038 File Offset: 0x0004F238
		public virtual void Put(PR pr, CriteriaCtx criteriaCtx)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)2304, new string[]
				{
					Trace.GetCPInfo(pr as OracleConnectionImpl, null, null, null, false, false)
				});
			}
			try
			{
				bool flag = false;
				lock (pr)
				{
					pr.m_bPutCompleted = true;
					if (pr.m_deletionRequestor != DeletionRequestor.None || !pr.m_cs.m_pooling)
					{
						pr.m_deletionRequestor = DeletionRequestor.Put;
						flag = pr.m_pm.Close(pr, criteriaCtx);
					}
					else if (this.m_cs.m_connectionLifetime > 0 && DateTime.Now - pr.m_creationTime > this.m_cs.m_connectionLifetimeTimeSpan)
					{
						if (pr.m_deletionRequestor != DeletionRequestor.HA)
						{
							pr.m_deletionRequestor = DeletionRequestor.ConnectionLifetime;
						}
						flag = pr.m_pm.Close(pr, criteriaCtx);
					}
				}
				if (flag)
				{
					pr = default(PR);
				}
				if (pr != null)
				{
					bool flag3 = pr.IsTAFEnabled();
					if (flag3)
					{
						Trace.Write(OracleTraceLevel.Private, OracleTraceTag.CP, new string[]
						{
							"PM.Put: TAF IS ENABLED"
						});
						if (pr.m_failoverOccured)
						{
							string preFailoverInstName = pr.m_preFailoverInstName;
							pr.m_preFailoverInstName = null;
							pr.m_failoverOccured = false;
							Trace.Write(OracleTraceLevel.Private, OracleTraceTag.CP, new string[]
							{
								string.Format("INSTANCE NAMES DO NOT MATCH{0}={1}", pr.m_instanceName, preFailoverInstName)
							});
							if (this.m_dictDictCP[pr.ServiceName] != null)
							{
								pr.m_cp = this.m_dictDictCP[pr.ServiceName][pr.m_instanceName];
							}
						}
					}
					if (pr.m_cp == null)
					{
						Trace.Write(OracleTraceLevel.Private, OracleTraceTag.CP, new string[]
						{
							"pr.m_cp is NULL"
						});
					}
					if (pr.m_cp != null)
					{
						if (pr.m_pm.m_cs.m_drcpEnabled == DrcpType.True && pr.bDRCPServerProcessAttached)
						{
							pr.DetachServerProcess(null, false);
							pr.bDRCPServerProcessAttached = false;
							pr.bGotMatchingServerProcess = false;
						}
						pr.m_pm.m_criteriaMapper.GetId(pr as OracleConnectionImpl);
						string serviceName = pr.ServiceName;
						if (serviceName != null && serviceName != "null" && !serviceName.Equals(pr.m_cp.m_serviceName, StringComparison.InvariantCultureIgnoreCase))
						{
							this.CreateServiceCtx(pr);
							bool bForPoolPopulation = true;
							bool migratePR = true;
							this.AddPRToPool(pr, bForPoolPopulation, migratePR);
						}
						else
						{
							pr.m_cp.Put(pr);
						}
					}
					this.m_currentIdleSemCount = this.m_semIdleResource.Release();
					Trace.Write(OracleTraceLevel.Private, OracleTraceTag.CP, new string[]
					{
						string.Format("PM.Put() : Idle Sempahore count={0}", this.m_currentIdleSemCount)
					});
				}
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)268437504, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)2560, new string[]
					{
						Trace.GetCPInfo(pr as OracleConnectionImpl, null, null, "pm:put:reg2", false, false)
					});
				}
			}
		}

		// Token: 0x060007D4 RID: 2004 RVA: 0x00051454 File Offset: 0x0004F654
		public virtual void MarkAllPRsForDeletion(DateTime haEventUtcDateTime, bool isHAEvnt = false)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)2304, new string[0]);
			}
			try
			{
				List<CP> list = this.m_pmListCP.GetList();
				for (int i = 0; i < list.Count; i++)
				{
					CP cp = list[i];
					cp.m_lastHADownEventUtcDateTime = haEventUtcDateTime;
					cp.m_bInstanceDown = true;
					if (ProviderConfig.m_bTraceLevelPrivate)
					{
						string text = string.Format("[pm_id={0}][instance={1}][instance_down={2}][type=ha_down_event][state_changed={3}][down_event_utc={4}][last_down_event_utc={5}]", new object[]
						{
							cp.m_pm.m_id,
							cp.m_instanceName,
							cp.m_bInstanceDown,
							true,
							haEventUtcDateTime.ToString(),
							cp.m_lastHADownEventUtcDateTime.ToString()
						});
						Trace.Write(OracleTraceLevel.Private, OracleTraceTag.CP, new string[]
						{
							text
						});
					}
				}
				for (int j = 0; j < list.Count; j++)
				{
					CP cp2 = list[j];
					cp2.MarkAllPRsForDeletion(haEventUtcDateTime, isHAEvnt);
				}
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)268437504, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)2560, new string[0]);
				}
			}
		}

		// Token: 0x060007D5 RID: 2005 RVA: 0x000515F8 File Offset: 0x0004F7F8
		public virtual void ClearAllPools(PR pr, bool isHAEvnt = false)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)2304, new string[]
				{
					Trace.GetCPInfo(pr as OracleConnectionImpl, null, null, null, false, false)
				});
			}
			try
			{
				this.m_fullDescriptor = null;
				this.m_bResolveTnsAlias = true;
				if (pr == null && OracleConnectionDispenser<PM, CP, PR>.m_listDataSources != null)
				{
					OracleConnectionDispenser<PM, CP, PR>.m_listDataSources.Remove(this.m_cs.m_dataSource);
				}
				List<CP> list = this.m_pmListCP.GetList();
				for (int i = 0; i < list.Count; i++)
				{
					CP cp = list[i];
					cp.ClearPool(pr, isHAEvnt);
				}
				lock (this.m_defaultEditionLocker)
				{
					this.m_defaultEditionDict.Clear();
				}
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)268437504, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)2560, new string[]
					{
						Trace.GetCPInfo(pr as OracleConnectionImpl, null, null, null, false, false)
					});
				}
			}
		}

		// Token: 0x060007D6 RID: 2006 RVA: 0x00051740 File Offset: 0x0004F940
		internal PR GetEnlisted(ConnectionString csWithDiffOrNewPwd, bool bGetForApp, CriteriaCtx criteriaCtx)
		{
			string text = null;
			Transaction transaction = null;
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)2304, new string[]
				{
					Trace.GetCPInfo(null, null, null, null, false, false)
				});
			}
			if (this.m_cs.m_enlist == Enlist.True && Transaction.Current != null)
			{
				transaction = Transaction.Current;
				text = transaction.TransactionInformation.LocalIdentifier;
			}
			PR pr = default(PR);
			string text2 = null;
			TransactionContext<PM, CP, PR> transactionContext = null;
			PR result;
			try
			{
				if (this.m_cs.m_drcpEnabled == DrcpType.True && !string.IsNullOrEmpty(this.m_cs.m_proxyUserId) && !string.IsNullOrEmpty(this.m_cs.Password))
				{
					throw new InvalidOperationException(OracleStringResourceManager.GetErrorMesg(28150, new string[0]));
				}
				if (criteriaCtx != null && this.m_cs.m_drcpEnabled == DrcpType.False && !string.IsNullOrEmpty(criteriaCtx.m_connectionClass))
				{
					throw new InvalidOperationException(OracleStringResourceManager.GetErrorMesg(-7601, new string[]
					{
						"DRCPConnectionClass",
						"DRCP"
					}));
				}
				this.ProcessCriteriaCtx_EnlistedConnection(ref criteriaCtx);
				if (this.m_dictDictTxnCtx[text] == null)
				{
					lock (this.m_syncTxnCtx)
					{
						if (this.m_dictDictTxnCtx[text] == null)
						{
							this.m_dictDictTxnCtx[text] = new SyncDictionary<string, TransactionContext<PM, CP, PR>>();
						}
					}
				}
				if (!string.IsNullOrEmpty(criteriaCtx.m_serviceName) && (transactionContext = this.m_dictDictTxnCtx[text][criteriaCtx.m_serviceName]) == null)
				{
					lock (this.m_syncTxnCtx)
					{
						if (!string.IsNullOrEmpty(criteriaCtx.m_serviceName) && (transactionContext = this.m_dictDictTxnCtx[text][criteriaCtx.m_serviceName]) == null)
						{
							if (ProviderConfig.m_bTraceLevelPrivate)
							{
								Trace.Write(OracleTraceLevel.Private, OracleTraceTag.MTS, new string[]
								{
									string.Concat(new string[]
									{
										"(txnid=",
										text,
										", servicename= ",
										criteriaCtx.m_serviceName,
										") txnCtx not available (1)"
									})
								});
							}
							transactionContext = this.m_pmListTxnCtx.Dequeue();
							if (transactionContext == null)
							{
								transactionContext = new TransactionContext<PM, CP, PR>(this as PM, text);
							}
							else
							{
								transactionContext.ReInitialize(this as PM, transaction);
							}
							transactionContext.m_mtsTxnRM = transactionContext.GetRM(this.m_cs, criteriaCtx.m_serviceName, criteriaCtx.m_pdbName, transaction);
							transactionContext.m_affinityInstanceName = transactionContext.m_mtsTxnRM.m_txnAffInstanceName;
							transaction.TransactionCompleted += transactionContext.m_mtsTxnRM.MTSTransactionCompleted;
							transactionContext.m_mtsTxnRM.m_connStrs.AddIfNotExist(this.m_cs);
							this.m_dictDictTxnCtx[text][criteriaCtx.m_serviceName] = transactionContext;
						}
					}
				}
				if (transactionContext == null || string.IsNullOrEmpty(transactionContext.m_mtsTxnRM.m_txnAffInstanceName))
				{
					lock (this.m_dictDictTxnCtx[text])
					{
						if (transactionContext != null)
						{
							if (!string.IsNullOrEmpty(transactionContext.m_mtsTxnRM.m_txnAffInstanceName))
							{
								goto IL_61C;
							}
						}
						try
						{
							if (ProviderConfig.m_bTraceLevelPrivate)
							{
								Trace.Write(OracleTraceLevel.Private, OracleTraceTag.MTS, new string[]
								{
									"(txnid=" + text + ") First Connection to enlist.  No Txn Affinity yet"
								});
							}
							pr = this.Get(csWithDiffOrNewPwd, bGetForApp, criteriaCtx, null, true);
							if (string.IsNullOrEmpty(criteriaCtx.m_serviceName))
							{
								criteriaCtx.m_serviceName = pr.ServiceName;
							}
							if ((transactionContext = this.m_dictDictTxnCtx[text][criteriaCtx.m_serviceName]) == null)
							{
								lock (this.m_syncTxnCtx)
								{
									if ((transactionContext = this.m_dictDictTxnCtx[text][criteriaCtx.m_serviceName]) == null)
									{
										if (ProviderConfig.m_bTraceLevelPrivate)
										{
											Trace.Write(OracleTraceLevel.Private, OracleTraceTag.MTS, new string[]
											{
												string.Concat(new string[]
												{
													"(txnid=",
													text,
													", servicename= ",
													criteriaCtx.m_serviceName,
													") txnCtx not available (2)"
												})
											});
										}
										transactionContext = this.m_pmListTxnCtx.Dequeue();
										if (transactionContext == null)
										{
											transactionContext = new TransactionContext<PM, CP, PR>(this as PM, text);
										}
										else
										{
											transactionContext.ReInitialize(this as PM, transaction);
										}
										transactionContext.m_mtsTxnRM = transactionContext.GetRM(this.m_cs, criteriaCtx.m_serviceName, criteriaCtx.m_pdbName, transaction);
										transactionContext.m_affinityInstanceName = transactionContext.m_mtsTxnRM.m_txnAffInstanceName;
										transaction.TransactionCompleted += transactionContext.m_mtsTxnRM.MTSTransactionCompleted;
										transactionContext.m_mtsTxnRM.m_connStrs.AddIfNotExist(this.m_cs);
										this.m_dictDictTxnCtx[text][criteriaCtx.m_serviceName] = transactionContext;
									}
								}
							}
							MTSRMManager.CCPEnlistTransaction(pr as OracleConnectionImpl, transaction, criteriaCtx);
							transactionContext.m_affinityInstanceName = (transactionContext.m_mtsTxnRM.m_txnAffInstanceName = pr.m_instanceName);
							if (ProviderConfig.m_bTraceLevelPrivate)
							{
								Trace.Write(OracleTraceLevel.Private, OracleTraceTag.MTS, new string[]
								{
									string.Concat(new object[]
									{
										"(affinity) (txnid=",
										text,
										") (rmid=",
										transactionContext.m_mtsTxnRM.GetHashCode(),
										") (affinity=",
										transactionContext.m_mtsTxnRM.m_txnAffInstanceName,
										") (sessid=",
										pr.m_endUserSessionId,
										":",
										pr.m_endUserSerialNum,
										")"
									})
								});
							}
							return pr;
						}
						catch (Exception ex)
						{
							try
							{
								OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)6144, ex, null);
							}
							catch
							{
							}
							if (pr != null)
							{
								try
								{
									OracleConnectionDispenser<PM, CP, PR>.PutFromApp(pr, null);
								}
								catch
								{
								}
							}
							throw;
						}
						IL_61C:
						transactionContext.m_affinityInstanceName = transactionContext.m_mtsTxnRM.m_txnAffInstanceName;
						goto IL_650;
					}
				}
				transactionContext.m_affinityInstanceName = transactionContext.m_mtsTxnRM.m_txnAffInstanceName;
				IL_650:
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, OracleTraceTag.MTS, new string[]
					{
						string.Concat(new string[]
						{
							"(txnid=",
							text,
							") affinity is set to = ",
							transactionContext.m_affinityInstanceName,
							" using MTSTxnRM ",
							transactionContext.m_mtsTxnRM.m_RMGuid.ToString()
						})
					});
				}
				text2 = transactionContext.m_affinityInstanceName;
				pr = transactionContext.GetEnlisted(csWithDiffOrNewPwd, bGetForApp, criteriaCtx, text2, true);
				if (pr != null)
				{
					if (OraclePool.m_bPerfNumberOfFreeConnections)
					{
						OraclePool.PerformanceCounterDecrement(OraclePerfParams.CounterIndex.NumberOfFreeConnections, pr as OracleConnectionImpl, pr.m_cp as OraclePool);
					}
					if (ProviderConfig.m_bTraceLevelPrivate)
					{
						Trace.Write(OracleTraceLevel.Private, OracleTraceTag.MTS, new string[]
						{
							string.Concat(new object[]
							{
								"(txnid=",
								text,
								") affinity is set to = ",
								transactionContext.m_affinityInstanceName,
								" using enlisted Conn ID ",
								pr.m_endUserSessionId
							})
						});
					}
					result = pr;
				}
				else
				{
					MTSTxnBranch mtstxnBranch = null;
					try
					{
						mtstxnBranch = transactionContext.m_mtsTxnRM.GetTxnBranch(this.m_cs, transactionContext.m_affinityInstanceName);
						if (ProviderConfig.m_bTraceLevelPrivate)
						{
							Trace.Write(OracleTraceLevel.Private, OracleTraceTag.MTS, new string[]
							{
								string.Format("[GetTxnBranch] (1) (txnid={0}) (affinity={1}) (rmid={2}) (rmtxid={3}) (brid={4}) (brtxnid={5})", new object[]
								{
									text,
									transactionContext.m_affinityInstanceName,
									transactionContext.m_mtsTxnRM.GetHashCode(),
									transactionContext.m_mtsTxnRM.m_txnLocalID,
									mtstxnBranch.GetHashCode(),
									mtstxnBranch.m_txnLocalID
								})
							});
						}
						if (mtstxnBranch != null)
						{
							pr = this.Get(csWithDiffOrNewPwd, bGetForApp, criteriaCtx, transactionContext.m_affinityInstanceName, true);
							if (string.Compare(mtstxnBranch.m_dbInstance, pr.m_instanceName) != 0)
							{
								transactionContext.m_mtsTxnRM.ReleaseTxnBranch(mtstxnBranch);
								mtstxnBranch = transactionContext.m_mtsTxnRM.GetTxnBranch(this.m_cs, pr.m_instanceName);
								if (mtstxnBranch == null)
								{
									throw new OracleException(ResourceStringConstants.CON_MTS_ENLIST_FAIL, string.Empty, string.Empty, OracleStringResourceManager.GetErrorMesg(ResourceStringConstants.CON_MTS_ENLIST_FAIL, new string[0]));
								}
							}
							MTSRMManager.CCPEnlistTransaction(pr as OracleConnectionImpl, transaction, transactionContext.m_mtsTxnRM, mtstxnBranch);
							if (ProviderConfig.m_bTraceLevelPrivate)
							{
								Trace.Write(OracleTraceLevel.Private, OracleTraceTag.MTS, new string[]
								{
									string.Concat(new object[]
									{
										"(txnid=",
										text,
										") affinity is set to = ",
										transactionContext.m_affinityInstanceName,
										" using regular Conn ID ",
										pr.m_endUserSessionId
									})
								});
							}
							return pr;
						}
					}
					catch (Exception ex2)
					{
						try
						{
							OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)6144, ex2, null);
						}
						catch
						{
						}
						if (pr != null)
						{
							try
							{
								OracleConnectionDispenser<PM, CP, PR>.PutFromApp(pr, null);
							}
							catch
							{
							}
						}
						if (transactionContext != null && transactionContext.m_mtsTxnRM != null && mtstxnBranch != null)
						{
							try
							{
								transactionContext.m_mtsTxnRM.ReleaseTxnBranch(mtstxnBranch);
							}
							catch
							{
							}
						}
						throw;
					}
					try
					{
						pr = transactionContext.GetEnlisted(csWithDiffOrNewPwd, bGetForApp, criteriaCtx, null, false);
						if (pr != null)
						{
							if (OraclePool.m_bPerfNumberOfFreeConnections)
							{
								OraclePool.PerformanceCounterDecrement(OraclePerfParams.CounterIndex.NumberOfFreeConnections, pr as OracleConnectionImpl, pr.m_cp as OraclePool);
							}
							if (ProviderConfig.m_bTraceLevelPrivate)
							{
								Trace.Write(OracleTraceLevel.Private, OracleTraceTag.MTS, new string[]
								{
									string.Concat(new object[]
									{
										"(txnid=",
										text,
										") affinity is set to = ",
										transactionContext.m_affinityInstanceName,
										" using any enlisted Conn ID ",
										pr.m_endUserSessionId,
										"affinity is set to = ",
										transactionContext.m_affinityInstanceName
									})
								});
							}
							return pr;
						}
					}
					catch (Exception ex3)
					{
						try
						{
							OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)6144, ex3, null);
						}
						catch
						{
						}
						throw;
					}
					TxnBranchesByDBInst freeBranches = transactionContext.m_mtsTxnRM.GetFreeBranches(this.m_cs);
					if (freeBranches == null || freeBranches.BranchCount == 0)
					{
						if (ProviderConfig.m_bTraceLevelPrivate)
						{
							string str;
							if (freeBranches == null)
							{
								str = " freeTxnBranches = null";
							}
							else
							{
								str = " freeTxnBrancvhes.BranchCount = 0";
							}
							Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)4352, new string[]
							{
								Trace.GetCPInfo(null, null, null, "UnableToEnlist1", false, false) + str
							});
						}
						throw new OracleException(ResourceStringConstants.CON_MTS_ENLIST_FAIL, string.Empty, string.Empty, OracleStringResourceManager.GetErrorMesg(ResourceStringConstants.CON_MTS_ENLIST_FAIL, new string[0]));
					}
					List<string> keys = freeBranches.GetKeys();
					for (int i = 0; i < keys.Count; i++)
					{
						text2 = keys[i];
						if (freeBranches.DequeueBranch(text2, out mtstxnBranch))
						{
							try
							{
								pr = this.Get(csWithDiffOrNewPwd, bGetForApp, criteriaCtx, text2, true);
								if (pr != null)
								{
									MTSRMManager.CCPEnlistTransaction(pr as OracleConnectionImpl, transaction, transactionContext.m_mtsTxnRM, mtstxnBranch);
								}
								if (ProviderConfig.m_bTraceLevelPrivate)
								{
									Trace.Write(OracleTraceLevel.Private, OracleTraceTag.MTS, new string[]
									{
										string.Concat(new object[]
										{
											"(txnid=",
											text,
											") affinity is set to = ",
											transactionContext.m_affinityInstanceName,
											" using Conn ID ",
											pr.m_endUserSessionId,
											" with any free TxnBranch without txnAffinity = ",
											keys[i]
										})
									});
								}
								return pr;
							}
							catch (Exception ex4)
							{
								try
								{
									OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)6144, ex4, null);
								}
								catch
								{
								}
								if (pr != null)
								{
									try
									{
										OracleConnectionDispenser<PM, CP, PR>.PutFromApp(pr, null);
									}
									catch
									{
									}
								}
								if (mtstxnBranch != null)
								{
									try
									{
										freeBranches.EnqueueBranch(text2, mtstxnBranch);
									}
									catch
									{
									}
								}
								throw;
							}
						}
					}
					if (ProviderConfig.m_bTraceLevelPrivate)
					{
						Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)4352, new string[]
						{
							Trace.GetCPInfo(null, null, null, "UnableToEnlist2", false, false) + " no free branch or connect to enlist"
						});
					}
					throw new OracleException(ResourceStringConstants.CON_MTS_ENLIST_FAIL, string.Empty, string.Empty, OracleStringResourceManager.GetErrorMesg(ResourceStringConstants.CON_MTS_ENLIST_FAIL, new string[0]));
				}
			}
			catch (Exception ex5)
			{
				OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)268437504, ex5, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)2560, new string[]
					{
						Trace.GetCPInfo(pr as OracleConnectionImpl, null, null, null, false, false)
					});
				}
			}
			return result;
		}

		// Token: 0x060007D7 RID: 2007 RVA: 0x00052608 File Offset: 0x00050808
		internal PR GetEnlisted(Transaction txn, string affinityInstance, int branchNum, bool bMustMatch, out bool bMatchFound, string serviceName, string pdbName)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)2304, new string[]
				{
					Trace.GetCPInfo(null, txn, affinityInstance, null, false, false) + "PM.GetEnlisted(aff={0};br={1};must={2})",
					affinityInstance,
					branchNum.ToString(),
					bMustMatch.ToString().Substring(0, 1)
				});
			}
			PR pr = default(PR);
			string localIdentifier = txn.TransactionInformation.LocalIdentifier;
			bMatchFound = false;
			TransactionContext<PM, CP, PR> transactionContext = null;
			PR result;
			try
			{
				if (this.m_dictDictTxnCtx[localIdentifier] != null)
				{
					transactionContext = this.m_dictDictTxnCtx[localIdentifier][serviceName];
				}
				if (transactionContext != null)
				{
					pr = transactionContext.GetEnlisted(affinityInstance, branchNum, bMustMatch, out bMatchFound);
					if (pr != null)
					{
						return pr;
					}
				}
				else
				{
					lock (this.m_syncTxnCtx)
					{
						if (this.m_dictDictTxnCtx[localIdentifier] == null || this.m_dictDictTxnCtx[localIdentifier][serviceName] == null)
						{
							transactionContext = this.m_pmListTxnCtx.Dequeue();
							if (transactionContext == null)
							{
								transactionContext = new TransactionContext<PM, CP, PR>(this as PM, localIdentifier);
							}
							else
							{
								transactionContext.ReInitialize(this as PM, txn);
							}
							transactionContext.m_mtsTxnRM = transactionContext.GetRM(this.m_cs, serviceName, pdbName, txn);
							transactionContext.m_affinityInstanceName = transactionContext.m_mtsTxnRM.m_txnAffInstanceName;
							transactionContext.m_mtsTxnRM.m_connStrs.AddIfNotExist(this.m_cs);
							txn.TransactionCompleted += transactionContext.m_mtsTxnRM.MTSTransactionCompleted;
							if (this.m_dictDictTxnCtx[localIdentifier] == null)
							{
								this.m_dictDictTxnCtx[localIdentifier] = new SyncDictionary<string, TransactionContext<PM, CP, PR>>();
							}
							this.m_dictDictTxnCtx[localIdentifier][serviceName] = transactionContext;
						}
					}
				}
				if (bMustMatch)
				{
					result = default(PR);
				}
				else
				{
					CriteriaCtx criteriaCtx = new CriteriaCtx();
					criteriaCtx.m_serviceName = serviceName;
					criteriaCtx.m_pdbName = pdbName;
					criteriaCtx.m_fromMTS = true;
					pr = this.Get(this.m_cs, false, criteriaCtx, affinityInstance, false);
					if (pr != null)
					{
						pr.m_resPoolRefCount++;
					}
					result = pr;
				}
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)268437504, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)2560, new string[]
					{
						Trace.GetCPInfo(pr as OracleConnectionImpl, txn, affinityInstance, null, false, false) + "PM.GetEnlisted(aff={0};br={1};must={2}) return (matchfound={3})",
						affinityInstance,
						branchNum.ToString(),
						bMustMatch.ToString().Substring(0, 1),
						bMatchFound.ToString().Substring(0, 1)
					});
				}
			}
			return result;
		}

		// Token: 0x060007D8 RID: 2008 RVA: 0x00052910 File Offset: 0x00050B10
		internal void InitializePSPEConn(Transaction txn, PR pr)
		{
			TransactionContext<PM, CP, PR> transactionContext = null;
			if (this.m_dictDictTxnCtx != null && this.m_dictDictTxnCtx.ContainsKey(txn.TransactionInformation.LocalIdentifier))
			{
				transactionContext = this.m_dictDictTxnCtx[txn.TransactionInformation.LocalIdentifier][pr.ServiceName];
			}
			if (transactionContext != null)
			{
				lock (transactionContext)
				{
					transactionContext.m_enlistedPRList[0] = pr;
					transactionContext.m_instances.Add(pr.m_instanceName);
					pr.m_bTxnCtxPrimaryCon = true;
				}
			}
		}

		// Token: 0x060007D9 RID: 2009 RVA: 0x000529C4 File Offset: 0x00050BC4
		internal MTSTxnRM GetRM(Transaction txn, CriteriaCtx dbCriteriaCtx, PR pr)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)2304, new string[0]);
			}
			MTSTxnRM mtsTxnRM;
			try
			{
				string localIdentifier = txn.TransactionInformation.LocalIdentifier;
				TransactionContext<PM, CP, PR> transactionContext = null;
				if (this.m_dictDictTxnCtx[localIdentifier] != null)
				{
					transactionContext = this.m_dictDictTxnCtx[localIdentifier][pr.ServiceName];
				}
				if (transactionContext == null)
				{
					lock (this.m_syncTxnCtx)
					{
						if (this.m_dictDictTxnCtx[localIdentifier] != null)
						{
							transactionContext = this.m_dictDictTxnCtx[localIdentifier][pr.ServiceName];
						}
						if (transactionContext == null)
						{
							transactionContext = this.m_pmListTxnCtx.Dequeue();
							if (transactionContext == null)
							{
								transactionContext = new TransactionContext<PM, CP, PR>(this as PM, localIdentifier);
							}
							else
							{
								transactionContext.ReInitialize(this as PM, txn);
							}
							transactionContext.m_mtsTxnRM = transactionContext.GetRM(this.m_cs, dbCriteriaCtx.m_serviceName, dbCriteriaCtx.m_pdbName, txn);
							transactionContext.m_mtsTxnRM.m_connStrs.AddIfNotExist(this.m_cs);
							txn.TransactionCompleted += transactionContext.m_mtsTxnRM.MTSTransactionCompleted;
							if (ProviderConfig.m_bTraceLevelPrivate)
							{
								Trace.Write(OracleTraceLevel.Private, OracleTraceTag.MTS, new string[]
								{
									string.Concat(new object[]
									{
										"(affinity) (txnid=",
										localIdentifier,
										") (rmid=",
										transactionContext.m_mtsTxnRM.GetHashCode(),
										") (affinity=",
										transactionContext.m_mtsTxnRM.m_txnAffInstanceName,
										") (sessid=",
										pr.m_endUserSessionId,
										":",
										pr.m_endUserSerialNum,
										")"
									})
								});
							}
							if (this.m_dictDictTxnCtx[localIdentifier] == null)
							{
								this.m_dictDictTxnCtx[localIdentifier] = new SyncDictionary<string, TransactionContext<PM, CP, PR>>();
							}
							this.m_dictDictTxnCtx[localIdentifier][pr.ServiceName] = transactionContext;
						}
					}
				}
				mtsTxnRM = transactionContext.m_mtsTxnRM;
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)268437504, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)2560, new string[0]);
				}
			}
			return mtsTxnRM;
		}

		// Token: 0x060007DA RID: 2010 RVA: 0x00052C7C File Offset: 0x00050E7C
		internal void RemoveRM(string serviceName, Transaction txn)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)2304, new string[0]);
			}
			try
			{
				TransactionContext<PM, CP, PR> transactionContext = null;
				string localIdentifier = txn.TransactionInformation.LocalIdentifier;
				if (localIdentifier != null)
				{
					try
					{
						if (this.m_dictDictTxnCtx[localIdentifier] != null)
						{
							lock (this.m_syncTxnCtx)
							{
								if (this.m_dictDictTxnCtx[localIdentifier] != null)
								{
									transactionContext = this.m_dictDictTxnCtx[localIdentifier][serviceName];
								}
								goto IL_C4;
							}
						}
						if (ProviderConfig.m_bTraceLevelPrivate)
						{
							Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)2304, new string[]
							{
								string.Format("Local txn id not found: {0}, pmid:{1}, threadid:{2}", txn.TransactionInformation.LocalIdentifier, this.m_id, Thread.CurrentThread.ManagedThreadId)
							});
						}
						IL_C4:;
					}
					catch
					{
					}
					try
					{
						if (this.m_dictDictTxnCtx[localIdentifier] != null)
						{
							lock (this.m_syncTxnCtx)
							{
								if (this.m_dictDictTxnCtx[localIdentifier] != null)
								{
									this.m_dictDictTxnCtx[localIdentifier].Remove(serviceName);
									if (ProviderConfig.m_bTraceLevelPrivate)
									{
										Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)2304, new string[]
										{
											string.Format("Removing the txn ctx. Local txn id: {0}, pmid:{1}, service:{2}, threadid:{3}", new object[]
											{
												localIdentifier,
												this.m_id,
												serviceName,
												Thread.CurrentThread.ManagedThreadId
											})
										});
									}
									if (this.m_dictDictTxnCtx[localIdentifier].Count == 0)
									{
										this.m_dictDictTxnCtx.Remove(localIdentifier);
										if (ProviderConfig.m_bTraceLevelPrivate)
										{
											Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)2304, new string[]
											{
												string.Format("Removing the txn ctx cache. Local txn id: {0}, pmid:{1}, threadid:{2}", localIdentifier, this.m_id, Thread.CurrentThread.ManagedThreadId)
											});
										}
									}
								}
								goto IL_218;
							}
						}
						if (ProviderConfig.m_bTraceLevelPrivate)
						{
							Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)2304, new string[]
							{
								string.Format("Local txn id not found: {0}, pmid:{1}, threadid:{2}", localIdentifier, this.m_id, Thread.CurrentThread.ManagedThreadId)
							});
						}
						IL_218:;
					}
					catch (Exception ex)
					{
						if (ProviderConfig.m_bTraceLevelPrivate)
						{
							Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)2304, new string[]
							{
								ex.ToString()
							});
						}
					}
				}
				if (transactionContext != null)
				{
					try
					{
						transactionContext.RemoveRM();
					}
					catch
					{
					}
					transactionContext.m_mtsTxnRM = null;
					transactionContext.m_localTxnId = null;
					this.m_pmListTxnCtx.Enqueue(transactionContext);
				}
			}
			catch (Exception ex2)
			{
				OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)268437504, ex2, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)2560, new string[0]);
				}
			}
		}

		// Token: 0x060007DB RID: 2011 RVA: 0x00052FD4 File Offset: 0x000511D4
		private void RLBGravitateThreadFunc(object state)
		{
			int num = 0;
			bool flag = false;
			string connectionClass = null;
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)2304, new string[0]);
			}
			try
			{
				object[] array = state as object[];
				num = (int)array[0];
				string text = (string)array[1];
				connectionClass = (string)array[2];
				if (text == null)
				{
					flag = true;
					if (ProviderConfig.m_bTraceLevelPrivate)
					{
						Trace.Write(OracleTraceLevel.Private, OracleTraceTag.CP, new string[]
						{
							num.ToString() + " connections to be unpopulated by the regulator thread"
						});
					}
				}
				else if (num == 0 && ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, OracleTraceTag.CP, new string[]
					{
						"called due to misses"
					});
				}
				CP cp = default(CP);
				float num2 = -1f;
				List<string> list = null;
				if (text != null)
				{
					list.Add(text);
				}
				else
				{
					list = this.m_dictSvcCtx.GetKeys();
				}
				foreach (string text2 in list)
				{
					if (this.m_dictSvcCtx[text2] != null)
					{
						string id = (this.m_dictSvcCtx[text2].m_databaseName + "|" + text2).ToLowerInvariant();
						RLB rlb = RLBManager.Get(id);
						if (rlb != null && this.m_pmListPR.Count > 0)
						{
							for (int i = 0; i < rlb.m_instances.Length; i++)
							{
								string text3 = rlb.m_instances[i];
								text = rlb.m_service;
								if (text3 != null)
								{
									CP cp2 = this.m_dictDictCP[text][text3];
									if (cp2 != null)
									{
										float num3 = (float)(cp2.m_cpListPR.Count * 100 / this.m_pmListPR.Count);
										float num4 = (float)rlb.m_rlbPercentages[i];
										float num5 = num3 - num4;
										if (num5 > num2)
										{
											cp = cp2;
											num2 = num5;
										}
									}
								}
							}
						}
					}
				}
				if (cp != null && cp.m_cpListPR.Count > 0 && num2 > 5f)
				{
					if (!flag)
					{
						int count = cp.m_cpListPR.Count;
						num = (int)(0.15f * (float)count);
						if (num == 0)
						{
							num = Math.Min(5, count - 1);
						}
					}
					if (num < 1)
					{
						goto IL_43D;
					}
					if (!flag)
					{
						if (ProviderConfig.m_bTraceLevelPrivate)
						{
							Trace.Write(OracleTraceLevel.Private, OracleTraceTag.CP, new string[]
							{
								string.Concat(new object[]
								{
									"RLBGravitateThreadFunc(instance=",
									cp.m_instanceName,
									"; close request=",
									num,
									");"
								})
							});
						}
						PoolPopulationOption state2 = new PoolPopulationOption(num, num + this.m_pmListPR.Count, null, false, connectionClass);
						ThreadPool.QueueUserWorkItem(new WaitCallback(this.PopulatePoolThreadFunc), state2);
						goto IL_43D;
					}
					UnPopulatePoolArgs state3 = new UnPopulatePoolArgs(cp.m_serviceName, cp.m_instanceName, 1);
					PoolPopulationOption state4 = new PoolPopulationOption(1, 1 + this.m_pmListPR.Count, null, false, connectionClass);
					int num6 = 0;
					int num7 = 0;
					try
					{
						int num8 = 0;
						while (num8 < num && this.UnPopulatePool(state3) > 0)
						{
							num6++;
							if (this.PopulatePool(state4) == 0)
							{
								break;
							}
							num7++;
							num8++;
						}
						goto IL_43D;
					}
					finally
					{
						if (ProviderConfig.m_bTraceLevelPrivate)
						{
							Trace.Write(OracleTraceLevel.Private, OracleTraceTag.CP, new string[]
							{
								string.Concat(new object[]
								{
									"(RLB) (GRAV) RLBGravitateThreadFunc(attempt to close=",
									num,
									"; closed=",
									num6,
									"; opened=",
									num7,
									")"
								})
							});
						}
					}
				}
				if (num > 0 && flag)
				{
					ThreadPool.QueueUserWorkItem(new WaitCallback(this.UnPopulatePoolThreadFunc), new UnPopulatePoolArgs(null, null, num));
				}
				else if (!flag && ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, OracleTraceTag.CP, new string[]
					{
						"(RLB) RLBGravitate() : no gravitation; max distribution dif = " + num2
					});
				}
				IL_43D:;
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)268437504, ex, null);
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)2560, new string[0]);
				}
			}
		}

		// Token: 0x170001DC RID: 476
		// (get) Token: 0x060007DC RID: 2012 RVA: 0x000534B4 File Offset: 0x000516B4
		internal string Password
		{
			get
			{
				if (this.m_cs.m_securedPassword == null)
				{
					return string.Empty;
				}
				SecureString secureString = null;
				bool flag = this.m_cs.m_secPwdList.Dequeue(out secureString);
				if (flag)
				{
					string stringFromSecureString = ConnectionString.GetStringFromSecureString(secureString);
					this.m_cs.m_secPwdList.Enqueue(secureString);
					return stringFromSecureString;
				}
				string stringFromSecureString2 = ConnectionString.GetStringFromSecureString(this.m_cs.m_securedPassword);
				secureString = new SecureString();
				for (int i = 0; i < stringFromSecureString2.Length; i++)
				{
					secureString.AppendChar(stringFromSecureString2[i]);
				}
				this.m_cs.m_secPwdList.Enqueue(secureString);
				return stringFromSecureString2;
			}
		}

		// Token: 0x170001DD RID: 477
		// (get) Token: 0x060007DD RID: 2013 RVA: 0x00053554 File Offset: 0x00051754
		internal string ProxyPassword
		{
			get
			{
				if (this.m_cs.m_securedProxyPassword == null)
				{
					return string.Empty;
				}
				SecureString secureString = null;
				bool flag = this.m_cs.m_secPxyPwdList.Dequeue(out secureString);
				if (flag)
				{
					string stringFromSecureString = ConnectionString.GetStringFromSecureString(secureString);
					this.m_cs.m_secPxyPwdList.Enqueue(secureString);
					return stringFromSecureString;
				}
				string stringFromSecureString2 = ConnectionString.GetStringFromSecureString(this.m_cs.m_securedProxyPassword);
				secureString = new SecureString();
				for (int i = 0; i < stringFromSecureString2.Length; i++)
				{
					secureString.AppendChar(stringFromSecureString2[i]);
				}
				this.m_cs.m_secPxyPwdList.Enqueue(secureString);
				return stringFromSecureString2;
			}
		}

		// Token: 0x060007DE RID: 2014 RVA: 0x000535F4 File Offset: 0x000517F4
		internal void RemoveAndRedistribute(RLB rlb, CP cp, int indexToBeRemoved)
		{
			try
			{
				DateTime now = DateTime.Now;
				if (!rlb.m_removedInstances.Contains(rlb.m_instances[indexToBeRemoved]))
				{
					int[] array = (int[])rlb.m_percentages.Clone();
					int num = 0;
					for (int i = array.Length - 1; i >= 0; i--)
					{
						if (array[i] > 0)
						{
							num = array[i];
							break;
						}
					}
					int num2;
					if (indexToBeRemoved > 0)
					{
						num2 = array[indexToBeRemoved] - array[indexToBeRemoved - 1];
					}
					else
					{
						num2 = array[0];
					}
					int num3 = num - num2;
					if (num3 > 0)
					{
						int num4 = 0;
						for (int j = 0; j < array.Length; j++)
						{
							if (j == indexToBeRemoved)
							{
								num4 = array[j];
								if (j > 0)
								{
									array[j] = array[j - 1];
								}
								else
								{
									array[j] = 0;
								}
							}
							else
							{
								int num5;
								if (j > 0)
								{
									num5 = array[j] - num4;
								}
								else
								{
									num5 = array[j];
								}
								num4 = array[j];
								array[j] = (int)((float)num5 / (float)num3 * 10000f);
								if (j > 0)
								{
									array[j] += array[j - 1];
								}
							}
						}
						lock (rlb.m_syncObject)
						{
							if (!cp.HasValidConnections)
							{
								if (!(now < rlb.m_lastUpdateTime))
								{
									rlb.m_lastUpdateTime = now;
									if (!rlb.m_removedInstances.Contains(rlb.m_instances[indexToBeRemoved]))
									{
										rlb.m_removedInstances.Add(rlb.m_instances[indexToBeRemoved]);
										rlb.m_percentages = array;
									}
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

		// Token: 0x04000A58 RID: 2648
		private const int MAX_RLB_MISS_COUNT = 2000;

		// Token: 0x04000A59 RID: 2649
		public SyncDictionary<string, SyncDictionary<string, CP>> m_dictDictCP;

		// Token: 0x04000A5A RID: 2650
		public SyncDictionary<string, SyncDictionary<string, TransactionContext<PM, CP, PR>>> m_dictDictTxnCtx;

		// Token: 0x04000A5B RID: 2651
		public SyncQueueList<CP> m_pmListCP;

		// Token: 0x04000A5C RID: 2652
		public SyncQueueList<PR> m_pmListPR;

		// Token: 0x04000A5D RID: 2653
		public SyncQueueList<CP> m_pmDeactivatedCP;

		// Token: 0x04000A5E RID: 2654
		public SyncQueueList<TransactionContext<PM, CP, PR>> m_pmListTxnCtx;

		// Token: 0x04000A5F RID: 2655
		public SyncDictionary<string, ServiceCtx> m_dictSvcCtx;

		// Token: 0x04000A60 RID: 2656
		private static ReaderWriterLockSlim m_serviceDomainDictLocker = new ReaderWriterLockSlim();

		// Token: 0x04000A61 RID: 2657
		private static SyncDictionary<string, string> m_serviceDSDict = new SyncDictionary<string, string>();

		// Token: 0x04000A62 RID: 2658
		private static SyncDictionary<string, string> m_domainDSDict = new SyncDictionary<string, string>();

		// Token: 0x04000A63 RID: 2659
		internal SyncDictionary<string, string> m_defaultEditionDict = new SyncDictionary<string, string>();

		// Token: 0x04000A64 RID: 2660
		internal object m_syncObjForDefaultSvcRelocWait = new object();

		// Token: 0x04000A65 RID: 2661
		public ConnectionString m_cs;

		// Token: 0x04000A66 RID: 2662
		internal bool m_bHAEnabled;

		// Token: 0x04000A67 RID: 2663
		internal bool m_bUsingSEPSCredentials;

		// Token: 0x04000A68 RID: 2664
		internal bool m_bSEPSForProxyCredentials;

		// Token: 0x04000A69 RID: 2665
		internal bool m_bSEPSCredentialsFetched;

		// Token: 0x04000A6A RID: 2666
		private object m_creationSync;

		// Token: 0x04000A6B RID: 2667
		private object m_syncPRClose;

		// Token: 0x04000A6C RID: 2668
		private object m_txnAffinityLock;

		// Token: 0x04000A6D RID: 2669
		private object m_syncTxnCtx;

		// Token: 0x04000A6E RID: 2670
		internal Timer m_timer;

		// Token: 0x04000A6F RID: 2671
		internal bool m_bDefaultsFetched;

		// Token: 0x04000A70 RID: 2672
		internal string m_databaseDomainName;

		// Token: 0x04000A71 RID: 2673
		internal string m_conStrServiceName;

		// Token: 0x04000A72 RID: 2674
		internal string m_conStrPdbName;

		// Token: 0x04000A73 RID: 2675
		public int m_drain_timeout;

		// Token: 0x04000A74 RID: 2676
		internal object m_conStrDefaultsLocker = new object();

		// Token: 0x04000A75 RID: 2677
		internal object m_defaultEditionLocker = new object();

		// Token: 0x04000A76 RID: 2678
		public Semaphore m_semIdleResource;

		// Token: 0x04000A77 RID: 2679
		public Semaphore m_semMaxPoolSize;

		// Token: 0x04000A78 RID: 2680
		public Semaphore m_semIncrPoolSize;

		// Token: 0x04000A79 RID: 2681
		public WaitHandle[] m_IdleAndIncrHandles;

		// Token: 0x04000A7A RID: 2682
		public WaitHandle[] m_IdleAndMaxHandles;

		// Token: 0x04000A7B RID: 2683
		public Semaphore m_semPoolPopulation;

		// Token: 0x04000A7C RID: 2684
		public string m_serverVersion;

		// Token: 0x04000A7D RID: 2685
		public string m_fullDescriptor;

		// Token: 0x04000A7E RID: 2686
		public CP m_pmSingleCP;

		// Token: 0x04000A7F RID: 2687
		public TimeSpan m_timeoutValue;

		// Token: 0x04000A80 RID: 2688
		public string m_id;

		// Token: 0x04000A81 RID: 2689
		private int m_maxPoolSize;

		// Token: 0x04000A82 RID: 2690
		internal bool m_bSelfTuningDisabled;

		// Token: 0x04000A83 RID: 2691
		public int m_rlbMissCount;

		// Token: 0x04000A84 RID: 2692
		private int m_currentMaxSemCount;

		// Token: 0x04000A85 RID: 2693
		private int m_currentIncrSemCount;

		// Token: 0x04000A86 RID: 2694
		private int m_currentIdleSemCount;

		// Token: 0x04000A87 RID: 2695
		private bool m_bResolveTnsAlias = true;

		// Token: 0x04000A88 RID: 2696
		internal bool? m_bNAEInUse = null;

		// Token: 0x04000A89 RID: 2697
		private IntPtr pShardCtx = IntPtr.Zero;

		// Token: 0x04000A8A RID: 2698
		internal CriteriaMapper m_criteriaMapper;

		// Token: 0x04000A8B RID: 2699
		internal static string[] m_criteriaArray;

		// Token: 0x04000A8C RID: 2700
		internal static int[] m_criteriaToCritTypeArray;

		// Token: 0x04000A8D RID: 2701
		internal static int[] m_criteriaToCritTypeForDTXN;

		// Token: 0x020000C8 RID: 200
		internal enum CriteriaNames
		{
			// Token: 0x04000A8F RID: 2703
			ConnectionClass,
			// Token: 0x04000A90 RID: 2704
			Edition,
			// Token: 0x04000A91 RID: 2705
			TagName
		}

		// Token: 0x020000C9 RID: 201
		internal enum CriteriaTypes
		{
			// Token: 0x04000A93 RID: 2707
			MustMatchCriteria,
			// Token: 0x04000A94 RID: 2708
			AlterableSessionCriteria,
			// Token: 0x04000A95 RID: 2709
			TagCriteria
		}
	}
}
