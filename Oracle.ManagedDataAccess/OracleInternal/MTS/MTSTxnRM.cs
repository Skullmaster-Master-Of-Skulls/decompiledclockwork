using System;
using System.Transactions;
using Oracle.ManagedDataAccess.Client;
using OracleInternal.Common;
using OracleInternal.ConnectionPool;
using OracleInternal.ServiceObjects;

namespace OracleInternal.MTS
{
	// Token: 0x0200011D RID: 285
	internal abstract class MTSTxnRM : ISinglePhaseNotification, IEnlistmentNotification
	{
		// Token: 0x06000C36 RID: 3126 RVA: 0x000885FC File Offset: 0x000867FC
		internal MTSTxnRM(bool bIsCCP)
		{
			this.m_bIsCCP = bIsCCP;
		}

		// Token: 0x06000C37 RID: 3127 RVA: 0x000886AC File Offset: 0x000868AC
		~MTSTxnRM()
		{
			this.m_txnBranches.ClearBranches();
			this.m_freeTxnBranches.ClearBranches();
			this.m_NotUsedBranches.ClearBranches();
		}

		// Token: 0x06000C38 RID: 3128
		internal abstract void ReleaseRPs(SyncQueueList<ConnectionString> csList, Transaction txn);

		// Token: 0x06000C39 RID: 3129
		internal abstract void ReleaseRP(ConnectionString cs, Transaction txn);

		// Token: 0x06000C3A RID: 3130 RVA: 0x000886F4 File Offset: 0x000868F4
		internal virtual void UnRegisteringTxnEvent(Transaction txn)
		{
		}

		// Token: 0x06000C3B RID: 3131 RVA: 0x000886F8 File Offset: 0x000868F8
		internal virtual void MTSTransactionCompleted(object sender, TransactionEventArgs e)
		{
		}

		// Token: 0x06000C3C RID: 3132 RVA: 0x000886FC File Offset: 0x000868FC
		internal void Initialize(string easyConnectName, string serviceName, string pdbName, Transaction txn)
		{
			if (ProviderConfig.m_bTraceLevelPrivate_NoTrace)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)4352, new string[]
				{
					string.Concat(new object[]
					{
						"MTSTxnRM : ",
						this.m_RMGuid,
						" RMState = ",
						this.m_state,
						" Local TxnID = ",
						txn.TransactionInformation.LocalIdentifier
					})
				});
			}
			this.m_dbEasyConnectName = easyConnectName;
			this.m_serviceName = serviceName;
			this.m_pdbName = pdbName;
			this.m_sysTxn = txn.Clone();
			this.m_txnLocalID = this.m_sysTxn.TransactionInformation.LocalIdentifier;
			this.m_RMWorker = new MTSTxnRMWorker();
			if (ProviderConfig.m_bTraceLevelPrivate_NoTrace)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)4608, new string[]
				{
					string.Concat(new object[]
					{
						"MTSTxnRM : ",
						this.m_RMGuid,
						" RMState = ",
						this.m_state,
						" Local TxnID = ",
						txn.TransactionInformation.LocalIdentifier
					})
				});
			}
		}

		// Token: 0x06000C3D RID: 3133 RVA: 0x00088828 File Offset: 0x00086A28
		internal void Reset()
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, OracleTraceTag.MTS, new string[]
				{
					string.Format("MTSTxnRM : {0} RM Guid = {1} RMState = {2} Local Txn ID = {3}", new object[]
					{
						this.GetHashCode(),
						this.m_RMGuid,
						this.m_state,
						this.m_txnLocalID
					})
				});
			}
			this.m_state = RMTxnState.Invalid;
			this.m_dbEasyConnectName = string.Empty;
			this.m_txnAffInstanceName = string.Empty;
			this.m_enlistedState = EnlistedState.Local;
			this.m_branchNum = 1;
			this.m_txnBranches.ClearBranches();
			this.m_NotUsedBranches.ClearBranches();
			this.m_freeTxnBranches.ClearBranches();
			this.m_connStrs.Clear();
			this.m_RMWorker = null;
			this.m_txnLocalID = string.Empty;
			this.m_bIgnoreIsolationLvl = false;
			if (ProviderConfig.m_bTraceLevelPrivate_NoTrace)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)4608, new string[]
				{
					string.Format("MTSTxnRM : {0} RM Guid = {1} RMState = {2} Local Txn ID = {3}", new object[]
					{
						this.GetHashCode(),
						this.m_RMGuid,
						this.m_state,
						this.m_txnLocalID
					})
				});
			}
		}

		// Token: 0x06000C3E RID: 3134 RVA: 0x0008896C File Offset: 0x00086B6C
		internal void ReleaseRM()
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)4352, new string[0]);
			}
			try
			{
				this.ReleaseRPs(this.m_connStrs, this.m_sysTxn);
				this.m_NotUsedBranches.ClearBranches();
				this.m_txnBranches.ClearBranches();
				this.m_freeTxnBranches.ClearBranches();
				MTSProxyPool.ReleaseRM(this.m_bIsCCP, this.m_dbEasyConnectName, this.m_sysTxn);
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)4608, new string[0]);
				}
			}
		}

		// Token: 0x06000C3F RID: 3135 RVA: 0x00088A08 File Offset: 0x00086C08
		public void SinglePhaseCommit(SinglePhaseEnlistment singlePhaseEnlistment)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)4352, new string[]
				{
					string.Concat(new object[]
					{
						"MTSTxnRM : ",
						this.m_RMGuid,
						"\tRMState = ",
						this.m_state,
						": ",
						this.ToString(),
						"\tLocal Txn ID = ",
						this.m_txnLocalID
					})
				});
			}
			try
			{
				if (this.m_state == RMTxnState.Enlisted)
				{
					this.m_state = RMTxnState.Preparing;
					this.m_RMWorker.SinglePhaseEvent += this.doSinglePhaseCommit;
					this.m_RMWorker.OnSinglePhase(new OnSinglePhaseEventArgs(singlePhaseEnlistment));
				}
				else
				{
					singlePhaseEnlistment.Done();
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
						string.Concat(new object[]
						{
							"MTSTxnRM : ",
							this.m_RMGuid,
							"\tRMState = ",
							this.m_state,
							": ",
							this.ToString(),
							"\tLocal Txn ID = ",
							this.m_txnLocalID
						})
					});
				}
			}
		}

		// Token: 0x06000C40 RID: 3136 RVA: 0x00088B94 File Offset: 0x00086D94
		public void Prepare(PreparingEnlistment preparingEnlistment)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)4352, new string[]
				{
					string.Concat(new object[]
					{
						"MTSTxnRM : ",
						this.m_RMGuid,
						"\tLocal Txn ID :",
						this.m_txnLocalID,
						"\tRMState = ",
						this.m_state,
						": ",
						this.ToString()
					})
				});
			}
			try
			{
				if (this.m_state == RMTxnState.Enlisted)
				{
					this.m_state = RMTxnState.Preparing;
					this.m_RMWorker.PrepareEvent += this.doPrepare;
					this.m_RMWorker.OnPrepare(new OnPrepareEventArgs(preparingEnlistment));
				}
				else
				{
					preparingEnlistment.Done();
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
							"MTSTxnRM : ",
							this.m_RMGuid,
							"\tLocal Txn ID:",
							this.m_txnLocalID,
							"\tRMState = ",
							this.m_state,
							": ",
							this.ToString()
						})
					});
				}
			}
		}

		// Token: 0x06000C41 RID: 3137 RVA: 0x00088CE4 File Offset: 0x00086EE4
		public void Commit(Enlistment enlistment)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)4352, new string[]
				{
					string.Concat(new object[]
					{
						"MTSTxnRM : ",
						this.m_RMGuid,
						"\tRMState = ",
						this.m_state,
						": ",
						this.ToString(),
						"\tLocal Txn ID = ",
						this.m_txnLocalID
					})
				});
			}
			try
			{
				if (this.m_state == RMTxnState.Prepared_ToCommit)
				{
					this.m_state = RMTxnState.Committing;
					this.m_RMWorker.CommitEvent += this.doCommit;
					this.m_RMWorker.OnCommit(new OnCommitEventArgs(enlistment));
				}
				else
				{
					enlistment.Done();
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
							"MTSTxnRM : ",
							this.m_RMGuid,
							"\tRMState = ",
							this.m_state,
							": ",
							this.ToString(),
							"\tLocal Txn ID = ",
							this.m_txnLocalID
						})
					});
				}
			}
		}

		// Token: 0x06000C42 RID: 3138 RVA: 0x00088E34 File Offset: 0x00087034
		public void Rollback(Enlistment enlistment)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)4352, new string[]
				{
					string.Concat(new object[]
					{
						"MTSTxnRM : ",
						this.m_RMGuid,
						"\tRMState = ",
						this.m_state,
						": ",
						this.ToString(),
						"\tLocal Txn ID = ",
						this.m_txnLocalID
					})
				});
			}
			try
			{
				if (this.m_state == RMTxnState.Enlisted || this.m_state == RMTxnState.Prepared_Failed || this.m_state == RMTxnState.Commit_Failed || this.m_state == RMTxnState.Prepared_ToCommit)
				{
					this.m_state = RMTxnState.RollingBack;
					this.m_RMWorker.AbortEvent += this.doAbort;
					this.m_RMWorker.OnAbort(new OnAbortEventArgs(enlistment));
				}
				else
				{
					enlistment.Done();
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
							"MTSTxnRM : ",
							this.m_RMGuid,
							"\tRMState = ",
							this.m_state,
							": ",
							this.ToString(),
							"\tLocal Txn ID = ",
							this.m_txnLocalID
						})
					});
				}
			}
		}

		// Token: 0x06000C43 RID: 3139 RVA: 0x00088FA4 File Offset: 0x000871A4
		public void InDoubt(Enlistment enlistment)
		{
			enlistment.Done();
		}

		// Token: 0x06000C44 RID: 3140 RVA: 0x00088FAC File Offset: 0x000871AC
		private RMTxnState doPrepare()
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)4352, new string[]
				{
					string.Concat(new object[]
					{
						"MTSTxnRM : ",
						this.m_RMGuid,
						"\tRMState = ",
						this.m_state,
						": ",
						this.ToString()
					})
				});
			}
			RMTxnState rmtxnState = RMTxnState.Prepared_ReadOnly;
			RMTxnState result;
			try
			{
				for (int i = 0; i < this.m_txnBranches.Count; i++)
				{
					MTSTxnBranch mtstxnBranch = this.m_txnBranches[i];
					if (mtstxnBranch.IsInTxn)
					{
						try
						{
							TxnState txnState = mtstxnBranch.Prepare();
							if (txnState == TxnState.Error)
							{
								return this.m_state = (rmtxnState = RMTxnState.Prepared_Failed);
							}
							if (txnState == TxnState.K2CMDrqcommit)
							{
								rmtxnState = (this.m_state = RMTxnState.Prepared_ToCommit);
							}
						}
						catch (Exception ex)
						{
							OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)268439552, ex, null);
							throw;
						}
					}
				}
				result = rmtxnState;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)4608, new string[]
					{
						string.Concat(new object[]
						{
							"MTSTxnRM : ",
							this.m_RMGuid,
							"\tRMState = ",
							this.m_state,
							": ",
							this.ToString()
						})
					});
				}
			}
			return result;
		}

		// Token: 0x06000C45 RID: 3141 RVA: 0x00089134 File Offset: 0x00087334
		private RMTxnState doCommit()
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)4352, new string[]
				{
					string.Concat(new object[]
					{
						"MTSTxnRM : ",
						this.m_RMGuid,
						"\tRMState = ",
						this.m_state,
						": ",
						this.ToString()
					})
				});
			}
			if ((DTCDebugConfig.s_DTCDbgEvt & DTCDebugEvent.FAILPHASE2) == DTCDebugEvent.FAILPHASE2)
			{
				this.m_state = RMTxnState.Commit_Failed;
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, OracleTraceTag.MTS, new string[]
					{
						string.Concat(new object[]
						{
							"DTC Debug Event is set to ",
							DTCDebugConfig.s_DTCDbgEvt,
							"  Phase 2 of 2pc commit failed intentionally.   TxnID = ",
							this.ToString()
						})
					});
				}
				throw new InvalidOperationException("Phase 2 of 2pc commit failed intentionally. " + this.ToString());
			}
			RMTxnState result;
			try
			{
				Exception ex = null;
				for (int i = 0; i < this.m_txnBranches.Count; i++)
				{
					MTSTxnBranch mtstxnBranch = this.m_txnBranches[i];
					if (mtstxnBranch.IsInTxn)
					{
						try
						{
							mtstxnBranch.Commit();
						}
						catch (Exception ex2)
						{
							OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)268439552, ex2, null);
							ex = ex2;
						}
					}
				}
				if (ex != null)
				{
					this.m_state = RMTxnState.Commit_Failed;
					throw ex;
				}
				result = (this.m_state = RMTxnState.Committed);
			}
			catch (Exception ex3)
			{
				try
				{
					OracleException.HandleError(OracleTraceLevel.Private, OracleTraceTag.MTS, ex3, null);
				}
				catch
				{
				}
				result = (this.m_state = RMTxnState.Commit_Failed);
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)4608, new string[]
					{
						string.Concat(new object[]
						{
							"MTSTxnRM : ",
							this.m_RMGuid,
							"\tRMState = ",
							this.m_state,
							": ",
							this.ToString()
						})
					});
				}
			}
			return result;
		}

		// Token: 0x06000C46 RID: 3142 RVA: 0x0008936C File Offset: 0x0008756C
		internal RMTxnState doAbort()
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)4352, new string[]
				{
					string.Concat(new object[]
					{
						"MTSTxnRM : ",
						this.m_RMGuid,
						"\tRMState = ",
						this.m_state,
						": ",
						this.ToString()
					})
				});
			}
			RMTxnState result;
			try
			{
				Exception ex = null;
				for (int i = 0; i < this.m_txnBranches.Count; i++)
				{
					MTSTxnBranch mtstxnBranch = this.m_txnBranches[i];
					if (mtstxnBranch.IsInTxn)
					{
						try
						{
							mtstxnBranch.Abort();
						}
						catch (Exception ex2)
						{
							OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)268439552, ex2, null);
							ex = ex2;
						}
					}
				}
				if (ex != null)
				{
					this.m_state = RMTxnState.Rollback_Failed;
					throw ex;
				}
				result = (this.m_state = RMTxnState.RollingBack);
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)4608, new string[]
					{
						string.Concat(new object[]
						{
							"MTSTxnRM : ",
							this.m_RMGuid,
							"\tRMState = ",
							this.m_state,
							": ",
							this.ToString()
						})
					});
				}
			}
			return result;
		}

		// Token: 0x06000C47 RID: 3143 RVA: 0x000894E4 File Offset: 0x000876E4
		private void doSinglePhaseCommit(object sender, OnSinglePhaseEventArgs e)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)4352, new string[]
				{
					string.Concat(new object[]
					{
						"MTSTxnRM : ",
						this.m_RMGuid,
						"\tRMState = ",
						this.m_state,
						": ",
						this.ToString(),
						"\tLocal Txn ID = ",
						this.m_txnLocalID
					})
				});
			}
			try
			{
				this.m_RMWorker.SinglePhaseEvent -= this.doSinglePhaseCommit;
				RMTxnState rmtxnState = this.m_state;
				try
				{
					rmtxnState = this.doPrepare();
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
					rmtxnState = (this.m_state = RMTxnState.Prepared_Failed);
				}
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, OracleTraceTag.MTS, new string[]
					{
						string.Concat(new object[]
						{
							"Prepare phase of doSinglePhaseCommit Result: ",
							rmtxnState,
							this.ToString(),
							"\tLocal Txn ID = ",
							this.m_txnLocalID
						})
					});
				}
				if (rmtxnState == RMTxnState.Prepared_Failed)
				{
					try
					{
						this.m_state = RMTxnState.RollingBack;
						rmtxnState = this.doAbort();
						this.m_state = RMTxnState.RollingBack;
						e.Enlistment.Aborted();
						goto IL_34E;
					}
					catch (Exception ex2)
					{
						try
						{
							OracleException.HandleError(OracleTraceLevel.Private, OracleTraceTag.MTS, ex2, null);
						}
						catch
						{
						}
						if (ProviderConfig.m_bTraceLevelPrivate)
						{
							Trace.Write(OracleTraceLevel.Private, OracleTraceTag.MTS, new string[]
							{
								string.Concat(new object[]
								{
									"Abort() phase of doSinglePhaseCommit failed Result: ",
									rmtxnState,
									this.ToString(),
									"\tLocal Txn ID = ",
									this.m_txnLocalID
								})
							});
						}
						this.m_state = RMTxnState.Rollback_Failed;
						try
						{
							e.Enlistment.Aborted(ex2);
						}
						catch
						{
						}
						goto IL_34E;
					}
				}
				if (rmtxnState == RMTxnState.Prepared_ToCommit)
				{
					if (ProviderConfig.m_bTraceLevelPrivate)
					{
						Trace.Write(OracleTraceLevel.Private, OracleTraceTag.MTS, new string[]
						{
							" Committing TxnID = " + this.ToString() + "\tLocal Txn ID = " + this.m_txnLocalID
						});
					}
					try
					{
						this.m_state = RMTxnState.Committing;
						rmtxnState = this.doCommit();
						this.m_state = RMTxnState.Committed;
					}
					catch (Exception ex3)
					{
						try
						{
							OracleException.HandleError(OracleTraceLevel.Private, OracleTraceTag.MTS, ex3, null);
						}
						catch
						{
						}
						this.m_state = RMTxnState.Commit_Failed;
						try
						{
							if (ProviderConfig.m_bTraceLevelPrivate)
							{
								Trace.Write(OracleTraceLevel.Private, OracleTraceTag.MTS, new string[]
								{
									" Committed failed.  Send InDoubt Ack for TxnID = " + this.ToString()
								});
							}
							e.Enlistment.InDoubt(ex3);
						}
						catch
						{
						}
					}
					if (rmtxnState == RMTxnState.Committed)
					{
						this.m_state = RMTxnState.Committed;
						e.Enlistment.Committed();
					}
					else
					{
						this.m_state = RMTxnState.Commit_Failed;
						if (ProviderConfig.m_bTraceLevelPrivate)
						{
							Trace.Write(OracleTraceLevel.Private, OracleTraceTag.MTS, new string[]
							{
								" Committed failed.  Send InDoubt Ack for TxnID = " + this.ToString()
							});
						}
						e.Enlistment.InDoubt();
					}
				}
				else
				{
					if (ProviderConfig.m_bTraceLevelPrivate)
					{
						Trace.Write(OracleTraceLevel.Private, OracleTraceTag.MTS, new string[]
						{
							" Tranasaction is readonly: TxnID = " + this.ToString() + "\tLocal Txn ID = " + this.m_txnLocalID
						});
					}
					this.m_state = RMTxnState.Prepared_ReadOnly;
					e.Enlistment.Committed();
				}
				IL_34E:;
			}
			catch (Exception ex4)
			{
				try
				{
					OracleException.HandleError(OracleTraceLevel.Private, OracleTraceTag.MTS, ex4, null);
				}
				catch
				{
				}
			}
			finally
			{
				this.UnRegisteringTxnEvent(this.m_sysTxn);
				try
				{
					this.ReleaseRM();
				}
				catch
				{
				}
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)4608, new string[]
					{
						string.Concat(new object[]
						{
							"MTSTxnRM : ",
							this.m_RMGuid,
							"\tRMState = ",
							this.m_state,
							" : ",
							this.ToString(),
							"\tLocal Txn ID = ",
							this.m_txnLocalID
						})
					});
				}
			}
		}

		// Token: 0x06000C48 RID: 3144 RVA: 0x00089A20 File Offset: 0x00087C20
		private void doPrepare(object sender, OnPrepareEventArgs e)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)4352, new string[]
				{
					string.Concat(new object[]
					{
						"RMState = ",
						this.m_state,
						": ",
						this.ToString(),
						"\tLocal Txn ID = ",
						this.m_txnLocalID
					})
				});
			}
			try
			{
				this.m_RMWorker.PrepareEvent -= this.doPrepare;
				try
				{
					this.m_state = this.doPrepare();
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
					this.m_state = RMTxnState.Prepared_Failed;
				}
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)4352, new string[]
					{
						string.Concat(new object[]
						{
							" MTSTxnRM.doPrepare() result:",
							this.m_state,
							" ",
							this.ToString(),
							"\tLocal Txn ID = ",
							this.m_txnLocalID
						})
					});
				}
				if (this.m_state == RMTxnState.Prepared_ToCommit)
				{
					if (ProviderConfig.m_bTraceLevelPrivate)
					{
						Trace.Write(OracleTraceLevel.Private, OracleTraceTag.MTS, new string[]
						{
							" MTSTxnRM.doPrepare: Prepared " + this.ToString() + "\tLocal Txn ID = " + this.m_txnLocalID
						});
					}
					e.Enlistment.Prepared();
				}
				else if (this.m_state == RMTxnState.Prepared_ReadOnly)
				{
					if (ProviderConfig.m_bTraceLevelPrivate)
					{
						Trace.Write(OracleTraceLevel.Private, OracleTraceTag.MTS, new string[]
						{
							" MTSTxnRM.doPrepare: ReadOnly " + this.ToString() + "\tLocal Txn ID = " + this.m_txnLocalID
						});
					}
					e.Enlistment.Done();
				}
				else
				{
					if (ProviderConfig.m_bTraceLevelPrivate)
					{
						Trace.Write(OracleTraceLevel.Private, OracleTraceTag.MTS, new string[]
						{
							" MTSTxnRM.doPrepare: ForceRollback " + this.ToString() + "\tLocal Txn ID = " + this.m_txnLocalID
						});
					}
					this.m_state = RMTxnState.Prepared_Failed;
					e.Enlistment.ForceRollback();
					try
					{
						this.m_state = RMTxnState.RollingBack;
						this.m_state = this.doAbort();
					}
					catch (Exception ex2)
					{
						if (ProviderConfig.m_bTraceLevelPrivate)
						{
							Trace.Write(OracleTraceLevel.Private, OracleTraceTag.MTS, new string[]
							{
								" MTSTxnRM.doPrepare:Prepare Failed.  Aborted " + this.ToString() + Environment.NewLine + ex2.Message
							});
						}
						this.m_state = RMTxnState.Rollback_Failed;
					}
				}
			}
			catch (Exception ex3)
			{
				OracleException.HandleError(OracleTraceLevel.Private, OracleTraceTag.MTS, ex3, null);
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, OracleTraceTag.MTS, new string[]
					{
						string.Concat(new object[]
						{
							" MTSTxnRM.doPrepare()-Finally Block: State=",
							this.m_state,
							" MTSTxnRM ID=",
							this.ToString()
						})
					});
				}
				if (this.m_state == RMTxnState.Prepared_ReadOnly || this.m_state == RMTxnState.Prepared_Failed || this.m_state == RMTxnState.Rollback_Failed || this.m_state == RMTxnState.RollingBack)
				{
					this.UnRegisteringTxnEvent(this.m_sysTxn);
					try
					{
						this.ReleaseRM();
					}
					catch
					{
					}
				}
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)4608, new string[]
					{
						string.Concat(new object[]
						{
							"MTSTxnRM : ",
							this.m_RMGuid,
							"\tRMState = ",
							this.m_state,
							": ",
							this.ToString(),
							"\tLocal Txn ID = ",
							this.m_txnLocalID
						})
					});
				}
			}
		}

		// Token: 0x06000C49 RID: 3145 RVA: 0x00089E60 File Offset: 0x00088060
		private void doCommit(object sender, OnCommitEventArgs e)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)4352, new string[]
				{
					string.Concat(new object[]
					{
						"RMState = ",
						this.m_state,
						": ",
						this.ToString(),
						"\tLocal Txn ID = ",
						this.m_txnLocalID
					})
				});
			}
			try
			{
				this.m_RMWorker.CommitEvent -= this.doCommit;
				RMTxnState rmtxnState = this.doCommit();
				if (rmtxnState == RMTxnState.Committed)
				{
					if (ProviderConfig.m_bTraceLevelPrivate)
					{
						Trace.Write(OracleTraceLevel.Private, OracleTraceTag.MTS, new string[]
						{
							" MTSTxnRM.doCommit: Committed. TxnID = " + this.ToString() + "\tLocal Txn ID = " + this.m_txnLocalID
						});
					}
					this.m_state = RMTxnState.Committed;
					e.Enlistment.Done();
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
				this.m_state = RMTxnState.Commit_Failed;
			}
			finally
			{
				this.UnRegisteringTxnEvent(this.m_sysTxn);
				try
				{
					this.ReleaseRM();
				}
				catch
				{
				}
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)4608, new string[]
					{
						string.Concat(new object[]
						{
							"RMState = ",
							this.m_state,
							" : ",
							this.ToString(),
							"\tLocal Txn ID = ",
							this.m_txnLocalID
						})
					});
				}
			}
		}

		// Token: 0x06000C4A RID: 3146 RVA: 0x0008A020 File Offset: 0x00088220
		private void doAbort(object sender, OnAbortEventArgs e)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)4352, new string[]
				{
					string.Concat(new object[]
					{
						"MTSTxnRM : ",
						this.m_RMGuid,
						"\tLocal Txn ID = ",
						this.m_txnLocalID
					})
				});
			}
			try
			{
				this.m_RMWorker.AbortEvent -= this.doAbort;
				this.m_state = RMTxnState.RollingBack;
				this.doAbort();
				this.m_state = RMTxnState.RollingBack;
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
				this.m_state = RMTxnState.Rollback_Failed;
			}
			finally
			{
				this.UnRegisteringTxnEvent(this.m_sysTxn);
				try
				{
					this.ReleaseRM();
				}
				catch
				{
				}
				try
				{
					e.Enlistment.Done();
				}
				catch
				{
				}
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)4608, new string[]
					{
						string.Concat(new object[]
						{
							"RMState = ",
							this.m_state,
							": ",
							this.ToString(),
							"\tLocal Txn ID = ",
							this.m_txnLocalID
						})
					});
				}
			}
		}

		// Token: 0x06000C4B RID: 3147 RVA: 0x0008A1A4 File Offset: 0x000883A4
		internal void EnlistToSysTransaction()
		{
			if (ProviderConfig.m_bTraceLevelPrivate_NoTrace)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)4352, new string[]
				{
					string.Concat(new object[]
					{
						"MTSTxnRM : ",
						this.m_RMGuid,
						" RMState = ",
						this.m_state
					})
				});
			}
			try
			{
				if (this.m_enlistedState == EnlistedState.Local)
				{
					lock (this.m_lock)
					{
						if (this.m_enlistedState == EnlistedState.Local)
						{
							this.m_sysTxn.EnlistDurable(this.m_RMGuid, this, EnlistmentOptions.None);
							this.m_enlistedState = EnlistedState.Distributed;
						}
					}
				}
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, OracleTraceTag.MTS, ex, null);
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate_NoTrace)
				{
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)4608, new string[]
					{
						string.Concat(new object[]
						{
							"MTSTxnRM : ",
							this.m_RMGuid,
							" RMState = ",
							this.m_state
						})
					});
				}
			}
		}

		// Token: 0x06000C4C RID: 3148 RVA: 0x0008A2F0 File Offset: 0x000884F0
		internal TxnBranchesByDBInst GetFreeBranches(ConnectionString cs)
		{
			return this.m_freeTxnBranches.m_freeBranchesByUserAuth[cs.UserAuthenticationString];
		}

		// Token: 0x06000C4D RID: 3149 RVA: 0x0008A308 File Offset: 0x00088508
		internal MTSTxnBranch GetTxnBranch(ConnectionString cs, string dbInst)
		{
			MTSTxnBranch result = null;
			if (this.m_NotUsedBranches.Dequeue(out result))
			{
				return result;
			}
			if (!this.m_freeTxnBranches.DequeueBranch(cs, dbInst, out result))
			{
				lock (this.m_branchLock)
				{
					if (this.m_branchNum > MTSTxnRM.MaxNumOfBranches)
					{
						if (this.m_freeTxnBranches.Count != 0)
						{
							return null;
						}
						if (ProviderConfig.m_bTraceLevelPrivate)
						{
							Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)4352, new string[]
							{
								Trace.GetCPInfo(null, null, null, "UnableToEnlist4", false, false) + " max num of branches reached"
							});
						}
						throw new OracleException(ResourceStringConstants.CON_MTS_ENLIST_FAIL, string.Empty, string.Empty, OracleStringResourceManager.GetErrorMesg(ResourceStringConstants.CON_MTS_ENLIST_FAIL, new string[0]));
					}
					else
					{
						result = MTSTxnBranch.CreateTxnBranch(this.m_bIsCCP, this, this.m_branchNum);
						this.m_branchNum++;
					}
				}
				return result;
			}
			return result;
		}

		// Token: 0x06000C4E RID: 3150 RVA: 0x0008A40C File Offset: 0x0008860C
		internal void ReleaseTxnBranch(MTSTxnBranch txnBranch)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, OracleTraceTag.MTS, new string[]
				{
					string.Concat(new object[]
					{
						"MTSTxnRM : ",
						this.m_RMGuid,
						"\t TxnID = ",
						txnBranch.TxnID
					})
				});
			}
			if (txnBranch.m_connCreds == null)
			{
				this.m_NotUsedBranches.Enqueue(txnBranch);
				return;
			}
			this.m_freeTxnBranches.EnqueueBranch(txnBranch);
		}

		// Token: 0x06000C4F RID: 3151 RVA: 0x0008A48C File Offset: 0x0008868C
		internal void FreeTxnBranch(MTSTxnBranch txnBranch)
		{
			if (ProviderConfig.m_bTraceLevelPrivate_NoTrace)
			{
				Trace.Write(OracleTraceLevel.Private, OracleTraceTag.MTS, new string[]
				{
					string.Concat(new object[]
					{
						"MTSTxnRM : ",
						this.m_RMGuid,
						"\t TxnID = ",
						txnBranch.TxnID
					})
				});
			}
			txnBranch.m_bNew = false;
			this.m_freeTxnBranches.EnqueueBranch(txnBranch);
		}

		// Token: 0x06000C50 RID: 3152 RVA: 0x0008A500 File Offset: 0x00088700
		internal void AddBranch(OracleConnectionImpl connImpl, MTSTxnBranch txnBranch, Guid sysTxnXID)
		{
			if (ProviderConfig.m_bTraceLevelPrivate_NoTrace)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)4352, new string[]
				{
					string.Concat(new object[]
					{
						"MTSTxnRM : ",
						this.m_RMGuid,
						"\t TxnID = ",
						txnBranch.TxnID
					})
				});
			}
			try
			{
				this.m_txnBranches.AddIfNotExist(txnBranch);
				this.m_connStrs.AddIfNotExist(connImpl.m_cs);
				this.m_state = RMTxnState.Enlisted;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate_NoTrace)
				{
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)4608, new string[]
					{
						string.Concat(new object[]
						{
							"MTSTxnRM : ",
							this.m_RMGuid,
							"\t TxnID = ",
							txnBranch.TxnID
						})
					});
				}
			}
		}

		// Token: 0x06000C51 RID: 3153 RVA: 0x0008A5E8 File Offset: 0x000887E8
		internal void DetachBranches()
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)4352, new string[]
				{
					string.Concat(new object[]
					{
						"MTSTxnRM : ",
						this.m_RMGuid,
						"\tRMState = ",
						this.m_state,
						": ",
						this.ToString()
					})
				});
			}
			try
			{
				for (int i = 0; i < this.m_txnBranches.Count; i++)
				{
					MTSTxnBranch mtstxnBranch = this.m_txnBranches[i];
					if (mtstxnBranch != null && mtstxnBranch.State == TxnBranchState.InUse)
					{
						try
						{
							mtstxnBranch.Detach();
						}
						catch (Exception ex)
						{
							OracleException.HandleError(OracleTraceLevel.Private, OracleTraceTag.MTS, ex, null);
						}
					}
				}
			}
			finally
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)4608, new string[]
				{
					string.Concat(new object[]
					{
						"MTSTxnRM : ",
						this.m_RMGuid,
						"\tRMState = ",
						this.m_state,
						": ",
						this.ToString()
					})
				});
			}
		}

		// Token: 0x06000C52 RID: 3154 RVA: 0x0008A738 File Offset: 0x00088938
		public override string ToString()
		{
			return string.Format("MTSTxnRM ID = {0}=={1}", this.m_sysTxn.TransactionInformation.DistributedIdentifier, this.m_RMGuid);
		}

		// Token: 0x04000D45 RID: 3397
		internal static int MaxNumOfBranches = 32;

		// Token: 0x04000D46 RID: 3398
		internal Guid m_RMGuid = Guid.NewGuid();

		// Token: 0x04000D47 RID: 3399
		internal Transaction m_sysTxn;

		// Token: 0x04000D48 RID: 3400
		internal string m_txnLocalID = string.Empty;

		// Token: 0x04000D49 RID: 3401
		internal SyncQueueList<ConnectionString> m_connStrs = new SyncQueueList<ConnectionString>(int.MaxValue);

		// Token: 0x04000D4A RID: 3402
		internal string m_serviceName;

		// Token: 0x04000D4B RID: 3403
		internal string m_pdbName;

		// Token: 0x04000D4C RID: 3404
		internal string m_dbEasyConnectName = string.Empty;

		// Token: 0x04000D4D RID: 3405
		internal string m_txnAffInstanceName = string.Empty;

		// Token: 0x04000D4E RID: 3406
		protected RMTxnState m_state;

		// Token: 0x04000D4F RID: 3407
		internal EnlistedState m_enlistedState = EnlistedState.Local;

		// Token: 0x04000D50 RID: 3408
		internal bool m_bIgnoreIsolationLvl;

		// Token: 0x04000D51 RID: 3409
		internal int m_branchNum = 1;

		// Token: 0x04000D52 RID: 3410
		private MTSTxnBranches m_txnBranches = new MTSTxnBranches();

		// Token: 0x04000D53 RID: 3411
		private MTSTxnBranches m_NotUsedBranches = new MTSTxnBranches();

		// Token: 0x04000D54 RID: 3412
		private MTSFreeTxnBranches m_freeTxnBranches = new MTSFreeTxnBranches();

		// Token: 0x04000D55 RID: 3413
		private MTSTxnRMWorker m_RMWorker;

		// Token: 0x04000D56 RID: 3414
		private object m_lock = new object();

		// Token: 0x04000D57 RID: 3415
		private object m_branchLock = new object();

		// Token: 0x04000D58 RID: 3416
		internal object m_txnAffinityLock = new object();

		// Token: 0x04000D59 RID: 3417
		internal bool m_bIsCCP = true;

		// Token: 0x0200011E RID: 286
		internal class TxnBranchesByUserAuth : TxnBranchesByString
		{
		}
	}
}
