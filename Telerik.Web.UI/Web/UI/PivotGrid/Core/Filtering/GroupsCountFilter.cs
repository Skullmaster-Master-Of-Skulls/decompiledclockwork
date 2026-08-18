using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Telerik.Web.UI.PivotGrid.Core.Filtering
{
	// Token: 0x02000CCF RID: 3279
	[DataContract]
	public sealed class GroupsCountFilter : SortedGroupsFilter, IGroupsCountFilter, ITopGroupsFilter
	{
		// Token: 0x06007A95 RID: 31381 RVA: 0x001C1FD9 File Offset: 0x001C01D9
		public GroupsCountFilter()
		{
			this.count = 10;
		}

		// Token: 0x17002758 RID: 10072
		// (get) Token: 0x06007A96 RID: 31382 RVA: 0x001C1FE9 File Offset: 0x001C01E9
		// (set) Token: 0x06007A97 RID: 31383 RVA: 0x001C1FF4 File Offset: 0x001C01F4
		[DataMember]
		public int Count
		{
			get
			{
				return this.count;
			}
			set
			{
				int num = Math.Max(1, value);
				if (this.count != num)
				{
					this.count = num;
					base.OnPropertyChanged("Count");
				}
			}
		}

		// Token: 0x06007A98 RID: 31384 RVA: 0x001C2024 File Offset: 0x001C0224
		internal override ICollection<IGroup> SelectGroups(IList<SortedGroupsFilter.GroupAndGrandTotal> list, AggregateValue total)
		{
			HashSet<IGroup> hashSet = new HashSet<IGroup>();
			int num = 0;
			while (num < list.Count && (num < this.Count || base.GetComparerOrDefault().Compare(list[num].GrandTotal, list[this.Count - 1].GrandTotal) == 0))
			{
				hashSet.Add(list[num].Group);
				num++;
			}
			return hashSet;
		}

		// Token: 0x06007A99 RID: 31385 RVA: 0x001C2091 File Offset: 0x001C0291
		protected override Cloneable CreateInstanceCore()
		{
			return new GroupsCountFilter();
		}

		// Token: 0x06007A9A RID: 31386 RVA: 0x001C2098 File Offset: 0x001C0298
		protected override void CloneCore(Cloneable source)
		{
			base.CloneCore(source);
			GroupsCountFilter groupsCountFilter = source as GroupsCountFilter;
			if (groupsCountFilter != null)
			{
				this.Count = groupsCountFilter.Count;
			}
		}

		// Token: 0x04002194 RID: 8596
		private int count;
	}
}
