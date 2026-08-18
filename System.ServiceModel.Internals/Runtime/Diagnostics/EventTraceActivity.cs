using System;
using System.Diagnostics;
using System.Security;

namespace System.Runtime.Diagnostics
{
	// Token: 0x0200003E RID: 62
	internal class EventTraceActivity
	{
		// Token: 0x0600026A RID: 618 RVA: 0x00009DA5 File Offset: 0x00007FA5
		public EventTraceActivity(bool setOnThread = false) : this(Guid.NewGuid(), setOnThread)
		{
		}

		// Token: 0x0600026B RID: 619 RVA: 0x00009DB3 File Offset: 0x00007FB3
		public EventTraceActivity(Guid guid, bool setOnThread = false)
		{
			this.ActivityId = guid;
			if (setOnThread)
			{
				this.SetActivityIdOnThread();
			}
		}

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x0600026C RID: 620 RVA: 0x00009DCB File Offset: 0x00007FCB
		public static EventTraceActivity Empty
		{
			get
			{
				if (EventTraceActivity.empty == null)
				{
					EventTraceActivity.empty = new EventTraceActivity(Guid.Empty, false);
				}
				return EventTraceActivity.empty;
			}
		}

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x0600026D RID: 621 RVA: 0x00009DE9 File Offset: 0x00007FE9
		public static string Name
		{
			get
			{
				return "E2EActivity";
			}
		}

		// Token: 0x0600026E RID: 622 RVA: 0x00009DF0 File Offset: 0x00007FF0
		[SecuritySafeCritical]
		public static EventTraceActivity GetFromThreadOrCreate(bool clearIdOnThread = false)
		{
			Guid guid = Trace.CorrelationManager.ActivityId;
			if (guid == Guid.Empty)
			{
				guid = Guid.NewGuid();
			}
			else if (clearIdOnThread)
			{
				Trace.CorrelationManager.ActivityId = Guid.Empty;
			}
			return new EventTraceActivity(guid, false);
		}

		// Token: 0x0600026F RID: 623 RVA: 0x00009E36 File Offset: 0x00008036
		[SecuritySafeCritical]
		public static Guid GetActivityIdFromThread()
		{
			return Trace.CorrelationManager.ActivityId;
		}

		// Token: 0x06000270 RID: 624 RVA: 0x00009E42 File Offset: 0x00008042
		public void SetActivityId(Guid guid)
		{
			this.ActivityId = guid;
		}

		// Token: 0x06000271 RID: 625 RVA: 0x00009E4B File Offset: 0x0000804B
		[SecuritySafeCritical]
		private void SetActivityIdOnThread()
		{
			Trace.CorrelationManager.ActivityId = this.ActivityId;
		}

		// Token: 0x04000102 RID: 258
		public Guid ActivityId;

		// Token: 0x04000103 RID: 259
		private static EventTraceActivity empty;
	}
}
