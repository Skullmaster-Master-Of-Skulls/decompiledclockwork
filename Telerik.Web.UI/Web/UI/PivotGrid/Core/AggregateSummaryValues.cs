using System;
using System.Collections.Generic;

namespace Telerik.Web.UI.PivotGrid.Core
{
	// Token: 0x02000718 RID: 1816
	internal class AggregateSummaryValues : IAggregateSummaryValues
	{
		// Token: 0x170014FD RID: 5373
		// (get) Token: 0x06004071 RID: 16497 RVA: 0x000CAD92 File Offset: 0x000C8F92
		// (set) Token: 0x06004072 RID: 16498 RVA: 0x000CAD9A File Offset: 0x000C8F9A
		public int AggregateIndex { get; set; }

		// Token: 0x170014FE RID: 5374
		// (get) Token: 0x06004073 RID: 16499 RVA: 0x000CADA3 File Offset: 0x000C8FA3
		// (set) Token: 0x06004074 RID: 16500 RVA: 0x000CADAB File Offset: 0x000C8FAB
		public IAggregateResultProvider Results { get; set; }

		// Token: 0x170014FF RID: 5375
		// (get) Token: 0x06004075 RID: 16501 RVA: 0x000CADB4 File Offset: 0x000C8FB4
		// (set) Token: 0x06004076 RID: 16502 RVA: 0x000CADBC File Offset: 0x000C8FBC
		public PivotAxis Axis { get; set; }

		// Token: 0x17001500 RID: 5376
		// (get) Token: 0x06004077 RID: 16503 RVA: 0x000CADC5 File Offset: 0x000C8FC5
		// (set) Token: 0x06004078 RID: 16504 RVA: 0x000CADCD File Offset: 0x000C8FCD
		public int Level { get; set; }

		// Token: 0x17001501 RID: 5377
		// (get) Token: 0x06004079 RID: 16505 RVA: 0x000CADD6 File Offset: 0x000C8FD6
		// (set) Token: 0x0600407A RID: 16506 RVA: 0x000CADDE File Offset: 0x000C8FDE
		public Coordinate Coordinate { get; set; }

		// Token: 0x0600407B RID: 16507 RVA: 0x000CADE7 File Offset: 0x000C8FE7
		public AggregateValue GetAggregateValue(object groupName)
		{
			if (this.Axis == PivotAxis.Rows)
			{
				return this.GetRowAggregateValue(groupName);
			}
			return this.GetColumnAggregateValue(groupName);
		}

		// Token: 0x0600407C RID: 16508 RVA: 0x000CAE00 File Offset: 0x000C9000
		private AggregateValue GetRowAggregateValue(object groupName)
		{
			IGroup group = this.FindRelativeGroupByName(this.Coordinate.RowGroup, groupName);
			if (group == null)
			{
				return null;
			}
			Coordinate groups = new Coordinate(group, this.Coordinate.ColumnGroup);
			return this.Results.GetAggregateResult(this.AggregateIndex, groups);
		}

		// Token: 0x0600407D RID: 16509 RVA: 0x000CAE54 File Offset: 0x000C9054
		private AggregateValue GetColumnAggregateValue(object groupName)
		{
			IGroup group = this.FindRelativeGroupByName(this.Coordinate.ColumnGroup, groupName);
			if (group == null)
			{
				return null;
			}
			Coordinate groups = new Coordinate(this.Coordinate.RowGroup, group);
			return this.Results.GetAggregateResult(this.AggregateIndex, groups);
		}

		// Token: 0x0600407E RID: 16510 RVA: 0x000CAEA8 File Offset: 0x000C90A8
		private IGroup FindRelativeGroupByName(IGroup group, object groupName)
		{
			List<IGroup> list = new List<IGroup>();
			while (group != null)
			{
				list.Insert(0, group);
				group = group.Parent;
			}
			IGroup group2 = list[this.Level - 1];
			group2 = ((Group)group2).GetGroupByName(groupName);
			int num = this.Level + 1;
			while (num < list.Count && group2 != null)
			{
				object name = list[num].Name;
				group2 = ((Group)group2).GetGroupByName(name);
				num++;
			}
			return group2;
		}
	}
}
