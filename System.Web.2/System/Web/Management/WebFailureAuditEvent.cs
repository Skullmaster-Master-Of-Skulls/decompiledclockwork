using System;

namespace System.Web.Management
{
	// Token: 0x02000194 RID: 404
	public class WebFailureAuditEvent : WebAuditEvent
	{
		// Token: 0x0600159C RID: 5532 RVA: 0x00042AB8 File Offset: 0x00040CB8
		protected internal WebFailureAuditEvent(string message, object eventSource, int eventCode) : base(message, eventSource, eventCode)
		{
		}

		// Token: 0x0600159D RID: 5533 RVA: 0x00042AC3 File Offset: 0x00040CC3
		protected internal WebFailureAuditEvent(string message, object eventSource, int eventCode, int eventDetailCode) : base(message, eventSource, eventCode, eventDetailCode)
		{
		}

		// Token: 0x0600159E RID: 5534 RVA: 0x00042AD0 File Offset: 0x00040CD0
		internal WebFailureAuditEvent()
		{
		}

		// Token: 0x0600159F RID: 5535 RVA: 0x00042AD8 File Offset: 0x00040CD8
		protected internal override void IncrementPerfCounters()
		{
			base.IncrementPerfCounters();
			PerfCounters.IncrementCounter(AppPerfCounter.AUDIT_FAIL);
			PerfCounters.IncrementGlobalCounter(GlobalPerfCounter.GLOBAL_AUDIT_FAIL);
		}
	}
}
