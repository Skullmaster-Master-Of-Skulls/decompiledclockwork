using System;
using System.Data;
using System.Reflection;
using System.Transactions;
using Oracle.ManagedDataAccess.Client;
using OracleInternal.Common;
using OracleInternal.ConnectionPool;
using OracleInternal.ServiceObjects;

namespace OracleInternal.MTS
{
	// Token: 0x02000114 RID: 276
	internal abstract class PSPEManager : PSPETxnManagerBase
	{
		// Token: 0x06000BF5 RID: 3061 RVA: 0x000857A0 File Offset: 0x000839A0
		static PSPEManager()
		{
			try
			{
				if (ConfigBaseClass.m_dtcUseDTCDLL)
				{
					PSPEManager.s_bUseDotNetAPIForPromotion = false;
				}
				else
				{
					Type typeFromHandle = typeof(Transaction);
					MethodInfo method = typeFromHandle.GetMethod(PSPEManager.s_promotableMethodName);
					if (method != null)
					{
						FWPSPEManager.InitPromoteAndEnlistMethod(method);
						PSPEManager.s_bUseDotNetAPIForPromotion = true;
					}
					else
					{
						PSPEManager.s_bUseDotNetAPIForPromotion = false;
					}
				}
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)268439552, ex, null);
				throw ex;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, OracleTraceTag.MTS, new string[]
					{
						"Use New .Net API to Promote = " + PSPEManager.s_bUseDotNetAPIForPromotion
					});
				}
			}
		}

		// Token: 0x06000BF6 RID: 3062 RVA: 0x00085864 File Offset: 0x00083A64
		internal PSPEManager(OracleConnectionImpl connImpl, Transaction txn, MTSTxnRM txnRM, MTSTxnBranch txnBranch)
		{
			this.m_sysTxn = txn.Clone();
			this.m_localTxnIdentifier = this.m_sysTxn.TransactionInformation.LocalIdentifier;
			this.m_connImpl = connImpl;
			this.m_connStr = this.m_connImpl.m_cs;
			this.m_mtsTxnRM = txnRM;
			this.m_promotedTxnBranch = txnBranch;
		}

		// Token: 0x06000BF7 RID: 3063
		internal abstract byte[] InternalPromote(out Guid txnGuid);

		// Token: 0x06000BF8 RID: 3064
		internal abstract bool InternalCommit();

		// Token: 0x06000BF9 RID: 3065
		internal abstract bool InternalRollback();

		// Token: 0x06000BFA RID: 3066
		internal abstract void InternalHandlePromoteError();

		// Token: 0x06000BFB RID: 3067
		internal abstract void InitialPSPEConn(Transaction txn, OracleConnectionImpl connImpl);

		// Token: 0x06000BFC RID: 3068
		internal abstract void ResetForPromotedTxn(OracleConnectionImpl connImpl, Transaction txn, string txnLocalId);

		// Token: 0x06000BFD RID: 3069 RVA: 0x000858C0 File Offset: 0x00083AC0
		internal static PSPEManager Create(OracleConnectionImpl connImpl, Transaction txn, MTSTxnRM txnRM, MTSTxnBranch txnBranch)
		{
			if (PSPEManager.s_bUseDotNetAPIForPromotion)
			{
				return new CCPFWPSPEManager(connImpl, txn, txnRM, txnBranch);
			}
			return new CCPDTCPSPEManager(connImpl, txn, txnRM, txnBranch);
		}

		// Token: 0x06000BFE RID: 3070 RVA: 0x000858DC File Offset: 0x00083ADC
		internal void ReleaseRM(ConnectionString cs)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)4352, new string[]
				{
					"Local TxnID = " + this.m_sysTxn.TransactionInformation.LocalIdentifier + "\t ConnectionString = " + cs.m_constring
				});
			}
			string localIdentifier = this.m_sysTxn.TransactionInformation.LocalIdentifier;
			this.m_mtsTxnRM.ReleaseRPs(this.m_mtsTxnRM.m_connStrs, this.m_sysTxn);
			this.m_mtsTxnRM.ReleaseRP(cs, this.m_sysTxn);
			MTSProxyPool.ReleaseRM(this.m_mtsTxnRM.m_bIsCCP, this.m_mtsTxnRM.m_dbEasyConnectName, this.m_sysTxn);
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)4608, new string[]
				{
					"Local TxnID = " + localIdentifier + "\t ConnectionString = " + cs.m_constring
				});
			}
		}

		// Token: 0x06000BFF RID: 3071 RVA: 0x000859C0 File Offset: 0x00083BC0
		public override void Initialize()
		{
			try
			{
				System.Data.IsolationLevel isolationLevel = System.Data.IsolationLevel.ReadCommitted;
				if (!ConfigBaseClass.m_bLegacyIsolationLevelBehavior && this.m_sysTxn.IsolationLevel == System.Transactions.IsolationLevel.Serializable)
				{
					isolationLevel = System.Data.IsolationLevel.Serializable;
				}
				OracleTransactionImpl localTxn = new OracleTransactionImpl(this.m_connImpl, isolationLevel);
				this.m_mtsTxnRM.m_bIgnoreIsolationLvl = true;
				this.m_connImpl.SetAutoCommit(false);
				if (this.m_connImpl.m_mtsTxnCtx == null)
				{
					this.m_connImpl.m_mtsTxnCtx = MTSTxnCtx.CreateMTSTxnCtx(this.m_connImpl);
				}
				this.m_connImpl.m_mtsTxnCtx.SetLocalCtx(this.m_sysTxn.TransactionInformation.LocalIdentifier, localTxn, (long)this.m_connImpl.m_endUserSessionId);
				this.InitialPSPEConn(this.m_sysTxn, this.m_connImpl);
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)268439552, ex, null);
				this.m_mtsTxnRM.ReleaseRP(this.m_connStr, this.m_sysTxn);
				throw;
			}
		}

		// Token: 0x06000C00 RID: 3072 RVA: 0x00085AB0 File Offset: 0x00083CB0
		public override byte[] Promote()
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)4352, new string[]
				{
					string.Concat(new object[]
					{
						"Local TxnID = ",
						this.m_sysTxn.TransactionInformation.LocalIdentifier,
						" using Conn ID = ",
						this.m_connImpl.m_endUserSessionId,
						" to DBInst = ",
						this.m_connImpl.m_instanceName
					})
				});
			}
			byte[] array = null;
			byte[] result;
			try
			{
				Guid sysTxnXID;
				array = this.InternalPromote(out sysTxnXID);
				try
				{
					MTSRMManager.EnlistPromotedTransaction(this.m_connImpl, this.m_sysTxn, this.m_mtsTxnRM, this.m_promotedTxnBranch, sysTxnXID);
					this.m_bLocalTxnPromoted = true;
					if (ProviderConfig.m_bTraceLevelPrivate)
					{
						Trace.Write(OracleTraceLevel.Private, OracleTraceTag.MTS, new string[]
						{
							string.Concat(new object[]
							{
								" Releasing Connection with Conn ID = ",
								this.m_connImpl.m_endUserSessionId,
								" to DBInst = ",
								this.m_connImpl.m_instanceName,
								"\t TxnID = ",
								this.m_promotedTxnBranch.TxnID
							})
						});
					}
					this.ResetForPromotedTxn(this.m_connImpl, this.m_sysTxn, this.m_localTxnIdentifier);
					this.m_connImpl = null;
				}
				catch (Exception ex)
				{
					if (ProviderConfig.m_bTraceLevelPrivate)
					{
						Trace.Write(OracleTraceLevel.Private, OracleTraceTag.MTS, new string[]
						{
							string.Concat(new object[]
							{
								"PSPEManager.Promote(): Database Promotion Error for Local Txn ID = ",
								this.m_localTxnIdentifier,
								" ",
								ex.Message,
								Environment.NewLine,
								" with Conn ID = ",
								this.m_connImpl.m_endUserSessionId,
								" to DBInst = ",
								this.m_connImpl.m_instanceName,
								"\t TxnID = ",
								this.m_promotedTxnBranch.TxnID
							})
						});
					}
					this.InternalHandlePromoteError();
					array = new byte[1];
				}
				result = array;
			}
			catch (Exception ex2)
			{
				try
				{
					OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)268439552, ex2, null);
				}
				catch
				{
				}
				array = new byte[1];
				result = array;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)4608, new string[]
					{
						string.Concat(new object[]
						{
							"Local TxnID = ",
							this.m_sysTxn.TransactionInformation.LocalIdentifier,
							"TxnID = ",
							this.m_sysTxn.TransactionInformation.DistributedIdentifier
						})
					});
				}
			}
			return result;
		}

		// Token: 0x06000C01 RID: 3073 RVA: 0x00085DCC File Offset: 0x00083FCC
		public override void SinglePhaseCommit(SinglePhaseEnlistment singlePhaseEnlistment)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)4352, new string[]
				{
					string.Concat(new object[]
					{
						"Local TxnID = ",
						this.m_sysTxn.TransactionInformation.LocalIdentifier,
						"\tTxnID = ",
						this.m_sysTxn.TransactionInformation.DistributedIdentifier
					})
				});
			}
			try
			{
				if (!this.m_bLocalTxnPromoted)
				{
					try
					{
						try
						{
							if (this.m_connImpl.m_mtsTxnCtx != null && this.m_connImpl.m_mtsTxnCtx.m_txnType == MTSTxnType.Local)
							{
								if (ProviderConfig.m_bTraceLevelPrivate)
								{
									Trace.Write(OracleTraceLevel.Private, OracleTraceTag.MTS, new string[]
									{
										string.Concat(new object[]
										{
											"Local transaction committing Local TxnID = ",
											this.m_sysTxn.TransactionInformation.LocalIdentifier,
											" using Conn ID = ",
											this.m_connImpl.m_endUserSessionId,
											" to DBInst = ",
											this.m_connImpl.m_instanceName
										})
									});
								}
								OracleLogicalTransaction oracleLogicalTransaction = null;
								this.m_connImpl.m_mtsTxnCtx.m_localTxn.Commit(null, ref oracleLogicalTransaction);
							}
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
						singlePhaseEnlistment.Committed();
						goto IL_253;
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
										"PSPEManager.SinglePhasecommit(): Releasing Conn ID = ",
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
							this.ReleaseRM(this.m_connStr);
						}
						catch
						{
						}
						this.m_connImpl = null;
					}
				}
				try
				{
					bool flag = this.InternalCommit();
					if (flag)
					{
						singlePhaseEnlistment.Committed();
					}
					else
					{
						singlePhaseEnlistment.Aborted();
					}
				}
				finally
				{
					this.m_mtsTxnRM = null;
				}
				IL_253:;
			}
			catch (Exception ex)
			{
				try
				{
					OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)268439552, ex, null);
				}
				catch
				{
				}
				try
				{
					singlePhaseEnlistment.Aborted(ex);
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
						string.Concat(new object[]
						{
							"Local TxnID = ",
							this.m_sysTxn.TransactionInformation.LocalIdentifier,
							"\tTxnID = ",
							this.m_sysTxn.TransactionInformation.DistributedIdentifier
						})
					});
				}
			}
		}

		// Token: 0x06000C02 RID: 3074 RVA: 0x000861E4 File Offset: 0x000843E4
		public override void Rollback(SinglePhaseEnlistment singlePhaseEnlistment)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)4352, new string[]
				{
					string.Concat(new object[]
					{
						"Local TxnID = ",
						this.m_sysTxn.TransactionInformation.LocalIdentifier,
						"\tTxnID = ",
						this.m_sysTxn.TransactionInformation.DistributedIdentifier
					})
				});
			}
			try
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
						singlePhaseEnlistment.Aborted();
						goto IL_2AD;
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
										"PSPEManager.Rollback(): Releasing Conn ID = ",
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
							this.ReleaseRM(this.m_connStr);
						}
						catch
						{
						}
						this.m_connImpl = null;
					}
				}
				try
				{
					this.InternalRollback();
					if (ProviderConfig.m_bTraceLevelPrivate)
					{
						Trace.Write(OracleTraceLevel.Private, OracleTraceTag.MTS, new string[]
						{
							string.Concat(new object[]
							{
								"PSPEManager.rollback(): DTC Transaction Rollback Local TxnID = ",
								this.m_sysTxn.TransactionInformation.LocalIdentifier,
								"\tTxnID = ",
								this.m_sysTxn.TransactionInformation.DistributedIdentifier
							})
						});
					}
					singlePhaseEnlistment.Aborted();
				}
				finally
				{
					this.m_mtsTxnRM = null;
				}
				IL_2AD:;
			}
			catch (Exception ex)
			{
				try
				{
					OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)268439552, ex, null);
				}
				catch
				{
				}
				try
				{
					singlePhaseEnlistment.Aborted(ex);
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
						string.Concat(new object[]
						{
							"Local TxnID = ",
							this.m_sysTxn.TransactionInformation.LocalIdentifier,
							"\tTxnID = ",
							this.m_sysTxn.TransactionInformation.DistributedIdentifier
						})
					});
				}
			}
		}

		// Token: 0x04000D25 RID: 3365
		internal static bool s_bUseDotNetAPIForPromotion = false;

		// Token: 0x04000D26 RID: 3366
		internal static string s_promotableMethodName = "PromoteAndEnlistDurable";

		// Token: 0x04000D27 RID: 3367
		internal OracleConnectionImpl m_connImpl;

		// Token: 0x04000D28 RID: 3368
		protected ConnectionString m_connStr;

		// Token: 0x04000D29 RID: 3369
		protected Transaction m_sysTxn;

		// Token: 0x04000D2A RID: 3370
		protected MTSTxnRM m_mtsTxnRM;

		// Token: 0x04000D2B RID: 3371
		private MTSTxnBranch m_promotedTxnBranch;
	}
}
