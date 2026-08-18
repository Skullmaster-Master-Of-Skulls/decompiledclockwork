using System;
using System.Collections.Generic;

namespace Telerik.Web.UI.PivotGrid.Core
{
	// Token: 0x02000CD7 RID: 3287
	internal class GroupComparerDecorator : IComparer<IGroup>
	{
		// Token: 0x06007ACF RID: 31439 RVA: 0x001C2C57 File Offset: 0x001C0E57
		public GroupComparerDecorator(GroupComparer groupComparer, SortOrder sortOrder, IAggregateResultProvider results, PivotAxis axis)
		{
			this.groupComparer = groupComparer;
			this.results = results;
			this.axis = axis;
			this.sortOrderMultiplier = ((sortOrder == SortOrder.Descending) ? -1 : 1);
		}

		// Token: 0x06007AD0 RID: 31440 RVA: 0x001C2C83 File Offset: 0x001C0E83
		public int Compare(IGroup x, IGroup y)
		{
			return this.groupComparer.CompareGroups(this.results, x, y, this.axis) * this.sortOrderMultiplier;
		}

		// Token: 0x040021A0 RID: 8608
		private GroupComparer groupComparer;

		// Token: 0x040021A1 RID: 8609
		private IAggregateResultProvider results;

		// Token: 0x040021A2 RID: 8610
		private PivotAxis axis;

		// Token: 0x040021A3 RID: 8611
		private int sortOrderMultiplier;
	}
}
