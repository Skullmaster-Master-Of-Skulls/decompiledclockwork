using System;
using Telerik.Web.UI.PivotGrid.Core.Filtering;

namespace Telerik.Web.UI
{
	// Token: 0x0200075C RID: 1884
	[Serializable]
	public abstract class PivotGridFilter : IPivotFilter
	{
		// Token: 0x170015B2 RID: 5554
		// (get) Token: 0x06004287 RID: 17031 RVA: 0x000D0594 File Offset: 0x000CE794
		// (set) Token: 0x06004288 RID: 17032 RVA: 0x000D059C File Offset: 0x000CE79C
		public string FieldName { get; set; }

		// Token: 0x06004289 RID: 17033
		public abstract GroupFilter GetDataEngineFilter();
	}
}
