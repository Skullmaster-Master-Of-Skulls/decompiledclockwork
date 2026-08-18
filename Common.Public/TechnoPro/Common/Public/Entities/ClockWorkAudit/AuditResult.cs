using System;
using System.Collections.Generic;
using TechnoPro.Common.Public.Entities.Adapters;

namespace TechnoPro.Common.Public.Entities.ClockWorkAudit
{
	// Token: 0x02000461 RID: 1121
	public class AuditResult
	{
		// Token: 0x06002230 RID: 8752 RVA: 0x0002631C File Offset: 0x0002451C
		public AuditResult()
		{
			this.Checks = new List<AuditCheck>();
		}

		// Token: 0x06002231 RID: 8753 RVA: 0x00026332 File Offset: 0x00024532
		public AuditResult(eClockWorkAuditType auditType)
		{
			this.AuditType = auditType;
			this.Checks = new List<AuditCheck>();
		}

		// Token: 0x17000E1F RID: 3615
		// (get) Token: 0x06002232 RID: 8754 RVA: 0x00026350 File Offset: 0x00024550
		public eAuditStatus Status
		{
			get
			{
				return this.Checks.GetStatus();
			}
		}

		// Token: 0x17000E20 RID: 3616
		// (get) Token: 0x06002233 RID: 8755 RVA: 0x0002636D File Offset: 0x0002456D
		// (set) Token: 0x06002234 RID: 8756 RVA: 0x00026375 File Offset: 0x00024575
		public eClockWorkAuditType AuditType { get; set; }

		// Token: 0x17000E21 RID: 3617
		// (get) Token: 0x06002235 RID: 8757 RVA: 0x0002637E File Offset: 0x0002457E
		// (set) Token: 0x06002236 RID: 8758 RVA: 0x00026386 File Offset: 0x00024586
		public IList<AuditCheck> Checks { get; set; }
	}
}
