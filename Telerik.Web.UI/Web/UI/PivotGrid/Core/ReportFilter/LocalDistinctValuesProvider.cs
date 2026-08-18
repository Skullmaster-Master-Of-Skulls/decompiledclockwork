using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Telerik.Web.UI.PivotGrid.Core.ReportFilter
{
	// Token: 0x0200071F RID: 1823
	internal class LocalDistinctValuesProvider : DistinctValuesProvider
	{
		// Token: 0x060040B7 RID: 16567 RVA: 0x000CBF70 File Offset: 0x000CA170
		public LocalDistinctValuesProvider(IDataProvider provider, FilterDescription filterDescription)
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

		// Token: 0x17001529 RID: 5417
		// (get) Token: 0x060040B8 RID: 16568 RVA: 0x000CBFAD File Offset: 0x000CA1AD
		public override IEnumerable<object> DisctinctValues
		{
			get
			{
				return this.disctinctValues;
			}
		}

		// Token: 0x060040B9 RID: 16569 RVA: 0x000CBFB8 File Offset: 0x000CA1B8
		private static Type GetDistinctType(IEnumerable<object> objects)
		{
			foreach (object obj in objects)
			{
				if (obj != null)
				{
					return obj.GetType();
				}
			}
			return null;
		}

		// Token: 0x060040BA RID: 16570 RVA: 0x000CC008 File Offset: 0x000CA208
		public override void Refresh()
		{
			int num = this.provider.Settings.FilterDescriptions.IndexOf(this.filterDescription);
			if (num < 0 || this.provider.Results == null)
			{
				return;
			}
			IEnumerable<object> uniqueFilterItems = this.provider.Results.GetUniqueFilterItems(num);
			if (uniqueFilterItems != null)
			{
				this.disctinctValues = LocalDistinctValuesProvider.GetSortedDistincsValues(uniqueFilterItems);
			}
			base.OnUpdated();
		}

		// Token: 0x060040BB RID: 16571 RVA: 0x000CC06C File Offset: 0x000CA26C
		private static IEnumerable<object> GetSortedDistincsValues(IEnumerable<object> uniqueItems)
		{
			Type distinctType = LocalDistinctValuesProvider.GetDistinctType(uniqueItems);
			if (distinctType == null)
			{
				return uniqueItems;
			}
			bool flag = PivotTypeExtensions.CanSort(distinctType);
			if (flag)
			{
				Type type = typeof(Comparer<>).MakeGenericType(new Type[]
				{
					distinctType
				});
				IComparer comparer = type.GetProperty("Default").GetValue(null, null) as IComparer;
				object[] array = uniqueItems.ToArray<object>();
				Array.Sort(array, comparer);
				uniqueItems = array.ToList<object>();
			}
			return uniqueItems;
		}

		// Token: 0x04001129 RID: 4393
		private readonly IDataProvider provider;

		// Token: 0x0400112A RID: 4394
		private readonly FilterDescription filterDescription;

		// Token: 0x0400112B RID: 4395
		private IEnumerable<object> disctinctValues;
	}
}
