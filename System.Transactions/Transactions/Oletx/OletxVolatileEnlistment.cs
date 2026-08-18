using System;
using System.Threading;
using System.Transactions.Diagnostics;

namespace System.Transactions.Oletx
{
	// Token: 0x02000098 RID: 152
	internal class OletxVolatileEnlistment : OletxBaseEnlistment, IPromotedEnlistment
	{
		// Token: 0x0600041D RID: 1053 RVA: 0x0003DD94 File Offset: 0x0003D194
		internal OletxVolatileEnlistment(IEnlistmentNotificationInternal enlistmentNotification, EnlistmentOptions enlistmentOptions, OletxTransaction oletxTransaction) : base(null, oletxTransaction)
		{
			this.iEnlistmentNotification = enlistmentNotification;
			this.enlistDuringPrepareRequired = ((enlistmentOptions & EnlistmentOptions.EnlistDuringPrepareRequired) != EnlistmentOptions.None);
			this.container = null;
			this.pendingOutcome = TransactionStatus.Active;
			if (DiagnosticTrace.Information)
			{
				EnlistmentTraceRecord.Trace(SR.GetString("TraceSourceOletx"), base.InternalTraceIdentifier, EnlistmentType.Volatile, enlistmentOptions);
			}
		}

		// Token: 0x0600041E RID: 1054 RVA: 0x0003DDF4 File Offset: 0x0003D1F4
		internal void Prepare(OletxVolatileEnlistmentContainer container)
		{
			OletxVolatileEnlistment.OletxVolatileEnlistmentState oletxVolatileEnlistmentState = OletxVolatileEnlistment.OletxVolatileEnlistmentState.Active;
			IEnlistmentNotificationInternal enlistmentNotificationInternal = null;
			lock (this)
			{
				enlistmentNotificationInternal = this.iEnlistmentNotification;
				if (this.state == OletxVolatileEnlistment.OletxVolatileEnlistmentState.Active)
				{
					oletxVolatileEnlistmentState = (this.state = OletxVolatileEnlistment.OletxVolatileEnlistmentState.Preparing);
				}
				else
				{
					oletxVolatileEnlistmentState = this.state;
				}
				this.container = container;
			}
			if (OletxVolatileEnlistment.OletxVolatileEnlistmentState.Preparing == oletxVolatileEnlistmentState)
			{
				if (enlistmentNotificationInternal != null)
				{
					if (DiagnosticTrace.Verbose)
					{
						EnlistmentNotificationCallTraceRecord.Trace(SR.GetString("TraceSourceOletx"), base.InternalTraceIdentifier, NotificationCall.Prepare);
					}
					enlistmentNotificationInternal.Prepare(this);
					return;
				}
				if (DiagnosticTrace.Critical)
				{
					InternalErrorTraceRecord.Trace(SR.GetString("TraceSourceOletx"), "");
				}
				throw new InvalidOperationException(SR.GetString("InternalError"));
			}
			else
			{
				if (OletxVolatileEnlistment.OletxVolatileEnlistmentState.Done == oletxVolatileEnlistmentState)
				{
					container.DecrementOutstandingNotifications(true);
					return;
				}
				if (OletxVolatileEnlistment.OletxVolatileEnlistmentState.Prepared == oletxVolatileEnlistmentState && this.enlistDuringPrepareRequired)
				{
					container.DecrementOutstandingNotifications(true);
					return;
				}
				if (OletxVolatileEnlistment.OletxVolatileEnlistmentState.Aborting == oletxVolatileEnlistmentState || OletxVolatileEnlistment.OletxVolatileEnlistmentState.Aborted == oletxVolatileEnlistmentState)
				{
					container.DecrementOutstandingNotifications(false);
					return;
				}
				if (DiagnosticTrace.Critical)
				{
					InternalErrorTraceRecord.Trace(SR.GetString("TraceSourceOletx"), "");
				}
				throw new InvalidOperationException(SR.GetString("InternalError"));
			}
		}

		// Token: 0x0600041F RID: 1055 RVA: 0x0003DF14 File Offset: 0x0003D314
		internal void Commit()
		{
			OletxVolatileEnlistment.OletxVolatileEnlistmentState oletxVolatileEnlistmentState = OletxVolatileEnlistment.OletxVolatileEnlistmentState.Active;
			IEnlistmentNotificationInternal enlistmentNotificationInternal = null;
			lock (this)
			{
				if (OletxVolatileEnlistment.OletxVolatileEnlistmentState.Prepared == this.state)
				{
					oletxVolatileEnlistmentState = (this.state = OletxVolatileEnlistment.OletxVolatileEnlistmentState.Committing);
					enlistmentNotificationInternal = this.iEnlistmentNotification;
				}
				else
				{
					oletxVolatileEnlistmentState = this.state;
				}
			}
			if (OletxVolatileEnlistment.OletxVolatileEnlistmentState.Committing == oletxVolatileEnlistmentState)
			{
				if (enlistmentNotificationInternal != null)
				{
					if (DiagnosticTrace.Verbose)
					{
						EnlistmentNotificationCallTraceRecord.Trace(SR.GetString("TraceSourceOletx"), base.InternalTraceIdentifier, NotificationCall.Commit);
					}
					enlistmentNotificationInternal.Commit(this);
					return;
				}
				if (DiagnosticTrace.Critical)
				{
					InternalErrorTraceRecord.Trace(SR.GetString("TraceSourceOletx"), "");
				}
				throw new InvalidOperationException(SR.GetString("InternalError"));
			}
			else
			{
				if (OletxVolatileEnlistment.OletxVolatileEnlistmentState.Done == oletxVolatileEnlistmentState)
				{
					return;
				}
				if (DiagnosticTrace.Critical)
				{
					InternalErrorTraceRecord.Trace(SR.GetString("TraceSourceOletx"), "");
				}
				throw new InvalidOperationException(SR.GetString("InternalError"));
			}
		}

		// Token: 0x06000420 RID: 1056 RVA: 0x0003E004 File Offset: 0x0003D404
		internal void Rollback()
		{
			OletxVolatileEnlistment.OletxVolatileEnlistmentState oletxVolatileEnlistmentState = OletxVolatileEnlistment.OletxVolatileEnlistmentState.Active;
			IEnlistmentNotificationInternal enlistmentNotificationInternal = null;
			lock (this)
			{
				if (OletxVolatileEnlistment.OletxVolatileEnlistmentState.Prepared == this.state || this.state == OletxVolatileEnlistment.OletxVolatileEnlistmentState.Active)
				{
					oletxVolatileEnlistmentState = (this.state = OletxVolatileEnlistment.OletxVolatileEnlistmentState.Aborting);
					enlistmentNotificationInternal = this.iEnlistmentNotification;
				}
				else
				{
					if (OletxVolatileEnlistment.OletxVolatileEnlistmentState.Preparing == this.state)
					{
						this.pendingOutcome = TransactionStatus.Aborted;
					}
					oletxVolatileEnlistmentState = this.state;
				}
			}
			if (OletxVolatileEnlistment.OletxVolatileEnlistmentState.Aborting == oletxVolatileEnlistmentState)
			{
				if (enlistmentNotificationInternal != null)
				{
					if (DiagnosticTrace.Verbose)
					{
						EnlistmentNotificationCallTraceRecord.Trace(SR.GetString("TraceSourceOletx"), base.InternalTraceIdentifier, NotificationCall.Rollback);
					}
					enlistmentNotificationInternal.Rollback(this);
					return;
				}
				return;
			}
			else
			{
				if (OletxVolatileEnlistment.OletxVolatileEnlistmentState.Preparing == oletxVolatileEnlistmentState)
				{
					return;
				}
				if (OletxVolatileEnlistment.OletxVolatileEnlistmentState.Done == oletxVolatileEnlistmentState)
				{
					return;
				}
				if (DiagnosticTrace.Critical)
				{
					InternalErrorTraceRecord.Trace(SR.GetString("TraceSourceOletx"), "");
				}
				throw new InvalidOperationException(SR.GetString("InternalError"));
			}
		}

		// Token: 0x06000421 RID: 1057 RVA: 0x0003E0E4 File Offset: 0x0003D4E4
		internal void InDoubt()
		{
			OletxVolatileEnlistment.OletxVolatileEnlistmentState oletxVolatileEnlistmentState = OletxVolatileEnlistment.OletxVolatileEnlistmentState.Active;
			IEnlistmentNotificationInternal enlistmentNotificationInternal = null;
			lock (this)
			{
				if (OletxVolatileEnlistment.OletxVolatileEnlistmentState.Prepared == this.state)
				{
					oletxVolatileEnlistmentState = (this.state = OletxVolatileEnlistment.OletxVolatileEnlistmentState.InDoubt);
					enlistmentNotificationInternal = this.iEnlistmentNotification;
				}
				else
				{
					if (OletxVolatileEnlistment.OletxVolatileEnlistmentState.Preparing == this.state)
					{
						this.pendingOutcome = TransactionStatus.InDoubt;
					}
					oletxVolatileEnlistmentState = this.state;
				}
			}
			if (OletxVolatileEnlistment.OletxVolatileEnlistmentState.InDoubt == oletxVolatileEnlistmentState)
			{
				if (enlistmentNotificationInternal != null)
				{
					if (DiagnosticTrace.Verbose)
					{
						EnlistmentNotificationCallTraceRecord.Trace(SR.GetString("TraceSourceOletx"), base.InternalTraceIdentifier, NotificationCall.InDoubt);
					}
					enlistmentNotificationInternal.InDoubt(this);
					return;
				}
				if (DiagnosticTrace.Critical)
				{
					InternalErrorTraceRecord.Trace(SR.GetString("TraceSourceOletx"), "");
				}
				throw new InvalidOperationException(SR.GetString("InternalError"));
			}
			else
			{
				if (OletxVolatileEnlistment.OletxVolatileEnlistmentState.Preparing == oletxVolatileEnlistmentState)
				{
					return;
				}
				if (OletxVolatileEnlistment.OletxVolatileEnlistmentState.Done == oletxVolatileEnlistmentState)
				{
					return;
				}
				if (DiagnosticTrace.Critical)
				{
					InternalErrorTraceRecord.Trace(SR.GetString("TraceSourceOletx"), "");
				}
				throw new InvalidOperationException(SR.GetString("InternalError"));
			}
		}

		// Token: 0x06000422 RID: 1058 RVA: 0x0003E1E4 File Offset: 0x0003D5E4
		void IPromotedEnlistment.EnlistmentDone()
		{
			if (DiagnosticTrace.Verbose)
			{
				MethodEnteredTraceRecord.Trace(SR.GetString("TraceSourceOletx"), "OletxEnlistment.EnlistmentDone");
				EnlistmentCallbackPositiveTraceRecord.Trace(SR.GetString("TraceSourceOletx"), base.InternalTraceIdentifier, EnlistmentCallback.Done);
			}
			OletxVolatileEnlistment.OletxVolatileEnlistmentState oletxVolatileEnlistmentState = OletxVolatileEnlistment.OletxVolatileEnlistmentState.Active;
			OletxVolatileEnlistmentContainer oletxVolatileEnlistmentContainer = null;
			lock (this)
			{
				oletxVolatileEnlistmentState = this.state;
				oletxVolatileEnlistmentContainer = this.container;
				if (this.state != OletxVolatileEnlistment.OletxVolatileEnlistmentState.Active && OletxVolatileEnlistment.OletxVolatileEnlistmentState.Preparing != this.state && OletxVolatileEnlistment.OletxVolatileEnlistmentState.Aborting != this.state && OletxVolatileEnlistment.OletxVolatileEnlistmentState.Committing != this.state && OletxVolatileEnlistment.OletxVolatileEnlistmentState.InDoubt != this.state)
				{
					throw TransactionException.CreateEnlistmentStateException(SR.GetString("TraceSourceOletx"), null);
				}
				this.state = OletxVolatileEnlistment.OletxVolatileEnlistmentState.Done;
			}
			if (OletxVolatileEnlistment.OletxVolatileEnlistmentState.Preparing == oletxVolatileEnlistmentState && oletxVolatileEnlistmentContainer != null)
			{
				oletxVolatileEnlistmentContainer.DecrementOutstandingNotifications(true);
			}
			if (DiagnosticTrace.Verbose)
			{
				MethodExitedTraceRecord.Trace(SR.GetString("TraceSourceOletx"), "OletxEnlistment.EnlistmentDone");
			}
		}

		// Token: 0x06000423 RID: 1059 RVA: 0x0003E2D4 File Offset: 0x0003D6D4
		void IPromotedEnlistment.Prepared()
		{
			if (DiagnosticTrace.Verbose)
			{
				MethodEnteredTraceRecord.Trace(SR.GetString("TraceSourceOletx"), "OletxPreparingEnlistment.Prepared");
				EnlistmentCallbackPositiveTraceRecord.Trace(SR.GetString("TraceSourceOletx"), base.InternalTraceIdentifier, EnlistmentCallback.Prepared);
			}
			OletxVolatileEnlistmentContainer oletxVolatileEnlistmentContainer = null;
			TransactionStatus transactionStatus = TransactionStatus.Active;
			lock (this)
			{
				if (OletxVolatileEnlistment.OletxVolatileEnlistmentState.Preparing != this.state)
				{
					throw TransactionException.CreateEnlistmentStateException(SR.GetString("TraceSourceOletx"), null);
				}
				this.state = OletxVolatileEnlistment.OletxVolatileEnlistmentState.Prepared;
				transactionStatus = this.pendingOutcome;
				if (this.container == null)
				{
					if (DiagnosticTrace.Critical)
					{
						InternalErrorTraceRecord.Trace(SR.GetString("TraceSourceOletx"), "");
					}
					throw new InvalidOperationException(SR.GetString("InternalError"));
				}
				oletxVolatileEnlistmentContainer = this.container;
			}
			oletxVolatileEnlistmentContainer.DecrementOutstandingNotifications(true);
			switch (transactionStatus)
			{
			case TransactionStatus.Active:
				goto IL_104;
			case TransactionStatus.Aborted:
				this.Rollback();
				goto IL_104;
			case TransactionStatus.InDoubt:
				this.InDoubt();
				goto IL_104;
			}
			if (DiagnosticTrace.Critical)
			{
				InternalErrorTraceRecord.Trace(SR.GetString("TraceSourceOletx"), "");
			}
			throw new InvalidOperationException(SR.GetString("InternalError"));
			IL_104:
			if (DiagnosticTrace.Verbose)
			{
				MethodExitedTraceRecord.Trace(SR.GetString("TraceSourceOletx"), "OletxPreparingEnlistment.Prepared");
			}
		}

		// Token: 0x06000424 RID: 1060 RVA: 0x0003E424 File Offset: 0x0003D824
		void IPromotedEnlistment.ForceRollback()
		{
			((IPromotedEnlistment)this).ForceRollback(null);
		}

		// Token: 0x06000425 RID: 1061 RVA: 0x0003E444 File Offset: 0x0003D844
		void IPromotedEnlistment.ForceRollback(Exception e)
		{
			if (DiagnosticTrace.Verbose)
			{
				MethodEnteredTraceRecord.Trace(SR.GetString("TraceSourceOletx"), "OletxPreparingEnlistment.ForceRollback");
			}
			if (DiagnosticTrace.Warning)
			{
				EnlistmentCallbackNegativeTraceRecord.Trace(SR.GetString("TraceSourceOletx"), base.InternalTraceIdentifier, EnlistmentCallback.ForceRollback);
			}
			OletxVolatileEnlistmentContainer oletxVolatileEnlistmentContainer = null;
			lock (this)
			{
				if (OletxVolatileEnlistment.OletxVolatileEnlistmentState.Preparing != this.state)
				{
					throw TransactionException.CreateEnlistmentStateException(SR.GetString("TraceSourceOletx"), null);
				}
				this.state = OletxVolatileEnlistment.OletxVolatileEnlistmentState.Done;
				if (this.container == null)
				{
					if (DiagnosticTrace.Critical)
					{
						InternalErrorTraceRecord.Trace(SR.GetString("TraceSourceOletx"), "");
					}
					throw new InvalidOperationException(SR.GetString("InternalError"));
				}
				oletxVolatileEnlistmentContainer = this.container;
			}
			Interlocked.CompareExchange<Exception>(ref this.oletxTransaction.realOletxTransaction.innerException, e, null);
			oletxVolatileEnlistmentContainer.DecrementOutstandingNotifications(false);
			if (DiagnosticTrace.Verbose)
			{
				MethodExitedTraceRecord.Trace(SR.GetString("TraceSourceOletx"), "OletxPreparingEnlistment.ForceRollback");
			}
		}

		// Token: 0x06000426 RID: 1062 RVA: 0x0003E554 File Offset: 0x0003D954
		void IPromotedEnlistment.Committed()
		{
			throw new InvalidOperationException();
		}

		// Token: 0x06000427 RID: 1063 RVA: 0x0003E574 File Offset: 0x0003D974
		void IPromotedEnlistment.Aborted()
		{
			throw new InvalidOperationException();
		}

		// Token: 0x06000428 RID: 1064 RVA: 0x0003E594 File Offset: 0x0003D994
		void IPromotedEnlistment.Aborted(Exception e)
		{
			throw new InvalidOperationException();
		}

		// Token: 0x06000429 RID: 1065 RVA: 0x0003E5B4 File Offset: 0x0003D9B4
		void IPromotedEnlistment.InDoubt()
		{
			throw new InvalidOperationException();
		}

		// Token: 0x0600042A RID: 1066 RVA: 0x0003E5D4 File Offset: 0x0003D9D4
		void IPromotedEnlistment.InDoubt(Exception e)
		{
			throw new InvalidOperationException();
		}

		// Token: 0x0600042B RID: 1067 RVA: 0x0003E5F4 File Offset: 0x0003D9F4
		byte[] IPromotedEnlistment.GetRecoveryInformation()
		{
			throw TransactionException.CreateInvalidOperationException(SR.GetString("TraceSourceOletx"), SR.GetString("VolEnlistNoRecoveryInfo"), null);
		}

		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x0600042C RID: 1068 RVA: 0x0003E624 File Offset: 0x0003DA24
		// (set) Token: 0x0600042D RID: 1069 RVA: 0x0003E644 File Offset: 0x0003DA44
		InternalEnlistment IPromotedEnlistment.InternalEnlistment
		{
			get
			{
				return this.internalEnlistment;
			}
			set
			{
				this.internalEnlistment = value;
			}
		}

		// Token: 0x04000237 RID: 567
		private IEnlistmentNotificationInternal iEnlistmentNotification;

		// Token: 0x04000238 RID: 568
		private OletxVolatileEnlistment.OletxVolatileEnlistmentState state;

		// Token: 0x04000239 RID: 569
		private OletxVolatileEnlistmentContainer container;

		// Token: 0x0400023A RID: 570
		internal bool enlistDuringPrepareRequired;

		// Token: 0x0400023B RID: 571
		private TransactionStatus pendingOutcome;

		// Token: 0x02000099 RID: 153
		private enum OletxVolatileEnlistmentState
		{
			// Token: 0x0400023D RID: 573
			Active,
			// Token: 0x0400023E RID: 574
			Preparing,
			// Token: 0x0400023F RID: 575
			Committing,
			// Token: 0x04000240 RID: 576
			Aborting,
			// Token: 0x04000241 RID: 577
			Prepared,
			// Token: 0x04000242 RID: 578
			Aborted,
			// Token: 0x04000243 RID: 579
			InDoubt,
			// Token: 0x04000244 RID: 580
			Done
		}
	}
}
