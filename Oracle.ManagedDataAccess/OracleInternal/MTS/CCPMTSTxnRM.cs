using System;
using System.Transactions;
using Oracle.ManagedDataAccess.Client;
using OracleInternal.Common;
using OracleInternal.ConnectionPool;
using OracleInternal.ServiceObjects;

namespace OracleInternal.MTS
{
	// Token: 0x02000121 RID: 289
	internal class CCPMTSTxnRM : MTSTxnRM
	{
		// Token: 0x06000C5C RID: 3164 RVA: 0x0008A8BC File Offset: 0x00088ABC
		internal CCPMTSTxnRM() : base(true)
		{
		}

		// Token: 0x06000C5D RID: 3165 RVA: 0x0008A8C8 File Offset: 0x00088AC8
		~CCPMTSTxnRM()
		{
		}

		// Token: 0x06000C5E RID: 3166 RVA: 0x0008A8F0 File Offset: 0x00088AF0
		internal override void ReleaseRPs(SyncQueueList<ConnectionString> csList, Transaction txn)
		{
			ConnectionString cs;
			while ((cs = csList.Dequeue()) != null)
			{
				try
				{
					OracleConnectionDispenser<OraclePoolManager, OraclePool, OracleConnectionImpl>.RemoveRM(cs, this.m_serviceName, txn);
				}
				catch
				{
				}
			}
		}

		// Token: 0x06000C5F RID: 3167 RVA: 0x0008A930 File Offset: 0x00088B30
		internal override void ReleaseRP(ConnectionString cs, Transaction txn)
		{
			if (cs != null)
			{
				try
				{
					OracleConnectionDispenser<OraclePoolManager, OraclePool, OracleConnectionImpl>.RemoveRM(cs, this.m_serviceName, this.m_sysTxn);
				}
				catch
				{
				}
			}
		}

		// Token: 0x06000C60 RID: 3168 RVA: 0x0008A968 File Offset: 0x00088B68
		internal override void MTSTransactionCompleted(object sender, TransactionEventArgs e)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)4352, new string[]
				{
					this.ToString()
				});
			}
			try
			{
				e.Transaction.TransactionCompleted -= this.MTSTransactionCompleted;
				if (string.Compare(this.m_txnLocalID, e.Transaction.TransactionInformation.LocalIdentifier, true) == 0)
				{
					if (this.m_enlistedState == EnlistedState.Distributed)
					{
						try
						{
							base.DetachBranches();
						}
						catch
						{
						}
					}
					if (ProviderConfig.m_bTraceLevelPrivate)
					{
						Trace.Write(OracleTraceLevel.Private, OracleTraceTag.MTS, new string[]
						{
							string.Concat(new object[]
							{
								"MTSTxnRM.MTSTransactionCompleted: Currently in ",
								this.m_state,
								" state. TxnID = ",
								this.ToString()
							})
						});
					}
				}
			}
			catch (Exception ex)
			{
				try
				{
					OracleException.HandleError(OracleTraceLevel.Private, OracleTraceTag.MTS, ex, null);
				}
				catch
				{
				}
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)4608, new string[]
					{
						this.ToString()
					});
				}
			}
		}

		// Token: 0x06000C61 RID: 3169 RVA: 0x0008AAAC File Offset: 0x00088CAC
		internal override void UnRegisteringTxnEvent(Transaction txn)
		{
			try
			{
				txn.TransactionCompleted -= this.MTSTransactionCompleted;
			}
			catch
			{
			}
		}
	}
}
