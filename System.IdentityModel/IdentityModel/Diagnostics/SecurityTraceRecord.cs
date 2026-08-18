using System;
using System.Runtime.Diagnostics;

namespace System.IdentityModel.Diagnostics
{
	// Token: 0x020001E8 RID: 488
	internal class SecurityTraceRecord : TraceRecord
	{
		// Token: 0x06001054 RID: 4180 RVA: 0x0004647C File Offset: 0x0004467C
		internal SecurityTraceRecord(string traceName)
		{
			if (string.IsNullOrEmpty(traceName))
			{
				this.traceName = "Empty";
				return;
			}
			this.traceName = traceName;
		}

		// Token: 0x17000485 RID: 1157
		// (get) Token: 0x06001055 RID: 4181 RVA: 0x0004649F File Offset: 0x0004469F
		internal override string EventId
		{
			get
			{
				return base.BuildEventId(this.traceName);
			}
		}

		// Token: 0x04000E40 RID: 3648
		private string traceName;
	}
}
