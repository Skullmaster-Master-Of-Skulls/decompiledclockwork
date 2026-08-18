using System;
using System.Collections.Generic;

namespace Telerik.Web.UI.PivotGrid.Core.ReportFilter
{
	// Token: 0x0200071E RID: 1822
	internal class LocalDistinctFilterValuesProvider : DistinctValuesProvider
	{
		// Token: 0x060040B4 RID: 16564 RVA: 0x000CBEDA File Offset: 0x000CA0DA
		public LocalDistinctFilterValuesProvider(IDataProvider provider, FilterDescription filterDescription)
		{
			if (provider == null)
			{
				throw new ArgumentNullException("provider");
			}
			if (filterDescription == null)
			{
				throw new ArgumentNullException("filterDescription");
			}
			this.provider = provider;
			this.filterDescription = filterDescription;
			this.disctinctValues = new List<object>();
		}

		// Token: 0x17001528 RID: 5416
		// (get) Token: 0x060040B5 RID: 16565 RVA: 0x000CBF17 File Offset: 0x000CA117
		public override IEnumerable<object> DisctinctValues
		{
			get
			{
				return this.disctinctValues;
			}
		}

		// Token: 0x060040B6 RID: 16566 RVA: 0x000CBF20 File Offset: 0x000CA120
		public override void Refresh()
		{
			int num = this.provider.Settings.FilterDescriptions.IndexOf(this.filterDescription);
			if (num < 0)
			{
				return;
			}
			IEnumerable<object> uniqueFilterItems = this.provider.Results.GetUniqueFilterItems(num);
			if (uniqueFilterItems != null)
			{
				this.disctinctValues = uniqueFilterItems;
			}
			base.OnUpdated();
		}

		// Token: 0x04001126 RID: 4390
		private readonly IDataProvider provider;

		// Token: 0x04001127 RID: 4391
		private readonly FilterDescription filterDescription;

		// Token: 0x04001128 RID: 4392
		private IEnumerable<object> disctinctValues;
	}
}
