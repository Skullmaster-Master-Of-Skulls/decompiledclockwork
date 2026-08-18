using System;
using System.Transactions.Diagnostics;

namespace System.Transactions
{
	// Token: 0x02000041 RID: 65
	public class SinglePhaseEnlistment : Enlistment
	{
		// Token: 0x060001E8 RID: 488 RVA: 0x00030A04 File Offset: 0x0002FE04
		internal SinglePhaseEnlistment(InternalEnlistment enlistment) : base(enlistment)
		{
		}

		// Token: 0x060001E9 RID: 489 RVA: 0x00030A24 File Offset: 0x0002FE24
		public void Aborted()
		{
			if (DiagnosticTrace.Verbose)
			{
				MethodEnteredTraceRecord.Trace(SR.GetString("TraceSourceLtm"), "SinglePhaseEnlistment.Aborted");
			}
			if (DiagnosticTrace.Warning)
			{
				EnlistmentCallbackNegativeTraceRecord.Trace(SR.GetString("TraceSourceLtm"), this.internalEnlistment.EnlistmentTraceId, EnlistmentCallback.Aborted);
			}
			lock (this.internalEnlistment.SyncRoot)
			{
				this.internalEnlistment.State.Aborted(this.internalEnlistment, null);
			}
			if (DiagnosticTrace.Verbose)
			{
				MethodExitedTraceRecord.Trace(SR.GetString("TraceSourceLtm"), "SinglePhaseEnlistment.Aborted");
			}
		}

		// Token: 0x060001EA RID: 490 RVA: 0x00030AE4 File Offset: 0x0002FEE4
		public void Aborted(Exception e)
		{
			if (DiagnosticTrace.Verbose)
			{
				MethodEnteredTraceRecord.Trace(SR.GetString("TraceSourceLtm"), "SinglePhaseEnlistment.Aborted");
			}
			if (DiagnosticTrace.Warning)
			{
				EnlistmentCallbackNegativeTraceRecord.Trace(SR.GetString("TraceSourceLtm"), this.internalEnlistment.EnlistmentTraceId, EnlistmentCallback.Aborted);
			}
			lock (this.internalEnlistment.SyncRoot)
			{
				this.internalEnlistment.State.Aborted(this.internalEnlistment, e);
			}
			if (DiagnosticTrace.Verbose)
			{
				MethodExitedTraceRecord.Trace(SR.GetString("TraceSourceLtm"), "SinglePhaseEnlistment.Aborted");
			}
		}

		// Token: 0x060001EB RID: 491 RVA: 0x00030BA4 File Offset: 0x0002FFA4
		public void Committed()
		{
			if (DiagnosticTrace.Verbose)
			{
				MethodEnteredTraceRecord.Trace(SR.GetString("TraceSourceLtm"), "SinglePhaseEnlistment.Committed");
				EnlistmentCallbackPositiveTraceRecord.Trace(SR.GetString("TraceSourceLtm"), this.internalEnlistment.EnlistmentTraceId, EnlistmentCallback.Committed);
			}
			lock (this.internalEnlistment.SyncRoot)
			{
				this.internalEnlistment.State.Committed(this.internalEnlistment);
			}
			if (DiagnosticTrace.Verbose)
			{
				MethodExitedTraceRecord.Trace(SR.GetString("TraceSourceLtm"), "SinglePhaseEnlistment.Committed");
			}
		}

		// Token: 0x060001EC RID: 492 RVA: 0x00030C54 File Offset: 0x00030054
		public void InDoubt()
		{
			if (DiagnosticTrace.Verbose)
			{
				MethodEnteredTraceRecord.Trace(SR.GetString("TraceSourceLtm"), "SinglePhaseEnlistment.InDoubt");
			}
			lock (this.internalEnlistment.SyncRoot)
			{
				if (DiagnosticTrace.Warning)
				{
					EnlistmentCallbackNegativeTraceRecord.Trace(SR.GetString("TraceSourceLtm"), this.internalEnlistment.EnlistmentTraceId, EnlistmentCallback.InDoubt);
				}
				this.internalEnlistment.State.InDoubt(this.internalEnlistment, null);
			}
			if (DiagnosticTrace.Verbose)
			{
				MethodExitedTraceRecord.Trace(SR.GetString("TraceSourceLtm"), "SinglePhaseEnlistment.InDoubt");
			}
		}

		// Token: 0x060001ED RID: 493 RVA: 0x00030D14 File Offset: 0x00030114
		public void InDoubt(Exception e)
		{
			if (DiagnosticTrace.Verbose)
			{
				MethodEnteredTraceRecord.Trace(SR.GetString("TraceSourceLtm"), "SinglePhaseEnlistment.InDoubt");
			}
			lock (this.internalEnlistment.SyncRoot)
			{
				if (DiagnosticTrace.Warning)
				{
					EnlistmentCallbackNegativeTraceRecord.Trace(SR.GetString("TraceSourceLtm"), this.internalEnlistment.EnlistmentTraceId, EnlistmentCallback.InDoubt);
				}
				this.internalEnlistment.State.InDoubt(this.internalEnlistment, e);
			}
			if (DiagnosticTrace.Verbose)
			{
				MethodExitedTraceRecord.Trace(SR.GetString("TraceSourceLtm"), "SinglePhaseEnlistment.InDoubt");
			}
		}
	}
}
