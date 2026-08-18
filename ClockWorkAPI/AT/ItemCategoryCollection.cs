using System;
using System.Collections;

namespace ClockWorkAPI.AT
{
	// Token: 0x02000090 RID: 144
	public class ItemCategoryCollection : CollectionBase
	{
		// Token: 0x0600072D RID: 1837 RVA: 0x00028EA0 File Offset: 0x00027EA0
		public ItemCategory AddIterative(string dotNotation)
		{
			return this.FindIterative(dotNotation, true);
		}

		// Token: 0x0600072E RID: 1838 RVA: 0x00028EBC File Offset: 0x00027EBC
		public ItemCategory FindIterative(string dotNotation)
		{
			return this.FindIterative(dotNotation, false);
		}

		// Token: 0x0600072F RID: 1839 RVA: 0x00028ED8 File Offset: 0x00027ED8
		public ItemCategory FindIterative(string dotNotation, bool createMissingNodes)
		{
			string[] array = dotNotation.Split(new char[]
			{
				'.'
			});
			ItemCategoryCollection itemCategoryCollection = this;
			for (int i = 0; i < array.Length; i++)
			{
				string text = array[i];
				ItemCategory itemCategory = itemCategoryCollection.FindOneLevel(text);
				if (itemCategory == null)
				{
					if (!createMissingNodes)
					{
						return null;
					}
					itemCategory = itemCategoryCollection.Add(text);
					itemCategoryCollection = itemCategory.ChildCategories;
				}
				else
				{
					if (i == array.Length - 1)
					{
						return itemCategory;
					}
					itemCategoryCollection = itemCategory.ChildCategories;
				}
			}
			return null;
		}

		// Token: 0x06000730 RID: 1840 RVA: 0x00028F80 File Offset: 0x00027F80
		public ItemCategory FindOneLevel(string categoryTitle)
		{
			foreach (object obj in base.List)
			{
				ItemCategory itemCategory = (ItemCategory)obj;
				if (itemCategory.Equals(categoryTitle))
				{
					return itemCategory;
				}
			}
			return null;
		}

		// Token: 0x06000731 RID: 1841 RVA: 0x00028FFC File Offset: 0x00027FFC
		public ItemCategory Add(ItemCategory category)
		{
			int index = base.List.Add(category);
			return (ItemCategory)base.List[index];
		}

		// Token: 0x06000732 RID: 1842 RVA: 0x0002902C File Offset: 0x0002802C
		public ItemCategory Add(string title)
		{
			return this.Add(new ItemCategory(title));
		}

		// Token: 0x06000733 RID: 1843 RVA: 0x0002904C File Offset: 0x0002804C
		public override string ToString()
		{
			string text = "";
			foreach (object obj in base.List)
			{
				ItemCategory itemCategory = (ItemCategory)obj;
				text = text + itemCategory.Title + " (";
				foreach (object obj2 in itemCategory.ChildCategories)
				{
					ItemCategory itemCategory2 = (ItemCategory)obj2;
					text = text + itemCategory2.Title + ", ";
				}
				text += "), ";
			}
			return text;
		}
	}
}
