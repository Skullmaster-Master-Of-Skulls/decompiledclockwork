using System;
using Oracle.ManagedDataAccess.Client;
using OracleInternal.Common;
using OracleInternal.ConnectionPool;
using OracleInternal.ServiceObjects;

namespace OracleInternal.MTS
{
	// Token: 0x0200011A RID: 282
	internal class CCPMTSTxnBranch : MTSTxnBranch
	{
		// Token: 0x06000C28 RID: 3112 RVA: 0x00087ECC File Offset: 0x000860CC
		internal CCPMTSTxnBranch(MTSTxnRM txnRM, int branchNum) : base(txnRM, branchNum)
		{
		}

		// Token: 0x06000C29 RID: 3113 RVA: 0x00087ED8 File Offset: 0x000860D8
		internal override OracleConnectionImpl GetConnection(bool bMustMatch, out bool bMatchFound)
		{
			OracleConnectionImpl oracleConnectionImpl = null;
			bMatchFound = false;
			try
			{
				oracleConnectionImpl = OracleConnectionDispenser<OraclePoolManager, OraclePool, OracleConnectionImpl>.GetEnlisted(this.m_connCreds, this.m_mtsTxnRM.m_serviceName, this.m_mtsTxnRM.m_pdbName, this.m_mtsTxnRM.m_sysTxn, this.m_dbInstance, this.m_branchNum, bMustMatch, out bMatchFound);
			}
			catch (Exception ex)
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)6144, new string[]
					{
						string.Concat(new object[]
						{
							"MTSTxnBranch.GetConnection(): Get Exception in finding  a connection in  OracleConnectionDispenser.GetEnlisted()bMustMatch = ",
							bMustMatch,
							" bMatchFound = ",
							bMatchFound,
							"\t TxnID = ",
							this.m_xid
						})
					});
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)6144, new string[]
					{
						OracleConnectionDispenser<OraclePoolManager, OraclePool, OracleConnectionImpl>.Dump()
					});
				}
				OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)6144, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					if (oracleConnectionImpl != null)
					{
						Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)6144, new string[]
						{
							string.Concat(new object[]
							{
								"MTSTxnBranch.GetConnection(bMustMatch = ",
								bMustMatch,
								", bMatchFound = ",
								bMatchFound,
								"): Get a Connection with Conn ID = ",
								oracleConnectionImpl.m_endUserSessionId,
								" to DBInst = ",
								oracleConnectionImpl.m_instanceName,
								"\t TxnID = ",
								this.m_xid
							})
						});
					}
					else
					{
						Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)6144, new string[]
						{
							string.Concat(new object[]
							{
								"MTSTxnBranch.GetConnection(bMustMatch = ",
								bMustMatch,
								", bMatchFound = ",
								bMatchFound,
								"): Cannot find a Connection. \t TxnID = ",
								this.m_xid
							})
						});
					}
				}
			}
			return oracleConnectionImpl;
		}

		// Token: 0x06000C2A RID: 3114 RVA: 0x000880F4 File Offset: 0x000862F4
		internal override void ReleaseConnection(string txnOperation, OracleConnectionImpl connImpl, TransXID txnXID)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)4352, new string[]
				{
					string.Concat(new object[]
					{
						txnOperation,
						": Releasing Conn ID = ",
						connImpl.m_endUserSessionId,
						" to DBInst = ",
						connImpl.m_instanceName,
						"\t TxnID = ",
						txnXID,
						"\t Local Txn id = ",
						this.m_txnLocalID
					})
				});
			}
			try
			{
				OracleConnectionDispenser<OraclePoolManager, OraclePool, OracleConnectionImpl>.PutFromDTC(connImpl);
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)6144, ex, null);
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)4608, new string[]
					{
						string.Concat(new object[]
						{
							txnOperation,
							": Releasing Conn ID = ",
							connImpl.m_endUserSessionId,
							" to DBInst = ",
							connImpl.m_instanceName,
							"\t TxnID = ",
							txnXID,
							"\t Local Txn ID = ",
							this.m_txnLocalID
						})
					});
				}
			}
		}

		// Token: 0x06000C2B RID: 3115 RVA: 0x0008822C File Offset: 0x0008642C
		internal override bool CanResetConnection(bool bMatchConn, TxnBranchState branchState)
		{
			return bMatchConn;
		}
	}
}
