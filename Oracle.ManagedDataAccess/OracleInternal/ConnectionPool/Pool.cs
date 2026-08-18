using System;
using System.Collections.Generic;
using Oracle.ManagedDataAccess.Client;
using OracleInternal.Common;
using OracleInternal.ServiceObjects;

namespace OracleInternal.ConnectionPool
{
	// Token: 0x020000CA RID: 202
	internal class Pool<PM, CP, PR> where PM : PoolManager<PM, CP, PR>, new() where CP : Pool<PM, CP, PR>, new() where PR : PoolResource<PM, CP, PR>, new()
	{
		// Token: 0x170001DE RID: 478
		// (get) Token: 0x060007DF RID: 2015 RVA: 0x000537B8 File Offset: 0x000519B8
		internal bool HasValidConnections
		{
			get
			{
				return this.m_cpListPR.Count > 0;
			}
		}

		// Token: 0x060007E0 RID: 2016 RVA: 0x000537C8 File Offset: 0x000519C8
		public Pool()
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)2304, new string[0]);
			}
			try
			{
				this.m_cpListPR = new SyncQueueList<PR>(int.MaxValue);
				this.m_cpQueuePR = new SyncQueueList<PR>(int.MaxValue);
				this.m_lastHADownEventUtcDateTime = DateTime.MinValue;
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

		// Token: 0x060007E1 RID: 2017 RVA: 0x00053868 File Offset: 0x00051A68
		public virtual void PutNewPR(PR pr, bool bForPoolPopulation)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)2304, new string[0]);
			}
			try
			{
				bool flag = false;
				if (this.m_bInstanceDown)
				{
					DateTimeOffset dateTimeOffset = new DateTimeOffset(pr.m_creationTime, pr.m_sessionTimeZone.Value);
					DateTime utcDateTime = dateTimeOffset.UtcDateTime;
					if (utcDateTime > pr.m_cp.m_lastHADownEventUtcDateTime)
					{
						flag = true;
						this.m_bInstanceDown = false;
					}
					if (ProviderConfig.m_bTraceLevelPrivate)
					{
						string text = string.Format("[pm_id={0}][instance={1}][instance_down={2}][type=new_connection][state_changed={3}][con_creation_time_utc={4}][last_down_event_utc={5}]", new object[]
						{
							this.m_pm.m_id,
							this.m_instanceName,
							this.m_bInstanceDown,
							flag,
							utcDateTime.ToString(),
							this.m_lastHADownEventUtcDateTime.ToString()
						});
						Trace.Write(OracleTraceLevel.Private, OracleTraceTag.CP, new string[]
						{
							text
						});
					}
				}
				this.m_cpListPR.Add(pr);
				if (bForPoolPopulation)
				{
					this.m_cpQueuePR.Add(pr);
					if (ProviderConfig.m_bTraceLevelPrivate)
					{
						Trace.Write(OracleTraceLevel.Private, OracleTraceTag.CP, new string[]
						{
							Trace.GetCPInfo(pr as OracleConnectionImpl, null, null, "cp:put:reg1", false, false)
						});
					}
				}
				pr.m_cp = (CP)((object)this);
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

		// Token: 0x060007E2 RID: 2018 RVA: 0x00053A48 File Offset: 0x00051C48
		public virtual void Put(PR pr)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)2304, new string[0]);
			}
			try
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
				this.m_cpQueuePR.Enqueue(pr);
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, OracleTraceTag.CP, new string[]
					{
						Trace.GetCPInfo(pr as OracleConnectionImpl, null, null, "cp:put:reg2", false, false)
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
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)2560, new string[0]);
				}
			}
		}

		// Token: 0x060007E3 RID: 2019 RVA: 0x00053B28 File Offset: 0x00051D28
		internal static void MatchCriteria(PR pr, ref PR bestMatchingPR, ref uint bitRepForBestMatchPR, CriteriaCtx criteriaCtx, bool prForDTXN)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)2304, new string[0]);
			}
			uint num = 0U;
			uint num2 = 268435456U;
			bool bBestMatchPRHasAllMustCrit = true;
			try
			{
				if (pr != null)
				{
					criteriaCtx.m_bfoundPRMatchingAllCrit = true;
					for (int i = 0; i < PoolManager<OraclePoolManager, OraclePool, OracleConnectionImpl>.m_criteriaToCritTypeArray.Length; i++)
					{
						uint num3 = pr.m_criteriaIds[i];
						uint num4 = criteriaCtx.m_criteriaIds[i];
						if (num3 == num4)
						{
							num |= num2;
						}
						else
						{
							criteriaCtx.m_bfoundPRMatchingAllCrit = false;
							if (((!prForDTXN) ? PoolManager<OraclePoolManager, OraclePool, OracleConnectionImpl>.m_criteriaToCritTypeArray[i] : PoolManager<OraclePoolManager, OraclePool, OracleConnectionImpl>.m_criteriaToCritTypeForDTXN[i]) == 0)
							{
								return;
							}
						}
						num2 >>= 1;
					}
					if (num > bitRepForBestMatchPR || criteriaCtx.m_bfoundPRMatchingAllCrit || bestMatchingPR == null)
					{
						bestMatchingPR = pr;
						bitRepForBestMatchPR = num;
						criteriaCtx.m_bBestMatchPRHasAllMustCrit = bBestMatchPRHasAllMustCrit;
					}
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

		// Token: 0x060007E4 RID: 2020 RVA: 0x00053C4C File Offset: 0x00051E4C
		internal void GetMatchingPR(ref PR bestMatchingPR, CriteriaCtx criteriaCtx)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)2304, new string[0]);
			}
			try
			{
				PR pr = default(PR);
				uint num = 0U;
				lock (this.m_cpQueuePR.m_sync)
				{
					for (int i = 0; i < this.m_cpQueuePR.m_list.Count; i++)
					{
						pr = this.m_cpQueuePR.m_list[i];
						Pool<PM, CP, PR>.MatchCriteria(pr, ref bestMatchingPR, ref num, criteriaCtx, false);
						if (criteriaCtx.m_bfoundPRMatchingAllCrit)
						{
							break;
						}
					}
					if (bestMatchingPR != null)
					{
						if (criteriaCtx == null || criteriaCtx.CanReturnBestMatchingPR())
						{
							this.m_cpQueuePR.m_list.Remove(bestMatchingPR);
						}
						else
						{
							bestMatchingPR = default(PR);
						}
					}
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

		// Token: 0x060007E5 RID: 2021 RVA: 0x00053D70 File Offset: 0x00051F70
		public virtual PR Get(CriteriaCtx criteriaCtx)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)2304, new string[0]);
			}
			PR result;
			try
			{
				try
				{
					PR pr = default(PR);
					while (pr == null)
					{
						if (criteriaCtx != null && !criteriaCtx.m_fromMTS)
						{
							this.GetMatchingPR(ref pr, criteriaCtx);
						}
						else
						{
							pr = this.m_cpQueuePR.Dequeue();
						}
						if (pr == null)
						{
							return default(PR);
						}
						bool flag = false;
						lock (pr)
						{
							if (pr.m_deletionRequestor != DeletionRequestor.None)
							{
								flag = this.m_pm.Close(pr, null);
							}
						}
						if (flag)
						{
							pr = default(PR);
						}
					}
					if (pr != null && ProviderConfig.m_bTraceLevelPrivate)
					{
						Trace.Write(OracleTraceLevel.Private, OracleTraceTag.CP, new string[]
						{
							Trace.GetCPInfo(pr as OracleConnectionImpl, null, null, "get:reg1", false, false)
						});
					}
					result = pr;
				}
				catch (Exception)
				{
					result = default(PR);
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
			return result;
		}

		// Token: 0x060007E6 RID: 2022 RVA: 0x00053F1C File Offset: 0x0005211C
		public PR GetProxy(ConnectionString cs, CriteriaCtx criteriaCtx)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)2304, new string[0]);
			}
			uint num = 0U;
			PR pr = default(PR);
			PR pr2 = default(PR);
			if (this.m_pm.m_cs.m_drcpEnabled == DrcpType.True && !string.IsNullOrEmpty(this.m_pm.m_cs.Password))
			{
				throw new Exception("ORA-28150: proxy not authorized to connect as client");
			}
			PR result;
			try
			{
				if (criteriaCtx != null && criteriaCtx.m_fromMTS)
				{
					criteriaCtx = null;
				}
				List<PR> list = this.m_cpQueuePR.GetList();
				lock (this.m_cpQueuePR.m_sync)
				{
					foreach (PR pr3 in list)
					{
						if (criteriaCtx != null)
						{
							Pool<PM, CP, PR>.MatchCriteria(pr3, ref pr, ref num, criteriaCtx, false);
						}
						if (!pr3.m_bEndUserSessionEstablished && this.m_pm.m_cs.m_drcpEnabled == DrcpType.False)
						{
							if (this.m_cpQueuePR.m_list.Remove(pr3))
							{
								if (ProviderConfig.m_bTraceLevelPrivate)
								{
									Trace.Write(OracleTraceLevel.Private, OracleTraceTag.CP, new string[]
									{
										Trace.GetCPInfo(pr3 as OracleConnectionImpl, null, null, "cp:get:reg1", false, false)
									});
								}
								return pr3;
							}
						}
						else if (pr3.m_cs.m_userId == cs.m_userId && (pr3.m_cs == cs || pr3.m_cs.Password == cs.Password))
						{
							if (pr2 == null)
							{
								pr2 = pr3;
							}
							if (criteriaCtx == null || pr == pr3)
							{
								pr = pr3;
								if ((criteriaCtx == null || criteriaCtx.m_bfoundPRMatchingAllCrit) && this.m_cpQueuePR.m_list.Remove(pr))
								{
									if (ProviderConfig.m_bTraceLevelPrivate)
									{
										Trace.Write(OracleTraceLevel.Private, OracleTraceTag.CP, new string[]
										{
											Trace.GetCPInfo(pr as OracleConnectionImpl, null, null, "cp:get:reg1", false, false)
										});
									}
									return pr;
								}
							}
						}
					}
					if (criteriaCtx != null)
					{
						PR pr4 = default(PR);
						if (pr != null && criteriaCtx.CanReturnBestMatchingPR())
						{
							pr4 = pr;
						}
						else if (pr2 != null)
						{
							pr4 = pr2;
						}
						if (this.m_cpQueuePR.m_list.Remove(pr4))
						{
							if (ProviderConfig.m_bTraceLevelPrivate)
							{
								Trace.Write(OracleTraceLevel.Private, OracleTraceTag.CP, new string[]
								{
									Trace.GetCPInfo(pr4 as OracleConnectionImpl, null, null, "cp:get:reg2", false, false)
								});
							}
							return pr4;
						}
					}
					list = this.m_cpQueuePR.GetList();
					foreach (PR pr5 in list)
					{
						if (!pr5.m_bEndUserSessionEstablished && this.m_pm.m_cs.m_drcpEnabled == DrcpType.False)
						{
							if (this.m_cpQueuePR.Remove(pr5))
							{
								if (ProviderConfig.m_bTraceLevelPrivate)
								{
									Trace.Write(OracleTraceLevel.Private, OracleTraceTag.CP, new string[]
									{
										Trace.GetCPInfo(pr5 as OracleConnectionImpl, null, null, "cp:get:reg3", false, false)
									});
								}
								return pr5;
							}
						}
						else if (this.m_cpQueuePR.Remove(pr5))
						{
							if (ProviderConfig.m_bTraceLevelPrivate)
							{
								Trace.Write(OracleTraceLevel.Private, OracleTraceTag.CP, new string[]
								{
									Trace.GetCPInfo(pr5 as OracleConnectionImpl, null, null, "cp:get:reg4", false, false)
								});
							}
							return pr5;
						}
					}
				}
				result = default(PR);
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

		// Token: 0x060007E7 RID: 2023 RVA: 0x000543A0 File Offset: 0x000525A0
		public void MarkAllPRsForDeletion(DateTime haEventUtcDataTime, bool isHAEvnt = false)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)2304, new string[0]);
			}
			try
			{
				this.m_lastHADownEventUtcDateTime = haEventUtcDataTime;
				if (!this.m_bInstanceDown)
				{
					bool flag = true;
					this.m_bInstanceDown = true;
					if (ProviderConfig.m_bTraceLevelPrivate)
					{
						string text = string.Format("[pm_id={0}][instance={1}][instance_down={2}][state_changed={3}][down_event_utc={4}][last_down_event_utc={5}]", new object[]
						{
							this.m_pm.m_id,
							this.m_instanceName,
							this.m_bInstanceDown,
							flag,
							haEventUtcDataTime.ToString(),
							this.m_lastHADownEventUtcDateTime.ToString()
						});
						Trace.Write(OracleTraceLevel.Private, OracleTraceTag.CP, new string[]
						{
							text
						});
					}
				}
				List<PR> list = this.m_cpListPR.GetList();
				for (int i = 0; i < list.Count; i++)
				{
					try
					{
						PR pr = list[i];
						if (pr != null)
						{
							lock (pr)
							{
								if (!isHAEvnt)
								{
									if (pr.m_deletionRequestor != DeletionRequestor.HA)
									{
										pr.m_deletionRequestor = DeletionRequestor.ClearPool;
									}
								}
								else
								{
									pr.m_deletionRequestor = DeletionRequestor.HA;
								}
							}
						}
					}
					catch
					{
					}
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

		// Token: 0x060007E8 RID: 2024 RVA: 0x00054594 File Offset: 0x00052794
		public void ClearPool(PR prToRetain, bool isHAEvnt = false)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)2304, new string[0]);
			}
			try
			{
				List<PR> list = this.m_cpListPR.GetList();
				int i = 0;
				while (i < list.Count)
				{
					if (prToRetain == null)
					{
						goto IL_47;
					}
					if (prToRetain != list[i])
					{
						goto Block_5;
					}
					IL_BF:
					i++;
					continue;
					Block_5:
					try
					{
						IL_47:
						PR pr = list[i];
						if (pr != null)
						{
							lock (pr)
							{
								if (!isHAEvnt)
								{
									if (pr.m_deletionRequestor != DeletionRequestor.HA)
									{
										pr.m_deletionRequestor = DeletionRequestor.ClearPool;
									}
								}
								else
								{
									pr.m_deletionRequestor = DeletionRequestor.HA;
								}
								pr.m_pm.Close(pr, null);
							}
						}
					}
					catch
					{
					}
					goto IL_BF;
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

		// Token: 0x060007E9 RID: 2025 RVA: 0x000546D4 File Offset: 0x000528D4
		protected override void Finalize()
		{
			try
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)2304, new string[0]);
				}
				try
				{
					this.ClearPool(default(PR), false);
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
			finally
			{
				base.Finalize();
			}
		}

		// Token: 0x04000A96 RID: 2710
		public PM m_pm;

		// Token: 0x04000A97 RID: 2711
		public SyncQueueList<PR> m_cpListPR;

		// Token: 0x04000A98 RID: 2712
		public SyncQueueList<PR> m_cpQueuePR;

		// Token: 0x04000A99 RID: 2713
		public string m_instanceName;

		// Token: 0x04000A9A RID: 2714
		public string m_serviceName;

		// Token: 0x04000A9B RID: 2715
		public DateTime m_lastHADownEventUtcDateTime;

		// Token: 0x04000A9C RID: 2716
		public bool m_bInstanceDown;
	}
}
