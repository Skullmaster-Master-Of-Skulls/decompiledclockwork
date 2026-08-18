using System;

namespace TechnoPro.Common.Public.Entities.ClockWorkAudit
{
	// Token: 0x02000464 RID: 1124
	public class ClockWorkAuditTypeAttribute : Attribute
	{
		// Token: 0x06002237 RID: 8759 RVA: 0x0000EC26 File Offset: 0x0000CE26
		public ClockWorkAuditTypeAttribute()
		{
		}

		// Token: 0x06002238 RID: 8760 RVA: 0x0002638F File Offset: 0x0002458F
		public ClockWorkAuditTypeAttribute(string title, string description)
		{
			this.Title = title;
			this.Description = description;
		}

		// Token: 0x17000E22 RID: 3618
		// (get) Token: 0x06002239 RID: 8761 RVA: 0x000263A9 File Offset: 0x000245A9
		// (set) Token: 0x0600223A RID: 8762 RVA: 0x000263B1 File Offset: 0x000245B1
		public string Title { get; set; }

		// Token: 0x17000E23 RID: 3619
		// (get) Token: 0x0600223B RID: 8763 RVA: 0x000263BA File Offset: 0x000245BA
		// (set) Token: 0x0600223C RID: 8764 RVA: 0x000263C2 File Offset: 0x000245C2
		public string Description { get; set; }

		// Token: 0x17000E24 RID: 3620
		// (get) Token: 0x0600223D RID: 8765 RVA: 0x000263CB File Offset: 0x000245CB
		// (set) Token: 0x0600223E RID: 8766 RVA: 0x000263D3 File Offset: 0x000245D3
		public bool IsDisabled { get; set; }
	}
}
