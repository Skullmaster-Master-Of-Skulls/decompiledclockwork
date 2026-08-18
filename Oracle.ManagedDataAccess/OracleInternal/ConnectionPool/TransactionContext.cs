using System;
using System.Collections.Generic;
using System.Text;
using System.Transactions;
using Oracle.ManagedDataAccess.Client;
using OracleInternal.Common;
using OracleInternal.MTS;
using OracleInternal.ServiceObjects;

namespace OracleInternal.ConnectionPool
{
	// Token: 0x020000D9 RID: 217
	internal class TransactionContext<PM, CP, PR> where PM : PoolManager<PM, CP, PR>, new() where CP : Pool<PM, CP, PR>, new() where PR : PoolResource<PM, CP, PR>, new()
	{
		// Token: 0x060008A7 RID: 2215 RVA: 0x0005C7D0 File Offset: 0x0005A9D0
		public TransactionContext(PM pm, string localTxnId)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)2304, new string[]
				{
					Trace.GetCPInfo(null, localTxnId, null, null, false, false)
				});
			}
			try
			{
				this.m_localTxnId = localTxnId;
				this.m_enlistedPRList = new PR[33];
				this.m_syncMTSTxnRM = new object();
				this.m_id = this.GetHashCode().ToString();
				this.m_pm = (pm as OraclePoolManager);
				this.m_syncStats = new object();
				this.m_instances = new List<string>();
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
						Trace.GetCPInfo(null, localTxnId, null, null, false, false)
					});
				}
			}
		}

		// Token: 0x060008A8 RID: 2216 RVA: 0x0005C8C0 File Offset: 0x0005AAC0
		public void ReInitialize(PM pm, Transaction txn)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)2304, new string[]
				{
					Trace.GetCPInfo(null, txn, null, null, false, false)
				});
			}
			try
			{
				this.m_pm = (pm as OraclePoolManager);
				this.m_localTxnId = txn.TransactionInformation.LocalIdentifier;
				lock (this)
				{
					for (int i = 0; i < this.m_enlistedPRList.Length; i++)
					{
						this.m_enlistedPRList[i] = default(PR);
					}
				}
				this.m_affinityInstanceName = null;
				this.m_maxBranchIndex = 0;
				this.m_instances.Clear();
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
						Trace.GetCPInfo(null, txn, null, null, false, false)
					});
				}
			}
		}

		// Token: 0x060008A9 RID: 2217 RVA: 0x0005C9DC File Offset: 0x0005ABDC
		internal MTSTxnRM GetRM(ConnectionString cs, string serviceName, string pdbName, Transaction txn)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)2304, new string[]
				{
					Trace.GetCPInfo(null, txn, null, null, false, false)
				});
			}
			MTSTxnRM mtsTxnRM;
			try
			{
				if (this.m_mtsTxnRM == null)
				{
					lock (this.m_syncMTSTxnRM)
					{
						if (this.m_mtsTxnRM == null)
						{
							this.m_mtsTxnRM = MTSProxyPool.GetRM(cs.m_connectionPoolType == ConnectionPoolType.CCP, cs.ServerID, serviceName, pdbName, txn);
						}
					}
				}
				mtsTxnRM = this.m_mtsTxnRM;
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
						Trace.GetCPInfo(null, txn, null, null, false, false)
					});
				}
			}
			return mtsTxnRM;
		}

		// Token: 0x060008AA RID: 2218 RVA: 0x0005CAD0 File Offset: 0x0005ACD0
		internal void RemoveRM()
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)2304, new string[]
				{
					Trace.GetCPInfo(null, this.m_localTxnId, null, null, false, false) + "(txnid:" + this.m_localTxnId + ")"
				});
			}
			try
			{
				PR pr = default(PR);
				for (int i = 0; i <= this.m_maxBranchIndex; i++)
				{
					pr = this.m_enlistedPRList[i];
					try
					{
						if (pr != null)
						{
							pr.m_bTxnCtxPrimaryCon = false;
							lock (this)
							{
								this.m_enlistedPRList[i] = default(PR);
							}
							if (pr.m_mtsTxnCtx != null && pr.m_mtsTxnCtx.m_txnType == MTSTxnType.Distributed)
							{
								pr.m_deletionRequestor = DeletionRequestor.HA;
							}
							if (!pr.m_bCheckedOutByApp && !pr.m_bPutCompleted && !pr.m_bCheckedOutByDTC)
							{
								lock (pr)
								{
									if (!pr.m_bCheckedOutByApp && !pr.m_bPutCompleted && !pr.m_bCheckedOutByDTC)
									{
										if (pr.m_mtsTxnCtx != null)
										{
											pr.m_mtsTxnCtx.m_mtsTxnBranch = null;
											(pr as OracleConnectionImpl).ResetMTSTxnCtx();
										}
										pr.m_txnCtx = null;
										try
										{
											pr.m_pm.Put(pr, null);
										}
										catch
										{
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
						Trace.GetCPInfo(null, this.m_localTxnId, null, null, false, false)
					});
				}
			}
		}

		// Token: 0x060008AB RID: 2219 RVA: 0x0005CD70 File Offset: 0x0005AF70
		internal PR GetEnlisted(ConnectionString csWithDiffPassword, bool bGetForApp, CriteriaCtx criteriaCtx, string instanceName = null, bool bEnforceAffinity = true)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)2304, new string[]
				{
					Trace.GetCPInfo(null, this.m_localTxnId, instanceName, null, false, false) + string.Format("TxnCtx.GetEnlisted(inst={0};enforce={1})", instanceName, bEnforceAffinity)
				});
			}
			PR pr = default(PR);
			string empty = string.Empty;
			PR pr2 = default(PR);
			uint num = 0U;
			if (instanceName == null)
			{
				instanceName = this.m_affinityInstanceName;
			}
			PR result;
			try
			{
				bool flag = false;
				if (criteriaCtx != null)
				{
					this.m_pm.m_criteriaMapper.AssignId(criteriaCtx);
				}
				pr = this.m_enlistedPRList[0];
				if (!this.CanReturnPR(pr, criteriaCtx))
				{
					pr = default(PR);
				}
				if (pr != null && (!bEnforceAffinity || (bEnforceAffinity && pr.m_instanceName == instanceName)) && !pr.m_bCheckedOutByApp && !pr.m_bPutCompleted && (csWithDiffPassword == null || (csWithDiffPassword.m_userId == pr.m_cs.m_userId && csWithDiffPassword.Password == pr.m_cs.Password)))
				{
					lock (pr)
					{
						if (!pr.m_bCheckedOutByApp && !pr.m_bPutCompleted && pr.m_mtsTxnCtx != null)
						{
							lock (this)
							{
								if (pr.m_mtsTxnCtx != null)
								{
									if (bGetForApp)
									{
										pr.m_bCheckedOutByApp = true;
									}
									else
									{
										pr.m_bCheckedOutByDTC = true;
										pr.m_resPoolRefCount++;
									}
									flag = true;
								}
							}
						}
					}
				}
				if (flag)
				{
					if (ProviderConfig.m_bTraceLevelPrivate)
					{
						Trace.Write(OracleTraceLevel.Private, OracleTraceTag.CP, new string[]
						{
							Trace.GetCPInfo(pr as OracleConnectionImpl, this.m_localTxnId, instanceName, "txnctx:get:res1", false, false)
						});
					}
					result = pr;
				}
				else
				{
					if (bEnforceAffinity)
					{
						pr2 = default(PR);
						num = 0U;
						for (int i = 1; i <= this.m_maxBranchIndex; i++)
						{
							pr = this.m_enlistedPRList[i];
							if (pr != null && !pr.m_bCheckedOutByApp && !pr.m_bPutCompleted && pr.m_instanceName == instanceName && (csWithDiffPassword == null || (csWithDiffPassword.m_userId == pr.m_cs.m_userId && csWithDiffPassword.Password == pr.m_cs.Password)))
							{
								lock (pr)
								{
									if (!pr.m_bCheckedOutByApp && !pr.m_bPutCompleted && pr.m_mtsTxnCtx != null && pr.m_txnCtx != null)
									{
										lock (pr.m_txnCtx)
										{
											if (pr.m_mtsTxnCtx != null)
											{
												if (criteriaCtx == null)
												{
													pr2 = pr;
													break;
												}
												Pool<PM, CP, PR>.MatchCriteria(pr, ref pr2, ref num, criteriaCtx, true);
												if (criteriaCtx.m_bfoundPRMatchingAllCrit)
												{
													break;
												}
											}
										}
									}
								}
							}
						}
						if (pr2 != null)
						{
							if (criteriaCtx == null || criteriaCtx.CanReturnBestMatchingPR())
							{
								pr = pr2;
								if (bGetForApp)
								{
									pr.m_bCheckedOutByApp = true;
								}
								else
								{
									pr.m_bCheckedOutByDTC = true;
									pr.m_resPoolRefCount++;
								}
								flag = true;
							}
							else
							{
								pr2 = default(PR);
								pr = default(PR);
							}
						}
						else
						{
							pr = default(PR);
						}
						if (flag)
						{
							if (ProviderConfig.m_bTraceLevelPrivate)
							{
								Trace.Write(OracleTraceLevel.Private, OracleTraceTag.CP, new string[]
								{
									Trace.GetCPInfo(pr as OracleConnectionImpl, this.m_localTxnId, instanceName, "txnctx:get:res2", false, false)
								});
							}
							return pr;
						}
						if (bEnforceAffinity)
						{
							return default(PR);
						}
					}
					flag = false;
					pr2 = default(PR);
					num = 0U;
					for (int j = 1; j <= this.m_maxBranchIndex; j++)
					{
						pr = this.m_enlistedPRList[j];
						if (pr != null && !pr.m_bCheckedOutByApp && !pr.m_bPutCompleted && (csWithDiffPassword == null || (csWithDiffPassword.m_userId == pr.m_cs.m_userId && csWithDiffPassword.Password == pr.m_cs.Password)))
						{
							lock (pr)
							{
								if (!pr.m_bCheckedOutByApp && !pr.m_bPutCompleted && pr.m_mtsTxnCtx != null && pr.m_mtsTxnCtx.m_txnType == MTSTxnType.Distributed && pr.m_txnCtx != null)
								{
									lock (pr.m_txnCtx)
									{
										if (pr.m_mtsTxnCtx != null && pr.m_mtsTxnCtx.m_txnType == MTSTxnType.Distributed)
										{
											if (criteriaCtx == null)
											{
												pr2 = pr;
												break;
											}
											Pool<PM, CP, PR>.MatchCriteria(pr, ref pr2, ref num, criteriaCtx, true);
											if (criteriaCtx.m_bfoundPRMatchingAllCrit)
											{
												break;
											}
										}
									}
								}
							}
						}
					}
					if (pr2 != null)
					{
						if (criteriaCtx == null || criteriaCtx.CanReturnBestMatchingPR())
						{
							pr = pr2;
							if (bGetForApp)
							{
								pr.m_bCheckedOutByApp = true;
							}
							else
							{
								pr.m_bCheckedOutByDTC = true;
								pr.m_resPoolRefCount++;
							}
							flag = true;
						}
						else
						{
							pr2 = default(PR);
							pr = default(PR);
						}
					}
					else
					{
						pr = default(PR);
					}
					if (flag)
					{
						if (ProviderConfig.m_bTraceLevelPrivate)
						{
							Trace.Write(OracleTraceLevel.Private, OracleTraceTag.CP, new string[]
							{
								Trace.GetCPInfo(pr as OracleConnectionImpl, this.m_localTxnId, instanceName, "txnctx:get:res3", false, false)
							});
						}
						result = pr;
					}
					else
					{
						result = default(PR);
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
					string arg = null;
					if (pr == null)
					{
						arg = "null";
					}
					else if (pr != null)
					{
						if (pr.m_instanceName == instanceName)
						{
							arg = "matchfound=T";
						}
						else
						{
							arg = "matchfound=F";
						}
					}
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)2560, new string[]
					{
						Trace.GetCPInfo(pr as OracleConnectionImpl, this.m_localTxnId, instanceName, null, true, false) + string.Format("TxnCtx.GetEnlisted(inst={0};enforce={1}) returning ({2})", instanceName, bEnforceAffinity, arg)
					});
				}
			}
			return result;
		}

		// Token: 0x060008AC RID: 2220 RVA: 0x0005D568 File Offset: 0x0005B768
		internal PR GetEnlisted(string affinityInstance, int branchNum, bool bMustMatch, out bool bMatchFound)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)2304, new string[]
				{
					Trace.GetCPInfo(null, this.m_localTxnId, null, null, false, false) + string.Format("TxnCtx.GetEnlisted(aff={0};br={1};bMustMatch={2})", affinityInstance, branchNum, bMustMatch)
				});
			}
			PR pr = default(PR);
			bMatchFound = false;
			string empty = string.Empty;
			PR pr2 = default(PR);
			uint num = 0U;
			CriteriaCtx criteriaCtx = null;
			PR result;
			try
			{
				if (criteriaCtx != null)
				{
					this.m_pm.m_criteriaMapper.AssignId(criteriaCtx);
				}
				pr2 = default(PR);
				num = 0U;
				if (branchNum >= 1 && branchNum <= 32)
				{
					pr = this.m_enlistedPRList[branchNum];
					if (!this.CanReturnPR(pr, criteriaCtx))
					{
						pr = default(PR);
					}
					if (pr != null && affinityInstance != null && pr.m_instanceName == affinityInstance && !pr.m_bPutCompleted)
					{
						lock (pr)
						{
							if (!pr.m_bPutCompleted && pr.m_mtsTxnCtx != null && pr.m_txnCtx != null)
							{
								lock (pr.m_txnCtx)
								{
									if (pr.m_mtsTxnCtx != null)
									{
										pr.m_bCheckedOutByDTC = true;
										pr.m_resPoolRefCount++;
										bMatchFound = true;
									}
								}
							}
						}
					}
				}
				if (!bMatchFound)
				{
					pr = default(PR);
				}
				if (bMustMatch || bMatchFound)
				{
					if (pr != null && ProviderConfig.m_bTraceLevelPrivate)
					{
						Trace.Write(OracleTraceLevel.Private, OracleTraceTag.CP, new string[]
						{
							Trace.GetCPInfo(pr as OracleConnectionImpl, this.m_localTxnId, null, "txnctx:get:res1", false, false)
						});
					}
					result = pr;
				}
				else
				{
					pr2 = default(PR);
					num = 0U;
					int num2 = -1;
					for (int i = 1; i <= this.m_maxBranchIndex; i++)
					{
						pr = this.m_enlistedPRList[i];
						if (pr != null && affinityInstance != null && pr.m_instanceName == affinityInstance && !pr.m_bPutCompleted)
						{
							lock (pr)
							{
								if (!pr.m_bPutCompleted && pr.m_mtsTxnCtx != null && pr.m_txnCtx != null)
								{
									lock (pr.m_txnCtx)
									{
										if (pr.m_mtsTxnCtx != null)
										{
											if (criteriaCtx == null)
											{
												pr2 = pr;
												break;
											}
											Pool<PM, CP, PR>.MatchCriteria(pr, ref pr2, ref num, criteriaCtx, true);
											if (pr == pr2)
											{
												num2 = i;
											}
											if (criteriaCtx.m_bfoundPRMatchingAllCrit)
											{
												break;
											}
										}
									}
								}
							}
						}
					}
					if (pr2 != null)
					{
						if (criteriaCtx == null || criteriaCtx.CanReturnBestMatchingPR())
						{
							pr = pr2;
							pr.m_bCheckedOutByDTC = true;
							pr.m_resPoolRefCount++;
							if (ProviderConfig.m_bTraceLevelPrivate)
							{
								Trace.Write(OracleTraceLevel.Private, OracleTraceTag.CP, new string[]
								{
									Trace.GetCPInfo(pr as OracleConnectionImpl, this.m_localTxnId, null, "txnctx:get:res2", false, false)
								});
							}
							if (num2 == branchNum)
							{
								bMatchFound = true;
							}
							return pr;
						}
						pr2 = default(PR);
						pr = default(PR);
					}
					else
					{
						pr = default(PR);
					}
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
				string text;
				if (pr == null)
				{
					text = "null";
				}
				else
				{
					text = "bMatchFound=F";
					if (pr.m_mtsTxnCtx != null && pr.m_mtsTxnCtx.m_mtsTxnBranch != null && pr.m_mtsTxnCtx.m_mtsTxnBranch.BranchNumber == branchNum)
					{
						text = "bMatchFound=T";
					}
				}
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)2560, new string[]
					{
						Trace.GetCPInfo(pr as OracleConnectionImpl, this.m_localTxnId, null, null, true, false) + string.Format("TxnCtx.GetEnlisted(aff={0};br={1};bMustMatch={2}) returning ({3})", new object[]
						{
							affinityInstance,
							branchNum,
							bMustMatch,
							text
						})
					});
				}
			}
			return result;
		}

		// Token: 0x060008AD RID: 2221 RVA: 0x0005DAA0 File Offset: 0x0005BCA0
		internal string GetListForTrace()
		{
			StringBuilder stringBuilder = new StringBuilder();
			if (this.m_enlistedPRList != null)
			{
				for (int i = 0; i < 33; i++)
				{
					PR pr = default(PR);
					lock (this)
					{
						pr = this.m_enlistedPRList[i];
					}
					if (pr != null)
					{
						if (pr.Dump())
						{
							if (pr.m_sessionType != SessionType.Two_Session_Proxy)
							{
								stringBuilder.Append(string.Concat(new object[]
								{
									"(",
									pr.m_endUserSessionId,
									":",
									pr.m_endUserSerialNum,
									":",
									pr.m_instanceName,
									")"
								}));
							}
							else
							{
								stringBuilder.Append(string.Concat(new object[]
								{
									i,
									":(sessid=",
									pr.m_endUserSessionId,
									",",
									pr.m_pxyUserSessionId,
									");"
								}));
							}
						}
						else
						{
							stringBuilder.Append(string.Concat(new object[]
							{
								i,
								":(sessid=",
								pr.m_endUserSessionId,
								":CLOSED);"
							}));
						}
					}
				}
			}
			else
			{
				stringBuilder.Append("(null list!!!)");
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060008AE RID: 2222 RVA: 0x0005DC70 File Offset: 0x0005BE70
		private bool CanReturnPR(PR pr, CriteriaCtx criteriaCtx)
		{
			PR pr2 = default(PR);
			uint num = 0U;
			bool result = false;
			if (criteriaCtx != null)
			{
				Pool<PM, CP, PR>.MatchCriteria(pr, ref pr2, ref num, criteriaCtx, true);
				if (pr2 != null && criteriaCtx.CanReturnBestMatchingPR())
				{
					result = true;
				}
			}
			else
			{
				result = true;
			}
			return result;
		}

		// Token: 0x04000B92 RID: 2962
		private const int s_maxBranchCount = 32;

		// Token: 0x04000B93 RID: 2963
		internal int m_maxBranchIndex;

		// Token: 0x04000B94 RID: 2964
		internal PR[] m_enlistedPRList;

		// Token: 0x04000B95 RID: 2965
		internal MTSTxnRM m_mtsTxnRM;

		// Token: 0x04000B96 RID: 2966
		internal object m_syncMTSTxnRM;

		// Token: 0x04000B97 RID: 2967
		internal string m_affinityInstanceName;

		// Token: 0x04000B98 RID: 2968
		internal string m_localTxnId;

		// Token: 0x04000B99 RID: 2969
		internal OraclePoolManager m_pm;

		// Token: 0x04000B9A RID: 2970
		internal object m_syncStats;

		// Token: 0x04000B9B RID: 2971
		internal List<string> m_instances;

		// Token: 0x04000B9C RID: 2972
		internal string m_id;
	}
}
