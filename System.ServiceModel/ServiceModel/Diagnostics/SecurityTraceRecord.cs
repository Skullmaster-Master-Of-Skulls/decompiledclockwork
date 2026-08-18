using System;
using System.Runtime.Diagnostics;

namespace System.ServiceModel.Diagnostics
{
	// Token: 0x02000A8F RID: 2703
	internal class SecurityTraceRecord : TraceRecord
	{
		// Token: 0x06006ABF RID: 27327 RVA: 0x0018E04A File Offset: 0x0018C24A
		internal SecurityTraceRecord(string traceName)
		{
			if (string.IsNullOrEmpty(traceName))
			{
				this.traceName = "Empty";
				return;
			}
			this.traceName = traceName;
		}

		// Token: 0x17001968 RID: 6504
		// (get) Token: 0x06006AC0 RID: 27328 RVA: 0x0018E06D File Offset: 0x0018C26D
		internal override string EventId
		{
			get
			{
				return base.BuildEventId(this.traceName);
			}
		}

		// Token: 0x04003CC8 RID: 15560
		private string traceName;
	}
}
