using System;
using OracleInternal.MTS;
using OracleInternal.TTC;

namespace OracleInternal.ServiceObjects
{
	// Token: 0x020001A5 RID: 421
	internal class MTSTransactionImpl
	{
		// Token: 0x06000FCE RID: 4046 RVA: 0x000A36D8 File Offset: 0x000A18D8
		internal static void Start(OracleConnectionImpl connImpl, OpoDTCTxnCtx opoTxnCtx, uint timeout)
		{
			try
			{
				connImpl.m_connectionFreeToUseEvent.WaitOne();
				TTCTransactionSE ttctransactionSE = connImpl.TTCTransactionSE;
				connImpl.AddAllPiggyBackRequests();
				opoTxnCtx.m_txnCtx = ttctransactionSE.Start(opoTxnCtx.m_opoDTCTxnXID, opoTxnCtx.m_txnCtx, timeout, ref opoTxnCtx.m_applicationValue);
			}
			finally
			{
				connImpl.m_connectionFreeToUseEvent.Set();
			}
		}

		// Token: 0x06000FCF RID: 4047 RVA: 0x000A373C File Offset: 0x000A193C
		internal static void Resume(OracleConnectionImpl connImpl, OpoDTCTxnCtx opoTxnCtx, uint timeout)
		{
			try
			{
				connImpl.m_connectionFreeToUseEvent.WaitOne();
				TTCTransactionSE ttctransactionSE = connImpl.TTCTransactionSE;
				connImpl.AddAllPiggyBackRequests();
				opoTxnCtx.m_txnCtx = ttctransactionSE.Resume(opoTxnCtx.m_opoDTCTxnXID, opoTxnCtx.m_txnCtx, timeout, ref opoTxnCtx.m_applicationValue);
			}
			finally
			{
				connImpl.m_connectionFreeToUseEvent.Set();
			}
		}

		// Token: 0x06000FD0 RID: 4048 RVA: 0x000A37A0 File Offset: 0x000A19A0
		internal static void Promote(OracleConnectionImpl connImpl, OpoDTCTxnCtx opoTxnCtx, uint timeout)
		{
			try
			{
				connImpl.m_connectionFreeToUseEvent.WaitOne();
				TTCTransactionSE ttctransactionSE = connImpl.TTCTransactionSE;
				connImpl.AddAllPiggyBackRequests();
				opoTxnCtx.m_txnCtx = ttctransactionSE.Promote(opoTxnCtx.m_opoDTCTxnXID, opoTxnCtx.m_txnCtx, timeout, ref opoTxnCtx.m_applicationValue);
			}
			finally
			{
				connImpl.m_connectionFreeToUseEvent.Set();
			}
		}

		// Token: 0x06000FD1 RID: 4049 RVA: 0x000A3804 File Offset: 0x000A1A04
		internal static void Detach(OracleConnectionImpl connImpl, OpoDTCTxnCtx opoTxnCtx, uint timeout)
		{
			try
			{
				connImpl.m_connectionFreeToUseEvent.WaitOne();
				TTCTransactionSE ttctransactionSE = connImpl.TTCTransactionSE;
				connImpl.AddAllPiggyBackRequests();
				opoTxnCtx.m_txnCtx = ttctransactionSE.Detach(opoTxnCtx.m_opoDTCTxnXID, opoTxnCtx.m_txnCtx, timeout, ref opoTxnCtx.m_applicationValue);
			}
			finally
			{
				connImpl.m_connectionFreeToUseEvent.Set();
			}
		}

		// Token: 0x06000FD2 RID: 4050 RVA: 0x000A3868 File Offset: 0x000A1A68
		internal static TxnState Prepare(OracleConnectionImpl connImpl, OpoDTCTxnCtx opoTxnCtx, uint timeout, bool bMatchConn)
		{
			TxnState result;
			try
			{
				connImpl.m_connectionFreeToUseEvent.WaitOne();
				TTCTransactionEN ttctransactionEN = connImpl.TTCTransactionEN;
				connImpl.AddAllPiggyBackRequests();
				result = ttctransactionEN.Prepare(opoTxnCtx.m_opoDTCTxnXID, opoTxnCtx.m_txnCtx, timeout, TxnState.K2CMDprepare);
			}
			finally
			{
				connImpl.m_connectionFreeToUseEvent.Set();
			}
			return result;
		}

		// Token: 0x06000FD3 RID: 4051 RVA: 0x000A38C4 File Offset: 0x000A1AC4
		internal static TxnState Commit(OracleConnectionImpl connImpl, OpoDTCTxnCtx opoTxnCtx, uint timeout, bool bMatchConn)
		{
			TxnState result;
			try
			{
				connImpl.m_connectionFreeToUseEvent.WaitOne();
				TTCTransactionEN ttctransactionEN = connImpl.TTCTransactionEN;
				connImpl.AddAllPiggyBackRequests();
				result = ttctransactionEN.Commit(opoTxnCtx.m_opoDTCTxnXID, opoTxnCtx.m_txnCtx, timeout, TxnState.K2CMDcommit);
			}
			finally
			{
				connImpl.m_connectionFreeToUseEvent.Set();
			}
			return result;
		}

		// Token: 0x06000FD4 RID: 4052 RVA: 0x000A3920 File Offset: 0x000A1B20
		internal static TxnState Abort(OracleConnectionImpl connImpl, OpoDTCTxnCtx opoTxnCtx, uint timeout, bool bMatchConn)
		{
			TxnState result;
			try
			{
				connImpl.m_connectionFreeToUseEvent.WaitOne();
				TTCTransactionEN ttctransactionEN = connImpl.TTCTransactionEN;
				connImpl.AddAllPiggyBackRequests();
				result = ttctransactionEN.Abort(opoTxnCtx.m_opoDTCTxnXID, opoTxnCtx.m_txnCtx, timeout, TxnState.K2CMDabort);
			}
			finally
			{
				connImpl.m_connectionFreeToUseEvent.Set();
			}
			return result;
		}
	}
}
