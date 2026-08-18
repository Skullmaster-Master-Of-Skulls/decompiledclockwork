using System;
using System.Transactions;
using OracleInternal.ConnectionPool;
using OracleInternal.ServiceObjects;

namespace OracleInternal.MTS
{
	// Token: 0x02000116 RID: 278
	internal class CCPDTCPSPEManager : DTCPSPEManager
	{
		// Token: 0x06000C09 RID: 3081 RVA: 0x00086ACC File Offset: 0x00084CCC
		internal CCPDTCPSPEManager(OracleConnectionImpl connImpl, Transaction txn, MTSTxnRM txnRM, MTSTxnBranch txnBranch) : base(connImpl, txn, txnRM, txnBranch)
		{
		}

		// Token: 0x06000C0A RID: 3082 RVA: 0x00086ADC File Offset: 0x00084CDC
		internal override void InitialPSPEConn(Transaction txn, OracleConnectionImpl connImpl)
		{
			connImpl.m_pm.InitializePSPEConn(txn, connImpl);
		}

		// Token: 0x06000C0B RID: 3083 RVA: 0x00086AEC File Offset: 0x00084CEC
		internal override void ResetForPromotedTxn(OracleConnectionImpl connImpl, Transaction txn, string txnLocalId)
		{
			OracleConnectionDispenser<OraclePoolManager, OraclePool, OracleConnectionImpl>.PutFromPSPE(txn, connImpl);
		}
	}
}
