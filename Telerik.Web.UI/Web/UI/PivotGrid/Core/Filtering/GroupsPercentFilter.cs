using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Telerik.Web.UI.PivotGrid.Core.Filtering
{
	// Token: 0x02000CD0 RID: 3280
	[DataContract]
	public sealed class GroupsPercentFilter : SortedGroupsFilter, IGroupsPercentFilter, ITopGroupsFilter
	{
		// Token: 0x06007A9B RID: 31387 RVA: 0x001C20C2 File Offset: 0x001C02C2
		public GroupsPercentFilter()
		{
			this.percent = 0.2;
		}

		// Token: 0x17002759 RID: 10073
		// (get) Token: 0x06007A9C RID: 31388 RVA: 0x001C20D9 File Offset: 0x001C02D9
		// (set) Token: 0x06007A9D RID: 31389 RVA: 0x001C20E4 File Offset: 0x001C02E4
		[DataMember]
		public double Percent
		{
			get
			{
				return this.percent;
			}
			set
			{
				double num = Math.Max(0.0, Math.Min(1.0, value));
				if (this.percent != num)
				{
					this.percent = num;
					base.OnPropertyChanged("Percent");
				}
			}
		}

		// Token: 0x06007A9E RID: 31390 RVA: 0x001C212C File Offset: 0x001C032C
		internal override ICollection<IGroup> SelectGroups(IList<SortedGroupsFilter.GroupAndGrandTotal> list, AggregateValue total)
		{
			double num;
			SortedGroupsFilter.TryGetDouble(total, out num);
			double num2 = num * this.Percent;
			double num3 = 0.0;
			bool flag = num3 < num;
			HashSet<IGroup> hashSet = new HashSet<IGroup>();
			foreach (SortedGroupsFilter.GroupAndGrandTotal groupAndGrandTotal in list)
			{
				hashSet.Add(groupAndGrandTotal.Group);
				double num4;
				SortedGroupsFilter.TryGetDouble(groupAndGrandTotal.GrandTotal, out num4);
				num3 += num4;
				if (flag != num3 < num2)
				{
					break;
				}
			}
			return hashSet;
		}

		// Token: 0x06007A9F RID: 31391 RVA: 0x001C21C8 File Offset: 0x001C03C8
		protected override Cloneable CreateInstanceCore()
		{
			return new GroupsPercentFilter();
		}

		// Token: 0x06007AA0 RID: 31392 RVA: 0x001C21D0 File Offset: 0x001C03D0
		protected override void CloneCore(Cloneable source)
		{
			base.CloneCore(source);
			GroupsPercentFilter groupsPercentFilter = source as GroupsPercentFilter;
			if (groupsPercentFilter != null)
			{
				this.Percent = groupsPercentFilter.Percent;
			}
		}

		// Token: 0x04002195 RID: 8597
		private double percent;
	}
}
