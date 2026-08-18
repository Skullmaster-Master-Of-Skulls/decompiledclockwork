using System;

namespace TechnoPro.Common.Public.Entities.Workflows
{
	// Token: 0x020000F7 RID: 247
	public class WorkflowTypeAttribute : Attribute
	{
		// Token: 0x060005BB RID: 1467 RVA: 0x0000EC26 File Offset: 0x0000CE26
		public WorkflowTypeAttribute()
		{
		}

		// Token: 0x060005BC RID: 1468 RVA: 0x0000EC30 File Offset: 0x0000CE30
		public WorkflowTypeAttribute(string code)
		{
			this.Code = code;
		}

		// Token: 0x170001FE RID: 510
		// (get) Token: 0x060005BD RID: 1469 RVA: 0x0000EC42 File Offset: 0x0000CE42
		// (set) Token: 0x060005BE RID: 1470 RVA: 0x0000EC4A File Offset: 0x0000CE4A
		public string Code { get; set; }
	}
}
