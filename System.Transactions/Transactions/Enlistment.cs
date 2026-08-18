using System;
using System.Transactions.Diagnostics;

namespace System.Transactions
{
	// Token: 0x0200003F RID: 63
	public class Enlistment
	{
		// Token: 0x060001DB RID: 475 RVA: 0x00030514 File Offset: 0x0002F914
		internal Enlistment(InternalEnlistment internalEnlistment)
		{
			this.internalEnlistment = internalEnlistment;
		}

		// Token: 0x060001DC RID: 476 RVA: 0x00030534 File Offset: 0x0002F934
		internal Enlistment(Guid resourceManagerIdentifier, InternalTransaction transaction, IEnlistmentNotification twoPhaseNotifications, ISinglePhaseNotification singlePhaseNotifications, Transaction atomicTransaction)
		{
			this.internalEnlistment = new DurableInternalEnlistment(this, resourceManagerIdentifier, transaction, twoPhaseNotifications, singlePhaseNotifications, atomicTransaction);
		}

		// Token: 0x060001DD RID: 477 RVA: 0x00030564 File Offset: 0x0002F964
		internal Enlistment(InternalTransaction transaction, IEnlistmentNotification twoPhaseNotifications, ISinglePhaseNotification singlePhaseNotifications, Transaction atomicTransaction, EnlistmentOptions enlistmentOptions)
		{
			if ((enlistmentOptions & EnlistmentOptions.EnlistDuringPrepareRequired) != EnlistmentOptions.None)
			{
				this.internalEnlistment = new InternalEnlistment(this, transaction, twoPhaseNotifications, singlePhaseNotifications, atomicTransaction);
				return;
			}
			this.internalEnlistment = new Phase1VolatileEnlistment(this, transaction, twoPhaseNotifications, singlePhaseNotifications, atomicTransaction);
		}

		// Token: 0x060001DE RID: 478 RVA: 0x000305A4 File Offset: 0x0002F9A4
		internal Enlistment(InternalTransaction transaction, IPromotableSinglePhaseNotification promotableSinglePhaseNotification, Transaction atomicTransaction)
		{
			this.internalEnlistment = new PromotableInternalEnlistment(this, transaction, promotableSinglePhaseNotification, atomicTransaction);
		}

		// Token: 0x060001DF RID: 479 RVA: 0x000305D4 File Offset: 0x0002F9D4
		internal Enlistment(IEnlistmentNotification twoPhaseNotifications, InternalTransaction transaction, Transaction atomicTransaction)
		{
			this.internalEnlistment = new InternalEnlistment(this, twoPhaseNotifications, transaction, atomicTransaction);
		}

		// Token: 0x060001E0 RID: 480 RVA: 0x00030604 File Offset: 0x0002FA04
		internal Enlistment(IEnlistmentNotification twoPhaseNotifications, object syncRoot)
		{
			this.internalEnlistment = new RecoveringInternalEnlistment(this, twoPhaseNotifications, syncRoot);
		}

		// Token: 0x060001E1 RID: 481 RVA: 0x00030634 File Offset: 0x0002FA34
		public void Done()
		{
			if (DiagnosticTrace.Verbose)
			{
				MethodEnteredTraceRecord.Trace(SR.GetString("TraceSourceLtm"), "Enlistment.Done");
				EnlistmentCallbackPositiveTraceRecord.Trace(SR.GetString("TraceSourceLtm"), this.internalEnlistment.EnlistmentTraceId, EnlistmentCallback.Done);
			}
			lock (this.internalEnlistment.SyncRoot)
			{
				this.internalEnlistment.State.EnlistmentDone(this.internalEnlistment);
			}
			if (DiagnosticTrace.Verbose)
			{
				MethodExitedTraceRecord.Trace(SR.GetString("TraceSourceLtm"), "Enlistment.Done");
			}
		}

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x060001E2 RID: 482 RVA: 0x000306E4 File Offset: 0x0002FAE4
		internal InternalEnlistment InternalEnlistment
		{
			get
			{
				return this.internalEnlistment;
			}
		}

		// Token: 0x040000F1 RID: 241
		internal InternalEnlistment internalEnlistment;
	}
}
