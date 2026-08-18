using System;
using System.Collections;
using System.Data;

namespace DynamicScreens
{
	// Token: 0x0200003F RID: 63
	public class DynamicListGroup : CollectionBase
	{
		// Token: 0x17000111 RID: 273
		// (get) Token: 0x060003B7 RID: 951 RVA: 0x00033410 File Offset: 0x00032410
		public ModificationType HowModified
		{
			get
			{
				return this.howModified;
			}
		}

		// Token: 0x17000112 RID: 274
		// (get) Token: 0x060003B8 RID: 952 RVA: 0x00033428 File Offset: 0x00032428
		// (set) Token: 0x060003B9 RID: 953 RVA: 0x00033440 File Offset: 0x00032440
		public string Description
		{
			get
			{
				return this.description;
			}
			set
			{
				this.description = value;
				if (this.howModified == ModificationType.Unchanged)
				{
					this.howModified = ModificationType.Modified;
				}
			}
		}

		// Token: 0x17000113 RID: 275
		// (get) Token: 0x060003BA RID: 954 RVA: 0x0003346C File Offset: 0x0003246C
		public int LookupGroupId
		{
			get
			{
				return this.lookupGroupId;
			}
		}

		// Token: 0x060003BB RID: 955 RVA: 0x00033484 File Offset: 0x00032484
		public override string ToString()
		{
			return this.description;
		}

		// Token: 0x060003BC RID: 956 RVA: 0x0003349C File Offset: 0x0003249C
		public DynamicListGroup(DataRow dr)
		{
			this.lookupGroupId = (int)dr["lookupgroupid"];
			this.description = (string)dr["description"];
			this.sortBy = (int)dr["sortby"];
			if (this.sortBy > this.biggestSortBy)
			{
				this.biggestSortBy = this.sortBy;
			}
		}

		// Token: 0x060003BD RID: 957 RVA: 0x00033524 File Offset: 0x00032524
		public DynamicListGroup(string description)
		{
			this.lookupGroupId = DynamicListGroup.newLookupGroupId--;
			this.description = description;
			this.biggestSortBy++;
			this.sortBy = this.biggestSortBy;
			this.childList = 0;
			this.howModified = ModificationType.Added;
		}

		// Token: 0x060003BE RID: 958 RVA: 0x0003358C File Offset: 0x0003258C
		public int Add(DynamicListItem listItem)
		{
			return base.List.Add(listItem);
		}

		// Token: 0x17000114 RID: 276
		public DynamicListItem this[int index]
		{
			get
			{
				return (DynamicListItem)base.List[index];
			}
			set
			{
				base.List[index] = value;
			}
		}

		// Token: 0x04000292 RID: 658
		private int lookupGroupId;

		// Token: 0x04000293 RID: 659
		private string description;

		// Token: 0x04000294 RID: 660
		private int sortBy;

		// Token: 0x04000295 RID: 661
		private int childList;

		// Token: 0x04000296 RID: 662
		private int biggestSortBy = 0;

		// Token: 0x04000297 RID: 663
		private ModificationType howModified = ModificationType.Unchanged;

		// Token: 0x04000298 RID: 664
		private static int newLookupGroupId = -1;
	}
}
