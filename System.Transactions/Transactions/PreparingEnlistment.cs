using System;
using System.Transactions.Diagnostics;

namespace System.Transactions
{
	// Token: 0x02000040 RID: 64
	public class PreparingEnlistment : Enlistment
	{
		// Token: 0x060001E3 RID: 483 RVA: 0x00030704 File Offset: 0x0002FB04
		internal PreparingEnlistment(InternalEnlistment enlistment) : base(enlistment)
		{
		}

		// Token: 0x060001E4 RID: 484 RVA: 0x00030724 File Offset: 0x0002FB24
		public void Prepared()
		{
			if (DiagnosticTrace.Verbose)
			{
				MethodEnteredTraceRecord.Trace(SR.GetString("TraceSourceLtm"), "PreparingEnlistment.Prepared");
				EnlistmentCallbackPositiveTraceRecord.Trace(SR.GetString("TraceSourceLtm"), this.internalEnlistment.EnlistmentTraceId, EnlistmentCallback.Prepared);
			}
			lock (this.internalEnlistment.SyncRoot)
			{
				this.internalEnlistment.State.Prepared(this.internalEnlistment);
			}
			if (DiagnosticTrace.Verbose)
			{
				MethodExitedTraceRecord.Trace(SR.GetString("TraceSourceLtm"), "PreparingEnlistment.Prepared");
			}
		}

		// Token: 0x060001E5 RID: 485 RVA: 0x000307D4 File Offset: 0x0002FBD4
		public void ForceRollback()
		{
			if (DiagnosticTrace.Verbose)
			{
				MethodEnteredTraceRecord.Trace(SR.GetString("TraceSourceLtm"), "PreparingEnlistment.ForceRollback");
			}
			if (DiagnosticTrace.Warning)
			{
				EnlistmentCallbackNegativeTraceRecord.Trace(SR.GetString("TraceSourceLtm"), this.internalEnlistment.EnlistmentTraceId, EnlistmentCallback.ForceRollback);
			}
			lock (this.internalEnlistment.SyncRoot)
			{
				this.internalEnlistment.State.ForceRollback(this.internalEnlistment, null);
			}
			if (DiagnosticTrace.Verbose)
			{
				MethodExitedTraceRecord.Trace(SR.GetString("TraceSourceLtm"), "PreparingEnlistment.ForceRollback");
			}
		}

		// Token: 0x060001E6 RID: 486 RVA: 0x00030894 File Offset: 0x0002FC94
		public void ForceRollback(Exception e)
		{
			if (DiagnosticTrace.Verbose)
			{
				MethodEnteredTraceRecord.Trace(SR.GetString("TraceSourceLtm"), "PreparingEnlistment.ForceRollback");
			}
			if (DiagnosticTrace.Warning)
			{
				EnlistmentCallbackNegativeTraceRecord.Trace(SR.GetString("TraceSourceLtm"), this.internalEnlistment.EnlistmentTraceId, EnlistmentCallback.ForceRollback);
			}
			lock (this.internalEnlistment.SyncRoot)
			{
				this.internalEnlistment.State.ForceRollback(this.internalEnlistment, e);
			}
			if (DiagnosticTrace.Verbose)
			{
				MethodExitedTraceRecord.Trace(SR.GetString("TraceSourceLtm"), "PreparingEnlistment.ForceRollback");
			}
		}

		// Token: 0x060001E7 RID: 487 RVA: 0x00030954 File Offset: 0x0002FD54
		public byte[] RecoveryInformation()
		{
			if (DiagnosticTrace.Verbose)
			{
				MethodEnteredTraceRecord.Trace(SR.GetString("TraceSourceLtm"), "PreparingEnlistment.RecoveryInformation");
			}
			byte[] result;
			try
			{
				lock (this.internalEnlistment.SyncRoot)
				{
					result = this.internalEnlistment.State.RecoveryInformation(this.internalEnlistment);
				}
			}
			finally
			{
				if (DiagnosticTrace.Verbose)
				{
					MethodExitedTraceRecord.Trace(SR.GetString("TraceSourceLtm"), "PreparingEnlistment.RecoveryInformation");
				}
			}
			return result;
		}
	}
}
