using System;
using Telerik.Web.UI.PivotGrid.Core;
using Telerik.Web.UI.PivotGrid.Core.Filtering;

namespace Telerik.Web.UI
{
	// Token: 0x02000DB4 RID: 3508
	[Serializable]
	public class PivotGridComparisonCondition : IFilterCondition, IPivotComparisonCondition
	{
		// Token: 0x060082FA RID: 33530 RVA: 0x001DDA9C File Offset: 0x001DBC9C
		public Condition GetDataEngineFilterCondition()
		{
			return new ComparisonCondition
			{
				Than = this.Than,
				Condition = this.Condition,
				IgnoreCase = this.IgnoreCase
			};
		}

		// Token: 0x17002961 RID: 10593
		// (get) Token: 0x060082FB RID: 33531 RVA: 0x001DDAD4 File Offset: 0x001DBCD4
		// (set) Token: 0x060082FC RID: 33532 RVA: 0x001DDADC File Offset: 0x001DBCDC
		public object Than { get; set; }

		// Token: 0x17002962 RID: 10594
		// (get) Token: 0x060082FD RID: 33533 RVA: 0x001DDAE5 File Offset: 0x001DBCE5
		// (set) Token: 0x060082FE RID: 33534 RVA: 0x001DDAED File Offset: 0x001DBCED
		public Comparison Condition { get; set; }

		// Token: 0x17002963 RID: 10595
		// (get) Token: 0x060082FF RID: 33535 RVA: 0x001DDAF6 File Offset: 0x001DBCF6
		// (set) Token: 0x06008300 RID: 33536 RVA: 0x001DDAFE File Offset: 0x001DBCFE
		public bool IgnoreCase { get; set; }
	}
}
