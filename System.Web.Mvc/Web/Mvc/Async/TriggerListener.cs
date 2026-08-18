using System;
using System.Threading;

namespace System.Web.Mvc.Async
{
	// Token: 0x02000123 RID: 291
	internal sealed class TriggerListener
	{
		// Token: 0x060007AC RID: 1964 RVA: 0x00014C92 File Offset: 0x00012E92
		public TriggerListener()
		{
			this._activateTrigger = this.CreateTrigger();
			this._setContinuationTrigger = this.CreateTrigger();
		}

		// Token: 0x060007AD RID: 1965 RVA: 0x00014CBD File Offset: 0x00012EBD
		public void Activate()
		{
			this._activateTrigger.Fire();
		}

		// Token: 0x060007AE RID: 1966 RVA: 0x00014CEC File Offset: 0x00012EEC
		public Trigger CreateTrigger()
		{
			Interlocked.Increment(ref this._outstandingTriggers);
			SingleEntryGate triggerFiredGate = new SingleEntryGate();
			return new Trigger(delegate()
			{
				if (triggerFiredGate.TryEnter())
				{
					this.HandleTriggerFired();
				}
			});
		}

		// Token: 0x060007AF RID: 1967 RVA: 0x00014D2E File Offset: 0x00012F2E
		private void HandleTriggerFired()
		{
			if (Interlocked.Decrement(ref this._outstandingTriggers) == 0 && this._continuationFiredGate.TryEnter())
			{
				this._continuation();
			}
		}

		// Token: 0x060007B0 RID: 1968 RVA: 0x00014D57 File Offset: 0x00012F57
		public void SetContinuation(Action continuation)
		{
			if (continuation != null)
			{
				this._continuation = continuation;
				this._setContinuationTrigger.Fire();
			}
		}

		// Token: 0x04000223 RID: 547
		private readonly Trigger _activateTrigger;

		// Token: 0x04000224 RID: 548
		private readonly SingleEntryGate _continuationFiredGate = new SingleEntryGate();

		// Token: 0x04000225 RID: 549
		private readonly Trigger _setContinuationTrigger;

		// Token: 0x04000226 RID: 550
		private volatile Action _continuation;

		// Token: 0x04000227 RID: 551
		private int _outstandingTriggers;
	}
}
