using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Transactions;

namespace System.ServiceModel.ComIntegration
{
	// Token: 0x02000272 RID: 626
	internal class TransactionProxy : ITransactionProxy, IExtension<InstanceContext>
	{
		// Token: 0x060011C5 RID: 4549 RVA: 0x00040727 File Offset: 0x0003E927
		public TransactionProxy(Guid appid, Guid clsid)
		{
			this.syncRoot = new object();
			this.appid = appid;
			this.clsid = clsid;
		}

		// Token: 0x170003F4 RID: 1012
		// (get) Token: 0x060011C6 RID: 4550 RVA: 0x00040748 File Offset: 0x0003E948
		public Transaction CurrentTransaction
		{
			get
			{
				return this.currentTransaction;
			}
		}

		// Token: 0x170003F5 RID: 1013
		// (get) Token: 0x060011C7 RID: 4551 RVA: 0x00040750 File Offset: 0x0003E950
		public Guid AppId
		{
			get
			{
				return this.appid;
			}
		}

		// Token: 0x170003F6 RID: 1014
		// (get) Token: 0x060011C8 RID: 4552 RVA: 0x00040758 File Offset: 0x0003E958
		public Guid Clsid
		{
			get
			{
				return this.clsid;
			}
		}

		// Token: 0x170003F7 RID: 1015
		// (get) Token: 0x060011C9 RID: 4553 RVA: 0x00040760 File Offset: 0x0003E960
		// (set) Token: 0x060011CA RID: 4554 RVA: 0x00040768 File Offset: 0x0003E968
		public int InstanceID
		{
			get
			{
				return this.instanceID;
			}
			set
			{
				this.instanceID = value;
			}
		}

		// Token: 0x060011CB RID: 4555 RVA: 0x00040774 File Offset: 0x0003E974
		public void SetTransaction(Transaction transaction)
		{
			object obj = this.syncRoot;
			lock (obj)
			{
				if (transaction == null)
				{
					DiagnosticUtility.FailFast("Attempting to set transaction to NULL");
				}
				if (this.currentTransaction == null)
				{
					TransactionProxy.ProxyEnlistment enlistmentNotification = new TransactionProxy.ProxyEnlistment(this, transaction);
					transaction.EnlistVolatile(enlistmentNotification, EnlistmentOptions.None);
					this.currentTransaction = transaction;
					if (this.currentVoter != null)
					{
						this.currentVoter.SetTransaction(this.currentTransaction);
					}
				}
				else if (this.currentTransaction != transaction)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(Error.TransactionMismatch());
				}
			}
		}

		// Token: 0x060011CC RID: 4556 RVA: 0x00040824 File Offset: 0x0003EA24
		public void Attach(InstanceContext owner)
		{
		}

		// Token: 0x060011CD RID: 4557 RVA: 0x00040826 File Offset: 0x0003EA26
		public void Detach(InstanceContext owner)
		{
		}

		// Token: 0x060011CE RID: 4558 RVA: 0x00040828 File Offset: 0x0003EA28
		public void Commit(Guid guid)
		{
			DiagnosticUtility.FailFast("Commit not supported: BYOT only!");
		}

		// Token: 0x060011CF RID: 4559 RVA: 0x00040835 File Offset: 0x0003EA35
		public void Abort()
		{
			if (this.currentTransaction != null)
			{
				this.currentTransaction.Rollback();
			}
		}

		// Token: 0x060011D0 RID: 4560 RVA: 0x00040850 File Offset: 0x0003EA50
		public IDtcTransaction Promote()
		{
			this.EnsureTransaction();
			return TransactionInterop.GetDtcTransaction(this.currentTransaction);
		}

		// Token: 0x060011D1 RID: 4561 RVA: 0x00040864 File Offset: 0x0003EA64
		public void CreateVoter(ITransactionVoterNotifyAsync2 voterNotification, IntPtr voterBallot)
		{
			if (IntPtr.Zero == voterBallot)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("voterBallot");
			}
			object obj = this.syncRoot;
			lock (obj)
			{
				if (this.currentVoter != null)
				{
					DiagnosticUtility.FailFast("Assumption: proxy only needs one voter");
				}
				TransactionProxy.VoterBallot voterBallot2 = new TransactionProxy.VoterBallot(voterNotification, this);
				if (this.currentTransaction != null)
				{
					voterBallot2.SetTransaction(this.currentTransaction);
				}
				this.currentVoter = voterBallot2;
				IntPtr interfacePtrForObject = InterfaceHelper.GetInterfacePtrForObject(typeof(ITransactionVoterBallotAsync2).GUID, this.currentVoter);
				Marshal.WriteIntPtr(voterBallot, interfacePtrForObject);
			}
		}

		// Token: 0x060011D2 RID: 4562 RVA: 0x0004091C File Offset: 0x0003EB1C
		public DtcIsolationLevel GetIsolationLevel()
		{
			DtcIsolationLevel result;
			switch (this.currentTransaction.IsolationLevel)
			{
			case IsolationLevel.Serializable:
				result = DtcIsolationLevel.ISOLATIONLEVEL_SERIALIZABLE;
				break;
			case IsolationLevel.RepeatableRead:
				result = DtcIsolationLevel.ISOLATIONLEVEL_REPEATABLEREAD;
				break;
			case IsolationLevel.ReadCommitted:
				result = DtcIsolationLevel.ISOLATIONLEVEL_CURSORSTABILITY;
				break;
			case IsolationLevel.ReadUncommitted:
				result = DtcIsolationLevel.ISOLATIONLEVEL_READUNCOMMITTED;
				break;
			default:
				result = DtcIsolationLevel.ISOLATIONLEVEL_SERIALIZABLE;
				break;
			}
			return result;
		}

		// Token: 0x060011D3 RID: 4563 RVA: 0x00040974 File Offset: 0x0003EB74
		public Guid GetIdentifier()
		{
			return this.currentTransaction.TransactionInformation.DistributedIdentifier;
		}

		// Token: 0x060011D4 RID: 4564 RVA: 0x00040986 File Offset: 0x0003EB86
		public bool IsReusable()
		{
			return true;
		}

		// Token: 0x060011D5 RID: 4565 RVA: 0x0004098C File Offset: 0x0003EB8C
		private void ClearTransaction(TransactionProxy.ProxyEnlistment enlistment)
		{
			object obj = this.syncRoot;
			lock (obj)
			{
				if (this.currentTransaction == null)
				{
					DiagnosticUtility.FailFast("Clearing inactive TransactionProxy");
				}
				if (enlistment.Transaction != this.currentTransaction)
				{
					DiagnosticUtility.FailFast("Incorrectly working on multiple transactions");
				}
				this.currentTransaction = null;
				this.currentVoter = null;
			}
		}

		// Token: 0x060011D6 RID: 4566 RVA: 0x00040A0C File Offset: 0x0003EC0C
		private void EnsureTransaction()
		{
			object obj = this.syncRoot;
			lock (obj)
			{
				if (this.currentTransaction == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new COMException(null, HR.CONTEXT_E_NOTRANSACTION));
				}
			}
		}

		// Token: 0x040019AA RID: 6570
		private Transaction currentTransaction;

		// Token: 0x040019AB RID: 6571
		private TransactionProxy.VoterBallot currentVoter;

		// Token: 0x040019AC RID: 6572
		private object syncRoot;

		// Token: 0x040019AD RID: 6573
		private Guid appid;

		// Token: 0x040019AE RID: 6574
		private Guid clsid;

		// Token: 0x040019AF RID: 6575
		private int instanceID;

		// Token: 0x02000B14 RID: 2836
		private class ProxyEnlistment : IEnlistmentNotification
		{
			// Token: 0x06006F7B RID: 28539 RVA: 0x0019DE87 File Offset: 0x0019C087
			public ProxyEnlistment(TransactionProxy proxy, Transaction transaction)
			{
				this.proxy = proxy;
				this.transaction = transaction;
			}

			// Token: 0x170019FF RID: 6655
			// (get) Token: 0x06006F7C RID: 28540 RVA: 0x0019DE9D File Offset: 0x0019C09D
			public Transaction Transaction
			{
				get
				{
					return this.transaction;
				}
			}

			// Token: 0x06006F7D RID: 28541 RVA: 0x0019DEA5 File Offset: 0x0019C0A5
			public void Prepare(PreparingEnlistment preparingEnlistment)
			{
				this.proxy.ClearTransaction(this);
				this.proxy = null;
				preparingEnlistment.Done();
			}

			// Token: 0x06006F7E RID: 28542 RVA: 0x0019DEC0 File Offset: 0x0019C0C0
			public void Commit(Enlistment enlistment)
			{
				DiagnosticUtility.FailFast("Should have voted read only");
			}

			// Token: 0x06006F7F RID: 28543 RVA: 0x0019DECD File Offset: 0x0019C0CD
			public void Rollback(Enlistment enlistment)
			{
				this.proxy.ClearTransaction(this);
				this.proxy = null;
				enlistment.Done();
			}

			// Token: 0x06006F80 RID: 28544 RVA: 0x0019DEE8 File Offset: 0x0019C0E8
			public void InDoubt(Enlistment enlistment)
			{
				DiagnosticUtility.FailFast("Should have voted read only");
			}

			// Token: 0x04003FBF RID: 16319
			private TransactionProxy proxy;

			// Token: 0x04003FC0 RID: 16320
			private Transaction transaction;
		}

		// Token: 0x02000B15 RID: 2837
		private class VoterBallot : ITransactionVoterBallotAsync2, IEnlistmentNotification
		{
			// Token: 0x06006F81 RID: 28545 RVA: 0x0019DEF5 File Offset: 0x0019C0F5
			public VoterBallot(ITransactionVoterNotifyAsync2 notification, TransactionProxy proxy)
			{
				this.notification = notification;
				this.proxy = proxy;
			}

			// Token: 0x06006F82 RID: 28546 RVA: 0x0019DF0B File Offset: 0x0019C10B
			public void SetTransaction(Transaction transaction)
			{
				if (this.transaction != null)
				{
					DiagnosticUtility.FailFast("Already have a transaction in the ballot!");
				}
				this.transaction = transaction;
				this.enlistment = transaction.EnlistVolatile(this, EnlistmentOptions.None);
			}

			// Token: 0x06006F83 RID: 28547 RVA: 0x0019DF3B File Offset: 0x0019C13B
			public void Prepare(PreparingEnlistment enlistment)
			{
				this.preparingEnlistment = enlistment;
				this.notification.VoteRequest();
			}

			// Token: 0x06006F84 RID: 28548 RVA: 0x0019DF50 File Offset: 0x0019C150
			public void Rollback(Enlistment enlistment)
			{
				enlistment.Done();
				this.notification.Aborted(0, false, 0, 0);
				ComPlusTxProxyTrace.Trace(TraceEventType.Verbose, 327715, "TraceCodeComIntegrationTxProxyTxAbortedByTM", this.proxy.AppId, this.proxy.Clsid, this.transaction.TransactionInformation.DistributedIdentifier, this.proxy.InstanceID);
				Marshal.ReleaseComObject(this.notification);
				this.notification = null;
			}

			// Token: 0x06006F85 RID: 28549 RVA: 0x0019DFC8 File Offset: 0x0019C1C8
			public void Commit(Enlistment enlistment)
			{
				enlistment.Done();
				this.notification.Committed(false, 0, 0);
				ComPlusTxProxyTrace.Trace(TraceEventType.Verbose, 327713, "TraceCodeComIntegrationTxProxyTxCommitted", this.proxy.AppId, this.proxy.Clsid, this.transaction.TransactionInformation.DistributedIdentifier, this.proxy.InstanceID);
				Marshal.ReleaseComObject(this.notification);
				this.notification = null;
			}

			// Token: 0x06006F86 RID: 28550 RVA: 0x0019E03E File Offset: 0x0019C23E
			public void InDoubt(Enlistment enlistment)
			{
				enlistment.Done();
				this.notification.InDoubt();
				Marshal.ReleaseComObject(this.notification);
				this.notification = null;
			}

			// Token: 0x06006F87 RID: 28551 RVA: 0x0019E064 File Offset: 0x0019C264
			public void VoteRequestDone(int hr, int reason)
			{
				if (this.preparingEnlistment == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("NoVoteIssued")));
				}
				if (hr == 0)
				{
					this.preparingEnlistment.Prepared();
					return;
				}
				this.preparingEnlistment.ForceRollback();
				ComPlusTxProxyTrace.Trace(TraceEventType.Verbose, 327714, "TraceCodeComIntegrationTxProxyTxAbortedByContext", this.proxy.AppId, this.proxy.Clsid, this.transaction.TransactionInformation.DistributedIdentifier, this.proxy.InstanceID);
			}

			// Token: 0x04003FC1 RID: 16321
			private const int S_OK = 0;

			// Token: 0x04003FC2 RID: 16322
			private ITransactionVoterNotifyAsync2 notification;

			// Token: 0x04003FC3 RID: 16323
			private Transaction transaction;

			// Token: 0x04003FC4 RID: 16324
			private Enlistment enlistment;

			// Token: 0x04003FC5 RID: 16325
			private PreparingEnlistment preparingEnlistment;

			// Token: 0x04003FC6 RID: 16326
			private TransactionProxy proxy;
		}
	}
}
