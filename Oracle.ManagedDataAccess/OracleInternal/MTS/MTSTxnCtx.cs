using System;
using OracleInternal.Common;
using OracleInternal.ServiceObjects;

namespace OracleInternal.MTS
{
	// Token: 0x0200011B RID: 283
	internal class MTSTxnCtx
	{
		// Token: 0x06000C2C RID: 3116 RVA: 0x00088230 File Offset: 0x00086430
		internal static MTSTxnCtx CreateMTSTxnCtx(OracleConnectionImpl connImpl)
		{
			return new CCPMTSTxnCtx();
		}

		// Token: 0x06000C2D RID: 3117 RVA: 0x00088238 File Offset: 0x00086438
		internal MTSTxnCtx()
		{
			GC.SuppressFinalize(this);
		}

		// Token: 0x06000C2E RID: 3118 RVA: 0x00088254 File Offset: 0x00086454
		internal virtual void SetCtx(OracleConnectionImpl connImpl)
		{
		}

		// Token: 0x06000C2F RID: 3119 RVA: 0x00088258 File Offset: 0x00086458
		internal virtual void SetTxnState(bool bState)
		{
		}

		// Token: 0x06000C30 RID: 3120 RVA: 0x0008825C File Offset: 0x0008645C
		protected void Reset()
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, OracleTraceTag.MTS, new string[]
				{
					string.Concat(new object[]
					{
						"MTSTxnCtx instance = ",
						this.GetHashCode(),
						" Local Txn = ",
						this.m_txnLocalID,
						"\t",
						(this.m_txnType == MTSTxnType.Distributed) ? string.Concat(new object[]
						{
							"Txn ID = ",
							this.m_mtsTxnBranch.TxnID,
							" using conn ID = ",
							this.m_connImplId
						}) : string.Empty
					})
				});
			}
			this.m_txnType = MTSTxnType.None;
			this.m_connImplId = 0L;
			this.m_localTxn = null;
			this.SetTxnState(false);
		}

		// Token: 0x06000C31 RID: 3121 RVA: 0x00088330 File Offset: 0x00086530
		internal virtual void Reset(OracleConnectionImpl connImpl)
		{
			this.Reset();
		}

		// Token: 0x06000C32 RID: 3122 RVA: 0x00088338 File Offset: 0x00086538
		internal void SetLocalCtx(string txnLocalID, OracleTransactionImpl localTxn, long connImplId)
		{
			this.m_txnType = MTSTxnType.Local;
			this.m_txnLocalID = txnLocalID;
			this.m_localTxn = localTxn;
			this.m_connImplId = connImplId;
			this.SetTxnState(true);
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, OracleTraceTag.MTS, new string[]
				{
					string.Format("(Local Txn ID ={0}) (sessid={1}) (mtstxnctx={2})", txnLocalID, connImplId, this.GetHashCode())
				});
			}
		}

		// Token: 0x06000C33 RID: 3123 RVA: 0x000883A4 File Offset: 0x000865A4
		internal void SetDistributedCtx(string txnLocalID, MTSTxnBranch mtsTxnBranch, long connImplId)
		{
			if (ProviderConfig.m_bTraceLevelPrivate_NoTrace)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)4352, new string[]
				{
					string.Format("BEFORE (Old Txn Local ID ={0}) (sessid={1}) (mtstxnctx={2}) (Txn ID ={3})", new object[]
					{
						this.m_txnLocalID,
						this.m_connImplId,
						this.GetHashCode(),
						mtsTxnBranch.TxnID
					})
				});
			}
			this.m_txnType = MTSTxnType.Distributed;
			this.m_txnLocalID = txnLocalID;
			this.m_mtsTxnBranch = mtsTxnBranch;
			this.m_connImplId = connImplId;
			this.m_localTxn = null;
			this.SetTxnState(true);
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, OracleTraceTag.MTS, new string[]
				{
					string.Format("(New Txn Local ID ={0}) (sessid={1}) (mtstxnctx={2}) (Txn ID ={3})", new object[]
					{
						txnLocalID,
						connImplId,
						this.GetHashCode(),
						mtsTxnBranch.TxnID
					})
				});
			}
		}

		// Token: 0x06000C34 RID: 3124 RVA: 0x0008848C File Offset: 0x0008668C
		internal void DelistTransaction(OracleConnectionImpl connImpl)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)4352, new string[]
				{
					string.Concat(new object[]
					{
						"MTSTxnCtx instance = ",
						this.GetHashCode(),
						"\tTxn ID = ",
						(this.m_mtsTxnBranch != null) ? this.m_mtsTxnBranch.TxnID.ToString() : "Null Txn",
						" using conn ID = ",
						connImpl.m_endUserSessionId,
						" to DBInst = ",
						connImpl.m_instanceName
					})
				});
			}
			try
			{
				if (this.m_mtsTxnBranch != null && this.m_txnType == MTSTxnType.Distributed)
				{
					this.m_mtsTxnBranch.Detach(connImpl);
				}
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)4608, new string[]
					{
						string.Concat(new object[]
						{
							"MTSTxnCtx instance = ",
							this.GetHashCode(),
							"\tTxn ID = ",
							(this.m_mtsTxnBranch != null) ? this.m_mtsTxnBranch.TxnID.ToString() : "Null Txn",
							" using conn ID = ",
							connImpl.m_endUserSessionId,
							" to DBInst = ",
							connImpl.m_instanceName
						})
					});
				}
			}
		}

		// Token: 0x04000D40 RID: 3392
		internal MTSTxnType m_txnType;

		// Token: 0x04000D41 RID: 3393
		internal string m_txnLocalID = string.Empty;

		// Token: 0x04000D42 RID: 3394
		internal OracleTransactionImpl m_localTxn;

		// Token: 0x04000D43 RID: 3395
		internal MTSTxnBranch m_mtsTxnBranch;

		// Token: 0x04000D44 RID: 3396
		private long m_connImplId;
	}
}
