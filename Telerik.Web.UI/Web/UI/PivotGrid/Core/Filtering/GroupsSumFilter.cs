using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Telerik.Web.UI.PivotGrid.Core.Filtering
{
	// Token: 0x02000CD1 RID: 3281
	[DataContract]
	public sealed class GroupsSumFilter : SortedGroupsFilter, IGroupsSumFilter, ITopGroupsFilter
	{
		// Token: 0x1700275A RID: 10074
		// (get) Token: 0x06007AA2 RID: 31394 RVA: 0x001C2202 File Offset: 0x001C0402
		// (set) Token: 0x06007AA3 RID: 31395 RVA: 0x001C220C File Offset: 0x001C040C
		[DataMember]
		public double Sum
		{
			get
			{
				return this.sum;
			}
			set
			{
				double num = Math.Max(0.0, value);
				if (this.sum != num)
				{
					this.sum = num;
					base.OnPropertyChanged("Sum");
				}
			}
		}

		// Token: 0x06007AA4 RID: 31396 RVA: 0x001C2244 File Offset: 0x001C0444
		internal override ICollection<IGroup> SelectGroups(IList<SortedGroupsFilter.GroupAndGrandTotal> list, AggregateValue total)
		{
			double num;
			SortedGroupsFilter.TryGetDouble(total, out num);
			double num2 = this.Sum;
			double num3 = 0.0;
			HashSet<IGroup> hashSet = new HashSet<IGroup>();
			foreach (SortedGroupsFilter.GroupAndGrandTotal groupAndGrandTotal in list)
			{
				hashSet.Add(groupAndGrandTotal.Group);
				double num4;
				SortedGroupsFilter.TryGetDouble(groupAndGrandTotal.GrandTotal, out num4);
				num3 += num4;
				if (num3 >= num2)
				{
					break;
				}
			}
			return hashSet;
		}

		// Token: 0x06007AA5 RID: 31397 RVA: 0x001C22D4 File Offset: 0x001C04D4
		protected override Cloneable CreateInstanceCore()
		{
			return new GroupsSumFilter();
		}

		// Token: 0x06007AA6 RID: 31398 RVA: 0x001C22DC File Offset: 0x001C04DC
		protected override void CloneCore(Cloneable source)
		{
			base.CloneCore(source);
			GroupsSumFilter groupsSumFilter = source as GroupsSumFilter;
			if (groupsSumFilter != null)
			{
				this.Sum = groupsSumFilter.Sum;
			}
		}

		// Token: 0x04002196 RID: 8598
		private double sum;
	}
}
