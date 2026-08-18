using System;
using System.Collections.Generic;

namespace TechnoPro.Common.Public.Entities.AlertTrigger
{
	// Token: 0x020005A1 RID: 1441
	public class AlertTriggerForUser
	{
		// Token: 0x170013A9 RID: 5033
		// (get) Token: 0x06002ECF RID: 11983 RVA: 0x00033888 File Offset: 0x00031A88
		// (set) Token: 0x06002ED0 RID: 11984 RVA: 0x00033890 File Offset: 0x00031A90
		public string MessageToUser { get; set; }

		// Token: 0x170013AA RID: 5034
		// (get) Token: 0x06002ED1 RID: 11985 RVA: 0x00033899 File Offset: 0x00031A99
		// (set) Token: 0x06002ED2 RID: 11986 RVA: 0x000338A1 File Offset: 0x00031AA1
		public IDictionary<string, string> Args { get; set; }

		// Token: 0x170013AB RID: 5035
		// (get) Token: 0x06002ED3 RID: 11987 RVA: 0x000338AA File Offset: 0x00031AAA
		// (set) Token: 0x06002ED4 RID: 11988 RVA: 0x000338B2 File Offset: 0x00031AB2
		public bool DontAllowAppointmentBooking { get; set; }
	}
}
