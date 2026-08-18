using System;
using System.ComponentModel;
using System.Threading;

namespace System.ServiceModel.Discovery
{
	// Token: 0x02000052 RID: 82
	internal class SyncOperationState
	{
		// Token: 0x060003EB RID: 1003 RVA: 0x0000C696 File Offset: 0x0000A896
		public SyncOperationState()
		{
			this.waitEvent = new ManualResetEvent(false);
			this.eventArgs = null;
		}

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x060003EC RID: 1004 RVA: 0x0000C6B1 File Offset: 0x0000A8B1
		// (set) Token: 0x060003ED RID: 1005 RVA: 0x0000C6B9 File Offset: 0x0000A8B9
		public AsyncCompletedEventArgs EventArgs
		{
			get
			{
				return this.eventArgs;
			}
			set
			{
				this.eventArgs = value;
			}
		}

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x060003EE RID: 1006 RVA: 0x0000C6C2 File Offset: 0x0000A8C2
		public ManualResetEvent WaitEvent
		{
			get
			{
				return this.waitEvent;
			}
		}

		// Token: 0x04000105 RID: 261
		private AsyncCompletedEventArgs eventArgs;

		// Token: 0x04000106 RID: 262
		private ManualResetEvent waitEvent;
	}
}
