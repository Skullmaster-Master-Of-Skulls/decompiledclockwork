using System;
using System.Collections.Generic;

namespace TechnoPro.Common.Public.Entities.Reports
{
	// Token: 0x02000225 RID: 549
	public class ReportFunction : BusinessBase<int>
	{
		// Token: 0x170006EB RID: 1771
		// (get) Token: 0x060010D9 RID: 4313 RVA: 0x00017C14 File Offset: 0x00015E14
		// (set) Token: 0x060010DA RID: 4314 RVA: 0x0000E258 File Offset: 0x0000C458
		public virtual int ReportFunctionId
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

		// Token: 0x170006EC RID: 1772
		// (get) Token: 0x060010DB RID: 4315 RVA: 0x00017C2C File Offset: 0x00015E2C
		// (set) Token: 0x060010DC RID: 4316 RVA: 0x00017C34 File Offset: 0x00015E34
		public eFunctionType FunctionCode { get; set; }

		// Token: 0x170006ED RID: 1773
		// (get) Token: 0x060010DD RID: 4317 RVA: 0x00017C3D File Offset: 0x00015E3D
		// (set) Token: 0x060010DE RID: 4318 RVA: 0x00017C45 File Offset: 0x00015E45
		public string Title { get; set; }

		// Token: 0x170006EE RID: 1774
		// (get) Token: 0x060010DF RID: 4319 RVA: 0x00017C4E File Offset: 0x00015E4E
		// (set) Token: 0x060010E0 RID: 4320 RVA: 0x00017C56 File Offset: 0x00015E56
		public string Description { get; set; }

		// Token: 0x170006EF RID: 1775
		// (get) Token: 0x060010E1 RID: 4321 RVA: 0x00017C5F File Offset: 0x00015E5F
		// (set) Token: 0x060010E2 RID: 4322 RVA: 0x00017C67 File Offset: 0x00015E67
		public string ExampleUsage { get; set; }

		// Token: 0x170006F0 RID: 1776
		// (get) Token: 0x060010E3 RID: 4323 RVA: 0x00017C70 File Offset: 0x00015E70
		// (set) Token: 0x060010E4 RID: 4324 RVA: 0x00017C78 File Offset: 0x00015E78
		public IList<ReportParameter> FunctionParameters { get; set; }

		// Token: 0x170006F1 RID: 1777
		// (get) Token: 0x060010E5 RID: 4325 RVA: 0x00017C81 File Offset: 0x00015E81
		// (set) Token: 0x060010E6 RID: 4326 RVA: 0x00017C89 File Offset: 0x00015E89
		public int OrderNum { get; set; }

		// Token: 0x170006F2 RID: 1778
		// (get) Token: 0x060010E7 RID: 4327 RVA: 0x00017C92 File Offset: 0x00015E92
		// (set) Token: 0x060010E8 RID: 4328 RVA: 0x00017C9A File Offset: 0x00015E9A
		public bool ExecuteThisFunctionOnClientIfPossible { get; set; }
	}
}
