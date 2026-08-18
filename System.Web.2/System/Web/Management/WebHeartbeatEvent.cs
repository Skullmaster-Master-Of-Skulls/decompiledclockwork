using System;

namespace System.Web.Management
{
	// Token: 0x0200018D RID: 397
	public class WebHeartbeatEvent : WebManagementEvent
	{
		// Token: 0x06001561 RID: 5473 RVA: 0x00041F40 File Offset: 0x00040140
		protected internal WebHeartbeatEvent(string message, int eventCode) : base(message, null, eventCode)
		{
		}

		// Token: 0x06001562 RID: 5474 RVA: 0x00041F4B File Offset: 0x0004014B
		internal WebHeartbeatEvent()
		{
		}

		// Token: 0x17000655 RID: 1621
		// (get) Token: 0x06001563 RID: 5475 RVA: 0x00041F53 File Offset: 0x00040153
		public WebProcessStatistics ProcessStatistics
		{
			get
			{
				return WebHeartbeatEvent.s_procStats;
			}
		}

		// Token: 0x06001564 RID: 5476 RVA: 0x00041F5C File Offset: 0x0004015C
		internal override void FormatToString(WebEventFormatter formatter, bool includeAppInfo)
		{
			base.FormatToString(formatter, includeAppInfo);
			formatter.AppendLine(string.Empty);
			formatter.AppendLine(WebBaseEvent.FormatResourceStringWithCache("Webevent_event_process_statistics"));
			formatter.IndentationLevel++;
			WebHeartbeatEvent.s_procStats.FormatToString(formatter);
			formatter.IndentationLevel--;
		}

		// Token: 0x0400163E RID: 5694
		private static WebProcessStatistics s_procStats = new WebProcessStatistics();
	}
}
