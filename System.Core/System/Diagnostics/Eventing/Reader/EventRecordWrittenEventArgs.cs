using System;
using System.Security.Permissions;

namespace System.Diagnostics.Eventing.Reader
{
	// Token: 0x020002B9 RID: 697
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	public sealed class EventRecordWrittenEventArgs : EventArgs
	{
		// Token: 0x06001961 RID: 6497 RVA: 0x0005C9E1 File Offset: 0x0005ABE1
		internal EventRecordWrittenEventArgs(EventLogRecord record)
		{
			this.record = record;
		}

		// Token: 0x06001962 RID: 6498 RVA: 0x0005C9F0 File Offset: 0x0005ABF0
		internal EventRecordWrittenEventArgs(Exception exception)
		{
			this.exception = exception;
		}

		// Token: 0x170004AA RID: 1194
		// (get) Token: 0x06001963 RID: 6499 RVA: 0x0005C9FF File Offset: 0x0005ABFF
		public EventRecord EventRecord
		{
			get
			{
				return this.record;
			}
		}

		// Token: 0x170004AB RID: 1195
		// (get) Token: 0x06001964 RID: 6500 RVA: 0x0005CA07 File Offset: 0x0005AC07
		public Exception EventException
		{
			get
			{
				return this.exception;
			}
		}

		// Token: 0x04000C67 RID: 3175
		private EventRecord record;

		// Token: 0x04000C68 RID: 3176
		private Exception exception;
	}
}
