using System;
using System.Collections;
using System.Runtime.InteropServices;
using System.Security;
using System.Transactions;

namespace Oracle.DataAccess.Client
{
	// Token: 0x02000032 RID: 50
	[SuppressUnmanagedCodeSecurity]
	[StructLayout(LayoutKind.Sequential)]
	internal class OpoConCtx : ICloneable
	{
		// Token: 0x0600021A RID: 538 RVA: 0x0001BF2D File Offset: 0x0001AF2D
		internal OpoConCtx()
		{
		}

		// Token: 0x0600021B RID: 539 RVA: 0x0001BF44 File Offset: 0x0001AF44
		public unsafe object Clone()
		{
			int num = 0;
			OpoConCtx opoConCtx = new OpoConCtx();
			try
			{
				num = OpsCon.AllocValCtx(ref opoConCtx.pOpoConValCtx);
			}
			catch (Exception ex)
			{
				if (OraTrace.m_TraceLevel != 0U)
				{
					OraTrace.TraceExceptionInfo(ex);
				}
				throw;
			}
			if (num != 0)
			{
				OracleException.HandleError(num, null, IntPtr.Zero, null);
			}
			opoConCtx.opoConRefCtx = new OpoConRefCtx();
			opoConCtx.opoConRefCtx.dataSource = this.opoConRefCtx.dataSource;
			opoConCtx.opoConRefCtx.newPassword = string.Empty;
			opoConCtx.opoConRefCtx.password = this.opoConRefCtx.password;
			opoConCtx.opoConRefCtx.pITransaction = null;
			opoConCtx.opoConRefCtx.proxyPassword = this.opoConRefCtx.proxyPassword;
			opoConCtx.opoConRefCtx.proxyUserId = this.opoConRefCtx.proxyUserId;
			opoConCtx.opoConRefCtx.serverVersion = this.opoConRefCtx.serverVersion;
			opoConCtx.opoConRefCtx.userID = this.opoConRefCtx.userID;
			opoConCtx.opoConRefCtx.dbName = this.opoConRefCtx.dbName;
			opoConCtx.opoConRefCtx.hostName = this.opoConRefCtx.hostName;
			opoConCtx.opoConRefCtx.instanceName = this.opoConRefCtx.instanceName;
			opoConCtx.opoConRefCtx.serviceName = this.opoConRefCtx.serviceName;
			opoConCtx.opoConRefCtx.dbDomainName = this.opoConRefCtx.dbDomainName;
			opoConCtx.opoConRefCtx.ttOpsConOpenErrMssg = this.opoConRefCtx.ttOpsConOpenErrMssg;
			opoConCtx.opoConRefCtx.appEdition = this.opoConRefCtx.appEdition;
			opoConCtx.conString = this.conString;
			opoConCtx.pool = this.pool;
			opoConCtx.maxPoolSize = this.maxPoolSize;
			opoConCtx.minPoolSize = this.origMinPoolSize;
			opoConCtx.lifeTime = this.origLifeTime;
			opoConCtx.creationTime = this.creationTime;
			opoConCtx.poolDecSize = this.origPoolDecSize;
			opoConCtx.poolIncSize = this.poolIncSize;
			opoConCtx.poolRegulator = this.poolRegulator;
			opoConCtx.validateCon = this.validateCon;
			opoConCtx.poolDecSize = this.origPoolDecSize;
			opoConCtx.gridCR = this.gridCR;
			opoConCtx.gridRLB = this.gridRLB;
			opoConCtx.bGridRac = this.bGridRac;
			opoConCtx.origMinPoolSize = this.origMinPoolSize;
			opoConCtx.origLifeTime = this.origLifeTime;
			opoConCtx.origPoolDecSize = this.origPoolDecSize;
			opoConCtx.dataSrc = this.dataSrc;
			opoConCtx.metaPool = this.metaPool;
			opoConCtx.timeOut = this.timeOut;
			opoConCtx.m_bSelfTuning = this.m_bSelfTuning;
			opoConCtx.m_defaultStmtCacheSize = this.m_defaultStmtCacheSize;
			opoConCtx.pOpoConValCtx->Enlist = this.pOpoConValCtx->Enlist;
			opoConCtx.pOpoConValCtx->InMtsTxn = 0;
			opoConCtx.pOpoConValCtx->OSAuthent = this.pOpoConValCtx->OSAuthent;
			opoConCtx.pOpoConValCtx->Pooling = this.pOpoConValCtx->Pooling;
			opoConCtx.pOpoConValCtx->ServerAttach = 0;
			opoConCtx.pOpoConValCtx->SessionBegin = 0;
			opoConCtx.pOpoConValCtx->TxnHndAllocated = 0;
			opoConCtx.pOpoConValCtx->SetIntAndExtName = this.pOpoConValCtx->SetIntAndExtName;
			opoConCtx.pOpoConValCtx->DBAPrivilege = this.pOpoConValCtx->DBAPrivilege;
			opoConCtx.pOpoConValCtx->registerHA = this.pOpoConValCtx->registerHA;
			opoConCtx.pOpoConValCtx->registerRLB = this.pOpoConValCtx->registerRLB;
			opoConCtx.pOpoConValCtx->HASubscrHnd = IntPtr.Zero;
			opoConCtx.pOpoConValCtx->reRegHAFailed = 0;
			opoConCtx.pOpoConValCtx->RLBSubscrHnd = IntPtr.Zero;
			opoConCtx.pOpoConValCtx->reRegRLBFailed = 0;
			opoConCtx.pOpoConValCtx->PSPE = this.pOpoConValCtx->PSPE;
			opoConCtx.pOpoConValCtx->bTAFEnabled = 0;
			opoConCtx.pOpoConValCtx->StmtCachePurge = this.pOpoConValCtx->StmtCachePurge;
			opoConCtx.pOpoConValCtx->StmtCacheSize = this.pOpoConValCtx->StmtCacheSize;
			opoConCtx.pOpoConValCtx->MajorVersion = this.pOpoConValCtx->MajorVersion;
			opoConCtx.pOpoConValCtx->MinorVersion = this.pOpoConValCtx->MinorVersion;
			opoConCtx.pOpoConValCtx->PatchSetVersion = this.pOpoConValCtx->PatchSetVersion;
			opoConCtx.pOpoConValCtx->DbNtfPort = this.pOpoConValCtx->DbNtfPort;
			opoConCtx.pOpoConValCtx->bIsTimesTen = this.pOpoConValCtx->bIsTimesTen;
			opoConCtx.m_conPooler = null;
			opoConCtx.m_udtDescPoolerByName = null;
			opoConCtx.m_udtDescPoolerByTDO = null;
			opoConCtx.m_systemTransaction = null;
			opoConCtx.m_txnType = TxnType.None;
			return opoConCtx;
		}

		// Token: 0x0600021C RID: 540 RVA: 0x0001C3C8 File Offset: 0x0001B3C8
		public int AuthenticateUser()
		{
			int result;
			lock (this.pool.m_passwordSyncObj)
			{
				bool flag2;
				if (this.pool.m_encryptedPwd != null)
				{
					if (this.opoConRefCtx.password == this.pool.m_encryptedPwd.Password)
					{
						flag2 = true;
					}
					else
					{
						if (this.opoConRefCtx.password == "")
						{
							return 1005;
						}
						flag2 = false;
					}
				}
				else
				{
					flag2 = (this.opoConRefCtx.password == "");
				}
				bool flag3;
				if (this.pool.m_encryptedPxyPwd != null)
				{
					flag3 = (this.opoConRefCtx.proxyPassword == this.pool.m_encryptedPxyPwd.Password);
				}
				else
				{
					flag3 = (this.opoConRefCtx.proxyPassword == "");
				}
				if (flag2 && flag3)
				{
					result = 0;
				}
				else
				{
					result = 1017;
				}
			}
			return result;
		}

		// Token: 0x04000176 RID: 374
		public IntPtr opsConCtx;

		// Token: 0x04000177 RID: 375
		public IntPtr opsErrCtx;

		// Token: 0x04000178 RID: 376
		public unsafe OpoConValCtx* pOpoConValCtx;

		// Token: 0x04000179 RID: 377
		public OpoConRefCtx opoConRefCtx;

		// Token: 0x0400017A RID: 378
		public string conString;

		// Token: 0x0400017B RID: 379
		public string affinityInstanceName;

		// Token: 0x0400017C RID: 380
		public int instanceConCount;

		// Token: 0x0400017D RID: 381
		public ConnectionPool pool;

		// Token: 0x0400017E RID: 382
		public int maxPoolSize;

		// Token: 0x0400017F RID: 383
		public int minPoolSize;

		// Token: 0x04000180 RID: 384
		public int poolIncSize;

		// Token: 0x04000181 RID: 385
		public int poolDecSize;

		// Token: 0x04000182 RID: 386
		public int poolRegulator;

		// Token: 0x04000183 RID: 387
		public DateTime creationTime;

		// Token: 0x04000184 RID: 388
		public TimeSpan lifeTime;

		// Token: 0x04000185 RID: 389
		public TimeSpan timeOut;

		// Token: 0x04000186 RID: 390
		public PooledConCtx pooledConCtx;

		// Token: 0x04000187 RID: 391
		public string poolName;

		// Token: 0x04000188 RID: 392
		public bool bErrorOnOpen;

		// Token: 0x04000189 RID: 393
		public int validateCon;

		// Token: 0x0400018A RID: 394
		public int gridCR;

		// Token: 0x0400018B RID: 395
		public int gridRLB;

		// Token: 0x0400018C RID: 396
		public bool bGridRac;

		// Token: 0x0400018D RID: 397
		public string dataSrc;

		// Token: 0x0400018E RID: 398
		public int metaPool;

		// Token: 0x0400018F RID: 399
		public TimeSpan origLifeTime;

		// Token: 0x04000190 RID: 400
		public int origPoolDecSize;

		// Token: 0x04000191 RID: 401
		public int origMinPoolSize;

		// Token: 0x04000192 RID: 402
		public string exceptMsg;

		// Token: 0x04000193 RID: 403
		public ConPooler m_conPooler;

		// Token: 0x04000194 RID: 404
		public Transaction m_systemTransaction;

		// Token: 0x04000195 RID: 405
		public TxnType m_txnType;

		// Token: 0x04000196 RID: 406
		public PromotableTxnMgr m_promotableTxnManager;

		// Token: 0x04000197 RID: 407
		internal FetchArrayPooler m_fetchArrayPooler;

		// Token: 0x04000198 RID: 408
		public string m_txnid;

		// Token: 0x04000199 RID: 409
		internal Hashtable m_statementData;

		// Token: 0x0400019A RID: 410
		internal int m_totalDataAvailable;

		// Token: 0x0400019B RID: 411
		internal bool m_bSelfTuning = true;

		// Token: 0x0400019C RID: 412
		internal int m_defaultStmtCacheSize = 30;

		// Token: 0x0400019D RID: 413
		public ConPooler m_udtDescPoolerByName;

		// Token: 0x0400019E RID: 414
		public ConPooler m_udtDescPoolerByTDO;
	}
}
