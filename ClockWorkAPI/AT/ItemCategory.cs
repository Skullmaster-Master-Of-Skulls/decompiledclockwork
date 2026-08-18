using System;

namespace ClockWorkAPI.AT
{
	// Token: 0x02000087 RID: 135
	public class ItemCategory
	{
		// Token: 0x1700026E RID: 622
		// (get) Token: 0x060006AD RID: 1709 RVA: 0x00024DD8 File Offset: 0x00023DD8
		// (set) Token: 0x060006AE RID: 1710 RVA: 0x00024DF0 File Offset: 0x00023DF0
		public string Title
		{
			get
			{
				return this.categoryTitle;
			}
			set
			{
				this.categoryTitle = value;
			}
		}

		// Token: 0x060006AF RID: 1711 RVA: 0x00024DFA File Offset: 0x00023DFA
		public ItemCategory(string title)
		{
			this.categoryTitle = title;
			this.parentCategory = null;
			this.childCategories = new ItemCategoryCollection();
		}

		// Token: 0x1700026F RID: 623
		// (get) Token: 0x060006B0 RID: 1712 RVA: 0x00024E20 File Offset: 0x00023E20
		public ItemCategoryCollection ChildCategories
		{
			get
			{
				return this.childCategories;
			}
		}

		// Token: 0x060006B1 RID: 1713 RVA: 0x00024E38 File Offset: 0x00023E38
		public override bool Equals(object obj)
		{
			bool result;
			if (obj == null)
			{
				result = false;
			}
			else if (obj is ItemCategory)
			{
				ItemCategory itemCategory = (ItemCategory)obj;
				result = (itemCategory.Title.ToLower().Trim().CompareTo(this.categoryTitle.ToLower().Trim()) == 0);
			}
			else
			{
				result = (obj is string && this.categoryTitle.ToLower().Trim().CompareTo(((string)obj).ToLower().Trim()) == 0);
			}
			return result;
		}

		// Token: 0x17000270 RID: 624
		// (get) Token: 0x060006B2 RID: 1714 RVA: 0x00024EDC File Offset: 0x00023EDC
		public string FormatString
		{
			get
			{
				return this.Title;
			}
		}

		// Token: 0x060006B3 RID: 1715 RVA: 0x00024EF4 File Offset: 0x00023EF4
		public override string ToString()
		{
			return this.Title;
		}

		// Token: 0x04000377 RID: 887
		private string categoryTitle;

		// Token: 0x04000378 RID: 888
		private ItemCategoryCollection childCategories;

		// Token: 0x04000379 RID: 889
		private ItemCategory parentCategory;
	}
}
