using System;
using System.Reflection;
using System.Transactions;
using Oracle.ManagedDataAccess.Client;
using OracleInternal.Common;
using OracleInternal.ServiceObjects;

namespace OracleInternal.MTS
{
	// Token: 0x02000117 RID: 279
	internal abstract class FWPSPEManager : PSPEManager
	{
		// Token: 0x06000C0C RID: 3084 RVA: 0x00086AF8 File Offset: 0x00084CF8
		internal static void InitPromoteAndEnlistMethod(MethodInfo promoteMethod)
		{
			FWPSPEManager.s_promoteMethod = promoteMethod;
		}

		// Token: 0x06000C0D RID: 3085 RVA: 0x00086B00 File Offset: 0x00084D00
		internal FWPSPEManager(OracleConnectionImpl connImpl, Transaction txn, MTSTxnRM txnRM, MTSTxnBranch txnBranch) : base(connImpl, txn, txnRM, txnBranch)
		{
		}

		// Token: 0x06000C0E RID: 3086 RVA: 0x00086B10 File Offset: 0x00084D10
		internal override byte[] InternalPromote(out Guid txnGuid)
		{
			FWPSPEManager.s_promoteMethod.Invoke(this.m_sysTxn, new object[]
			{
				this.m_mtsTxnRM.m_RMGuid,
				this,
				this.m_mtsTxnRM,
				EnlistmentOptions.None
			});
			this.m_mtsTxnRM.m_enlistedState = EnlistedState.Distributed;
			txnGuid = this.m_sysTxn.TransactionInformation.DistributedIdentifier;
			return null;
		}

		// Token: 0x06000C0F RID: 3087 RVA: 0x00086B84 File Offset: 0x00084D84
		internal override bool InternalCommit()
		{
			return true;
		}

		// Token: 0x06000C10 RID: 3088 RVA: 0x00086B88 File Offset: 0x00084D88
		internal override bool InternalRollback()
		{
			return true;
		}

		// Token: 0x06000C11 RID: 3089 RVA: 0x00086B8C File Offset: 0x00084D8C
		internal override void InternalHandlePromoteError()
		{
			if (!this.m_bLocalTxnPromoted)
			{
				try
				{
					try
					{
						if (this.m_connImpl.m_mtsTxnCtx != null && this.m_connImpl.m_mtsTxnCtx.m_txnType == MTSTxnType.Local)
						{
							OracleLogicalTransaction oracleLogicalTransaction = null;
							this.m_connImpl.m_mtsTxnCtx.m_localTxn.Rollback(null, ref oracleLogicalTransaction);
						}
					}
					catch
					{
					}
					finally
					{
						try
						{
							this.m_connImpl.SetAutoCommit(true);
						}
						catch
						{
						}
						try
						{
							if (this.m_connImpl.m_mtsTxnCtx != null)
							{
								this.m_connImpl.ResetMTSTxnCtx();
							}
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
								"Rolled back local transactionLocal TxnID = ",
								this.m_sysTxn.TransactionInformation.LocalIdentifier,
								"using Conn ID = ",
								this.m_connImpl.m_endUserSessionId,
								" to DBInst = ",
								this.m_connImpl.m_instanceName
							})
						});
					}
				}
				finally
				{
					try
					{
						if (ProviderConfig.m_bTraceLevelPrivate)
						{
							Trace.Write(OracleTraceLevel.Private, OracleTraceTag.MTS, new string[]
							{
								string.Concat(new object[]
								{
									"FWPSPEManager.HandlePromoteError(): Releasing Conn ID = ",
									this.m_connImpl.m_endUserSessionId,
									" to DBInst = ",
									this.m_connImpl.m_instanceName,
									"\tLocal TxnID ",
									this.m_sysTxn.TransactionInformation.LocalIdentifier
								})
							});
						}
					}
					catch
					{
					}
					try
					{
						this.ResetForPromotedTxn(this.m_connImpl, this.m_sysTxn, this.m_localTxnIdentifier);
					}
					catch
					{
					}
					try
					{
						this.m_mtsTxnRM.ReleaseRP(this.m_connStr, this.m_sysTxn);
					}
					catch
					{
					}
					this.m_connImpl = null;
				}
			}
		}

		// Token: 0x04000D33 RID: 3379
		private static MethodInfo s_promoteMethod;
	}
}
