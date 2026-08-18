using System;
using System.Runtime;
using System.Runtime.Diagnostics;
using System.ServiceModel.Channels;
using System.ServiceModel.Diagnostics;

namespace System.ServiceModel.Dispatcher
{
	// Token: 0x02000599 RID: 1433
	internal struct ProxyRpc
	{
		// Token: 0x06003788 RID: 14216 RVA: 0x000D62F8 File Offset: 0x000D44F8
		internal ProxyRpc(ServiceChannel channel, ProxyOperationRuntime operation, string action, object[] inputs, TimeSpan timeout)
		{
			this.Action = action;
			this.Activity = null;
			this.eventTraceActivity = null;
			this.Channel = channel;
			this.Correlation = EmptyArray.Allocate(operation.Parent.CorrelationCount);
			this.InputParameters = inputs;
			this.Operation = operation;
			this.OutputParameters = null;
			this.Request = null;
			this.Reply = null;
			this.ActivityId = Guid.Empty;
			this.ReturnValue = null;
			this.MessageVersion = channel.MessageVersion;
			this.TimeoutHelper = new TimeoutHelper(timeout);
		}

		// Token: 0x17000D39 RID: 3385
		// (get) Token: 0x06003789 RID: 14217 RVA: 0x000D6386 File Offset: 0x000D4586
		// (set) Token: 0x0600378A RID: 14218 RVA: 0x000D63A2 File Offset: 0x000D45A2
		internal EventTraceActivity EventTraceActivity
		{
			get
			{
				if (this.eventTraceActivity == null)
				{
					this.eventTraceActivity = new EventTraceActivity(false);
				}
				return this.eventTraceActivity;
			}
			set
			{
				this.eventTraceActivity = value;
			}
		}

		// Token: 0x04002946 RID: 10566
		internal readonly string Action;

		// Token: 0x04002947 RID: 10567
		internal ServiceModelActivity Activity;

		// Token: 0x04002948 RID: 10568
		internal Guid ActivityId;

		// Token: 0x04002949 RID: 10569
		internal readonly ServiceChannel Channel;

		// Token: 0x0400294A RID: 10570
		internal object[] Correlation;

		// Token: 0x0400294B RID: 10571
		internal readonly object[] InputParameters;

		// Token: 0x0400294C RID: 10572
		internal readonly ProxyOperationRuntime Operation;

		// Token: 0x0400294D RID: 10573
		internal object[] OutputParameters;

		// Token: 0x0400294E RID: 10574
		internal Message Request;

		// Token: 0x0400294F RID: 10575
		internal Message Reply;

		// Token: 0x04002950 RID: 10576
		internal object ReturnValue;

		// Token: 0x04002951 RID: 10577
		internal MessageVersion MessageVersion;

		// Token: 0x04002952 RID: 10578
		internal readonly TimeoutHelper TimeoutHelper;

		// Token: 0x04002953 RID: 10579
		private EventTraceActivity eventTraceActivity;
	}
}
