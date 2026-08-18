using System;
using System.Security.Permissions;

namespace System.Diagnostics.Eventing.Reader
{
	// Token: 0x020002B0 RID: 688
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	public sealed class EventLogStatus
	{
		// Token: 0x060018E9 RID: 6377 RVA: 0x0005B352 File Offset: 0x00059552
		internal EventLogStatus(string channelName, int win32ErrorCode)
		{
			this.channelName = channelName;
			this.win32ErrorCode = win32ErrorCode;
		}

		// Token: 0x1700046A RID: 1130
		// (get) Token: 0x060018EA RID: 6378 RVA: 0x0005B368 File Offset: 0x00059568
		public string LogName
		{
			get
			{
				return this.channelName;
			}
		}

		// Token: 0x1700046B RID: 1131
		// (get) Token: 0x060018EB RID: 6379 RVA: 0x0005B370 File Offset: 0x00059570
		public int StatusCode
		{
			get
			{
				return this.win32ErrorCode;
			}
		}

		// Token: 0x04000C32 RID: 3122
		private string channelName;

		// Token: 0x04000C33 RID: 3123
		private int win32ErrorCode;
	}
}
