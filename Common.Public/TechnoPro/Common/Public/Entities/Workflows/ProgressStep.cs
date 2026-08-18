using System;

namespace TechnoPro.Common.Public.Entities.Workflows
{
	// Token: 0x020000F8 RID: 248
	public class ProgressStep : BusinessBase<Guid>
	{
		// Token: 0x170001FF RID: 511
		// (get) Token: 0x060005BF RID: 1471 RVA: 0x0000EC54 File Offset: 0x0000CE54
		// (set) Token: 0x060005C0 RID: 1472 RVA: 0x0000EC6C File Offset: 0x0000CE6C
		public virtual Guid ProgressStepId
		{
			get
			{
				return this.Id;
			}
			set
			{
				this.Id = value;
			}
		}

		// Token: 0x17000200 RID: 512
		// (get) Token: 0x060005C1 RID: 1473 RVA: 0x0000EC77 File Offset: 0x0000CE77
		// (set) Token: 0x060005C2 RID: 1474 RVA: 0x0000EC7F File Offset: 0x0000CE7F
		public eWorkflowType WorkflowType { get; set; }

		// Token: 0x17000201 RID: 513
		// (get) Token: 0x060005C3 RID: 1475 RVA: 0x0000EC88 File Offset: 0x0000CE88
		// (set) Token: 0x060005C4 RID: 1476 RVA: 0x0000EC90 File Offset: 0x0000CE90
		public string Title { get; set; }

		// Token: 0x17000202 RID: 514
		// (get) Token: 0x060005C5 RID: 1477 RVA: 0x0000EC99 File Offset: 0x0000CE99
		// (set) Token: 0x060005C6 RID: 1478 RVA: 0x0000ECA1 File Offset: 0x0000CEA1
		public string Description { get; set; }

		// Token: 0x17000203 RID: 515
		// (get) Token: 0x060005C7 RID: 1479 RVA: 0x0000ECAA File Offset: 0x0000CEAA
		// (set) Token: 0x060005C8 RID: 1480 RVA: 0x0000ECB2 File Offset: 0x0000CEB2
		public int ProgressStepNumber { get; set; }

		// Token: 0x17000204 RID: 516
		// (get) Token: 0x060005C9 RID: 1481 RVA: 0x0000ECBB File Offset: 0x0000CEBB
		// (set) Token: 0x060005CA RID: 1482 RVA: 0x0000ECC3 File Offset: 0x0000CEC3
		public int ProgressStepTotalCount { get; set; }
	}
}
