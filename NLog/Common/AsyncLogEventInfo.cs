using System;

namespace NLog.Common
{
	// Token: 0x02000026 RID: 38
	public struct AsyncLogEventInfo
	{
		// Token: 0x06000066 RID: 102 RVA: 0x000029E2 File Offset: 0x00000BE2
		public AsyncLogEventInfo(LogEventInfo logEvent, AsyncContinuation continuation)
		{
			this = default(AsyncLogEventInfo);
			this.LogEvent = logEvent;
			this.Continuation = continuation;
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000067 RID: 103 RVA: 0x000029F9 File Offset: 0x00000BF9
		// (set) Token: 0x06000068 RID: 104 RVA: 0x00002A01 File Offset: 0x00000C01
		public LogEventInfo LogEvent { get; private set; }

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000069 RID: 105 RVA: 0x00002A0A File Offset: 0x00000C0A
		// (set) Token: 0x0600006A RID: 106 RVA: 0x00002A12 File Offset: 0x00000C12
		public AsyncContinuation Continuation { get; internal set; }

		// Token: 0x0600006B RID: 107 RVA: 0x00002A1B File Offset: 0x00000C1B
		public static bool operator ==(AsyncLogEventInfo eventInfo1, AsyncLogEventInfo eventInfo2)
		{
			return object.ReferenceEquals(eventInfo1.Continuation, eventInfo2.Continuation) && object.ReferenceEquals(eventInfo1.LogEvent, eventInfo2.LogEvent);
		}

		// Token: 0x0600006C RID: 108 RVA: 0x00002A47 File Offset: 0x00000C47
		public static bool operator !=(AsyncLogEventInfo eventInfo1, AsyncLogEventInfo eventInfo2)
		{
			return !object.ReferenceEquals(eventInfo1.Continuation, eventInfo2.Continuation) || !object.ReferenceEquals(eventInfo1.LogEvent, eventInfo2.LogEvent);
		}

		// Token: 0x0600006D RID: 109 RVA: 0x00002A78 File Offset: 0x00000C78
		public override bool Equals(object obj)
		{
			AsyncLogEventInfo eventInfo = (AsyncLogEventInfo)obj;
			return this == eventInfo;
		}

		// Token: 0x0600006E RID: 110 RVA: 0x00002A98 File Offset: 0x00000C98
		public override int GetHashCode()
		{
			return this.LogEvent.GetHashCode() ^ this.Continuation.GetHashCode();
		}
	}
}
