using System;
using System.Collections.Generic;
using System.IO;
using System.Security;
using System.Text;
using System.Threading;
using System.Transactions;
using Oracle.ManagedDataAccess.Client;
using OracleInternal.Common;
using OracleInternal.MTS;
using OracleInternal.Network;
using OracleInternal.ServiceObjects;

namespace OracleInternal.ConnectionPool
{
	// Token: 0x020000C6 RID: 198
	internal static class OracleConnectionDispenser<PM, CP, PR> where PM : PoolManager<PM, CP, PR>, new() where CP : Pool<PM, CP, PR>, new() where PR : PoolResource<PM, CP, PR>, new()
	{
		// Token: 0x0600079B RID: 1947 RVA: 0x0004779C File Offset: 0x0004599C
		static OracleConnectionDispenser()
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)2304, new string[0]);
			}
			try
			{
				OracleConnectionDispenser<PM, CP, PR>.m_htPM = new SyncDictionary<string, PM>();
				OracleConnectionDispenser<PM, CP, PR>.m_listPM = new SyncQueueList<PM>(int.MaxValue);
				OracleConnectionDispenser<PM, CP, PR>.m_listDataSources = new SyncQueueList<string>(int.MaxValue);
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

		// Token: 0x0600079C RID: 1948 RVA: 0x0004784C File Offset: 0x00045A4C
		internal static void CreateSEPSFileWatcher(string walletPath, string walletFile)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)2304, new string[0]);
			}
			try
			{
				if (!OracleConnectionDispenser<PM, CP, PR>.s_bSEPSFileWatcherCreated)
				{
					lock (OracleConnectionDispenser<PM, CP, PR>.s_sepsFileWatcherCreationLock)
					{
						if (!OracleConnectionDispenser<PM, CP, PR>.s_bSEPSFileWatcherCreated)
						{
							Trace.Write(OracleTraceLevel.Private, OracleTraceTag.CP, new string[]
							{
								"Creating FileWatcher for WalletPath:<{0}>, WalletFile:<{1}>",
								walletPath,
								walletFile
							});
							OracleConnectionDispenser<PM, CP, PR>.s_sepsFileWatcher = new FileSystemWatcher();
							OracleConnectionDispenser<PM, CP, PR>.s_sepsFileWatcher.Filter = walletFile;
							FileSystemEventHandler value = new FileSystemEventHandler(OracleConnectionDispenser<PM, CP, PR>.SEPSFileChangedEvent);
							OracleConnectionDispenser<PM, CP, PR>.s_sepsFileWatcher.Changed += value;
							OracleConnectionDispenser<PM, CP, PR>.s_sepsFileWatcher.Created += value;
							OracleConnectionDispenser<PM, CP, PR>.s_sepsFileWatcher.Deleted += value;
							OracleConnectionDispenser<PM, CP, PR>.s_sepsFileWatcher.Renamed += OracleConnectionDispenser<PM, CP, PR>.SEPSFileRenamedEvent;
							OracleConnectionDispenser<PM, CP, PR>.s_sepsFileWatcher.Path = walletPath;
							OracleConnectionDispenser<PM, CP, PR>.s_sepsFileWatcher.EnableRaisingEvents = true;
							OracleConnectionDispenser<PM, CP, PR>.s_bSEPSFileWatcherCreated = true;
						}
					}
				}
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)2560, new string[0]);
				}
			}
		}

		// Token: 0x0600079D RID: 1949 RVA: 0x00047970 File Offset: 0x00045B70
		internal static void SEPSFileRenamedEvent(object source, RenamedEventArgs e)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)2304, new string[0]);
			}
			try
			{
				Trace.Write(OracleTraceLevel.Private, OracleTraceTag.CP, new string[]
				{
					"TID:{0} => File <{1}> has been {2} at Time:{3}!",
					Thread.CurrentThread.ManagedThreadId.ToString(),
					e.FullPath,
					e.ChangeType.ToString(),
					DateTime.Now.ToString()
				});
				OracleConnectionDispenser<PM, CP, PR>.ProcessSEPSFileChangeEvent();
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)2560, new string[0]);
				}
			}
		}

		// Token: 0x0600079E RID: 1950 RVA: 0x00047A28 File Offset: 0x00045C28
		internal static void SEPSFileChangedEvent(object sender, FileSystemEventArgs e)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)2304, new string[0]);
			}
			Trace.Write(OracleTraceLevel.Private, OracleTraceTag.CP, new string[]
			{
				"TID:{0} => File <{1}> has been {2} at Time:{3}!",
				Thread.CurrentThread.ManagedThreadId.ToString(),
				e.FullPath,
				e.ChangeType.ToString(),
				DateTime.Now.ToString()
			});
			OracleConnectionDispenser<PM, CP, PR>.ProcessSEPSFileChangeEvent();
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)2560, new string[0]);
			}
		}

		// Token: 0x0600079F RID: 1951 RVA: 0x00047ACC File Offset: 0x00045CCC
		internal static void ProcessSEPSFileChangeEvent()
		{
			List<PM> list = OracleConnectionDispenser<PM, CP, PR>.m_listPM.GetList();
			for (int i = 0; i < list.Count; i++)
			{
				PM pm = list[i];
				if (pm.m_bUsingSEPSCredentials)
				{
					try
					{
						Trace.Write(OracleTraceLevel.Private, OracleTraceTag.CP, new string[]
						{
							"ProcessSEPSFileChangeEvent => Querying Data Source:" + pm.m_cs.m_dataSource
						});
						string text;
						string text2;
						try
						{
							string text3;
							string text4;
							OracleCommunication.GetSEPSUserIDandPW(pm.m_cs.m_dataSource, out text, out text2, out text3, out text4);
						}
						catch
						{
							Trace.Write(OracleTraceLevel.Private, OracleTraceTag.CP, new string[]
							{
								"ProcessSEPSFileChangeEvent => Exception raised from OracleCommunication.GetSEPSUserIDandPW for DataSource: " + pm.m_cs.m_dataSource
							});
							OracleConnectionDispenser<PM, CP, PR>.m_listPM.GetList();
							for (int j = 0; j < list.Count; j++)
							{
								PM pm2 = list[i];
								if (pm2.m_bUsingSEPSCredentials)
								{
									pm2.m_bSEPSCredentialsFetched = false;
									pm2.ClearAllPools(default(PR), false);
								}
							}
							break;
						}
						Trace.Write(OracleTraceLevel.Private, OracleTraceTag.CP, new string[]
						{
							"ProcessSEPSFileChangeEvent => Got Credentials from Client Wallet"
						});
						bool flag = false;
						if (pm.m_bSEPSForProxyCredentials)
						{
							if (text != pm.m_cs.m_sepsProxyUserId || text2 != pm.m_cs.SEPSProxyPassword)
							{
								flag = true;
							}
						}
						else if (text != pm.m_cs.m_sepsUserId || text2 != pm.m_cs.SEPSPassword)
						{
							flag = true;
						}
						if (flag)
						{
							Trace.Write(OracleTraceLevel.Private, OracleTraceTag.CP, new string[]
							{
								"ProcessSEPSFileChangeEvent => Credentials DOES NOT Match: Calling ClearPool"
							});
							pm.ClearAllPools(default(PR), false);
							if (pm.m_bSEPSForProxyCredentials)
							{
								pm.m_cs.m_sepsProxyUserId = text;
								pm.m_cs.m_sepsProxyPassword = text2;
								pm.m_cs.m_sepsSecuredProxyPassword = null;
								pm.m_cs.m_sepsSecPxyPwdList.Clear();
							}
							else
							{
								pm.m_cs.m_sepsUserId = text;
								pm.m_cs.m_sepsPassword = text2;
								pm.m_cs.m_sepsSecuredPassword = null;
								pm.m_cs.m_sepsSecPwdList.Clear();
							}
							pm.m_cs.SecureSEPSPassword();
							pm.m_bSEPSCredentialsFetched = true;
						}
					}
					catch (Exception ex)
					{
						Trace.Write(OracleTraceLevel.Private, OracleTraceTag.CP, new string[]
						{
							"ProcessSEPSFileChangeEvent => Exception while processing: {0}",
							ex.ToString()
						});
					}
				}
			}
		}

		// Token: 0x060007A0 RID: 1952 RVA: 0x00047E10 File Offset: 0x00046010
		internal static string Dump()
		{
			StringBuilder stringBuilder = new StringBuilder();
			try
			{
				stringBuilder.AppendLine("(DUMP)");
				List<string> keys = OracleConnectionDispenser<PM, CP, PR>.m_htPM.GetKeys();
				foreach (string text in keys)
				{
					PM pm = OracleConnectionDispenser<PM, CP, PR>.m_htPM[text];
					if (pm != null)
					{
						stringBuilder.AppendLine("(CP) =================================");
						stringBuilder.AppendLine("(CP) " + text);
						stringBuilder.AppendLine("(CP) pmid=");
						stringBuilder.Append(pm.m_id);
						stringBuilder.AppendLine("(CP) =================================");
						List<CP> list = pm.m_pmListCP.GetList();
						foreach (CP cp in list)
						{
							stringBuilder.AppendLine(string.Concat(new string[]
							{
								"(CP) ",
								cp.m_serviceName,
								" , ",
								cp.m_instanceName,
								":"
							}));
							List<PR> list2 = cp.m_cpListPR.GetList();
							stringBuilder.Append("(CP)   list  (count:" + list2.Count + ") : ");
							foreach (PR pr in list2)
							{
								if (pr.Dump())
								{
									if (pr.m_sessionType != SessionType.Two_Session_Proxy)
									{
										stringBuilder.AppendLine(string.Concat(new object[]
										{
											"(",
											pr.m_endUserSessionId,
											":",
											pr.m_endUserSerialNum,
											":",
											pr.m_bCheckedOutByApp.ToString().Substring(0, 1),
											":",
											pr.m_bCheckedOutByDTC.ToString().Substring(0, 1),
											":",
											pr.m_bPutCompleted.ToString().Substring(0, 1),
											":",
											pr.m_bTxnCtxPrimaryCon.ToString().Substring(0, 1),
											":",
											(pr.m_mtsTxnCtx != null) ? pr.m_mtsTxnCtx.m_txnType.ToString().Substring(0, 1) : "N",
											":",
											pr.ServiceName,
											":",
											pr.EditionName,
											":",
											pr.m_instanceName,
											")"
										}));
									}
									else
									{
										stringBuilder.AppendLine(string.Concat(new object[]
										{
											"(",
											pr.m_endUserSessionId,
											",",
											pr.m_pxyUserSessionId,
											pr.m_bCheckedOutByApp.ToString().Substring(0, 1),
											":",
											pr.m_bCheckedOutByDTC.ToString().Substring(0, 1),
											":",
											pr.m_bPutCompleted.ToString().Substring(0, 1),
											":",
											pr.m_bTxnCtxPrimaryCon.ToString().Substring(0, 1),
											":",
											(pr.m_mtsTxnCtx != null) ? pr.m_mtsTxnCtx.m_txnType.ToString().Substring(0, 1) : "N",
											":",
											pr.ServiceName,
											":",
											pr.EditionName,
											":",
											pr.m_instanceName,
											")"
										}));
									}
								}
								else
								{
									stringBuilder.Append("(" + pr.m_endUserSessionId + ":CLOSED)");
								}
							}
							stringBuilder.AppendLine();
							list2 = cp.m_cpQueuePR.GetList();
							stringBuilder.Append("(CP)   queue (count:" + list2.Count + ") : ");
							foreach (PR pr2 in list2)
							{
								if (pr2.Dump())
								{
									if (pr2.m_sessionType != SessionType.Two_Session_Proxy)
									{
										stringBuilder.Append(string.Concat(new object[]
										{
											"(",
											pr2.m_endUserSessionId,
											":",
											pr2.m_endUserSerialNum,
											":",
											pr2.m_bCheckedOutByApp.ToString().Substring(0, 1),
											":",
											pr2.m_bCheckedOutByDTC.ToString().Substring(0, 1),
											":",
											pr2.m_bPutCompleted.ToString().Substring(0, 1),
											":",
											pr2.m_bTxnCtxPrimaryCon.ToString().Substring(0, 1),
											":",
											(pr2.m_mtsTxnCtx != null) ? pr2.m_mtsTxnCtx.m_txnType.ToString().Substring(0, 1) : "N",
											":",
											pr2.ServiceName,
											":",
											pr2.EditionName,
											":",
											pr2.m_instanceName,
											")"
										}));
									}
									else
									{
										stringBuilder.Append(string.Concat(new object[]
										{
											"(",
											pr2.m_endUserSessionId,
											",",
											pr2.m_pxyUserSessionId,
											pr2.m_bCheckedOutByApp.ToString().Substring(0, 1),
											":",
											pr2.m_bCheckedOutByDTC.ToString().Substring(0, 1),
											":",
											pr2.m_bPutCompleted.ToString().Substring(0, 1),
											":",
											pr2.m_bTxnCtxPrimaryCon.ToString().Substring(0, 1),
											":",
											(pr2.m_mtsTxnCtx != null) ? pr2.m_mtsTxnCtx.m_txnType.ToString().Substring(0, 1) : "N",
											":",
											pr2.ServiceName,
											":",
											pr2.EditionName,
											":",
											pr2.m_instanceName,
											")"
										}));
									}
								}
								else
								{
									stringBuilder.Append("(" + pr2.m_endUserSessionId + ":CLOSED)");
								}
							}
							stringBuilder.AppendLine();
						}
						if (pm.m_dictDictTxnCtx != null)
						{
							List<string> keys2 = pm.m_dictDictTxnCtx.GetKeys();
							foreach (string text2 in keys2)
							{
								List<string> keys3 = pm.m_dictDictTxnCtx[text2].GetKeys();
								for (int i = 0; i < keys3.Count; i++)
								{
									int num = 0;
									TransactionContext<PM, CP, PR> transactionContext = pm.m_dictDictTxnCtx[text2][keys3[i]];
									if (transactionContext != null)
									{
										stringBuilder.Append(string.Concat(new string[]
										{
											"(CP) mts (",
											text2,
											") : (",
											keys3[i],
											")"
										}));
										if (transactionContext.m_enlistedPRList != null)
										{
											for (int j = 0; j < 33; j++)
											{
												PR pr3 = transactionContext.m_enlistedPRList[j];
												if (pr3 != null)
												{
													num++;
													if (pr3.Dump())
													{
														if (pr3.m_sessionType != SessionType.Two_Session_Proxy)
														{
															stringBuilder.Append(string.Format("[{0}:({1}:{2});{3};{4};{5};{6};{7};{8};{9};{10}]", new object[]
															{
																j,
																pr3.m_endUserSessionId,
																pr3.m_endUserSerialNum,
																pr3.m_bCheckedOutByApp.ToString().Substring(0, 1),
																pr3.m_bCheckedOutByDTC.ToString().Substring(0, 1),
																pr3.m_bPutCompleted.ToString().Substring(0, 1),
																pr3.m_bTxnCtxPrimaryCon.ToString().Substring(0, 1),
																(pr3.m_mtsTxnCtx != null) ? pr3.m_mtsTxnCtx.m_txnType.ToString().Substring(0, 1) : "N",
																pr3.ServiceName,
																pr3.EditionName,
																pr3.m_instanceName
															}));
														}
														else
														{
															stringBuilder.Append(string.Concat(new object[]
															{
																"(",
																pr3.m_endUserSessionId,
																",",
																pr3.m_pxyUserSessionId,
																")"
															}));
														}
													}
													else
													{
														stringBuilder.Append("(" + pr3.m_endUserSessionId + ":CLOSED)");
													}
												}
											}
											stringBuilder.Append("(count:" + num + ")");
										}
										else
										{
											stringBuilder.Append("(null list!!!)");
										}
										stringBuilder.AppendLine();
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
			return stringBuilder.ToString();
		}

		// Token: 0x060007A1 RID: 1953 RVA: 0x00048AF0 File Offset: 0x00046CF0
		internal static string Dump(string txnid)
		{
			StringBuilder stringBuilder = new StringBuilder();
			try
			{
				stringBuilder.Append("(DUMP)");
				List<string> keys = OracleConnectionDispenser<PM, CP, PR>.m_htPM.GetKeys();
				foreach (string key in keys)
				{
					PM pm = OracleConnectionDispenser<PM, CP, PR>.m_htPM[key];
					if (pm != null && pm.m_dictDictTxnCtx != null && pm.m_dictDictTxnCtx.ContainsKey(txnid))
					{
						List<string> keys2 = pm.m_dictDictTxnCtx[txnid].GetKeys();
						foreach (string text in keys2)
						{
							TransactionContext<PM, CP, PR> transactionContext = pm.m_dictDictTxnCtx[txnid][text];
							if (transactionContext != null)
							{
								stringBuilder.Append("(txnid=" + txnid + ") ");
								stringBuilder.Append("(service name=" + text + ") ");
								stringBuilder.Append("(pmid=" + pm.m_id + ")[");
								if (transactionContext.m_enlistedPRList != null)
								{
									int num = 0;
									for (int i = 0; i < 33; i++)
									{
										PR pr = transactionContext.m_enlistedPRList[i];
										if (pr != null)
										{
											num++;
											if (pr.Dump())
											{
												if (pr.m_sessionType != SessionType.Two_Session_Proxy)
												{
													stringBuilder.Append(string.Format("[{0}:({1}:{2});{3};{4};{5};{6};{7};{8};{9};{10}]", new object[]
													{
														i,
														pr.m_endUserSessionId,
														pr.m_endUserSerialNum,
														pr.m_bCheckedOutByApp.ToString().Substring(0, 1),
														pr.m_bCheckedOutByDTC.ToString().Substring(0, 1),
														pr.m_bPutCompleted.ToString().Substring(0, 1),
														pr.m_bTxnCtxPrimaryCon.ToString().Substring(0, 1),
														(pr.m_mtsTxnCtx != null) ? pr.m_mtsTxnCtx.m_txnType.ToString().Substring(0, 1) : "N",
														pr.ServiceName,
														pr.EditionName,
														string.Concat(new object[]
														{
															pr.m_instanceName,
															" (pr.m_pm.GetHashCode()      : ",
															pm.GetHashCode(),
															") (pr.m_pm.m_cs.GetHashCode() : ",
															pm.m_cs.GetHashCode(),
															") (pr.m_pm.m_cs.m_userId      : ",
															pm.m_cs.m_userId,
															") (pr.m_pm.m_cs.m_proxyUserId : ",
															pm.m_cs.m_proxyUserId,
															") (pr.m_cs.GetHashCode()      : ",
															pm.m_cs.GetHashCode(),
															") (pr.m_cs.m_userId           : ",
															pm.m_cs.m_userId,
															") (pr.m_cs.m_proxyUserId      : ",
															pm.m_cs.m_proxyUserId,
															") "
														})
													}));
												}
												else
												{
													stringBuilder.Append(string.Concat(new object[]
													{
														"(",
														pr.m_endUserSessionId,
														",",
														pr.m_pxyUserSessionId,
														")"
													}));
												}
											}
											else
											{
												stringBuilder.Append("(" + pr.m_endUserSessionId + ":CLOSED)");
											}
										}
									}
									stringBuilder.Append("(count:" + num + ")");
								}
								else
								{
									stringBuilder.Append("(null list!!!)");
								}
								stringBuilder.Append("] ");
							}
						}
					}
				}
			}
			catch
			{
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060007A2 RID: 1954 RVA: 0x00049000 File Offset: 0x00047200
		internal static PM GetPM(ConnectionString cs, PM conPM, ConnectionString pmCS, SecureString securedPassword, SecureString securedProxyPassword, out bool bAuthenticated, out bool newPM)
		{
			string pmId = cs.m_pmId;
			PM pm = default(PM);
			bAuthenticated = false;
			newPM = false;
			if (conPM == null)
			{
				pm = OracleConnectionDispenser<PM, CP, PR>.m_htPM[pmId];
				if (pm == null)
				{
					lock (OracleConnectionDispenser<PM, CP, PR>.m_htPM)
					{
						pm = OracleConnectionDispenser<PM, CP, PR>.m_htPM[pmId];
						if (pm == null)
						{
							pm = Activator.CreateInstance<PM>();
							pm.Initialize(cs);
							OracleConnectionDispenser<PM, CP, PR>.m_htPM[pmId] = pm;
							OracleConnectionDispenser<PM, CP, PR>.m_listPM.Add(pm);
							newPM = true;
							bAuthenticated = true;
						}
					}
				}
				if (pm != null && !newPM)
				{
					if (pm.m_cs.m_bProxyPasswordSet)
					{
						bAuthenticated = (pm.ProxyPassword == cs.ProxyPassword);
					}
					else
					{
						bAuthenticated = (pm.Password == cs.Password);
					}
				}
			}
			else if (securedPassword == null && securedProxyPassword == null)
			{
				pm = conPM;
				bAuthenticated = true;
			}
			else
			{
				pm = conPM;
				bAuthenticated = true;
				if (pmCS != pm.m_cs)
				{
					if (securedPassword != null)
					{
						bAuthenticated = (pm.Password == ConnectionString.GetStringFromSecureString(securedPassword));
					}
					if (bAuthenticated && securedProxyPassword != null)
					{
						bAuthenticated = (pm.ProxyPassword == ConnectionString.GetStringFromSecureString(securedProxyPassword));
					}
				}
			}
			return pm;
		}

		// Token: 0x060007A3 RID: 1955 RVA: 0x00049180 File Offset: 0x00047380
		internal static PR Get(ConnectionString cs, PM conPM, ConnectionString pmCS, SecureString securedPassword, SecureString securedProxyPassword, CriteriaCtx criteriaCtx)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)2304, new string[0]);
			}
			PR pr = default(PR);
			PR result;
			try
			{
				string pmId = cs.m_pmId;
				PM pm = default(PM);
				bool flag = false;
				bool flag2 = false;
				pm = OracleConnectionDispenser<PM, CP, PR>.GetPM(cs, conPM, pmCS, securedPassword, securedProxyPassword, out flag2, out flag);
				try
				{
					if (flag2 && cs.m_newPassword == null)
					{
						ConnectionString csWithDiffOrNewPwd = null;
						if (cs.m_bProxyUserIdSet || cs.m_bDBStartup)
						{
							csWithDiffOrNewPwd = cs;
						}
						if (Transaction.Current != null && cs.m_enlist == Enlist.True)
						{
							pr = pm.GetEnlisted(csWithDiffOrNewPwd, true, criteriaCtx);
						}
						else
						{
							pr = pm.Get(csWithDiffOrNewPwd, true, criteriaCtx, null, false);
						}
					}
					else
					{
						if (cs.m_bSecured)
						{
							cs = cs.Clone();
							if (securedPassword != null)
							{
								cs.m_password = ConnectionString.GetStringFromSecureString(securedPassword);
							}
							if (securedProxyPassword != null)
							{
								cs.m_proxyPassword = ConnectionString.GetStringFromSecureString(securedProxyPassword);
							}
						}
						pr = pm.GetUsingDiffPassword(cs, criteriaCtx);
						if (pr != null)
						{
							pm.ClearAllPools(pr, false);
							ConnectionString.m_conStringPool.Remove(pm.m_cs);
							pm.m_cs = pm.m_cs.Clone();
							if (cs.m_newPassword != null)
							{
								pm.m_cs.SecureWithNewPassword(cs.m_newPassword);
							}
							else
							{
								pm.m_cs.SecureWithNewPassword(cs.Password);
							}
							pm.m_cs.m_secPxyPwdList.Clear();
							pm.m_cs.m_secPwdList.Clear();
							ConnectionString.m_conStringPool.Put(pm.m_cs);
						}
					}
				}
				catch
				{
					if (flag)
					{
						if (pmId != null && OracleConnectionDispenser<PM, CP, PR>.m_htPM != null)
						{
							OracleConnectionDispenser<PM, CP, PR>.m_htPM.Remove(pmId);
						}
						if (pm != null && OracleConnectionDispenser<PM, CP, PR>.m_listPM != null)
						{
							OracleConnectionDispenser<PM, CP, PR>.m_listPM.Remove(pm);
						}
						if (pm != null && OracleConnectionDispenser<PM, CP, PR>.m_listDataSources != null)
						{
							OracleConnectionDispenser<PM, CP, PR>.m_listDataSources.Remove(pm.m_cs.m_dataSource);
						}
						if (pm != null && pm.m_cs != null)
						{
							ConnectionString.m_conStringPool.Remove(pm.m_cs);
						}
						if (pm != null && pm.m_timer != null)
						{
							try
							{
								pm.m_timer.Dispose();
							}
							catch
							{
							}
						}
					}
					throw;
				}
				if (pm != null)
				{
					pm.InitializeSelfTuning();
				}
				if (OraclePool.m_bPerfCounterEnabled && pr != null)
				{
					OraclePool op = pr.m_cp as OraclePool;
					if (pr.m_cs.m_pooling)
					{
						if (OraclePool.m_bPerfSoftConnectsPerSecond)
						{
							OraclePool.PerformanceCounterIncrement(OraclePerfParams.CounterIndex.SoftConnectsPerSecond, pr as OracleConnectionImpl, op);
						}
					}
					else if (OraclePool.m_bPerfNumberOfNonPooledConnections)
					{
						OraclePool.PerformanceCounterIncrement(OraclePerfParams.CounterIndex.NumberOfNonPooledConnections, pr as OracleConnectionImpl, op);
					}
					if (OraclePool.m_bPerfNumberOfActiveConnections)
					{
						OraclePool.PerformanceCounterIncrement(OraclePerfParams.CounterIndex.NumberOfActiveConnections, pr as OracleConnectionImpl, op);
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
				if (pr != null)
				{
					pr.m_bCheckedOutByApp = true;
				}
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

		// Token: 0x060007A4 RID: 1956 RVA: 0x00049564 File Offset: 0x00047764
		internal static void PutFromApp(PR pr, CriteriaCtx criteriaCtx)
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
				if (pr != null)
				{
					pr.m_bCheckedOutByApp = false;
					OracleConnectionDispenser<PM, CP, PR>.Put(pr, criteriaCtx);
					if (OraclePool.m_bPerfCounterEnabled)
					{
						OraclePool op = pr.m_cp as OraclePool;
						if (pr.m_cs.m_pooling)
						{
							if (OraclePool.m_bPerfNumberOfFreeConnections)
							{
								OraclePool.PerformanceCounterIncrement(OraclePerfParams.CounterIndex.NumberOfFreeConnections, pr as OracleConnectionImpl, op);
							}
							if (OraclePool.m_bPerfSoftDisconnectsPerSecond)
							{
								OraclePool.PerformanceCounterIncrement(OraclePerfParams.CounterIndex.SoftDisconnectsPerSecond, pr as OracleConnectionImpl, op);
							}
						}
						else if (OraclePool.m_bPerfNumberOfNonPooledConnections)
						{
							OraclePool.PerformanceCounterDecrement(OraclePerfParams.CounterIndex.NumberOfNonPooledConnections, pr as OracleConnectionImpl, op);
						}
						if (OraclePool.m_bPerfNumberOfActiveConnections)
						{
							OraclePool.PerformanceCounterDecrement(OraclePerfParams.CounterIndex.NumberOfActiveConnections, pr as OracleConnectionImpl, op);
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
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)2560, new string[]
					{
						Trace.GetCPInfo(pr as OracleConnectionImpl, null, null, null, false, false)
					});
				}
			}
		}

		// Token: 0x060007A5 RID: 1957 RVA: 0x000496C0 File Offset: 0x000478C0
		internal static void PutFromPSPE(Transaction txn, PR pr)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)6400, new string[]
				{
					string.Concat(new object[]
					{
						"Local TxnID = ",
						txn.TransactionInformation.LocalIdentifier,
						" using Conn ID = ",
						pr.m_endUserSessionId,
						" to DBInst = ",
						pr.m_instanceName
					})
				});
			}
			try
			{
				if (pr != null && pr.m_pm != null && pr.m_pm.m_dictDictTxnCtx != null && pr.m_pm.m_dictDictTxnCtx[txn.TransactionInformation.LocalIdentifier] != null && pr.m_pm.m_dictDictTxnCtx[txn.TransactionInformation.LocalIdentifier][pr.ServiceName] != null && pr.m_mtsTxnCtx.m_txnType == MTSTxnType.Distributed)
				{
					pr.m_pm.m_dictDictTxnCtx[txn.TransactionInformation.LocalIdentifier][pr.ServiceName].m_enlistedPRList[0] = default(PR);
				}
				OracleConnectionDispenser<PM, CP, PR>.PutFromDTC(pr);
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
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)6656, new string[]
					{
						string.Concat(new object[]
						{
							"Local TxnID = ",
							txn.TransactionInformation.LocalIdentifier,
							" using Conn ID = ",
							pr.m_endUserSessionId,
							" to DBInst = ",
							pr.m_instanceName
						})
					});
				}
			}
		}

		// Token: 0x060007A6 RID: 1958 RVA: 0x00049904 File Offset: 0x00047B04
		internal static void PutFromDTC(PR pr)
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
				if (pr != null)
				{
					lock (pr)
					{
						if (pr.m_bCheckedOutByDTC)
						{
							pr.m_resPoolRefCount--;
						}
						if (pr.m_resPoolRefCount != 0)
						{
							return;
						}
						pr.m_bCheckedOutByDTC = false;
					}
					OracleConnectionDispenser<PM, CP, PR>.Put(pr, null);
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

		// Token: 0x060007A7 RID: 1959 RVA: 0x00049A24 File Offset: 0x00047C24
		private static void Put(PR pr, CriteriaCtx criteriaCtx)
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
				if (pr.m_mtsTxnCtx != null)
				{
					pr.m_pm.m_criteriaMapper.GetId(pr as OracleConnectionImpl);
					if (pr.m_mtsTxnCtx.m_txnType == MTSTxnType.Local)
					{
						return;
					}
					if (pr.m_mtsTxnCtx != null && pr.m_mtsTxnCtx.m_txnType == MTSTxnType.Distributed)
					{
						lock (pr)
						{
							if (pr.m_mtsTxnCtx != null && pr.m_mtsTxnCtx.m_txnType == MTSTxnType.Distributed)
							{
								if (pr.m_txnCtx == null && pr.m_mtsTxnCtx != null && pr.m_pm.m_dictDictTxnCtx[pr.m_mtsTxnCtx.m_txnLocalID] != null)
								{
									pr.m_txnCtx = pr.m_pm.m_dictDictTxnCtx[pr.m_mtsTxnCtx.m_txnLocalID][pr.ServiceName];
								}
								if (pr.m_txnCtx != null && !pr.m_bCheckedOutByApp && !pr.m_bCheckedOutByDTC && !pr.m_bPutCompleted && pr.m_mtsTxnCtx != null && pr.m_mtsTxnCtx.m_txnType == MTSTxnType.Distributed && !pr.m_bTxnCtxPrimaryCon)
								{
									lock (pr.m_txnCtx)
									{
										if (pr.m_mtsTxnCtx != null && pr.m_mtsTxnCtx.m_txnType == MTSTxnType.Distributed)
										{
											try
											{
												pr.m_mtsTxnCtx.DelistTransaction(pr as OracleConnectionImpl);
											}
											catch
											{
											}
											if (ProviderConfig.m_bTraceLevelPrivate)
											{
												Trace.Write(OracleTraceLevel.Private, OracleTraceTag.CP, new string[]
												{
													Trace.GetCPInfo(pr as OracleConnectionImpl, null, null, "disp:put:del1", false, false)
												});
											}
										}
										if (pr.m_localTxnId == pr.m_txnCtx.m_localTxnId)
										{
											PR pr2 = default(PR);
											if (pr.m_mtsTxnCtx.m_mtsTxnBranch != null)
											{
												pr2 = pr.m_txnCtx.m_enlistedPRList[pr.m_mtsTxnCtx.m_mtsTxnBranch.BranchNumber];
											}
											if (pr2 != null && pr2.m_endUserSessionId == pr.m_endUserSessionId && pr2.m_endUserSerialNum == pr.m_endUserSerialNum)
											{
												if (ProviderConfig.m_bTraceLevelPrivate)
												{
													Trace.Write(OracleTraceLevel.Private, OracleTraceTag.CP, new string[]
													{
														Trace.GetCPInfo(pr as OracleConnectionImpl, null, null, "disp:put:nul1", true, false)
													});
												}
												pr.m_txnCtx.m_enlistedPRList[pr.m_mtsTxnCtx.m_mtsTxnBranch.BranchNumber] = default(PR);
											}
											else if (ProviderConfig.m_bTraceLevelPrivate)
											{
												Trace.Write(OracleTraceLevel.Private, OracleTraceTag.CP, new string[]
												{
													Trace.GetCPInfo(pr as OracleConnectionImpl, null, null, "disp:put:didnotnullyafterdelist", true, false)
												});
											}
										}
									}
									if (pr.m_mtsTxnCtx != null)
									{
										pr.m_mtsTxnCtx.m_mtsTxnBranch = null;
									}
									pr.m_txnCtx = null;
									pr.m_pm.Put(pr, criteriaCtx);
								}
								else if (pr.m_txnCtx != null && pr.m_mtsTxnCtx != null && pr.m_mtsTxnCtx.m_txnType != MTSTxnType.None && pr.m_mtsTxnCtx.m_mtsTxnBranch != null && pr.m_localTxnId == pr.m_txnCtx.m_localTxnId && pr.m_txnCtx.m_enlistedPRList[pr.m_mtsTxnCtx.m_mtsTxnBranch.BranchNumber] == null)
								{
									lock (pr.m_txnCtx)
									{
										if (pr.m_mtsTxnCtx != null && pr.m_mtsTxnCtx.m_txnType != MTSTxnType.None && pr.m_mtsTxnCtx.m_mtsTxnBranch != null && pr.m_localTxnId == pr.m_txnCtx.m_localTxnId && pr.m_txnCtx.m_enlistedPRList[pr.m_mtsTxnCtx.m_mtsTxnBranch.BranchNumber] == null)
										{
											pr.m_txnCtx.m_enlistedPRList[pr.m_mtsTxnCtx.m_mtsTxnBranch.BranchNumber] = pr;
											if (pr.m_mtsTxnCtx.m_mtsTxnBranch.BranchNumber != 0 && !pr.m_txnCtx.m_instances.Contains(pr.m_instanceName))
											{
												pr.m_txnCtx.m_instances.Add(pr.m_instanceName);
												pr.m_bTxnCtxPrimaryCon = true;
											}
											if (pr.m_mtsTxnCtx.m_mtsTxnBranch.BranchNumber > pr.m_txnCtx.m_maxBranchIndex)
											{
												pr.m_txnCtx.m_maxBranchIndex = pr.m_mtsTxnCtx.m_mtsTxnBranch.BranchNumber;
											}
											if (ProviderConfig.m_bTraceLevelPrivate)
											{
												Trace.Write(OracleTraceLevel.Private, OracleTraceTag.CP, new string[]
												{
													Trace.GetCPInfo(pr as OracleConnectionImpl, null, null, "disp:put:res1", true, false)
												});
											}
										}
									}
								}
							}
						}
					}
					if (pr.m_bPutCompleted || pr.m_mtsTxnCtx.m_txnType != MTSTxnType.None || pr.m_bTxnCtxPrimaryCon)
					{
						goto IL_BFA;
					}
					lock (pr)
					{
						if (!pr.m_bPutCompleted && pr.m_mtsTxnCtx.m_txnType == MTSTxnType.None && !pr.m_bTxnCtxPrimaryCon && pr.m_txnCtx != null && pr.m_mtsTxnCtx != null && pr.m_mtsTxnCtx.m_mtsTxnBranch != null && pr.m_localTxnId == pr.m_txnCtx.m_localTxnId)
						{
							lock (pr.m_txnCtx)
							{
								if (pr.m_mtsTxnCtx != null && pr.m_mtsTxnCtx.m_mtsTxnBranch != null && pr.m_localTxnId == pr.m_txnCtx.m_localTxnId && pr.m_mtsTxnCtx.m_txnType == MTSTxnType.None)
								{
									PR pr3 = default(PR);
									if (pr.m_mtsTxnCtx.m_mtsTxnBranch != null)
									{
										pr3 = pr.m_txnCtx.m_enlistedPRList[pr.m_mtsTxnCtx.m_mtsTxnBranch.BranchNumber];
									}
									if (pr3 != null && pr3.m_endUserSessionId == pr.m_endUserSessionId && pr3.m_endUserSerialNum == pr.m_endUserSerialNum)
									{
										if (ProviderConfig.m_bTraceLevelPrivate)
										{
											Trace.Write(OracleTraceLevel.Private, OracleTraceTag.CP, new string[]
											{
												Trace.GetCPInfo(pr as OracleConnectionImpl, null, null, "disp:put:null1", true, false)
											});
										}
										pr.m_txnCtx.m_enlistedPRList[pr.m_mtsTxnCtx.m_mtsTxnBranch.BranchNumber] = default(PR);
									}
								}
							}
						}
						if (!pr.m_bCheckedOutByApp && !pr.m_bPutCompleted && !pr.m_bCheckedOutByDTC && !pr.m_bTxnCtxPrimaryCon)
						{
							if (pr.m_mtsTxnCtx != null)
							{
								pr.m_mtsTxnCtx.m_mtsTxnBranch = null;
							}
							pr.m_txnCtx = null;
							pr.m_pm.Put(pr, criteriaCtx);
						}
						goto IL_BFA;
					}
				}
				if (!pr.m_bCheckedOutByApp && !pr.m_bPutCompleted && !pr.m_bCheckedOutByDTC)
				{
					lock (pr)
					{
						if (!pr.m_bCheckedOutByApp && !pr.m_bPutCompleted && !pr.m_bCheckedOutByDTC)
						{
							if (pr.m_txnCtx != null && pr.m_mtsTxnCtx != null && pr.m_mtsTxnCtx.m_mtsTxnBranch != null && pr.m_localTxnId == pr.m_txnCtx.m_localTxnId && pr.m_mtsTxnCtx.m_txnType == MTSTxnType.None)
							{
								lock (pr.m_txnCtx)
								{
									if (pr.m_mtsTxnCtx != null && pr.m_mtsTxnCtx.m_mtsTxnBranch != null && pr.m_localTxnId == pr.m_txnCtx.m_localTxnId && pr.m_mtsTxnCtx.m_txnType == MTSTxnType.None)
									{
										PR pr4 = default(PR);
										if (pr.m_mtsTxnCtx.m_mtsTxnBranch != null)
										{
											pr4 = pr.m_txnCtx.m_enlistedPRList[pr.m_mtsTxnCtx.m_mtsTxnBranch.BranchNumber];
										}
										if (pr4 != null && pr4.m_endUserSessionId == pr.m_endUserSessionId && pr4.m_endUserSerialNum == pr.m_endUserSerialNum)
										{
											if (ProviderConfig.m_bTraceLevelPrivate)
											{
												Trace.Write(OracleTraceLevel.Private, OracleTraceTag.CP, new string[]
												{
													Trace.GetCPInfo(pr as OracleConnectionImpl, null, null, "disp:put:nul2", true, false)
												});
											}
											pr.m_txnCtx.m_enlistedPRList[pr.m_mtsTxnCtx.m_mtsTxnBranch.BranchNumber] = default(PR);
										}
									}
								}
							}
							pr.m_txnCtx = null;
							pr.m_pm.Put(pr, criteriaCtx);
						}
					}
				}
				IL_BFA:;
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

		// Token: 0x060007A8 RID: 1960 RVA: 0x0004A76C File Offset: 0x0004896C
		internal static void ClearAllPools()
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)2304, new string[0]);
			}
			try
			{
				List<PM> list = OracleConnectionDispenser<PM, CP, PR>.m_listPM.GetList();
				for (int i = 0; i < list.Count; i++)
				{
					PM pm = list[i];
					pm.ClearAllPools(default(PR), false);
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

		// Token: 0x060007A9 RID: 1961 RVA: 0x0004A818 File Offset: 0x00048A18
		internal static void ClearPool(ConnectionString cs)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)2304, new string[0]);
			}
			try
			{
				PM pm = OracleConnectionDispenser<PM, CP, PR>.m_htPM[cs.m_pmId];
				if (pm != null)
				{
					pm.ClearAllPools(default(PR), false);
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

		// Token: 0x060007AA RID: 1962 RVA: 0x0004A8B8 File Offset: 0x00048AB8
		internal static List<string> GetDataSources()
		{
			return OracleConnectionDispenser<PM, CP, PR>.m_listDataSources.GetList();
		}

		// Token: 0x060007AB RID: 1963 RVA: 0x0004A8C4 File Offset: 0x00048AC4
		internal static void ProcessHAEvent(OracleHAEventArgs haEvent)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)2304, new string[0]);
			}
			string a = string.Empty;
			bool flag = true;
			try
			{
				if (haEvent.m_bFireHADotNetEvent)
				{
					ThreadPool.QueueUserWorkItem(new WaitCallback(OracleConnection.OnHAEvent), haEvent);
				}
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					string text = "haEvent.DatabaseName      : " + haEvent.DatabaseName + " \n";
					text = text + "haEvent.ServiceName       : " + haEvent.ServiceName + " \n";
					text = text + "haEvent.InstanceName      : " + haEvent.InstanceName + " \n";
					object obj = text;
					text = string.Concat(new object[]
					{
						obj,
						"haEvent.Source            : ",
						haEvent.Source,
						" \n"
					});
					text = text + "haEvent.HostName          : " + haEvent.HostName + " \n";
					text = text + "haEvent.Reason            : " + haEvent.Reason + " \n";
					object obj2 = text;
					text = string.Concat(new object[]
					{
						obj2,
						"haEvent.Status            : ",
						haEvent.Status,
						" \n"
					});
					object obj3 = text;
					text = string.Concat(new object[]
					{
						obj3,
						"haEvent.Time              : ",
						haEvent.Time,
						" \n"
					});
					text = text + "haEvent.DatabaseDomainName: " + haEvent.DatabaseDomainName + " \n";
					object obj4 = text;
					text = string.Concat(new object[]
					{
						obj4,
						"haEvent.drain_timeout   : ",
						haEvent.DrainTimeout,
						" \n"
					});
					Trace.Write(OracleTraceLevel.Private, OracleTraceTag.CP, new string[]
					{
						"ProcessHAEvent Info: \n" + text
					});
				}
				if (haEvent.Status == OracleHAEventStatus.Down)
				{
					if (!string.IsNullOrEmpty(haEvent.Reason))
					{
						a = haEvent.Reason.Trim().ToLowerInvariant();
					}
					if (a == "user" || a == "user_action" || a == "application_state_change")
					{
						flag = false;
					}
					List<PM> list;
					List<CP> list2;
					switch (haEvent.Source)
					{
					case OracleHAEventSource.Instance:
					case OracleHAEventSource.ServiceMember:
						break;
					case OracleHAEventSource.Database:
					case OracleHAEventSource.Service:
					{
						list = OracleConnectionDispenser<PM, CP, PR>.m_listPM.GetList();
						list2 = new List<CP>();
						List<PM> list3 = new List<PM>();
						for (int i = 0; i < list.Count; i++)
						{
							PM pm = list[i];
							if (pm == null || pm.m_bHAEnabled)
							{
								pm.m_drain_timeout = haEvent.DrainTimeout;
								string text2 = haEvent.ServiceName.ToLowerInvariant();
								string a2 = haEvent.DatabaseName.ToLowerInvariant();
								Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)2304, new string[]
								{
									string.Format("Service {0} present in SvcCtx: {1}, SvcCtx.DatabaseName: {2}, PM.ID: {3}", new object[]
									{
										text2,
										pm.m_dictSvcCtx.ContainsKey(text2).ToString(),
										pm.m_dictSvcCtx.ContainsKey(text2) ? pm.m_dictSvcCtx[text2].m_databaseName : "null",
										pm.m_id
									})
								});
								if ((haEvent.DatabaseDomainName == null || haEvent.DatabaseDomainName.Length == 0 || (pm.m_databaseDomainName != null && pm.m_databaseDomainName.ToLowerInvariant() == haEvent.DatabaseDomainName.ToLowerInvariant())) && pm.m_dictSvcCtx.ContainsKey(text2) && a2 == pm.m_dictSvcCtx[text2].m_databaseName.ToLowerInvariant())
								{
									pm.m_dictSvcCtx[text2].UpdateServiceDown(true, haEvent.Time);
									if (pm.m_dictDictCP[haEvent.ServiceName] != null)
									{
										SyncDictionary<string, CP> syncDictionary = pm.m_dictDictCP[haEvent.ServiceName];
										foreach (CP item in syncDictionary.GetValues())
										{
											item.MarkAllPRsForDeletion(haEvent.Time, flag);
											list2.Add(item);
										}
									}
									string id = (haEvent.DatabaseName + "|" + haEvent.ServiceName).ToLowerInvariant();
									RLBManager.InvalidateRLBData(id);
									if (!flag)
									{
										if (ProviderConfig.m_bTraceLevelPrivate)
										{
											Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)2304, new string[]
											{
												string.Format("Service DOWN for service: {0}", haEvent.ServiceName)
											});
										}
										pm.m_dictSvcCtx[text2].m_serviceDownTime = DateTime.Now;
										if (ProviderConfig.m_bTraceLevelPrivate)
										{
											Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)2304, new string[]
											{
												"ServiceUpEvent RESET for connection pool " + pm.GetHashCode()
											});
										}
										pm.m_dictSvcCtx[text2].m_serviceUpEvent.Reset();
										pm.m_dictSvcCtx[text2].m_bWaitedForSvcReloc = false;
									}
									if (pm.m_dictSvcCtx.Count == 1)
									{
										list3.Add(pm);
									}
								}
							}
						}
						foreach (CP cp in list2)
						{
							cp.ClearPool(default(PR), flag);
						}
						using (List<PM>.Enumerator enumerator3 = list3.GetEnumerator())
						{
							while (enumerator3.MoveNext())
							{
								PM pm2 = enumerator3.Current;
								pm2.ClearAllPools(default(PR), flag);
							}
							goto IL_108C;
						}
						break;
					}
					case OracleHAEventSource.Node:
						goto IL_983;
					default:
						goto IL_108C;
					}
					list = OracleConnectionDispenser<PM, CP, PR>.m_listPM.GetList();
					list2 = new List<CP>();
					for (int j = 0; j < list.Count; j++)
					{
						PM pm3 = list[j];
						Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)2304, new string[]
						{
							string.Format("Service {0} present in SvcCtx: {1}, SvcCtx.DatabaseName: {2}, currentPM.ID: {3}, currentPM.DatabaseDomainName: {4}", new object[]
							{
								haEvent.ServiceName,
								pm3.m_dictSvcCtx.ContainsKey(haEvent.ServiceName),
								pm3.m_dictSvcCtx.ContainsKey(haEvent.ServiceName) ? pm3.m_dictSvcCtx[haEvent.ServiceName].m_databaseName : "null",
								pm3.m_id,
								pm3.m_databaseDomainName
							})
						});
						if (pm3 == null || pm3.m_bHAEnabled)
						{
							pm3.m_drain_timeout = haEvent.DrainTimeout;
							if ((haEvent.DatabaseDomainName == null || haEvent.DatabaseDomainName.Length == 0 || (pm3.m_databaseDomainName != null && pm3.m_databaseDomainName.ToLowerInvariant() == haEvent.DatabaseDomainName.ToLowerInvariant())) && pm3.m_dictSvcCtx.ContainsKey(haEvent.ServiceName) && pm3.m_dictDictCP[haEvent.ServiceName] != null && pm3.m_dictDictCP[haEvent.ServiceName].Count > 0 && pm3.m_dictSvcCtx[haEvent.ServiceName].m_databaseName.ToLowerInvariant() == haEvent.DatabaseName.ToLowerInvariant())
							{
								if (ProviderConfig.m_bTraceLevelPrivate)
								{
									Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)2304, new string[]
									{
										string.Format("ServiceMember DOWN for service: {0}, instance name: {1}", haEvent.ServiceName, haEvent.InstanceName)
									});
								}
								pm3.m_dictSvcCtx[haEvent.ServiceName].CheckAndUpdateServiceMemberDOWNNames_HA(haEvent.InstanceName, true, haEvent.Time);
								CP cp2 = default(CP);
								if (pm3.m_dictDictCP[haEvent.ServiceName] != null && pm3.m_dictDictCP[haEvent.ServiceName].ContainsKey(haEvent.InstanceName))
								{
									cp2 = pm3.m_dictDictCP[haEvent.ServiceName][haEvent.InstanceName];
								}
								if (cp2 != null)
								{
									cp2.MarkAllPRsForDeletion(haEvent.Time, flag);
									string id2 = (haEvent.DatabaseName + "|" + haEvent.ServiceName).ToLowerInvariant();
									RLBManager.InvalidateRLBData(id2);
									list2.Add(cp2);
								}
							}
						}
					}
					using (List<CP>.Enumerator enumerator = list2.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							CP cp3 = enumerator.Current;
							cp3.ClearPool(default(PR), flag);
						}
						goto IL_108C;
					}
					IL_983:
					list = OracleConnectionDispenser<PM, CP, PR>.m_listPM.GetList();
					for (int k = 0; k < list.Count; k++)
					{
						bool flag2 = false;
						PM pm4 = list[k];
						if (pm4 == null || pm4.m_bHAEnabled)
						{
							List<PR> list4 = pm4.m_pmListPR.GetList();
							for (int l = 0; l < list4.Count; l++)
							{
								PR pr = list4[l];
								if (pr.m_hostName == haEvent.HostName)
								{
									lock (pr)
									{
										pr.m_deletionRequestor = DeletionRequestor.HA;
										pr.m_pm.Close(pr, null);
									}
									flag2 = true;
								}
							}
							if (flag2)
							{
								string id3 = (haEvent.DatabaseName + "|" + haEvent.ServiceName).ToLowerInvariant();
								RLBManager.InvalidateRLBData(id3);
							}
						}
					}
				}
				else
				{
					if (!string.IsNullOrEmpty(haEvent.Reason))
					{
						a = haEvent.Reason.Trim().ToLowerInvariant();
					}
					if (a == "user" || a == "user_action" || a == "application_state_change")
					{
						flag = false;
					}
					List<PM> list5 = null;
					switch (haEvent.Source)
					{
					case OracleHAEventSource.Instance:
					case OracleHAEventSource.ServiceMember:
						list5 = OracleConnectionDispenser<PM, CP, PR>.m_listPM.GetList();
						for (int m = 0; m < list5.Count; m++)
						{
							PM pm5 = list5[m];
							if ((pm5 == null || pm5.m_bHAEnabled) && (haEvent.DatabaseDomainName == null || haEvent.DatabaseDomainName.Length == 0 || (pm5.m_databaseDomainName != null && pm5.m_databaseDomainName.ToLowerInvariant() == haEvent.DatabaseDomainName.ToLowerInvariant())) && pm5.m_dictSvcCtx.ContainsKey(haEvent.ServiceName) && pm5.m_dictDictCP[haEvent.ServiceName] != null && pm5.m_dictDictCP[haEvent.ServiceName].Count > 0 && pm5.m_dictSvcCtx[haEvent.ServiceName].m_databaseName.ToLowerInvariant() == haEvent.DatabaseName.ToLowerInvariant())
							{
								if (ProviderConfig.m_bTraceLevelPrivate)
								{
									Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)2304, new string[]
									{
										string.Format("ServiceMember UP for service: {0}, instance name: {1}, currentPM.ID: {2}", haEvent.ServiceName, haEvent.InstanceName, pm5.m_id)
									});
								}
								pm5.m_dictSvcCtx[haEvent.ServiceName].CheckAndUpdateServiceMemberDOWNNames_HA(haEvent.InstanceName, false, haEvent.Time);
								pm5.m_dictSvcCtx[haEvent.ServiceName].UpdateServiceDown(false, haEvent.Time);
								CP cp4 = default(CP);
								if (pm5.m_dictDictCP[haEvent.ServiceName] != null && pm5.m_dictDictCP[haEvent.ServiceName].ContainsKey(haEvent.InstanceName))
								{
									cp4 = pm5.m_dictDictCP[haEvent.ServiceName][haEvent.InstanceName];
								}
								if (cp4 != null)
								{
									SyncQueueList<PR> cpListPR = cp4.m_cpListPR;
									for (int n = 0; n < cpListPR.Count; n++)
									{
										try
										{
											PR pr2 = cpListPR[n];
											if (pr2 != null)
											{
												lock (pr2)
												{
													if (flag && !pr2.IsTAFEnabled())
													{
														pr2.m_deletionRequestor = DeletionRequestor.HA;
													}
												}
											}
										}
										catch
										{
										}
									}
									bool flag5 = false;
									if (cp4.m_bInstanceDown && haEvent.Time > cp4.m_lastHADownEventUtcDateTime)
									{
										flag5 = true;
										cp4.m_bInstanceDown = false;
										if (OracleHAEventSource.ServiceMember == haEvent.Source)
										{
											pm5.m_dictSvcCtx[haEvent.ServiceName].m_serviceUpEvent.Set();
											pm5.m_dictSvcCtx[haEvent.ServiceName].m_bWaitedForSvcReloc = false;
											if (ProviderConfig.m_bTraceLevelPrivate)
											{
												Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)2304, new string[]
												{
													"ServiceMemberUp: ServiceUpEvent SET for connection pool " + pm5.GetHashCode()
												});
											}
										}
									}
									if (ProviderConfig.m_bTraceLevelPrivate)
									{
										string text3 = string.Format("[pm_id={0}][instance={1}][instance_down={2}][type=ha_up_event][state_changed={3}][up_event_utc={4}][last_down_event_utc={5}]", new object[]
										{
											pm5.m_id,
											cp4.m_instanceName,
											cp4.m_bInstanceDown,
											flag5,
											haEvent.Time.ToString(),
											cp4.m_lastHADownEventUtcDateTime.ToString()
										});
										Trace.Write(OracleTraceLevel.Private, OracleTraceTag.CP, new string[]
										{
											text3
										});
									}
								}
							}
						}
						break;
					case OracleHAEventSource.Database:
					case OracleHAEventSource.Service:
						list5 = OracleConnectionDispenser<PM, CP, PR>.m_listPM.GetList();
						for (int num = 0; num < list5.Count; num++)
						{
							PM pm6 = list5[num];
							if ((pm6 == null || pm6.m_bHAEnabled) && haEvent.ServiceName != null && pm6.m_dictSvcCtx.ContainsKey(haEvent.ServiceName) && OracleHAEventSource.Service == haEvent.Source)
							{
								if (ProviderConfig.m_bTraceLevelPrivate)
								{
									Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)2304, new string[]
									{
										string.Format("Service UP for service: {0}, currentPM.ID: {1}", haEvent.ServiceName, pm6.m_id)
									});
								}
								pm6.m_dictSvcCtx[haEvent.ServiceName].UpdateServiceDown(false, haEvent.Time);
								pm6.m_dictSvcCtx[haEvent.ServiceName].m_serviceUpEvent.Set();
								pm6.m_dictSvcCtx[haEvent.ServiceName].m_bWaitedForSvcReloc = false;
								if (ProviderConfig.m_bTraceLevelPrivate)
								{
									Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)2304, new string[]
									{
										"ServiceUp: ServiceUpEvent SET for connection pool " + pm6.GetHashCode()
									});
								}
							}
						}
						break;
					}
				}
				IL_108C:;
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

		// Token: 0x060007AC RID: 1964 RVA: 0x0004BA68 File Offset: 0x00049C68
		internal static PR GetEnlisted(ConnectionString cs, string serviceName, string pdbName, Transaction txn, string affinityInstance, int branchNum, bool bMustMatch, out bool bMatchFound)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)2304, new string[]
				{
					Trace.GetCPInfo(null, txn, null, null, false, false) + "Disp.GetEnlisted(br={0};must={1})",
					branchNum.ToString(),
					bMustMatch.ToString()
				});
			}
			bMatchFound = false;
			PR pr = default(PR);
			PR result;
			try
			{
				string pmId = cs.m_pmId;
				PM pm = default(PM);
				bool flag = false;
				bool flag2 = false;
				pm = OracleConnectionDispenser<PM, CP, PR>.GetPM(cs, default(PM), null, null, null, out flag2, out flag);
				pr = pm.GetEnlisted(txn, affinityInstance, branchNum, bMustMatch, out bMatchFound, serviceName, pdbName);
				if (pr == null && bMustMatch)
				{
					result = pr;
				}
				else
				{
					if (pr == null)
					{
						pr = pm.Get(cs, false, new CriteriaCtx
						{
							m_serviceName = serviceName,
							m_pdbName = pdbName,
							m_fromMTS = true
						}, affinityInstance, false);
						if (pr != null)
						{
							pr.m_resPoolRefCount++;
						}
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
						Trace.GetCPInfo(pr as OracleConnectionImpl, txn, null, null, false, false) + "Disp.GetEnlisted(br={0};must={1}) return (matchfound={2})",
						branchNum.ToString(),
						bMustMatch.ToString().Substring(0, 1),
						bMatchFound.ToString().Substring(0, 1)
					});
				}
			}
			return result;
		}

		// Token: 0x060007AD RID: 1965 RVA: 0x0004BC24 File Offset: 0x00049E24
		internal static MTSTxnRM GetRM(ConnectionString cs, CriteriaCtx criteriaCtx, Transaction txn, OracleConnectionImpl pr)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)2304, new string[0]);
			}
			PM pm = default(PM);
			MTSTxnRM rm;
			try
			{
				bool flag = false;
				bool flag2 = false;
				pm = OracleConnectionDispenser<PM, CP, PR>.GetPM(cs, default(PM), null, null, null, out flag, out flag2);
				rm = pm.GetRM(txn, criteriaCtx, pr as PR);
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
						Trace.GetCPInfo(pr, null, null, null, false, false)
					});
				}
			}
			return rm;
		}

		// Token: 0x060007AE RID: 1966 RVA: 0x0004BCE8 File Offset: 0x00049EE8
		internal static void RemoveRM(ConnectionString cs, string serviceName, Transaction txn)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)2304, new string[]
				{
					Trace.GetCPInfo(null, txn, null, null, false, false)
				});
			}
			PM pm = default(PM);
			try
			{
				bool flag = false;
				bool flag2 = false;
				pm = OracleConnectionDispenser<PM, CP, PR>.GetPM(cs, default(PM), null, null, null, out flag, out flag2);
				if (pm != null)
				{
					pm.RemoveRM(serviceName, txn);
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
						Trace.GetCPInfo(null, txn, null, null, false, false)
					});
				}
			}
		}

		// Token: 0x04000A51 RID: 2641
		public static SyncDictionary<string, PM> m_htPM;

		// Token: 0x04000A52 RID: 2642
		public static SyncQueueList<PM> m_listPM;

		// Token: 0x04000A53 RID: 2643
		internal static SyncQueueList<string> m_listDataSources;

		// Token: 0x04000A54 RID: 2644
		internal static object m_syncObjForGetDataSources = new object();

		// Token: 0x04000A55 RID: 2645
		internal static FileSystemWatcher s_sepsFileWatcher;

		// Token: 0x04000A56 RID: 2646
		internal static object s_sepsFileWatcherCreationLock = new object();

		// Token: 0x04000A57 RID: 2647
		internal static bool s_bSEPSFileWatcherCreated = false;
	}
}
