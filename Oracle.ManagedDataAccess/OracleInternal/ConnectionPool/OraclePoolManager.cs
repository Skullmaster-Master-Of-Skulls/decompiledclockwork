using System;
using System.Globalization;
using System.Threading;
using Oracle.ManagedDataAccess.Client;
using OracleInternal.BinXml;
using OracleInternal.Common;
using OracleInternal.Network;
using OracleInternal.SelfTuning;
using OracleInternal.ServiceObjects;

namespace OracleInternal.ConnectionPool
{
	// Token: 0x020000D3 RID: 211
	internal class OraclePoolManager : PoolManager<OraclePoolManager, OraclePool, OracleConnectionImpl>, IOracleTunable
	{
		// Token: 0x0600080E RID: 2062 RVA: 0x00055230 File Offset: 0x00053430
		internal override void Initialize(ConnectionString cs)
		{
			base.Initialize(cs);
			this.m_appThreadLCID = CultureInfo.CurrentCulture.LCID;
			int num = this.m_cs.m_maxPoolSize;
			if (num > 200)
			{
				num = 200;
			}
			this.m_oraBufPool = new OraBufPool(num * OraclePoolManager.s_maxListCapacity);
			this.m_bSelfTuning = (this.m_cs.m_selfTuning && this.m_cs.m_pooling);
			this.m_recommendedSCS = ((!this.m_bSelfTuning) ? this.m_cs.m_stmtCacheSize : ((30 <= ProviderConfig.MaxStatementCacheSize.Value) ? 30 : ProviderConfig.MaxStatementCacheSize.Value));
			if (this.m_cs.m_metadataPooling)
			{
				if (this.m_dictStatementmetadatacache == null)
				{
					this.m_dictStatementmetadatacache = new SyncDictionary<string, SQLLocalParsePrimaryKeyInfoPool>();
				}
				if (this.m_dictDeriveParamInfoPool == null)
				{
					this.m_dictDeriveParamInfoPool = new SyncDictionary<string, DeriveParamInfoPool>();
				}
				if (this.m_dictXmlSchemaPool == null)
				{
					this.m_dictXmlSchemaPool = new SyncDictionary<string, XmlSchemaPool>();
				}
			}
			if (this.m_sqlParseInfoPool == null)
			{
				this.m_sqlParseInfoPool = new SQLParseInfoPool(500);
			}
			if (this.m_xmlTokenManager == null && ConfigBaseClass.m_XMLTypeClientSideDecoding)
			{
				this.m_xmlTokenManager = new ObxmlTokenManager(this);
			}
			if (this.m_cs.m_pooling)
			{
				this.InitializeSEPSCredentials();
			}
			if (this.m_tableColumnsCache == null)
			{
				this.m_tableColumnsCache = new TableColumnsCache();
			}
		}

		// Token: 0x0600080F RID: 2063 RVA: 0x00055370 File Offset: 0x00053570
		internal void InitializeSEPSCredentials()
		{
			string password = this.m_cs.Password;
			string proxyPassword = this.m_cs.ProxyPassword;
			if (this.m_cs.m_userId != "/")
			{
				string userId = this.m_cs.m_userId;
			}
			if (this.m_cs.m_proxyUserId != "/")
			{
				string proxyUserId = this.m_cs.m_proxyUserId;
			}
			if (this.m_cs.m_dbaPrivilege == DBAPrivilege.SYSDBA)
			{
				this.m_logonMode |= 32L;
				this.m_bDoExternalAuth = (this.m_bDoNAHandShake = true);
			}
			else if (this.m_cs.m_dbaPrivilege == DBAPrivilege.SYSOPER)
			{
				this.m_logonMode |= 64L;
				this.m_bDoExternalAuth = (this.m_bDoNAHandShake = true);
			}
			if (!this.m_bDoExternalAuth && ("/" == this.m_cs.m_userId || "/" == this.m_cs.m_proxyUserId))
			{
				if (!SqlNetOraConfig.WalletOverride)
				{
					this.m_bDoExternalAuth = (this.m_bDoNAHandShake = true);
					return;
				}
				this.m_bDoExternalAuth = false;
				string text = null;
				string text2 = null;
				string walletPath = null;
				string walletFile = null;
				OraclePoolManager.FetchSEPSCredentails(this.m_cs.m_dataSource, out text, out text2, out walletPath, out walletFile);
				if ("/" == this.m_cs.m_userId)
				{
					this.m_cs.m_sepsUserId = text;
					this.m_cs.m_sepsPassword = text2;
					this.m_cs.m_sepsSecPwdList.Clear();
					this.m_bSEPSForProxyCredentials = false;
				}
				else if ("/" == this.m_cs.m_proxyUserId)
				{
					this.m_cs.m_sepsProxyUserId = text;
					this.m_cs.m_sepsProxyPassword = text2;
					this.m_cs.m_sepsSecPxyPwdList.Clear();
					this.m_bSEPSForProxyCredentials = true;
				}
				this.m_cs.SecureSEPSPassword();
				this.m_bUsingSEPSCredentials = true;
				this.m_bSEPSCredentialsFetched = true;
				Trace.Write(OracleTraceLevel.Private, OracleTraceTag.CP, new string[]
				{
					"InitializeSEPSCredentials => SEPS credentials stored in the PM CACHE for PM: " + this.m_id
				});
				if (!OracleConnectionDispenser<OraclePoolManager, OraclePool, OracleConnectionImpl>.s_bSEPSFileWatcherCreated)
				{
					OracleConnectionDispenser<OraclePoolManager, OraclePool, OracleConnectionImpl>.CreateSEPSFileWatcher(walletPath, walletFile);
				}
			}
		}

		// Token: 0x06000810 RID: 2064 RVA: 0x00055598 File Offset: 0x00053798
		internal static void FetchSEPSCredentails(string ds, out string seps_userId, out string seps_password, out string walletPath, out string walletFile)
		{
			seps_userId = null;
			seps_password = null;
			walletPath = null;
			walletFile = null;
			for (int i = 0; i < OraclePoolManager.NUM_CRED_RETRIEVAL_TRIES; i++)
			{
				try
				{
					Trace.Write(OracleTraceLevel.Private, OracleTraceTag.CP, new string[]
					{
						"FetchSEPSCredentails => Querying Data Source (TRIAL: {0}): {1}",
						i.ToString(),
						ds
					});
					OracleCommunication.GetSEPSUserIDandPW(ds, out seps_userId, out seps_password, out walletPath, out walletFile);
					Trace.Write(OracleTraceLevel.Private, OracleTraceTag.CP, new string[]
					{
						"FetchSEPSCredentails => Got Credentials from Client Wallet"
					});
					break;
				}
				catch (Exception ex)
				{
					Trace.Write(OracleTraceLevel.Private, OracleTraceTag.CP, new string[]
					{
						"FetchSEPSCredentails => Exception (TRIAL: {0}): {1}",
						i.ToString(),
						ex.ToString()
					});
					if (i < OraclePoolManager.NUM_CRED_RETRIEVAL_TRIES)
					{
						Thread.Sleep(200);
					}
				}
			}
		}

		// Token: 0x06000811 RID: 2065 RVA: 0x00055674 File Offset: 0x00053874
		internal override void InitializeSelfTuning()
		{
			if (this.m_cs != null && this.m_cs.m_selfTuning && this.m_cs.m_pooling && !this.m_bSelfTuningDisabled)
			{
				this.m_bSelfTuning = OracleTuner.Instance.Register(this);
			}
		}

		// Token: 0x06000812 RID: 2066 RVA: 0x000556B4 File Offset: 0x000538B4
		public override void MarkAllPRsForDeletion(DateTime haEventUtcDateTime, bool isHAEvnt = false)
		{
			base.MarkAllPRsForDeletion(haEventUtcDateTime, isHAEvnt);
		}

		// Token: 0x06000813 RID: 2067 RVA: 0x000556C0 File Offset: 0x000538C0
		public override void ClearAllPools(OracleConnectionImpl pr, bool isHAEvnt = false)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)2304, new string[0]);
			}
			try
			{
				base.ClearAllPools(pr, isHAEvnt);
				if (this.m_xmlTokenManager != null)
				{
					this.m_xmlTokenManager.PurgeTokenMaps(null, -1);
					this.m_xmlTokenManager.Dispose();
					this.m_xmlTokenManager = null;
				}
				if (this.m_tableColumnsCache != null)
				{
					this.m_tableColumnsCache.Clear();
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

		// Token: 0x06000814 RID: 2068 RVA: 0x00055770 File Offset: 0x00053970
		internal override bool Close(OracleConnectionImpl con, CriteriaCtx criteriaCtx)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)2304, new string[0]);
			}
			bool result;
			try
			{
				result = base.Close(con, criteriaCtx);
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

		// Token: 0x06000815 RID: 2069 RVA: 0x000557E8 File Offset: 0x000539E8
		public override OracleConnectionImpl GetUsingDiffPassword(ConnectionString csWithDiffPassword, CriteriaCtx criteriaCtx)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)2304, new string[0]);
			}
			OracleConnectionImpl result;
			try
			{
				OracleConnectionImpl usingDiffPassword = base.GetUsingDiffPassword(csWithDiffPassword, criteriaCtx);
				result = usingDiffPassword;
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

		// Token: 0x06000816 RID: 2070 RVA: 0x00055860 File Offset: 0x00053A60
		public override OracleConnectionImpl Get(ConnectionString csWithNewPassword, bool bGetForApp, CriteriaCtx criteriaCtx, string affinityInstanceName = null, bool bForceMatch = false)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)2304, new string[0]);
			}
			OracleConnectionImpl oracleConnectionImpl = null;
			OracleConnectionImpl result;
			try
			{
				while (oracleConnectionImpl == null)
				{
					oracleConnectionImpl = base.Get(csWithNewPassword, bGetForApp, criteriaCtx, affinityInstanceName, bForceMatch);
					bool flag = true;
					if (base.ConnectionString.m_validateConnection)
					{
						try
						{
							flag = oracleConnectionImpl.PingServer();
							goto IL_90;
						}
						catch
						{
							flag = false;
							goto IL_90;
						}
						goto IL_4A;
					}
					goto IL_4A;
					IL_90:
					if (!flag)
					{
						lock (oracleConnectionImpl)
						{
							oracleConnectionImpl.m_deletionRequestor = DeletionRequestor.HA;
							this.Close(oracleConnectionImpl, null);
						}
						oracleConnectionImpl = null;
					}
					if (oracleConnectionImpl == null || !this.m_cs.m_bProxyUserIdSet)
					{
						continue;
					}
					if (oracleConnectionImpl.m_sessionType == SessionType.Two_Session_Proxy && !oracleConnectionImpl.m_bEndUserSessionEstablished)
					{
						oracleConnectionImpl.OpenEndUserSession(csWithNewPassword.m_userId, csWithNewPassword.Password, criteriaCtx);
						oracleConnectionImpl.m_cs = csWithNewPassword;
						continue;
					}
					if (!(oracleConnectionImpl.m_cs.m_userId != csWithNewPassword.m_userId) && (criteriaCtx == null || criteriaCtx.m_bNewConCreated || criteriaCtx.CanReturnBestMatchingPR()))
					{
						continue;
					}
					oracleConnectionImpl.m_statementCache.Purge(0);
					if (oracleConnectionImpl.m_sessionType == SessionType.Two_Session_Proxy)
					{
						oracleConnectionImpl.CloseEndUserSession();
						oracleConnectionImpl.OpenEndUserSession(csWithNewPassword.m_userId, csWithNewPassword.Password, criteriaCtx);
						oracleConnectionImpl.m_cs = csWithNewPassword;
						continue;
					}
					oracleConnectionImpl.DisConnect(null);
					oracleConnectionImpl.Connect(csWithNewPassword, true, criteriaCtx, affinityInstanceName);
					this.ProcessCriteriaCtxAndAlterSessionIfReqd(criteriaCtx, oracleConnectionImpl);
					continue;
					IL_4A:
					if (flag && (oracleConnectionImpl.m_deletionRequestor == DeletionRequestor.HA || (oracleConnectionImpl.m_cp != null && oracleConnectionImpl.m_cp.m_bInstanceDown)))
					{
						flag = false;
					}
					if (flag && oracleConnectionImpl.m_cs.m_pooling)
					{
						try
						{
							flag = oracleConnectionImpl.m_oracleCommunication.TransportAlive;
						}
						catch
						{
							flag = false;
						}
						goto IL_90;
					}
					goto IL_90;
				}
				if (oracleConnectionImpl != null && oracleConnectionImpl.m_pm != null && oracleConnectionImpl.m_pm.m_bSelfTuning)
				{
					if (this.m_recommendedSCS == 0 && oracleConnectionImpl.m_statementCache != null)
					{
						oracleConnectionImpl.PurgeStatementCache(0);
						oracleConnectionImpl.m_statementCache.m_maxCacheSize = this.m_recommendedSCS;
						this.m_dictStatementmetadatacache = null;
						this.m_dictDeriveParamInfoPool = null;
						this.m_sqlParseInfoPool = null;
					}
					else if (oracleConnectionImpl.m_statementCache != null && (oracleConnectionImpl.m_statementCache.m_maxCacheSize < this.m_recommendedSCS || (float)oracleConnectionImpl.m_statementCache.m_maxCacheSize * 0.95f >= (float)this.m_recommendedSCS))
					{
						if (oracleConnectionImpl.m_statementCache.m_maxCacheSize > this.m_recommendedSCS)
						{
							oracleConnectionImpl.PurgeStatementCache(this.m_recommendedSCS);
						}
						oracleConnectionImpl.m_statementCache.m_maxCacheSize = this.m_recommendedSCS;
					}
				}
				result = oracleConnectionImpl;
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
						Trace.GetCPInfo(oracleConnectionImpl, null, null, null, false, false)
					});
				}
			}
			return result;
		}

		// Token: 0x06000817 RID: 2071 RVA: 0x00055B78 File Offset: 0x00053D78
		public override void Put(OracleConnectionImpl con, CriteriaCtx criteriaCtx)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)2304, new string[0]);
			}
			try
			{
				if (!this.m_cs.m_pooling && SessionType.Two_Session_Proxy == con.SessionType)
				{
					con.CloseEndUserSession();
				}
				if (this.m_cs.m_stmtCachePurge && con.m_statementCache != null)
				{
					con.PurgeStatementCache(0);
				}
				con.m_oracleCommunication.OraBufPool.Init(con.m_oracleCommunication);
				base.Put(con, criteriaCtx);
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

		// Token: 0x06000818 RID: 2072 RVA: 0x00055C3C File Offset: 0x00053E3C
		public override bool RemoveCheckedInPR(OracleConnectionImpl pr, bool bForce)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)2304, new string[0]);
			}
			bool result;
			try
			{
				bool flag = base.RemoveCheckedInPR(pr, bForce);
				if (flag)
				{
					if (OraclePool.m_bPerfNumberOfFreeConnections)
					{
						OraclePool.PerformanceCounterDecrement(OraclePerfParams.CounterIndex.NumberOfFreeConnections, pr, pr.m_cp);
					}
					if (OraclePool.m_bPerfNumberOfPooledConnections)
					{
						OraclePool.PerformanceCounterDecrement(OraclePerfParams.CounterIndex.NumberOfPooledConnections, pr, pr.m_cp);
					}
					if (pr.m_cp.m_cpListPR.Count == 0)
					{
						if (OraclePool.m_bPerfNumberOfActiveConnectionPools)
						{
							OraclePool.PerformanceCounterDecrement(OraclePerfParams.CounterIndex.NumberOfActiveConnectionPools, pr, pr.m_cp);
						}
						if (OraclePool.m_bPerfNumberOfInactiveConnectionPools)
						{
							OraclePool.PerformanceCounterIncrement(OraclePerfParams.CounterIndex.NumberOfInactiveConnectionPools, pr, pr.m_cp);
						}
						pr.m_cp.m_bIsPoolActive = false;
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
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)2560, new string[0]);
				}
			}
			return result;
		}

		// Token: 0x06000819 RID: 2073 RVA: 0x00055D28 File Offset: 0x00053F28
		public override bool RemoveCheckedOutPR(OracleConnectionImpl pr, bool bForce)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)2304, new string[0]);
			}
			bool result;
			try
			{
				bool flag = base.RemoveCheckedOutPR(pr, bForce);
				if (flag)
				{
					OraclePool cp = pr.m_cp;
					if (cp != null)
					{
						if (OraclePool.m_bPerfNumberOfPooledConnections)
						{
							OraclePool.PerformanceCounterDecrement(OraclePerfParams.CounterIndex.NumberOfPooledConnections, pr, pr.m_cp);
						}
						if (pr.m_cp.m_cpListPR.Count == 0)
						{
							if (OraclePool.m_bPerfNumberOfActiveConnectionPools)
							{
								OraclePool.PerformanceCounterDecrement(OraclePerfParams.CounterIndex.NumberOfActiveConnectionPools, pr, pr.m_cp);
							}
							if (OraclePool.m_bPerfNumberOfInactiveConnectionPools)
							{
								OraclePool.PerformanceCounterIncrement(OraclePerfParams.CounterIndex.NumberOfInactiveConnectionPools, pr, pr.m_cp);
							}
							pr.m_cp.m_bIsPoolActive = false;
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
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)2560, new string[0]);
				}
			}
			return result;
		}

		// Token: 0x0600081A RID: 2074 RVA: 0x00055E08 File Offset: 0x00054008
		internal bool TryRetrieveLocalParseInfoFromCache(string serviceName, string cmdText, ref SQLMetaData sqlMetaData)
		{
			if (string.IsNullOrWhiteSpace(cmdText) || sqlMetaData == null || this.m_dictStatementmetadatacache == null)
			{
				return false;
			}
			if (sqlMetaData.m_sqlMetaInfo != null && sqlMetaData.m_sqlMetaInfo.bIsPooled)
			{
				return sqlMetaData.m_sqlMetaInfo.bStmtParsed;
			}
			if (!this.m_dictStatementmetadatacache.ContainsKey(serviceName))
			{
				return false;
			}
			SQLLocalParsePrimaryKeyInfo sqllocalParsePrimaryKeyInfo = this.m_dictStatementmetadatacache[serviceName].Get(cmdText);
			if (sqllocalParsePrimaryKeyInfo == null)
			{
				return false;
			}
			sqlMetaData.m_sqlMetaInfo = sqllocalParsePrimaryKeyInfo;
			sqlMetaData.bStmtParsed = sqllocalParsePrimaryKeyInfo.bStmtParsed;
			sqlMetaData.bPkFetched = sqllocalParsePrimaryKeyInfo.bPkFetched;
			return sqlMetaData.bStmtParsed;
		}

		// Token: 0x0600081B RID: 2075 RVA: 0x00055EA0 File Offset: 0x000540A0
		internal bool TryRetrievePrimaryKeyInfoFromCache(string serviceName, string cmdText, ref SQLMetaData sqlMetaData)
		{
			if (string.IsNullOrWhiteSpace(cmdText) || sqlMetaData == null || this.m_dictStatementmetadatacache == null)
			{
				return false;
			}
			if (sqlMetaData.m_sqlMetaInfo != null && sqlMetaData.m_sqlMetaInfo.bIsPooled)
			{
				return sqlMetaData.m_sqlMetaInfo.bPkFetched;
			}
			if (!this.m_dictStatementmetadatacache.ContainsKey(serviceName))
			{
				return false;
			}
			SQLLocalParsePrimaryKeyInfo sqllocalParsePrimaryKeyInfo = this.m_dictStatementmetadatacache[serviceName].Get(cmdText);
			if (sqllocalParsePrimaryKeyInfo == null)
			{
				return false;
			}
			sqlMetaData.m_sqlMetaInfo = sqllocalParsePrimaryKeyInfo;
			sqlMetaData.bPkFetched = sqllocalParsePrimaryKeyInfo.bPkFetched;
			sqlMetaData.bStmtParsed = sqllocalParsePrimaryKeyInfo.bStmtParsed;
			return sqlMetaData.bPkFetched;
		}

		// Token: 0x0600081C RID: 2076 RVA: 0x00055F38 File Offset: 0x00054138
		internal void TryCacheLocalParsePrimaryKeyInfo(string serviceName, string cmdText, SQLMetaData sqlMetaData)
		{
			if (string.IsNullOrWhiteSpace(cmdText) || sqlMetaData == null || this.m_dictStatementmetadatacache == null)
			{
				return;
			}
			if (sqlMetaData.m_sqlMetaInfo == SQLLocalParsePrimaryKeyInfo.Null || sqlMetaData.m_sqlMetaInfo == null)
			{
				return;
			}
			if (!sqlMetaData.m_sqlMetaInfo.bIsPooled)
			{
				if (!this.m_dictStatementmetadatacache.ContainsKey(serviceName))
				{
					lock (this.m_dictStatementmetadatacacheLock)
					{
						if (!this.m_dictStatementmetadatacache.ContainsKey(serviceName))
						{
							this.m_dictStatementmetadatacache[serviceName] = new SQLLocalParsePrimaryKeyInfoPool(1000);
						}
					}
				}
				this.m_dictStatementmetadatacache[serviceName].Put(cmdText, sqlMetaData.m_sqlMetaInfo);
			}
		}

		// Token: 0x0600081D RID: 2077 RVA: 0x00055FF4 File Offset: 0x000541F4
		internal bool TryGetSqlWithRowId(ref string cmdText, out bool hadRowId, out bool addedRowId)
		{
			hadRowId = (addedRowId = false);
			if (this.m_sqlParseInfoPool == null)
			{
				return false;
			}
			string text = this.m_sqlParseInfoPool.Get(cmdText, out hadRowId);
			if (text != null)
			{
				addedRowId = !text.Equals(cmdText);
				cmdText = text;
				return true;
			}
			return false;
		}

		// Token: 0x0600081E RID: 2078 RVA: 0x00056038 File Offset: 0x00054238
		internal void CacheSqlWithRowIdInfo(string cmdText, string cmdTextWithRowId, bool hasRowId)
		{
			if (this.m_sqlParseInfoPool != null)
			{
				this.m_sqlParseInfoPool.Put(cmdText, cmdTextWithRowId, hasRowId);
			}
		}

		// Token: 0x0600081F RID: 2079 RVA: 0x00056050 File Offset: 0x00054250
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
					this.Dispose();
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

		// Token: 0x06000820 RID: 2080 RVA: 0x000560DC File Offset: 0x000542DC
		internal void Dispose()
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)2304, new string[0]);
			}
			try
			{
				if (!this.m_isDisposed)
				{
					if (this.m_bSelfTuning)
					{
						try
						{
							OracleTuner.Instance.Unregister(this);
						}
						catch
						{
						}
					}
					this.m_isDisposed = true;
					GC.SuppressFinalize(this);
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

		// Token: 0x170001E2 RID: 482
		// (get) Token: 0x06000821 RID: 2081 RVA: 0x00056188 File Offset: 0x00054388
		// (set) Token: 0x06000822 RID: 2082 RVA: 0x00056190 File Offset: 0x00054390
		public int MaxAllowedValue
		{
			get
			{
				return this.m_maxAllowedCursors;
			}
			set
			{
				if (this.m_maxAllowedCursors == value)
				{
					return;
				}
				this.m_maxAllowedCursors = value;
				if (this.m_bSelfTuning)
				{
					OracleTuner.Instance.setThreshold(this, value);
				}
			}
		}

		// Token: 0x170001E3 RID: 483
		// (get) Token: 0x06000823 RID: 2083 RVA: 0x000561B8 File Offset: 0x000543B8
		public string ID
		{
			get
			{
				if (this.m_cs != null)
				{
					return this.m_cs.m_pmId;
				}
				return string.Empty;
			}
		}

		// Token: 0x06000824 RID: 2084 RVA: 0x000561D4 File Offset: 0x000543D4
		public void OnUpdateRecommendations(RecommendationType recommendationType, int value)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)65792, new string[0]);
			}
			try
			{
				switch (recommendationType)
				{
				case RecommendationType.SCS:
					if (value >= 0)
					{
						this.m_recommendedSCS = ((value <= ProviderConfig.MaxStatementCacheSize.Value) ? value : ProviderConfig.MaxStatementCacheSize.Value);
						if (this.m_recommendedSCS > this.m_maxAllowedCursors)
						{
							this.m_recommendedSCS = this.m_maxAllowedCursors;
						}
					}
					break;
				case RecommendationType.Unregister:
					this.m_bSelfTuningDisabled = true;
					OracleTuner.Instance.Unregister(this);
					this.m_recommendedSCS = value;
					break;
				}
			}
			catch (Exception ex)
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)268500992, new string[]
					{
						ex.Message
					});
				}
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)66048, new string[0]);
				}
			}
		}

		// Token: 0x04000B08 RID: 2824
		private const int DEFAULT_MAX_ELEMS_IN_METADATAPOOL_TUNING_OFF = 200;

		// Token: 0x04000B09 RID: 2825
		private const int DEFAULT_MAX_ELEMS_IN_METADATAPOOL_TUNING_ON = 50;

		// Token: 0x04000B0A RID: 2826
		private const int DEFAULT_MAX_ELEMS_IN_SQLPARSEINFOPOOL_TUNING_OFF = 200;

		// Token: 0x04000B0B RID: 2827
		private const int DEFAULT_MAX_ELEMS_IN_SQLPARSEINFOPOOL_TUNING_ON = 50;

		// Token: 0x04000B0C RID: 2828
		private const int DEFAULT_MAX_ELEMS_IN_METADATAPOOL = 1000;

		// Token: 0x04000B0D RID: 2829
		private const int DEFAULT_MAX_ELEMS_IN_SQLPARSEINFOPOOL = 500;

		// Token: 0x04000B0E RID: 2830
		internal const int DEFAULT_MAX_ELEMS_IN_DERIVEPARAMINFOPOOL = 50;

		// Token: 0x04000B0F RID: 2831
		internal const int DEFAULT_MAX_ELEMS_IN_XMLSCHEMAPOOL = 200;

		// Token: 0x04000B10 RID: 2832
		private const int DEFAULT_MAX_ELEMS_IN_XMLTOKENPOOL = 4096;

		// Token: 0x04000B11 RID: 2833
		private static int s_maxBucketCapacity = 4;

		// Token: 0x04000B12 RID: 2834
		private static int s_maxListCapacity = 10;

		// Token: 0x04000B13 RID: 2835
		public OraBufPool m_oraBufPool;

		// Token: 0x04000B14 RID: 2836
		internal SyncDictionary<string, DeriveParamInfoPool> m_dictDeriveParamInfoPool;

		// Token: 0x04000B15 RID: 2837
		internal object m_dictDeriveParamInfoPoolLock = new object();

		// Token: 0x04000B16 RID: 2838
		internal SyncDictionary<string, XmlSchemaPool> m_dictXmlSchemaPool;

		// Token: 0x04000B17 RID: 2839
		internal object m_dictXmlSchemaPoolLock = new object();

		// Token: 0x04000B18 RID: 2840
		internal object m_orclGlobLock = new object();

		// Token: 0x04000B19 RID: 2841
		internal int m_appThreadLCID = 1033;

		// Token: 0x04000B1A RID: 2842
		internal static int NUM_CRED_RETRIEVAL_TRIES = 3;

		// Token: 0x04000B1B RID: 2843
		internal long m_logonMode;

		// Token: 0x04000B1C RID: 2844
		internal bool m_bDoNAHandShake;

		// Token: 0x04000B1D RID: 2845
		internal bool m_bDoExternalAuth;

		// Token: 0x04000B1E RID: 2846
		internal bool m_maxOpenCursorsFetched;

		// Token: 0x04000B1F RID: 2847
		internal ObxmlTokenManager m_xmlTokenManager;

		// Token: 0x04000B20 RID: 2848
		internal TableColumnsCache m_tableColumnsCache;

		// Token: 0x04000B21 RID: 2849
		private SyncDictionary<string, SQLLocalParsePrimaryKeyInfoPool> m_dictStatementmetadatacache;

		// Token: 0x04000B22 RID: 2850
		private object m_dictStatementmetadatacacheLock = new object();

		// Token: 0x04000B23 RID: 2851
		private SQLParseInfoPool m_sqlParseInfoPool;

		// Token: 0x04000B24 RID: 2852
		private bool m_isDisposed;

		// Token: 0x04000B25 RID: 2853
		internal bool m_bSelfTuning;

		// Token: 0x04000B26 RID: 2854
		private object m_tuningLock = new object();

		// Token: 0x04000B27 RID: 2855
		internal int m_recommendedSCS;

		// Token: 0x04000B28 RID: 2856
		private int m_maxAllowedCursors = int.MaxValue;
	}
}
