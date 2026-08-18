using System;

namespace Telerik.Web.UI
{
	// Token: 0x0200075D RID: 1885
	[Serializable]
	public abstract class PivotGridSingleGroupFilter : PivotGridFilter, IPivotConditionFilter, IPivotFilter
	{
		// Token: 0x170015B3 RID: 5555
		// (get) Token: 0x0600428B RID: 17035 RVA: 0x000D05AD File Offset: 0x000CE7AD
		// (set) Token: 0x0600428C RID: 17036 RVA: 0x000D05B5 File Offset: 0x000CE7B5
		public IFilterCondition Condition { get; set; }
	}
}
