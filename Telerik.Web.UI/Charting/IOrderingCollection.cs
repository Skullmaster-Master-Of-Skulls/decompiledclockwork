using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Telerik.Charting.Styles;

namespace Telerik.Charting
{
	// Token: 0x0200175A RID: 5978
	internal class IOrderingCollection : CollectionBase
	{
		// Token: 0x170046C3 RID: 18115
		public IOrdering this[int index]
		{
			get
			{
				return (IOrdering)base.List[index];
			}
			set
			{
				base.List[index] = value;
			}
		}

		// Token: 0x0600E8FE RID: 59646 RVA: 0x00345588 File Offset: 0x00343788
		[SuppressMessage("Microsoft.Usage", "CA2233:OperationsShouldNotOverflow")]
		public void AddRange(List<IOrdering> order, int afterIndex)
		{
			foreach (IOrdering value in order)
			{
				base.List.Insert(++afterIndex, value);
			}
		}

		// Token: 0x0600E8FF RID: 59647 RVA: 0x003455E4 File Offset: 0x003437E4
		[SuppressMessage("Microsoft.Usage", "CA2233:OperationsShouldNotOverflow")]
		public void AddVisibleRange(List<IOrdering> order, int afterIndex)
		{
			foreach (IOrdering ordering in order)
			{
				if (Style.IsVisible(ordering) || ordering is EmptySeriesMessage || ordering is ChartAxis)
				{
					base.List.Insert(++afterIndex, ordering);
				}
			}
		}

		// Token: 0x0600E900 RID: 59648 RVA: 0x00345658 File Offset: 0x00343858
		[SuppressMessage("Microsoft.Usage", "CA2233:OperationsShouldNotOverflow")]
		public void AddVisible(IOrdering elem, int afterIndex)
		{
			if (Style.IsVisible(elem) || elem is EmptySeriesMessage || elem is ChartAxis)
			{
				base.List.Insert(++afterIndex, elem);
			}
		}
	}
}
