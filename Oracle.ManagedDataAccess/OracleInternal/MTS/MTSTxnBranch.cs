using System;
using System.Data;
using System.Transactions;
using Oracle.ManagedDataAccess.Client;
using OracleInternal.Common;
using OracleInternal.ConnectionPool;
using OracleInternal.ServiceObjects;

namespace OracleInternal.MTS
{
	// Token: 0x02000119 RID: 281
	internal abstract class MTSTxnBranch
	{
		// Token: 0x06000C15 RID: 3093 RVA: 0x00086DE4 File Offset: 0x00084FE4
		internal static MTSTxnBranch CreateTxnBranch(bool bIsCCP, MTSTxnRM txnRM, int branchNum)
		{
			return new CCPMTSTxnBranch(txnRM, branchNum);
		}

		// Token: 0x06000C16 RID: 3094 RVA: 0x00086DF0 File Offset: 0x00084FF0
		internal MTSTxnBranch(MTSTxnRM txnRM, int branchNum)
		{
			this.m_mtsTxnRM = txnRM;
			this.m_branchNum = branchNum;
		}

		// Token: 0x06000C17 RID: 3095 RVA: 0x00086E4C File Offset: 0x0008504C
		~MTSTxnBranch()
		{
		}

		// Token: 0x17000253 RID: 595
		// (get) Token: 0x06000C18 RID: 3096 RVA: 0x00086E74 File Offset: 0x00085074
		internal int BranchNumber
		{
			get
			{
				return this.m_branchNum;
			}
		}

		// Token: 0x17000254 RID: 596
		// (get) Token: 0x06000C19 RID: 3097 RVA: 0x00086E7C File Offset: 0x0008507C
		internal TransXID TxnID
		{
			get
			{
				return this.m_xid;
			}
		}

		// Token: 0x17000255 RID: 597
		// (get) Token: 0x06000C1A RID: 3098 RVA: 0x00086E84 File Offset: 0x00085084
		internal TxnBranchState State
		{
			get
			{
				return this.m_branchState;
			}
		}

		// Token: 0x17000256 RID: 598
		// (get) Token: 0x06000C1B RID: 3099 RVA: 0x00086E8C File Offset: 0x0008508C
		internal bool IsInTxn
		{
			get
			{
				return this.m_branchState == TxnBranchState.InUse || this.m_branchState == TxnBranchState.Free;
			}
		}

		// Token: 0x06000C1C RID: 3100
		internal abstract OracleConnectionImpl GetConnection(bool bMustMatch, out bool bMatchFound);

		// Token: 0x06000C1D RID: 3101
		internal abstract bool CanResetConnection(bool bMatchConn, TxnBranchState branchState);

		// Token: 0x06000C1E RID: 3102 RVA: 0x00086EA4 File Offset: 0x000850A4
		internal virtual void ReleaseConnection(string txnOperation, OracleConnectionImpl connImpl, TransXID txnXID)
		{
		}

		// Token: 0x06000C1F RID: 3103 RVA: 0x00086EA8 File Offset: 0x000850A8
		internal virtual void SetConnection(OracleConnectionImpl connImpl)
		{
		}

		// Token: 0x06000C20 RID: 3104 RVA: 0x00086EAC File Offset: 0x000850AC
		internal void Set(string txnLocalID, Guid txnXID, System.Transactions.IsolationLevel txnIsolationLvl)
		{
			this.m_txnLocalID = txnLocalID;
			this.m_xid = TransXID.CreateOracleXID(txnXID, this.m_mtsTxnRM.m_RMGuid, this.m_branchNum);
			this.m_opoDTCTxnCtx = new OpoDTCTxnCtx(this.m_xid.m_opoDTCTxnXID);
			if (!ConfigBaseClass.m_bLegacyIsolationLevelBehavior && txnIsolationLvl == System.Transactions.IsolationLevel.Serializable)
			{
				this.m_txnIsolationLvl = System.Data.IsolationLevel.Serializable;
			}
		}

		// Token: 0x06000C21 RID: 3105 RVA: 0x00086F08 File Offset: 0x00085108
		internal void StartDistributedTransaction(OracleConnectionImpl connImpl)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)4352, new string[]
				{
					string.Concat(new object[]
					{
						"Branch State = ",
						this.m_branchState,
						"\t",
						this.m_bNew ? "Start " : "Resume ",
						" TxnID = ",
						this.m_xid,
						" using Conn ID = ",
						connImpl.m_endUserSessionId,
						" to DBInst = ",
						connImpl.m_instanceName,
						" Isolation Level = ",
						this.m_txnIsolationLvl
					})
				});
			}
			try
			{
				if (connImpl.m_currentIsolationLvl != this.m_txnIsolationLvl && !this.m_mtsTxnRM.m_bIgnoreIsolationLvl)
				{
					connImpl.SwitchIsolationLevel(this.m_txnIsolationLvl);
				}
				if (this.m_bNew)
				{
					MTSTransactionImpl.Start(connImpl, this.m_opoDTCTxnCtx, ConfigBaseClass.m_dtcTxnTimeout);
				}
				else
				{
					MTSTransactionImpl.Resume(connImpl, this.m_opoDTCTxnCtx, ConfigBaseClass.m_dtcTxnTimeout);
				}
				this.m_mtsTxnRM.m_bIgnoreIsolationLvl = true;
				this.m_connCreds = connImpl.m_cs;
				connImpl.SetAutoCommit(false);
				if (connImpl.m_mtsTxnCtx == null)
				{
					connImpl.m_mtsTxnCtx = MTSTxnCtx.CreateMTSTxnCtx(connImpl);
				}
				connImpl.m_mtsTxnCtx.SetDistributedCtx(this.m_txnLocalID, this, (long)connImpl.m_endUserSessionId);
				this.SetConnection(connImpl);
				this.m_dbInstance = connImpl.m_instanceName;
				this.m_branchState = TxnBranchState.InUse;
			}
			catch (Exception ex)
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)268439552, new string[]
					{
						string.Concat(new object[]
						{
							"Error in starting transaction ",
							this.m_bNew ? "Start " : "Resume ",
							" TxnID = ",
							this.m_xid,
							" using Conn ID = ",
							connImpl.m_endUserSessionId,
							" to DBInst = ",
							connImpl.m_instanceName,
							Environment.NewLine,
							ex.Message
						})
					});
				}
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)4608, new string[]
					{
						string.Concat(new object[]
						{
							"Branch State = ",
							this.m_branchState,
							"\t",
							this.m_bNew ? "Start " : "Resume ",
							" TxnID = ",
							this.m_xid,
							" using Conn ID = ",
							connImpl.m_endUserSessionId,
							" to DBInst = ",
							connImpl.m_instanceName
						})
					});
				}
			}
		}

		// Token: 0x06000C22 RID: 3106 RVA: 0x00087214 File Offset: 0x00085414
		internal void PromoteDistributedTransaction(OracleConnectionImpl connImpl)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)4352, new string[]
				{
					string.Concat(new object[]
					{
						"Branch State = ",
						this.m_branchState,
						"\t TxnID = ",
						this.m_xid,
						" using Conn ID = ",
						connImpl.m_endUserSessionId,
						" to DBInst = ",
						connImpl.m_instanceName
					})
				});
			}
			try
			{
				MTSTransactionImpl.Promote(connImpl, this.m_opoDTCTxnCtx, ConfigBaseClass.m_dtcTxnTimeout);
				this.m_connCreds = connImpl.m_cs;
				connImpl.SetAutoCommit(false);
				connImpl.m_mtsTxnCtx.SetDistributedCtx(this.m_txnLocalID, this, (long)connImpl.m_endUserSessionId);
				this.SetConnection(connImpl);
				this.m_dbInstance = connImpl.m_instanceName;
				this.m_branchState = TxnBranchState.InUse;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)4608, new string[]
					{
						string.Concat(new object[]
						{
							"Branch State = ",
							this.m_branchState,
							"\tTxnID = ",
							this.m_xid,
							" using Conn ID = ",
							connImpl.m_endUserSessionId,
							" to DBInst = ",
							connImpl.m_instanceName
						})
					});
				}
			}
		}

		// Token: 0x06000C23 RID: 3107 RVA: 0x0008737C File Offset: 0x0008557C
		internal void Detach()
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)4352, new string[]
				{
					string.Concat(new object[]
					{
						"Branch State = ",
						this.m_branchState,
						"\t TxnID = ",
						this.m_xid
					})
				});
			}
			OracleConnectionImpl oracleConnectionImpl = null;
			bool flag = false;
			try
			{
				if (this.m_branchState == TxnBranchState.InUse)
				{
					if (this.m_branchState == TxnBranchState.InUse)
					{
						lock (this.m_lock)
						{
							if (this.m_branchState == TxnBranchState.InUse)
							{
								oracleConnectionImpl = this.GetConnection(true, out flag);
								if (oracleConnectionImpl != null && oracleConnectionImpl.m_bConnected)
								{
									MTSTransactionImpl.Detach(oracleConnectionImpl, this.m_opoDTCTxnCtx, ConfigBaseClass.m_dtcTxnTimeout);
									if (flag)
									{
										oracleConnectionImpl.SetAutoCommit(true);
										if (oracleConnectionImpl.m_mtsTxnCtx != null)
										{
											oracleConnectionImpl.ResetMTSTxnCtx();
										}
									}
									this.m_branchState = TxnBranchState.Free;
									this.m_mtsTxnRM.FreeTxnBranch(this);
								}
							}
						}
					}
				}
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, OracleTraceTag.MTS, ex, null);
			}
			finally
			{
				if (oracleConnectionImpl != null)
				{
					this.ReleaseConnection("Detach", oracleConnectionImpl, this.m_xid);
				}
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)4608, new string[]
					{
						string.Concat(new object[]
						{
							"Branch State = ",
							this.m_branchState,
							"\t TxnID = ",
							this.m_xid
						})
					});
				}
			}
		}

		// Token: 0x06000C24 RID: 3108 RVA: 0x00087524 File Offset: 0x00085724
		internal void Detach(OracleConnectionImpl connImpl)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)4352, new string[]
				{
					string.Concat(new object[]
					{
						"Branch State = ",
						this.m_branchState,
						"\t TxnID = ",
						this.m_xid,
						" using Conn ID = ",
						connImpl.m_endUserSessionId,
						" to DBInst = ",
						connImpl.m_instanceName
					})
				});
			}
			try
			{
				if (this.m_branchState == TxnBranchState.InUse && connImpl.m_bConnected)
				{
					MTSTransactionImpl.Detach(connImpl, this.m_opoDTCTxnCtx, ConfigBaseClass.m_dtcTxnTimeout);
				}
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, OracleTraceTag.MTS, ex, null);
			}
			finally
			{
				if (connImpl.m_bConnected)
				{
					connImpl.SetAutoCommit(true);
					if (connImpl.m_mtsTxnCtx != null)
					{
						connImpl.ResetMTSTxnCtx();
					}
					this.m_branchState = TxnBranchState.Free;
					this.m_mtsTxnRM.FreeTxnBranch(this);
				}
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)4608, new string[]
					{
						string.Concat(new object[]
						{
							"Branch State = ",
							this.m_branchState,
							"\t TxnID = ",
							this.m_xid,
							" using Conn ID = ",
							connImpl.m_endUserSessionId,
							" to DBInst = ",
							connImpl.m_instanceName
						})
					});
				}
			}
		}

		// Token: 0x06000C25 RID: 3109 RVA: 0x000876B4 File Offset: 0x000858B4
		internal TxnState Prepare()
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)4352, new string[]
				{
					string.Concat(new object[]
					{
						"Branch State = ",
						this.m_branchState,
						"\t TxnID = ",
						this.m_xid,
						" Branch # = ",
						this.m_branchNum,
						"\t Local Txn ID = ",
						this.m_txnLocalID
					})
				});
			}
			TxnState result;
			try
			{
				if (this.m_branchState == TxnBranchState.Done || this.m_branchState == TxnBranchState.NotValid)
				{
					result = TxnState.K2CMDrdonly;
				}
				else
				{
					TxnState txnState = TxnState.Error;
					OracleConnectionImpl oracleConnectionImpl = null;
					bool bMatchConn = false;
					try
					{
						if (this.m_branchState == TxnBranchState.InUse || this.m_branchState == TxnBranchState.Free)
						{
							lock (this.m_lock)
							{
								if (this.m_branchState == TxnBranchState.InUse || this.m_branchState == TxnBranchState.Free)
								{
									oracleConnectionImpl = this.GetConnection(false, out bMatchConn);
									if (oracleConnectionImpl.m_bConnected)
									{
										txnState = MTSTransactionImpl.Prepare(oracleConnectionImpl, this.m_opoDTCTxnCtx, ConfigBaseClass.m_dtcTxnTimeout, bMatchConn);
										if (ProviderConfig.m_bTraceLevelPrivate)
										{
											Trace.Write(OracleTraceLevel.Private, OracleTraceTag.MTS, new string[]
											{
												string.Concat(new object[]
												{
													"MTSTxnBranch.Prepare(): Prepare State = ",
													txnState,
													"\tTxnID = ",
													this.m_xid,
													" using Conn ID = ",
													oracleConnectionImpl.m_endUserSessionId,
													" to DBInst = ",
													oracleConnectionImpl.m_instanceName
												})
											});
										}
										if (txnState == TxnState.K2CMDrdonly || txnState == TxnState.K2CMDrqcommit)
										{
											if (txnState == TxnState.K2CMDrdonly)
											{
												this.m_branchState = TxnBranchState.Done;
											}
											else
											{
												this.m_branchState = TxnBranchState.Free;
											}
											if (this.CanResetConnection(bMatchConn, this.m_branchState))
											{
												oracleConnectionImpl.SetAutoCommit(true);
												if (oracleConnectionImpl.m_mtsTxnCtx != null)
												{
													oracleConnectionImpl.ResetMTSTxnCtx();
												}
											}
										}
									}
								}
							}
						}
					}
					catch (Exception ex)
					{
						OracleException.HandleError(OracleTraceLevel.Private, OracleTraceTag.MTS, ex, null);
						txnState = TxnState.Error;
						throw;
					}
					finally
					{
						if (oracleConnectionImpl != null)
						{
							this.ReleaseConnection("Prepare", oracleConnectionImpl, this.m_xid);
						}
					}
					result = txnState;
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
							"Branch State = ",
							this.m_branchState,
							"\t TxnID = ",
							this.m_xid,
							" Branch # = ",
							this.m_branchNum,
							"\t Local Txn ID = ",
							this.m_txnLocalID
						})
					});
				}
			}
			return result;
		}

		// Token: 0x06000C26 RID: 3110 RVA: 0x000879BC File Offset: 0x00085BBC
		internal TxnState Commit()
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)4352, new string[]
				{
					string.Concat(new object[]
					{
						"Branch State = ",
						this.m_branchState,
						"\t TxnID = ",
						this.m_xid
					})
				});
			}
			TxnState result;
			try
			{
				if (this.m_branchState == TxnBranchState.Done || this.m_branchState == TxnBranchState.NotValid)
				{
					result = TxnState.K2CMDcommit;
				}
				else
				{
					TxnState txnState = TxnState.Error;
					OracleConnectionImpl oracleConnectionImpl = null;
					bool flag = false;
					try
					{
						if (this.m_branchState == TxnBranchState.InUse || this.m_branchState == TxnBranchState.Free)
						{
							lock (this.m_lock)
							{
								if (this.m_branchState == TxnBranchState.InUse || this.m_branchState == TxnBranchState.Free)
								{
									oracleConnectionImpl = this.GetConnection(false, out flag);
									txnState = MTSTransactionImpl.Commit(oracleConnectionImpl, this.m_opoDTCTxnCtx, ConfigBaseClass.m_dtcTxnTimeout, flag);
									if (ProviderConfig.m_bTraceLevelPrivate)
									{
										Trace.Write(OracleTraceLevel.Private, OracleTraceTag.MTS, new string[]
										{
											string.Concat(new object[]
											{
												"MTSTxnBranch.Commit(): Commit State = ",
												txnState,
												"\tTxnID = ",
												this.m_xid,
												" using Conn ID = ",
												oracleConnectionImpl.m_endUserSessionId,
												" to DBInst = ",
												oracleConnectionImpl.m_instanceName
											})
										});
									}
									if (txnState == TxnState.K2CMDcommit || txnState == TxnState.K2CMDforget)
									{
										this.m_branchState = TxnBranchState.Done;
										if (flag)
										{
											oracleConnectionImpl.SetAutoCommit(true);
											if (oracleConnectionImpl.m_mtsTxnCtx != null)
											{
												oracleConnectionImpl.ResetMTSTxnCtx();
											}
										}
									}
								}
							}
						}
					}
					catch (Exception ex)
					{
						OracleException.HandleError(OracleTraceLevel.Private, OracleTraceTag.MTS, ex, null);
						txnState = TxnState.Error;
						throw;
					}
					finally
					{
						if (oracleConnectionImpl != null)
						{
							this.ReleaseConnection("Commit", oracleConnectionImpl, this.m_xid);
						}
					}
					result = txnState;
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
							"Branch State = ",
							this.m_branchState,
							"\t TxnID = ",
							this.m_xid
						})
					});
				}
			}
			return result;
		}

		// Token: 0x06000C27 RID: 3111 RVA: 0x00087C48 File Offset: 0x00085E48
		internal TxnState Abort()
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)4352, new string[]
				{
					string.Concat(new object[]
					{
						"Branch State = ",
						this.m_branchState,
						"\t TxnID = ",
						this.m_xid
					})
				});
			}
			TxnState result;
			try
			{
				if (this.m_branchState == TxnBranchState.Done || this.m_branchState == TxnBranchState.NotValid)
				{
					result = TxnState.K2CMDabort;
				}
				else
				{
					TxnState txnState = TxnState.Error;
					OracleConnectionImpl oracleConnectionImpl = null;
					bool flag = false;
					try
					{
						if (this.m_branchState == TxnBranchState.InUse || this.m_branchState == TxnBranchState.Free)
						{
							lock (this.m_lock)
							{
								if (this.m_branchState == TxnBranchState.InUse || this.m_branchState == TxnBranchState.Free)
								{
									oracleConnectionImpl = this.GetConnection(false, out flag);
									txnState = MTSTransactionImpl.Abort(oracleConnectionImpl, this.m_opoDTCTxnCtx, 2U, flag);
									if (ProviderConfig.m_bTraceLevelPrivate)
									{
										Trace.Write(OracleTraceLevel.Private, OracleTraceTag.MTS, new string[]
										{
											string.Concat(new object[]
											{
												"Abort State = ",
												txnState,
												"\tTxnID = ",
												this.m_xid,
												" using Conn ID = ",
												oracleConnectionImpl.m_endUserSessionId,
												" to DBInst = ",
												oracleConnectionImpl.m_instanceName
											})
										});
									}
									if (txnState == TxnState.K2CMDabort)
									{
										this.m_branchState = TxnBranchState.Done;
										if (flag)
										{
											oracleConnectionImpl.SetAutoCommit(true);
											if (oracleConnectionImpl.m_mtsTxnCtx != null)
											{
												oracleConnectionImpl.ResetMTSTxnCtx();
											}
										}
									}
								}
							}
						}
					}
					catch (Exception ex)
					{
						OracleException.HandleError(OracleTraceLevel.Private, OracleTraceTag.MTS, ex, null);
						txnState = TxnState.Error;
						throw;
					}
					finally
					{
						if (oracleConnectionImpl != null)
						{
							this.ReleaseConnection("Abort", oracleConnectionImpl, this.m_xid);
						}
					}
					result = txnState;
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
							"Branch State = ",
							this.m_branchState,
							"\t TxnID = ",
							this.m_xid
						})
					});
				}
			}
			return result;
		}

		// Token: 0x04000D34 RID: 3380
		internal const int ORA_ERR_SET_TXN_ISOLATIONLVL = 1453;

		// Token: 0x04000D35 RID: 3381
		protected MTSTxnRM m_mtsTxnRM;

		// Token: 0x04000D36 RID: 3382
		internal string m_txnLocalID = string.Empty;

		// Token: 0x04000D37 RID: 3383
		protected TransXID m_xid;

		// Token: 0x04000D38 RID: 3384
		internal OpoDTCTxnCtx m_opoDTCTxnCtx;

		// Token: 0x04000D39 RID: 3385
		internal ConnectionString m_connCreds;

		// Token: 0x04000D3A RID: 3386
		protected int m_branchNum = -1;

		// Token: 0x04000D3B RID: 3387
		internal bool m_bNew = true;

		// Token: 0x04000D3C RID: 3388
		internal System.Data.IsolationLevel m_txnIsolationLvl = System.Data.IsolationLevel.ReadCommitted;

		// Token: 0x04000D3D RID: 3389
		internal string m_dbInstance = string.Empty;

		// Token: 0x04000D3E RID: 3390
		protected TxnBranchState m_branchState;

		// Token: 0x04000D3F RID: 3391
		private object m_lock = new object();
	}
}
