using System;
using System.Transactions;
using OracleInternal.ConnectionPool;
using OracleInternal.ServiceObjects;

namespace OracleInternal.MTS
{
	// Token: 0x02000118 RID: 280
	internal class CCPFWPSPEManager : FWPSPEManager
	{
		// Token: 0x06000C12 RID: 3090 RVA: 0x00086DB8 File Offset: 0x00084FB8
		internal CCPFWPSPEManager(OracleConnectionImpl connImpl, Transaction txn, MTSTxnRM txnRM, MTSTxnBranch txnBranch) : base(connImpl, txn, txnRM, txnBranch)
		{
		}

		// Token: 0x06000C13 RID: 3091 RVA: 0x00086DC8 File Offset: 0x00084FC8
		internal override void InitialPSPEConn(Transaction txn, OracleConnectionImpl connImpl)
		{
			connImpl.m_pm.InitializePSPEConn(txn, connImpl);
		}

		// Token: 0x06000C14 RID: 3092 RVA: 0x00086DD8 File Offset: 0x00084FD8
		internal override void ResetForPromotedTxn(OracleConnectionImpl connImpl, Transaction txn, string txnLocalId)
		{
			OracleConnectionDispenser<OraclePoolManager, OraclePool, OracleConnectionImpl>.PutFromPSPE(txn, connImpl);
		}
	}
}
