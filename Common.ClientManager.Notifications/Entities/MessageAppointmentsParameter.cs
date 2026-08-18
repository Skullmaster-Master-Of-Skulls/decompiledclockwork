using System;
using System.Collections.Generic;

namespace TechnoPro.Common.ClientManager.Notifications.Entities
{
	// Token: 0x02000010 RID: 16
	public class MessageAppointmentsParameter
	{
		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000087 RID: 135 RVA: 0x0000315D File Offset: 0x0000135D
		// (set) Token: 0x06000088 RID: 136 RVA: 0x00003165 File Offset: 0x00001365
		public int AppointmentId { get; set; }

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000089 RID: 137 RVA: 0x0000316E File Offset: 0x0000136E
		// (set) Token: 0x0600008A RID: 138 RVA: 0x00003176 File Offset: 0x00001376
		public DateTime StartDate { get; set; }

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x0600008B RID: 139 RVA: 0x0000317F File Offset: 0x0000137F
		// (set) Token: 0x0600008C RID: 140 RVA: 0x00003187 File Offset: 0x00001387
		public List<int> PersonIds { get; set; }

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x0600008D RID: 141 RVA: 0x00003190 File Offset: 0x00001390
		// (set) Token: 0x0600008E RID: 142 RVA: 0x00003198 File Offset: 0x00001398
		public string Guid { get; set; }

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x0600008F RID: 143 RVA: 0x000031A1 File Offset: 0x000013A1
		// (set) Token: 0x06000090 RID: 144 RVA: 0x000031A9 File Offset: 0x000013A9
		public DateTime EndDate { get; set; }
	}
}
