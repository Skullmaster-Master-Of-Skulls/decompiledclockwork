using System;

namespace System.Web.Management
{
	// Token: 0x02000197 RID: 407
	public class WebSuccessAuditEvent : WebAuditEvent
	{
		// Token: 0x060015AD RID: 5549 RVA: 0x00042AB8 File Offset: 0x00040CB8
		protected internal WebSuccessAuditEvent(string message, object eventSource, int eventCode) : base(message, eventSource, eventCode)
		{
		}

		// Token: 0x060015AE RID: 5550 RVA: 0x00042AC3 File Offset: 0x00040CC3
		protected internal WebSuccessAuditEvent(string message, object eventSource, int eventCode, int eventDetailCode) : base(message, eventSource, eventCode, eventDetailCode)
		{
		}

		// Token: 0x060015AF RID: 5551 RVA: 0x00042AD0 File Offset: 0x00040CD0
		internal WebSuccessAuditEvent()
		{
		}

		// Token: 0x060015B0 RID: 5552 RVA: 0x00042CEB File Offset: 0x00040EEB
		protected internal override void IncrementPerfCounters()
		{
			base.IncrementPerfCounters();
			PerfCounters.IncrementCounter(AppPerfCounter.AUDIT_SUCCESS);
			PerfCounters.IncrementGlobalCounter(GlobalPerfCounter.GLOBAL_AUDIT_SUCCESS);
		}
	}
}
